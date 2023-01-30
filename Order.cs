using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem
{
    public class Order
    {
        public List<int> itemNumbers;
        public List<int> itemQuantities;
        public List<string> itemName;
        public List<double> itemPrice;
        public List<double> sum;
        public int tableNumber;
        public string orderId;


        // ??? NOTE: should a constructor be used for creating receipt???
        /*
        public Order(List<int> orderItemNumbers, List<int> orderItemQuantities, List<string> orderItemName, List<double> orderPrice, List<double> orderSum, int aTableNumber, string aOrderId) 
        {
            itemNumbers = orderItemNumbers;
            itemQuantities = orderItemQuantities;
            itemName = orderItemName;
            itemPrice = orderPrice;
            sum = orderSum;
            //tableNumber = aTableNumber;
            //orderId = aOrderId;

        } */
       


        }
}
