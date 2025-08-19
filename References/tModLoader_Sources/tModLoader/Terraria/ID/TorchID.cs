using System;
using Microsoft.Xna.Framework;

namespace Terraria.ID
{
	// Token: 0x02000432 RID: 1074
	public static class TorchID
	{
		// Token: 0x0600358F RID: 13711 RVA: 0x005776E8 File Offset: 0x005758E8
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

		// Token: 0x06003590 RID: 13712 RVA: 0x00577994 File Offset: 0x00575B94
		public static void TorchColor(int torchID, out float R, out float G, out float B)
		{
			if (torchID < 0 || torchID >= TorchID._lights.Length)
			{
				R = (G = (B = 0f));
				return;
			}
			TorchID._lights[torchID].GetRGB(out R, out G, out B);
		}

		// Token: 0x04004E41 RID: 20033
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

		// Token: 0x04004E42 RID: 20034
		private static TorchID.ITorchLightProvider[] _lights;

		// Token: 0x04004E43 RID: 20035
		public const short Torch = 0;

		// Token: 0x04004E44 RID: 20036
		public const short Blue = 1;

		// Token: 0x04004E45 RID: 20037
		public const short Red = 2;

		// Token: 0x04004E46 RID: 20038
		public const short Green = 3;

		// Token: 0x04004E47 RID: 20039
		public const short Purple = 4;

		// Token: 0x04004E48 RID: 20040
		public const short White = 5;

		// Token: 0x04004E49 RID: 20041
		public const short Yellow = 6;

		// Token: 0x04004E4A RID: 20042
		public const short Demon = 7;

		// Token: 0x04004E4B RID: 20043
		public const short Cursed = 8;

		// Token: 0x04004E4C RID: 20044
		public const short Ice = 9;

		// Token: 0x04004E4D RID: 20045
		public const short Orange = 10;

		// Token: 0x04004E4E RID: 20046
		public const short Ichor = 11;

		// Token: 0x04004E4F RID: 20047
		public const short UltraBright = 12;

		// Token: 0x04004E50 RID: 20048
		public const short Bone = 13;

		// Token: 0x04004E51 RID: 20049
		public const short Rainbow = 14;

		// Token: 0x04004E52 RID: 20050
		public const short Pink = 15;

		// Token: 0x04004E53 RID: 20051
		public const short Desert = 16;

		// Token: 0x04004E54 RID: 20052
		public const short Coral = 17;

		// Token: 0x04004E55 RID: 20053
		public const short Corrupt = 18;

		// Token: 0x04004E56 RID: 20054
		public const short Crimson = 19;

		// Token: 0x04004E57 RID: 20055
		public const short Hallowed = 20;

		// Token: 0x04004E58 RID: 20056
		public const short Jungle = 21;

		// Token: 0x04004E59 RID: 20057
		public const short Mushroom = 22;

		// Token: 0x04004E5A RID: 20058
		public const short Shimmer = 23;

		// Token: 0x04004E5B RID: 20059
		public static readonly short Count = 24;

		// Token: 0x02000B6B RID: 2923
		private interface ITorchLightProvider
		{
			// Token: 0x06005CCA RID: 23754
			void GetRGB(out float r, out float g, out float b);
		}

		// Token: 0x02000B6C RID: 2924
		private struct ConstantTorchLight : TorchID.ITorchLightProvider
		{
			// Token: 0x06005CCB RID: 23755 RVA: 0x006C4F7B File Offset: 0x006C317B
			public ConstantTorchLight(float Red, float Green, float Blue)
			{
				this.R = Red;
				this.G = Green;
				this.B = Blue;
			}

			// Token: 0x06005CCC RID: 23756 RVA: 0x006C4F92 File Offset: 0x006C3192
			public void GetRGB(out float r, out float g, out float b)
			{
				r = this.R;
				g = this.G;
				b = this.B;
			}

			// Token: 0x040075DE RID: 30174
			public float R;

			// Token: 0x040075DF RID: 30175
			public float G;

			// Token: 0x040075E0 RID: 30176
			public float B;
		}

		// Token: 0x02000B6D RID: 2925
		private struct DemonTorchLight : TorchID.ITorchLightProvider
		{
			// Token: 0x06005CCD RID: 23757 RVA: 0x006C4FAC File Offset: 0x006C31AC
			public void GetRGB(out float r, out float g, out float b)
			{
				r = 0.5f * Main.demonTorch + (1f - Main.demonTorch);
				g = 0.3f;
				b = Main.demonTorch + 0.5f * (1f - Main.demonTorch);
			}
		}

		// Token: 0x02000B6E RID: 2926
		private struct ShimmerTorchLight : TorchID.ITorchLightProvider
		{
			// Token: 0x06005CCE RID: 23758 RVA: 0x006C4FE8 File Offset: 0x006C31E8
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

		// Token: 0x02000B6F RID: 2927
		private struct DiscoTorchLight : TorchID.ITorchLightProvider
		{
			// Token: 0x06005CCF RID: 23759 RVA: 0x006C5068 File Offset: 0x006C3268
			public void GetRGB(out float r, out float g, out float b)
			{
				r = (float)Main.DiscoR / 255f;
				g = (float)Main.DiscoG / 255f;
				b = (float)Main.DiscoB / 255f;
			}
		}
	}
}
