namespace AssetRipper.VersionUtilities
{
	/// <summary>
	/// A value type for holding Unity versions
	/// </summary>
	public readonly partial struct UnityVersion
	{
		private const int majorOffset = 48;
		private const int minorOffset = 40;
		private const int buildOffset = 32;
		private const int typeOffset = 24;
		private const int typeNumberOffset = 16;
		private readonly ulong m_data;

		/// <summary>
		/// The first number in a Unity version string
		/// </summary>
		public int Major => unchecked((int)((m_data >> majorOffset) & 0xFFFFUL));

		/// <summary>
		/// The second number in a Unity version string
		/// </summary>
		public int Minor => unchecked((int)((m_data >> minorOffset) & 0xFFUL));

		/// <summary>
		/// The third number in a Unity version string
		/// </summary>
		public int Build => unchecked((int)((m_data >> buildOffset) & 0xFFUL));

		/// <summary>
		/// The letter in a Unity version string
		/// </summary>
		public UnityVersionType Type => (UnityVersionType)unchecked((int)((m_data >> typeOffset) & 0xFFUL));

		/// <summary>
		/// The last number in a Unity version string
		/// </summary>
		public int TypeNumber => unchecked((int)((m_data >> typeNumberOffset) & 0xFFUL));

		/// <summary>
		/// The minimum value this type can have
		/// </summary>
		public static UnityVersion MinVersion { get; } = new UnityVersion(0UL);

		/// <summary>
		/// The maximum value this type can have
		/// </summary>
		public static UnityVersion MaxVersion { get; } = new UnityVersion(ulong.MaxValue);

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public UnityVersion(int major)
		{
			m_data = (ulong)(major & 0xFFFF) << majorOffset;
		}

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public UnityVersion(int major, int minor)
		{
			m_data = ((ulong)(major & 0xFFFF) << majorOffset) | ((ulong)(minor & 0xFF) << minorOffset);
		}

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public UnityVersion(int major, int minor, int build)
		{
			m_data = ((ulong)(major & 0xFFFF) << majorOffset) | ((ulong)(minor & 0xFF) << minorOffset) | ((ulong)(build & 0xFF) << buildOffset);
		}

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public UnityVersion(int major, int minor, int build, UnityVersionType type)
		{
			m_data = ((ulong)(major & 0xFFFF) << majorOffset) | ((ulong)(minor & 0xFF) << minorOffset) | ((ulong)(build & 0xFF) << buildOffset)
				| ((ulong)((int)type & 0xFF) << typeOffset);
		}

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public UnityVersion(int major, int minor, int build, UnityVersionType type, int typeNumber)
		{
			m_data = ((ulong)(major & 0xFFFF) << majorOffset) | ((ulong)(minor & 0xFF) << minorOffset) | ((ulong)(build & 0xFF) << buildOffset)
				| ((ulong)((int)type & 0xFF) << typeOffset) | ((ulong)(typeNumber & 0xFF) << typeNumberOffset);
		}

		private UnityVersion(ulong data)
		{
			m_data = data;
		}
	}
}
