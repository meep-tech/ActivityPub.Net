using System;
using System.Collections.Generic;
using System.Linq;

namespace ActivityPub.Utilities.Collections {

  /// <summary>
  /// A list restricted to certain valid types set on init:
  /// </summary>
  public class RestrictiveList<TValue> : List<TValue> {

    /// <summary>
    /// Types to restrict validation to
    /// </summary>
    public IEnumerable<Type> ValidTypes {
      private get;
      init;
    }

    /// <summary>
    /// Add an item to the list
    /// </summary>
    public new void Add(TValue item) {
      if(!(ValidTypes?.Contains(item.GetType()) ?? true)) {
        throw new ArgumentException($"Can only add types in ValidTypes to the collection");
      }
      base.Add(item);
    }

    /// <summary>
    /// Insert an item into the list
    /// </summary>
    public new void Insert(int index, TValue item) {
      if(!(ValidTypes?.Contains(item.GetType()) ?? true)) {
        throw new ArgumentException($"Can only add types in ValidTypes to the collection");
      }
      base.Insert(index, item);
    }
  }

  public static class RestrictiveListExtensions {

    /// <summary>
    /// Turn an enumerable into a restrictive list
    /// </summary>
    public static RestrictiveList<T> ToRestrictiveList<T>(this IEnumerable<T> values) {
      var @return = new RestrictiveList<T>();
      values.ToList().ForEach(@return.Add);
      return @return;
    }
  }
}
