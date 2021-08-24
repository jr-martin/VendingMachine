using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Contracts
{
  public interface ILogService
  {
    void LogError(string exception, string message);
    void LogInfo(string message);
  }
}
