using System;
using System.Text.Json;

namespace ActivityPub.Utilities.Json {

	/// <summary>
	/// Based On: https://stackoverflow.com/a/65430421/5037067 [Answer By dbc]
	/// </summary>
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