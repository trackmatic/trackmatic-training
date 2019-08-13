using System.Runtime.Serialization;

namespace TeljoySerialiser.Model
{
    [DataContract(Name = "Product", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class Product
    {
        [DataMember(Order = 0)]
        public string Category { get; set; }
        [DataMember(Order = 1)]
        public string IsSpecified { get; set; }
        [DataMember(Order = 2)]
        public string Barcode { get; set; }
        [DataMember(Order = 3)]
        public string ProductCode { get; set; }
        [DataMember(Order = 4)]
        public string ProductModel { get; set; }
        [DataMember(Order = 5)]
        public string Description { get; set; }
        [DataMember(Order = 6)]
        public string ExternalReference { get; set; }
        [DataMember(Order = 7)]
        public string InternalReference { get; set; }
        [DataMember(Order = 8)]
        public Properties Properties { get; set; }
    }
}
