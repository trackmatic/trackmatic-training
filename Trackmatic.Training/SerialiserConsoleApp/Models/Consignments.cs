using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SerialiserConsoleApp.Models
{
    [DataContract(Name = "CompanyConsignment", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Template")]
    public class Consignments
    {
        [DataMember(Order = 1)]
        public string Site { get; set; }
        [DataMember(Order = 2)]
        public string StartDateTime { get; set; }
        [DataMember(Order = 3)]
        public List<Stop> Stops { get; set; }
    }
}
