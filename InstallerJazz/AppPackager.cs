using ConsoleUtils.Interaction;
using InstallerJazz.Models;
using InstallerJazz.Installer;

namespace InstallerJazz {
    public class AppPackager {

        public AppPackager(string[] externalArguments, InstallArguments installArguments) {
            List<string> appActions = new List<string>();
            if (installArguments.EnableInstallFeature) appActions.Add("Install");
            if (installArguments.EnableUninstallFeature) appActions.Add("Uninstall");
            if (installArguments.EnableUpdateFeature) appActions.Add("Update");

            int option = 0;
            if (appActions.Count > 1) {
                Console.WriteLine("Enter the corresponding value presented in the brackets:");
                option = MultipleChoiceOption.AskOption(appActions, "Please select an install option: ");
            }
            if (option == -1) throw new Exception();

            if (installArguments.AllowUsersToChooseInstallDrive) installArguments.TargetLocation = GetNewInstallPath(installArguments.TargetLocation);

            try {
                switch (appActions[option].ToLower()) {
                    case "install":
                        Console.WriteLine("Please Wait While The Application Installs");
                        AppInstaller appInstaller = new AppInstaller(installArguments);
                        break;
                    case "update":
                        Updater.AppUpdater appUpdater = new Updater.AppUpdater();
                        if(appUpdater.isUpdate(installArguments.VersionInfoURL, installArguments.AppVersion)) {
                            AppInstaller appUpdateInstaller = new AppInstaller(installArguments); 
                        }
                        break;
                    case "uninstall":
                        throw new NotImplementedException();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }

            Console.WriteLine("Function Complete. You may now close this window!");
            Console.ReadLine();
        }

        private string GetNewInstallPath(string oldPath) {
            Console.WriteLine("Please Select An Install Drive From Below:");
            Console.WriteLine(@"The Application Will Be Found On '[Drive]:\Program Files\TerminalChad\'");
            Console.WriteLine(@"Note: 'If you are uninstalling, please select the drive that you have installed it on.'");
            Console.WriteLine();

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives) {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);
                if (d.IsReady == true) {
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine(
                        "  Available space to current user:{0, 15} bytes",
                        d.AvailableFreeSpace);

                    Console.WriteLine(
                        "  Total available space:          {0, 15} bytes",
                        d.TotalFreeSpace);

                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} bytes ",
                        d.TotalSize);
                }
            }

            bool correct = false;
            string drive = "";

            while (!correct) {
                Console.WriteLine();
                Console.Write("Drive (Enter The Letter, i.e. 'A', 'D', 'C'): ");
                drive = Console.ReadLine();
                Console.Write($@"Is '{drive.ToUpper()}:\' correct? (y/n): ");
                var res = Console.ReadLine();
                if (res.ToLower() == "y") {
                    correct = true;
                }
            }
            var newPath = Path.Combine(drive.ToUpper() + "\\", oldPath);
            return newPath;
        }
    }
}
