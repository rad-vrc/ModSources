using System;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000254 RID: 596
	public static class NoiseHelper
	{
		// Token: 0x06001F5E RID: 8030 RVA: 0x00512A94 File Offset: 0x00510C94
		private static float[] CreateStaticNoise(int length)
		{
			UnifiedRandom r = new UnifiedRandom(1);
			float[] array = new float[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = r.NextFloat();
			}
			return array;
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x00512AC8 File Offset: 0x00510CC8
		public static float GetDynamicNoise(int index, float currentTime)
		{
			float num = NoiseHelper.StaticNoise[index & 1023];
			float num2 = currentTime % 1f;
			return Math.Abs(Math.Abs(num - num2) - 0.5f) * 2f;
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00512B02 File Offset: 0x00510D02
		public static float GetStaticNoise(int index)
		{
			return NoiseHelper.StaticNoise[index & 1023];
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00512B11 File Offset: 0x00510D11
		public static float GetDynamicNoise(int x, int y, float currentTime)
		{
			return NoiseHelper.GetDynamicNoiseInternal(x, y, currentTime % 1f);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x00512B21 File Offset: 0x00510D21
		private static float GetDynamicNoiseInternal(int x, int y, float wrappedTime)
		{
			x &= 31;
			y &= 31;
			return Math.Abs(Math.Abs(NoiseHelper.StaticNoise[y * 32 + x] - wrappedTime) - 0.5f) * 2f;
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00512B53 File Offset: 0x00510D53
		public static float GetStaticNoise(int x, int y)
		{
			x &= 31;
			y &= 31;
			return NoiseHelper.StaticNoise[y * 32 + x];
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x00512B70 File Offset: 0x00510D70
		public static float GetDynamicNoise(Vector2 position, float currentTime)
		{
			position *= 10f;
			currentTime %= 1f;
			Vector2 vector = new Vector2((float)Math.Floor((double)position.X), (float)Math.Floor((double)position.Y));
			Point point = new Point((int)vector.X, (int)vector.Y);
			Vector2 vector2 = new Vector2(position.X - vector.X, position.Y - vector.Y);
			float value = MathHelper.Lerp(NoiseHelper.GetDynamicNoiseInternal(point.X, point.Y, currentTime), NoiseHelper.GetDynamicNoiseInternal(point.X, point.Y + 1, currentTime), vector2.Y);
			float value2 = MathHelper.Lerp(NoiseHelper.GetDynamicNoiseInternal(point.X + 1, point.Y, currentTime), NoiseHelper.GetDynamicNoiseInternal(point.X + 1, point.Y + 1, currentTime), vector2.Y);
			return MathHelper.Lerp(value, value2, vector2.X);
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x00512C60 File Offset: 0x00510E60
		public static float GetStaticNoise(Vector2 position)
		{
			position *= 10f;
			Vector2 vector = new Vector2((float)Math.Floor((double)position.X), (float)Math.Floor((double)position.Y));
			Point point = new Point((int)vector.X, (int)vector.Y);
			Vector2 vector2 = new Vector2(position.X - vector.X, position.Y - vector.Y);
			float value = MathHelper.Lerp(NoiseHelper.GetStaticNoise(point.X, point.Y), NoiseHelper.GetStaticNoise(point.X, point.Y + 1), vector2.Y);
			float value2 = MathHelper.Lerp(NoiseHelper.GetStaticNoise(point.X + 1, point.Y), NoiseHelper.GetStaticNoise(point.X + 1, point.Y + 1), vector2.Y);
			return MathHelper.Lerp(value, value2, vector2.X);
		}

		// Token: 0x04004666 RID: 18022
		private const int RANDOM_SEED = 1;

		// Token: 0x04004667 RID: 18023
		private const int NOISE_2D_SIZE = 32;

		// Token: 0x04004668 RID: 18024
		private const int NOISE_2D_SIZE_MASK = 31;

		// Token: 0x04004669 RID: 18025
		private const int NOISE_SIZE_MASK = 1023;

		// Token: 0x0400466A RID: 18026
		private static readonly float[] StaticNoise = NoiseHelper.CreateStaticNoise(1024);
	}
}
