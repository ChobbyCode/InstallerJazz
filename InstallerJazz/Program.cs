// Copyright (c) 2024 ChobbyCode
// Copyright (c) 2025 ChobbyCode

using IWshRuntimeLibrary;
using System.Diagnostics;
using Installer;

namespace InstallerJazz {
    public class Program {

        public static bool isBeta = false;

        public static void Main(string[] args) {
            Console.Clear();
            Console.Title = "ChobbyCodes Janky Installer v1.0.0";

            try {
                if (args[0] == "true") AutoUpdate(AppDomain.CurrentDomain.BaseDirectory);
            }
            catch (Exception ex) {
                if (isInstall()) UserInstall();
                else {
                    string installDrive = GetInstallDrive();

                    Uninstaller uninstaller = new Uninstaller();
                    uninstaller.Uninstall(installDrive);
                }
            }
        }

        public static void AutoUpdate(string installDrive) {
            Console.WriteLine("Installing");
            FileDownloader fd = new FileDownloader();
            fd.DownloadFiles();
            Console.WriteLine("Install Complete! This window will automatically close after 2.487 seconds.");
            Thread.Sleep(2487);
        }

        public static void UserInstall() {
            try {
                string installDrive = GetInstallDrive();
                if (!Directory.Exists(installDrive + @"\Program Files\"))
                    Directory.CreateDirectory(installDrive + @"\Program Files\");
                if (!Directory.Exists(installDrive + @"\Program Files\TerminalChad\"))
                    Directory.CreateDirectory(installDrive + @"\Program Files\TerminalChad\");
                bool _dS = AskDesktopShortcut();
                bool _sS = AskStartMenuShortcut();

                Console.WriteLine("Installing Dotnet Core Library");
                DotNetInstaller dotNetInstaller = new DotNetInstaller();
                dotNetInstaller.InstallDotNet();

                // Download Latest Versions
                FileDownloader fd = new FileDownloader();
                fd.DownloadFiles();
                AddAsPath(installDrive + @"Program Files\TerminalChad\");

                if (_dS) {
                    CreateDesktopShortcut(installDrive + @"\Program Files\TerminalChad\TerminalChad.exe");
                }
                if (_sS) {
                    CreateShortcut(installDrive + @"\Program Files\TerminalChad\TerminalChad.exe",
                     installDrive + @"\Program Files\TerminalChad\TerminalChad.lnk");
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Installation Failed!");
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        public static bool isInstall() {
            bool correct = false;
            string answer = "";
            while (!correct) {
                Console.WriteLine();
                Console.Write("Would you like to install the application or uninstall the application? (install/uninstall): ");
                answer = Console.ReadLine();
                if (answer.ToLower() == "uninstall" || answer.ToLower() == "install") correct = true;
                else Console.WriteLine("Please Enter A Valid Answer");
            }
            if (answer.ToLower() == "install") return true;
            else return false;
        }

        public static bool AskDesktopShortcut() {
            bool correct = false;
            string answer = "";
            while (!correct) {
                Console.WriteLine();
                Console.Write("Would you like to add a desktop shortcut to the application? (y/n): ");
                answer = Console.ReadLine();
                if (answer.ToLower() == "n" || answer.ToLower() == "y") correct = true;
                else Console.WriteLine("Please Enter A Valid Answer");
            }
            if (answer.ToLower() == "y") return true;
            else return false;
        }

        public static bool AskStartMenuShortcut() {
            bool correct = false;
            string answer = "";
            while (!correct) {
                Console.WriteLine();
                Console.Write("Would you like to add TerminalChad to your start menu? (y/n): ");
                answer = Console.ReadLine();
                if (answer.ToLower() == "n" || answer.ToLower() == "y") correct = true;
                else Console.WriteLine("Please Enter A Valid Answer");
            }
            if (answer.ToLower() == "y") return true;
            else return false;
        }

        public static void CreateDesktopShortcut(string ApplicationPath) {
            object shDesktop = (object)"Desktop";
            WshShell shell = new WshShell();
            string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\TerminalChad.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "TerminalChad is a free, opensource, text-macro creating software - created by ChobbyCode.";
            shortcut.TargetPath = ApplicationPath;
            shortcut.Save();
        }

        private static void CreateStartMenuShortcut(string ApplicationPath, string installDrive) {
            WshShell shell = new WshShell();
            string shortcutAddress = installDrive + @"\TerminalChad.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "TerminalChad is a free, opensource, text-macro creating software - created by ChobbyCode.";
            shortcut.TargetPath = ApplicationPath + @"\TerminalChad.exe";
            shortcut.Save();
        }

        public static string GetInstallDrive() {
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
            return @$"{drive.ToUpper()}:\";
        }

        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="SourceFile">A file you want to make shortcut to</param>
        /// <param name="ShortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        public static void CreateShortcut(string SourceFile, string ShortcutFile) {
            CreateShortcut(SourceFile, ShortcutFile, null, null, null, null);
        }

        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="SourceFile">A file you want to make shortcut to</param>
        /// <param name="ShortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        /// <param name="Description">Shortcut description</param>
        /// <param name="Arguments">Command line arguments</param>
        /// <param name="HotKey">Shortcut hot key as a string, for example "Ctrl+F"</param>
        /// <param name="WorkingDirectory">"Start in" shorcut parameter</param>
        public static void CreateShortcut(string TargetPath, string ShortcutFile, string Description,
           string Arguments, string HotKey, string WorkingDirectory) {
            // Check necessary parameters first:
            if (String.IsNullOrEmpty(TargetPath))
                throw new ArgumentNullException("TargetPath");
            if (String.IsNullOrEmpty(ShortcutFile))
                throw new ArgumentNullException("ShortcutFile");

            // Create WshShellClass instance:
            var wshShell = new WshShell();

            // Create shortcut object:
            IWshRuntimeLibrary.IWshShortcut shorcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(ShortcutFile);

            // Assign shortcut properties:
            shorcut.TargetPath = TargetPath;
            shorcut.Description = Description;
            if (!String.IsNullOrEmpty(Arguments))
                shorcut.Arguments = Arguments;
            if (!String.IsNullOrEmpty(HotKey))
                shorcut.Hotkey = HotKey;
            if (!String.IsNullOrEmpty(WorkingDirectory))
                shorcut.WorkingDirectory = WorkingDirectory;

            // Save the shortcut:
            shorcut.Save();
        }

        private static void AddAsPath(string location) {
            var name = "PATH";
            var scope = EnvironmentVariableTarget.Machine; // or User
            var oldValue = Environment.GetEnvironmentVariable(name, scope);
            if (oldValue.Contains($@";{location}")) return;
            var newValue = oldValue + $@";{location}";
            Environment.SetEnvironmentVariable(name, newValue, scope);
        }

    }
}