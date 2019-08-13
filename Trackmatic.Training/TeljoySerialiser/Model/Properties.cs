using System.Runtime.Serialization;

namespace TeljoySerialiser.Model
{
    [DataContract(Name = "Properties", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class Properties
    {
        [DataMember(Order = 0)]
        public int FixedMst { get; set; }
        [DataMember(Order = 1)]
        public int VariableMSt { get; set; }
        [DataMember(Order = 2)]
        public double AmountExcl { get; set; }
        [DataMember(Order = 3)]
        public double AmountIncl { get; set; }
        [DataMember(Order = 4)]
        public double Discount { get; set; }
        [DataMember(Order = 5)]
        public double Pallets { get; set; }
        [DataMember(Order = 6)]
        public int Units { get; set; }
        [DataMember(Order = 7)]
        public double Volume { get; set; }
        [DataMember(Order = 8)]
        public double Weight { get; set; }
        [DataMember(Order = 9)]
        public string UOM { get; set; }
        [DataMember(Order = 10)]
        public Dimensions Dimensions { get; set; }
    }
}
