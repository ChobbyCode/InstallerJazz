using InstallerJazz.Models;
using InstallerJazz.FileUtils.SourceDownloader;
using InstallerJazz.Installer;

namespace InstallerJazz.Installer {
    public class AppInstaller {

        bool load = true;

        public AppInstaller(InstallArguments InstallArguments, bool OverrideDependencies = false) {
            SourceDownloader sourceDownloader = new SourceDownloader();
            DotNetInstaller dotNetInstaller = new DotNetInstaller();
            Thread Loading = new Thread(new ThreadStart(RenderLoadingIcon));
            Loading.Start();

            if(InstallArguments.InstallDotNet && !OverrideDependencies) dotNetInstaller.InstallDotNet();
            sourceDownloader.DownloadAndCopy(InstallArguments.SourceURL, InstallArguments.TargetLocation);

            load = false;
            Thread.Sleep(300);
        }


        private void RenderLoadingIcon() {
            var loaderChars = new[] { '/', '-', '\\', '|' };
            var a = 0;

            while (load) {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(loaderChars[a++]);
                a = a == loaderChars.Length ? 0 : a;
                Thread.Sleep(300);
            }
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}
