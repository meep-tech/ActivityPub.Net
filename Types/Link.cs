using ActivityPub.Collections;
using ActivityPub.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivityPub.Types {

  /// <summary>
  /// A Link is an indirect, qualified reference to a resource identified by a URL.
  /// The fundamental model for links is established by [ RFC5988].
  /// Many of the properties defined by the Activity Vocabulary allow values that are either instances of Object or Link.
  /// When a Link is used, it establishes a qualified relation connecting the subject (the containing object) to the resource identified by the href.
  /// Properties of the Link are properties of the reference as opposed to properties of the resource.
  /// 
  /// https://www.w3.org/ns/activitystreams#Link
  /// </summary>
  [JsonSerializable(typeof(Link))]
  public class Link : Entity {

    /// <summary>
    /// The type name for Objects
    /// </summary>
    [JsonIgnore]
    public override IEnumerable<string> DefaultTypes {
      get;
    } = new string[] {
      "Link"
    };

    /// <summary>
    /// The target resource pointed to by a Link.
    /// </summary>
    [JsonPropertyName("href")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual string Href {
      get => _href;
      init => _href = value;
    }
    protected string _href;

    /// <summary>
    /// A link relation associated with a Link. 
    /// The value must conform to both the [HTML5] and [RFC5988] "link relation" definitions.
    /// In the [HTML5], any string not containing the 
    /// "space" U+0020,
    /// "tab" (U+0009), 
    /// "LF" (U+000A),
    /// "FF" (U+000C),
    /// "CR" (U+000D) 
    /// or "," (U+002C) 
    /// characters can be used as a valid link relation.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("rel")]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    public virtual IEnumerable<string> Rels {
      get => _rels;
      init => _rels = value?.ToList();
    }
    protected List<string> _rels;
    [JsonIgnore] public string Rel {
      get => Rels?.GetDefault();
      set {
        if(value == null) {
          _rels = null;
        }
        else {
          _rels ??= new List<string>();
          _rels.SetDefault(value);
        }
      }
    }

    /// <summary>
    /// Hints as to the language used by the target resource.
    /// Value must be a [BCP47] Language-Tag.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("hreflang")]
    public virtual string HrefLang {
      get => _hrefLang;
      init => _hrefLang = value;
    }
    protected string _hrefLang;

    /// <summary>
    /// On a Link, specifies a hint as to the rendering height in device-independent pixels of the linked resource.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("height")]
    public virtual float? Height {
      get => _height;
      init => _height = value;
    }
    [JsonIgnore]
    protected float? _height {
      get => __height;
      set {
        if(value < 0) {
          throw new InvalidOperationException("value cannot be negative");
        }

        __height = value;
      }
    }
    float? __height;

    /// <summary>
    /// On a Link, specifies a hint as to the rendering width in device-independent pixels of the linked resource.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("width")]
    public virtual float? Width {
      get => _width;
      init => _width = value;
    }
    [JsonIgnore]
    protected float? _width {
      get => __width;
      set {
        if(value < 0) {
          throw new InvalidOperationException("value cannot be negative");
        }

        __width = value;
      }
    }
    float? __width;

    /// <summary>
    /// A cache for the internal Object represented by the link.
    /// </summary>
    protected Object _object;

    /// <summary>
    /// Make a new Link.
    /// Use init to fill properties.
    /// </summary>
    public Link(string href, bool withoutContext = true) : base(withoutContext) {
      Href = href;
    }

    /// <summary>
    /// For serialization
    /// </summary>
    public Link()
      : this("") { }

    /// <summary>
    /// You can turn links into strings if you want
    /// </summary>
    /// <param name="link"></param>
    public static implicit operator string(Link link)
      => link.Href;

    /// <summary>
    /// Turn a string by default into a link like this
    /// </summary>
    /// <param name="link"></param>
    public static implicit operator Link(string link)
      => new(link);

    /// <summary>
    /// Get this link as the object it represents
    /// </summary>
    /// <returns></returns>
    public Object asObject()
      => _object ??= FetchObject(Href);

    /// <summary>
    /// Get the object from this link
    /// </summary>
    /// <param name="link"></param>
    public static implicit operator Object(Link link)
      => link.asObject();

    /// <summary>
    /// Fetch an object from it's link
    /// </summary>
    public static Object FetchObject(string href) {
      throw new System.NotImplementedException();
    }

    public new class JsonConverter : JsonConverterWithDefaultImplimentationFactory<Link> {

      protected override Link Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions modifiedOptions) {
        // pure strings turn into link entities
        if(reader.TokenType == JsonTokenType.String) {
          return new Link(reader.GetString()) { Context = null };
        } else // use default converter:
          return base.Read(ref reader, typeToConvert, modifiedOptions);
      }
    }
  }
}
