using System;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000598 RID: 1432
	public static class NoiseHelper
	{
		// Token: 0x06004246 RID: 16966 RVA: 0x005F5B94 File Offset: 0x005F3D94
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

		// Token: 0x06004247 RID: 16967 RVA: 0x005F5BC8 File Offset: 0x005F3DC8
		public static float GetDynamicNoise(int index, float currentTime)
		{
			float num3 = NoiseHelper.StaticNoise[index & 1023];
			float num2 = currentTime % 1f;
			return Math.Abs(Math.Abs(num3 - num2) - 0.5f) * 2f;
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x005F5C02 File Offset: 0x005F3E02
		public static float GetStaticNoise(int index)
		{
			return NoiseHelper.StaticNoise[index & 1023];
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x005F5C11 File Offset: 0x005F3E11
		public static float GetDynamicNoise(int x, int y, float currentTime)
		{
			return NoiseHelper.GetDynamicNoiseInternal(x, y, currentTime % 1f);
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x005F5C21 File Offset: 0x005F3E21
		private static float GetDynamicNoiseInternal(int x, int y, float wrappedTime)
		{
			x &= 31;
			y &= 31;
			return Math.Abs(Math.Abs(NoiseHelper.StaticNoise[y * 32 + x] - wrappedTime) - 0.5f) * 2f;
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x005F5C53 File Offset: 0x005F3E53
		public static float GetStaticNoise(int x, int y)
		{
			x &= 31;
			y &= 31;
			return NoiseHelper.StaticNoise[y * 32 + x];
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x005F5C70 File Offset: 0x005F3E70
		public static float GetDynamicNoise(Vector2 position, float currentTime)
		{
			position *= 10f;
			currentTime %= 1f;
			Vector2 vector;
			vector..ctor((float)Math.Floor((double)position.X), (float)Math.Floor((double)position.Y));
			Point point;
			point..ctor((int)vector.X, (int)vector.Y);
			Vector2 vector2;
			vector2..ctor(position.X - vector.X, position.Y - vector.Y);
			float num = MathHelper.Lerp(NoiseHelper.GetDynamicNoiseInternal(point.X, point.Y, currentTime), NoiseHelper.GetDynamicNoiseInternal(point.X, point.Y + 1, currentTime), vector2.Y);
			float value2 = MathHelper.Lerp(NoiseHelper.GetDynamicNoiseInternal(point.X + 1, point.Y, currentTime), NoiseHelper.GetDynamicNoiseInternal(point.X + 1, point.Y + 1, currentTime), vector2.Y);
			return MathHelper.Lerp(num, value2, vector2.X);
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x005F5D60 File Offset: 0x005F3F60
		public static float GetStaticNoise(Vector2 position)
		{
			position *= 10f;
			Vector2 vector;
			vector..ctor((float)Math.Floor((double)position.X), (float)Math.Floor((double)position.Y));
			Point point;
			point..ctor((int)vector.X, (int)vector.Y);
			Vector2 vector2;
			vector2..ctor(position.X - vector.X, position.Y - vector.Y);
			float num = MathHelper.Lerp(NoiseHelper.GetStaticNoise(point.X, point.Y), NoiseHelper.GetStaticNoise(point.X, point.Y + 1), vector2.Y);
			float value2 = MathHelper.Lerp(NoiseHelper.GetStaticNoise(point.X + 1, point.Y), NoiseHelper.GetStaticNoise(point.X + 1, point.Y + 1), vector2.Y);
			return MathHelper.Lerp(num, value2, vector2.X);
		}

		// Token: 0x04005980 RID: 22912
		private const int RANDOM_SEED = 1;

		// Token: 0x04005981 RID: 22913
		private const int NOISE_2D_SIZE = 32;

		// Token: 0x04005982 RID: 22914
		private const int NOISE_2D_SIZE_MASK = 31;

		// Token: 0x04005983 RID: 22915
		private const int NOISE_SIZE_MASK = 1023;

		// Token: 0x04005984 RID: 22916
		private static readonly float[] StaticNoise = NoiseHelper.CreateStaticNoise(1024);
	}
}
