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
	// Token: 0x02000009 RID: 9
	public class DungeonPylonTile : ModPylon
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000029F7 File Offset: 0x00000BF7
		public override void Load()
		{
			this.crystalTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/PylonCrystals/DungeonPylonTile_Crystal", 2);
			this.crystalHighlightTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Highlights/PylonCrystal_Highlight", 2);
			this.mapIcon = ModContent.Request<Texture2D>("QoLCompendium/Assets/MapIcons/DungeonPylonTile_MapIcon", 2);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A2C File Offset: 0x00000C2C
		public override void SetStaticDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TEModdedPylon instance = (TEModdedPylon)ModContent.GetInstance<DungeonPylonTileEntity>();
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.PlacementPreviewHook_CheckIfCanPlace), 1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.Hook_AfterPlacement), -1, 0, false);
			TileObjectData.addTile((int)base.Type);
			TileID.Sets.InteractibleByNPCs[(int)base.Type] = true;
			TileID.Sets.PreventsSandfall[(int)base.Type] = true;
			TileID.Sets.AvoidedByMeteorLanding[(int)base.Type] = true;
			base.AddToArray(ref TileID.Sets.CountsAsPylon);
			LocalizedText val = base.CreateMapEntryName();
			base.AddMapEntry(Color.Aqua, val);
			base.DustType = -1;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B22 File Offset: 0x00000D22
		public override NPCShop.Entry GetNPCShopEntry()
		{
			if (QoLCompendium.itemConfig.Pylons)
			{
				return new NPCShop.Entry(ModContent.ItemType<DungeonPylon>(), new Condition[]
				{
					Condition.InDungeon
				});
			}
			return null;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002B4A File Offset: 0x00000D4A
		public override void MouseOver(int i, int j)
		{
			Main.LocalPlayer.cursorItemIconEnabled = true;
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<DungeonPylon>();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002B66 File Offset: 0x00000D66
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			ModContent.GetInstance<DungeonPylonTileEntity>().Kill(i, j);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002430 File Offset: 0x00000630
		public override bool ValidTeleportCheck_NPCCount(TeleportPylonInfo pylonInfo, int defaultNecessaryNPCCount)
		{
			return true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B74 File Offset: 0x00000D74
		public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
		{
			return sceneData.DungeonTileCount > 50;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B80 File Offset: 0x00000D80
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = (g = (b = 0.75f));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			base.DefaultDrawPylonCrystal(spriteBatch, i, j, this.crystalTexture, this.crystalHighlightTexture, new Vector2(-1f, -12f), Color.White * 0.1f, Common.ColorSwap(Color.Aqua, Color.DodgerBlue, 1.5f), 4, this.CrystalVerticalFrameCount);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002BFC File Offset: 0x00000DFC
		public override void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale)
		{
			bool flag = base.DefaultDrawMapIcon(ref context, this.mapIcon, pylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), drawColor, deselectedScale, selectedScale);
			base.DefaultMapClickHandle(flag, pylonInfo, ModContent.GetInstance<DungeonPylon>().DisplayName.Key, ref mouseOverText);
		}

		// Token: 0x04000015 RID: 21
		public int CrystalVerticalFrameCount = 8;

		// Token: 0x04000016 RID: 22
		public Asset<Texture2D> crystalTexture;

		// Token: 0x04000017 RID: 23
		public Asset<Texture2D> crystalHighlightTexture;

		// Token: 0x04000018 RID: 24
		public Asset<Texture2D> mapIcon;
	}
}
