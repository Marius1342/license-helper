using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace license_helper.Classes
{
    
    public class Project
    {
        public string Name { get; set; }
        public string ExtraInfos { get; set; }
        public string ProjectUrl { get; set; }
        public string Version { get; set; }
        public string TemplateGuid { get; set; } = "";
        public List<Packet> Projects { get; set; } = new List<Packet>();

        public static string ToJson(Project[] projects)
        {
           return Newtonsoft.Json.JsonConvert.SerializeObject(projects);
        }
        public static Project[] ToArrayFromJson(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Project[]>(json);
        }
    }
}
