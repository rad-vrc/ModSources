using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000293 RID: 659
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public class Vector3TagSerializer : TagSerializer<Vector3, TagCompound>
	{
		// Token: 0x06002C75 RID: 11381 RVA: 0x005278C8 File Offset: 0x00525AC8
		public override TagCompound Serialize(Vector3 value)
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound["x"] = value.X;
			tagCompound["y"] = value.Y;
			tagCompound["z"] = value.Z;
			return tagCompound;
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x0052791C File Offset: 0x00525B1C
		public override Vector3 Deserialize(TagCompound tag)
		{
			return new Vector3(tag.GetFloat("x"), tag.GetFloat("y"), tag.GetFloat("z"));
		}
	}
}
