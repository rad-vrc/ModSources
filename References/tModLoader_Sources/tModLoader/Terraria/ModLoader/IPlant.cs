using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.ModLoader
{
	// Token: 0x020001BB RID: 443
	public interface IPlant : ILoadable
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600227C RID: 8828
		int PlantTileId { get; }

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600227D RID: 8829
		int VanillaCount { get; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600227E RID: 8830
		// (set) Token: 0x0600227F RID: 8831
		int[] GrowsOnTileId { get; set; }

		// Token: 0x06002280 RID: 8832
		Asset<Texture2D> GetTexture();

		// Token: 0x06002281 RID: 8833
		void SetStaticDefaults();

		// Token: 0x06002282 RID: 8834 RVA: 0x004E7D62 File Offset: 0x004E5F62
		void Load(Mod mod)
		{
			PlantLoader.plantList.Add(this);
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x004E7D6F File Offset: 0x004E5F6F
		void Unload()
		{
		}
	}
}
