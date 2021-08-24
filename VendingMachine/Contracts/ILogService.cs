using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Contracts
{
  /// <summary>
  /// Logs output.
  /// </summary>
  public interface ILogService
  {
    void LogError(string exceptionName, string message);
    void LogError(string message);
    void LogInfo(string message);
  }
}
