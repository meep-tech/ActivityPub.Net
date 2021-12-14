using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ActivityPub.Utilities {

  /// <summary>
  /// This allows the jsonconverter to fall back to default serialization.
  /// Based On: https://stackoverflow.com/a/70308500/5037067 [Answer By dbc]
  /// </summary>
  public abstract class JsonConverterWithDefaultImplimentationFactory<TTypeToConvert> 
    : JsonConverterFactory 
  {

    class Converter : JsonConverter<TTypeToConvert> {

      /// <summary>
      /// The factory that created this converter
      /// </summary>
      JsonConverterWithDefaultImplimentationFactory<TTypeToConvert> factory {
        get;
      }

      /// <summary>
      /// The options withou the custom converter
      /// </summary>
      JsonSerializerOptions trimmedOptions {
        get;
      }

      public Converter(
        JsonSerializerOptions options,
        JsonConverterWithDefaultImplimentationFactory<TTypeToConvert> factory
      ) {
        this.factory = factory;
        trimmedOptions = options.CopyOptionsAndRemoveConverter(factory.GetType());
      }

      public override TTypeToConvert Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => factory.Read(ref reader, typeToConvert, trimmedOptions);

      public override void Write(Utf8JsonWriter writer, TTypeToConvert value, JsonSerializerOptions options)
        => factory.Write(writer, value, trimmedOptions);
    }

    protected virtual TTypeToConvert Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      => (TTypeToConvert)JsonSerializer.Deserialize(ref reader, typeToConvert, options);

    protected virtual void Write(Utf8JsonWriter writer, TTypeToConvert value, JsonSerializerOptions options)
      => JsonSerializer.Serialize(writer, value, options);

    public override bool CanConvert(Type typeToConvert)
      => typeof(TTypeToConvert) == typeToConvert;

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
      => new Converter(options, this);
  }
}
