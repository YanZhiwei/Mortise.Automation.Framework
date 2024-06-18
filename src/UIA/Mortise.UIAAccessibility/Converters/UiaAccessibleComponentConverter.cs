using System.Text.Json;
using System.Text.Json.Serialization;
using Mortise.Accessibility.Abstractions;

namespace Mortise.UiaAccessibility.Converters;

public sealed class UiaAccessibleComponentConverter : JsonConverter<AccessibleComponent>
{
    public override AccessibleComponent? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        using (var doc = JsonDocument.ParseValue(ref reader))
        {
            if (doc.RootElement.TryGetProperty("className", out var typeProperty))
                return JsonSerializer.Deserialize<UiaAccessibleComponent>(doc.RootElement.GetRawText(), options);
            return JsonSerializer.Deserialize<AccessibleComponent>(doc.RootElement.GetRawText(), options);
        }
    }

    public override void Write(Utf8JsonWriter writer, AccessibleComponent value, JsonSerializerOptions options)
    {
        if (value is UiaAccessibleComponent derived)
            JsonSerializer.Serialize(writer, derived, options);
        else
            JsonSerializer.Serialize(writer, value, options);
    }
}