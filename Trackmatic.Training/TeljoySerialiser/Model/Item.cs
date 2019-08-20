using System;
using System.Globalization;

namespace TeljoySerialiser.Model
{
    public class Item
    {
        public string Id { get; set; }
        public string CusNo { get; set; }
        public string CustomerName { get; set; }
        public string CellNumber { get; set; }
        public string Address { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public string DocNr { get; set; }
        public string ProdCode { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string SerialNr { get; set; }
        public string MovementType { get; set; }
        public string ActionDate { get; set; }
        public string DeliveryInstructions { get; set; }
        public DateTime GetDate()
        {
            return DateTime.SpecifyKind(DateTime.ParseExact(ActionDate, "yyyy-MM-dd", null, DateTimeStyles.AssumeLocal), DateTimeKind.Local).ToUniversalTime();
        }
        public string GetFirstName()
        {
            var cleanName = CustomerName.Replace("Mrs ", string.Empty).Replace("Ms ", string.Empty).Replace("Mr ", string.Empty);
            return cleanName.Split(' ')[0];
        }
        public string GetLastName()
        {
            return CustomerName.Substring(CustomerName.LastIndexOf(" ") + 1);
        }
    }
}
