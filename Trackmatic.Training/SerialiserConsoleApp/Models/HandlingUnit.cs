using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SerialiserConsoleApp.Models
{
    [DataContract(Name = "HandlingUnit", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Template")]
    public class HandlingUnit
    {
        [DataMember(Order = 1)]
        public int Barcode { get; set; }
        [DataMember(Order = 2)]
        public int Code { get; set; }
        [DataMember(Order = 3)]
        public string Reference { get; set; }
        [DataMember(Order = 4)]
        public string Description { get; set; }
        [DataMember(Order = 5)]
        public double Height { get; set; }
        [DataMember(Order = 6)]
        public double Length { get; set; }
        [DataMember(Order = 7)]
        public int Pieces { get; set; }
        [DataMember(Order = 8)]
        public double Volume { get; set; }
        [DataMember(Order = 9)]
        public double Width { get; set; }
        [DataMember(Order = 10)]
        public double Weight { get; set; }
        [DataMember(Order = 11)]
        public double AmountEx { get; set; }
        [DataMember(Order = 12)]
        public double AmountInc { get; set; }
        [DataMember(Order = 13)]
        public double Discount { get; set; }
    }
}
