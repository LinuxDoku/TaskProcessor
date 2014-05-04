# TaskProcessor
[![Build Status](https://travis-ci.org/LinuxDoku/TaskProcessor.svg?branch=master)](https://travis-ci.org/LinuxDoku/TaskProcessor)

The task processor is a in progress project which is the basement for the new residata platform task
engine. The project aim is to create a application which can run tasks on the local machine or even in 
a distributed environment. The tasks should be easy to implement, portable and reuseable.

The project is written in C# and uses the .NET Framework 4.5 as target. It is full compatible to mono.
So you could even use it on your mac or linux box.

## Compile
```
git clone https://github.com/LinuxDoku/TaskProcessor
cd TaskProcessor
mono .nuget/nuget.exe restore
xbuild
```
