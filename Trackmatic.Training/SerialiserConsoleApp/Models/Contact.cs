using System.Runtime.Serialization;

namespace SerialiserConsoleApp.Models
{
    [DataContract(Name = "Contact", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Template")]
    public class Contact
    {
        [DataMember(Order = 1)]
        public string Reference { get; set; }
        [DataMember(Order = 2)]
        public string Email { get; set; }
        [DataMember(Order = 3)]
        public string FirstName { get; set; }
        [DataMember(Order = 4)]
        public string LastName { get; set; }
        [DataMember(Order = 5)]
        public string Mobile { get; set; }
        [DataMember(Order = 6)]
        public string Work { get; set; }
        [DataMember(Order = 7)]
        public string Department { get; set; }
        [DataMember(Order = 8)]
        public string Position { get; set; }
        [DataMember(Order = 9)]
        public string ComEmail { get; set; } // Set to bool.
        [DataMember(Order = 10)]
        public string ComSms { get; set; } // Set to bool.
    }
}
