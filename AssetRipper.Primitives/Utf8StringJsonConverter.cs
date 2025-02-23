#if NET5_0_OR_GREATER
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AssetRipper.Primitives;

public sealed class Utf8StringJsonConverter : JsonConverter<Utf8String>
{
	public override Utf8String Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.GetString() ?? throw new JsonException("String was read as null");
	}

	public override void Write(Utf8JsonWriter writer, Utf8String value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString());
	}
}
#endif