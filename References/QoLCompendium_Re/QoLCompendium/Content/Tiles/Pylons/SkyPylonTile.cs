using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Content.Items.Placeables.Pylons;
using QoLCompendium.Core;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ObjectData;

namespace QoLCompendium.Content.Tiles.Pylons
{
	// Token: 0x0200000D RID: 13
	public class SkyPylonTile : ModPylon
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002ED7 File Offset: 0x000010D7
		public override void Load()
		{
			this.crystalTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/PylonCrystals/SkyPylonTile_Crystal", 2);
			this.crystalHighlightTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Highlights/PylonCrystal_Highlight", 2);
			this.mapIcon = ModContent.Request<Texture2D>("QoLCompendium/Assets/MapIcons/SkyPylonTile_MapIcon", 2);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F0C File Offset: 0x0000110C
		public override void SetStaticDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TEModdedPylon instance = (TEModdedPylon)ModContent.GetInstance<SkyPylonTileEntity>();
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.PlacementPreviewHook_CheckIfCanPlace), 1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.Hook_AfterPlacement), -1, 0, false);
			TileObjectData.addTile((int)base.Type);
			TileID.Sets.InteractibleByNPCs[(int)base.Type] = true;
			TileID.Sets.PreventsSandfall[(int)base.Type] = true;
			TileID.Sets.AvoidedByMeteorLanding[(int)base.Type] = true;
			base.AddToArray(ref TileID.Sets.CountsAsPylon);
			LocalizedText val = base.CreateMapEntryName();
			base.AddMapEntry(Color.Azure, val);
			base.DustType = -1;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003002 File Offset: 0x00001202
		public override NPCShop.Entry GetNPCShopEntry()
		{
			if (QoLCompendium.itemConfig.Pylons)
			{
				return new NPCShop.Entry(ModContent.ItemType<SkyPylon>(), new Condition[]
				{
					Condition.InSkyHeight
				});
			}
			return null;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000302A File Offset: 0x0000122A
		public override void MouseOver(int i, int j)
		{
			Main.LocalPlayer.cursorItemIconEnabled = true;
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<SkyPylon>();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003046 File Offset: 0x00001246
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			ModContent.GetInstance<SkyPylonTileEntity>().Kill(i, j);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002430 File Offset: 0x00000630
		public override bool ValidTeleportCheck_NPCCount(TeleportPylonInfo pylonInfo, int defaultNecessaryNPCCount)
		{
			return true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003054 File Offset: 0x00001254
		public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
		{
			return (double)pylonInfo.PositionInTiles.Y <= Main.worldSurface * 0.3499999940395355;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003078 File Offset: 0x00001278
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = (g = (b = 0.75f));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003098 File Offset: 0x00001298
		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			base.DefaultDrawPylonCrystal(spriteBatch, i, j, this.crystalTexture, this.crystalHighlightTexture, new Vector2(-1f, -12f), Color.White * 0.1f, Common.ColorSwap(Color.Azure, Color.AliceBlue, 1.5f), 4, this.CrystalVerticalFrameCount);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000030F4 File Offset: 0x000012F4
		public override void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale)
		{
			bool flag = base.DefaultDrawMapIcon(ref context, this.mapIcon, pylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), drawColor, deselectedScale, selectedScale);
			base.DefaultMapClickHandle(flag, pylonInfo, ModContent.GetInstance<SkyPylon>().DisplayName.Key, ref mouseOverText);
		}

		// Token: 0x0400001D RID: 29
		public int CrystalVerticalFrameCount = 8;

		// Token: 0x0400001E RID: 30
		public Asset<Texture2D> crystalTexture;

		// Token: 0x0400001F RID: 31
		public Asset<Texture2D> crystalHighlightTexture;

		// Token: 0x04000020 RID: 32
		public Asset<Texture2D> mapIcon;
	}
}
