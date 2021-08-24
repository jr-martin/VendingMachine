using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Contracts
{
  public interface IFileService
  {
    public string GetInventoryFile();

    public string GetMoneyFile();
  }
}
