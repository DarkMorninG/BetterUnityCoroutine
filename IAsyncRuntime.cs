namespace Vault.BetterCoroutine {
    public interface IAsyncRuntime {
        /// Returns true if and only if the coroutine is running.  Paused tasks
        /// are considered to be running.
        bool Running { get; }

        /// Returns true if and only if the coroutine is currently paused.
        bool Paused { get; }

        /// Termination event.  Triggered when the coroutine completes execution.
        event AsyncRuntime.FinishedHandler OnFinished;

        /// Begins execution of the coroutine
        void Start();

        /// Discontinues execution of the coroutine at its next yield.
        void Stop();

        void Pause();
        void Unpause();
    }
}