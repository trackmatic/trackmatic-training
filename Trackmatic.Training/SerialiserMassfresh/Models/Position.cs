using System.Runtime.Serialization;

namespace Massfresh.Models
{
    [DataContract(Name = "Position", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Massfresh")]
    public class Position
    {
        [DataMember(Order = 1)]
        public double Latitude { get; set; }
        [DataMember(Order = 2)]
        public double Longitude { get; set; }
    }
}
