using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SerialiserConsoleApp.Models
{
    [DataContract(Name = "Consignment", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Template")]
    public class Consignment
    {
        [DataMember(Order = 1)]
        public double AmountEx { get; set; }
        [DataMember(Order = 2)]
        public double AmountInc { get; set; }
        [DataMember(Order = 3)]
        public string DueAtDateTime { get; set; }
        [DataMember(Order = 4)]
        public List<HandlingUnit> HandlingUnits { get; set; }
        [DataMember(Order = 5)]
        public bool IsCod { get; set; }
        [DataMember(Order = 6)]
        public int Nature { get; set; }
        [DataMember(Order = 7)]
        public string Reference { get; set; }
        [DataMember(Order = 8)]
        public string SpecialInstructions { get; set; }
        [DataMember(Order = 9 )]
        public int Pieces { get; set; }
        [DataMember(Order = 10)]
        public double Width { get; set; }
        [DataMember(Order = 11)]
        public double Height { get; set; }
        [DataMember(Order = 12)]
        public double Length { get; set; }
        [DataMember(Order = 13)]
        public int Pallets { get; set; }
        [DataMember(Order = 14)]
        public double Volume { get; set; }
        [DataMember(Order = 15)]
        public double Weight { get; set; }
        [DataMember(Order = 16)]
        public int Type { get; set; }
    }
}
