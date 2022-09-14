namespace JSONPloymorphism;
internal sealed class DiscriminatorConverter<T> : JsonConverter<T>
{
    private readonly string discriminatorPropertyName;
    private readonly Dictionary<string, Type> allSupportedTypes;
    private static JsonSerializerOptions DefaultOptions = new(JsonSerializerDefaults.Web);
    public DiscriminatorConverter(string discriminatorPropertyName, Type classType)
    {
        this.discriminatorPropertyName = discriminatorPropertyName;
        allSupportedTypes = typeof(T).Assembly.GetTypes()
            .Where(x => x.IsAssignableTo(typeof(T)) && x != classType)
            .ToDictionary(x => x.Name);
    }

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var discriminatorProperty = document.RootElement.EnumerateObject().Single(x => x.NameEquals(discriminatorPropertyName));
        var discriminatorValue = discriminatorProperty.Value.GetString();
        var objectType = allSupportedTypes.Single(x => x.Key == discriminatorValue).Value;
        return (T) document.Deserialize(objectType, DefaultOptions);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var jsonString = JsonSerializer.Serialize(value, value.GetType(), DefaultOptions);
        using var document = JsonDocument.Parse(jsonString);
        writer.WriteStartObject();
        foreach (var property in document.RootElement.EnumerateObject())
            property.WriteTo(writer);
        writer.WriteEndObject();
    }
}