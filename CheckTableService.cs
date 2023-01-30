using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem
{
    public class CheckTableService
    {
        public static List<Table> CheckTables(List<Table> TableData, int partySize)
        {
            // Removing busy tables 
            List<Table> FreeTables = TableData.Where(item => item.tableFree).ToList();
            // Checking for appropriate 
            List<Table> tableCheck(List<Table> FreeTablesList, int seatingSize)
            {
                List<Table> availableTables = FreeTables.Where(table => table.seatAmount == seatingSize).ToList();
                return availableTables;
            }
            // Getting the largest seat amount to set the loop counter
            int maxSeat = TableData.Max(item => item.seatAmount);
            int i = 0;
            // This loop is required for cases when there is no perfect match however we still need to check if we have larger tables available
            do
            {
                List<Table> suitableTables = tableCheck(FreeTables, partySize + i);
                if (suitableTables.Count() > 0)
                {
                    return suitableTables;
                }
                i++;
                
                if (i > maxSeat)
                {
                    return suitableTables;
                }
            }
            while (true);

        }
    }
}
