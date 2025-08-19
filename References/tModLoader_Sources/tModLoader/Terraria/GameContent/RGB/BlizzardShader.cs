using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000579 RID: 1401
	public class BlizzardShader : ChromaShader
	{
		// Token: 0x060041E9 RID: 16873 RVA: 0x005F284C File Offset: 0x005F0A4C
		public BlizzardShader(Vector4 frontColor, Vector4 backColor, float panSpeedX, float panSpeedY)
		{
			this._frontColor = frontColor;
			this._backColor = backColor;
			this._timeScaleX = panSpeedX;
			this._timeScaleY = panSpeedY;
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x005F28BC File Offset: 0x005F0ABC
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
				Vector4 color = Vector4.Lerp(this._backColor, this._frontColor, staticNoise * staticNoise);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x04005923 RID: 22819
		private readonly Vector4 _backColor = new Vector4(0.1f, 0.1f, 0.3f, 1f);

		// Token: 0x04005924 RID: 22820
		private readonly Vector4 _frontColor = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x04005925 RID: 22821
		private readonly float _timeScaleX;

		// Token: 0x04005926 RID: 22822
		private readonly float _timeScaleY;
	}
}
