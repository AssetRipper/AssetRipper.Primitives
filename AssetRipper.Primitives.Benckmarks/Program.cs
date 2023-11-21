using BenchmarkDotNet.Running;

namespace AssetRipper.Primitives.Benckmarks;

internal static class Program
{
	static void Main()
	{
		BenchmarkRunner.Run(typeof(Program).Assembly);
	}
}
