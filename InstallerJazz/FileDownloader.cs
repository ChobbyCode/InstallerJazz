// Copyright (c) 2024 ChobbyCode

using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InstallerJazz
{
    public class FileDownloader
    {
        public bool DownloadFiles()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            if (Directory.Exists(baseDir + @"\tmp\")) Directory.Delete(baseDir + @"\tmp\", true);
            if (Directory.Exists(baseDir + @"\Files\")) Directory.Delete(baseDir + @"\Files\", true);

            if (!Directory.Exists(baseDir + @"\tmp\")) Directory.CreateDirectory(baseDir + @"\tmp\");
            if (!Directory.Exists(baseDir + @"\tmp\zip")) Directory.CreateDirectory(baseDir + @"\tmp\zip");
            if (!Directory.Exists(baseDir + @"\tmp\dez")) Directory.CreateDirectory(baseDir + @"\tmp\dez");
            if (!Directory.Exists(baseDir + @"\Files\")) Directory.CreateDirectory(baseDir + @"\Files\");

            string path = "https://github.com/ChobbyCode/MicroMacro" + @"/zipball/InstallerFiles";
            if (Program.isBeta) path = "https://github.com/ChobbyCode/MicroMacro" + @"/zipball/BetaFiles";

                Console.WriteLine("Downloading Latest Version: ");
            Console.Write("Fetching Files From: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(path);
            Console.ForegroundColor = ConsoleColor.White;

            using (var client = new WebClient())
            {
                client.DownloadFile(path, baseDir + @"\tmp\zip\download.zip");
            }

            ZipFile.ExtractToDirectory(baseDir + @"\tmp\zip\download.zip", baseDir + @"\tmp\dez\");

            string DownloadLocation = Directory.GetDirectories(baseDir + @"\tmp\dez\")[0];

            DirectoryInfo _dI = new DirectoryInfo(DownloadLocation);

            // Get All Files In There & Directories and copy them to the copy folder
            string[] FilesInDownload = Directory.GetFiles(_dI.FullName);

            foreach (var file in FilesInDownload)
            {
                FileInfo _fI = new FileInfo(file);
                File.Copy(_fI.FullName, baseDir + @$"\Files\{_fI.Name}");
            }

            return true;
        }
    }
}
