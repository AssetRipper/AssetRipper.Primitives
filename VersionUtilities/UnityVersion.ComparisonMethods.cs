namespace AssetRipper.VersionUtilities
{
	public readonly partial struct UnityVersion
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		public bool IsEqual(int major) => this == From(major);
		public bool IsEqual(int major, int minor) => this == From(major, minor);
		public bool IsEqual(int major, int minor, int build) => this == From(major, minor, build);
		public bool IsEqual(int major, int minor, int build, UnityVersionType type) => this == From(major, minor, build, type);
		public bool IsEqual(int major, int minor, int build, UnityVersionType type, int typeNumber) => this == new UnityVersion(major, minor, build, type, typeNumber);
		public bool IsEqual(string version) => this == Parse(version);

		public bool IsLess(int major) => this < From(major);
		public bool IsLess(int major, int minor) => this < From(major, minor);
		public bool IsLess(int major, int minor, int build) => this < From(major, minor, build);
		public bool IsLess(int major, int minor, int build, UnityVersionType type) => this < From(major, minor, build, type);
		public bool IsLess(int major, int minor, int build, UnityVersionType type, int typeNumber) => this < new UnityVersion(major, minor, build, type, typeNumber);
		public bool IsLess(string version) => this < Parse(version);

		public bool IsLessEqual(int major) => this <= From(major);
		public bool IsLessEqual(int major, int minor) => this <= From(major, minor);
		public bool IsLessEqual(int major, int minor, int build) => this <= From(major, minor, build);
		public bool IsLessEqual(int major, int minor, int build, UnityVersionType type) => this <= From(major, minor, build, type);
		public bool IsLessEqual(int major, int minor, int build, UnityVersionType type, int typeNumber) => this <= new UnityVersion(major, minor, build, type, typeNumber);
		public bool IsLessEqual(string version) => this <= Parse(version);

		public bool IsGreater(int major) => this > From(major);
		public bool IsGreater(int major, int minor) => this > From(major, minor);
		public bool IsGreater(int major, int minor, int build) => this > From(major, minor, build);
		public bool IsGreater(int major, int minor, int build, UnityVersionType type) => this > From(major, minor, build, type);
		public bool IsGreater(int major, int minor, int build, UnityVersionType type, int typeNumber) => this > new UnityVersion(major, minor, build, type, typeNumber);
		public bool IsGreater(string version) => this > Parse(version);

		public bool IsGreaterEqual(int major) => this >= From(major);
		public bool IsGreaterEqual(int major, int minor) => this >= From(major, minor);
		public bool IsGreaterEqual(int major, int minor, int build) => this >= From(major, minor, build);
		public bool IsGreaterEqual(int major, int minor, int build, UnityVersionType type) => this >= From(major, minor, build, type);
		public bool IsGreaterEqual(int major, int minor, int build, UnityVersionType type, int typeNumber) => this >= new UnityVersion(major, minor, build, type, typeNumber);
		public bool IsGreaterEqual(string version) => this >= Parse(version);
		
		private UnityVersion From(int major)
		{
			ulong data = ((ulong)(major & 0xFFFF) << majorOffset) | (0x0000FFFFFFFFFFFFUL & m_data);
			return new UnityVersion(data);
		}
		private UnityVersion From(int major, int minor)
		{
			ulong data = ((ulong)(major & 0xFFFF) << majorOffset) | ((ulong)(minor & 0xFF) << minorOffset) | (0x000000FFFFFFFFFFUL & m_data);
			return new UnityVersion(data);
		}
		private UnityVersion From(int major, int minor, int build)
		{
			ulong data = ((ulong)(major & 0xFFFF) << majorOffset) | ((ulong)(minor & 0xFF) << minorOffset) | ((ulong)(build & 0xFF) << buildOffset) |
				(0x00000000FFFFFFFFUL & m_data);
			return new UnityVersion(data);
		}
		private UnityVersion From(int major, int minor, int build, UnityVersionType type)
		{
			ulong data = ((ulong)(major & 0xFFFF) << majorOffset) | ((ulong)(minor & 0xFF) << minorOffset) | ((ulong)(build & 0xFF) << buildOffset) |
				((ulong)((int)type & 0xFF) << typeOffset) | (0x0000000000FFFFFFUL & m_data);
			return new UnityVersion(data);
		}
		private UnityVersion From(int major, int minor, int build, UnityVersionType type, int typeNumber)
		{
			ulong data = ((ulong)(major & 0xFFFF) << majorOffset) | ((ulong)(minor & 0xFF) << minorOffset) | ((ulong)(build & 0xFF) << buildOffset)
				| ((ulong)((int)type & 0xFF) << typeOffset) | ((ulong)(typeNumber & 0xFF) << typeNumberOffset) | (0x000000000000FFFFUL & m_data);
			return new UnityVersion(data);
		}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}
