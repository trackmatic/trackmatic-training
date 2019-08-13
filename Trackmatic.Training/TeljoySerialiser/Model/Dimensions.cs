using System.Runtime.Serialization;

namespace TeljoySerialiser.Model
{
    [DataContract(Name = "Dimensions", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class Dimensions
    {
        [DataMember(Order = 0)]
        public double Height { get; set; }
        [DataMember(Order = 1)]
        public double Length { get; set; }
        [DataMember(Order = 2)]
        public double Width { get; set; }
    }
}
