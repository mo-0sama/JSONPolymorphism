namespace JSONPloymorphism;
public static class JsonExtensions
{
    public static void AddDiscriminatorConverterForHierarchy<TBaseClass>(
        this JsonSerializerOptions options,
        string discriminatorPropertyName = "discriminator")
    {
        var converters = typeof(TBaseClass).Assembly.GetTypes()
            .Where(x => x.IsAssignableTo(typeof(TBaseClass)))
            .Select(x => (JsonConverter)Activator.CreateInstance(
                typeof(DiscriminatorConverter<>).MakeGenericType(x),
                discriminatorPropertyName,
                typeof(TBaseClass)));

        foreach (var converter in converters)
            options.Converters.Add(converter);
    }
}