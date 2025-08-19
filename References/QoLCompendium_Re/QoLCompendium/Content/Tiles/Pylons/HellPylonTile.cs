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
	// Token: 0x0200000B RID: 11
	public class HellPylonTile : ModPylon
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002C63 File Offset: 0x00000E63
		public override void Load()
		{
			this.crystalTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/PylonCrystals/HellPylonTile_Crystal", 2);
			this.crystalHighlightTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Highlights/PylonCrystal_Highlight", 2);
			this.mapIcon = ModContent.Request<Texture2D>("QoLCompendium/Assets/MapIcons/HellPylonTile_MapIcon", 2);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C98 File Offset: 0x00000E98
		public override void SetStaticDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TEModdedPylon instance = (TEModdedPylon)ModContent.GetInstance<HellPylonTileEntity>();
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.PlacementPreviewHook_CheckIfCanPlace), 1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.Hook_AfterPlacement), -1, 0, false);
			TileObjectData.addTile((int)base.Type);
			TileID.Sets.InteractibleByNPCs[(int)base.Type] = true;
			TileID.Sets.PreventsSandfall[(int)base.Type] = true;
			TileID.Sets.AvoidedByMeteorLanding[(int)base.Type] = true;
			base.AddToArray(ref TileID.Sets.CountsAsPylon);
			LocalizedText val = base.CreateMapEntryName();
			base.AddMapEntry(Color.Orange, val);
			base.DustType = -1;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002D8E File Offset: 0x00000F8E
		public override NPCShop.Entry GetNPCShopEntry()
		{
			if (QoLCompendium.itemConfig.Pylons)
			{
				return new NPCShop.Entry(ModContent.ItemType<HellPylon>(), new Condition[]
				{
					Condition.InUnderworld
				});
			}
			return null;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002DB6 File Offset: 0x00000FB6
		public override void MouseOver(int i, int j)
		{
			Main.LocalPlayer.cursorItemIconEnabled = true;
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<HellPylon>();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002DD2 File Offset: 0x00000FD2
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			ModContent.GetInstance<HellPylonTileEntity>().Kill(i, j);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002430 File Offset: 0x00000630
		public override bool ValidTeleportCheck_NPCCount(TeleportPylonInfo pylonInfo, int defaultNecessaryNPCCount)
		{
			return true;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
		{
			return (int)pylonInfo.PositionInTiles.Y > Main.UnderworldLayer;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = (g = (b = 0.75f));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002E14 File Offset: 0x00001014
		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			base.DefaultDrawPylonCrystal(spriteBatch, i, j, this.crystalTexture, this.crystalHighlightTexture, new Vector2(-1f, -12f), Color.White * 0.1f, Common.ColorSwap(Color.Orange, Color.OrangeRed, 1.5f), 4, this.CrystalVerticalFrameCount);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002E70 File Offset: 0x00001070
		public override void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale)
		{
			bool flag = base.DefaultDrawMapIcon(ref context, this.mapIcon, pylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), drawColor, deselectedScale, selectedScale);
			base.DefaultMapClickHandle(flag, pylonInfo, ModContent.GetInstance<HellPylon>().DisplayName.Key, ref mouseOverText);
		}

		// Token: 0x04000019 RID: 25
		public int CrystalVerticalFrameCount = 8;

		// Token: 0x0400001A RID: 26
		public Asset<Texture2D> crystalTexture;

		// Token: 0x0400001B RID: 27
		public Asset<Texture2D> crystalHighlightTexture;

		// Token: 0x0400001C RID: 28
		public Asset<Texture2D> mapIcon;
	}
}
