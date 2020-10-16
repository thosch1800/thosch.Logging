using System;

namespace Microsoft.Extensions.Logging
{
  /// <summary>
  /// Tracks a logging scope and triggers a LogExit on dispose. 
  /// </summary>
  public class Scope : IDisposable
  {
    internal Scope(Action onEnter, Action onExit)
    {
      this.onExit = onExit;
      onEnter();
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose() => onExit();

    private readonly Action onExit;
  }
}