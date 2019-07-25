using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SerialiserConsoleApp.Models
{
    [DataContract(Name = "Consignment", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Template")]
    public class Consignment
    {
        [DataMember(Order = 1)]
        public string AmountEx { get; set; }
        [DataMember(Order = 2)]
        public string AmountInc { get; set; }
        [DataMember(Order = 3)]
        public DateTime DueAtDateTime { get; set; }
        [DataMember(Order = 4)]
        public List<HandlingUnit> HandlingUnits { get; set; }
        [DataMember(Order = 5)]
        public Owner Owner { get; set; }
        [DataMember(Order = 6)]
        public string IsCod { get; set; } // Set to bool.
        [DataMember(Order = 7)]
        public string IsCTC { get; set; } // Set to bool.
        [DataMember(Order = 8)]
        public string Nature { get; set; }
        [DataMember(Order = 9)]
        public string Reference { get; set; }
        [DataMember(Order = 10)]
        public string InternalReference { get; set; }
        [DataMember(Order = 11)]
        public string SpecialInstructions { get; set; }
        [DataMember(Order = 12)]
        public string Pieces { get; set; }
        [DataMember(Order = 13)]
        public string Width { get; set; }
        [DataMember(Order = 14)]
        public string Height { get; set; }
        [DataMember(Order = 15)]
        public string Length { get; set; }
        [DataMember(Order = 16)]
        public string Pallets { get; set; }
        [DataMember(Order = 17)]
        public string Volume { get; set; }
        [DataMember(Order = 18)]
        public string Weight { get; set; }
        [DataMember(Order = 19)]
        public string CommodityType { get; set; }
        [DataMember(Order = 20)]
        public string Type { get; set; }
    }
}
