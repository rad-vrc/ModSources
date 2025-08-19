using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.IO
{
	/// <summary>
	/// Tag compounds contained named values, serializable as per the NBT spec: <see href="https://minecraft.wiki/w/NBT_format">NBT spec wiki page</see> <br />
	/// All primitive data types are supported as well as byte[], int[] and Lists of other supported data types <br />
	/// Lists of Lists are internally stored as IList&lt;IList&gt; <br />
	/// Modification of lists stored in a TagCompound will only work if there were no type conversions involved and is not advised <br />
	/// bool is supported using TagConverter, serialized as a byte. IList&lt;bool&gt; will serialize as IList&lt;byte&gt; (quite inefficient) <br />
	/// Additional conversions can be added using TagConverter
	/// <para /> The <see href="https://github.com/tModLoader/tModLoader/wiki/Saving-and-loading-using-TagCompound">Saving and loading using TagCompound</see> teaches how to properly use the TagCompound class.
	/// </summary>
	// Token: 0x02000287 RID: 647
	public class TagCompound : IEnumerable<KeyValuePair<string, object>>, IEnumerable, ICloneable
	{
		/// <summary>
		/// Retrieves the value corresponding to the <paramref name="key" /> of the Type <typeparamref name="T" />.
		/// <para /> If no entry is found, a default value will be returned. For primitives this will be the typical default value for that primitive (0, false, ""). For classes and structs the returned value will be the result of calling the appropriate deserialize method with an empty TagCompound. This will usually be a default instance of that class or struct. For <see cref="T:System.Collections.Generic.List`1" />, an empty list is returned. For arrays, an empty array would be returned.
		/// <para /> If the found entry is not of the Type <typeparamref name="T" /> an exception will be thrown.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <exception cref="T:System.IO.IOException"></exception>
		// Token: 0x06002C11 RID: 11281 RVA: 0x00525FEC File Offset: 0x005241EC
		public T Get<T>(string key)
		{
			T value;
			if (!this.TryGet<T>(key, out value) && value == null)
			{
				try
				{
					return TagIO.Deserialize<T>(null);
				}
				catch (Exception e)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 2);
					defaultInterpolatedStringHandler.AppendLiteral("NBT Deserialization (type=");
					defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(T));
					defaultInterpolatedStringHandler.AppendLiteral(",");
					defaultInterpolatedStringHandler.AppendLiteral("entry=");
					defaultInterpolatedStringHandler.AppendFormatted(TagPrinter.Print(new KeyValuePair<string, object>(key, null)));
					defaultInterpolatedStringHandler.AppendLiteral(")");
					throw new IOException(defaultInterpolatedStringHandler.ToStringAndClear(), e);
				}
				return value;
			}
			return value;
		}

		/// <summary>
		/// Attempts to retrieve the value corresponding to the provided <paramref name="key" /> and sets it to <paramref name="value" />. If found, true is returned by this method, otherwise false is returned.
		/// <para /> Unlike <see cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />, TryGet will not attempt to set <paramref name="value" /> to a valid fallback default created by the deserialize method of classes and structs in the case where the key is not found.
		/// <para /> Use this instead of <see cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" /> in situations where falling back to the default value for missing entries would be undesirable.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <exception cref="T:System.IO.IOException"></exception>
		// Token: 0x06002C12 RID: 11282 RVA: 0x00526098 File Offset: 0x00524298
		public bool TryGet<T>(string key, out T value)
		{
			object tag;
			if (!this.dict.TryGetValue(key, out tag))
			{
				value = default(T);
				return false;
			}
			bool result;
			try
			{
				value = TagIO.Deserialize<T>(tag);
				result = true;
			}
			catch (Exception e)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 2);
				defaultInterpolatedStringHandler.AppendLiteral("NBT Deserialization (type=");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(T));
				defaultInterpolatedStringHandler.AppendLiteral(",");
				defaultInterpolatedStringHandler.AppendLiteral("entry=");
				defaultInterpolatedStringHandler.AppendFormatted(TagPrinter.Print(new KeyValuePair<string, object>(key, tag)));
				defaultInterpolatedStringHandler.AppendLiteral(")");
				throw new IOException(defaultInterpolatedStringHandler.ToStringAndClear(), e);
			}
			return result;
		}

		/// <summary> Use this to set values to the TagCompound indexed by the specified <paramref name="key" />. </summary>
		// Token: 0x06002C13 RID: 11283 RVA: 0x00526150 File Offset: 0x00524350
		public void Set(string key, object value, bool replace = false)
		{
			if (value == null)
			{
				this.Remove(key);
				return;
			}
			object serialized;
			try
			{
				serialized = TagIO.Serialize(value);
			}
			catch (IOException e)
			{
				string valueInfo = "value=" + ((value != null) ? value.ToString() : null);
				if (value.GetType().ToString() != value.ToString())
				{
					string str = "type=";
					Type type = value.GetType();
					valueInfo = str + ((type != null) ? type.ToString() : null) + "," + valueInfo;
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 2);
				defaultInterpolatedStringHandler.AppendLiteral("NBT Serialization (key=");
				defaultInterpolatedStringHandler.AppendFormatted(key);
				defaultInterpolatedStringHandler.AppendLiteral(",");
				defaultInterpolatedStringHandler.AppendFormatted(valueInfo);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				throw new IOException(defaultInterpolatedStringHandler.ToStringAndClear(), e);
			}
			if (replace)
			{
				this.dict[key] = serialized;
				return;
			}
			this.dict.Add(key, serialized);
		}

		/// <summary> Returns true if an entry with specified key exists. This is useful to check if a key is present prior to retrieving the value in the case that key potentially won't exist and the default behavior of <see cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" /> would be undesirable. The <see href="https://github.com/tModLoader/tModLoader/wiki/Saving-and-loading-using-TagCompound#mod-version-updates">Mod Version Updates section of the wiki guide</see> shows an example of one such situation. </summary>
		// Token: 0x06002C14 RID: 11284 RVA: 0x00526244 File Offset: 0x00524444
		public bool ContainsKey(string key)
		{
			return this.dict.ContainsKey(key);
		}

		/// <summary> Removed the entry corresponding to the <paramref name="key" />. Returns true if the element is successfully found and removed; otherwise, false. </summary>
		// Token: 0x06002C15 RID: 11285 RVA: 0x00526252 File Offset: 0x00524452
		public bool Remove(string key)
		{
			return this.dict.Remove(key);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />
		// Token: 0x06002C16 RID: 11286 RVA: 0x00526260 File Offset: 0x00524460
		public byte GetByte(string key)
		{
			return this.Get<byte>(key);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />
		// Token: 0x06002C17 RID: 11287 RVA: 0x00526269 File Offset: 0x00524469
		public short GetShort(string key)
		{
			return this.Get<short>(key);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />
		// Token: 0x06002C18 RID: 11288 RVA: 0x00526272 File Offset: 0x00524472
		public int GetInt(string key)
		{
			return this.Get<int>(key);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />
		// Token: 0x06002C19 RID: 11289 RVA: 0x0052627B File Offset: 0x0052447B
		public long GetLong(string key)
		{
			return this.Get<long>(key);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />
		// Token: 0x06002C1A RID: 11290 RVA: 0x00526284 File Offset: 0x00524484
		public float GetFloat(string key)
		{
			return this.Get<float>(key);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />
		// Token: 0x06002C1B RID: 11291 RVA: 0x0052628D File Offset: 0x0052448D
		public double GetDouble(string key)
		{
			return this.Get<double>(key);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />
		// Token: 0x06002C1C RID: 11292 RVA: 0x00526296 File Offset: 0x00524496
		public byte[] GetByteArray(string key)
		{
			return this.Get<byte[]>(key);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />
		// Token: 0x06002C1D RID: 11293 RVA: 0x0052629F File Offset: 0x0052449F
		public int[] GetIntArray(string key)
		{
			return this.Get<int[]>(key);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />
		// Token: 0x06002C1E RID: 11294 RVA: 0x005262A8 File Offset: 0x005244A8
		public string GetString(string key)
		{
			return this.Get<string>(key);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />
		// Token: 0x06002C1F RID: 11295 RVA: 0x005262B1 File Offset: 0x005244B1
		public IList<T> GetList<T>(string key)
		{
			return this.Get<List<T>>(key);
		}

		/// <summary>
		/// GetCompound can be used to retrieve nested TagCompounds. This can be useful for saving complex data. An empty TagCompound is returned if not present.
		/// <para /> <inheritdoc cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />
		/// </summary>
		// Token: 0x06002C20 RID: 11296 RVA: 0x005262BA File Offset: 0x005244BA
		public TagCompound GetCompound(string key)
		{
			return this.Get<TagCompound>(key);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" />
		// Token: 0x06002C21 RID: 11297 RVA: 0x005262C3 File Offset: 0x005244C3
		public bool GetBool(string key)
		{
			return this.Get<bool>(key);
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x005262CC File Offset: 0x005244CC
		public short GetAsShort(string key)
		{
			object o = this.Get<object>(key);
			short? num = o as short?;
			if (num == null)
			{
				return (short)(o as byte?).GetValueOrDefault();
			}
			return num.GetValueOrDefault();
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x00526314 File Offset: 0x00524514
		public int GetAsInt(string key)
		{
			object o = this.Get<object>(key);
			int? num = o as int?;
			if (num != null)
			{
				return num.GetValueOrDefault();
			}
			short? num2 = o as short?;
			if (num2 == null)
			{
				return (int)(o as byte?).GetValueOrDefault();
			}
			return (int)num2.GetValueOrDefault();
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x00526378 File Offset: 0x00524578
		public long GetAsLong(string key)
		{
			object o = this.Get<object>(key);
			long? num = o as long?;
			if (num == null)
			{
				return (long)((o as int?) ?? ((int)((o as short?) ?? ((short)(o as byte?).GetValueOrDefault()))));
			}
			return num.GetValueOrDefault();
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x005263FC File Offset: 0x005245FC
		public double GetAsDouble(string key)
		{
			object o = this.Get<object>(key);
			double? num = o as double?;
			if (num == null)
			{
				return (double)(o as float?).GetValueOrDefault();
			}
			return num.GetValueOrDefault();
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x00526444 File Offset: 0x00524644
		public object Clone()
		{
			TagCompound copy = new TagCompound();
			foreach (KeyValuePair<string, object> entry in this)
			{
				copy.Set(entry.Key, TagIO.Clone<object>(entry.Value), false);
			}
			return copy;
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x005264A8 File Offset: 0x005246A8
		public override string ToString()
		{
			return TagPrinter.Print(this);
		}

		/// <summary>
		/// Use this to add values to the TagCompound, similar to working with a <see cref="T:System.Collections.Generic.Dictionary`2" />. An alternate to this is calling <see cref="M:Terraria.ModLoader.IO.TagCompound.Add(System.String,System.Object)" /> directly.
		/// <para /> If is also possible to use this to retrieve entries from the TagCompound directly, but since the return value is <see cref="T:System.Object" /> this is rarely the best approach. Usually one of the <see cref="M:Terraria.ModLoader.IO.TagCompound.Get``1(System.String)" /> methods should be used for this. One situation where this is necessary is described in the <see href="https://github.com/tModLoader/tModLoader/wiki/Saving-and-loading-using-TagCompound#updates-to-data-type">Updates to data Type section of the wiki guide</see>.
		/// </summary>
		// Token: 0x17000514 RID: 1300
		public object this[string key]
		{
			get
			{
				return this.Get<object>(key);
			}
			set
			{
				this.Set(key, value, true);
			}
		}

		/// <summary> Use this to add values to the TagCompound indexed by the specified <paramref name="key" />. </summary>
		// Token: 0x06002C2A RID: 11306 RVA: 0x005264C4 File Offset: 0x005246C4
		public void Add(string key, object value)
		{
			this.Set(key, value, false);
		}

		/// <summary> Use this to add a KeyValuePair to the TagCompound. </summary>
		// Token: 0x06002C2B RID: 11307 RVA: 0x005264CF File Offset: 0x005246CF
		public void Add(KeyValuePair<string, object> entry)
		{
			this.Set(entry.Key, entry.Value, false);
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x005264E6 File Offset: 0x005246E6
		public void Clear()
		{
			this.dict.Clear();
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x005264F3 File Offset: 0x005246F3
		public int Count
		{
			get
			{
				return this.dict.Count;
			}
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x00526500 File Offset: 0x00524700
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return this.dict.GetEnumerator();
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x00526512 File Offset: 0x00524712
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04001BFC RID: 7164
		private Dictionary<string, object> dict = new Dictionary<string, object>();
	}
}
