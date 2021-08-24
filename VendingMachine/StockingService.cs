using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Contracts;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using VendingMachine.Models;

namespace VendingMachine
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

        //deserialize moneyYaml string into a sorted list
        var deserializer = new DeserializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance) .Build();
        SortedList<int, int> funds = deserializer.Deserialize<SortedList<int, int>>(moneyYaml);

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
