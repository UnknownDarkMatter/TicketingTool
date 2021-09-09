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

        private const string ColumnId = "Id";
        private const string ColumnTitle = "Title";
        private const string ColumnCreationDate = "CreationDate";
        private const string ColumnContent = "Content";
        private const string ColumnStatus = "Status";

        private string _filePath;

        public TicketService(string filePath)
        {
            _filePath = filePath;
        }

        public void AddNewTicket(Ticket ticket)
        {
            CreateFileIfNotExists();
            var fileContent = File.ReadAllText(_filePath);
            ticket.CreationDate = DateTime.Now;
            fileContent += TicketToCsv(ticket);
            File.WriteAllText(_filePath, fileContent);
        }

        public List<Ticket> GetTickets()
        {
            CreateFileIfNotExists();
            var lines = File.ReadAllLines(_filePath);
            return StringToTickets(lines).ToList();
        }

        public void Update(Ticket ticket)
        {
            CreateFileIfNotExists();
            var lines = File.ReadAllLines(_filePath)
                .Where(m => !m.StartsWith($"{ticket.Id}{CsvSeparator}"))
                .ToList();
            lines.Add(TicketToCsv(ticket));
            File.WriteAllText(_filePath, string.Join(NewLineSeparatorReplacement, lines));
        }

        public void Remove(Ticket ticket)
        {
            CreateFileIfNotExists();
            var lines = File.ReadAllLines(_filePath)
                .Where(m => !m.StartsWith($"{ticket.Id}{CsvSeparator}"))
                .ToList();
            File.WriteAllText(_filePath, string.Join(NewLineSeparatorReplacement, lines));
        }

        private void CreateFileIfNotExists()
        {
            if(!File.Exists(_filePath) || File.ReadAllLines(_filePath).Length == 0)
            {
                File.WriteAllText(_filePath, $"{ColumnId}{CsvSeparator}{ColumnTitle}{CsvSeparator}{ColumnCreationDate}{CsvSeparator}{ColumnContent}{CsvSeparator}{ColumnStatus}{CsvSeparator}{Environment.NewLine}");
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

        private IEnumerable<Ticket> StringToTickets(string[] ticketsAsString)
        {
            var tickets = new List<Ticket>();
            if(ticketsAsString is null || ticketsAsString.Length == 0) { return tickets; }

            var header = ticketsAsString[0];
            for(int i = 1; i < ticketsAsString.Length; i++)
            {
                var ticket = new Ticket();
                var line = ticketsAsString[i];
                var lineAsArray = line.Split(CsvSeparator);

                int.TryParse(lineAsArray[IndexOfColumn(ColumnId, header)], out int id);
                ticket.Id = id;
                ticket.Title = DecodeCsv(lineAsArray[IndexOfColumn(ColumnTitle, header)]);
                ticket.Content = DecodeCsv(lineAsArray[IndexOfColumn(ColumnContent, header)]);
                DateTime.TryParse(lineAsArray[IndexOfColumn(ColumnCreationDate, header)], out DateTime creationDate);
                ticket.CreationDate = creationDate;
                int.TryParse(lineAsArray[IndexOfColumn(ColumnStatus, header)], out int status);
                ticket.Status = status;

                tickets.Add(ticket);
            }
            return tickets;
        }

        private int IndexOfColumn(string colunmnName, string header)
        {
            var headerAsArray = header.Split(CsvSeparator);
            for(int i = 0; i < headerAsArray.Length; i++)
            {
                if(headerAsArray[i] == colunmnName)
                {
                    return i;
                }
            }
            return -1;
        }

        private string DecodeCsv(string csv)
        {
            return csv.Replace(CsvSeparatorReplacement, CsvSeparator).Replace(NewLineSeparatorReplacement, Environment.NewLine);
        }
    }
}
