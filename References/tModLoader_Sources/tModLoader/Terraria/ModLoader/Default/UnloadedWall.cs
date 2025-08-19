using System;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002DD RID: 733
	public class UnloadedWall : ModWall
	{
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x00531021 File Offset: 0x0052F221
		public override string Texture
		{
			get
			{
				return "ModLoader/UnloadedWall";
			}
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x00531028 File Offset: 0x0052F228
		public override void SetStaticDefaults()
		{
			TileIO.Walls.unloadedTypes.Add(base.Type);
		}
	}
}
