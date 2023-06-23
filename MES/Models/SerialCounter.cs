namespace MES.Models
{
    public class SerialCounter
    {
        public List<serialcounter> CounterList { get; set; }
    }

    public class serialcounter
    {
        public int? Station_ID { get; set; }

        public int Station_Suffix { get; set; }

        public int Year_Code { get; set; }

        public int Week_Code { get; set; }

        public int Counter_Code { get; set; }
    }
}
