using System;
using Microsoft.Xna.Framework;

namespace Terraria.ID
{
	// Token: 0x020001A4 RID: 420
	public static class TorchID
	{
		// Token: 0x06001B7F RID: 7039 RVA: 0x004E87DC File Offset: 0x004E69DC
		public static void Initialize()
		{
			TorchID.ITorchLightProvider[] array = new TorchID.ITorchLightProvider[(int)TorchID.Count];
			array[0] = new TorchID.ConstantTorchLight(1f, 0.95f, 0.8f);
			array[1] = new TorchID.ConstantTorchLight(0f, 0.1f, 1.3f);
			array[2] = new TorchID.ConstantTorchLight(1f, 0.1f, 0.1f);
			array[3] = new TorchID.ConstantTorchLight(0f, 1f, 0.1f);
			array[4] = new TorchID.ConstantTorchLight(0.9f, 0f, 0.9f);
			array[5] = new TorchID.ConstantTorchLight(1.4f, 1.4f, 1.4f);
			array[6] = new TorchID.ConstantTorchLight(0.9f, 0.9f, 0f);
			array[7] = default(TorchID.DemonTorchLight);
			array[8] = new TorchID.ConstantTorchLight(1f, 1.6f, 0.5f);
			array[9] = new TorchID.ConstantTorchLight(0.75f, 0.85f, 1.4f);
			array[10] = new TorchID.ConstantTorchLight(1f, 0.5f, 0f);
			array[11] = new TorchID.ConstantTorchLight(1.4f, 1.4f, 0.7f);
			array[12] = new TorchID.ConstantTorchLight(0.75f, 1.3499999f, 1.5f);
			array[13] = new TorchID.ConstantTorchLight(0.95f, 0.75f, 1.3f);
			array[14] = default(TorchID.DiscoTorchLight);
			array[15] = new TorchID.ConstantTorchLight(1f, 0f, 1f);
			array[16] = new TorchID.ConstantTorchLight(1.4f, 0.85f, 0.55f);
			array[17] = new TorchID.ConstantTorchLight(0.25f, 1.3f, 0.8f);
			array[18] = new TorchID.ConstantTorchLight(0.95f, 0.4f, 1.4f);
			array[19] = new TorchID.ConstantTorchLight(1.4f, 0.7f, 0.5f);
			array[20] = new TorchID.ConstantTorchLight(1.25f, 0.6f, 1.2f);
			array[21] = new TorchID.ConstantTorchLight(0.75f, 1.45f, 0.9f);
			array[22] = new TorchID.ConstantTorchLight(0.3f, 0.78f, 1.2f);
			array[23] = default(TorchID.ShimmerTorchLight);
			TorchID._lights = array;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x004E8A88 File Offset: 0x004E6C88
		public static void TorchColor(int torchID, out float R, out float G, out float B)
		{
			if (torchID < 0 || torchID >= TorchID._lights.Length)
			{
				R = (G = (B = 0f));
				return;
			}
			TorchID._lights[torchID].GetRGB(out R, out G, out B);
		}

		// Token: 0x0400174A RID: 5962
		public static int[] Dust = new int[]
		{
			6,
			59,
			60,
			61,
			62,
			63,
			64,
			65,
			75,
			135,
			158,
			169,
			156,
			234,
			66,
			242,
			293,
			294,
			295,
			296,
			297,
			298,
			307,
			310
		};

		// Token: 0x0400174B RID: 5963
		private static TorchID.ITorchLightProvider[] _lights;

		// Token: 0x0400174C RID: 5964
		public const short Torch = 0;

		// Token: 0x0400174D RID: 5965
		public const short Blue = 1;

		// Token: 0x0400174E RID: 5966
		public const short Red = 2;

		// Token: 0x0400174F RID: 5967
		public const short Green = 3;

		// Token: 0x04001750 RID: 5968
		public const short Purple = 4;

		// Token: 0x04001751 RID: 5969
		public const short White = 5;

		// Token: 0x04001752 RID: 5970
		public const short Yellow = 6;

		// Token: 0x04001753 RID: 5971
		public const short Demon = 7;

		// Token: 0x04001754 RID: 5972
		public const short Cursed = 8;

		// Token: 0x04001755 RID: 5973
		public const short Ice = 9;

		// Token: 0x04001756 RID: 5974
		public const short Orange = 10;

		// Token: 0x04001757 RID: 5975
		public const short Ichor = 11;

		// Token: 0x04001758 RID: 5976
		public const short UltraBright = 12;

		// Token: 0x04001759 RID: 5977
		public const short Bone = 13;

		// Token: 0x0400175A RID: 5978
		public const short Rainbow = 14;

		// Token: 0x0400175B RID: 5979
		public const short Pink = 15;

		// Token: 0x0400175C RID: 5980
		public const short Desert = 16;

		// Token: 0x0400175D RID: 5981
		public const short Coral = 17;

		// Token: 0x0400175E RID: 5982
		public const short Corrupt = 18;

		// Token: 0x0400175F RID: 5983
		public const short Crimson = 19;

		// Token: 0x04001760 RID: 5984
		public const short Hallowed = 20;

		// Token: 0x04001761 RID: 5985
		public const short Jungle = 21;

		// Token: 0x04001762 RID: 5986
		public const short Mushroom = 22;

		// Token: 0x04001763 RID: 5987
		public const short Shimmer = 23;

		// Token: 0x04001764 RID: 5988
		public static readonly short Count = 24;

		// Token: 0x020005C9 RID: 1481
		private interface ITorchLightProvider
		{
			// Token: 0x060032BF RID: 12991
			void GetRGB(out float r, out float g, out float b);
		}

		// Token: 0x020005CA RID: 1482
		private struct ConstantTorchLight : TorchID.ITorchLightProvider
		{
			// Token: 0x060032C0 RID: 12992 RVA: 0x005EDD1D File Offset: 0x005EBF1D
			public ConstantTorchLight(float Red, float Green, float Blue)
			{
				this.R = Red;
				this.G = Green;
				this.B = Blue;
			}

			// Token: 0x060032C1 RID: 12993 RVA: 0x005EDD34 File Offset: 0x005EBF34
			public void GetRGB(out float r, out float g, out float b)
			{
				r = this.R;
				g = this.G;
				b = this.B;
			}

			// Token: 0x04005AA7 RID: 23207
			public float R;

			// Token: 0x04005AA8 RID: 23208
			public float G;

			// Token: 0x04005AA9 RID: 23209
			public float B;
		}

		// Token: 0x020005CB RID: 1483
		private struct DemonTorchLight : TorchID.ITorchLightProvider
		{
			// Token: 0x060032C2 RID: 12994 RVA: 0x005EDD4E File Offset: 0x005EBF4E
			public void GetRGB(out float r, out float g, out float b)
			{
				r = 0.5f * Main.demonTorch + (1f - Main.demonTorch);
				g = 0.3f;
				b = Main.demonTorch + 0.5f * (1f - Main.demonTorch);
			}
		}

		// Token: 0x020005CC RID: 1484
		private struct ShimmerTorchLight : TorchID.ITorchLightProvider
		{
			// Token: 0x060032C3 RID: 12995 RVA: 0x005EDD8C File Offset: 0x005EBF8C
			public void GetRGB(out float r, out float g, out float b)
			{
				float num = 0.9f;
				float num2 = 0.9f;
				num += (float)(270 - (int)Main.mouseTextColor) / 900f;
				num2 += (float)(270 - (int)Main.mouseTextColor) / 125f;
				num = MathHelper.Clamp(num, 0f, 1f);
				num2 = MathHelper.Clamp(num2, 0f, 1f);
				r = num * 0.9f;
				g = num2 * 0.55f;
				b = num * 1.2f;
			}
		}

		// Token: 0x020005CD RID: 1485
		private struct DiscoTorchLight : TorchID.ITorchLightProvider
		{
			// Token: 0x060032C4 RID: 12996 RVA: 0x005EDE0C File Offset: 0x005EC00C
			public void GetRGB(out float r, out float g, out float b)
			{
				r = (float)Main.DiscoR / 255f;
				g = (float)Main.DiscoG / 255f;
				b = (float)Main.DiscoB / 255f;
			}
		}
	}
}
