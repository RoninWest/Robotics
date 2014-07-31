using System;
using System.Diagnostics;
using MonoBrickFirmware;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Sound;

namespace Ronin.Robotics.NancyBrick
{
	public class LcdConsoleTraceListenter : TraceListener
	{
		public LcdConsoleTraceListenter () : base(typeof(LcdConsoleTraceListenter).Name)
		{
		}

		public override bool IsThreadSafe {
			get {
				return true;
			}
		}

		public override void WriteLine (object o)
		{
			this.WriteLine (o, string.Empty);
		}

		public override void WriteLine (object o, string category)
		{
			string m = o == null ? string.Empty : o.ToString ();
			this.WriteLine (m, category);
		}

		public override void WriteLine (string message, string category)
		{
			message = message ?? string.Empty;
			if (!string.IsNullOrWhiteSpace (category))
				message = category + ": " + message;

			this.WriteLine (message);
		}

		public override void WriteLine (string message)
		{
			LcdConsole.WriteLine (message);
		}

		public override void Write (object o)
		{
			this.WriteLine (o);
		}

		public override void Write (object o, string category)
		{
			this.WriteLine (o, category);
		}

		public override void Write (string message, string category)
		{
			this.WriteLine (message, category);
		}

		public override void Write (string message)
		{
			this.WriteLine (message);
		}
	}
}

