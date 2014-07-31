using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Nancy;
using Nancy.Hosting.Self;

namespace Ronin.Robotics.NancyBrick
{
	class MainClass
	{
		static readonly Logger _logger = new Logger(typeof(MainClass));

		static MainClass() 
		{
			StaticConfiguration.DisableErrorTraces = false;
		}

		static internal ManualResetEvent terminateProgram = new ManualResetEvent(false);

		public static void Main (string[] args)
		{
			try {
				_logger.Trace ("Begin Main");

				var ulist = new List<Uri> ();
				if (args != null && args.Length > 0) {
					(from a in args
					 where !string.IsNullOrWhiteSpace (a) && Uri.IsWellFormedUriString (a, UriKind.Absolute)
					 select new Uri (a)).ToList ().ForEach (ulist.Add);
				}
				if (ulist.Count == 0)
					ulist.Add (new Uri (@"http://127.0.0.1:8080"));

				var sb = new StringBuilder ("Nancy is listening on:");
				ulist.ForEach (u => sb.AppendFormat ("\n\t{0}", u));
				//sb.Append ("Press ENTER to Stop");
				sb.Append("HTTP GET /stop to Quit");

				using (var nancyHost = new NancyHost (ulist.ToArray())) 
				{
					nancyHost.Start ();

					_logger.Debug (sb.ToString());
					//Console.ReadKey ();

					terminateProgram.WaitOne ();

					nancyHost.Stop ();
					_logger.Debug ("Stopped. Good bye!");
				}

				_logger.Trace ("Exit Main");
			}
			catch(Exception ex) {
				_logger.Error ("Main(...)", ex);
			}
		}

	}
}
