using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingTool.Models;

namespace TicketingTool.Business
{
    public class TicketService
    {
        public string _filePath;

        public TicketService(string filePath)
        {
            _filePath = filePath;
            

        }
        public void AddNewTicket(Ticket ticket)
        {
            Console.WriteLine(_filePath);
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
