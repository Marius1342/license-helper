using license_helper.Classes;
using license_helper.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace license_helper.Services
{
    internal static class DbService
    {
        public static readonly string mFileJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.json");
        public static void Save()
        {
            var arr = MainWindow.Projects.ToArray();


            //Save by group, groups are created by the paths
            string[] groups = arr.Select(x => x.ExternalPath).Distinct().ToArray();
            foreach (var item in groups)
            {
                if(File.Exists(item) == false)
                {
                    continue;
                }

                File.WriteAllText(item, Project.ToJson(arr.Where(x => x.ExternalPath == item).ToArray()));
            }

            //Old
            //File.WriteAllText(mFileJson, Project.ToJson(arr));
        }
        public static void LoadProjects()
        {
            if (File.Exists(mFileJson) == false)
            {
                return;
            }

            MainWindow.Projects = new List<Project>(10);

            foreach (var item in Project.ToArrayFromJson(File.ReadAllText(mFileJson)))
            {
                item.ExternalLoaded = false;
                item.ExternalPath = mFileJson;

                MainWindow.Projects.Add(item);
            }


            //Load extenal
            foreach (var projectDb in Properties.Settings.Default.FileLocations)
            {

                if (File.Exists(projectDb) == false)
                {
                    MessageBox.Show($"Cannot find project db: {projectDb}");
                    continue;
                }

                foreach (var item in Project.ToArrayFromJson(File.ReadAllText(projectDb)))
                {
                    item.ExternalLoaded = true;
                    item.ExternalPath = projectDb;

                    MainWindow.Projects.Add(item);
                }

            }
        }

    }
}
