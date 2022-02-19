namespace AssetRipper.VersionUtilities
{
	/// <summary>
	/// Structure representing a range of Unity versions
	/// </summary>
	public readonly struct UnityVersionRange
	{
		/// <summary>
		/// The lower bound of the range
		/// </summary>
		public UnityVersion LowerBound { get; }
		/// <summary>
		/// The upper bound of the range
		/// </summary>
		public UnityVersion UpperBound { get; }
		/// <summary>
		/// Does the range include the lower bound?
		/// </summary>
		public bool LowerInclusive { get; }
		/// <summary>
		/// Does the range include the upper bound?
		/// </summary>
		public bool UpperInclusive { get; }

		/// <summary>
		/// A constructor for the range
		/// </summary>
		/// <param name="lowerBound">The lower bound of the range</param>
		/// <param name="upperBound">The upper bound of the range</param>
		/// <param name="lowerInclusive">Does the range include the lower bound?</param>
		/// <param name="upperInclusive">Does the range include the upper bound?</param>
		public UnityVersionRange(UnityVersion lowerBound, UnityVersion upperBound, bool lowerInclusive, bool upperInclusive)
		{
			LowerBound = lowerBound;
			UpperBound = upperBound;
			LowerInclusive = lowerInclusive;
			UpperInclusive = upperInclusive;
		}

		/// <summary>
		/// Checks if a version is contained within the range
		/// </summary>
		/// <param name="version">The Unity version</param>
		/// <returns>True if it lies between the boundaries</returns>
		public bool Contains(UnityVersion version)
		{
			bool satisfiesLowerBound = LowerInclusive ? LowerBound <= version : LowerBound < version;
			return satisfiesLowerBound && (UpperInclusive ? UpperBound >= version : UpperBound > version);
		}
	}
}
