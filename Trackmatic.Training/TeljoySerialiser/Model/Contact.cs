using System.Runtime.Serialization;

namespace TeljoySerialiser.Model
{
    [DataContract(Name = "Contact", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class Contact
    {
        [DataMember(Order = 0, IsRequired = true)]
        public string Reference { get; set; }
        [DataMember(Order = 1, IsRequired = true)]
        public string FirstName { get; set; }
        [DataMember(Order = 2, IsRequired = true)]
        public string LastName { get; set; }
        [DataMember(Order = 3)]
        public string Title { get; set; }
        [DataMember(Order = 4)]
        public string BusinessUnit { get; set; }
        [DataMember(Order = 5)]
        public string Email { get; set; }
        [DataMember(Order = 6)]
        public string Mobile { get; set; }
        [DataMember(Order = 7)]
        public string TelWork { get; set; }
        [DataMember(Order = 8)]
        public bool ComEmail { get; set; }
        [DataMember(Order = 9)]
        public bool ComSms { get; set; }
    }
}
