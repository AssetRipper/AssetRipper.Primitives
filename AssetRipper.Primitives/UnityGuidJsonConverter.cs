#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AssetRipper.Primitives;

public sealed class UnityGuidJsonConverter : JsonConverter<UnityGuid>
{
	public override UnityGuid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return UnityGuid.Parse(reader.GetString() ?? throw new JsonException("String was read as null"));
	}

	public override void Write(Utf8JsonWriter writer, UnityGuid value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString());
	}
}
#endif