using System.Runtime.Serialization;

namespace SerialiserConsoleApp.Models
{
    [DataContract(Name = "HandlingUnit", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Template")]
    public class HandlingUnit
    {
        [DataMember(Order = 1)]
        public string Barcode { get; set; }
        [DataMember(Order = 2)]
        public string Code { get; set; }
        [DataMember(Order = 3)]
        public string Reference { get; set; }
        [DataMember(Order = 4)]
        public string Description { get; set; }
        [DataMember(Order = 5)]
        public string Height { get; set; }
        [DataMember(Order = 6)]
        public string Length { get; set; }
        [DataMember(Order = 7)]
        public string Pieces { get; set; }
        [DataMember(Order = 8)]
        public string Volume { get; set; }
        [DataMember(Order = 9)]
        public string Width { get; set; }
        [DataMember(Order = 10)]
        public string Weight { get; set; }
        [DataMember(Order = 11)]
        public string AmountEx { get; set; }
        [DataMember(Order = 12)]
        public string AmountIncl { get; set; }
        [DataMember(Order = 13)]
        public string Discount { get; set; }
    }
}
