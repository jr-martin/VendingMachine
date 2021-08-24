namespace VendingMachine.Contracts
{
  public interface IFileService
  {
    /// <summary>
    /// Returns the contents of the Inventory file as string.
    /// </summary>
    public string GetInventoryFile();

    /// <summary>
    /// Returns the contents of the Money file as string.
    /// </summary>
    public string GetMoneyFile();
  }
}
