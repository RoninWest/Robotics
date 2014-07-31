using System;
using System.Diagnostics;
using Nancy;
using MonoBrickFirmware;
using MonoBrickFirmware.Sound;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;

namespace Ronin.Robotics.NancyBrick
{
	public class MotorsModule : NancyModule
	{
		static readonly Logger _logger = new Logger(typeof(MotorsModule));

		public MotorsModule ()
		{
			Get ["/{volume?80}"] = VolumeTest;
			Get ["/stop"] = Stop;
		}

		dynamic MotorTest(dynamic a) {
			var m = new Motor ();
			m.Port = 'D';
			m.Power = 50f;
			m.Direction = MotorDirection.Default;
			m.Time = TimeSpan.FromSeconds (3);
			return "OK";
		}

		dynamic VolumeTest(dynamic a) {
			int vol = (int)a.volume;
			_logger.Debug ("Volume is set to {0}", vol);

			var s = new Speaker(vol);
			s.Beep();
			//s.Buzz();

			return "Hell0w REST client from NancyFX!\r\nVolume @" + vol;
		}

		dynamic Stop(dynamic a) {
			_logger.Debug ("Stop signal received");
			MainClass.terminateProgram.Set();

			return "G00d Bye!";
		}
	}
}

