using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem
{
    public class ProvideTableService
    {
        public static void ProvideTable(List<Table> availableTables, List<Table> loadedTables)
        {
            // Providing suitable tables found
            if (availableTables.Count() > 0)
            {
                Console.WriteLine("Found results: " + availableTables.Count());
                Console.WriteLine("{0,20} {1,20}", "Table Number:", "Seat Amount:");

                foreach (Table table in availableTables)
                {

                    Console.WriteLine("{0,20} {1,20}", table.tableNumber, table.seatAmount);
                }
            }
            // In case of no suitable tables found, giving all available table info 
            if (availableTables.Count() == 0)
            {
                Console.WriteLine("Unfortunately no suitable tables have been found. Full list of free tables");
                Console.WriteLine("{0,20} {1,20}", "Table Number:", "Seat Amount:");
                List<Table> FreeTables = loadedTables.Where(item => item.tableFree).ToList();
                foreach (Table table in FreeTables)
                {
                    Console.WriteLine("{0,20} {1,20}", table.tableNumber, table.seatAmount, table.tableFree);
                }
            }
        }
    }
}
