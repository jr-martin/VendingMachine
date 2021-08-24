using System.Collections.Generic;

namespace VendingMachine.Models
{
  public class UserInput
  {
    public int ItemCode { get; set; }
    public List<int> Coins { get; set; }
    public decimal TotalInDollars { get; set; }
    public int TotalInCents { get; set; }
  }
}
