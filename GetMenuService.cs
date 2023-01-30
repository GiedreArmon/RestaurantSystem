using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem
{
    public class GetMenuService
    {
        public static List<Menu> GetMenu(string filePath)         // !!! NOTE: GetTableService and GetMenuService could be solved with interface (???). Solve later.
        {
            Debug.WriteLine("Getting Menu");
            List<Menu> loadedMenu = LoadCSV(filePath);
            Debug.WriteLine("Attempting to read file " + filePath);

            List<Menu> LoadCSV(string Path)
            {
                var list = new List<Menu>();
                using (var reader = new StreamReader(Path))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        list.Add(new Menu
                        {
                            itemNumber = int.Parse(values[0]),
                            itemName = values[1],
                            itemPrice = double.Parse(values[2])
                        });
                    }
                }
                return list;
            }


            return loadedMenu;

        }

    }
}
