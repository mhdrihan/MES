namespace MES.Models
{
    public class MasterLine
    {
        public List<masterline> LineList { get; set; }
    }

    public class masterline
    {
        public int Line_ID { get; set; }

        public string Line_Name { get; set; }

        public string Line_Location { get; set; }

        public string Line_Description { get; set; }

        public DateTime? Last_Modify { get; set; }

        public string Transact_By { get; set; }

    }
}
