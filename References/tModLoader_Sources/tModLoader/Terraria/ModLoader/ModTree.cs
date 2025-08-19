using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Enums;
using Terraria.GameContent;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class represents a type of modded tree.
	/// The tree will share a tile ID with the vanilla trees (5), so that the trees can freely convert between each other if the soil below is converted.
	/// This class encapsulates several functions that distinguish each type of tree from each other.
	/// </summary>
	// Token: 0x020001BF RID: 447
	public abstract class ModTree : ITree, IPlant, ILoadable
	{
		/// <summary>
		/// The tree will share a tile ID with the vanilla trees (5), so that the trees can freely convert between each other if the soil below is converted.
		/// </summary>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x004E8037 File Offset: 0x004E6237
		public int PlantTileId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x004E803A File Offset: 0x004E623A
		public int VanillaCount
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600229D RID: 8861
		public abstract TreePaintingSettings TreeShaderSettings { get; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x004E803D File Offset: 0x004E623D
		// (set) Token: 0x0600229F RID: 8863 RVA: 0x004E8045 File Offset: 0x004E6245
		public int[] GrowsOnTileId { get; set; }

		// Token: 0x060022A0 RID: 8864
		public abstract void SetStaticDefaults();

		// Token: 0x060022A1 RID: 8865
		public abstract Asset<Texture2D> GetTexture();

		/// <summary>
		/// <br>Used mostly for vanilla tree shake loot tables</br>
		/// <br>Defaults to <see cref="F:Terraria.Enums.TreeTypes.Custom" />. Set to <see cref="F:Terraria.Enums.TreeTypes.None" /> to prevent the tree from being able to be shaken.</br>
		/// </summary>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x004E804E File Offset: 0x004E624E
		public virtual TreeTypes CountsAsTreeType
		{
			get
			{
				return TreeTypes.Custom;
			}
		}

		/// <summary>
		/// Return the type of dust created when this tree is destroyed. Returns 7 by default.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060022A3 RID: 8867 RVA: 0x004E8052 File Offset: 0x004E6252
		public virtual int CreateDust()
		{
			return 7;
		}

		/// <summary>
		/// Return the type of gore created when the tree grow, being shook and falling leaves on windy days, returns -1 by default
		/// </summary>
		/// <returns></returns>
		// Token: 0x060022A4 RID: 8868 RVA: 0x004E8055 File Offset: 0x004E6255
		public virtual int TreeLeaf()
		{
			return -1;
		}

		/// <summary>
		/// Executed on tree shake, return false to skip vanilla tree shake drops.<br />
		/// The x and y coordinates correspond to the top of the tree, where items usually spawn.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060022A5 RID: 8869 RVA: 0x004E8058 File Offset: 0x004E6258
		public virtual bool Shake(int x, int y, ref bool createLeaves)
		{
			return true;
		}

		/// <summary>
		/// Whether or not this tree can drop acorns. Returns true by default.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060022A6 RID: 8870 RVA: 0x004E805B File Offset: 0x004E625B
		public virtual bool CanDropAcorn()
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
		// Token: 0x060022A7 RID: 8871 RVA: 0x004E805E File Offset: 0x004E625E
		public virtual int SaplingGrowthType(ref int style)
		{
			style = 0;
			return 20;
		}

		/// <summary>
		/// The ID of the item that is dropped in bulk when this tree is destroyed.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060022A8 RID: 8872
		public abstract int DropWood();

		// Token: 0x060022A9 RID: 8873
		public abstract void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight);

		/// <summary>
		/// Return the texture containing the possible tree tops that can be drawn above this tree.
		/// The framing was determined under <cref>SetTreeFoliageSettings</cref>
		/// </summary>
		// Token: 0x060022AA RID: 8874
		public abstract Asset<Texture2D> GetTopTextures();

		/// <summary>
		/// Return the texture containing the possible tree branches that can be drawn next to this tree.
		/// The framing was determined under <cref>SetTreeFoliageSettings</cref>
		/// </summary>
		// Token: 0x060022AB RID: 8875
		public abstract Asset<Texture2D> GetBranchTextures();

		// Token: 0x04001711 RID: 5905
		public const int VanillaStyleCount = 7;

		// Token: 0x04001712 RID: 5906
		public const int VanillaTopTextureCount = 100;
	}
}
