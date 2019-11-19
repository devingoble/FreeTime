using FreeTime.Models;
using System.Collections.Generic;

namespace FreeTime.Core.Schedule
{
    public class TimeSlotOverlapComparer : IEqualityComparer<TimeSlot>
    {
        public bool Equals(TimeSlot slotA, TimeSlot slotB)
        {
            if(slotA is null && slotB is null)
            {
                return true;
            }
            else if(slotA is null || slotB is null)
            {
                return false;
            }

            return !((slotA.End <= slotB.Start) || (slotA.Start >= slotB.End));
        }

        /// <summary>
        /// Because Jon Skeet said so: https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode/263416#263416
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public int GetHashCode(TimeSlot slot)
        {
            int hash = 17;
            hash = hash * 23 + slot.Start.GetHashCode();
            hash = hash * 23 + slot.End.GetHashCode();

            return hash;
        }
    }
}
