using System;
using System.Diagnostics;
using D = System.Diagnostics;
using MonoBrickFirmware;
using MonoBrickFirmware.Display;

namespace Ronin.Robotics.NancyBrick
{
	public class Logger
	{
		readonly Type _callerType;

		public Logger (Type callerType)
		{
			if (callerType == null)
				throw new ArgumentNullException ("callerType");

			_callerType = callerType;

			try {
				if (MonoBrickFirmware.Services.WiFiDevice.IsLinkUp())
					D.Trace.Listeners.Add (new LcdConsoleTraceListenter ());
				else
					D.Trace.Listeners.Add (new ConsoleTraceListener ());
			}
			catch {
				D.Trace.Listeners.Add (new ConsoleTraceListener ());
			}
		}

		public void Trace(string msg, params object[] args) 
		{
			try {
				if(!string.IsNullOrWhiteSpace(msg) && args.Length > 0)
					msg = string.Format (msg, args);

				D.Trace.WriteLine (msg, _callerType.Name);
			}
			catch
			{
			}
		}

		public void Debug(string msg, params object[] args) 
		{
			try {
				if (!string.IsNullOrWhiteSpace (msg) && args.Length > 0)
					msg = string.Format (msg, args);

				D.Debug.WriteLine (msg, _callerType.Name);
			}
			catch 
			{
			}
		}

		public void Error(string msg, Exception ex, params object[] args) 
		{
			try 
			{
				if (!string.IsNullOrWhiteSpace (msg) && args.Length > 0)
					msg = string.Format (msg, args);
					
				if (ex != null)
					D.Debug.Fail (msg, ex.Message);
				else
					D.Debug.Fail (msg);
			}
			catch 
			{
			}
		}

	}
}

