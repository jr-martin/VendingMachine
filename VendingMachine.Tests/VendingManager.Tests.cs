using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VendingMachine.Contracts;
using VendingMachine.Services;
using VendingMachine.Models;

namespace VendingMachine.Tests
{
  [TestFixture]
  public class VendingManager
  {
    private IVendingManager _vendingManager;

    [OneTimeSetUp]
    public void SetUp()
    {
      var funds = new SortedList<int, int>
      {
        { 1, 10 },
        { 5, 5 },
        { 10, 5 },
        { 25, 5 }
      };

      var inventory = new List<InventoryItem>
      {
        new InventoryItem()
        {
          Name = "Item1",
          Price = "1.00",
          Amount = 3
        },
        new InventoryItem()
        {
          Name = "Item2",
          Price = ".65",
          Amount = 2
        }
      };

      _vendingManager = new VendingManager(inventory, funds);
    }
  }
}