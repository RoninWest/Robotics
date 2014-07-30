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
			Get ["/{volume?80}"] = a => {
				//LcdConsole.WriteLine("Hello C0nsole from NancyFX!");

				int vol = (int)a.volume;
				var s = new Speaker(vol);
				s.Beep(500);
				//s.Buzz(500);

				return "Hell0w REST client from NancyFX!\r\nVolume @" + vol;
			};
		}
	}
}

