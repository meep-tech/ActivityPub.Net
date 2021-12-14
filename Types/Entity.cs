using ActivityPub.Utilities.Json;
using ActivityPub.Utilities.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using ActivityPub.Utilities;

namespace ActivityPub {

  /// <summary>
  /// Any type extending from any type in the ActivityPub standard https://www.w3.org/TR/activitystreams-vocabulary/#types
  /// </summary>
  public abstract partial class Entity {

    /// <summary>
    /// Default media type used by links and objects
    /// </summary>
    public const string DefaultMediaType
      = "text/html";

    /// <summary>
    /// The type name for Objects
    /// </summary>
    [JsonIgnore]
    public abstract IEnumerable<string> DefaultTypes {
      get;
    }

    /// <summary>
    /// Provides the globally unique identifier for an Object or Link.
    /// 
    /// https://www.w3.org/ns/activitystreams#id
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual string Id {
      get => _id;
      init => _id = value;
    } protected string _id;

    /// <summary>
    /// Identifies the context within which the object exists or an activity was performed.
    /// The notion of "context" used is intentionally vague.
    /// 
    /// The intended function is to serve as a means of grouping objects and activities that share a common originating context or purpose.
    /// An example could be all activities relating to a common project or event.
    /// 
    /// https://www.w3.org/ns/activitystreams#context
    /// </summary>
    [JsonPropertyOrder(-100)]
    [JsonPropertyName("@context")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> Contexts {
      get => _contexts;
      init => _contexts = value?.ToList();
    } protected List<Entity> _contexts;
    /// <summary>
    /// Can be used to just get/set the default context
    /// </summary>
    [JsonIgnore]
    public virtual Entity Context {
      get => Contexts?.First();
      init {
        if(value == null) {
          _contexts = null;
        } else {
          _contexts = new List<Entity>();
          _contexts.Insert(0, value);
        }
      }
    }
    /// <summary>
    /// Can be used to just set additional contexts to the default one
    /// </summary>
    [JsonIgnore]
    public virtual IEnumerable<Entity> AdditionalContexts {
      init {
        _contexts ??= new List<Entity>();
        _contexts.AddRange(value ?? Enumerable.Empty<Entity>());
      }
    }

    /// <summary>
    /// Identifies the value of the type field of this ActicityPub entity.
    /// 
    /// https://www.w3.org/ns/activitystreams#type
    /// </summary>
    [JsonPropertyOrder(-99)]
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    public virtual IEnumerable<string> Types {
      get => _types;
      init => _types = value?.ToList();
    } protected List<string> _types;
    /// <summary>
    /// Can be used to just get/set the default Type during init
    /// </summary>
    [JsonIgnore]
    public virtual string Type {
      get => Types?.GetDefault();
      init {
        if(value == null) {
          _types = null;
        } else {
          _types = new List<string>();
          _types.Insert(0, value);
        }
      }
    }
    /// <summary>
    /// Can be used to just set additional types to the default ones
    /// </summary>
    [JsonIgnore]
    public virtual IEnumerable<string> AdditionalTypes {
      init {
        _types ??= new List<string>();
        _types.AddRange(value ?? Enumerable.Empty<string>());
      }
    }

    /// <summary>
    /// When used on a Link, identifies the MIME media type of the referenced resource.
    /// When used on an Object, identifies the MIME media type of the value of the content property.
    /// If not specified, the content property is assumed to contain text/html content.
    /// </summary>
    [JsonPropertyName("mediaType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual string MediaType {
      get => _mediaType;
      init => _mediaType = value;
    } protected string _mediaType
      = DefaultMediaType;

    /// <summary>
    /// A simple, human-readable, plain-text name for the object.
    /// HTML markup must not be included. 
    /// The name may be expressed using multiple language-tagged values.
    /// </summary>
    [JsonPropertyName("nameMap")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual LanguageMap<string> Names {
      get => _names == null ? null : new(_names);
      init {
        KeyValuePair<string, string>[] values 
          = new KeyValuePair<string, string>[value.Count];
        value?.CopyTo(values, 0);
        _names = values.ToDictionary(
          e => e.Key,
          e => e.Value
        );
      }
    } protected Dictionary<string, string> _names;
    /// <summary>
    /// Can be used to just set the default name value for the defautl language on init
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual string Name {
      get => Names?.Default;
      init {
        if(value == null) {
          _names = null;
        } else {
          _names ??= new Dictionary<string, string>();
          _names.SetForDefaultLanguage(value);
        }
      }
    }

    /// <summary>
    /// Identifies an entity that provides a preview of this object.
    /// </summary>
    [JsonPropertyName("preview")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> Previews {
      get => _previews;
      init => _previews = value?.ToList();
    } protected List<Entity> _previews;
    /// <summary>
    /// Can be used to just get/set the default/first Preview
    /// </summary>
    [JsonIgnore]
    public virtual Entity Preview {
      get => Previews?.GetDefault();
      init {
        if(value == null) {
          _types = null;
        } else {
          _previews ??= new List<Entity>();
          _previews.SetDefault(value);
        }
      }
    }

    /// <summary>
    /// Default ctor for an entity
    /// </summary>
    protected Entity(bool withoutContext = false) {
      if(!withoutContext && Context == null) {
        Context = Settings.DefaultContext;
      }
      Types = DefaultTypes;
    }

    /// <summary>
    /// Turn a string into a link by default.
    /// </summary>
    /// <param name="link"></param>
    public static implicit operator Entity(string link)
      => new Link(link);

    /// <summary>
    /// ToString is To Json String
    /// </summary>
    public override string ToString()
      => this.Serialize();
  }
}
