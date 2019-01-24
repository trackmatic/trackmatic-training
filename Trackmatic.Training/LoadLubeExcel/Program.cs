namespace LoadLubeExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            var eLook = new EntityLookupAndMatch("556");
            eLook.PullData();
            var wE = new WriteToExcel(eLook.Entities, "LubeMarketing");
            wE.Write();
        }
    }
}
