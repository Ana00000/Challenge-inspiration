using System;
using System.Collections.Generic;

namespace ExamplesApp.Coupling
{

    class RentalSchedule
    {
        int carId;
        List<DateRange> bookings;

        /// <summary>
        /// How would you reduce couplingStrength(IsCarAvailable, DateRange class)?
        /// </summary>
        bool IsCarAvailable(DateTime onDay)
        {
            foreach (var booking in bookings)
            {
                if (booking.From > booking.To) throw new InvalidOperationException();
                if (booking.From > onDay && booking.To < onDay) return false; //Car is rented during this period.
            }
            return true;
        }
    }

    class DateRange
    {
        public DateTime From { get; }
        public DateTime To { get; }
    }
}
