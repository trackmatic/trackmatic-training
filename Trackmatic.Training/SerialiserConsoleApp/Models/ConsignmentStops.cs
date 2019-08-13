using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SerialiserConsoleApp.Models
{
    [DataContract(Name = "ConsignmentStops", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class ConsignmentStops
    {
        [DataMember(Order = 1)]
        public string SiteId { get; set; }
        [DataMember(Order = 2)]
        public string SiteName { get; set; }
        [DataMember(Order = 3)]
        public List<Stop> Stops { get; set; }
    }
}
