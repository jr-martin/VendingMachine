using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Contracts;

namespace VendingMachine
{
  /// <summary>
  /// Logs output. This implementation writes to the console.
  /// </summary>
  public class LogService : ILogService
  {
    public void LogError(string exceptionName, string message)
    {
      Console.WriteLine("---------------------------------------------------------");
      Console.WriteLine($"Error: {exceptionName} - {message}");
      Console.WriteLine("---------------------------------------------------------");
    }
    public void LogError(string message)
    {
      Console.WriteLine("---------------------------------------------------------");
      Console.WriteLine($"Error: {message}");
      Console.WriteLine("---------------------------------------------------------");
    }

    public void LogInfo(string message)
    {
      Console.WriteLine("---------------------------------------------------------");
      Console.WriteLine($"Info: {message}");
      Console.WriteLine("---------------------------------------------------------");
    }
  }
}
