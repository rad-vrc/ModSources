using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ObjectData;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Extension to <seealso cref="T:Terraria.ModLoader.ModTile" /> that streamlines the process of creating an enemy banner tile. Behaves the same as <see cref="F:Terraria.ID.TileID.Banners" /> except it does not set StyleWrapLimit to 111.
	/// <para /> Handles applying banner buffs for <see cref="T:Terraria.ModLoader.ModNPC" /> automatically provided <see cref="P:Terraria.ModLoader.ModNPC.Banner" /> and <see cref="P:Terraria.ModLoader.ModNPC.BannerItem" /> are properly assigned and a <see cref="T:Terraria.ModLoader.ModItem" /> to place each banner placement style exists.
	/// </summary>
	// Token: 0x0200019E RID: 414
	public abstract class ModBannerTile : ModTile
	{
		// Token: 0x06001FE4 RID: 8164 RVA: 0x004E2704 File Offset: 0x004E0904
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileID.Sets.DisableSmartCursor[(int)base.Type] = true;
			TileID.Sets.MultiTileSway[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.PlanterBox, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -10;
			TileObjectData.addAlternate(0);
			TileObjectData.addTile((int)base.Type);
			base.DustType = -1;
			base.AddMapEntry(new Color(13, 88, 130), Language.GetText("MapObject.Banner"));
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x004E282D File Offset: 0x004E0A2D
		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			if (TileObjectData.IsTopLeft(Main.tile[i, j]))
			{
				Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
			}
			return false;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x004E2855 File Offset: 0x004E0A55
		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
			offsetY += 2;
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x004E2860 File Offset: 0x004E0A60
		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (closer)
			{
				return;
			}
			int tileStyle = TileObjectData.GetTileStyle(Main.tile[i, j]);
			int itemType = TileLoader.GetItemDropFromTypeAndStyle((int)base.Type, tileStyle);
			int bannerID = NPCLoader.BannerItemToNPC(itemType);
			if (bannerID == -1)
			{
				return;
			}
			if (ItemID.Sets.BannerStrength.IndexInRange(itemType) && ItemID.Sets.BannerStrength[itemType].Enabled)
			{
				Main.SceneMetrics.NPCBannerBuff[bannerID] = true;
				Main.SceneMetrics.hasBanner = true;
			}
		}
	}
}
