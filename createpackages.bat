dotnet pack ./src/AvScan.Core/AvScan.Core.csproj -c Release -o %~dp0
dotnet pack ./src/AvScan.Avast/AvScan.Avast.csproj -c Release -o %~dp0
dotnet pack ./src/AvScan.AVG/AvScan.AVG.csproj -c Release -o %~dp0
dotnet pack ./src/AvScan.EsetScanner/AvScan.EsetScanner.csproj -c Release -o %~dp0
dotnet pack ./src/AvScan.WindowsDefender/AvScan.WindowsDefender.csproj -c Release -o %~dp0
dotnet pack ./src/AvScan.Core/AvScan.Core.csproj -c Release -o %~dp0
pause