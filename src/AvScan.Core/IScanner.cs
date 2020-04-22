namespace AvScan.Core
{
    /// <summary>
    ///     Generic interface for virus scanner
    /// </summary>
    public interface IScanner
    {
        /// <summary>
        ///     Scan a single file
        /// </summary>
        /// <param name="file">The file to scan</param>
        /// <param name="timeoutInMs">The maximum time in milliseconds to take for this scan</param>
        /// <returns>The scan result</returns>
        ScanResult Scan(string file, int timeoutInMs = 30000);
    }
}