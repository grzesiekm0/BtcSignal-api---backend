using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Btcsignal.Core.Models.Dao
{
    public class Alert
    {
        public int AlertId { get; set; }
        public string UserId { get; set; }
        public string Currency { get; set; }
        public string Exchange { get; set; }
        public decimal Threshold { get; set; }
        public bool Active { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] 
        public DateTime? UpdateDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateDate { get; set; }

    }

}

//AlertId	UserId	Currency	Exchange	Threshold	Active	UpdateDate	CreateDate

