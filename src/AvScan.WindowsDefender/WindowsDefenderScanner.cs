using System.Diagnostics;
using System.IO;
using AvScan.Core;

namespace AvScan.WindowsDefender
{
    public class WindowsDefenderScanner : IScanner
    {
        private readonly string mpcmdrunLocation;

        /// <summary>
        ///     Creates a new Windows defender scanner
        /// </summary>
        /// <param name="mpcmdrunLocation">
        ///     The location of the mpcmdrun.exe file e.g. C:\Program Files\Windows
        ///     Defender\MpCmdRun.exe
        /// </param>
        public WindowsDefenderScanner(string mpcmdrunLocation)
        {
            if (!File.Exists(mpcmdrunLocation)) throw new FileNotFoundException();

            this.mpcmdrunLocation = new FileInfo(mpcmdrunLocation).FullName;
        }

        /// <summary>
        ///     Scan a single file
        /// </summary>
        /// <param name="file">The file to scan</param>
        /// <param name="timeoutInMs">The maximum time in milliseconds to take for this scan</param>
        /// <returns>The scan result</returns>
        public ScanResult Scan(string file, int timeoutInMs = 30000)
        {
            if (!File.Exists(file)) return ScanResult.FileNotFound;

            var fileInfo = new FileInfo(file);

            var process = new Process();

            var startInfo = new ProcessStartInfo(mpcmdrunLocation)
            {
                Arguments = $"-Scan -ScanType 3 -File \"{fileInfo.FullName}\" -DisableRemediation",
                CreateNoWindow = true,
                ErrorDialog = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false
            };

            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit(timeoutInMs);

            if (!process.HasExited)
            {
                process.Kill();
                return ScanResult.Timeout;
            }

            switch (process.ExitCode)
            {
                case 0:
                    return ScanResult.NoThreatFound;
                case 2:
                    return ScanResult.ThreatFound;
                default:
                    return ScanResult.Error;
            }
        }
    }
}