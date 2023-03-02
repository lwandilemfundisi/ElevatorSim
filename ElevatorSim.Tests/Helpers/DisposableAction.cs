namespace ElevatorSim.Tests.Helpers
{
    public sealed class DisposableAction : IDisposable
    {
        public static readonly DisposableAction Empty = new DisposableAction(null);

        private Action _disposeAction;

        public DisposableAction(Action disposeAction)
        {
            _disposeAction = disposeAction;
        }

        public void Dispose()
        {
            var continuation = Interlocked.Exchange(ref _disposeAction, null);
            continuation?.Invoke();
        }
    }
}
