using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000587 RID: 1415
	public class EmpressShader : ChromaShader
	{
		// Token: 0x0600420F RID: 16911 RVA: 0x005F39AC File Offset: 0x005F1BAC
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		}, IsTransparent = false)]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			float num = time * 2f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float staticNoise = NoiseHelper.GetStaticNoise(gridPositionOfIndex.X);
				float num5 = MathHelper.Max(0f, (float)Math.Cos((double)((staticNoise + num) * 6.2831855f * 0.2f)));
				Vector4 value = Color.Lerp(Color.Black, Color.Indigo, 0.5f).ToVector4();
				float num2 = Math.Max(0f, (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 2f + canvasPositionOfIndex.X * 1f)));
				num2 = 0f;
				value = Vector4.Lerp(value, new Vector4(1f, 0.1f, 0.1f, 1f), num2);
				float num3 = (num5 + canvasPositionOfIndex.X + canvasPositionOfIndex.Y) % 1f;
				if (num3 > 0f)
				{
					int num4 = (gridPositionOfIndex.X + gridPositionOfIndex.Y) % EmpressShader._colors.Length;
					if (num4 < 0)
					{
						num4 += EmpressShader._colors.Length;
					}
					Vector4 value2 = Main.hslToRgb(((canvasPositionOfIndex.X + canvasPositionOfIndex.Y) * 0.15f + time * 0.1f) % 1f, 1f, 0.5f, byte.MaxValue).ToVector4();
					value = Vector4.Lerp(value, value2, num3);
				}
				fragment.SetColor(i, value);
			}
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x005F3B2C File Offset: 0x005F1D2C
		private static void RedsVersion(Fragment fragment, float time)
		{
			time *= 3f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 7f + time * 0.4f) % 7f - canvasPositionOfIndex.Y;
				Vector4 vector = default(Vector4);
				if (num > 0f)
				{
					float amount = Math.Max(0f, 1.4f - num);
					if (num < 0.4f)
					{
						amount = num / 0.4f;
					}
					int num2 = (gridPositionOfIndex.X + EmpressShader._colors.Length + (int)(time / 6f)) % EmpressShader._colors.Length;
					vector = Vector4.Lerp(vector, EmpressShader._colors[num2], amount);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04005947 RID: 22855
		private static readonly Vector4[] _colors = new Vector4[]
		{
			new Vector4(1f, 0.1f, 0.1f, 1f),
			new Vector4(1f, 0.5f, 0.1f, 1f),
			new Vector4(1f, 1f, 0.1f, 1f),
			new Vector4(0.5f, 1f, 0.1f, 1f),
			new Vector4(0.1f, 1f, 0.1f, 1f),
			new Vector4(0.1f, 1f, 0.5f, 1f),
			new Vector4(0.1f, 1f, 1f, 1f),
			new Vector4(0.1f, 0.5f, 1f, 1f),
			new Vector4(0.1f, 0.1f, 1f, 1f),
			new Vector4(0.5f, 0.1f, 1f, 1f),
			new Vector4(1f, 0.1f, 1f, 1f),
			new Vector4(1f, 0.1f, 0.5f, 1f)
		};
	}
}
