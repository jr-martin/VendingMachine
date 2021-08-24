using System;
using System.IO;
using VendingMachine.Contracts;

namespace VendingMachine
{
  /// <summary>
  /// Handles File IO operations. Reads money and inventory files and returns the contents as strings.
  /// </summary>
  public class FileService : IFileService
  {
    private const string _configFileDirectory = "ConfigFiles";
    private const string _inventoryFile = "Inventory.yaml";
    private const string _moneyFile = "Money.yaml";

    private ILogService _logService;
    public FileService(ILogService logService)
    {
      _logService = logService;
    }

    public string GetInventoryFile()
    {
      return GetFileContentsAsString(_inventoryFile);
    }

    public string GetMoneyFile()
    {
      return GetFileContentsAsString(_moneyFile);
    }

    private string GetFileContentsAsString(string fileName)
    {
      var rootPath = Directory.GetParent(AppContext.BaseDirectory).FullName;
      var fullPath = Path.Combine(rootPath, _configFileDirectory, fileName);

      if (!File.Exists(fullPath))
      {
        _logService.LogError($"{fileName} does not exist. Please place it in the same directory as the executable.");
        return null;
      }

      return File.ReadAllText(fullPath);
    }
  }
}
