using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Enums;
using Terraria.GameContent;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class represents a type of modded palm tree.
	/// The palm tree will share a tile ID with the vanilla palm trees (323), so that the trees can freely convert between each other if the sand below is converted.
	/// This class encapsulates several functions that distinguish each type of palm tree from each other.
	/// </summary>
	// Token: 0x020001C0 RID: 448
	public abstract class ModPalmTree : ITree, IPlant, ILoadable
	{
		/// <summary>
		/// The tree will share a tile ID with the vanilla palm trees (323), so that the trees can freely convert between each other if the sand below is converted.
		/// </summary>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x004E806D File Offset: 0x004E626D
		public int PlantTileId
		{
			get
			{
				return 323;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x004E8074 File Offset: 0x004E6274
		public int VanillaCount
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060022AF RID: 8879
		public abstract TreePaintingSettings TreeShaderSettings { get; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x004E8077 File Offset: 0x004E6277
		// (set) Token: 0x060022B1 RID: 8881 RVA: 0x004E807F File Offset: 0x004E627F
		public int[] GrowsOnTileId { get; set; }

		// Token: 0x060022B2 RID: 8882
		public abstract void SetStaticDefaults();

		// Token: 0x060022B3 RID: 8883
		public abstract Asset<Texture2D> GetTexture();

		/// <summary>
		/// <br>Used mostly for vanilla tree shake loot tables</br>
		/// <br>Defaults to <see cref="F:Terraria.Enums.TreeTypes.Custom" />. Set to <see cref="F:Terraria.Enums.TreeTypes.None" /> to prevent the tree from being able to be shaken.</br>
		/// </summary>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x004E8088 File Offset: 0x004E6288
		public virtual TreeTypes CountsAsTreeType
		{
			get
			{
				return TreeTypes.Custom;
			}
		}

		/// <summary>
		/// Return the type of dust created when this palm tree is destroyed. Returns 215 by default.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060022B5 RID: 8885 RVA: 0x004E808C File Offset: 0x004E628C
		public virtual int CreateDust()
		{
			return 215;
		}

		/// <summary>
		/// Return the type of gore created when the tree grow, being shook and falling leaves on windy days, returns -1 by default
		/// </summary>
		/// <returns></returns>
		// Token: 0x060022B6 RID: 8886 RVA: 0x004E8093 File Offset: 0x004E6293
		public virtual int TreeLeaf()
		{
			return -1;
		}

		/// <summary>
		/// Executed on tree shake, return false to skip vanilla tree shake drops
		/// </summary>
		/// <returns></returns>
		// Token: 0x060022B7 RID: 8887 RVA: 0x004E8096 File Offset: 0x004E6296
		public virtual bool Shake(int x, int y, ref bool createLeaves)
		{
			return true;
		}

		/// <summary>
		/// Defines the sapling that can eventually grow into a tree. The type of the sapling should be returned here. Returns 20 and style 0 by default.
		/// The style parameter will determine which sapling is chosen if multiple sapling types share the same ID;
		/// even if you only have a single sapling in an ID, you must still set this to 0.
		/// </summary>
		/// <param name="style"></param>
		/// <returns></returns>
		// Token: 0x060022B8 RID: 8888 RVA: 0x004E8099 File Offset: 0x004E6299
		public virtual int SaplingGrowthType(ref int style)
		{
			style = 1;
			return 20;
		}

		/// <summary>
		/// The ID of the item that is dropped in bulk when this palm tree is destroyed.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060022B9 RID: 8889
		public abstract int DropWood();

		/// <summary>
		/// Return the texture containing the possible tree tops that can be drawn above this palm tree.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060022BA RID: 8890
		public abstract Asset<Texture2D> GetTopTextures();

		/// <summary>
		/// Return the texture containing the possible tree tops that can be drawn above this palm tree.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060022BB RID: 8891
		public abstract Asset<Texture2D> GetOasisTopTextures();

		// Token: 0x04001714 RID: 5908
		public const int VanillaStyleCount = 8;
	}
}
