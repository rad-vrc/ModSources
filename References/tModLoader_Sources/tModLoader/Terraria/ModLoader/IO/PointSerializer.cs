using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000296 RID: 662
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public class PointSerializer : TagSerializer<Point, TagCompound>
	{
		// Token: 0x06002C7E RID: 11390 RVA: 0x005279E0 File Offset: 0x00525BE0
		public override TagCompound Serialize(Point value)
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound["x"] = value.X;
			tagCompound["y"] = value.Y;
			return tagCompound;
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x00527A13 File Offset: 0x00525C13
		public override Point Deserialize(TagCompound tag)
		{
			return new Point(tag.GetInt("x"), tag.GetInt("y"));
		}
	}
}
