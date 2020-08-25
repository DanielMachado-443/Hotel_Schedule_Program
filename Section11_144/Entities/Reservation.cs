using System;
using System.Text;
using Section11_144.Entities.Exceptions;

namespace Section11_144.Entities
{
    class Reservation
    {
        public int RoomNumber { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public Reservation()
        {
            RoomNumber = 0;
            CheckIn = DateTime.Now;
            CheckOut = DateTime.Now;
        }

        public Reservation(int roomNumber, DateTime checkIn, DateTime checkOut)
        {
            DateTime aTime = DateTime.Today;
            if (checkIn < DateTime.Now || checkOut < aTime.AddDays(1))
            {
                throw new DomainException("\n   Error! Reservation checkIn date or checkOut are not scheduled to the future");  // throw CUTS the method OFF just LIKE RETURN
            }

            if (checkIn >= checkOut)  // IT IS NOT NECESSARY TO USE ELSE HERE, BECAUSE THE RETURN CUTS THE METHOD OFF ANYWAY
            {
                throw new DomainException("\n   Error! The checkIn date must be before the checkOut");

            }

            RoomNumber = roomNumber;
            CheckIn = checkIn;
            CheckOut = checkOut;
        }

        public int Duration()
        {
            TimeSpan duration = CheckOut.Subtract(CheckIn);
            return (int)duration.TotalDays;                    // COOLER TO CAST TO INT LIKE THIS INSTEAD OF ONLY CONVERTING
        }

        public void UpdateDates(DateTime checkIn, DateTime checkOut)
        {
            DateTime aTime = DateTime.Today;
            if (checkIn < DateTime.Now || checkOut < aTime.AddDays(1))
            {
                throw new DomainException("\n   Error! Reservation checkIn date or checkOut are not scheduled to the future");  // throw CUTS the method OFF just LIKE RETURN
            }

            if (checkIn >= checkOut)  // IT IS NOT NECESSARY TO USE ELSE HERE, BECAUSE THE RETURN CUTS THE METHOD OFF ANYWAY
            {
                throw new DomainException("\n   Error! The checkIn date must be before the checkOut");

            }

            CheckIn = checkIn;
            CheckOut = checkOut;
        }

        public override string ToString()
        {
            StringBuilder s2 = new StringBuilder();
            s2.AppendLine("\n   RESERVE INFO ");
            s2.Append("\n   Room number: " + RoomNumber);
            s2.Append("\n   Checkin date: " + CheckIn);
            s2.Append("\n   Checkout date: " + CheckOut);
            s2.Append("\n   Duration: " + this.Duration() + " day(s)"); // It could've been writen WITHOUT the 'this' notation, since 'Duration()' IS a method of this class itself
            return s2.ToString();
        }
    }
}
