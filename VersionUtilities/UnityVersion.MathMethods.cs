namespace AssetRipper.VersionUtilities
{
	public readonly partial struct UnityVersion
	{
		/// <summary>
		/// A maximizing function for Unity versions
		/// </summary>
		/// <param name="left">A Unity version</param>
		/// <param name="right">A Unity version</param>
		/// <returns>The larger Unity version</returns>
		public static UnityVersion Max(UnityVersion left, UnityVersion right)
		{
			return left > right ? left : right;
		}

		/// <summary>
		/// A minimizing function for Unity versions
		/// </summary>
		/// <param name="left">A Unity version</param>
		/// <param name="right">A Unity version</param>
		/// <returns>The smaller Unity version</returns>
		public static UnityVersion Min(UnityVersion left, UnityVersion right)
		{
			return left < right ? left : right;
		}

		/// <summary>
		/// A distance function for measuring version proximity
		/// </summary>
		/// <remarks>
		/// The returned value is ordinal and should not be saved anywhere.
		/// It's only for runtime comparisons, such as finding the closest version in a list.
		/// </remarks>
		/// <param name="left">A Unity version</param>
		/// <param name="right">A Unity version</param>
		/// <returns>
		/// An ordinal number representing the distance between 2 versions. 
		/// A value of zero means they're equal.
		/// </returns>
		public static ulong Distance(UnityVersion left, UnityVersion right)
		{
			return left.m_data < right.m_data 
				? right.m_data - left.m_data 
				: left.m_data - right.m_data;
		}
	}
}
