using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Runtime.CompilerServices;

namespace AssetRipper.Primitives.Benckmarks;

[SimpleJob(RuntimeMoniker.Net80)]
[RPlotExporter]
public class YearVersionCheck
{
	[Params(4, 5, 6, 7, 2016, 2017, 2018, 2022, 2023, 2024)]
	public ushort Major;

	[Benchmark]
	public bool Simple() => Major is >= 2017 and <= 2023;

	[Benchmark]
	public bool BitsWith2016() => BitComparison(Major);

	[Benchmark]
	public bool BitsWithout2016() => BitComparison(Major) && Major is not 2016;

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static bool BitComparison(uint value)
	{
		//This function determines if an unsigned integer
		//is between 2016 and 2023, inclusive. It does this
		//by comparing bits of the value with know constants.

		const uint VariableBitMask = 0x7;
		const uint FixedBitMask = ~VariableBitMask;
		const uint ExpectedFixedBits = 0x7E0;//2016
		return (value & FixedBitMask) == ExpectedFixedBits;
	}

	/* Results:
	 * Due to the simplicity of the methods being profiled,
	 * benchmarking was inconclusive. The costs of calling
	 * these methods are almost neglible.
	 * 
	 * Decision:
	 * For the time being, the library will continue to use
	 * the simple bounds check. If another option is shown
	 * to be significantly better, I am open to switching.
	 */
}