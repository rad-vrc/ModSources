using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000584 RID: 1412
	public class DrowningShader : ChromaShader
	{
		// Token: 0x06004206 RID: 16902 RVA: 0x005F3654 File Offset: 0x005F1854
		public override void Update(float elapsedTime)
		{
			Player player = Main.player[Main.myPlayer];
			this._breath = (float)(player.breath * player.breathCDMax - player.breathCD) / (float)(player.breathMax * player.breathCDMax);
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x005F3698 File Offset: 0x005F1898
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		}, IsTransparent = true)]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.GetCanvasPositionOfIndex(i);
				Vector4 color;
				color..ctor(0f, 0f, 1f, 1f - this._breath);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x005F36E8 File Offset: 0x005F18E8
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		}, IsTransparent = true)]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			float num = this._breath * 1.2f - 0.1f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 color = Vector4.Zero;
				if (canvasPositionOfIndex.Y > num)
				{
					color..ctor(0f, 0f, 1f, MathHelper.Clamp((canvasPositionOfIndex.Y - num) * 5f, 0f, 1f));
				}
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x04005941 RID: 22849
		private float _breath = 1f;
	}
}
