namespace FileCopyAndDelete
{
    internal class Program
    {
        private const string sourceFolder = @"\\server\folder";  // Replace with your network folder path
        private const string destinationFolder = @"C:\local\folder";  // Replace with your local folder path

        // Sample list of filenames (replace with your actual filenames)
        private static readonly string[] filenames = new string[] { "file1.txt", "file2.jpg", "file3.pdf" };

        public static void Main(string[] args)
        {
            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine("Error: Source folder does not exist!");
                return;
            }

            try
            {
                foreach (string filename in filenames)
                {
                    string sourcePath = Path.Combine(sourceFolder, filename);
                    string destinationPath = Path.Combine(destinationFolder, filename);

                    // Delete existing file if it exists
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                        Console.WriteLine($"Deleted existing file: {destinationPath}");
                    }

                    CopyFile(sourcePath, destinationPath);
                }
                Console.WriteLine("All files copied successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error copying files: {0}", ex.Message);
            }
        }

        private static void CopyFile(string sourcePath, string destinationPath)
        {
            if (!File.Exists(sourcePath))
            {
                Console.WriteLine($"Error: Skipping {sourcePath} - File does not exist!");
                return;
            }

            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream destinationStream = new FileStream(destinationPath, FileMode.CreateNew, FileAccess.Write))
                {
                    byte[] buffer = new byte[4096];
                    long bytesCopied = 0;
                    int bytesRead;

                    while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        destinationStream.Write(buffer, 0, bytesRead);
                        bytesCopied += bytesRead;
                    }
                }
                Console.WriteLine($"Copied: {sourcePath}");
            }
        }
    }
}
