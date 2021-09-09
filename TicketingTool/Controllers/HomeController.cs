using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TicketingTool.Business;
using TicketingTool.Models;

namespace TicketingTool.Controllers
{
    public class HomeController : Controller
    {

        private TicketService _ticketService;

        public HomeController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ListeTickets()
        {
            var tickets = _ticketService.GetTickets();
            return View(tickets);
        }

        public IActionResult CreateTicket()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult CreateTicket(Ticket model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _ticketService.AddNewTicket(model);
            model = new Ticket();
            return View(model);
        }

        public IActionResult EditTicket()
        {
            return View();
        }
    }
}
