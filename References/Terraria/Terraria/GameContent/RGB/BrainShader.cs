using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000228 RID: 552
	public class BrainShader : ChromaShader
	{
		// Token: 0x06001ED9 RID: 7897 RVA: 0x0050E245 File Offset: 0x0050C445
		public BrainShader(Color brainColor, Color veinColor)
		{
			this._brainColor = brainColor.ToVector4();
			this._veinColor = veinColor.ToVector4();
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0050E268 File Offset: 0x0050C468
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector = Vector4.Lerp(this._brainColor, this._veinColor, Math.Max(0f, (float)Math.Sin((double)(time * 3f))));
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x0050E2BC File Offset: 0x0050C4BC
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			new Vector2(1.6f, 0.5f);
			Vector4 value = Vector4.Lerp(this._brainColor, this._veinColor, Math.Max(0f, (float)Math.Sin((double)(time * 3f))) * 0.5f + 0.5f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = this._brainColor;
				float num = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.15f + new Vector2(time * 0.002f), time * 0.03f);
				num = (float)Math.Sin((double)(num * 10f)) * 0.5f + 0.5f;
				num = Math.Max(0f, 1f - 5f * num);
				vector = Vector4.Lerp(vector, value, num);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x040045E4 RID: 17892
		private readonly Vector4 _brainColor;

		// Token: 0x040045E5 RID: 17893
		private readonly Vector4 _veinColor;
	}
}
