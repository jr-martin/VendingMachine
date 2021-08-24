using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Contracts;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using VendingMachine.Models;

namespace VendingMachine.Services
{
  /// <summary>
  /// Performs stocking functions. This implementation gets values from yaml files.
  /// </summary>
  public class StockingService : IStockingService
  {
    IFileService _fileService;

    public StockingService(IFileService fileService)
    {
      _fileService = fileService;
    }

    public SortedList<int, int> LoadFunds()
    {
      try
      {
        string moneyYaml = _fileService.GetMoneyFile();
        if (moneyYaml == null)
        {
          return null;
        }

        // deserialize moneyYaml string into a dictionary, then create a sorted list from the dictionary
        var deserializer = new DeserializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance) .Build();

        Dictionary<int, int> fundsDict = deserializer.Deserialize<Dictionary<int, int>>(moneyYaml);

        SortedList<int, int> funds = new SortedList<int, int>(fundsDict, new ReverseComparer<int>(Comparer<int>.Default));

        return funds;

      }
      // catch exception, log necessary data, and rethrow it for handling further up the stack
      catch (Exception ex)
      {
        Console.Write("An error occurred while attempting to load the funds");
        throw;
      }
    }

    public List<InventoryItem> StockMachine()
    {
      try
      {
        string inventoryYaml = _fileService.GetInventoryFile();
        if (inventoryYaml == null)
        {
          return null;
        }

        //deserialize inventoryYaml string into a dictionary
        var deserializer = new DeserializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance).Build();
        return deserializer.Deserialize<List<InventoryItem>>(inventoryYaml);
      }
      // catch exception, log necessary data, and rethrow it for further handling up the stack
      catch (Exception ex)
      {
        var exceptionName = ex.GetType().Name;
        Console.Write("An error occurred while attempting to load the inventory.");
        throw;
      }
    }
  }
}
