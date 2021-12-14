using ActivityPub.Utilities;
using System;
using System.Linq;
using System.Text.Json;

namespace ActivityPub.Utilities.Json {

  public static class Link {

    /// <summary>
    /// The default JsonConverter for Links
    /// </summary>
    public class Converter : JsonConverterWithDefaultImplimentationFactory<ActivityPub.Link> {

      protected override ActivityPub.Link Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions modifiedOptions) {
        // pure strings turn into link entities
        if(reader.TokenType == JsonTokenType.String) {
          return new ActivityPub.Link(reader.GetString()) { Context = null };
        } else // use default converter:
          return base.Read(ref reader, typeToConvert, modifiedOptions);
      }

      protected override void Write(Utf8JsonWriter writer, ActivityPub.Link value, JsonSerializerOptions modifiedOptions) {
        /// If everything is empty and the type is default, 
        /// .. and mediatype is default, then we can just 
        /// .. turn a link into a text link
        if(value.Types.SequenceEqual(ActivityPub.Link.DefaultTypeNames)
          && value.MediaType == ActivityPub.Link.DefaultMediaType
          && value.Rel == null
          && value.Name == null
          && value.HrefLang == null
          && value.Context == null
          && value.Id == null
          && value.Height == null
          && value.Width == null
          && value.Preview == null
        ) {
          writer.WriteStringValue(value.Href);
        } // default serialization
        else {
          base.Write(writer, value, modifiedOptions);
        }
      }
    }
  }
}
