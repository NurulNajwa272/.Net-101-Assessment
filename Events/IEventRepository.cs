using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public interface IEventRepository : IRepositoryBase<Event>
    {
        PagedList<Event> GetAllEvents(EventParameters eventParameters);
        Event GetEventById(Guid EventId);
        Event GetEventWithDetails(Guid EventId);
        void CreateEvent(Event events);
        void UpdateEvent(Event events);
        void DeleteEvent(Event events);
    }
}
