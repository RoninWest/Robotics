using System;
using Nancy;
using MonoBrickFirmware;
using MonoBrickFirmware.Sound;
using MonoBrickFirmware.Display;

namespace Ronin.Robotics.NancyBrick
{
	public class MotorsModule : NancyModule
	{
		public MotorsModule ()
		{
			Get ["/"] = a => {
				LcdConsole.WriteLine("Hello C0nsole from NancyFX!");

				var s = new Speaker(50);
				s.Beep(200);
				s.Buzz(200);

				return "Hell0w REST client from NancyFX!";
			};
		}
	}
}

