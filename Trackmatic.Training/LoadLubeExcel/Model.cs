namespace LoadLubeExcel
{
    public class Model
    {
        public Model(string iD, string @ref, string name)
        {
            ID = iD;
            Ref = @ref;
            Name = name;
        }

        public string ID { get; set; }
        public string Ref { get; set; }
        public string Name { get; set; }

        
    }
}
