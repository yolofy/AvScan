namespace AvScan.AVG
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Core;

    public class AVGScanner : IScanner
    {
        private const int RETURNCODE_OK = 0;
        private const int RETURNCODE_USERSTOP = 1;
        private const int RETURNCODE_ERROR = 2;
        private const int RETURNCODE_WARNING = 3;
        private const int RETURNCODE_PUPDETECTED = 4;
        private const int RETURNCODE_VIRUSDETECTED = 5;
        private const int RETURNCODE_PWDARCHIVE = 6;

        private readonly string avgscanLocation;

        /// <summary>
        /// Creates a new scanner
        /// </summary>
        /// <param name="avgscanLocation">The location of the avgscanx.exe (x86) or avgscana.exe (x64) file e.g. C:\Program Files\AVAST Software\avast</param>
        public AVGScanner(string avgscanLocation)
        {
            if (!File.Exists(avgscanLocation))
            {
                throw new FileNotFoundException();
            }

            this.avgscanLocation = new FileInfo(avgscanLocation).FullName;
        }

        /// <summary>
        /// Scan a single file
        /// </summary>
        /// <param name="file">The file to scan</param>
        /// <param name="timeoutInMs">The maximum time in milliseconds to take for this scan</param>
        /// <returns>The scan result</returns>
        public ScanResult Scan(string file, int timeoutInMs = 30000)
        {
            if (!File.Exists(file))
            {
                return ScanResult.FileNotFound;
            }

            var fileInfo = new FileInfo(file);

            var process = new Process();

            var startInfo = new ProcessStartInfo(this.avgscanLocation)
            {
                Arguments = $"/SCAN=\"{fileInfo.FullName}\"",
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
                case RETURNCODE_OK:
                    return ScanResult.NoThreatFound;
                case RETURNCODE_VIRUSDETECTED:
                case RETURNCODE_PUPDETECTED:
                    return ScanResult.ThreatFound;
                case RETURNCODE_USERSTOP:
                case RETURNCODE_ERROR:
                case RETURNCODE_WARNING:
                case RETURNCODE_PWDARCHIVE:
                default:
                    return ScanResult.Error;
            }
        }
    }
}