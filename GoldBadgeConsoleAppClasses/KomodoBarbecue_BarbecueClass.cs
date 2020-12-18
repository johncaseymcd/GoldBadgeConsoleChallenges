using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeConsoleAppClasses
{
    public class Barbecue
    {
        public DateTime Date { get; set; }
        public int HBSold { get; set; }
        public int VBSold { get; set; }
        public int HDSold { get; set; }
        public int ICSold { get; set; }
        public int PCSold { get; set; }
        public int BurgerTicketsTaken { get; set; }
        public int IceCreamTicketsTaken { get; set; }
        public decimal BurgerSuppliesCost { get; set; }
        public decimal IceCreamSuppliesCost { get; set; }
        public decimal TotalBurgerCost { get; set; }
        public decimal TotalIceCreamCost { get; set; }
        public int TotalTicketsTaken { get; set; }
        public decimal TotalOverallCost { get; set; }

        public Barbecue(DateTime date, int hbSold, int vbSold, int hdSold, int icSold, int pcSold)
        {
            Date = date;
            HBSold = hbSold;
            VBSold = vbSold;
            HDSold = hdSold;
            ICSold = icSold;
            PCSold = pcSold;
            BurgerTicketsTaken = HBSold + VBSold + HDSold;
            IceCreamTicketsTaken = ICSold + PCSold;
            BurgerSuppliesCost = 1.50m * BurgerTicketsTaken;
            IceCreamSuppliesCost = 2.25m * IceCreamTicketsTaken;
            TotalBurgerCost = BurgerSuppliesCost + (HBSold * 7.50m) + (VBSold * 6.00m) + (HDSold * 4.50m);
            TotalIceCreamCost = IceCreamSuppliesCost + (ICSold * 3.25m) + (PCSold * 4.00m);
            TotalTicketsTaken = BurgerTicketsTaken + IceCreamTicketsTaken;
            TotalOverallCost = TotalBurgerCost + TotalIceCreamCost;
        }
    }
}
