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
    ILogService _logService;
    IFileService _fileService;

    public StockingService(IFileService fileService, ILogService logService)
    {
      _fileService = fileService;
      _logService = logService;
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

        // deserialize moneyYaml string into a dictionary, then create a sorted list and add all values to it
        // unfortunately, we can't deserialize directly into a sorted list
        var deserializer = new DeserializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance) .Build();

        Dictionary<int, int> fundsDict = deserializer.Deserialize<Dictionary<int, int>>(moneyYaml);

        SortedList<int, int> funds = new SortedList<int, int>(fundsDict, new ReverseComparer<int>(Comparer<int>.Default));

        return funds;

      }
      // catch exception, log necessary data, and rethrow it for handling further up the stack
      catch (Exception ex)
      {
        var exceptionName = ex.GetType().Name;
        _logService.LogError(exceptionName, ex.Message);
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
        _logService.LogError(exceptionName, ex.Message);
        throw;
      }
    }
  }
}
