using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000285 RID: 645
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1
	})]
	[Autoload(false)]
	public class MultiDimArraySerializer : TagSerializer<Array, TagCompound>
	{
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x00524A91 File Offset: 0x00522C91
		public Type ArrayType { get; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06002BE5 RID: 11237 RVA: 0x00524A99 File Offset: 0x00522C99
		public Type ElementType { get; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x00524AA1 File Offset: 0x00522CA1
		public int ArrayRank { get; }

		// Token: 0x06002BE7 RID: 11239 RVA: 0x00524AAC File Offset: 0x00522CAC
		public MultiDimArraySerializer(Type arrayType)
		{
			ArgumentNullException.ThrowIfNull(arrayType, "arrayType");
			if (!arrayType.IsArray)
			{
				throw new ArgumentException("Must be an array type", "arrayType");
			}
			this.ArrayType = arrayType;
			this.ElementType = arrayType.GetElementType();
			this.ArrayRank = arrayType.GetArrayRank();
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x00524B04 File Offset: 0x00522D04
		public override TagCompound Serialize(Array array)
		{
			ArgumentNullException.ThrowIfNull(array, "array");
			if (array.Length == 0)
			{
				return MultiDimArraySerializer.ToTagCompound(array, null, null);
			}
			Type serializedType = TagIO.Serialize(array.GetValue(new int[array.Rank])).GetType();
			Type elementType = serializedType;
			MultiDimArraySerializer.Converter converter;
			if ((converter = MultiDimArraySerializer.<>O.<0>__Serialize) == null)
			{
				converter = (MultiDimArraySerializer.<>O.<0>__Serialize = new MultiDimArraySerializer.Converter(TagIO.Serialize));
			}
			return MultiDimArraySerializer.ToTagCompound(array, elementType, converter);
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x00524B6B File Offset: 0x00522D6B
		public override Array Deserialize(TagCompound tag)
		{
			ArgumentNullException.ThrowIfNull(tag, "tag");
			return MultiDimArraySerializer.FromTagCompound(tag, this.ArrayType, (object e) => TagIO.Deserialize(this.ElementType, e));
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x00524B90 File Offset: 0x00522D90
		public override IList SerializeList(IList list)
		{
			ArgumentNullException.ThrowIfNull(list, "list");
			List<TagCompound> serializedList = new List<TagCompound>(list.Count);
			foreach (object obj in list)
			{
				Array array = (Array)obj;
				serializedList.Add(this.Serialize(array));
			}
			return serializedList;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x00524C04 File Offset: 0x00522E04
		public override IList DeserializeList(IList list)
		{
			ArgumentNullException.ThrowIfNull(list, "list");
			IList<TagCompound> listT = (IList<TagCompound>)list;
			IList deserializedList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
			{
				this.ArrayType
			}), new object[]
			{
				listT.Count
			});
			foreach (TagCompound tagCompound in listT)
			{
				deserializedList.Add(this.Deserialize(tagCompound));
			}
			return deserializedList;
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x00524CA4 File Offset: 0x00522EA4
		public static TagCompound ToTagCompound(Array array, [Nullable(2)] Type elementType = null, [Nullable(2)] MultiDimArraySerializer.Converter converter = null)
		{
			ArgumentNullException.ThrowIfNull(array, "array");
			int[] ranks = new int[array.Rank];
			for (int i = 0; i < ranks.Length; i++)
			{
				ranks[i] = array.GetLength(i);
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound["ranks"] = ranks;
			tagCompound["list"] = MultiDimArraySerializer.ToList(array, elementType, converter);
			return tagCompound;
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x00524D04 File Offset: 0x00522F04
		public static IList ToList(Array array, [Nullable(2)] Type elementType = null, [Nullable(2)] MultiDimArraySerializer.Converter converter = null)
		{
			ArgumentNullException.ThrowIfNull(array, "array");
			Type arrayType = array.GetType();
			IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
			{
				elementType ?? arrayType.GetElementType()
			}), new object[]
			{
				array.Length
			});
			foreach (object o in array)
			{
				list.Add((converter != null) ? converter(o) : o);
			}
			return list;
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x00524DB8 File Offset: 0x00522FB8
		public static Array FromTagCompound(TagCompound tag, Type arrayType, [Nullable(2)] MultiDimArraySerializer.Converter converter = null)
		{
			ArgumentNullException.ThrowIfNull(tag, "tag");
			ArgumentNullException.ThrowIfNull(arrayType, "arrayType");
			if (!arrayType.IsArray)
			{
				throw new ArgumentException("Must be an array type", "arrayType");
			}
			Type elementType = arrayType.GetElementType();
			int[] ranks;
			if (!tag.TryGet<int[]>("ranks", out ranks))
			{
				return Array.CreateInstance(elementType, new int[arrayType.GetArrayRank()]);
			}
			return MultiDimArraySerializer.FromList(tag.Get<List<object>>("list"), ranks, elementType, converter);
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x00524E30 File Offset: 0x00523030
		public static Array FromList(IList list, int[] arrayRanks, [Nullable(2)] Type elementType = null, [Nullable(2)] MultiDimArraySerializer.Converter converter = null)
		{
			ArgumentNullException.ThrowIfNull(list, "list");
			ArgumentNullException.ThrowIfNull(arrayRanks, "arrayRanks");
			if (arrayRanks.Length == 0)
			{
				throw new ArgumentException("Array rank must be greater than 0");
			}
			if (list.Count != arrayRanks.Aggregate(1, (int current, int length) => current * length))
			{
				throw new ArgumentException("List length does not match array length");
			}
			Type type = list.GetType();
			if (elementType == null)
			{
				elementType = type.GetElementType();
			}
			if (elementType == null)
			{
				Type[] genericArguments = type.GetGenericArguments();
				if (genericArguments == null || genericArguments.Length != 1)
				{
					throw new ArgumentException("IList type must have exactly one generic argument");
				}
				elementType = genericArguments[0];
			}
			Array array = Array.CreateInstance(elementType, arrayRanks);
			int[] indices = new int[arrayRanks.Length];
			foreach (object value in list)
			{
				int r = indices.Length - 1;
				while (r >= 0 && indices[r] >= arrayRanks[r])
				{
					if (r == 0)
					{
						return array;
					}
					indices[r] = 0;
					indices[r - 1]++;
					r--;
				}
				if (converter != null)
				{
					value = converter(value);
				}
				array.SetValue(value, indices);
				int[] array2 = indices;
				array2[array2.Length - 1]++;
			}
			return array;
		}

		// Token: 0x04001BF7 RID: 7159
		private const string Ranks = "ranks";

		// Token: 0x04001BF8 RID: 7160
		private const string List = "list";

		// Token: 0x02000A37 RID: 2615
		// (Invoke) Token: 0x060057FC RID: 22524
		[NullableContext(0)]
		public delegate object Converter(object element);

		// Token: 0x02000A38 RID: 2616
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006CAB RID: 27819
			[Nullable(0)]
			public static MultiDimArraySerializer.Converter <0>__Serialize;
		}
	}
}
