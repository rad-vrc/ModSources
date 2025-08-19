using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200028B RID: 651
	internal class TagSerializableSerializer<T> : TagSerializer<T, TagCompound> where T : TagSerializable
	{
		// Token: 0x06002C4B RID: 11339 RVA: 0x0052742C File Offset: 0x0052562C
		public TagSerializableSerializer()
		{
			Type type = typeof(T);
			FieldInfo field = type.GetField("DESERIALIZER");
			if (field != null)
			{
				if (field.FieldType != typeof(Func<TagCompound, T>))
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 3);
					defaultInterpolatedStringHandler.AppendLiteral("Invalid deserializer field type ");
					defaultInterpolatedStringHandler.AppendFormatted<Type>(field.FieldType);
					defaultInterpolatedStringHandler.AppendLiteral(" in ");
					defaultInterpolatedStringHandler.AppendFormatted(type.FullName);
					defaultInterpolatedStringHandler.AppendLiteral(" expected ");
					defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(Func<TagCompound, T>));
					defaultInterpolatedStringHandler.AppendLiteral(".");
					throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				this.deserializer = (Func<TagCompound, T>)field.GetValue(null);
			}
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x005274FD File Offset: 0x005256FD
		public override TagCompound Serialize(T value)
		{
			TagCompound tagCompound = value.SerializeData();
			tagCompound["<type>"] = value.GetType().FullName;
			return tagCompound;
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x0052752C File Offset: 0x0052572C
		public override T Deserialize(TagCompound tag)
		{
			string typeName;
			TagSerializer subtypeSerializer;
			if (tag.TryGet<string>("<type>", out typeName) && typeName != this.Type.FullName && this.TryGetSubtypeSerializer(typeName, out subtypeSerializer))
			{
				return (T)((object)subtypeSerializer.Deserialize(tag));
			}
			if (this.deserializer == null)
			{
				string msg = "Missing deserializer for type '" + this.Type.FullName + "'";
				if (typeName != null && typeName != this.Type.FullName)
				{
					msg = msg + ", subtype '" + typeName + "'";
				}
				throw new ArgumentException(msg);
			}
			return this.deserializer(tag);
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x005275D4 File Offset: 0x005257D4
		private bool TryGetSubtypeSerializer(string typeName, out TagSerializer subtypeSerializer)
		{
			if (this.subtypeSerializers == null)
			{
				this.subtypeSerializers = new Dictionary<string, TagSerializer>();
			}
			if (!this.subtypeSerializers.TryGetValue(typeName, out subtypeSerializer))
			{
				Type subtype = AssemblyManager.FindSubtype(typeof(T), typeName);
				if (subtype != null)
				{
					TagSerializer.TryGetSerializer(subtype, out subtypeSerializer);
				}
				this.subtypeSerializers[typeName] = subtypeSerializer;
			}
			return subtypeSerializer != null;
		}

		// Token: 0x04001C02 RID: 7170
		private Func<TagCompound, T> deserializer;

		// Token: 0x04001C03 RID: 7171
		private Dictionary<string, TagSerializer> subtypeSerializers;
	}
}
