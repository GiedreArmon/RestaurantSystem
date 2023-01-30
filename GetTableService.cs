using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem
{
    public class GetTableService
    {
        public static List<Table> GetTables()     
        {
            Debug.WriteLine("Getting Tables");                   // !!! NOTE: GetTableService and GetMenuService could be solved with interface (???). Solve later.
            string filePath = "..\\..\\Tables.csv";
            List<Table> loadedTables = LoadCSV(filePath);
            Debug.WriteLine("Attempting to read file " + filePath);

            List<Table> LoadCSV(string Path)
            {
                var list = new List<Table>();
                using (var reader = new StreamReader(Path))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        list.Add(new Table
                        {
                            tableNumber = int.Parse(values[0]),
                            seatAmount = int.Parse(values[1]),
                            tableFree = bool.Parse(values[2])
                        });
                    }
                }
                return list;
            }


            return loadedTables;

        }
    }
}
