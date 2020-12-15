using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeConsoleAppClasses
{
    public enum OutingType
    {
        Amusement_Park = 1,
        Bowling,
        Concert,
        Golf
    }
        
    public class Outing
    {
        public DateTime Date { get; set; }
        public OutingType Type { get; set; }
        public int Attendance { get; set; }
        public decimal CostPerPerson { get; set; }
        public decimal TotalCost { get; set; }

        public Outing() { }

        public Outing(DateTime date, OutingType type, int attendance)
        {
            Date = date;
            Type = type;
            Attendance = attendance;
            switch (type)
            {
                case OutingType.Amusement_Park:
                    CostPerPerson = 60.00m;
                    break;
                case OutingType.Bowling:
                    CostPerPerson = 25.00m;
                    break;
                case OutingType.Concert:
                    CostPerPerson = 75.00m;
                    break;
                case OutingType.Golf:
                    CostPerPerson = 150.00m;
                    break;
            }
            TotalCost = CostPerPerson * attendance;
        }
    }

    public class OutingCRUD
    {
        SortedList<DateTime, Outing> _listOfOutings = new SortedList<DateTime, Outing>();

        // Create
        public void CreateNewOuting(Outing newOuting)
        {
            _listOfOutings.Add(newOuting.Date, newOuting);
        }

        // Read
        public SortedList<DateTime, Outing> GetAllOutings()
        {
            return _listOfOutings;
        }

        // Per scope of challenge, no update or delete methods are necessary
    }
}
