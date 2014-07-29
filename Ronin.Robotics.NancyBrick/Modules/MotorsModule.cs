using System;
using Nancy;

namespace Ronin.Robotics.NancyBrick
{
	public class MotorsModule : NancyModule
	{
		public MotorsModule ()
		{
			Get ["/"] = a => {
				return "Hellow from NancyFX!";
			};
		}
	}
}

