using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
  /// <summary>
  /// Represents an item in the vending machine
  /// </summary>
  public class InventoryItem
  {
    public string Name { get; set; }
    public string Price;
    public string Amount { get; set; }

    public decimal PriceDec => Convert.ToDecimal(Price);
  }
}
