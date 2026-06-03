using license_helper.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace license_helper.Services
{
    internal static class TemplateService
    {
        private static string mTemplateFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates");
        public static InIFile[] GetAllTempalte()
        {
            string[] files = new string[0];
            try
            {
                files = Directory.GetFiles(mTemplateFolder, "*.ini");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in GetAllTempalte: {ex.Message} {ex.InnerException}");
                return default(InIFile[]);
            }
            if (files.Length == 0)
            {
                MessageBox.Show("No ini files for templates found");
                return default(InIFile[]);
            }

            List<InIFile> iniFiles = new List<InIFile>();

            foreach (var f in files)
            {
                var nameLine = File.ReadLines(f);

                InIFile iniFile = new InIFile()
                {
                    Name = nameLine.Where(x => x.StartsWith("Name=")).FirstOrDefault()?.Substring("Name=".Length),
                    Uid = nameLine.Where(x => x.StartsWith("Uid=")).FirstOrDefault()?.Substring("Uid=".Length),
                    TemplateFile = nameLine.Where(x => x.StartsWith("TemplateFile=")).FirstOrDefault()?.Substring("TemplateFile=".Length),
                    Header = nameLine.Where(x => x.StartsWith("Header=")).FirstOrDefault()?.Substring("Header=".Length)?.Replace("\\n", Environment.NewLine)
                };

                if (iniFiles.Any(x => x.Uid == iniFile.Uid))
                {
                    MessageBox.Show($"Guid already taken: {iniFile.Name} {iniFile.Uid}");
                    continue;
                }
                iniFiles.Add(iniFile);

            }

            return iniFiles.ToArray();
        }


        internal static string ReplaceWithDataTempalte(string tempalte, string key, string data)
        {
            return tempalte.Replace(key, data);
        }

        internal static string GenerateLegalText(InIFile inIFile, Project project)
        {

            List<string> lines = new List<string>();

            lines.Add("Generated with: https://github.com/Marius1342/license-helper");
            lines.Add(inIFile.Header);
            foreach (Packet packet in project.Projects)
            {
                lines.Add("-".PadRight(35, '-'));

                string template = File.ReadAllText(Path.Combine(mTemplateFolder, inIFile.TemplateFile));
                template = ReplaceWithDataTempalte(template, "<NameOfProject>", packet.Name);
                template = ReplaceWithDataTempalte(template, "<Url>", packet.Url);
                template = ReplaceWithDataTempalte(template, "<Version>", packet.Version);
                template = ReplaceWithDataTempalte(template, "<LegalText>", packet.LegalText);

                lines.Add(template);
            }

            return String.Join(Environment.NewLine, lines);
        }
    }
}
