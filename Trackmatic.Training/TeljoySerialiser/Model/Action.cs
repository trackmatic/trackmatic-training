using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TeljoySerialiser.Model
{
    [DataContract(Name = "Action", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class Action
    {
        [DataMember(Order = 0, IsRequired = true)]
        public string ConsigneeID { get; set; }
        [DataMember(Order = 1, IsRequired = true)]
        public string ConsignorID { get; set; }
        [DataMember(Order = 2, IsRequired = true)]
        public string Type { get; set; }
        [DataMember(Order = 3)]
        public string Nature { get; set; }
        [DataMember(Order = 4)]
        public string PickupType { get; set; }
        [DataMember(Order = 5)]
        public string DocumentType { get; set; }
        [DataMember(Order = 6, IsRequired = true)]
        public string ActionId { get; set; }
        [DataMember(Order = 7, IsRequired = true)]
        public string ActionReference { get; set; }
        [DataMember(Order = 8)]
        public string ActionInternalReferemce { get; set; }
        [DataMember(Order = 9)]
        public bool IsCod { get; set; }
        [DataMember(Order = 10)]
        public string SpecialInstructions { get; set; }
        [DataMember(Order = 11, IsRequired = true)]
        public Dates Dates { get; set; }
        [DataMember(Order = 12)]
        public Properties Properties { get; set; }
        [DataMember(Order = 13)]
        public Owner Owner { get; set; }
        [DataMember(Order = 14)]
        public List<Product> Products { get; set; }
    }
}
