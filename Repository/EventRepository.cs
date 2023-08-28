using Entities.Helpers;
using Entities.Models;
using Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public Guid CustomerId { get; private set; }

        public EventRepository(EventDbContext eventDbContext)
            : base(eventDbContext)
        {
        }

        public PagedList<Event> GetAllEvents(EventParameters eventParameters)
        {
            var events = FindByCondition(ev => ev.EventDate.Month >= eventParameters.MinMonthOfEvent &&
                               ev.EventDate.Month <= eventParameters.MaxMonthOfEvent)
                           .OrderBy(e => e.EventName);

            return PagedList<Event>.ToPagedList(events,
                eventParameters.PageNumber, eventParameters.PageSize);

            //return FindAll()
            //    .OrderBy(ev => ev.EventName)
            //    .Skip((eventParameters.PageNumber - 1) * eventParameters.PageSize)
            //    .Take(eventParameters.PageSize)
            //    .ToList();
        }

        public Event GetEventById(Guid EventId)
        {
            return FindByCondition(ev => ev.Id.Equals(EventId)).FirstOrDefault();
        }

        public Event GetEventWithDetails(Guid EventId)
        {
             Event ev = FindByCondition(ev => ev.Id.Equals(CustomerId))
                    .Include(cs => cs.Customers)
                    .FirstOrDefault();

            //return FindByCondition(ev => ev.Id.Equals(EventId))
            //       .Include(cs => cs.Customers)
            //       .FirstOrDefault();

            return ev;
        }

        public void CreateEvent(Event events) => Create(events);
        public void UpdateEvent(Event events) => Update(events);

        public void DeleteEvent(Event events) => Delete(events);

    }
}

