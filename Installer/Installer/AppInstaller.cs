using InstallerJazz.Models;
using InstallerJazz.FileUtils.SourceDownloader;

namespace InstallerJazz.Installer {
    public class AppInstaller {

        bool load = true;

        public AppInstaller(InstallArguments InstallArguments) {
            SourceDownloader sourceDownloader = new SourceDownloader();

            Thread Loading = new Thread(new ThreadStart(RenderLoadingIcon));
            Loading.Start();

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
