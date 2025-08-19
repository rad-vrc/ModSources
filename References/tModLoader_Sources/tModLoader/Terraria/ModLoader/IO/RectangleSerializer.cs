using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000297 RID: 663
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public class RectangleSerializer : TagSerializer<Rectangle, TagCompound>
	{
		// Token: 0x06002C81 RID: 11393 RVA: 0x00527A38 File Offset: 0x00525C38
		public override TagCompound Serialize(Rectangle value)
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound["x"] = value.X;
			tagCompound["y"] = value.Y;
			tagCompound["width"] = value.Width;
			tagCompound["height"] = value.Height;
			return tagCompound;
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x00527AA2 File Offset: 0x00525CA2
		public override Rectangle Deserialize(TagCompound tag)
		{
			return new Rectangle(tag.GetInt("x"), tag.GetInt("y"), tag.GetInt("width"), tag.GetInt("height"));
		}
	}
}
