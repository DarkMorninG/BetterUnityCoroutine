# BetterUnityCoroutine
Atempting to write cleaner Coroutines

Since Coroutines in Unity can be a bit of a pain in the a** sometimes
while also repeating code all over your project

I searched for a better Solution and stumbled upon a quite old Unity Taskmanager
After a bit of rewriting and some addition im comfortable with sharing it

# Usage

to create an normal (AsyncRuntime) Coroutine

per Static creation (hides the autostart parameter as default on true)
```c#
public void SomeRuntime() {
  IAsyncRuntime asyncRuntime = AsyncRuntime.create(someEnumerator());
}
```
per new Operator
```c#
public void SomeRuntime() {
  bool shouldAutoStart;
  IAsyncRuntime asyncRuntime = new AsyncRuntime(someEnumerator(), shouldAutoStart);
}
```

while the creation doenst really look that differet to the normal creation
u can do by far more with the returned AsyncRuntime

#### Manual Stop
```c#
public void SomeRuntime() {
  IAsyncRuntime asyncRuntime = AsyncRuntime.create(someEnumerator());
  asyncRuntime.Stop();
}
```
#### Manual Start
```c#
public void SomeRuntime() {
  IAsyncRuntime asyncRuntime = AsyncRuntime.create(someEnumerator(), false);
  asyncRuntime.Start();
}
```

#### Pause

```c#
public void SomeRuntime() {
  IAsyncRuntime asyncRuntime = AsyncRuntime.create(someEnumerator());
  asyncRuntime.Pause();
}
```
#### Unpause

```c#
public void SomeRuntime() {
  IAsyncRuntime asyncRuntime = AsyncRuntime.create(someEnumerator());
  asyncRuntime.Unpause();
}
```


##### On Finish Listener
```c#
public void StartSomeRuntime() {
  IAsyncRuntime asyncRuntime = AsyncRuntime.create(someEnumerator());
  asyncRuntime.OnFinished += manualAborted => manualFinished ? ClosedManualy() : FInishedNormaly();  
}
```


## Helper Methods 
for common use cases (my common use cases for now)

#### WaitForSeconds

```c#
public void SomeRuntime() {
  float timeUntilExecution = 2f;
  AsyncRuntime.WaitForSeconds(() => doSomethingAfterSomeTime(), timeUntilExecution);
}
```


#### WaitForCondition

```c#
public bool StartTheCoroutine = false;
public void SomeRuntime() {
  float timeUntilExecution = 2f;
  AsyncRuntime.WaitUntil(() => Startit, doSomethingAfterCondition());
}
```
