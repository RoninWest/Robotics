using System;

namespace Ronin.Robotics.NancyBrick
{
	public class Motor
	{
		public Motor ()
		{
		}

		public char Port { get; set; }
		public MotorDirection Direction { get; set; }
		public double Power { get; set; }
		public TimeSpan Time { get; set; }
	}
}

