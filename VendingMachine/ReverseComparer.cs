using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  /// <summary>
  /// Generic Reverse Comparer 
  /// </summary>
  public sealed class ReverseComparer<T> : IComparer<T>
  {
    private readonly IComparer<T> _comparer;

    public ReverseComparer(IComparer<T> comparer)
    {
      _comparer = comparer;
    }

    public int Compare(T left, T right)
    {
      return _comparer.Compare(right, left);
    }
  }
}
