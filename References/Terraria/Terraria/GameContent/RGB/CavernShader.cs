using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000241 RID: 577
	public class CavernShader : ChromaShader
	{
		// Token: 0x06001F20 RID: 7968 RVA: 0x00510DE5 File Offset: 0x0050EFE5
		public CavernShader(Color backColor, Color frontColor, float speed)
		{
			this._backColor = backColor.ToVector4();
			this._frontColor = frontColor.ToVector4();
			this._speed = speed;
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x00510E10 File Offset: 0x0050F010
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._backColor, this._frontColor, (float)Math.Sin((double)(time * this._speed + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00510E74 File Offset: 0x0050F074
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
				float num2 = NoiseHelper.GetStaticNoise(fragment.GetCanvasPositionOfIndex(i) * 0.5f + new Vector2(0f, time * 0.5f));
				Vector4 vector2 = vector;
				num2 += num;
				if (num2 > 0.999f)
				{
					float amount = MathHelper.Clamp((num2 - 0.999f) / 0.2f, 0f, 1f);
					vector2 = Vector4.Lerp(vector2, value, amount);
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x04004631 RID: 17969
		private readonly Vector4 _backColor;

		// Token: 0x04004632 RID: 17970
		private readonly Vector4 _frontColor;

		// Token: 0x04004633 RID: 17971
		private readonly float _speed;
	}
}
