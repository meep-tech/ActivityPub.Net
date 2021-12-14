using System.Collections.Generic;
using System.Linq;

namespace ActivityPub {
  public class Collection : Object {

    public override IEnumerable<string> DefaultTypes
      => new[] {
        "Collection"
      };

    public Collection() 
      : this(Enumerable.Empty<Entity>()) { }

    public Collection(IEnumerable<Entity> entries) 
      : base() { }
  }
}
