using System;

namespace VendingMachine.Models
{
  /// <summary>
  /// Represents an item in the vending machine
  /// </summary>
  public class InventoryItem
  {
    public string Name { get; set; }

    public string Price;
    public int Amount { get; set; }

    public decimal PriceDec => Convert.ToDecimal(Price);
  }
}
