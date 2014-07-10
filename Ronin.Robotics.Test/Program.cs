using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Threading;
using System.Threading.Tasks;
using System.Management;
using Lego.Ev3.Core;
using Lego.Ev3.Desktop;

namespace Ronin.Robotics.Test
{
	sealed class Program : IDisposable
	{
		enum ConnType { Bluetooth = 0, WiFi = 1 }

		readonly Brick _brick;

		private Program(ConnType connType, string address = null)
		{
			ICommunication conn;
			switch (connType)
			{
				case ConnType.WiFi:
					conn = new NetworkCommunication(string.IsNullOrWhiteSpace(address) ? "ev3" : address);
					break;
				case ConnType.Bluetooth:
				default:
					conn = new BluetoothCommunication(ExtractEv3ComPort(address));
					break;
			}
			_brick = new Brick(conn, true);
			_brick.BrickChanged += _brick_BrickChanged;
			Trace.WriteLine("CTOR completed");
		}

		static string ExtractEv3ComPort(string defaultport)
		{
			string res = string.IsNullOrWhiteSpace(defaultport) ? "COM4" : defaultport;
			//try
			//{
			//	//var searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSSerial_PortName");
			//	var searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'");
			//	var re = new Regex(@"^ev3\W*serial", RegexOptions.IgnoreCase);
			//	foreach (ManagementObject queryObj in searcher.Get())
			//	{
			//		//Console.WriteLine("-----------------------------------");
			//		//Console.WriteLine("MSSerial_PortName instance");
			//		//Console.WriteLine("-----------------------------------");
			//		//Console.WriteLine("InstanceName: {0}", queryObj["InstanceName"]);

			//		//Console.WriteLine("-----------------------------------");
			//		//Console.WriteLine("MSSerial_PortName instance");
			//		//Console.WriteLine("-----------------------------------");
			//		//Console.WriteLine("PortName: {0}", queryObj["PortName"]);

			//		//If the serial port's instance name contains USB 
			//		//it must be a USB to serial device
			//		string n = queryObj["Caption"].ToString();
			//		if (re.IsMatch(n))
			//		{
			//			res = queryObj["PortName"].ToString();
			//			break;
			//		}
			//	}
			//}
			//catch (ManagementException e)
			//{
			//	Debug.Fail("An error occurred while querying for WMI data", e.Message);
			//}
			return res;
		}

		void _brick_BrickChanged(object sender, BrickChangedEventArgs e)
		{
			Trace.TraceInformation("{0} Changed", sender);
		}

		public async Task Run()
		{
			Trace.WriteLine("Begin Run");
			await _brick.ConnectAsync();

			Debug.WriteLine("Connected to brick...");
			await _brick.DirectCommand.PlayToneAsync(10, 1000, 300);
			Debug.WriteLine("Completed Run");
		}

		~Program() { Dispose(); }
		int _disposed = 0;
		public void Dispose()
		{
			if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 0)
			{
				Trace.WriteLine("Disconnecting from the brick");
				_brick.Disconnect();
				Trace.WriteLine("Disconnected from the brick");
			}
		}

		static void Main(string[] args)
		{
			try
			{
				Trace.Listeners.Add(new ConsoleTraceListener());
				Trace.WriteLine("Begin Main");
				ConnType t = ConnType.Bluetooth;
				if (args.Length >= 1)
					Enum.TryParse(args.FirstOrDefault(), true, out t);

				string adr = null;
				if (args.Length >= 2)
					adr = args.Skip(1).FirstOrDefault();

				using (var logic = new Program(t, adr))
				{
					logic.Run().Wait();
					Trace.WriteLine("Not waiting anymore...");
				}
			}
			catch (Exception ex)
			{
				Debug.Fail("Main Exception", ex.Message);
			}
			finally
			{
				Trace.WriteLine("Exit Main");
			}
		}
	}
}
