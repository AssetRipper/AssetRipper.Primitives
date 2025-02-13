#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;

namespace AssetRipper.Primitives;

[JsonConverter(typeof(UnityVersionJsonConverter))]
partial struct UnityVersion
{
}
#endif