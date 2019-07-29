using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Massfresh.Models
{
    [DataContract(Name = "Consignment", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Massfresh")]
    public class Consignment
    {
        [DataMember(Order = 1)]
        public double? AmountEx { get; set; }
        [DataMember(Order = 2)]
        public double? AmountIncl { get; set; }
        [DataMember(Order = 3)]
        public DateTime DueAtDateTime { get; set; }
        [DataMember(Order = 4)]
        public List<HandlingUnit> HandlingUnits { get; set; }
        [DataMember(Order = 5)]
        public bool IsCod { get; set; } 
        [DataMember(Order = 6)]
        public bool IsCtc { get; set; } 
        [DataMember(Order = 7)]
        public string Type { get; set; }
        [DataMember(Order = 8)]
        public string Reference { get; set; }
        [DataMember(Order = 9)]
        public string ReferenceInternal { get; set; }
        [DataMember(Order = 10)]
        public string ReferenceExternal { get; set; }
        [DataMember(Order = 11)]
        public string SpecialInstructions { get; set; }
        [DataMember(Order = 12)]
        public int Pieces { get; set; }
        [DataMember(Order = 13)]
        public double? Width { get; set; }
        [DataMember(Order = 14)]
        public double? Height { get; set; }
        [DataMember(Order = 15)]
        public double? Length { get; set; }
        [DataMember(Order = 16)]
        public double? Pallets { get; set; }
        [DataMember(Order = 17)]
        public double? Volume { get; set; }
        [DataMember(Order = 18)]
        public double? Weight { get; set; }
        [DataMember(Order = 19)]
        public string CommodityType { get; set; }
        [DataMember(Order = 20)]
        public Owner Owner { get; set; }
    }
}
