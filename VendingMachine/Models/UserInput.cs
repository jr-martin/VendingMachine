using System.Collections.Generic;

namespace VendingMachine.Models
{
  public class UserInput
  {
    public int SelectedItemCode { get; set; }
    public decimal SelectedItemPrice { get; set; }
    public List<int> Coins { get; set; }
    public int CoinTotalInCents { get; set; }
  }
}
