using System;
using System.Runtime.CompilerServices;
using Terraria.DataStructures;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000295 RID: 661
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public class Point16Serializer : TagSerializer<Point16, TagCompound>
	{
		// Token: 0x06002C7B RID: 11387 RVA: 0x00527988 File Offset: 0x00525B88
		public override TagCompound Serialize(Point16 value)
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound["x"] = value.X;
			tagCompound["y"] = value.Y;
			return tagCompound;
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x005279BB File Offset: 0x00525BBB
		public override Point16 Deserialize(TagCompound tag)
		{
			return new Point16(tag.GetShort("x"), tag.GetShort("y"));
		}
	}
}
