// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Resyslib.Collections.Benchmarks.Generics.ArrayLists;

BenchmarkRunner.Run<GenericArrayListAdditionBenchmark>();