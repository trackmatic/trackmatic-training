using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TeljoySerialiser.Model
{
    [DataContract(Name = "Stops", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class Stops
    {
        [DataMember(Order = 0)]
        public string SiteReference { get; set; }
        [DataMember(Order = 1)]
        public string SiteName { get; set; }
        [DataMember(Order = 2)]
        public List<Consignee> Consignees { get; set; }
        [DataMember(Order = 3)]
        public List<Consignor> Consignors { get; set; }
        [DataMember(Order = 4)]
        public List<Action> Actions { get; set; }
    }
}
