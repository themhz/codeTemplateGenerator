using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CFSH
{
    public class TemplateGenerator
    {
        private readonly string _path;
        private readonly string _oldWord;
        private readonly string _oldWordPlural;
        private readonly string _newWord;
        private readonly string _newWordPlural;
        private readonly string _templatePath;

        public TemplateGenerator(string path, string oldWord, string newWord)
        {
            _templatePath = @"C:\Users\themis\source\repos\CFSH\templates\";
            _path = path;
            _oldWord = oldWord;
            _oldWordPlural = oldWord + "s";
            _newWord = newWord;
            _newWordPlural = newWord + "s";
        }

        public void Generate()
        {

            DeleteSubDirectories(_path);
            CopyDirectory(_templatePath, _path);

            // Process files
            var files = Directory.GetFiles(_path, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                // Replace in file content
                var content = File.ReadAllText(file);
                content = Regex.Replace(content, _oldWordPlural, _newWordPlural, RegexOptions.IgnoreCase);
                content = Regex.Replace(content, _oldWord, _newWord, RegexOptions.IgnoreCase);
                File.WriteAllText(file, content);

                // Replace in file name
                Rename(file);
            }

            // Process directories
            var directories = Directory.GetDirectories(_path, "*.*", SearchOption.AllDirectories);
            foreach (var directory in directories)
            {
                // Replace in directory name
                Rename(directory);
            }
        }

        private void Rename(string path)
        {
            var info = new FileInfo(path);
            var newName = Regex.Replace(info.Name, _oldWordPlural, _newWordPlural, RegexOptions.IgnoreCase);
            newName = Regex.Replace(newName, _oldWord, _newWord, RegexOptions.IgnoreCase);

            if (newName != info.Name)
            {
                var newPath = Path.Combine(info.DirectoryName, newName);
                if (File.Exists(path))
                {
                    File.Move(path, newPath);
                }
                else if (Directory.Exists(path))
                {
                    Directory.Move(path, newPath);
                }
            }
        }
        

        public void CopyDirectory(string sourceDirectory, string targetDirectory)
            {
                // Check if Source Directory exists
                if (!Directory.Exists(sourceDirectory))
                {
                    throw new DirectoryNotFoundException("Source directory not found: " + sourceDirectory);
                }

                // Create Target Directory if it does not exist
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                // Copy all the files of the source directory
                foreach (string file in Directory.GetFiles(sourceDirectory))
                {
                    string targetFile = Path.Combine(targetDirectory, Path.GetFileName(file));
                    File.Copy(file, targetFile, true);
                }

                // Copy all the subdirectories of the source directory
                foreach (string directory in Directory.GetDirectories(sourceDirectory))
                {
                    string targetDirectoryPath = Path.Combine(targetDirectory, Path.GetFileName(directory));
                    CopyDirectory(directory, targetDirectoryPath);
                }
            }
        

        public void DeleteDirectory(string targetDirectory)
        {
                // Check if Directory exists
                if (!Directory.Exists(targetDirectory))
                {
                    throw new DirectoryNotFoundException("Directory not found: " + targetDirectory);
                }

                // Delete the target directory
                Directory.Delete(targetDirectory, true); // true => remove contents
        }

        public void DeleteSubDirectories(string targetDirectory)
        {
            // Check if Directory exists
            if (!Directory.Exists(targetDirectory))
            {
                throw new DirectoryNotFoundException("Directory not found: " + targetDirectory);
            }

            // Get all the subdirectories
            string[] subdirectories = Directory.GetDirectories(targetDirectory);

            // Loop through them and delete
            foreach (string dir in subdirectories)
            {
                Directory.Delete(dir, true); // true => remove contents
            }
        }

    }

}