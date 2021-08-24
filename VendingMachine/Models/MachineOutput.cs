using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
  public class MachineOutput
  {
    public String Item { get; set; }
    public SortedList<int, int> Change { get; set; }
  }
}
