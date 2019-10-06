using System;
namespace btcsignalwebservice
{
    public class Alert
    {
        public int AlertId { get; set; }
        public string UserId { get; set; }
        public string Exchange { get; set; }
        public string Course { get; set; }
        public string Currency { get; set; }
        public int Status { get; set; }
    }

}
