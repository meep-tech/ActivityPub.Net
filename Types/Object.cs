using ActivityPub.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ActivityPub.Types {

  /// <summary>
  /// Describes an object of any kind. 
  /// The Object type serves as the base type for most of the other kinds of objects defined in the Activity Vocabulary,
  /// including other Core types such as Activity, IntransitiveActivity, Collection and OrderedCollection.
  /// 
  /// https://www.w3.org/ns/activitystreams#Object
  /// </summary>
  public class Object : Entity {

    /// <summary>
    /// The default type for an object
    /// </summary>
    public const string DefaultType
      = "Object";

    /// <summary>
    /// The type name for Objects
    /// </summary>
    [JsonIgnore]
    public override IEnumerable<string> DefaultTypes {
      get;
    } = new string[] {
      DefaultType
    };

    /// <summary>
    /// Identifies a resource attached or related to an object that potentially requires special handling. 
    /// The intent is to provide a model that is at least semantically similar to attachments in email.
    /// </summary>
    [JsonPropertyName("attachment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> Attachments {
      get => _attachments;
      init => _attachments = value?.ToList();
    } protected List<Entity> _attachments;
    /// <summary>
    /// Can be used to just get/set the default/first Attachment
    /// </summary>
    [JsonIgnore]
    public virtual Entity Attachment {
      get => Attachments?.GetDefault();
      init {
        if(value == null) {
          _attachments = null;
        } else {
          _attachments ??= new List<Entity>();
          _attachments.SetDefault(value);
        }
      }
    }

    /// <summary>
    /// Identifies one or more entities to which this object is attributed. 
    /// The attributed entities might not be Actors. For instance, an object might be attributed to the completion of another activity.
    /// </summary>
    [JsonPropertyName("attributedTo")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> AttributedTo {
      get => _attributedTo;
      init => _attributedTo = value?.ToList();
    } protected List<Entity> _attributedTo;
    /// <summary>
    /// Can be used to just get/set the default/first AttributedTo Value
    /// </summary>
    [JsonIgnore]
    public virtual Entity Attribution {
      get => AttributedTo?.GetDefault();
      init {
        if(value == null) {
          _attributedTo = null;
        } else {
          _attributedTo ??= new List<Entity>();
          _attributedTo.SetDefault(value);
        }
      }
    }

    /// <summary>
    /// Identifies one or more entities to which this object is attributed. 
    /// The attributed entities might not be Actors. For instance, an object might be attributed to the completion of another activity.
    /// </summary>
    [JsonPropertyName("audience")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> Audiences {
      get => _audiences;
      init => _audiences = value?.ToList();
    } protected List<Entity> _audiences;
    /// <summary>
    /// Can be used to just get/set the default/first
    /// </summary>
    [JsonIgnore]
    public virtual Entity Audience {
      get => Audiences?.GetDefault();
      init {
        if(value == null) {
          _audiences = null;
        } else {
          _audiences ??= new List<Entity>();
          _audiences.SetDefault(value);
        }
      }
    }

    /// <summary>
    /// Identifies an entries considered to be part of the public primary audience of an Object
    /// </summary>
    [JsonPropertyName("to")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> To {
      get => _to;
      init => _to = value?.ToList();
    } protected List<Entity> _to;
    /// <summary>
    /// Can be used to just get/set the default/first To value
    /// </summary>
    [JsonIgnore]
    public virtual Entity At {
      get => To?.GetDefault();
      init {
        if(value == null) {
          _to = null;
        } else {
          _to ??= new List<Entity>();
          _to.SetDefault(value);
        }
      }
    }

    /// <summary>
    /// Identifies an entries considered to be part of the private primary audience of an Object
    /// </summary>
    [JsonPropertyName("bto")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> BTo {
      get => _bTo;
      init => _bTo = value?.ToList();
    } protected List<Entity> _bTo;

    /// <summary>
    /// Identifies an entries considered to be part of the public secondary audience of an Object
    /// </summary>
    [JsonPropertyName("cc")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> Cc {
      get => _cc;
      init => _cc = value?.ToList();
    } protected List<Entity> _cc;

    /// <summary>
    /// Identifies an entries considered to be part of the private secondary audience of an Object
    /// </summary>
    [JsonPropertyName("bcc")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> BCc {
      get => _bCc;
      init => _bCc = value?.ToList();
    } protected List<Entity> _bCc;

    /// <summary>
    /// Identifies the entities(e.g.an application) that generated the object.
    /// </summary>
    [JsonPropertyName("generator")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> Generators {
      get => _generators;
      init => _generators = value?.ToList();
    } protected List<Entity> _generators;
    /// <summary>
    /// Can be used to just get/set the default/first generator
    /// </summary>
    [JsonIgnore]
    public virtual Entity Generator {
      get => Generators?.GetDefault();
      init {
        if(value == null) {
          _generators = null;
        } else {
          _generators ??= new List<Entity>();
          _generators.SetDefault(value);
        }
      }
    }

    /// <summary>
    /// Indicates one or more entities for which this object is considered a response.
    /// </summary>
    [JsonPropertyName("inReplyTo")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> InReplyTo {
      get => _inReplyTo;
      init => _inReplyTo = value?.ToList();
    } protected List<Entity> _inReplyTo;

    /// <summary>
    /// Indicates an entity that icons for this object.
    /// The image should have an aspect ratio of one (horizontal) to one (vertical) and should be suitable for presentation at a small size.
    /// </summary>
    /// TODO: this probably needs custom json deserialization for when it's empty
    [JsonPropertyName("icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> Icons {
      get => _icons.Count == 0 ? null : _icons;
      init {
        _icons.Clear();
        _icons.AddRange(value ?? Enumerable.Empty<Entity>());
      }
    } [JsonIgnore] protected RestrictiveList<Entity> _icons {
      get;
    } = new RestrictiveList<Entity>() {
        ValidTypes = new Type[] {
          typeof(Link),
          typeof(Image)
        }
      };
    /// <summary>
    /// Can be used to just get/set the default/first Icon
    /// </summary>
    [JsonIgnore]
    public virtual Entity Icon {
      get => Icons?.GetDefault();
      init {
        if(!(value is null)) {
          _icons.SetDefault(value);
        }
      }
    }

    /// <summary>
    /// Indicates an entity that describes an images for this object.
    /// Unlike the icon property, there are no aspect ratio or display size limitations assumed.
    /// </summary>
    /// TODO: this probably needs custom json deserialization for when it's empty
    [JsonPropertyName("image")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> Images {
      get => _images.Count == 0 ? null : _images;
      init {
        _images.Clear();
        _images.AddRange(value ?? Enumerable.Empty<Entity>());
      }
    } [JsonIgnore] protected RestrictiveList<Entity> _images {
      get;
    } = new RestrictiveList<Entity>() {
        ValidTypes = new Type[] {
          typeof(Link),
          typeof(Image)
        }
      };
    /// <summary>
    /// Can be used to just get/set the default/first image
    /// </summary>
    [JsonIgnore]
    public virtual Entity Image {
      get => Images?.GetDefault();
      init {
        if(!(value is null)) {
          _images.SetDefault(value);
        }
      }
    }

    /// <summary>
    /// A simple, human-readable, plain-text name for the object.
    /// HTML markup must not be included. 
    /// The name may be expressed using multiple language-tagged values.
    /// </summary>
    [JsonPropertyName("summaryMap")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual LanguageMap<string> Summaries {
      get => _summaries == null ? null : new(_summaries);
      init {
        KeyValuePair<string, string>[] values
          = new KeyValuePair<string, string>[value.Count];
        value?.CopyTo(values, 0);
        _summaries = values.ToDictionary(e => e.Key, e => e.Value);
      }
    } protected Dictionary<string, string> _summaries;
    /// <summary>
    /// Can be used to just set the default name value for the defautl language on init
    /// </summary>
    [JsonPropertyName("summary")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual string Summary {
      get => Summaries?.Default;
      init {
        if(value == null) {
          _summaries = null;
        } else {
          _summaries ??= new Dictionary<string, string>();
          _summaries.SetForDefaultLanguage(value);
        }
      }
    }

    /// <summary>
    /// Identifies the entities(e.g.an application) that generated the object.
    /// </summary>
    [JsonPropertyName("contentMap")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual LanguageMap<Entity> Contents {
      get => _contents == null ? null : new(_contents);
      init {
        KeyValuePair<string, Entity>[] values
          = new KeyValuePair<string, Entity>[value.Count];
        value?.CopyTo(values, 0);
        _contents = values.ToDictionary(e => e.Key, e => e.Value);
      }
    } protected Dictionary<string, Entity> _contents;
    /// <summary>
    /// Can be used to just get/set the default/first generator
    /// </summary>
    [JsonPropertyName("content")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual Entity Content {
      get => Contents?.Default;
      init {
        if(value == null) {
          _contents = null;
        } else {
          _contents ??= new Dictionary<string, Entity>();
          _contents.SetForDefaultLanguage(value);
        }
      }
    }

    /// <summary>
    /// Identifies one or more links to representations of the object
    /// </summary>
    [JsonPropertyName("link")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Link>))]
    public virtual IEnumerable<Link> Urls {
      get => _urls;
      init => _urls = value?.ToList();
    } protected List<Link> _urls;
    /// <summary>
    /// Can be used to just get/set the default/first generator
    /// </summary>
    [JsonIgnore]
    public virtual Link Url {
      get => Urls?.GetDefault();
      init {
        if(value == null) {
          _urls = null;
        } else {
          _urls ??= new List<Link>();
          _urls.SetDefault(value);
        }
      }
    }

    /// <summary>
    /// Identifies a Collection containing objects considered to be responses to this object.
    /// </summary>
    [JsonPropertyName("replies")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual Collection Replies {
      get => _replies;
      init => _replies = value;
    } protected Collection _replies;

    /// <summary>
    /// The date and time at which the object was published
    /// </summary>
    [JsonPropertyName("published")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual DateTime? Published {
      get => _published;
      init => _published = value;
    } protected DateTime? _published;

    /// <summary>
    /// The date and time describing the actual or expected starting time of the object. 
    /// When used with an Activity object, for instance, 
    /// the startTime property specifies the moment the activity began or is expected to begin.
    /// </summary>
    [JsonPropertyName("startTime")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual DateTime? StartTime {
      get => _startTime;
      init => _startTime = value;
    } protected DateTime? _startTime;

    /// <summary>
    /// The date and time describing the actual or expected ending time of the object. 
    /// When used with an Activity object, for instance, 
    /// the endTime property specifies the moment the activity concluded or is expected to conclude.
    /// </summary>
    [JsonPropertyName("endTime")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual DateTime? EndTime {
      get => _endTime;
      init => _endTime = value;
    } protected DateTime? _endTime;

    /// <summary>
    /// The date and time at which the object was updated
    /// </summary>
    [JsonPropertyName("updated")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual DateTime? Updated {
      get => _updated;
      init => _updated = value;
    } protected DateTime? _updated;

    /// <summary>
    /// When the object describes a time-bound resource, 
    /// such as an audio or video, a meeting, etc, 
    /// the duration property indicates the object's approximate duration.
    /// The value must be expressed as an xsd:duration as defined by [ xmlschema11-2],
    /// section 3.3.6 (e.g. a period of 5 seconds is represented as "PT5S").
    /// </summary>
    [JsonPropertyName("duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual TimeSpan? Duration {
      get => _duration;
      init => _duration = value;
    } protected TimeSpan? _duration;

    /// <summary>
    /// Indicates one or more physical or logical locations associated with the object.
    /// </summary>
    [JsonPropertyName("location")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> Locations {
      get => _locations;
      init => _locations = value?.ToList();
    } protected List<Entity> _locations;
    /// <summary>
    /// Can be used to just get/set the default/first Location
    /// </summary>
    [JsonIgnore]
    public virtual Entity Location {
      get => Locations?.GetDefault();
      init {
        if(value == null) {
          _locations = null;
        } else {
          _locations ??= new List<Entity>();
          _locations.SetDefault(value);
        }
      }
    }

    /// <summary>
    /// One or more "tags" that have been associated with an objects.
    /// A tag can be any kind of Object. 
    /// The key difference between attachment and tag is that the former implies association by inclusion,
    /// while the latter implies associated by reference.
    /// </summary>
    [JsonPropertyName("tag")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(SingleOrArrayConverter<Entity>))]
    public virtual IEnumerable<Entity> Tags {
      get => _tags;
      init => _tags = value?.ToList();
    } protected List<Entity> _tags;
    /// <summary>
    /// Can be used to just get/set the default/first tag
    /// </summary>
    [JsonIgnore]
    public virtual Entity Tag {
      get => Tags?.GetDefault();
      init {
        if(value == null) {
          _tags = null;
        } else {
          _tags ??= new List<Entity>();
          _tags.SetDefault(value);
        }
      }
    }

    /// <summary>
    /// Make an Object
    /// </summary>
    public Object() 
      : base() {}
  }
}
