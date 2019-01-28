namespace LoadLubeExcel
{
    public class EntityModel
    {
        public EntityModel(string reference, string name)
        {
            Reference = reference;
            Name = name;
        }

        public string Reference { get; set; }
        public string Name { get; set; }
    }
}
