using System;

namespace AssetRipper.VersionUtilities
{
	internal static class CharacterExtensions
	{
		public static int ParseDigit(this char _this)
		{
			return _this - '0';
		}

		public static UnityVersionType ToUnityVersionType(this char _this)
		{
			return _this switch
			{
				'a' => UnityVersionType.Alpha,
				'b' => UnityVersionType.Beta,
				'c' => UnityVersionType.China,
				'f' => UnityVersionType.Final,
				'p' => UnityVersionType.Patch,
				'x' => UnityVersionType.Experimental,
				_ => throw new Exception($"Unsupported version type {_this}"),
			};
		}
	}
}
