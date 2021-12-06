using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivityPub.Utilities {
	public abstract class DefaultConverterFactory<T> : JsonConverterFactory {
		class DefaultConverter : JsonConverter<T> {
			readonly JsonSerializerOptions modifiedOptions;
			readonly DefaultConverterFactory<T> factory;
			readonly JsonConverter<T> defaultConverter;

			public DefaultConverter(JsonSerializerOptions options, DefaultConverterFactory<T> factory) {
				this.factory = factory;
				this.modifiedOptions = options.CopyAndRemoveConverter(factory.GetType());
				this.defaultConverter = (JsonConverter<T>)modifiedOptions.GetConverter(typeof(T));
			}

			public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) => factory.Write(writer, value, modifiedOptions, defaultConverter);

			public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => factory.Read(ref reader, typeToConvert, modifiedOptions, defaultConverter);
		}

		protected virtual T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions modifiedOptions, JsonConverter<T> defaultConverter)
	 => defaultConverter.ReadOrSerialize<T>(ref reader, typeToConvert, modifiedOptions);

		protected virtual void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions modifiedOptions, JsonConverter<T> defaultConverter)
			=> defaultConverter.WriteOrSerialize(writer, value, modifiedOptions);

		public override bool CanConvert(Type typeToConvert) => typeof(T) == typeToConvert;

		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) => new DefaultConverter(options, this);
	}

	public static class JsonSerializerExtensions {
		public static JsonSerializerOptions CopyAndRemoveConverter(this JsonSerializerOptions options, Type converterType) {
			var copy = new JsonSerializerOptions(options);
			for(var i = copy.Converters.Count - 1; i >= 0; i--)
				if(copy.Converters[i].GetType() == converterType)
					copy.Converters.RemoveAt(i);
			return copy;
		}

		public static void WriteOrSerialize<T>(this JsonConverter<T> converter, Utf8JsonWriter writer, T value, JsonSerializerOptions options) {
			if(converter != null)
				converter.Write(writer, value, options);
			else
				JsonSerializer.Serialize(writer, value, options);
		}

		public static T ReadOrSerialize<T>(this JsonConverter<T> converter, ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
			if(converter != null)
				return converter.Read(ref reader, typeToConvert, options);
			else
				return (T)JsonSerializer.Deserialize(ref reader, typeToConvert, options);
		}
	}
}