using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020004F9 RID: 1273
	public class EmoteButton : UIElement
	{
		// Token: 0x06003D9D RID: 15773 RVA: 0x005CB66C File Offset: 0x005C986C
		public EmoteButton(int emoteIndex)
		{
			if (emoteIndex >= EmoteID.Count)
			{
				this._texture = ModContent.Request<Texture2D>(EmoteBubbleLoader.GetEmoteBubble(emoteIndex).Texture, 2);
			}
			else
			{
				this._texture = Main.Assets.Request<Texture2D>("Images/Extra_" + 48.ToString());
			}
			this._bubbleTexture = Main.Assets.Request<Texture2D>("Images/Extra_" + 48.ToString());
			this._textureBorder = Main.Assets.Request<Texture2D>("Images/UI/EmoteBubbleBorder");
			this._emoteIndex = emoteIndex;
			Rectangle frame = this.GetFrame();
			this.Width.Set((float)frame.Width, 0f);
			this.Height.Set((float)frame.Height, 0f);
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x005CB73C File Offset: 0x005C993C
		private Rectangle GetFrame()
		{
			int num = (this._frameCounter >= 10) ? 1 : 0;
			if (this._emoteIndex < EmoteID.Count)
			{
				return this._texture.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, this._emoteIndex % 4 * 2 + num, this._emoteIndex / 4 + 1, 0, 0);
			}
			Rectangle? frameInEmoteMenu = EmoteBubbleLoader.GetFrameInEmoteMenu(this._emoteIndex, num, this._frameCounter);
			if (frameInEmoteMenu == null)
			{
				return this._texture.Frame(2, 1, num, 0, 0, 0);
			}
			return frameInEmoteMenu.GetValueOrDefault();
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x005CB7C4 File Offset: 0x005C99C4
		private void UpdateFrame()
		{
			if (this._emoteIndex >= EmoteID.Count && !EmoteBubbleLoader.UpdateFrameInEmoteMenu(this._emoteIndex, ref this._frameCounter))
			{
				return;
			}
			int num = this._frameCounter + 1;
			this._frameCounter = num;
			if (num >= 20)
			{
				this._frameCounter = 0;
			}
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x005CB80E File Offset: 0x005C9A0E
		public override void Update(GameTime gameTime)
		{
			this.UpdateFrame();
			base.Update(gameTime);
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x005CB820 File Offset: 0x005C9A20
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 vector = dimensions.Position() + new Vector2(dimensions.Width, dimensions.Height) / 2f;
			Rectangle frame = this.GetFrame();
			Rectangle value = this._bubbleTexture.Frame(8, 39, 1, 0, 0, 0);
			Vector2 origin = frame.Size() / 2f;
			Color white = Color.White;
			Color color = Color.Black;
			if (this._hovered)
			{
				color = Main.OurFavoriteColor;
			}
			if (EmoteBubbleLoader.PreDrawInEmoteMenu(this._emoteIndex, spriteBatch, this, vector, frame, origin))
			{
				spriteBatch.Draw(this._bubbleTexture.Value, vector, new Rectangle?(value), white, 0f, origin, 1f, 0, 0f);
				spriteBatch.Draw(this._texture.Value, vector, new Rectangle?(frame), white, 0f, origin, 1f, 0, 0f);
				spriteBatch.Draw(this._textureBorder.Value, vector - Vector2.One * 2f, null, color, 0f, origin, 1f, 0, 0f);
			}
			EmoteBubbleLoader.PostDrawInEmoteMenu(this._emoteIndex, spriteBatch, this, vector, frame, origin);
			if (this._hovered)
			{
				if (this._emoteIndex >= EmoteID.Count)
				{
					Main instance = Main.instance;
					string str = "/";
					LocalizedText emojiName = Lang.GetEmojiName(this._emoteIndex);
					instance.MouseText(str + ((emojiName != null) ? emojiName.ToString() : null), 0, 0, -1, -1, -1, -1, 0);
					return;
				}
				string name = EmoteID.Search.GetName(this._emoteIndex);
				string cursorText = "/" + Language.GetTextValue("EmojiName." + name);
				Main.instance.MouseText(cursorText, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x005CB9F5 File Offset: 0x005C9BF5
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._hovered = true;
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x005CBA1A File Offset: 0x005C9C1A
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x005CBA2A File Offset: 0x005C9C2A
		public override void LeftClick(UIMouseEvent evt)
		{
			base.LeftClick(evt);
			EmoteBubble.MakeLocalPlayerEmote(this._emoteIndex);
			IngameFancyUI.Close();
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06003DA5 RID: 15781 RVA: 0x005CBA43 File Offset: 0x005C9C43
		public ref int FrameCounter
		{
			get
			{
				return ref this._frameCounter;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06003DA6 RID: 15782 RVA: 0x005CBA4B File Offset: 0x005C9C4B
		public bool Hovered
		{
			get
			{
				return this._hovered;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06003DA7 RID: 15783 RVA: 0x005CBA53 File Offset: 0x005C9C53
		public Asset<Texture2D> BubbleTexture
		{
			get
			{
				return this._bubbleTexture;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06003DA8 RID: 15784 RVA: 0x005CBA5B File Offset: 0x005C9C5B
		public Asset<Texture2D> EmoteTexture
		{
			get
			{
				return this._texture;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x005CBA63 File Offset: 0x005C9C63
		public Asset<Texture2D> BorderTexture
		{
			get
			{
				return this._textureBorder;
			}
		}

		// Token: 0x04005662 RID: 22114
		private Asset<Texture2D> _bubbleTexture;

		// Token: 0x04005663 RID: 22115
		private Asset<Texture2D> _texture;

		// Token: 0x04005664 RID: 22116
		private Asset<Texture2D> _textureBorder;

		// Token: 0x04005665 RID: 22117
		private int _emoteIndex;

		// Token: 0x04005666 RID: 22118
		private bool _hovered;

		// Token: 0x04005667 RID: 22119
		private int _frameCounter;
	}
}
