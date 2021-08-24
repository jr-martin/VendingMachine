﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Contracts;
using VendingMachine.Models;

namespace VendingMachine
{
  public class VendingManager : IVendingManager
  {
    private ILogService _logService;

    private List<InventoryItem> _inventoryItems;
    private SortedList<int, int> _funds;

    public VendingManager(List<InventoryItem> inventoryItems, SortedList<int, int> funds, ILogService logService)
    {
      _inventoryItems = inventoryItems;
      _funds = funds;
      _logService = logService;
    }

    public List<InventoryItem> GetAvailableItems()
    {
      return _inventoryItems;
    }

    public HashSet<int> GetAcceptedDenominations()
    {
      return new HashSet<int>(_funds.Keys);
    }

    public MachineOutput VendItem(UserInput userInput)
    {

    }

    //private MakeChange(SortedList<int, int> funds, SortedList<int, int> )

  }
}
