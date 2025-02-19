

using System.Diagnostics;
using System.IO;
using System.Net;

namespace Installer;

public class DotNetInstaller
{
    private string baseDir = AppDomain.CurrentDomain.BaseDirectory;

    public void InstallDotNet()
    {
        CreateTmpFolders();
        DownloadScripts();
        InstallScripts();
    }

    private void CreateTmpFolders()
    {
        if (!Directory.Exists(baseDir + @"\tmp\")) Directory.CreateDirectory(baseDir + @"\tmp\");
        if (!Directory.Exists(baseDir + @"\tmp\dotnet\")) Directory.CreateDirectory(baseDir + @"\tmp\dotnet\");
    }

    private void DownloadScripts()
    {
        string script = "https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.ps1";

        using (var client = new WebClient())
        {
            client.DownloadFile(script, baseDir + @"\tmp\dotnet\script.ps1");
        }
    }

    private void InstallScripts()
    {
        ExecuteScript(baseDir + @"\tmp\dotnet\script.ps1");
    }

    private void ExecuteScript(string pathToScript)
    {
        var scriptArguments = "-ExecutionPolicy Bypass -File \"" + pathToScript + "\"";
        var processStartInfo = new ProcessStartInfo("powershell.exe", scriptArguments);
        processStartInfo.RedirectStandardOutput = true;
        processStartInfo.RedirectStandardError = true;

        using var process = new Process();
        process.StartInfo = processStartInfo;
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        Console.WriteLine(output); // I am invoked using ProcessStartInfoClass!
    }
}
