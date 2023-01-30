using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem
{
    public class SetTableService
    {
        public static void SetTable(List<Table> Tables, List<int> selection)
        {
            string FilePath = "..\\..\\Tables.csv";
            List<Table> updatedTables = Tables;
            // If 0 is specified we will be resetting the tables. All tables are set to be free
            if (selection[0] == 0)
            {
                Console.WriteLine("Resetting Tables");
                foreach (Table table in updatedTables)
                {
                    table.tableFree = true;
                }
                Console.WriteLine("Press any key to continue. The application will exit after table rest");
                Console.ReadLine();
                System.Environment.Exit(0);
            }
            // If 0 is not set then updating the appropriate table/tables
            else
            {
                foreach (int i in selection)
                {
                    updatedTables[i - 1].tableFree = false;
                }
            }
            // Backing up the existing file
            try
            {
                // Checking if backup already exits
                if (File.Exists(@FilePath + "_backup"))
                {
                    //If backup exists, removing the file
                    File.Delete(@FilePath + "_backup");
                }
                // Backing up the file
                File.Move(@FilePath, @FilePath + "_backup");
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
            try
            {
                // Writing updatedTables object to csv file
                string csv = string.Join("\n", updatedTables.Select(o => string.Join(";", o.tableNumber, o.seatAmount, o.tableFree)));
                File.WriteAllText(FilePath, csv);
            }
            catch (Exception ex)
            // In case of an exception during csv creation we will be rolling back the change and restoring the file from backup
            {
                if (File.Exists(@FilePath))
                {
                    File.Delete(@FilePath);
                }
                File.Move(@FilePath + "_backup", @FilePath);
                Console.WriteLine(ex);
            }

        }
        public static void freeTable(int tableNumber, List<Table> tables)
        {
            tables[tableNumber - 1].tableFree = true;
            string FilePath = "..\\..\\Tables.csv";

            try
            {
                // Checking if backup already exits
                if (File.Exists(@FilePath + "_backup"))
                {
                    // If backup exists, removing the file
                    File.Delete(@FilePath + "_backup");
                }
                // Backing up the file
                File.Move(@FilePath, @FilePath + "_backup");
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
            try
            {
                // Writing updatedTables object to csv file
                string csv = string.Join("\n", tables.Select(o => string.Join(";", o.tableNumber, o.seatAmount, o.tableFree)));
                File.WriteAllText(FilePath, csv);
            }
            catch (Exception ex)
            // In case of an exception during csv creation we will be rolling back the change and restoring the file from backup
            {
                if (File.Exists(@FilePath))
                {
                    File.Delete(@FilePath);
                }
                File.Move(@FilePath + "_backup", @FilePath);
                Console.WriteLine(ex);
            }

        }
    }
}
