using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria.GameContent
{
	// Token: 0x020001F1 RID: 497
	public class TreeTopsInfo
	{
		// Token: 0x06001CDE RID: 7390 RVA: 0x004FE170 File Offset: 0x004FC370
		public void Save(BinaryWriter writer)
		{
			writer.Write(this._variations.Length);
			for (int i = 0; i < this._variations.Length; i++)
			{
				writer.Write(this._variations[i]);
			}
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x004FE1AC File Offset: 0x004FC3AC
		public void Load(BinaryReader reader, int loadVersion)
		{
			if (loadVersion < 211)
			{
				this.CopyExistingWorldInfo();
				return;
			}
			int num = reader.ReadInt32();
			int num2 = 0;
			while (num2 < num && num2 < this._variations.Length)
			{
				this._variations[num2] = reader.ReadInt32();
				num2++;
			}
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x004FE1F4 File Offset: 0x004FC3F4
		public void SyncSend(BinaryWriter writer)
		{
			for (int i = 0; i < this._variations.Length; i++)
			{
				writer.Write((byte)this._variations[i]);
			}
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x004FE224 File Offset: 0x004FC424
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

		// Token: 0x06001CE2 RID: 7394 RVA: 0x004FE26D File Offset: 0x004FC46D
		public int GetTreeStyle(int areaId)
		{
			return this._variations[areaId];
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x004FE278 File Offset: 0x004FC478
		public void RandomizeTreeStyleBasedOnWorldPosition(UnifiedRandom rand, Vector2 worldPosition)
		{
			Point point = new Point((int)(worldPosition.X / 16f), (int)(worldPosition.Y / 16f) + 1);
			Tile tileSafely = Framing.GetTileSafely(point);
			if (!tileSafely.active())
			{
				return;
			}
			int num = -1;
			if (tileSafely.type == 70)
			{
				num = 11;
			}
			else if (tileSafely.type == 53 && WorldGen.oceanDepths(point.X, point.Y))
			{
				num = 10;
			}
			else if (tileSafely.type == 23)
			{
				num = 4;
			}
			else if (tileSafely.type == 199)
			{
				num = 8;
			}
			else if (tileSafely.type == 109 || tileSafely.type == 492)
			{
				num = 7;
			}
			else if (tileSafely.type == 53)
			{
				num = 9;
			}
			else if (tileSafely.type == 147)
			{
				num = 6;
			}
			else if (tileSafely.type == 60)
			{
				num = 5;
			}
			else if (tileSafely.type == 633)
			{
				num = 12;
			}
			else if (tileSafely.type == 2 || tileSafely.type == 477)
			{
				if (point.X < Main.treeX[0])
				{
					num = 0;
				}
				else if (point.X < Main.treeX[1])
				{
					num = 1;
				}
				else if (point.X < Main.treeX[2])
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
			}
			if (num > -1)
			{
				this.RandomizeTreeStyle(rand, num);
			}
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x004FE3D4 File Offset: 0x004FC5D4
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

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private void DoTreeFX(int areaID)
		{
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x004FE53E File Offset: 0x004FC73E
		public void CopyExistingWorldInfoForWorldGeneration()
		{
			this.CopyExistingWorldInfo();
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x004FE548 File Offset: 0x004FC748
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

		// Token: 0x040043DD RID: 17373
		private int[] _variations = new int[TreeTopsInfo.AreaId.Count];

		// Token: 0x0200060E RID: 1550
		public class AreaId
		{
			// Token: 0x0400605B RID: 24667
			public const int Forest1 = 0;

			// Token: 0x0400605C RID: 24668
			public const int Forest2 = 1;

			// Token: 0x0400605D RID: 24669
			public const int Forest3 = 2;

			// Token: 0x0400605E RID: 24670
			public const int Forest4 = 3;

			// Token: 0x0400605F RID: 24671
			public const int Corruption = 4;

			// Token: 0x04006060 RID: 24672
			public const int Jungle = 5;

			// Token: 0x04006061 RID: 24673
			public const int Snow = 6;

			// Token: 0x04006062 RID: 24674
			public const int Hallow = 7;

			// Token: 0x04006063 RID: 24675
			public const int Crimson = 8;

			// Token: 0x04006064 RID: 24676
			public const int Desert = 9;

			// Token: 0x04006065 RID: 24677
			public const int Ocean = 10;

			// Token: 0x04006066 RID: 24678
			public const int GlowingMushroom = 11;

			// Token: 0x04006067 RID: 24679
			public const int Underworld = 12;

			// Token: 0x04006068 RID: 24680
			public static readonly int Count = 13;
		}
	}
}
