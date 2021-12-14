using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivityPub.Utilities.Json {

  public static partial class Entity {

    /// <summary>
    /// Used to [de]serialize all types of entity.
    /// </summary>
    public class Converter : JsonConverter<ActivityPub.Entity> {

      public override bool CanConvert(Type typeToConvert) 
        => typeof(ActivityPub.Entity) == typeToConvert;

      public override ActivityPub.Entity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
       // pure strings turn into link entities
        if(reader.TokenType == JsonTokenType.String) {
          return new ActivityPub.Link(reader.GetString()) { Context = null };
        }

        // Clone the reader to get the type
        Utf8JsonReader typeTestReader = reader;

        // make sure it's an object
        if(typeTestReader.TokenType != JsonTokenType.StartObject) {
          throw new JsonException();
        }

        // the first property might be type, if there's no context:
        typeTestReader.Read();
        if(typeTestReader.TokenType != JsonTokenType.PropertyName) {
          throw new JsonException();
        }

        bool? foundType = false;
        string propertyName = typeTestReader.GetString();
        // if the first property isn't type, we need to find it
        if(propertyName != "type") {
          // skip child objects
          bool isInChild = false;
          while(foundType.HasValue && (!foundType.Value)) {
            while(typeTestReader.TokenType != JsonTokenType.PropertyName || isInChild) {
              try {
                typeTestReader.Read();
              }
              catch {
                foundType = null;
                break;
              }
              if(typeTestReader.TokenType == JsonTokenType.StartObject) {
                isInChild = true;
              }
              if(typeTestReader.TokenType == JsonTokenType.EndObject) {
                isInChild = false;
              }
            }

            // ran out of tape
            if(foundType == null) {
              break;
            }

            // found a property:
            foundType = typeTestReader.GetString() == "type";
            typeTestReader.Read();
          }
        }
        else {
          typeTestReader.Read();
        }

        // Get the type property values
        IEnumerable<string> objectTypes
          = null;
        // if we didn't find any kind of type value
        if(foundType is null) {
          objectTypes = new string[] { Object.DefaultType };
        } // if the type is in an array:
        else if(typeTestReader.TokenType == JsonTokenType.StartArray) {
          var types = new List<string>();
          while(typeTestReader.TokenType != JsonTokenType.EndArray) {
            typeTestReader.Read();
            if(typeTestReader.TokenType == JsonTokenType.String) {
              types.Add(typeTestReader.GetString());
            }

            objectTypes = types;
          }
        } // if the type is just a string
        else if(typeTestReader.TokenType == JsonTokenType.String) {
          objectTypes = new string[] { typeTestReader.GetString() };
        } else 
          throw new JsonException();

        ActivityPub.Entity @base = null;
        foreach(var type in Settings.DefaultEntityTypeStrings) {
          (System.Type systemType, IEnumerable<string> activityPubTypes) = type;
          if(activityPubTypes.Intersect(objectTypes).Any()) {
            @base = JsonSerializer.Deserialize(ref reader, systemType, options) as ActivityPub.Entity;
          }
        }

        return @base // if null, try to make it into a generic Object:
          ?? (JsonSerializer.Deserialize(ref reader, typeof(Object), options) as ActivityPub.Entity);
      }

      public override void Write(Utf8JsonWriter writer, ActivityPub.Entity value, JsonSerializerOptions options) {
        JsonSerializer.Serialize(writer, value, value.GetType(), Settings.EntitySerializationOptions);
      }
    }
  }
}
