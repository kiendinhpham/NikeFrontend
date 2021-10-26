using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFrontend.Data
{
    public class TeamMemberModelRootobject
    {
        public List<TeamMemberModel> data { get; set; }
        public bool succeeded { get; set; }
        public object error { get; set; }
    }

    public class TeamMemberModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string role { get; set; }
    }

}
