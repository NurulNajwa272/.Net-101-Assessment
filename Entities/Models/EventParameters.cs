using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class EventParameters : QueryStringParameters
    {
        public uint MinMonthOfEvent { get; set; }
        public uint MaxMonthOfEvent { get; set; } = (uint)DateTime.Now.Month;

        public bool ValidMonthRange => MaxMonthOfEvent > MinMonthOfEvent;

    }
}
