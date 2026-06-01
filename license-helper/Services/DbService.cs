using license_helper.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace license_helper.Services
{
    internal static class DbService
    {
        private static string mFileJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.json");
        public static void Save()
        {
            var arr = MainWindow.Projects.ToArray();
            File.WriteAllText(mFileJson, Project.ToJson(arr));
        }
        public static void LoadProjects()
        {
            if (File.Exists(mFileJson) == false)
            {
                return;
            }

            MainWindow.Projects = Project.ToArrayFromJson(File.ReadAllText(mFileJson)).ToList();
        }

    }
}
