using System;
using Terraria.ModLoader.Default;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200029C RID: 668
	internal class WallEntry : ModBlockEntry
	{
		// Token: 0x06002CAC RID: 11436 RVA: 0x00528F1C File Offset: 0x0052711C
		public WallEntry(ModWall wall) : base(wall)
		{
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x00528F25 File Offset: 0x00527125
		public WallEntry(TagCompound tag) : base(tag)
		{
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x00528F2E File Offset: 0x0052712E
		public override ModBlockType DefaultUnloadedPlaceholder
		{
			get
			{
				return ModContent.GetInstance<UnloadedWall>();
			}
		}

		// Token: 0x04001C0E RID: 7182
		public static Func<TagCompound, WallEntry> DESERIALIZER = (TagCompound tag) => new WallEntry(tag);
	}
}
