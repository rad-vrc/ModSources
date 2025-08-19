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
	// Token: 0x0200000F RID: 15
	public class TemplePylonTile : ModPylon
	{
		// Token: 0x06000052 RID: 82 RVA: 0x0000315B File Offset: 0x0000135B
		public override void Load()
		{
			this.crystalTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/PylonCrystals/TemplePylonTile_Crystal", 2);
			this.crystalHighlightTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Highlights/PylonCrystal_Highlight", 2);
			this.mapIcon = ModContent.Request<Texture2D>("QoLCompendium/Assets/MapIcons/TemplePylonTile_MapIcon", 2);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003190 File Offset: 0x00001390
		public override void SetStaticDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TEModdedPylon instance = (TEModdedPylon)ModContent.GetInstance<TemplePylonTileEntity>();
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.PlacementPreviewHook_CheckIfCanPlace), 1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.Hook_AfterPlacement), -1, 0, false);
			TileObjectData.addTile((int)base.Type);
			TileID.Sets.InteractibleByNPCs[(int)base.Type] = true;
			TileID.Sets.PreventsSandfall[(int)base.Type] = true;
			TileID.Sets.AvoidedByMeteorLanding[(int)base.Type] = true;
			base.AddToArray(ref TileID.Sets.CountsAsPylon);
			LocalizedText val = base.CreateMapEntryName();
			base.AddMapEntry(Color.GreenYellow, val);
			base.DustType = -1;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003286 File Offset: 0x00001486
		public override NPCShop.Entry GetNPCShopEntry()
		{
			if (QoLCompendium.itemConfig.Pylons)
			{
				return new NPCShop.Entry(ModContent.ItemType<TemplePylon>(), new Condition[]
				{
					Condition.InLihzhardTemple
				});
			}
			return null;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000032AE File Offset: 0x000014AE
		public override void MouseOver(int i, int j)
		{
			Main.LocalPlayer.cursorItemIconEnabled = true;
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<TemplePylon>();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000032CA File Offset: 0x000014CA
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			ModContent.GetInstance<TemplePylonTileEntity>().Kill(i, j);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002430 File Offset: 0x00000630
		public override bool ValidTeleportCheck_NPCCount(TeleportPylonInfo pylonInfo, int defaultNecessaryNPCCount)
		{
			return true;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000032D8 File Offset: 0x000014D8
		public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
		{
			return sceneData.GetTileCount(226) > 50;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000032EC File Offset: 0x000014EC
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = (g = (b = 0.75f));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000330C File Offset: 0x0000150C
		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			base.DefaultDrawPylonCrystal(spriteBatch, i, j, this.crystalTexture, this.crystalHighlightTexture, new Vector2(-1f, -12f), Color.White * 0.1f, Common.ColorSwap(Color.GreenYellow, Color.YellowGreen, 1.5f), 4, this.CrystalVerticalFrameCount);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003368 File Offset: 0x00001568
		public override void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale)
		{
			bool flag = base.DefaultDrawMapIcon(ref context, this.mapIcon, pylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), drawColor, deselectedScale, selectedScale);
			base.DefaultMapClickHandle(flag, pylonInfo, ModContent.GetInstance<TemplePylon>().DisplayName.Key, ref mouseOverText);
		}

		// Token: 0x04000021 RID: 33
		public int CrystalVerticalFrameCount = 8;

		// Token: 0x04000022 RID: 34
		public Asset<Texture2D> crystalTexture;

		// Token: 0x04000023 RID: 35
		public Asset<Texture2D> crystalHighlightTexture;

		// Token: 0x04000024 RID: 36
		public Asset<Texture2D> mapIcon;
	}
}
