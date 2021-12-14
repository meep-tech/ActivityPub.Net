using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ActivityPub.Utilities.Json {

  /// <summary>
  /// Used to convert a single object or an array of objects.
  /// For IEnumerable properties on entites.
  /// This serializes to/from a list or a single item, depending on what is there.
  /// </summary>
  public class SingleOrArrayConverter<TEnumeratingObject> 
    : System.Text.Json.Serialization.JsonConverter<IEnumerable<TEnumeratingObject>> 
  {

    public override IEnumerable<TEnumeratingObject> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      if(reader.TokenType == JsonTokenType.StartArray) {
        return JsonSerializer.Deserialize<List<TEnumeratingObject>>(ref reader, options);
      }
      else {
        return new[] {JsonSerializer.Deserialize<TEnumeratingObject>(ref reader, options)};
      }
    }

    public override void Write(Utf8JsonWriter writer, IEnumerable<TEnumeratingObject> value, JsonSerializerOptions options) {
      if(value is List<TEnumeratingObject> list) {
        if(list.Count == 1) {
          JsonSerializer.Serialize(writer, value.First(), list.First().GetType(), options);
        }
        else {
          JsonSerializer.Serialize(writer, value, list.GetType(), options);
        }
      } else throw new NotImplementedException();
    }
  }
}
