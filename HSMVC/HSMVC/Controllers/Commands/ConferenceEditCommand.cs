using System;

namespace HSMVC.Controllers.Commands
{
    public class ConferenceEditCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HashTag { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Cost { get; set; }
    }
}