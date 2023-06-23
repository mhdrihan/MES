namespace MES.Models
{
    public class MasterStation
    {
        public List<masterstation> StationList { get; set; }
    }
    public class masterstation
    {
        public int Station_ID { get; set; }

        public int Station_Suffix { get; set; }

        public string Station_Name { get; set; }

        public int Process_ID { get; set; }

        public int Line_ID { get; set; }

        public int Target_Output { get; set; }

        public int Target_Yield { get; set; }

        public DateTime? Last_Modify { get; set; }

        public string Transact_By { get; set; }

        public string Tester_Name { get; set; }

    }
}
