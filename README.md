# AvScan

CLI wrappers for virus scanners.

###Windows Defender
Usage example for windows defender:
```
  var exeLocation = @"C:\Program Files\Windows Defender\MpCmdRun.exe";
  var scanner = new WindowsDefenderScanner(exeLocation);
  var result = scanner.Scan(@"C:\virus.txt");
  Console.WriteLine(result);
```

###Eset
Usage example for ESET
```
  var exeLocation = @"C:\Program Files\ESET\ESET Endpoint Antivirus\ecls.exe";
  var scanner = new EsetScanner(exeLocation);
  var result = scanner.Scan(@"C:\virus.txt");
  Console.WriteLine(result);
```



You can use the eicar file for testing purposes: http://www.eicar.org/86-0-Intended-use.html

