using System.Runtime.Serialization;

namespace Massfresh.Models
{
    [DataContract(Name = "Owner", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Massfresh")]
    public class Owner
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }
        [DataMember(Order = 2)]
        public string Reference { get; set; }
    }
}
