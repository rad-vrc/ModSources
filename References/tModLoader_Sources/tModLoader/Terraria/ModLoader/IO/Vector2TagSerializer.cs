using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000292 RID: 658
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public class Vector2TagSerializer : TagSerializer<Vector2, TagCompound>
	{
		// Token: 0x06002C72 RID: 11378 RVA: 0x0052786F File Offset: 0x00525A6F
		public override TagCompound Serialize(Vector2 value)
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound["x"] = value.X;
			tagCompound["y"] = value.Y;
			return tagCompound;
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x005278A2 File Offset: 0x00525AA2
		public override Vector2 Deserialize(TagCompound tag)
		{
			return new Vector2(tag.GetFloat("x"), tag.GetFloat("y"));
		}
	}
}
