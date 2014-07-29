using System;
using System.Diagnostics;
using Nancy;
using Nancy.Hosting.Self;

namespace Ronin.Robotics.NancyBrick
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			const string HOST_ADDRESS = @"http://127.0.0.1:8080";
			using (var nancyHost = new NancyHost (new Uri (HOST_ADDRESS))) {
				nancyHost.Start ();

				Console.WriteLine ("Nancy is listening on {0}. Press enter to stop", HOST_ADDRESS);
				Console.ReadKey ();

				nancyHost.Stop ();
				Console.WriteLine ("Stopped. Good bye!");
			}
		}

	}
}
