# AvScan

CLI wrappers for virus scanners.

Usage example:

```
  var exeLocation = @"C:\Program Files\Windows Defender\MpCmdRun.exe";
  var scanner = new WindowsDefenderScanner(exeLocation);
  var result = scanner.Scan("C:\virus.txt");
  Console.WriteLine(result);
```

You can use the eicar file for testing purposes: http://www.eicar.org/86-0-Intended-use.html

