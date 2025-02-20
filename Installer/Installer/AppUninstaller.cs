
namespace InstallerJazz.Installer {
    public class AppUninstaller {
        private List<String> allInfoLocations = new List<String> { };

        public void Uninstall(string installedLocation, string executablePath) {
            allInfoLocations.Add(installedLocation);

            foreach (string d in allInfoLocations) {
                try {
                    Directory.Delete(d, true);
                }
                catch {
                    Console.WriteLine($"Failed To Delete Directory: '{d}'");
                }
            }
            RemoveFromPath(executablePath);

            Console.WriteLine("Successfully Uninstalled");
        }

        public static void RemoveFromPath(string location) {
            var name = "PATH";
            var scope = EnvironmentVariableTarget.Machine; // or User
            var oldValue = Environment.GetEnvironmentVariable(name, scope);

            var index = oldValue.IndexOf($@";{location}");
            if (index < 0) return;
            var newValue = oldValue.Substring(0, index - 1) + (oldValue.Substring(index + location.Length));
            Environment.SetEnvironmentVariable(name, newValue, scope);
        }
    }
}
