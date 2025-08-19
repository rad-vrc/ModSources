using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200057B RID: 1403
	public class CavernShader : ChromaShader
	{
		// Token: 0x060041EE RID: 16878 RVA: 0x005F2A9F File Offset: 0x005F0C9F
		public CavernShader(Color backColor, Color frontColor, float speed)
		{
			this._backColor = backColor.ToVector4();
			this._frontColor = frontColor.ToVector4();
			this._speed = speed;
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x005F2AC8 File Offset: 0x005F0CC8
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 color = Vector4.Lerp(this._backColor, this._frontColor, (float)Math.Sin((double)(time * this._speed + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x005F2B2C File Offset: 0x005F0D2C
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			time *= this._speed * 0.5f;
			float num = time % 1f;
			bool flag = time % 2f > 1f;
			Vector4 vector = flag ? this._frontColor : this._backColor;
			Vector4 value = flag ? this._backColor : this._frontColor;
			num *= 1.2f;
			for (int i = 0; i < fragment.Count; i++)
			{
				float staticNoise = NoiseHelper.GetStaticNoise(fragment.GetCanvasPositionOfIndex(i) * 0.5f + new Vector2(0f, time * 0.5f));
				Vector4 vector2 = vector;
				staticNoise += num;
				if (staticNoise > 0.999f)
				{
					float amount = MathHelper.Clamp((staticNoise - 0.999f) / 0.2f, 0f, 1f);
					vector2 = Vector4.Lerp(vector2, value, amount);
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x04005929 RID: 22825
		private readonly Vector4 _backColor;

		// Token: 0x0400592A RID: 22826
		private readonly Vector4 _frontColor;

		// Token: 0x0400592B RID: 22827
		private readonly float _speed;
	}
}
