using System;

namespace Microsoft.Extensions.Logging
{
  public class Scope : IDisposable
  {
    internal Scope(Action onEnter, Action onExit)
    {
      this.onExit = onExit;
      onEnter();
    }

    public void Dispose() => onExit();

    private readonly Action onExit;
  }
}