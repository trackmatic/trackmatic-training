using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Massfresh.Models
{
    [DataContract(Name = "Stop", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Massfresh")]
    public class Stop
    {
        [DataMember(Order = 1)]
        public List<Consignment> Consignments { get; set; }
        [DataMember(Order = 2)]
        public Consignor Consignor { get; set; }
        [DataMember(Order = 3)]
        public Consignee Consignee { get; set; }
    }
}
