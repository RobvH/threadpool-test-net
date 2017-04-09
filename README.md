.NET ThreadPool Test
====================

ThreadTest is a simple C# .Net project that demonstrates use of thread pool with >64 threads using a single `ManualResetEvent` for completion synchronization.


Purpose
-
Part of the purpose of this project was to perform a proof of concept for conversion of a thread pool originally written using one reset event per thread into one that tracked jobs with Interlocked counter and a single event. 


Performance Considerations
-
One issue that has surprised some developers is the relative slow rate at which the .NET thread pool grows from its minimum size toward max. The default minimum will be the number of logical cores on the machine and new threads are added at a rate of 1/sec. In jobs where you can expect to need dozens of threads, it can be significantly faster to set the minimum programmatically.


Compatibility
-
This implementation written and tested to work on .NET Core 1.1 and therefore only uses the simplified `ThreadPool`. It is designed to be buildable with C# 4.0 and deployable on .NET 2.0.

Unfortunately, .NET Core does not implement `System.Threading.ThreadPool.SetMinThreads`. While this is for a good reason, see: [dotnet/coreclr#1606 (comment)](https://github.com/dotnet/coreclr/issues/1606#issuecomment-144126757), CoreCLR systems must set this value on the CLR at runtime. There are two options: runtime configuration or environment variables.

####Runtime Configuration:
 `System.Threading.ThreadPool.MinThreads` (see: [CoreCLR Host Configuration knobs](https://github.com/dotnet/coreclr/blob/master/Documentation/project-docs/clr-configuration-knobs.md), [Runtime Configuration format](https://github.com/dotnet/cli/blob/rel/1.0.0/Documentation/specs/runtime-configuration-file.md)).

####Environment Variable:
`set COMPlus_ThreadPool_ForceMinWorkerThreads=100` (see: [dotnet/corefx Threadpool Min/Max ThreadCount Configuration](https://github.com/dotnet/corefx/issues/15990#issuecomment-279023245))


Related
-
This project is part of a short polyglot series on thread pools:

* https://github.com/RobvH/threadpool-test-net
* https://github.com/RobvH/threadpool-test-php
