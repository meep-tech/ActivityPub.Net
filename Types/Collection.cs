using ActivityPub.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ActivityPub.Types {
  public class Collection : Object {

    public override IEnumerable<string> DefaultTypes
      => new[] {
        "Collection"
      };

    public Collection() 
      : this(Enumerable.Empty<Entity>()) { }

    public Collection(IEnumerable<Entity> entries) 
      : base() { }

    /*/// <summary>
    /// If this collection object can no longer be modified
    /// </summary>
    [JsonIgnore]
    public virtual bool IsImmutable
      => false;

    /// <summary>
    /// Return an immutable version of this collection
    /// </summary>
    /// <returns></returns>
    public ImmutableCollection AsImmutable()
      => new ImmutableCollection(this);*/
  }
/* // I THINK COLLECTIONS ARE IMMUTABLE ALREADY DUE TO HOW IM MAKING OBJECTS WORK
  public class ImmutableCollection : Collection {

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override bool IsImmutable
      => true;

    Collection _internal;

    //TODO: finish these when collection is finished

    public override Entity Attachment {
      get => _internal.Attachment;
      init => throw new AccessViolationException();
    }

    public override IEnumerable<Entity> Attachments {
      get => _internal.Attachments;
      init => throw new AccessViolationException();
    }

    public override IEnumerable<Entity> AttributedTo {
      get => _internal.AttributedTo;
      init => throw new AccessViolationException();
    }

    public override Entity Audience {
      get => _internal.Audience;
      init => throw new AccessViolationException();
    }

    public override IEnumerable<Entity> Audiences {
      get => _internal.Audiences;
      init => throw new AccessViolationException();
    }

    public override Entity Content {
      get => _internal.Content;
      init => throw new AccessViolationException();
    }

    public override LanguageMap<Entity> Contents {
      get => _internal.Contents;
      init => throw new AccessViolationException();
    }

    public override Entity Context {
      get => _internal.Context;
      init => throw new AccessViolationException();
    }

    public override IEnumerable<Entity> Contexts {
      get => _internal.Contexts;
      init => throw new AccessViolationException();
    }

    public override DateTime? EndTime {
      get => _internal.EndTime;
      init => throw new AccessViolationException();
    }

    public override Entity Generator {
      get => _internal.Generator;
      init => throw new AccessViolationException();
    }

    public override IEnumerable<Entity> Generators {
      get => _internal.Generators;
      init => throw new AccessViolationException();
    }

    public override Entity Icon {
      get => _internal.Icon;
      init => throw new AccessViolationException();
    }

    public override IEnumerable<Entity> Icons {
      get => _internal.Icons;
      init => throw new AccessViolationException();
    }

    public override string Id {
      get => _internal.Id;
      init => throw new AccessViolationException();
    }

    public override Entity Image {
      get => _internal.Image;
      init => throw new AccessViolationException();
    }

    public override IEnumerable<Entity> Images {
      get => _internal.Images;
      init => throw new AccessViolationException();
    }

    public override IEnumerable<Entity> InReplyTo {
      get => _internal.InReplyTo;
      init => throw new AccessViolationException();
    }

    public override Entity Location {
      get => _internal.Location;
      init => throw new AccessViolationException();
    }

    public override IEnumerable<Entity> Locations {
      get => _internal.Locations;
      init => throw new AccessViolationException();
    }

    public override string Name {
      get => _internal.Name;
      init => throw new AccessViolationException();
    }

    public override LanguageMap<string> Names {
      get => _internal.Names;
      init => throw new AccessViolationException();
    }

    public override Entity Preview {
      get => _internal.Preview;
      init => throw new AccessViolationException();
    }

    public override IEnumerable<Entity> Previews {
      get => _internal.Previews;
      init => throw new AccessViolationException();
    }

    public override DateTime? Published {
      get => _internal.Published;
      init => throw new AccessViolationException();
    }

    public override Collection Replies {
      get => _internal.Replies;
      init => throw new AccessViolationException();
    }

    public override DateTime? StartTime {
      get => _internal.StartTime;
      init => throw new AccessViolationException();
    }

    public override LanguageMap<string> Summaries {
      get => _internal.Summaries;
      init => throw new AccessViolationException();
    }

    public override string Summary {
      get => _internal.Summary;
      init => throw new AccessViolationException();
    }

    public override string Type {
      get => _internal.Type;
      init => throw new AccessViolationException();
    }

    public override IEnumerable<string> Types {
      get => _internal.Types;
      init => throw new AccessViolationException();
    }

    /// <summary>
    /// Make a collection immutable
    /// </summary>
    /// <param name="original"></param>
    public ImmutableCollection(Collection original) {
      _internal = original;
    }
  }*/
}
