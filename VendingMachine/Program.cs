using System;
using System.Collections.Generic;
using VendingMachine.Contracts;
using VendingMachine.Models;

namespace VendingMachine
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        // To do for future release: implement a DI container
        #region Setup

        // Instantiate a Logging Service
        var logService = new LogService();

        // Instantiate a File Service to read config files
        var fileService = new FileService(logService);

        // Instantiate a Stocking Service to get the Inventory and Funds
        var stockingService = new StockingService(fileService, logService);

        #endregion

        // Close the application if Funds or Inventory are not found
        var funds = stockingService.LoadFunds();
        if (funds == null)
        {
          Console.WriteLine("No funds in machine. Restart needed. Press any key to close");
          Console.ReadKey();
          return;
        }

        var inventory = stockingService.StockMachine();
        if (inventory == null)
        {
          Console.WriteLine("No inventory in machine. Restart needed. Press any key to close");
          Console.ReadKey();
          return;
        }

        // instantiate a Vending Manager, which will be the only object to manage the funds an inventory
        var vendingManager = new VendingManager(inventory, funds, logService);
        var items = vendingManager.GetAvailableItems();
        DisplayItems(items);

        Console.ReadKey();
        return;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        Console.WriteLine("An error occurred. Press any key to close.");
        Console.ReadKey();
        return;
      }
    }

    private static void DisplayItems(List<InventoryItem> items)
    {
      Console.WriteLine("Vending Machine Items");
      Console.WriteLine("---------------------");
      for(var i = 0; i < items.Count; i++)
      {
        Console.WriteLine($"Item Number:{i} Name: {items[i].Name} Price: {items[i].PriceDec} Quantity: {items[i].Amount}");
        Console.WriteLine("");
      }
    }
  }
}
