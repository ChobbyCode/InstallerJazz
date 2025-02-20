
namespace InstallerJazz.FileUtils.Files {
    public class FileUtils {
        public static void CopyFiles(DirectoryInfo source, DirectoryInfo target) {
            Directory.CreateDirectory(target.FullName);

            foreach (var file in source.GetFiles()) {
                try {
                    file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                }
                catch {
                    // Don't bother with errors
                }
            }

            foreach (var sourceSubDirectory in source.GetDirectories()) {
                var targetSubDirectory = target.CreateSubdirectory(sourceSubDirectory.Name);
                CopyFiles(sourceSubDirectory, targetSubDirectory);
            }
        }
    }
}
