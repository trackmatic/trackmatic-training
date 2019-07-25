using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SerialiserConsoleApp.Models
{
    [DataContract(Name = "Position", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Template")]
    public class Position
    {
        [DataMember(Order = 1)]
        public string Latitude { get; set; }
        [DataMember(Order = 2)]
        public string Longitude { get; set; }
    }
}
