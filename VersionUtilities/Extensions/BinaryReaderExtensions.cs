using System.IO;

namespace AssetRipper.VersionUtilities.Extensions
{
	/// <summary>
	/// Unity version extension methods for <see cref="BinaryReader"/>
	/// </summary>
	public static class BinaryReaderExtensions
	{
		/// <summary>
		/// Reads a Unity version from the stream
		/// </summary>
		/// <param name="reader">A binary reader</param>
		/// <returns>The read Unity version</returns>
		public static UnityVersion ReadUnityVersion(this BinaryReader reader)
		{
			return UnityVersion.FromBits(reader.ReadUInt64());
		}

		/// <summary>
		/// Reads a Unity version from the stream
		/// </summary>
		/// <param name="reader">A binary reader</param>
		/// <returns>The read Unity version</returns>
		public static CompactUnityVersion32 ReadCompactUnityVersion32(this BinaryReader reader)
		{
			return CompactUnityVersion32.FromBits(reader.ReadUInt32());
		}

		/// <summary>
		/// Reads a Unity version from the stream
		/// </summary>
		/// <param name="reader">A binary reader</param>
		/// <returns>The read Unity version</returns>
		public static CompactUnityVersion24 ReadCompactUnityVersion24(this BinaryReader reader)
		{
			byte b = reader.ReadByte();
			ushort s = reader.ReadUInt16();
			return CompactUnityVersion24.FromBits(b, s);
		}
	}
}
