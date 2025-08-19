using System;

namespace Terraria
{
	// Token: 0x02000054 RID: 84
	public class Sign
	{
		/// <summary>
		/// Kills the <see cref="T:Terraria.Sign" /> at the provided coordinates. Call this in <see cref="M:Terraria.ModLoader.ModTile.KillMultiTile(System.Int32,System.Int32,System.Int32,System.Int32)" /> to clean up the Sign instance corresponding to a <see cref="F:Terraria.Main.tileSign" /> tile.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		// Token: 0x06000EFF RID: 3839 RVA: 0x003FB3E8 File Offset: 0x003F95E8
		public static void KillSign(int x, int y)
		{
			for (int i = 0; i < 1000; i++)
			{
				if (Main.sign[i] != null && Main.sign[i].x == x && Main.sign[i].y == y)
				{
					Main.sign[i] = null;
				}
			}
		}

		/// <summary>
		/// Retrieves the index of the <see cref="T:Terraria.Sign" /> within the <see cref="F:Terraria.Main.sign" /> array that exists at the coordinates provided. Any tile of the 2x2 sign can be passed in. The actual Sign instance is located at the top left corner of the multi-tile. If <paramref name="CreateIfMissing" /> is true the sign will be initialized at the coordinates if it does not exist.
		/// <para /> Returns -1 if there is no <see cref="F:Terraria.Main.tileSign" /> tile at the provided coordinate, if <paramref name="CreateIfMissing" /> is false and no sign is initialized at the location, or if the sign limit for the world has been reached.
		/// </summary>
		// Token: 0x06000F00 RID: 3840 RVA: 0x003FB434 File Offset: 0x003F9634
		public unsafe static int ReadSign(int i, int j, bool CreateIfMissing = true)
		{
			int num = (int)(*Main.tile[i, j].frameX / 18);
			int num2 = (int)(*Main.tile[i, j].frameY / 18);
			num %= 2;
			int num3 = i - num;
			int num4 = j - num2;
			if (!Main.tileSign[(int)(*Main.tile[num3, num4].type)])
			{
				Sign.KillSign(num3, num4);
				return -1;
			}
			int num5 = -1;
			for (int k = 0; k < 1000; k++)
			{
				if (Main.sign[k] != null && Main.sign[k].x == num3 && Main.sign[k].y == num4)
				{
					num5 = k;
					break;
				}
			}
			if (num5 < 0 && CreateIfMissing)
			{
				for (int l = 0; l < 1000; l++)
				{
					if (Main.sign[l] == null)
					{
						num5 = l;
						Main.sign[l] = new Sign();
						Main.sign[l].x = num3;
						Main.sign[l].y = num4;
						Main.sign[l].text = "";
						break;
					}
				}
			}
			return num5;
		}

		/// <summary>
		/// Sets the text of the sign corresponding to the given index. Use <see cref="M:Terraria.Sign.ReadSign(System.Int32,System.Int32,System.Boolean)" /> to retrieve the index. Behaves similar to <c>Main.sign[i].text = text;</c> except it also handles nulling the sign if it is no longer valid for some reason.
		/// </summary>
		// Token: 0x06000F01 RID: 3841 RVA: 0x003FB55C File Offset: 0x003F975C
		public unsafe static void TextSign(int i, string text)
		{
			if (Main.tile[Main.sign[i].x, Main.sign[i].y] == null || !Main.tile[Main.sign[i].x, Main.sign[i].y].active() || !Main.tileSign[(int)(*Main.tile[Main.sign[i].x, Main.sign[i].y].type)])
			{
				Main.sign[i] = null;
				return;
			}
			Main.sign[i].text = text;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x003FB608 File Offset: 0x003F9808
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"x",
				this.x.ToString(),
				"\ty",
				this.y.ToString(),
				"\t",
				this.text
			});
		}

		// Token: 0x04000EAE RID: 3758
		public const int maxSigns = 1000;

		// Token: 0x04000EAF RID: 3759
		public int x;

		// Token: 0x04000EB0 RID: 3760
		public int y;

		// Token: 0x04000EB1 RID: 3761
		public string text;
	}
}
