namespace MES.Models
{
    public class TraceabillityModel
    {
        public List<traceabillitymodel> Traceabillitylist { get; set; }
    }

    public class traceabillitymodel
    {

        public string Serial_Number { get; set; }

        public string Batch_ID { get; set; }

        public string Refference_Name { get; set; }

        public string Work_Order { get; set; }

        public int Station_ID { get; set; }

        public int Station_Suffix { get; set; }

        public string Station_Name { get; set; }

        public string User_ID { get; set; }

        public string Cavity_Number { get; set; }

        public DateTime Time_In { get; set; }

        public DateTime Time_Out { get; set; }

        public string Status_Result { get; set; }

        public string Status_Running { get; set; }

        public string Transact_By { get; set; }

        public string FullRefference { get; set; }
    }
}
