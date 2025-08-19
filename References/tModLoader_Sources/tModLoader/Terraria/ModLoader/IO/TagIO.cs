using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Ionic.Zlib;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000288 RID: 648
	[NullableContext(1)]
	[Nullable(0)]
	public static class TagIO
	{
		// Token: 0x06002C31 RID: 11313 RVA: 0x0052652D File Offset: 0x0052472D
		private static TagIO.PayloadHandler GetHandler(int id)
		{
			if (id < 1 || id >= TagIO.PayloadHandlers.Length)
			{
				throw new IOException("Invalid NBT payload id: " + id.ToString());
			}
			return TagIO.PayloadHandlers[id];
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x0052655C File Offset: 0x0052475C
		private static int GetPayloadId(Type t)
		{
			int id;
			if (TagIO.PayloadIDs.TryGetValue(t, out id))
			{
				return id;
			}
			if (typeof(IList).IsAssignableFrom(t))
			{
				return 9;
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Invalid NBT payload type '");
			defaultInterpolatedStringHandler.AppendFormatted<Type>(t);
			defaultInterpolatedStringHandler.AppendLiteral("'");
			throw new IOException(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x005265C8 File Offset: 0x005247C8
		private static Type GetListElementType(Type type)
		{
			Type elemType = type.GetElementType();
			if (elemType != null)
			{
				return elemType;
			}
			while (!type.IsGenericType || !(type.GetGenericTypeDefinition() == typeof(List<>)))
			{
				if (type.BaseType == null)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Invalid NBT payload type '");
					defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
					defaultInterpolatedStringHandler.AppendLiteral("'");
					throw new IOException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				type = type.BaseType;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x00526654 File Offset: 0x00524854
		public static object Serialize(object value)
		{
			ArgumentNullException.ThrowIfNull(value, "value");
			bool flag = value is string || value is int || value is TagCompound || value is List<TagCompound>;
			if (flag)
			{
				return value;
			}
			Type type = value.GetType();
			TagSerializer serializer;
			if (TagSerializer.TryGetSerializer(type, out serializer))
			{
				return serializer.Serialize(value);
			}
			if (TagIO.GetPayloadId(type) != 9)
			{
				return value;
			}
			IList list = (IList)value;
			Type elemType = TagIO.GetListElementType(type);
			if (TagSerializer.TryGetSerializer(elemType, out serializer))
			{
				return serializer.SerializeList(list);
			}
			if (TagIO.GetPayloadId(elemType) != 9)
			{
				return list;
			}
			List<IList> serializedList = new List<IList>(list.Count);
			foreach (object elem in list)
			{
				serializedList.Add((IList)TagIO.Serialize(elem));
			}
			return serializedList;
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x00526750 File Offset: 0x00524950
		[NullableContext(2)]
		[return: Nullable(1)]
		public static T Deserialize<T>(object tag)
		{
			if (tag is T)
			{
				return (T)((object)tag);
			}
			return (T)((object)TagIO.Deserialize(typeof(T), tag));
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x00526784 File Offset: 0x00524984
		public static object Deserialize(Type type, [Nullable(2)] object tag)
		{
			ArgumentNullException.ThrowIfNull(type, "type");
			if (type.IsInstanceOfType(tag))
			{
				return tag;
			}
			TagSerializer serializer;
			if (TagSerializer.TryGetSerializer(type, out serializer))
			{
				if (tag == null)
				{
					tag = TagIO.Deserialize(serializer.TagType, null);
				}
				return serializer.Deserialize(tag);
			}
			if (tag == null && !type.IsArray)
			{
				if (type.GetGenericArguments().Length == 0)
				{
					return TagIO.GetHandler(TagIO.GetPayloadId(type)).Default();
				}
				if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					return Activator.CreateInstance(type);
				}
			}
			if (tag == null || tag is IList || type.IsArray)
			{
				if (type.IsArray)
				{
					Type elemType = type.GetElementType();
					if (tag == null)
					{
						return Array.CreateInstance(elemType, 0);
					}
					IList serializedList = (IList)tag;
					if (TagSerializer.TryGetSerializer(elemType, out serializer))
					{
						IList array = Array.CreateInstance(elemType, serializedList.Count);
						for (int i = 0; i < serializedList.Count; i++)
						{
							array[i] = serializer.Deserialize(serializedList[i]);
						}
						return array;
					}
					IList deserializedArray = Array.CreateInstance(elemType, serializedList.Count);
					for (int j = 0; j < serializedList.Count; j++)
					{
						deserializedArray[j] = TagIO.Deserialize(elemType, serializedList[j]);
					}
					return deserializedArray;
				}
				else if (type.GetGenericArguments().Length == 1)
				{
					Type elemType2 = type.GetGenericArguments()[0];
					Type newListType = typeof(List<>).MakeGenericType(new Type[]
					{
						elemType2
					});
					if (type.IsAssignableFrom(newListType))
					{
						if (tag == null)
						{
							return Activator.CreateInstance(newListType);
						}
						if (TagSerializer.TryGetSerializer(elemType2, out serializer))
						{
							return serializer.DeserializeList((IList)tag);
						}
						IList oldList = (IList)tag;
						IList newList = (IList)Activator.CreateInstance(newListType, new object[]
						{
							oldList.Count
						});
						foreach (object elem in oldList)
						{
							newList.Add(TagIO.Deserialize(elemType2, elem));
						}
						return newList;
					}
				}
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			if (tag == null)
			{
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Invalid NBT payload type '");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
				defaultInterpolatedStringHandler.AppendLiteral("'");
				throw new IOException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Unable to cast object of type '");
			defaultInterpolatedStringHandler.AppendFormatted<Type>(tag.GetType());
			defaultInterpolatedStringHandler.AppendLiteral("' to type '");
			defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
			defaultInterpolatedStringHandler.AppendLiteral("'");
			throw new InvalidCastException(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x00526A38 File Offset: 0x00524C38
		public static T Clone<T>(T o)
		{
			return (T)((object)TagIO.GetHandler(TagIO.GetPayloadId(o.GetType())).Clone(o));
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x00526A64 File Offset: 0x00524C64
		[NullableContext(2)]
		public static object ReadTag([Nullable(1)] BinaryReader r, out string name)
		{
			int id = (int)r.ReadByte();
			if (id == 0)
			{
				name = null;
				return null;
			}
			name = TagIO.StringHandler.reader(r);
			return TagIO.ReadTagImpl(id, r);
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x00526A99 File Offset: 0x00524C99
		[return: Nullable(2)]
		public static object ReadTagImpl(int id, BinaryReader r)
		{
			return TagIO.PayloadHandlers[id].Read(r);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x00526AA8 File Offset: 0x00524CA8
		public static void WriteTag(string name, object tag, BinaryWriter w)
		{
			int id = TagIO.GetPayloadId(tag.GetType());
			w.Write((byte)id);
			TagIO.StringHandler.writer(w, name);
			TagIO.PayloadHandlers[id].Write(w, tag);
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x00526AE8 File Offset: 0x00524CE8
		public static TagCompound FromFile(string path, bool compressed = true)
		{
			TagCompound result;
			try
			{
				using (Stream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
				{
					result = TagIO.FromStream(fs, compressed);
				}
			}
			catch (IOException e)
			{
				throw new IOException("Failed to read NBT file: " + path, e);
			}
			return result;
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x00526B44 File Offset: 0x00524D44
		public static TagCompound FromStream(Stream stream, bool compressed = true)
		{
			if (compressed)
			{
				stream = new GZipStream(stream, 1);
				MemoryStream ms = new MemoryStream(1048576);
				stream.CopyTo(ms);
				ms.Position = 0L;
				stream = ms;
			}
			return TagIO.Read(new BigEndianReader(stream));
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x00526B88 File Offset: 0x00524D88
		public static TagCompound Read(BinaryReader reader)
		{
			string name;
			TagCompound tagCompound = TagIO.ReadTag(reader, out name) as TagCompound;
			if (tagCompound == null)
			{
				throw new IOException("Root tag not a TagCompound");
			}
			return tagCompound;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x00526BB0 File Offset: 0x00524DB0
		public static void ToFile(TagCompound root, string path, bool compress = true)
		{
			try
			{
				using (Stream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
				{
					TagIO.ToStream(root, fs, compress);
				}
			}
			catch (IOException e)
			{
				throw new IOException("Failed to read NBT file: " + path, e);
			}
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x00526C0C File Offset: 0x00524E0C
		public static void ToStream(TagCompound root, Stream stream, bool compress = true)
		{
			if (compress)
			{
				stream = new GZipStream(stream, 0, true);
			}
			TagIO.Write(root, new BigEndianWriter(stream));
			if (compress)
			{
				stream.Close();
			}
		}

		/// <summary>
		/// Writes the TagCompound to the writer. Please don't use this to send TagCompound over the network if you can avoid it. If you have to, consider using <see cref="M:Terraria.ModLoader.IO.TagIO.ToStream(Terraria.ModLoader.IO.TagCompound,System.IO.Stream,System.Boolean)" />/<see cref="M:Terraria.ModLoader.IO.TagIO.FromStream(System.IO.Stream,System.Boolean)" /> with <c>compress: true</c>.
		/// </summary>
		// Token: 0x06002C40 RID: 11328 RVA: 0x00526C30 File Offset: 0x00524E30
		public static void Write(TagCompound root, BinaryWriter writer)
		{
			TagIO.WriteTag("", root, writer);
		}

		// Token: 0x04001BFD RID: 7165
		private static readonly TagIO.PayloadHandler[] PayloadHandlers = new TagIO.PayloadHandler[]
		{
			null,
			new TagIO.PayloadHandler<byte>((BinaryReader r) => r.ReadByte(), delegate(BinaryWriter w, byte v)
			{
				w.Write(v);
			}),
			new TagIO.PayloadHandler<short>((BinaryReader r) => r.ReadInt16(), delegate(BinaryWriter w, short v)
			{
				w.Write(v);
			}),
			new TagIO.PayloadHandler<int>((BinaryReader r) => r.ReadInt32(), delegate(BinaryWriter w, int v)
			{
				w.Write(v);
			}),
			new TagIO.PayloadHandler<long>((BinaryReader r) => r.ReadInt64(), delegate(BinaryWriter w, long v)
			{
				w.Write(v);
			}),
			new TagIO.PayloadHandler<float>((BinaryReader r) => r.ReadSingle(), delegate(BinaryWriter w, float v)
			{
				w.Write(v);
			}),
			new TagIO.PayloadHandler<double>((BinaryReader r) => r.ReadDouble(), delegate(BinaryWriter w, double v)
			{
				w.Write(v);
			}),
			new TagIO.ClassPayloadHandler<byte[]>((BinaryReader r) => r.ReadBytes(r.ReadInt32()), delegate(BinaryWriter w, byte[] v)
			{
				w.Write(v.Length);
				w.Write(v);
			}, (byte[] v) => (byte[])v.Clone(), () => Array.Empty<byte>()),
			new TagIO.ClassPayloadHandler<string>((BinaryReader r) => Encoding.UTF8.GetString(r.BaseStream.ReadByteSpan((int)r.ReadInt16())), delegate(BinaryWriter w, string v)
			{
				byte[] b = Encoding.UTF8.GetBytes(v);
				w.Write((short)b.Length);
				w.Write(b);
			}, (string v) => v, () => ""),
			new TagIO.ClassPayloadHandler<IList>((BinaryReader r) => TagIO.GetHandler((int)r.ReadByte()).ReadList(r, r.ReadInt32()), delegate(BinaryWriter w, IList v)
			{
				int id;
				try
				{
					id = TagIO.GetPayloadId(TagIO.GetListElementType(v.GetType()));
				}
				catch (IOException)
				{
					string str = "Invalid NBT list type: ";
					Type type = v.GetType();
					throw new IOException(str + ((type != null) ? type.ToString() : null));
				}
				w.Write((byte)id);
				w.Write(v.Count);
				TagIO.PayloadHandlers[id].WriteList(w, v);
			}, delegate(IList v)
			{
				IList result;
				try
				{
					result = TagIO.GetHandler(TagIO.GetPayloadId(TagIO.GetListElementType(v.GetType()))).CloneList(v);
				}
				catch (IOException)
				{
					string str = "Invalid NBT list type: ";
					Type type = v.GetType();
					throw new IOException(str + ((type != null) ? type.ToString() : null));
				}
				return result;
			}, null),
			new TagIO.ClassPayloadHandler<TagCompound>(delegate(BinaryReader r)
			{
				TagCompound compound = new TagCompound();
				string name;
				object tag;
				while ((tag = TagIO.ReadTag(r, out name)) != null)
				{
					compound.Set(name, tag, false);
				}
				return compound;
			}, delegate(BinaryWriter w, TagCompound v)
			{
				foreach (KeyValuePair<string, object> entry in v)
				{
					if (entry.Value != null)
					{
						TagIO.WriteTag(entry.Key, entry.Value, w);
					}
				}
				w.Write(0);
			}, (TagCompound v) => (TagCompound)v.Clone(), () => new TagCompound()),
			new TagIO.ClassPayloadHandler<int[]>(delegate(BinaryReader r)
			{
				int[] ia = new int[r.ReadInt32()];
				for (int i = 0; i < ia.Length; i++)
				{
					ia[i] = r.ReadInt32();
				}
				return ia;
			}, delegate(BinaryWriter w, int[] v)
			{
				w.Write(v.Length);
				foreach (int i in v)
				{
					w.Write(i);
				}
			}, (int[] v) => (int[])v.Clone(), () => Array.Empty<int>())
		};

		// Token: 0x04001BFE RID: 7166
		private static readonly Dictionary<Type, int> PayloadIDs = Enumerable.Range(1, TagIO.PayloadHandlers.Length - 1).ToDictionary((int i) => TagIO.PayloadHandlers[i].PayloadType);

		// Token: 0x04001BFF RID: 7167
		private static readonly TagIO.PayloadHandler<string> StringHandler = (TagIO.PayloadHandler<string>)TagIO.PayloadHandlers[8];

		// Token: 0x02000A3D RID: 2621
		[Nullable(0)]
		private abstract class PayloadHandler
		{
			// Token: 0x170008FC RID: 2300
			// (get) Token: 0x0600580A RID: 22538
			public abstract Type PayloadType { get; }

			// Token: 0x0600580B RID: 22539
			public abstract object Default();

			// Token: 0x0600580C RID: 22540
			public abstract object Read(BinaryReader r);

			// Token: 0x0600580D RID: 22541
			public abstract void Write(BinaryWriter w, object v);

			// Token: 0x0600580E RID: 22542
			public abstract IList ReadList(BinaryReader r, int size);

			// Token: 0x0600580F RID: 22543
			public abstract void WriteList(BinaryWriter w, IList list);

			// Token: 0x06005810 RID: 22544
			public abstract object Clone(object o);

			// Token: 0x06005811 RID: 22545
			public abstract IList CloneList(IList list);
		}

		// Token: 0x02000A3E RID: 2622
		[Nullable(0)]
		private class PayloadHandler<T> : TagIO.PayloadHandler
		{
			// Token: 0x06005813 RID: 22547 RVA: 0x0069F066 File Offset: 0x0069D266
			public PayloadHandler(Func<BinaryReader, T> reader, Action<BinaryWriter, T> writer)
			{
				this.reader = reader;
				this.writer = writer;
			}

			// Token: 0x170008FD RID: 2301
			// (get) Token: 0x06005814 RID: 22548 RVA: 0x0069F07C File Offset: 0x0069D27C
			public override Type PayloadType
			{
				get
				{
					return typeof(T);
				}
			}

			// Token: 0x06005815 RID: 22549 RVA: 0x0069F088 File Offset: 0x0069D288
			public override object Read(BinaryReader r)
			{
				return this.reader(r);
			}

			// Token: 0x06005816 RID: 22550 RVA: 0x0069F09B File Offset: 0x0069D29B
			public override void Write(BinaryWriter w, object v)
			{
				this.writer(w, (T)((object)v));
			}

			// Token: 0x06005817 RID: 22551 RVA: 0x0069F0B0 File Offset: 0x0069D2B0
			public override IList ReadList(BinaryReader r, int size)
			{
				List<T> list = new List<T>(size);
				for (int i = 0; i < size; i++)
				{
					list.Add(this.reader(r));
				}
				return list;
			}

			// Token: 0x06005818 RID: 22552 RVA: 0x0069F0E4 File Offset: 0x0069D2E4
			public override void WriteList(BinaryWriter w, IList list)
			{
				foreach (T t in ((IEnumerable<!0>)list))
				{
					this.writer(w, t);
				}
			}

			// Token: 0x06005819 RID: 22553 RVA: 0x0069F138 File Offset: 0x0069D338
			public override object Clone(object o)
			{
				return o;
			}

			// Token: 0x0600581A RID: 22554 RVA: 0x0069F13B File Offset: 0x0069D33B
			public override IList CloneList(IList list)
			{
				return this.CloneList((IList<T>)list);
			}

			// Token: 0x0600581B RID: 22555 RVA: 0x0069F149 File Offset: 0x0069D349
			public virtual IList CloneList(IList<T> list)
			{
				return new List<T>(list);
			}

			// Token: 0x0600581C RID: 22556 RVA: 0x0069F154 File Offset: 0x0069D354
			public override object Default()
			{
				return default(T);
			}

			// Token: 0x04006CB3 RID: 27827
			internal Func<BinaryReader, T> reader;

			// Token: 0x04006CB4 RID: 27828
			internal Action<BinaryWriter, T> writer;
		}

		// Token: 0x02000A3F RID: 2623
		[Nullable(new byte[]
		{
			0,
			1
		})]
		private class ClassPayloadHandler<T> : TagIO.PayloadHandler<T> where T : class
		{
			// Token: 0x0600581D RID: 22557 RVA: 0x0069F16F File Offset: 0x0069D36F
			public ClassPayloadHandler(Func<BinaryReader, T> reader, Action<BinaryWriter, T> writer, Func<T, T> clone, [Nullable(new byte[]
			{
				2,
				1
			})] Func<T> makeDefault = null) : base(reader, writer)
			{
				this.clone = clone;
				this.makeDefault = makeDefault;
			}

			// Token: 0x0600581E RID: 22558 RVA: 0x0069F188 File Offset: 0x0069D388
			public override object Clone(object o)
			{
				return this.clone((T)((object)o));
			}

			// Token: 0x0600581F RID: 22559 RVA: 0x0069F1A0 File Offset: 0x0069D3A0
			public override IList CloneList(IList<T> list)
			{
				return list.Select(this.clone).ToList<T>();
			}

			// Token: 0x06005820 RID: 22560 RVA: 0x0069F1B3 File Offset: 0x0069D3B3
			public override object Default()
			{
				return this.makeDefault();
			}

			// Token: 0x04006CB5 RID: 27829
			private Func<T, T> clone;

			// Token: 0x04006CB6 RID: 27830
			[Nullable(new byte[]
			{
				2,
				1
			})]
			private Func<T> makeDefault;
		}
	}
}
