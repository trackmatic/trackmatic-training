using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SerialiserConsoleApp.Models
{
    [DataContract(Name = "Position", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class Position
    {
        [DataMember(Order = 1)]
        public double Latitude { get; set; }
        [DataMember(Order = 2)]
        public double Longitude { get; set; }
    }
}
