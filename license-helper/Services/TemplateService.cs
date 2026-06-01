using license_helper.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace license_helper.Services
{
    internal static class TemplateService
    {
        private static string mTemplateFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates");
        public static InIFile[] GetAllTempalte()
        {

            string[] files = Directory.GetFiles(mTemplateFolder, "*.ini");

            List<InIFile> iniFiles = new List<InIFile>();

            foreach (var f in files)
            {
                var nameLine = File.ReadLines(f);

                InIFile iniFile = new InIFile()
                {
                    Name = nameLine.Where(x => x.StartsWith("Name=")).FirstOrDefault()?.Substring("Name=".Length),
                    Uid = nameLine.Where(x => x.StartsWith("Uid=")).FirstOrDefault()?.Substring("Uid=".Length),
                    TemplateFile = nameLine.Where(x => x.StartsWith("TemplateFile=")).FirstOrDefault()?.Substring("TemplateFile=".Length)
                };


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
            lines.Add("No license needed for this tool here ;)");

            foreach (Packet packet in project.Projects)
            {
                lines.Add("-".PadRight(20, '-'));

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
