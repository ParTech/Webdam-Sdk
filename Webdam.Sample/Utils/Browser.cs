using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Webdam.Sample.Utils
{
    /// <summary>
    /// Helper class to launch a Browser in different platforms.
    /// </summary>
    public class Browser
    {
        /// <summary>
        /// Launch a browser using the specified URL.
        /// </summary>
        /// <param name="url">URL.</param>
        public static void Launch(string url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}")); // Works ok on windows
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }

            Console.WriteLine(string.Format("Open the url in your browser: {0}", url));
        }
    }
}
