using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CFSH
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\themis\source\repos\CFSH\generatedfiles\";
            var oldWord = "{entityName}";
            var newWord = "Banana";
            GenerateTemplates(path, oldWord, newWord);

        }

        static void CreateEntityBoilerPlate(string name)
        {
            string folderName = CapitalizeFirstLetter(name);
            

        }

        static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input);
        }

        static void GenerateTemplates(string path, string oldWord, string newWord)
        {         
            var generator = new TemplateGenerator(path, oldWord, newWord);
            generator.Generate();
        }
    }
}
