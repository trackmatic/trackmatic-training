using System;
using System.Runtime.Serialization;

namespace TeljoySerialiser.Model
{
    [DataContract(Name = "Dates", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class Dates
    {
        [DataMember(Order = 1)]
        public string DateCreated { get; set; }
        [DataMember(Order = 2)]
        public string DateReceived { get; set; }
        [DataMember(Order = 3)]
        public string DatePicked { get; set; }
        [DataMember(Order = 4)]
        public string DateLoaded { get; set; }
        [DataMember(Order = 5, IsRequired = true)]
        public string DueAt { get; set; }
    }
}
