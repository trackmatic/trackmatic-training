using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Routing.Requests;

namespace LoadLubeExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            var ELook = new EntityLookupAndMatch("556");
            ELook.PullData();
            var WE = new WriteToExcel(ELook.Entities, "LubeMarketing");
        }
    }
}
