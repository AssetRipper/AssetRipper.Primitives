namespace AssetRipper.VersionUtilities
{
	public partial struct CompactUnityVersion32
	{
		/// <summary>
		/// Expand to 64 bits
		/// </summary>
		/// <param name="version">The version to convert</param>
		public static implicit operator UnityVersion(CompactUnityVersion32 version)
		{
			return new UnityVersion(version.Major, version.Minor, version.Build, version.Type, version.TypeNumber);
		}

		/// <summary>
		/// Compact to 32 bits
		/// </summary>
		/// <param name="version"></param>
		public static explicit operator CompactUnityVersion32(UnityVersion version)
		{
			return new CompactUnityVersion32(version.Major, (byte)version.Minor, (byte)version.Build, version.Type, version.TypeNumber);
		}

		/// <summary>
		/// Equality operator
		/// </summary>
		/// <param name="left">The left Unity version</param>
		/// <param name="right">The right Unity version</param>
		/// <returns></returns>
		public static bool operator ==(CompactUnityVersion32 left, CompactUnityVersion32 right)
		{
			return left.m_data == right.m_data;
		}

		/// <summary>
		/// Inequality operator
		/// </summary>
		/// <param name="left">The left Unity version</param>
		/// <param name="right">The right Unity version</param>
		/// <returns></returns>
		public static bool operator !=(CompactUnityVersion32 left, CompactUnityVersion32 right)
		{
			return left.m_data != right.m_data;
		}

		/// <summary>
		/// Greater than
		/// </summary>
		/// <param name="left">The left Unity version</param>
		/// <param name="right">The right Unity version</param>
		/// <returns></returns>
		public static bool operator >(CompactUnityVersion32 left, CompactUnityVersion32 right)
		{
			return left.m_data > right.m_data;
		}

		/// <summary>
		/// Greater than or equal to
		/// </summary>
		/// <param name="left">The left Unity version</param>
		/// <param name="right">The right Unity version</param>
		/// <returns></returns>
		public static bool operator >=(CompactUnityVersion32 left, CompactUnityVersion32 right)
		{
			return left.m_data >= right.m_data;
		}

		/// <summary>
		/// Less than
		/// </summary>
		/// <param name="left">The left Unity version</param>
		/// <param name="right">The right Unity version</param>
		/// <returns></returns>
		public static bool operator <(CompactUnityVersion32 left, CompactUnityVersion32 right)
		{
			return left.m_data < right.m_data;
		}

		/// <summary>
		/// Less than or equal to
		/// </summary>
		/// <param name="left">The left Unity version</param>
		/// <param name="right">The right Unity version</param>
		/// <returns></returns>
		public static bool operator <=(CompactUnityVersion32 left, CompactUnityVersion32 right)
		{
			return left.m_data <= right.m_data;
		}
	}
}
