using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Hosting.Self;

namespace Ronin.Robotics.NancyBrick
{
	class MainClass
	{
		static MainClass() 
		{
			StaticConfiguration.DisableErrorTraces = false;
		}

		public static void Main (string[] args)
		{
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
			sb.Append ("Press ENTER to Stop");

			using (var nancyHost = new NancyHost (ulist.ToArray())) 
			{
				nancyHost.Start ();

				Console.WriteLine (sb.ToString());
				Console.ReadKey ();

				nancyHost.Stop ();
				Console.WriteLine ("Stopped. Good bye!");
			}
		}

	}
}
