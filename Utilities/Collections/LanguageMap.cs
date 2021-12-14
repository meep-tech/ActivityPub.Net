using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ActivityPub.Utilities.Collections {

  /// <summary>
  /// Used to map a language identifier to a set of values
  /// </summary>
  public class LanguageMap<TValue> 
    : IReadOnlyDictionary<string, TValue>,
    IEnumerable<TValue>
  {

    /// <summary>
    /// The default value for the default set language
    /// </summary>
    public TValue Default {
      get => TryGetValue(Settings.DefaultLanguage, out var value) 
        ? value 
        : default;
    }

    Dictionary<string, TValue> _keyValuePairs;

    public LanguageMap(Dictionary<string, TValue> values) {
      _keyValuePairs = new Dictionary<string, TValue>(values);
    }

    public LanguageMap() {
      _keyValuePairs = new Dictionary<string, TValue>();
    }

    public int Count => ((ICollection<KeyValuePair<string, TValue>>)_keyValuePairs).Count;

    public IEnumerable<string> Keys => ((IReadOnlyDictionary<string, TValue>)_keyValuePairs).Keys;

    public IEnumerable<TValue> Values => ((IReadOnlyDictionary<string, TValue>)_keyValuePairs).Values;

    public TValue this[string key] { 
      get => ((IDictionary<string, TValue>)_keyValuePairs)[key];
    }

    public bool ContainsKey(string key) {
      return ((IDictionary<string, TValue>)_keyValuePairs).ContainsKey(key);
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out TValue value) {
      return ((IDictionary<string, TValue>)_keyValuePairs).TryGetValue(key, out value);
    }

    public bool Contains(KeyValuePair<string, TValue> item) {
      return ((ICollection<KeyValuePair<string, TValue>>)_keyValuePairs).Contains(item);
    }

    public void CopyTo(KeyValuePair<string, TValue>[] array, int arrayIndex) {
      ((ICollection<KeyValuePair<string, TValue>>)_keyValuePairs).CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator() {
      return ((IEnumerable<KeyValuePair<string, TValue>>)_keyValuePairs).GetEnumerator();
    }

    IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
      => Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
      => Values.GetEnumerator();
  }

  public static class LanguageMapEnumerableExtensions {

    /// <summary>
    /// Get the default value from a collection field on an Activitypub Object
    /// </summary>
    public static TValue GetDefault<TValue>(this IEnumerable<TValue> values)
      => values is LanguageMap<TValue> langMap
        ? langMap.Default
        : values.FirstOrDefault();

    /// <summary>
    /// set the default value from a collection field on an Activitypub Object
    /// </summary>
    public static void SetForDefaultLanguage<TValue>(this Dictionary<string, TValue> values, TValue value) {
      values[Settings.DefaultLanguage] = value;
    }

    /// <summary>
    /// set the default value from a collection field on an Activitypub Object
    /// </summary>
    public static void SetDefault<TValue>(this List<TValue> values, TValue value) {
      values.Insert(0, value);
    }
  }
}
