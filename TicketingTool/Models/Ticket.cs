using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingTool.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? CloturationDate { get; set; }
        public string CloturationMessage { get; set; }
    }
}
