using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivityPub.Utilities {

	public static class JsonSerializerExtensions {

		/// <summary>
		/// Remove a single converter and make a copy of the options object
		/// </summary>
		public static JsonSerializerOptions CopyOptionsAndRemoveConverter(
			this JsonSerializerOptions options,
			Type converterType
		) {
      JsonSerializerOptions @return 
				= new JsonSerializerOptions(options);
			for(int index = @return.Converters.Count - 1; index >= 0; index--) {
				if(@return.Converters[index].GetType() == converterType) {
					@return.Converters.RemoveAt(index);
				}
			}

			return @return;
		}
	}
}