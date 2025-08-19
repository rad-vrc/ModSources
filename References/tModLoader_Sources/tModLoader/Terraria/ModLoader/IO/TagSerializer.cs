using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200028C RID: 652
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class TagSerializer : ModType
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06002C4F RID: 11343
		public abstract Type Type { get; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06002C50 RID: 11344
		public abstract Type TagType { get; }

		// Token: 0x06002C51 RID: 11345
		public abstract object Serialize(object value);

		// Token: 0x06002C52 RID: 11346
		public abstract object Deserialize(object tag);

		// Token: 0x06002C53 RID: 11347
		public abstract IList SerializeList(IList value);

		// Token: 0x06002C54 RID: 11348
		public abstract IList DeserializeList(IList value);

		// Token: 0x06002C55 RID: 11349 RVA: 0x00527638 File Offset: 0x00525838
		static TagSerializer()
		{
			TagSerializer.Reload();
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x00527653 File Offset: 0x00525853
		internal static void Reload()
		{
			TagSerializer.serializers.Clear();
			TagSerializer.typeNameCache.Clear();
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x0052766C File Offset: 0x0052586C
		public static bool TryGetSerializer(Type type, [Nullable(2)] [NotNullWhen(true)] out TagSerializer serializer)
		{
			if (TagSerializer.serializers.TryGetValue(type, out serializer))
			{
				return true;
			}
			if (type.IsArray && type.GetArrayRank() > 1)
			{
				IDictionary<Type, TagSerializer> dictionary = TagSerializer.serializers;
				TagSerializer value;
				serializer = (value = new MultiDimArraySerializer(type));
				dictionary[type] = value;
				return true;
			}
			if (typeof(TagSerializable).IsAssignableFrom(type))
			{
				Type sType = typeof(TagSerializableSerializer<>).MakeGenericType(new Type[]
				{
					type
				});
				IDictionary<Type, TagSerializer> dictionary2 = TagSerializer.serializers;
				TagSerializer value;
				serializer = (value = (TagSerializer)Activator.CreateInstance(sType));
				dictionary2[type] = value;
				return true;
			}
			return false;
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x005276FC File Offset: 0x005258FC
		internal static void AddSerializer(TagSerializer serializer)
		{
			TagSerializer.serializers.Add(serializer.Type, serializer);
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x00527710 File Offset: 0x00525910
		[Obsolete("Cannot find types in mod dllReferences, and dictionary is unnecessarily large. Use AssemblyManager.FindSubtype/FindTypes instead")]
		[return: Nullable(2)]
		public static Type GetType(string name)
		{
			Type type;
			if (TagSerializer.typeNameCache.TryGetValue(name, out type))
			{
				return type;
			}
			type = Type.GetType(name);
			if (type != null)
			{
				return TagSerializer.typeNameCache[name] = type;
			}
			Mod[] mods = ModLoader.Mods;
			for (int i = 0; i < mods.Length; i++)
			{
				Assembly code = mods[i].Code;
				type = ((code != null) ? code.GetType(name) : null);
				if (type != null)
				{
					return TagSerializer.typeNameCache[name] = type;
				}
			}
			return null;
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x00527792 File Offset: 0x00525992
		protected sealed override void Register()
		{
			TagSerializer.AddSerializer(this);
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x0052779A File Offset: 0x0052599A
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		// Token: 0x04001C04 RID: 7172
		private static IDictionary<Type, TagSerializer> serializers = new Dictionary<Type, TagSerializer>();

		// Token: 0x04001C05 RID: 7173
		private static IDictionary<string, Type> typeNameCache = new Dictionary<string, Type>();
	}
}
