using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200023D RID: 573
	public class UnderworldShader : ChromaShader
	{
		// Token: 0x06001F15 RID: 7957 RVA: 0x005109A0 File Offset: 0x0050EBA0
		public UnderworldShader(Color backColor, Color frontColor, float speed)
		{
			this._backColor = backColor.ToVector4();
			this._frontColor = frontColor.ToVector4();
			this._speed = speed;
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x005109CC File Offset: 0x0050EBCC
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

		// Token: 0x06001F17 RID: 7959 RVA: 0x00510A30 File Offset: 0x0050EC30
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float dynamicNoise = NoiseHelper.GetDynamicNoise(fragment.GetCanvasPositionOfIndex(i) * 0.5f, time * this._speed / 3f);
				Vector4 vector = Vector4.Lerp(this._backColor, this._frontColor, dynamicNoise);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004625 RID: 17957
		private readonly Vector4 _backColor;

		// Token: 0x04004626 RID: 17958
		private readonly Vector4 _frontColor;

		// Token: 0x04004627 RID: 17959
		private readonly float _speed;
	}
}
