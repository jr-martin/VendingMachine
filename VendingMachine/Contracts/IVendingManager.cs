using System.Collections.Generic;
using VendingMachine.Models;

namespace VendingMachine.Contracts
{
  /// <summary>
  /// Handles the business logic of the machine.
  /// </summary>
  public interface IVendingManager
  {
    /// <summary>
    /// Returns the current inventory.
    /// </summary>
    List<InventoryItem> GetAvailableItems();

    /// <summary>
    /// Returns a hash set which represents the coin types that the machine accepts.
    /// </summary>
    HashSet<int> GetAcceptedDenominations();

  }
}
