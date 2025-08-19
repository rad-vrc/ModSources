using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200028D RID: 653
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class TagSerializer<T, S> : TagSerializer
	{
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x005277AA File Offset: 0x005259AA
		public override Type Type
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x005277B6 File Offset: 0x005259B6
		public override Type TagType
		{
			get
			{
				return typeof(S);
			}
		}

		// Token: 0x06002C5F RID: 11359
		public abstract S Serialize(T value);

		// Token: 0x06002C60 RID: 11360
		public abstract T Deserialize(S tag);

		// Token: 0x06002C61 RID: 11361 RVA: 0x005277C2 File Offset: 0x005259C2
		public override object Serialize(object value)
		{
			return this.Serialize((T)((object)value));
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x005277D5 File Offset: 0x005259D5
		public override object Deserialize(object tag)
		{
			return this.Deserialize((S)((object)tag));
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x005277E8 File Offset: 0x005259E8
		public override IList SerializeList(IList value)
		{
			return ((IList<T>)value).Select(new Func<T, S>(this.Serialize)).ToList<S>();
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x00527807 File Offset: 0x00525A07
		public override IList DeserializeList(IList value)
		{
			return ((IList<S>)value).Select(new Func<S, T>(this.Deserialize)).ToList<T>();
		}
	}
}
