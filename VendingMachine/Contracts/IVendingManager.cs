using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Models;

namespace VendingMachine.Contracts
{
  /// <summary>
  /// Handles the business logic of the machine.
  /// </summary>
  public interface IVendingManager
  {
    string VendItem();
    Dictionary<int, int> ReturnChange();
    public List<InventoryItem> GetAvailableItems();
  }
}
