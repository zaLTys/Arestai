using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace hue2.Models
{
    public class ArestaiModels
    {
        public int Id { get; set; }
        public long VldId { get; set; }
        public DateTime DataNuo { get; set; }
        public DateTime? DataIki { get; set; }
    }
}