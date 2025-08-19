using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002CF RID: 719
	public class UnloadedChest : UnloadedTile
	{
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x0053032B File Offset: 0x0052E52B
		public override string Texture
		{
			get
			{
				return "ModLoader/UnloadedChest";
			}
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x00530334 File Offset: 0x0052E534
		public override void SetStaticDefaults()
		{
			TileIO.Tiles.unloadedTypes.Add(base.Type);
			Main.tileFrameImportant[(int)base.Type] = true;
			TileID.Sets.DisableSmartCursor[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			TileID.Sets.BasicChest[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile((int)base.Type);
			Main.tileSpelunker[(int)base.Type] = true;
			Main.tileContainer[(int)base.Type] = true;
			Main.tileShine2[(int)base.Type] = true;
			Main.tileShine[(int)base.Type] = 1200;
			Main.tileOreFinderPriority[(int)base.Type] = 500;
			base.AdjTiles = new int[]
			{
				21
			};
			Color color = new Color(200, 200, 200);
			LocalizedText localization = this.GetLocalization("MapEntry0", null);
			Func<string, int, int, string> nameFunc;
			if ((nameFunc = UnloadedChest.<>O.<0>__MapChestName) == null)
			{
				nameFunc = (UnloadedChest.<>O.<0>__MapChestName = new Func<string, int, int, string>(UnloadedChest.MapChestName));
			}
			base.AddMapEntry(color, localization, nameFunc);
			Color color2 = new Color(0, 141, 63);
			LocalizedText localization2 = this.GetLocalization("MapEntry1", null);
			Func<string, int, int, string> nameFunc2;
			if ((nameFunc2 = UnloadedChest.<>O.<0>__MapChestName) == null)
			{
				nameFunc2 = (UnloadedChest.<>O.<0>__MapChestName = new Func<string, int, int, string>(UnloadedChest.MapChestName));
			}
			base.AddMapEntry(color2, localization2, nameFunc2);
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x00530486 File Offset: 0x0052E686
		public override LocalizedText DefaultContainerName(int frameX, int frameY)
		{
			return Language.GetText(this.GetLocalizationKey("MapEntry0"));
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x00530498 File Offset: 0x0052E698
		public override ushort GetMapOption(int i, int j)
		{
			return 0;
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x0053049C File Offset: 0x0052E69C
		public unsafe static string MapChestName(string name, int i, int j)
		{
			int left = i;
			int top = j;
			Tile tile = Main.tile[i, j];
			if (*tile.frameX % 36 != 0)
			{
				left--;
			}
			if (*tile.frameY != 0)
			{
				top--;
			}
			int chest = Chest.FindChest(left, top);
			if (chest < 0)
			{
				return Language.GetTextValue("LegacyChestType.0");
			}
			if (Main.chest[chest].name == "")
			{
				return name;
			}
			return name + ": " + Main.chest[chest].name;
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x00530521 File Offset: 0x0052E721
		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
			tileFrameX %= 32;
		}

		// Token: 0x02000A7E RID: 2686
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006D43 RID: 27971
			public static Func<string, int, int, string> <0>__MapChestName;
		}
	}
}
