using System;
using System.Collections;
using UnityEngine;

namespace Vault.BetterCoroutine {
    /// A Task object represents a coroutine.  Tasks can be started, paused, and stopped.
    /// It is an error to attempt to start a task that has been stopped or which has
    /// naturally terminated.
    public class AsyncRuntime : IAsyncRuntime {
        /// Delegate for termination subscribers.  manual is true if and only if
        /// the coroutine was stopped with an explicit call to Stop().
        public delegate void FinishedHandler(bool manual);

        private readonly TaskManager.TaskState _task;

        /// Creates a new Task object for the given coroutine.
        /// 
        /// If autoStart is true (default) the task is automatically started
        /// upon construction.
        public AsyncRuntime(IEnumerator c, bool autoStart = true) {
            _task = TaskManager.CreateTask(c);
            _task.OnFinished += TaskFinished;
            if (autoStart)
                Start();
        }

        /// Returns true if and only if the coroutine is running.  Paused tasks
        /// are considered to be running.
        public bool Running => _task.Running;

        /// Returns true if and only if the coroutine is currently paused.
        public bool Paused => _task.Paused;

        public static IAsyncRuntime Create(IEnumerator c, bool autoStart = true) {
            return new AsyncRuntime(c, autoStart);
        }

        public static IAsyncRuntime WaitUntil(Func<bool> isTrue, Action toExecute, bool autoStart = true) {
            return new AsyncRuntime(WaitUntilInternal(isTrue, toExecute), autoStart);
        }

        private static IEnumerator WaitUntilInternal(Func<bool> trueBefore, Action toExecute) {
            yield return new WaitUntil(trueBefore);
            toExecute?.Invoke();
        }

        public static IAsyncRuntime WaitForSeconds(Action action, float seconds, bool autoStart = true) {
            return new AsyncRuntime(WaitForSecondsInternal(action, seconds), autoStart);
        }

        private static IEnumerator WaitForSecondsInternal(Action action, float seconds) {
            yield return new WaitForSeconds(seconds);
            action?.Invoke();
        }

        /// Termination event.  Triggered when the coroutine completes execution.
        public event FinishedHandler OnFinished;

        /// Begins execution of the coroutine
        public void Start() {
            _task.Start();
        }

        /// Discontinues execution of the coroutine at its next yield.
        public void Stop() {
            _task.Stop();
        }

        public void Pause() {
            _task.Pause();
        }

        public void Unpause() {
            _task.Unpause();
        }

        private void TaskFinished(bool manual) {
            var handler = OnFinished;
            handler?.Invoke(manual);
        }
        
    }
}