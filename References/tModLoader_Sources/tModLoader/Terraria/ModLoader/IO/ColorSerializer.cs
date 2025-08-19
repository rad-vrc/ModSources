using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000294 RID: 660
	public class ColorSerializer : TagSerializer<Color, int>
	{
		// Token: 0x06002C78 RID: 11384 RVA: 0x0052794C File Offset: 0x00525B4C
		public override int Serialize(Color value)
		{
			return (int)value.PackedValue;
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x00527955 File Offset: 0x00525B55
		public override Color Deserialize(int tag)
		{
			return new Color(tag & 255, tag >> 8 & 255, tag >> 16 & 255, tag >> 24 & 255);
		}
	}
}
