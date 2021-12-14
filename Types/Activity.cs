using ActivityPub.Utilities.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace ActivityPub {

  /// <summary>
  /// An Activity is a subtype of Object that describes some form of action that may happen,
  /// is currently happening, 
  /// or has already happened. 
  /// 
  /// The Activity type itself serves as an abstract base type for all types of activities. 
  /// It is important to note that the Activity type itself does not carry any specific semantics about the kind of action being taken.
  /// 
  /// https://www.w3.org/ns/activitystreams#Activity
  /// </summary>
  public class Activity : Object {

    public override IEnumerable<string> DefaultTypes 
      => new[] {
        "Activity"
      };

    /// <summary>
    /// Activity requires type
    /// </summary>
    public Activity(string type) : base() {
      Type = type ?? DefaultTypes.First();
    } public Activity() 
      : this(default(string)) { }

    /// <summary>
    /// Activity requires type
    /// </summary>
    public Activity(IEnumerable<string> types) : base() {
      Types = types;
    }

    /// <summary>
    /// Describes one or more entities that either performed or are expected to perform the activity. 
    /// Any single activity can have multiple actors.
    /// The actor may be specified using an indirect Link.
    /// ====
    /// Set this after AttributedTo when applicable, as this will automatically add values to _attributedTo
    /// </summary>
    [JsonPropertyName("actor")]
    public virtual IEnumerable<Entity> Actors {
      get => _actors;
      init {
        _actors = value.ToList();

        // Adds to attributed to by default:
        if(_attributedTo is null) {
          _attributedTo = value.ToList();
        } else {
          _attributedTo.AddRange(value ?? Enumerable.Empty<Entity>());
        }
      }
    } protected List<Entity> _actors;
    /// <summary>
    /// Can be used to just get/set the default/first Actor
    /// </summary>
    [JsonIgnore]
    public virtual Entity Actor {
      get => Actors.GetDefault();
      init {
        _attachments ??= new List<Entity>();
        _attachments.SetDefault(value);

        // Adds to attributed to by default:
        if(_attributedTo is null) {
          _attributedTo = new Entity[] { value }.ToList();
        } else {
          _attributedTo.Add(value);
        }
      }
    }
  }
}
