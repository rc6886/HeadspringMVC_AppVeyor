using System.Collections.Generic;

namespace HSMVC.Controllers.Commands
{
    public class ConferenceBulkEditCommand
    {
        public IList<ConferenceEditCommand> Commands { get; set; } 
    }
}