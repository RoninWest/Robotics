using System;
using Nancy;
using MonoBrickFirmware;
using MonoBrickFirmware.Sound;

namespace Ronin.Robotics.NancyBrick
{
	public class MotorsModule : NancyModule
	{
		public MotorsModule ()
		{
			Get ["/"] = a => {
				var s = new Speaker(50);
				s.Beep(200);
				s.Buzz(200);
				return "Hellow from NancyFX!";
			};
		}
	}
}

