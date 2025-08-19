using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020004FA RID: 1274
	public class EmotesGroupListItem : UIElement
	{
		// Token: 0x06003DAA RID: 15786 RVA: 0x005CBA6C File Offset: 0x005C9C6C
		public EmotesGroupListItem(LocalizedText groupTitle, int groupIndex, int maxEmotesPerRow, params int[] emotes)
		{
			maxEmotesPerRow = 14;
			base.SetPadding(0f);
			this._groupIndex = groupIndex;
			this._maxEmotesPerRow = maxEmotesPerRow;
			this._tempTex = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteInactive");
			int num = emotes.Length / this._maxEmotesPerRow;
			if (emotes.Length % this._maxEmotesPerRow != 0)
			{
				num++;
			}
			this.Height.Set((float)(30 + 36 * num), 0f);
			this.Width.Set(0f, 1f);
			UIElement uIElement = new UIElement
			{
				Height = StyleDimension.FromPixels(30f),
				Width = StyleDimension.FromPixelsAndPercent(-20f, 1f),
				HAlign = 0.5f
			};
			uIElement.SetPadding(0f);
			base.Append(uIElement);
			UIHorizontalSeparator element = new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				VAlign = 1f,
				HAlign = 0.5f,
				Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
			};
			uIElement.Append(element);
			UIText element2 = new UIText(groupTitle, 1f, false)
			{
				VAlign = 1f,
				HAlign = 0.5f,
				Top = StyleDimension.FromPixels(-6f)
			};
			uIElement.Append(element2);
			float num2 = 6f;
			for (int i = 0; i < emotes.Length; i++)
			{
				int emoteIndex = emotes[i];
				int num3 = i / this._maxEmotesPerRow;
				int num4 = i % this._maxEmotesPerRow;
				int num5 = emotes.Length % this._maxEmotesPerRow;
				if (emotes.Length / this._maxEmotesPerRow != num3)
				{
					num5 = this._maxEmotesPerRow;
				}
				if (num5 == 0)
				{
					num5 = this._maxEmotesPerRow;
				}
				float num6 = 36f * ((float)num5 / 2f);
				num6 -= 16f;
				num6 = -16f;
				EmoteButton emoteButton = new EmoteButton(emoteIndex)
				{
					HAlign = 0f,
					VAlign = 0f,
					Top = StyleDimension.FromPixels((float)(30 + num3 * 36) + num2),
					Left = StyleDimension.FromPixels((float)(36 * num4) - num6)
				};
				base.Append(emoteButton);
				emoteButton.SetSnapPoint("Group " + groupIndex.ToString(), i, null, null);
			}
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x005CBCFC File Offset: 0x005C9EFC
		public override int CompareTo(object obj)
		{
			EmotesGroupListItem emotesGroupListItem = obj as EmotesGroupListItem;
			if (emotesGroupListItem != null)
			{
				return this._groupIndex.CompareTo(emotesGroupListItem._groupIndex);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x04005668 RID: 22120
		private const int TITLE_HEIGHT = 20;

		// Token: 0x04005669 RID: 22121
		private const int SEPARATOR_HEIGHT = 10;

		// Token: 0x0400566A RID: 22122
		private const int SIZE_PER_EMOTE = 36;

		// Token: 0x0400566B RID: 22123
		private Asset<Texture2D> _tempTex;

		// Token: 0x0400566C RID: 22124
		private int _groupIndex;

		// Token: 0x0400566D RID: 22125
		private int _maxEmotesPerRow = 10;
	}
}
