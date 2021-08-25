using System;
using System.Collections.Generic;
using VendingMachine.Models;
using VendingMachine.Services;

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

        // Instantiate a File Service to read config files
        var fileService = new FileService();

        // Instantiate a Stocking Service to get the Inventory and Funds
        var stockingService = new StockingService(fileService);

        #endregion

        // Load funds.
        var funds = stockingService.LoadFunds();
        var inventory = stockingService.StockMachine();
        ValidateStock(inventory, funds);

        // Instantiate a Vending Manager, which manages the funds and inventory
        var vendingManager = new VendingManager(inventory, funds);

        HashSet<int> acceptedDenominations = vendingManager.GetAcceptedDenominations();

        // loop this code to prevent closing the console app
        bool runMachine = true;
        while (runMachine)
        {
          List<InventoryItem> items = vendingManager.GetAvailableItems();
          UserInput input = GetUserInput(items, acceptedDenominations);
          if (input == null)
          {
            runMachine = false;
            continue;
          }

          var output = vendingManager.VendItem(input);
          if (output == null)
          {
            Console.WriteLine("Sorry, we couldn't make change for that item. Please try another selection.");
            Console.Write("Your change: ");
            foreach(var coin in input.Coins)
            {
              Console.Write(coin);
            }
            Console.WriteLine();
          }
          else
          {
            Console.WriteLine($"Your item: {output.Item}");
            Console.Write("Your change: ");
            PrintSortedIntList(output.Change);
            Console.WriteLine();
          }

          Console.WriteLine("Enter Y to make another selection. Enter any other character to exit.");
          var selection = Console.ReadLine().Trim();
          if (selection.Equals("y") || selection.Equals("Y"))
          {
            runMachine = true;
          }
          else
          {
            runMachine = false;
          }
        }
        Console.WriteLine("Exiting Vending Machine.");

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        Console.WriteLine("An error occurred. Press any key to close.");
        Console.ReadKey();
        return;
      }
    }

    /// <summary>
    /// Close the application if Funds or Inventory are not found
    /// </summary>
    private static void ValidateStock(List<InventoryItem> inventory, SortedList<int, int> funds)
    {
      if (inventory == null)
      {
        Console.WriteLine("No inventory in machine. Restart needed. Press any key to close");
        Console.ReadKey();
        return;
      }

      if (funds == null)
      {
        Console.WriteLine("No funds in machine. Restart needed. Press any key to close");
        Console.ReadKey();
        return;
      }
    }

    private static void DisplayItems(List<InventoryItem> items)
    {
      Console.WriteLine();
      Console.WriteLine("Vending Machine Items");
      Console.WriteLine("---------------------");
      for (var i = 0; i < items.Count; i++)
      {
        Console.Write($"Item Code: {i}");
        Console.Write($" | Name: {items[i].Name}".PadRight(20));
        Console.Write($" | Price: ${items[i].PriceDec:0.00}".PadRight(4));
        Console.Write($" | Quantity: {items[i].Amount}");
        Console.WriteLine();
      }
      Console.WriteLine();
    }

    // keep displaying this to the user until a valid selection is made or they choose to exit.
    private static UserInput GetUserInput(List<InventoryItem> items, HashSet<int> acceptedDenominations)
    {
      var input = new UserInput();

      bool validUserInput = false;
      while (!validUserInput)
      {
        DisplayItems(items);

        Console.WriteLine("Please make a selection by entering an Item Code. (Enter X to exit.)");
        var selection = Console.ReadLine().Trim();
        if (selection.Equals("x") || selection.Equals("X"))
        {
          return null;
        }

        int itemCode;
        var parsedSelection = Int32.TryParse(selection, out itemCode);
        if (!parsedSelection || (itemCode > (items.Count - 1)) || (itemCode < 0))
        {
          Console.WriteLine();
          Console.WriteLine("Please enter a valid item code.");
          continue;
        }

        var selectedItem = items[itemCode];
        if (selectedItem.Amount == 0)
        {
          Console.WriteLine();
          Console.WriteLine($"Sorry, we are out of {selectedItem.Name}. Please make another selection.");
          Console.WriteLine();

          continue;
        }

        Console.WriteLine("Please insert your coins.");
        Console.WriteLine($"Enter ${selectedItem.PriceDec:0.00} as a list of coin values separated by spaces.");

        var coinsInput = Console.ReadLine().Trim();

        var coins = GetCoins(coinsInput, acceptedDenominations);
        if (coins == null)
        {
          Console.WriteLine();
          Console.WriteLine("Coins could not be accepted. Please try again.");
          Console.WriteLine($"Your change: {coinsInput}");

          continue;
        }

        var totals = GetTotals(coins, selectedItem.PriceDec);
        if (totals.totalInDollars < selectedItem.PriceDec)
        {

          Console.WriteLine();
          Console.WriteLine($"Insufficient amount inserted: ${totals.totalInDollars:0.00}. Please try again.");
          Console.WriteLine($"Your change: {coinsInput}");

          continue;
        }

        Console.WriteLine($"${totals.totalInDollars:0.00} inserted");

        input.SelectedItemCode = itemCode;
        input.SelectedItemPrice = selectedItem.PriceDec;
        input.Coins = coins;
        input.CoinTotalInCents = totals.totalInCents;

        validUserInput = true;
      }
      return input;
    }

    private static List<int> GetCoins(string coinsInput, HashSet<int> acceptedDenominations)
    {
      var coins = new List<int>();

      var coinsArray = coinsInput.Split(" ");
      foreach (var coinStr in coinsArray)
      {
        int coin;
        var parsedCoin = int.TryParse(coinStr, out coin);
        if (!parsedCoin)
        {
          return null;
        }

        // check if an invalid coin value was inserted
        if (!acceptedDenominations.Contains(coin))
        {
          return null;
        }

        coins.Add(coin);
      }

      return coins;
    }

    private static (decimal totalInDollars, int totalInCents) GetTotals(List<int> coins, decimal price)
    {
      int totalInCents = 0;
      foreach (var coin in coins)
      {
        totalInCents += coin;
      }
      decimal totalDecimal = totalInCents;

      decimal totalInDollars = totalDecimal / 100M;

      return (totalInDollars, totalInCents);
    }

    private static void PrintSortedIntList(SortedList<int, int> sortedList)
    {
      foreach (var kvp in sortedList)
      {
        for (int i = 0; i < kvp.Value; i++)
        {
          Console.Write(kvp.Key + " ");
        }
      }
    }
  }
}
