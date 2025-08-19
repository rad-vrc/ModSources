using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.GameContent.Animations
{
	// Token: 0x020006B3 RID: 1715
	public class StardewValleyAnimation
	{
		// Token: 0x0600488B RID: 18571 RVA: 0x00649B9F File Offset: 0x00647D9F
		public StardewValleyAnimation()
		{
			this.ComposeAnimation();
		}

		// Token: 0x0600488C RID: 18572 RVA: 0x00649BB8 File Offset: 0x00647DB8
		private void ComposeAnimation()
		{
			Asset<Texture2D> asset = TextureAssets.Extra[247];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData data = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 0.5f), 1f, 0, 0f);
			int targetTime = 128;
			int num = 60;
			int num2 = 360;
			int duration = 60;
			int num3 = 4;
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> item = new Segments.SpriteSegment(asset, targetTime, data, Vector2.Zero).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(new Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction(this.GetMatrixForAnimation), "StardewValleyFade", 8, num3).WithPanX(new Segments.Panning
			{
				Delay = 128f,
				Duration = (float)(num2 - 120 + num - 60),
				AmountOverTime = 0.55f,
				StartAmount = -0.4f
			}).WithPanY(new Segments.Panning
			{
				StartAmount = 0f
			})).Then(new Actions.Sprites.OutCircleScale(Vector2.Zero)).With(new Actions.Sprites.OutCircleScale(Vector2.One, num)).Then(new Actions.Sprites.Wait(num2)).Then(new Actions.Sprites.OutCircleScale(Vector2.Zero, duration));
			this._segments.Add(item);
			Asset<Texture2D> asset2 = TextureAssets.Extra[249];
			Rectangle rectangle2 = asset2.Frame(1, 8, 0, 0, 0, 0);
			DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() * new Vector2(0.5f, 0.5f), 1f, 0, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> item2 = new Segments.SpriteSegment(asset2, targetTime, data2, Vector2.Zero).Then(new Actions.Sprites.OutCircleScale(Vector2.Zero)).With(new Actions.Sprites.OutCircleScale(Vector2.One, num)).With(new Actions.Sprites.SetFrameSequence(100, new Point[]
			{
				new Point(0, 0),
				new Point(0, 1),
				new Point(0, 2),
				new Point(0, 3),
				new Point(0, 4),
				new Point(0, 5),
				new Point(0, 6),
				new Point(0, 7)
			}, num3, 0, 0)).Then(new Actions.Sprites.Wait(num2)).Then(new Actions.Sprites.OutCircleScale(Vector2.Zero, duration));
			this._segments.Add(item2);
		}

		// Token: 0x0600488D RID: 18573 RVA: 0x00649E5C File Offset: 0x0064805C
		private Matrix GetMatrixForAnimation()
		{
			return Main.Transform;
		}

		// Token: 0x0600488E RID: 18574 RVA: 0x00649E64 File Offset: 0x00648064
		public void Draw(SpriteBatch spriteBatch, int timeInAnimation, Vector2 positionInScreen)
		{
			GameAnimationSegment info = new GameAnimationSegment
			{
				SpriteBatch = spriteBatch,
				AnchorPositionOnScreen = positionInScreen,
				TimeInAnimation = timeInAnimation,
				DisplayOpacity = 1f
			};
			for (int i = 0; i < this._segments.Count; i++)
			{
				this._segments[i].Draw(ref info);
			}
		}

		// Token: 0x04005C48 RID: 23624
		private List<IAnimationSegment> _segments = new List<IAnimationSegment>();
	}
}
