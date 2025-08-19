using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000240 RID: 576
	public class BlizzardShader : ChromaShader
	{
		// Token: 0x06001F1E RID: 7966 RVA: 0x00510CF0 File Offset: 0x0050EEF0
		public BlizzardShader(Vector4 frontColor, Vector4 backColor, float panSpeedX, float panSpeedY)
		{
			this._frontColor = frontColor;
			this._backColor = backColor;
			this._timeScaleX = panSpeedX;
			this._timeScaleY = panSpeedY;
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x00510D60 File Offset: 0x0050EF60
		[RgbProcessor(new EffectDetailLevel[]
		{
			0,
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			if (quality == null)
			{
				time *= 0.25f;
			}
			for (int i = 0; i < fragment.Count; i++)
			{
				float staticNoise = NoiseHelper.GetStaticNoise(fragment.GetCanvasPositionOfIndex(i) * new Vector2(0.2f, 0.4f) + new Vector2(time * this._timeScaleX, time * this._timeScaleY));
				Vector4 vector = Vector4.Lerp(this._backColor, this._frontColor, staticNoise * staticNoise);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0400462D RID: 17965
		private readonly Vector4 _backColor = new Vector4(0.1f, 0.1f, 0.3f, 1f);

		// Token: 0x0400462E RID: 17966
		private readonly Vector4 _frontColor = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x0400462F RID: 17967
		private readonly float _timeScaleX;

		// Token: 0x04004630 RID: 17968
		private readonly float _timeScaleY;
	}
}
