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
	// Token: 0x02000007 RID: 7
	public class CrimsonPylonTile : ModPylon
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000278F File Offset: 0x0000098F
		public override void Load()
		{
			this.crystalTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/PylonCrystals/CrimsonPylonTile_Crystal", 2);
			this.crystalHighlightTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Highlights/PylonCrystal_Highlight", 2);
			this.mapIcon = ModContent.Request<Texture2D>("QoLCompendium/Assets/MapIcons/CrimsonPylonTile_MapIcon", 2);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027C4 File Offset: 0x000009C4
		public override void SetStaticDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TEModdedPylon instance = (TEModdedPylon)ModContent.GetInstance<CrimsonPylonTileEntity>();
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.PlacementPreviewHook_CheckIfCanPlace), 1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.Hook_AfterPlacement), -1, 0, false);
			TileObjectData.addTile((int)base.Type);
			TileID.Sets.InteractibleByNPCs[(int)base.Type] = true;
			TileID.Sets.PreventsSandfall[(int)base.Type] = true;
			TileID.Sets.AvoidedByMeteorLanding[(int)base.Type] = true;
			base.AddToArray(ref TileID.Sets.CountsAsPylon);
			LocalizedText val = base.CreateMapEntryName();
			base.AddMapEntry(Color.Firebrick, val);
			base.DustType = -1;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000028BA File Offset: 0x00000ABA
		public override NPCShop.Entry GetNPCShopEntry()
		{
			if (QoLCompendium.itemConfig.Pylons)
			{
				return new NPCShop.Entry(ModContent.ItemType<CrimsonPylon>(), new Condition[]
				{
					Condition.InCrimson
				});
			}
			return null;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000028E2 File Offset: 0x00000AE2
		public override void MouseOver(int i, int j)
		{
			Main.LocalPlayer.cursorItemIconEnabled = true;
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<CrimsonPylon>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028FE File Offset: 0x00000AFE
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			ModContent.GetInstance<CrimsonPylonTileEntity>().Kill(i, j);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002430 File Offset: 0x00000630
		public override bool ValidTeleportCheck_NPCCount(TeleportPylonInfo pylonInfo, int defaultNecessaryNPCCount)
		{
			return true;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000290C File Offset: 0x00000B0C
		public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
		{
			return sceneData.EnoughTilesForCrimson;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002914 File Offset: 0x00000B14
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = (g = (b = 0.75f));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002934 File Offset: 0x00000B34
		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			base.DefaultDrawPylonCrystal(spriteBatch, i, j, this.crystalTexture, this.crystalHighlightTexture, new Vector2(-1f, -12f), Color.White * 0.1f, Common.ColorSwap(Color.IndianRed, Color.OrangeRed, 1.5f), 4, this.CrystalVerticalFrameCount);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002990 File Offset: 0x00000B90
		public override void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale)
		{
			bool flag = base.DefaultDrawMapIcon(ref context, this.mapIcon, pylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), drawColor, deselectedScale, selectedScale);
			base.DefaultMapClickHandle(flag, pylonInfo, ModContent.GetInstance<CrimsonPylon>().DisplayName.Key, ref mouseOverText);
		}

		// Token: 0x04000011 RID: 17
		public int CrystalVerticalFrameCount = 8;

		// Token: 0x04000012 RID: 18
		public Asset<Texture2D> crystalTexture;

		// Token: 0x04000013 RID: 19
		public Asset<Texture2D> crystalHighlightTexture;

		// Token: 0x04000014 RID: 20
		public Asset<Texture2D> mapIcon;
	}
}
