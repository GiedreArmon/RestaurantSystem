using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem
{
    internal class Program
    {
        static void Main()                                      
        {
            while (true)
            {
                Console.WriteLine("Welcome to the Restaurant");
                // Getting input from user
                Console.WriteLine("Please select an option:" + "\n" + "1. Find a table and take order" + "\n" + "2. Specify tables that became free");
                int menuChoise = int.Parse(Console.ReadLine());
                if (menuChoise == 1)
                {
                    Console.WriteLine("How many people are we serving?");
                    int partySize = int.Parse(Console.ReadLine());

                    // Getting table details
                    List<Table> loadedTables = GetTableService.GetTables();
                    List<Table> availableTables = CheckTableService.CheckTables(loadedTables, partySize);

                    // Providing table information to user
                    ProvideTableService.ProvideTable(availableTables, loadedTables);

                    // Getting user input for table selection
                    Console.WriteLine("Please enter table number you will be seating your guests");
                    Console.WriteLine("If you will need to select 2 or more tables, please, separate them with a comma");
                    Console.WriteLine("If you wish to reset tables enter 0");
                    List<int> selectedTables = Console.ReadLine()?.Split(',')?.Select(Int32.Parse)?.ToList();

                    // Writing the updated Table list to csv file
                    SetTableService.SetTable(loadedTables, selectedTables);

                    // Loading menu items from csv files
                    string filePathFoodMenu = "..\\..\\FoodMenu.csv";
                    string filePathDrinksMenu = "..\\..\\DrinksMenu.csv";
                    List<Menu> loadedMenu = GetMenuService.GetMenu(filePathFoodMenu);             
                    List<Menu> loadedDrinksMenu = GetMenuService.GetMenu(filePathDrinksMenu);   
                    loadedMenu.AddRange(loadedDrinksMenu);

                    foreach (Menu item in loadedMenu)
                    {

                        Console.WriteLine("{0,20} {1,20} {2,20}", item.itemNumber, item.itemName, item.itemPrice);
                    }

                    int tableNumber = selectedTables[0];
                    List<int> menuItemNumbers = new List<int>();
                    List<int> itemQuantities = new List<int>();
                    while (true)
                    {

                        Console.WriteLine("Please specify the Menu Item Number. Specify '0' to complete order");

                        int choise = int.Parse(Console.ReadLine());
                        if (choise == 0)
                        {
                            break;
                        }
                        menuItemNumbers.Add(choise);
                        Console.WriteLine("Please specify the quantity of item");
                        itemQuantities.Add(int.Parse(Console.ReadLine()));

                    }



                    Order order = OrderService.TakeOrder(tableNumber, loadedMenu, menuItemNumbers, itemQuantities);
                    OrderService.SaveOrder(order);

                    Console.WriteLine("Your Order");
                    Console.WriteLine("Order Number " + order.orderId);
                    Console.WriteLine("Table number " + order.tableNumber);
                    int i = order.itemNumbers.Count;
                    int y = 0;
                    DateTime now = DateTime.Now;
                    Console.WriteLine("Date: " + now + "\n");
                    Console.WriteLine("{0,20} {1,20} {2,20} {3,20} {4,20}", "Item Number", "Item Name", "Item Price", "Item Quantity", "Sum");
                    while (y < i)
                    {
                        Console.WriteLine("{0,20} {1,20} {2,20} {3,20} {4,20}", order.itemNumbers[y], order.itemName[y], order.itemPrice[y], order.itemQuantities[y], order.sum[y]);
                        y++;
                    }
                    Console.WriteLine("Total amount: " + order.sum.Sum());
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                }
                if (menuChoise == 2)
                {
                    List<Table> loadedTables = GetTableService.GetTables();
                    List<Table>  busyTables = loadedTables.Where(item => item.tableFree==false).ToList();
                    Console.WriteLine("List of currently busy tables");
                    Console.WriteLine("{0,20} {1,20}", "Table Number ", "Table Seat Amount");
                    foreach (Table table in busyTables)
                    {

                        Console.WriteLine("{0,20} {1,20}", table.tableNumber, table.seatAmount);
                    }
                    Console.WriteLine("Please specify the number of the table which is free");
                    int freeTableChoise = int.Parse(Console.ReadLine());
                    SetTableService.freeTable(freeTableChoise, loadedTables);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                }
            }
        }
    }
}
