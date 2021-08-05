using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TicketingTool.Models;

namespace TicketingTool.Business
{
    public class TicketService
    {
        private const string CsvSeparator = ";";
        private const string CsvSeparatorReplacement = "\\|";
        private const string NewLineSeparatorReplacement = "\\NL";

        private string _filePath;

        public TicketService(string filePath)
        {
            _filePath = filePath;
        }

        public void AddNewTicket(Ticket ticket)
        {
            CreateFileIfNotExists();
            var fileContent = File.ReadAllText(_filePath);
            fileContent += TicketToCsv(ticket);
        }

        public List<Ticket> GetTicket()
        {
            CreateFileIfNotExists();
            return new List<Ticket>();
        }

        public void Update(Ticket ticket)
        {
            CreateFileIfNotExists();

        }

        public void Remove(Ticket ticket)
        {
            CreateFileIfNotExists();

        }

        private void CreateFileIfNotExists()
        {
            if(!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, $"Id{CsvSeparator}Title{CsvSeparator}CreationDate{CsvSeparator}Content{CsvSeparator}Status{CsvSeparator}{Environment.NewLine}");
            }
        }

        private string TicketToCsv(Ticket ticket)
        {
            string csv =  $"{ticket.Id}{CsvSeparator}";
            csv += $"{ticket.Title.Replace(CsvSeparator, CsvSeparatorReplacement)}{CsvSeparator}";
            csv += $"{ticket.CreationDate}{CsvSeparator}";
            csv += $"{ticket.Content.Replace(CsvSeparator, CsvSeparatorReplacement).Replace(Environment.NewLine, NewLineSeparatorReplacement)}{CsvSeparator}";
            csv += $"{ticket.Status}{CsvSeparator}";
            csv += Environment.NewLine;
            return csv;
        }

    }
}
