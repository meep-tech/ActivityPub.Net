using ActivityPub.Utilities;
using System.Text.Json;

namespace ActivityPub {

  public static class EntitySerializationExtensions {

    /// <summary>
    /// Serialize an entity with default settings provided
    /// </summary>
    public static string Serialize(this Entity entity, JsonSerializerOptions optionsOverride = null)
      => JsonSerializer.Serialize(entity, optionsOverride ?? Settings.EntitySerializationOptions);

    /// <summary>
    /// Serialize an link with default settings provided
    /// </summary>
    public static string Serialize(this Link link, JsonSerializerOptions optionsOverride = null)
      => JsonSerializer.Serialize(link, optionsOverride ?? Settings.EntitySerializationOptions);

    /// <summary>
    /// Serialize an entity with default settings provided
    /// </summary>
    public static Entity DeSerializeEntity(this string json, JsonSerializerOptions optionsOverride = null)
      => JsonSerializer.Deserialize(json, typeof(Entity), optionsOverride ?? Settings.EntitySerializationOptions) as Entity;

    /// <summary>
    /// Serialize an entity with default settings provided
    /// </summary>
    public static TEntityType DeSerializeEntity<TEntityType>(this string json, JsonSerializerOptions optionsOverride = null)
      where TEntityType : Entity
        => JsonSerializer.Deserialize<TEntityType>(json, optionsOverride ?? Settings.EntitySerializationOptions);
  }
}
