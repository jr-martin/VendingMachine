using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Contracts;
using VendingMachine.Models;

namespace VendingMachine
{
  public class VendingManager : IVendingManager
  {
    private readonly List<InventoryItem> _inventoryItems;
    private readonly SortedList<int, int> _funds;

    public VendingManager(List<InventoryItem> inventoryItems, SortedList<int, int> funds)
    {
      _inventoryItems = inventoryItems;
      _funds = funds;
    }

    public List<InventoryItem> GetAvailableItems()
    {
      return _inventoryItems;
    }

    // we only accept types of coins that have already been loaded into the machine
    public HashSet<int> GetAcceptedDenominations()
    {
      return new HashSet<int>(_funds.Keys);
    }

    public MachineOutput VendItem(UserInput input)
    {
      var output = new MachineOutput();
      var change = new SortedList<int, int>();

      // get the change needed in cents as an integer
      decimal itemPriceInCents = input.SelectedItemPrice * 100;
      int changeInCents = (int)(input.CoinTotalInCents - itemPriceInCents);

      // create new temporary funds sorted list and add the coins the user inserted
      // we are creating a new object in case we can't give change.
      // otherwise we'd have to rollback changes made in the _funds sorted list
      var tempFunds = new SortedList<int, int>(_funds, new ReverseComparer<int>(Comparer<int>.Default));

      // add the user coins to the machine's total funds
      foreach (var coin in input.Coins)
      {
        tempFunds[coin]++;
      }

      // in this case, no change is needed
      if (itemPriceInCents == input.CoinTotalInCents)
      {
        change.Add(0, 0); 
      }
      else
      {
        change = GetChange(changeInCents, tempFunds);
        if (change == null)
        {
          return null;
        }
      }

      // update inventory
      DecreaseInventoryItemAmount(input.SelectedItemCode);
      // update funds
      UpdateFunds(tempFunds);

      output.Change = change;
      output.Item = _inventoryItems[input.SelectedItemCode].Name;

      return output;
    }

    public SortedList<int, int> GetChange(int changeInCents, SortedList<int, int> tempFunds)
    {
      var changeDictionary = new SortedList<int, int>(new ReverseComparer<int>(Comparer<int>.Default));

      int index = 0;
      CalculateChange(changeInCents, tempFunds, changeDictionary, ref index);

      // unable to make change
      if (index != -1)
      {
        return null;
      }

      return changeDictionary;
    }


    // recursive function to do calculations
    private void CalculateChange(int changeAmount, SortedList<int, int> funds, SortedList<int, int> change, ref int index)
    {
      // stop this loop if we reached the end of the items in funds or if we manually set the index
      while (index < funds.Count && index != -1)
      {
        var denomination = funds.ElementAt(index++).Key;

        // returned coins must be smaller than the change amount, so skip to the next iteration
        if (changeAmount < denomination)
        {
          continue;
        }

        // coins of this denomination needed compared to the amount available
        // using Min to get the smaller number (either only getting the amount available or getting the rest of the coins available) 
        int count = Math.Min(changeAmount / denomination, funds[denomination]);

        change.Add(denomination, count);
        funds[denomination] -= count;

        int remainder = changeAmount - (count * denomination);

        if (remainder == 0)
        {
          // setting the index to -1 (arbitrary) so we can check it in the calling method
          index = -1;
          return;
        }

        CalculateChange(remainder, funds, change, ref index);
      }
    }

    private void DecreaseInventoryItemAmount(int index)
    {
      _inventoryItems[index].Amount -= 1;
    }

    private void UpdateFunds(SortedList<int, int> newFunds)
    {
      foreach(var coin in newFunds)
      {
        _funds[coin.Key] = coin.Value;
      }
    }

  }
}
