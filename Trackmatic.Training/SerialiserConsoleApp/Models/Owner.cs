using System.Runtime.Serialization;

namespace SerialiserConsoleApp.Models
{
    [DataContract(Name = "Owner", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Template")]
    public class Owner
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }
        [DataMember(Order = 2)]
        public string Reference { get; set; }
    }
}
