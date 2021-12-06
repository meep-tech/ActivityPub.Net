using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ActivityPub.Types {

  /// <summary>
  /// An object representing an image
  /// </summary>
  public class Image : Object {

    [JsonIgnore]
    public override IEnumerable<string> DefaultTypes 
      => new[] {
        "Image"
      };

    public Image(string href) : base() {
      
    } public Image() : this(null) { }
  }
}