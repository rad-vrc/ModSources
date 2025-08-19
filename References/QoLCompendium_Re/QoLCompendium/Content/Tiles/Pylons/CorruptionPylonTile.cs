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
	// Token: 0x02000005 RID: 5
	public class CorruptionPylonTile : ModPylon
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002527 File Offset: 0x00000727
		public override void Load()
		{
			this.crystalTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/PylonCrystals/CorruptionPylonTile_Crystal", 2);
			this.crystalHighlightTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Highlights/PylonCrystal_Highlight", 2);
			this.mapIcon = ModContent.Request<Texture2D>("QoLCompendium/Assets/MapIcons/CorruptionPylonTile_MapIcon", 2);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000255C File Offset: 0x0000075C
		public override void SetStaticDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TEModdedPylon instance = (TEModdedPylon)ModContent.GetInstance<CorruptionPylonTileEntity>();
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.PlacementPreviewHook_CheckIfCanPlace), 1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.Hook_AfterPlacement), -1, 0, false);
			TileObjectData.addTile((int)base.Type);
			TileID.Sets.InteractibleByNPCs[(int)base.Type] = true;
			TileID.Sets.PreventsSandfall[(int)base.Type] = true;
			TileID.Sets.AvoidedByMeteorLanding[(int)base.Type] = true;
			base.AddToArray(ref TileID.Sets.CountsAsPylon);
			LocalizedText val = base.CreateMapEntryName();
			base.AddMapEntry(Color.BlueViolet, val);
			base.DustType = -1;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002652 File Offset: 0x00000852
		public override NPCShop.Entry GetNPCShopEntry()
		{
			if (QoLCompendium.itemConfig.Pylons)
			{
				return new NPCShop.Entry(ModContent.ItemType<CorruptionPylon>(), new Condition[]
				{
					Condition.InCorrupt
				});
			}
			return null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000267A File Offset: 0x0000087A
		public override void MouseOver(int i, int j)
		{
			Main.LocalPlayer.cursorItemIconEnabled = true;
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<CorruptionPylon>();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002696 File Offset: 0x00000896
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			ModContent.GetInstance<CorruptionPylonTileEntity>().Kill(i, j);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002430 File Offset: 0x00000630
		public override bool ValidTeleportCheck_NPCCount(TeleportPylonInfo pylonInfo, int defaultNecessaryNPCCount)
		{
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026A4 File Offset: 0x000008A4
		public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
		{
			return sceneData.EnoughTilesForCorruption;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026AC File Offset: 0x000008AC
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = (g = (b = 0.75f));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026CC File Offset: 0x000008CC
		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			base.DefaultDrawPylonCrystal(spriteBatch, i, j, this.crystalTexture, this.crystalHighlightTexture, new Vector2(-1f, -12f), Color.White * 0.1f, Common.ColorSwap(Color.BlueViolet, Color.SlateBlue, 1.5f), 4, this.CrystalVerticalFrameCount);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002728 File Offset: 0x00000928
		public override void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale)
		{
			bool flag = base.DefaultDrawMapIcon(ref context, this.mapIcon, pylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), drawColor, deselectedScale, selectedScale);
			base.DefaultMapClickHandle(flag, pylonInfo, ModContent.GetInstance<CorruptionPylon>().DisplayName.Key, ref mouseOverText);
		}

		// Token: 0x0400000D RID: 13
		public int CrystalVerticalFrameCount = 8;

		// Token: 0x0400000E RID: 14
		public Asset<Texture2D> crystalTexture;

		// Token: 0x0400000F RID: 15
		public Asset<Texture2D> crystalHighlightTexture;

		// Token: 0x04000010 RID: 16
		public Asset<Texture2D> mapIcon;
	}
}
