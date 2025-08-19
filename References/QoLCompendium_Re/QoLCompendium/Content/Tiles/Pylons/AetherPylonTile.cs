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
	// Token: 0x02000003 RID: 3
	public class AetherPylonTile : ModPylon
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000022B3 File Offset: 0x000004B3
		public override void Load()
		{
			this.crystalTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/PylonCrystals/AetherPylonTile_Crystal", 2);
			this.crystalHighlightTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Highlights/PylonCrystal_Highlight", 2);
			this.mapIcon = ModContent.Request<Texture2D>("QoLCompendium/Assets/MapIcons/AetherPylonTile_MapIcon", 2);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022E8 File Offset: 0x000004E8
		public override void SetStaticDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TEModdedPylon instance = (TEModdedPylon)ModContent.GetInstance<AetherPylonTileEntity>();
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.PlacementPreviewHook_CheckIfCanPlace), 1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.Hook_AfterPlacement), -1, 0, false);
			TileObjectData.addTile((int)base.Type);
			TileID.Sets.InteractibleByNPCs[(int)base.Type] = true;
			TileID.Sets.PreventsSandfall[(int)base.Type] = true;
			TileID.Sets.AvoidedByMeteorLanding[(int)base.Type] = true;
			base.AddToArray(ref TileID.Sets.CountsAsPylon);
			LocalizedText val = base.CreateMapEntryName();
			base.AddMapEntry(Color.MediumOrchid, val);
			base.DustType = -1;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023DE File Offset: 0x000005DE
		public override NPCShop.Entry GetNPCShopEntry()
		{
			if (QoLCompendium.itemConfig.Pylons)
			{
				return new NPCShop.Entry(ModContent.ItemType<AetherPylon>(), new Condition[]
				{
					Condition.InAether
				});
			}
			return null;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002406 File Offset: 0x00000606
		public override void MouseOver(int i, int j)
		{
			Main.LocalPlayer.cursorItemIconEnabled = true;
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<AetherPylon>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002422 File Offset: 0x00000622
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			ModContent.GetInstance<AetherPylonTileEntity>().Kill(i, j);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002430 File Offset: 0x00000630
		public override bool ValidTeleportCheck_NPCCount(TeleportPylonInfo pylonInfo, int defaultNecessaryNPCCount)
		{
			return true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002433 File Offset: 0x00000633
		public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
		{
			return sceneData.EnoughTilesForShimmer;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000243C File Offset: 0x0000063C
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = (g = (b = 0.75f));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000245C File Offset: 0x0000065C
		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			base.DefaultDrawPylonCrystal(spriteBatch, i, j, this.crystalTexture, this.crystalHighlightTexture, new Vector2(-1f, -12f), Color.White * 0.1f, Common.ColorSwap(Color.MediumOrchid, Color.MediumSlateBlue, 1.5f), 4, this.CrystalVerticalFrameCount);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024B8 File Offset: 0x000006B8
		public override void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale)
		{
			bool flag = base.DefaultDrawMapIcon(ref context, this.mapIcon, pylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), drawColor, deselectedScale, selectedScale);
			base.DefaultMapClickHandle(flag, pylonInfo, ModContent.GetInstance<AetherPylon>().DisplayName.Key, ref mouseOverText);
		}

		// Token: 0x04000009 RID: 9
		public int CrystalVerticalFrameCount = 8;

		// Token: 0x0400000A RID: 10
		public Asset<Texture2D> crystalTexture;

		// Token: 0x0400000B RID: 11
		public Asset<Texture2D> crystalHighlightTexture;

		// Token: 0x0400000C RID: 12
		public Asset<Texture2D> mapIcon;
	}
}
