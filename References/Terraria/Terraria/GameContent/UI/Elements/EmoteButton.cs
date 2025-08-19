using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000372 RID: 882
	public class EmoteButton : UIElement
	{
		// Token: 0x0600285C RID: 10332 RVA: 0x0058A368 File Offset: 0x00588568
		public EmoteButton(int emoteIndex)
		{
			this._texture = Main.Assets.Request<Texture2D>("Images/Extra_" + 48, 1);
			this._textureBorder = Main.Assets.Request<Texture2D>("Images/UI/EmoteBubbleBorder", 1);
			this._emoteIndex = emoteIndex;
			Rectangle frame = this.GetFrame();
			this.Width.Set((float)frame.Width, 0f);
			this.Height.Set((float)frame.Height, 0f);
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x0058A3F0 File Offset: 0x005885F0
		private Rectangle GetFrame()
		{
			int num = (this._frameCounter >= 10) ? 1 : 0;
			return this._texture.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, this._emoteIndex % 4 * 2 + num, this._emoteIndex / 4 + 1, 0, 0);
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x0058A438 File Offset: 0x00588638
		private void UpdateFrame()
		{
			int num = this._frameCounter + 1;
			this._frameCounter = num;
			if (num >= 20)
			{
				this._frameCounter = 0;
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x0058A461 File Offset: 0x00588661
		public override void Update(GameTime gameTime)
		{
			this.UpdateFrame();
			base.Update(gameTime);
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x0058A470 File Offset: 0x00588670
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 vector = dimensions.Position() + new Vector2(dimensions.Width, dimensions.Height) / 2f;
			Rectangle frame = this.GetFrame();
			Rectangle value = frame;
			value.X = this._texture.Width() / 8;
			value.Y = 0;
			Vector2 origin = frame.Size() / 2f;
			Color white = Color.White;
			Color color = Color.Black;
			if (this._hovered)
			{
				color = Main.OurFavoriteColor;
			}
			spriteBatch.Draw(this._texture.Value, vector, new Rectangle?(value), white, 0f, origin, 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(this._texture.Value, vector, new Rectangle?(frame), white, 0f, origin, 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(this._textureBorder.Value, vector - Vector2.One * 2f, null, color, 0f, origin, 1f, SpriteEffects.None, 0f);
			if (this._hovered)
			{
				string name = EmoteID.Search.GetName(this._emoteIndex);
				string cursorText = "/" + Language.GetTextValue("EmojiName." + name);
				Main.instance.MouseText(cursorText, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x0058A5E6 File Offset: 0x005887E6
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._hovered = true;
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x0058A60B File Offset: 0x0058880B
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x0058A61B File Offset: 0x0058881B
		public override void LeftClick(UIMouseEvent evt)
		{
			base.LeftClick(evt);
			EmoteBubble.MakeLocalPlayerEmote(this._emoteIndex);
			IngameFancyUI.Close();
		}

		// Token: 0x04004B83 RID: 19331
		private Asset<Texture2D> _texture;

		// Token: 0x04004B84 RID: 19332
		private Asset<Texture2D> _textureBorder;

		// Token: 0x04004B85 RID: 19333
		private int _emoteIndex;

		// Token: 0x04004B86 RID: 19334
		private bool _hovered;

		// Token: 0x04004B87 RID: 19335
		private int _frameCounter;
	}
}
