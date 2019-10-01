using System;
namespace btcsignalwebservice
{
    public class Alert
    {
        public int Id { get; set; }
        public string alertId { get; set; }
        public string exchange { get; set; }
        public string course { get; set; }
        public string currency { get; set; }
        public int enableAlarm { get; set; }
    }

}
