namespace LoadEntityAndEntityLoaction
{
    public class EntityAndLocationModel
    {
        public EntityAndLocationModel(string entityName, string entityRefernce, string locationName, string locationReference)
        {
            EntityName = entityName;
            EntityRefernce = entityRefernce;
            LocationName = locationName;
            LocationReference = locationReference;
        }

        public string EntityName { get; set; }
        public string EntityRefernce { get; set; }
        public string LocationName { get; set; }
        public string LocationReference { get; set; }
    }
}
