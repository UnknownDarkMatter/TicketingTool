using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingTool.Models;

namespace TicketingTool.Business
{
    public class TicketService
    {
        private string _filePath;

        public TicketService()
        {
            _filePath = " ";
        }
        public void AddNewTicket(Ticket ticket)
        {
            
        }

        public List<Ticket> GetTicket()
        {
            return new List<Ticket>();
        }

        public void Update(Ticket ticket)
        {
           
        }
        
        public void Remove(Ticket ticket)
        {

        }

    }
}
