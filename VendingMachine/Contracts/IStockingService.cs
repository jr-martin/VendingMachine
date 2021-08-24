using System.Collections.Generic;
using VendingMachine.Models;

namespace VendingMachine.Contracts
{
  /// <summary>
  /// Performs stocking functions, which include populating the vending machine with both items and money.
  /// </summary>
  public interface IStockingService
  {
    /// <summary>
    /// Returns a nested dictionary with the name of the item as the top level key,
    /// and the price as the nested key with quantity as the nested value.
    /// </summary>
     List<InventoryItem> StockMachine();

    /// <summary>
    /// Returns a Sorted List with the coin denomination as the key and quantity as the value.
    /// </summary>
    SortedList<int, int> LoadFunds();
  }
}
