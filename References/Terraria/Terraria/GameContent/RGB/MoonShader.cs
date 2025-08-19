using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200024D RID: 589
	public class MoonShader : ChromaShader
	{
		// Token: 0x06001F47 RID: 8007 RVA: 0x00512114 File Offset: 0x00510314
		public MoonShader(Color skyColor, Color moonRingColor, Color moonCoreColor) : this(skyColor, moonRingColor, moonCoreColor, Color.White)
		{
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00512124 File Offset: 0x00510324
		public MoonShader(Color skyColor, Color moonColor) : this(skyColor, moonColor, moonColor)
		{
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0051212F File Offset: 0x0051032F
		public MoonShader(Color skyColor, Color moonRingColor, Color moonCoreColor, Color cloudColor)
		{
			this._skyColor = skyColor.ToVector4();
			this._moonRingColor = moonRingColor.ToVector4();
			this._moonCoreColor = moonCoreColor.ToVector4();
			this._cloudColor = cloudColor.ToVector4();
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0051216B File Offset: 0x0051036B
		public override void Update(float elapsedTime)
		{
			if (Main.dayTime)
			{
				this._progress = (float)(Main.time / 54000.0);
				return;
			}
			this._progress = (float)(Main.time / 32400.0);
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x005121A4 File Offset: 0x005103A4
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = NoiseHelper.GetDynamicNoise(fragment.GetCanvasPositionOfIndex(i) * new Vector2(0.1f, 0.5f) + new Vector2(time * 0.02f, 0f), time / 40f);
				num = (float)Math.Sqrt((double)Math.Max(0f, 1f - 2f * num));
				Vector4 vector = Vector4.Lerp(this._skyColor, this._cloudColor, num * 0.1f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x00512248 File Offset: 0x00510448
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			if (device.Type != null && device.Type != 6)
			{
				this.ProcessLowDetail(device, fragment, quality, time);
				return;
			}
			Vector2 value = new Vector2(2f, 0.5f);
			Vector2 value2 = new Vector2(2.5f, 1f);
			float num = this._progress * 3.1415927f + 3.1415927f;
			Vector2 value3 = new Vector2((float)Math.Cos((double)num), (float)Math.Sin((double)num)) * value2 + value;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num2 = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * new Vector2(0.1f, 0.5f) + new Vector2(time * 0.02f, 0f), time / 40f);
				num2 = (float)Math.Sqrt((double)Math.Max(0f, 1f - 2f * num2));
				float num3 = (canvasPositionOfIndex - value3).Length();
				Vector4 vector = Vector4.Lerp(this._skyColor, this._cloudColor, num2 * 0.15f);
				if (num3 < 0.8f)
				{
					vector = Vector4.Lerp(this._moonRingColor, this._moonCoreColor, Math.Min(0.1f, 0.8f - num3) / 0.1f);
				}
				else if (num3 < 1f)
				{
					vector = Vector4.Lerp(vector, this._moonRingColor, Math.Min(0.2f, 1f - num3) / 0.2f);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004656 RID: 18006
		private readonly Vector4 _moonCoreColor;

		// Token: 0x04004657 RID: 18007
		private readonly Vector4 _moonRingColor;

		// Token: 0x04004658 RID: 18008
		private readonly Vector4 _skyColor;

		// Token: 0x04004659 RID: 18009
		private readonly Vector4 _cloudColor;

		// Token: 0x0400465A RID: 18010
		private float _progress;
	}
}
