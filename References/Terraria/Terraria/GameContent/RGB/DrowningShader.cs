using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000253 RID: 595
	public class DrowningShader : ChromaShader
	{
		// Token: 0x06001F5A RID: 8026 RVA: 0x00512968 File Offset: 0x00510B68
		public override void Update(float elapsedTime)
		{
			Player player = Main.player[Main.myPlayer];
			this._breath = (float)(player.breath * player.breathCDMax - player.breathCD) / (float)(player.breathMax * player.breathCDMax);
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x005129AC File Offset: 0x00510BAC
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		}, IsTransparent = true)]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = new Vector4(0f, 0f, 1f, 1f - this._breath);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x005129FC File Offset: 0x00510BFC
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
				Vector4 zero = Vector4.Zero;
				if (canvasPositionOfIndex.Y > num)
				{
					zero = new Vector4(0f, 0f, 1f, MathHelper.Clamp((canvasPositionOfIndex.Y - num) * 5f, 0f, 1f));
				}
				fragment.SetColor(i, zero);
			}
		}

		// Token: 0x04004665 RID: 18021
		private float _breath = 1f;
	}
}
