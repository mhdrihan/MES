namespace MES.Models
{
    public class MasterPart
    {
        public List<masterpart> PartList { get; set; }
    }

    public class masterpart
    {
        public int Material_ID { get; set; }

        public string? Material_Name { get; set; }

        public string? Vendor { get; set; }
    }
}
