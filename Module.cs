using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2781_Group_Project
{
    internal class Module
    {
        private string ModuleCode;
        private string ModuleName;
        private string ModuleDescription;
        private string YouTubeLink;

        public Module(string moduleCode, string moduleName, string moduleDescription, string youTubeLink)
        {
            ModuleCode1 = moduleCode;
            ModuleName1 = moduleName;
            ModuleDescription1 = moduleDescription;
            YouTubeLink1 = youTubeLink;
        }

        public Module()
        {
           
        }

        public string ModuleCode1 { get => ModuleCode; set => ModuleCode = value; }
        public string ModuleName1 { get => ModuleName; set => ModuleName = value; }
        public string ModuleDescription1 { get => ModuleDescription; set => ModuleDescription = value; }
        public string YouTubeLink1 { get => YouTubeLink; set => YouTubeLink = value; }
    }
}
