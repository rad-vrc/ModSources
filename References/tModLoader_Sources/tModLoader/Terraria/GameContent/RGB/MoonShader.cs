using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000597 RID: 1431
	public class MoonShader : ChromaShader
	{
		// Token: 0x06004240 RID: 16960 RVA: 0x005F58C4 File Offset: 0x005F3AC4
		public MoonShader(Color skyColor, Color moonRingColor, Color moonCoreColor) : this(skyColor, moonRingColor, moonCoreColor, Color.White)
		{
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x005F58D4 File Offset: 0x005F3AD4
		public MoonShader(Color skyColor, Color moonColor) : this(skyColor, moonColor, moonColor)
		{
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x005F58DF File Offset: 0x005F3ADF
		public MoonShader(Color skyColor, Color moonRingColor, Color moonCoreColor, Color cloudColor)
		{
			this._skyColor = skyColor.ToVector4();
			this._moonRingColor = moonRingColor.ToVector4();
			this._moonCoreColor = moonCoreColor.ToVector4();
			this._cloudColor = cloudColor.ToVector4();
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x005F591B File Offset: 0x005F3B1B
		public override void Update(float elapsedTime)
		{
			if (Main.dayTime)
			{
				this._progress = (float)(Main.time / 54000.0);
				return;
			}
			this._progress = (float)(Main.time / 32400.0);
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x005F5954 File Offset: 0x005F3B54
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float dynamicNoise = NoiseHelper.GetDynamicNoise(fragment.GetCanvasPositionOfIndex(i) * new Vector2(0.1f, 0.5f) + new Vector2(time * 0.02f, 0f), time / 40f);
				dynamicNoise = (float)Math.Sqrt((double)Math.Max(0f, 1f - 2f * dynamicNoise));
				Vector4 color = Vector4.Lerp(this._skyColor, this._cloudColor, dynamicNoise * 0.1f);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x005F59F8 File Offset: 0x005F3BF8
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
			Vector2 vector;
			vector..ctor(2f, 0.5f);
			Vector2 vector2;
			vector2..ctor(2.5f, 1f);
			float num = this._progress * 3.1415927f + 3.1415927f;
			Vector2 vector3 = new Vector2((float)Math.Cos((double)num), (float)Math.Sin((double)num)) * vector2 + vector;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float dynamicNoise = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * new Vector2(0.1f, 0.5f) + new Vector2(time * 0.02f, 0f), time / 40f);
				dynamicNoise = (float)Math.Sqrt((double)Math.Max(0f, 1f - 2f * dynamicNoise));
				float num2 = (canvasPositionOfIndex - vector3).Length();
				Vector4 vector4 = Vector4.Lerp(this._skyColor, this._cloudColor, dynamicNoise * 0.15f);
				if (num2 < 0.8f)
				{
					vector4 = Vector4.Lerp(this._moonRingColor, this._moonCoreColor, Math.Min(0.1f, 0.8f - num2) / 0.1f);
				}
				else if (num2 < 1f)
				{
					vector4 = Vector4.Lerp(vector4, this._moonRingColor, Math.Min(0.2f, 1f - num2) / 0.2f);
				}
				fragment.SetColor(i, vector4);
			}
		}

		// Token: 0x0400597B RID: 22907
		private readonly Vector4 _moonCoreColor;

		// Token: 0x0400597C RID: 22908
		private readonly Vector4 _moonRingColor;

		// Token: 0x0400597D RID: 22909
		private readonly Vector4 _skyColor;

		// Token: 0x0400597E RID: 22910
		private readonly Vector4 _cloudColor;

		// Token: 0x0400597F RID: 22911
		private float _progress;
	}
}
