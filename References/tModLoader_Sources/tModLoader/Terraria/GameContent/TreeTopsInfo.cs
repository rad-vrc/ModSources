using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria.GameContent
{
	// Token: 0x020004C2 RID: 1218
	public class TreeTopsInfo
	{
		// Token: 0x06003A50 RID: 14928 RVA: 0x005A7970 File Offset: 0x005A5B70
		public void Save(BinaryWriter writer)
		{
			writer.Write(this._variations.Length);
			for (int i = 0; i < this._variations.Length; i++)
			{
				writer.Write(this._variations[i]);
			}
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x005A79AC File Offset: 0x005A5BAC
		public void Load(BinaryReader reader, int loadVersion)
		{
			if (loadVersion < 211)
			{
				this.CopyExistingWorldInfo();
				return;
			}
			int num = reader.ReadInt32();
			int i = 0;
			while (i < num && i < this._variations.Length)
			{
				this._variations[i] = reader.ReadInt32();
				i++;
			}
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x005A79F4 File Offset: 0x005A5BF4
		public void SyncSend(BinaryWriter writer)
		{
			for (int i = 0; i < this._variations.Length; i++)
			{
				writer.Write((byte)this._variations[i]);
			}
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x005A7A24 File Offset: 0x005A5C24
		public void SyncReceive(BinaryReader reader)
		{
			for (int i = 0; i < this._variations.Length; i++)
			{
				int num = this._variations[i];
				this._variations[i] = (int)reader.ReadByte();
				if (this._variations[i] != num)
				{
					this.DoTreeFX(i);
				}
			}
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x005A7A6D File Offset: 0x005A5C6D
		public int GetTreeStyle(int areaId)
		{
			return this._variations[areaId];
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x005A7A78 File Offset: 0x005A5C78
		public unsafe void RandomizeTreeStyleBasedOnWorldPosition(UnifiedRandom rand, Vector2 worldPosition)
		{
			Point pt;
			pt..ctor((int)(worldPosition.X / 16f), (int)(worldPosition.Y / 16f) + 1);
			Tile tileSafely = Framing.GetTileSafely(pt);
			if (tileSafely.active())
			{
				int num = -1;
				if (*tileSafely.type == 70)
				{
					num = 11;
				}
				else if (*tileSafely.type == 53 && WorldGen.oceanDepths(pt.X, pt.Y))
				{
					num = 10;
				}
				else if (*tileSafely.type == 23)
				{
					num = 4;
				}
				else if (*tileSafely.type == 199)
				{
					num = 8;
				}
				else if (*tileSafely.type == 109 || *tileSafely.type == 492)
				{
					num = 7;
				}
				else if (*tileSafely.type == 53)
				{
					num = 9;
				}
				else if (*tileSafely.type == 147)
				{
					num = 6;
				}
				else if (*tileSafely.type == 60)
				{
					num = 5;
				}
				else if (*tileSafely.type == 633)
				{
					num = 12;
				}
				else if (*tileSafely.type == 2 || *tileSafely.type == 477)
				{
					num = ((pt.X >= Main.treeX[0]) ? ((pt.X < Main.treeX[1]) ? 1 : ((pt.X >= Main.treeX[2]) ? 3 : 2)) : 0);
				}
				if (num > -1)
				{
					this.RandomizeTreeStyle(rand, num);
				}
			}
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x005A7BEC File Offset: 0x005A5DEC
		public void RandomizeTreeStyle(UnifiedRandom rand, int areaId)
		{
			int num = this._variations[areaId];
			bool flag = false;
			while (this._variations[areaId] == num)
			{
				switch (areaId)
				{
				case 0:
				case 1:
				case 2:
				case 3:
					this._variations[areaId] = rand.Next(6);
					break;
				case 4:
					this._variations[areaId] = rand.Next(5);
					break;
				case 5:
					this._variations[areaId] = rand.Next(6);
					break;
				case 6:
					this._variations[areaId] = rand.NextFromList(new int[]
					{
						0,
						1,
						2,
						21,
						22,
						3,
						31,
						32,
						4,
						41,
						42,
						5,
						6,
						7
					});
					break;
				case 7:
					this._variations[areaId] = rand.Next(5);
					break;
				case 8:
					this._variations[areaId] = rand.Next(6);
					break;
				case 9:
					this._variations[areaId] = rand.Next(5);
					break;
				case 10:
					this._variations[areaId] = rand.Next(6);
					break;
				case 11:
					this._variations[areaId] = rand.Next(4);
					break;
				case 12:
					this._variations[areaId] = rand.Next(6);
					break;
				default:
					flag = true;
					break;
				}
				if (flag)
				{
					break;
				}
			}
			if (num != this._variations[areaId])
			{
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				this.DoTreeFX(areaId);
			}
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x005A7D56 File Offset: 0x005A5F56
		private void DoTreeFX(int areaID)
		{
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x005A7D58 File Offset: 0x005A5F58
		public void CopyExistingWorldInfoForWorldGeneration()
		{
			this.CopyExistingWorldInfo();
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x005A7D60 File Offset: 0x005A5F60
		private void CopyExistingWorldInfo()
		{
			this._variations[0] = Main.treeStyle[0];
			this._variations[1] = Main.treeStyle[1];
			this._variations[2] = Main.treeStyle[2];
			this._variations[3] = Main.treeStyle[3];
			this._variations[4] = WorldGen.corruptBG;
			this._variations[5] = WorldGen.jungleBG;
			this._variations[6] = WorldGen.snowBG;
			this._variations[7] = WorldGen.hallowBG;
			this._variations[8] = WorldGen.crimsonBG;
			this._variations[9] = WorldGen.desertBG;
			this._variations[10] = WorldGen.oceanBG;
			this._variations[11] = WorldGen.mushroomBG;
			this._variations[12] = WorldGen.underworldBG;
		}

		// Token: 0x040053F9 RID: 21497
		private int[] _variations = new int[TreeTopsInfo.AreaId.Count];

		// Token: 0x02000BCB RID: 3019
		public class AreaId
		{
			// Token: 0x0400771B RID: 30491
			public const int Forest1 = 0;

			// Token: 0x0400771C RID: 30492
			public const int Forest2 = 1;

			// Token: 0x0400771D RID: 30493
			public const int Forest3 = 2;

			// Token: 0x0400771E RID: 30494
			public const int Forest4 = 3;

			// Token: 0x0400771F RID: 30495
			public const int Corruption = 4;

			// Token: 0x04007720 RID: 30496
			public const int Jungle = 5;

			// Token: 0x04007721 RID: 30497
			public const int Snow = 6;

			// Token: 0x04007722 RID: 30498
			public const int Hallow = 7;

			// Token: 0x04007723 RID: 30499
			public const int Crimson = 8;

			// Token: 0x04007724 RID: 30500
			public const int Desert = 9;

			// Token: 0x04007725 RID: 30501
			public const int Ocean = 10;

			// Token: 0x04007726 RID: 30502
			public const int GlowingMushroom = 11;

			// Token: 0x04007727 RID: 30503
			public const int Underworld = 12;

			// Token: 0x04007728 RID: 30504
			public static readonly int Count = 13;
		}
	}
}
