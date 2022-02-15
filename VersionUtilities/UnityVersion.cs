namespace AssetRipper.VersionUtilities
{
	/// <summary>
	/// A value type for holding Unity versions
	/// </summary>
	public readonly partial struct UnityVersion
	{
		private readonly ulong m_data;

		/// <summary>
		/// The first number in a Unity version string
		/// </summary>
		public int Major => unchecked((int)((m_data >> 48) & 0xFFFFUL));

		/// <summary>
		/// The second number in a Unity version string
		/// </summary>
		public int Minor => unchecked((int)((m_data >> 40) & 0xFFUL));

		/// <summary>
		/// The third number in a Unity version string
		/// </summary>
		public int Build => unchecked((int)((m_data >> 32) & 0xFFUL));

		/// <summary>
		/// The letter in a Unity version string
		/// </summary>
		public UnityVersionType Type => (UnityVersionType)unchecked((int)((m_data >> 24) & 0xFFUL));

		/// <summary>
		/// The last number in a Unity version string
		/// </summary>
		public int TypeNumber => unchecked((int)((m_data >> 16) & 0xFFUL));

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
			m_data = (ulong)(major & 0xFFFF) << 48;
		}

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public UnityVersion(int major, int minor)
		{
			m_data = ((ulong)(major & 0xFFFF) << 48) | ((ulong)(minor & 0xFF) << 40);
		}

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public UnityVersion(int major, int minor, int build)
		{
			m_data = ((ulong)(major & 0xFFFF) << 48) | ((ulong)(minor & 0xFF) << 40) | ((ulong)(build & 0xFF) << 32);
		}

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public UnityVersion(int major, int minor, int build, UnityVersionType type)
		{
			m_data = ((ulong)(major & 0xFFFF) << 48) | ((ulong)(minor & 0xFF) << 40) | ((ulong)(build & 0xFF) << 32)
				| ((ulong)((int)type & 0xFF) << 24);
		}

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public UnityVersion(int major, int minor, int build, UnityVersionType type, int typeNumber)
		{
			m_data = ((ulong)(major & 0xFFFF) << 48) | ((ulong)(minor & 0xFF) << 40) | ((ulong)(build & 0xFF) << 32)
				| ((ulong)((int)type & 0xFF) << 24) | ((ulong)(typeNumber & 0xFF) << 16);
		}

		private UnityVersion(ulong data)
		{
			m_data = data;
		}
	}
}
