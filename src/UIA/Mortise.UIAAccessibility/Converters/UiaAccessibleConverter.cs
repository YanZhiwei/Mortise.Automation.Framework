using System.Text.Json;
using System.Text.Json.Serialization;
using Mortise.Accessibility.Abstractions;

namespace Mortise.UiaAccessibility.Converters;

public sealed class UiaAccessibleConverter : JsonConverter<Accessible>
{
    public override Accessible? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var doc = JsonDocument.ParseValue(ref reader))
        {
            if (doc.RootElement.TryGetProperty("provider", out var typeProperty))
            {
                if (typeProperty.GetString()?.Equals("Uia", StringComparison.OrdinalIgnoreCase) ?? false)
                    return JsonSerializer.Deserialize<UiaAccessible>(doc.RootElement.GetRawText(), options);
            }

            return JsonSerializer.Deserialize<Accessible>(doc.RootElement.GetRawText(), options);
        }
    }

    public override void Write(Utf8JsonWriter writer, Accessible value, JsonSerializerOptions options)
    {
        if (value is UiaAccessible derived)
            JsonSerializer.Serialize(writer, derived, options);
        else
            JsonSerializer.Serialize(writer, value, options);
    }
}