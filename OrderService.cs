using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace RestaurantSystem
{
    public class OrderService
    {
        public static Order TakeOrder(int tableNumber, List<Menu> loadedMenu, List<int> itemNumbers, List<int> itemQuantities)
        {

            Order myOrder = new Order();
            myOrder.tableNumber = tableNumber;
            myOrder.orderId = Guid.NewGuid().ToString();
            myOrder.itemNumbers = new List<int>();
            myOrder.itemQuantities = new List<int>();
            myOrder.itemName = new List<string>();
            myOrder.itemPrice = new List<double>();
            myOrder.sum = new List<double>();


            List<Menu> menuItems = new List<Menu>();              
            foreach (int item in itemNumbers)
            {
                menuItems.Add(loadedMenu.Find(y => y.itemNumber == item));
            }
            int i = menuItems.Count;
            int x = 0;
            while (x < i)
            {
                myOrder.itemNumbers.Add(menuItems[x].itemNumber);
                myOrder.itemQuantities.Add(itemQuantities[x]);
                myOrder.itemName.Add(menuItems[x].itemName);
                myOrder.itemPrice.Add(menuItems[x].itemPrice);
                myOrder.sum.Add(itemQuantities[x] * menuItems[x].itemPrice);
                x++;
            }

            return myOrder;

        }
        public static void SaveOrder(Order myOrder)
        {

            string FilePath = "..\\..\\ActiveOrders\\" + myOrder.tableNumber + ".csv";
            // !!! NOTE: At the moment we do not support adding to order. We take the order and print the receipt. Solve this later. 
            if (File.Exists(@FilePath))
            {
                DateTime time = DateTime.Now;
                File.Move(@FilePath, "..\\..\\ArchivedOrders\\" + myOrder.tableNumber + "_" + myOrder.orderId + ".csv");
            }
            try
            {
                // Writing updatedTables object to csv file
                string csv = "";
                int i = myOrder.itemNumbers.Count;
                int x = 0;
                while (x < i) {
                    csv = csv + "\n" + string.Join("\n", string.Join(";", myOrder.itemName[x], myOrder.itemPrice[x], myOrder.itemQuantities[x], myOrder.sum[x]));
                x++;
                }
                DateTime now = DateTime.Now;

                csv ="Date: " + now + "\n" + "Table Number: " + myOrder.tableNumber + "\n" + "Order ID: " + myOrder.orderId + "\n" + "\n" + "Item Name;Item Price;Item Quantity; Sum" + csv + "\n" + "\n" + "Total Sum: " + myOrder.sum.Sum();

                File.WriteAllText(FilePath, csv);
            }
            catch (Exception ex)
            // In case of an exception during csv creation we will be rolling back the change and restoring the file from backup
            {
                Console.WriteLine(ex);
            }
        }
    }
}
