using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ActivityPub.Utilities {

  /// <summary>
  /// Static settings for ActivityPub
  /// </summary>
  public static class Settings {

    static Settings() {
      // Default settings with the two built in converters
      EntitySerializationOptions = new JsonSerializerOptions {
        Converters = {
          new Json.Entity.Converter(),
          new Json.Link.Converter()
        },
        WriteIndented = true
      };

      ///Collect all default entity types. Used for serialization and deserialization:
      IEnumerable<System.Type> entityTypes = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(type => typeof(Entity).IsAssignableFrom(type) 
          && type.IsClass && !type.IsAbstract);

      entityTypes.ToList().ForEach(type
        => _defaultTypes[type] 
          = (type.GetConstructor(
              System.Reflection.BindingFlags.Public 
                | System.Reflection.BindingFlags.NonPublic 
                | System.Reflection.BindingFlags.Instance,
              Array.Empty<Type>()
            )?.Invoke(null) as Entity 
              ?? throw new NotImplementedException(
                "ActivityPub Entity Subtype: " +
                type.FullName +
                ", does NOT have the required Private or Public Parameterless Constructor"
              )
            ).DefaultTypes
      );
    }

    /// <summary>
    /// Default serialization settings for entities
    /// </summary>
    public static JsonSerializerOptions EntitySerializationOptions {
      get;
    }

    /// <summary>
    /// This must be an entity with no context of it's own, else this will loop.
    /// </summary>
    public static Entity DefaultContext
      = null;

    /// <summary>
    /// The default set language
    /// </summary>
    public static string DefaultLanguage
      = "en";

    /// <summary>
    /// Default Entity Types for each System.Type that inherits from ActivityPub.Types.Entity
    /// </summary>
    public static IReadOnlyDictionary<Type, IEnumerable<string>> DefaultEntityTypeStrings
      => _defaultTypes;
    static Dictionary<Type, IEnumerable<string>> _defaultTypes {
      get;
    } = new Dictionary<Type, IEnumerable<string>>();
  }
}
