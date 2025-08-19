using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class represents a type of modded cactus.
	/// This class encapsulates a function for retrieving the cactus's texture and an array for type of soil it grows on.
	/// </summary>
	// Token: 0x020001BE RID: 446
	public abstract class ModCactus : IPlant, ILoadable
	{
		/// <summary>
		/// The cactus will share a tile ID with the vanilla cacti (80), so that the cacti can freely convert between each other if the sand below is converted.
		/// </summary>
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x004E8017 File Offset: 0x004E6217
		public int PlantTileId
		{
			get
			{
				return 80;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x004E801B File Offset: 0x004E621B
		public int VanillaCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x004E801E File Offset: 0x004E621E
		// (set) Token: 0x06002296 RID: 8854 RVA: 0x004E8026 File Offset: 0x004E6226
		public int[] GrowsOnTileId { get; set; }

		// Token: 0x06002297 RID: 8855
		public abstract void SetStaticDefaults();

		// Token: 0x06002298 RID: 8856
		public abstract Asset<Texture2D> GetTexture();

		/// <summary> The fruit texture has a special layout that needs to be followed, see ExampleCactus_Fruit.png in ExampleMod for a template. </summary>
		// Token: 0x06002299 RID: 8857
		public abstract Asset<Texture2D> GetFruitTexture();
	}
}
