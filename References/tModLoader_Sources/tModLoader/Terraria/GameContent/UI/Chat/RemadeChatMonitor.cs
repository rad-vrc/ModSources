using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000545 RID: 1349
	public class RemadeChatMonitor : IChatMonitor
	{
		// Token: 0x0600400C RID: 16396 RVA: 0x005DDD34 File Offset: 0x005DBF34
		public RemadeChatMonitor()
		{
			this._showCount = 10;
			this._startChatLine = 0;
			this._messages = new List<ChatMessageContainer>();
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x005DDD56 File Offset: 0x005DBF56
		public void NewText(string newText, byte R = 255, byte G = 255, byte B = 255)
		{
			this.AddNewMessage(newText, new Color((int)R, (int)G, (int)B), -1);
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x005DDD69 File Offset: 0x005DBF69
		public void NewTextMultiline(string text, bool force = false, Color c = default(Color), int WidthLimit = -1)
		{
			this.AddNewMessage(text, c, WidthLimit);
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x005DDD78 File Offset: 0x005DBF78
		public void AddNewMessage(string text, Color color, int widthLimitInPixels = -1)
		{
			if (Main.dedServ)
			{
				return;
			}
			ChatMessageContainer chatMessageContainer = new ChatMessageContainer();
			chatMessageContainer.SetContents(text, color, widthLimitInPixels);
			this._messages.Insert(0, chatMessageContainer);
			while (this._messages.Count > 500)
			{
				this._messages.RemoveAt(this._messages.Count - 1);
			}
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x005DDDD8 File Offset: 0x005DBFD8
		public void DrawChat(bool drawingPlayerChat)
		{
			int num = this._startChatLine;
			int num2 = 0;
			int num3 = 0;
			while (num > 0 && num2 < this._messages.Count)
			{
				int num4 = Math.Min(num, this._messages[num2].LineCount);
				num -= num4;
				num3 += num4;
				if (num3 == this._messages[num2].LineCount)
				{
					num3 = 0;
					num2++;
				}
			}
			int num5 = 0;
			int? num6 = null;
			int snippetIndex = -1;
			int? num7 = null;
			int hoveredSnippet = -1;
			while (num5 < this._showCount && num2 < this._messages.Count)
			{
				ChatMessageContainer chatMessageContainer = this._messages[num2];
				if (!chatMessageContainer.Prepared || !(drawingPlayerChat | chatMessageContainer.CanBeShownWhenChatIsClosed))
				{
					break;
				}
				TextSnippet[] snippetWithInversedIndex = chatMessageContainer.GetSnippetWithInversedIndex(num3);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, snippetWithInversedIndex, new Vector2(88f, (float)(Main.screenHeight - 30 - 28 - num5 * 21)), 0f, Vector2.Zero, Vector2.One, out hoveredSnippet, -1f, 2f);
				if (hoveredSnippet >= 0)
				{
					num7 = new int?(hoveredSnippet);
					num6 = new int?(num2);
					snippetIndex = num3;
				}
				num5++;
				num3++;
				if (num3 >= chatMessageContainer.LineCount)
				{
					num3 = 0;
					num2++;
				}
			}
			if (num6 != null && num7 != null)
			{
				TextSnippet[] snippetWithInversedIndex2 = this._messages[num6.Value].GetSnippetWithInversedIndex(snippetIndex);
				snippetWithInversedIndex2[num7.Value].OnHover();
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					snippetWithInversedIndex2[num7.Value].OnClick();
				}
			}
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x005DDF81 File Offset: 0x005DC181
		public void Clear()
		{
			this._messages.Clear();
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x005DDF90 File Offset: 0x005DC190
		public void Update()
		{
			if (this._recalculateOnNextUpdate)
			{
				this._recalculateOnNextUpdate = false;
				for (int i = 0; i < this._messages.Count; i++)
				{
					this._messages[i].MarkToNeedRefresh();
				}
			}
			for (int j = 0; j < this._messages.Count; j++)
			{
				this._messages[j].Update();
			}
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x005DDFFA File Offset: 0x005DC1FA
		public void Offset(int linesOffset)
		{
			this._startChatLine += linesOffset;
			this.ClampMessageIndex();
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x005DE010 File Offset: 0x005DC210
		private void ClampMessageIndex()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = this._startChatLine + this._showCount;
			while (num < num4 && num2 < this._messages.Count)
			{
				int num5 = Math.Min(num4 - num, this._messages[num2].LineCount);
				num += num5;
				if (num < num4)
				{
					num2++;
					num3 = 0;
				}
				else
				{
					num3 = num5;
				}
			}
			int num6 = this._showCount;
			while (num6 > 0 && num > 0)
			{
				num3--;
				num6--;
				num--;
				if (num3 < 0)
				{
					num2--;
					if (num2 == -1)
					{
						break;
					}
					num3 = this._messages[num2].LineCount - 1;
				}
			}
			this._startChatLine = num;
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x005DE0BC File Offset: 0x005DC2BC
		public void ResetOffset()
		{
			this._startChatLine = 0;
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x005DE0C5 File Offset: 0x005DC2C5
		public void OnResolutionChange()
		{
			this._recalculateOnNextUpdate = true;
		}

		// Token: 0x0400583A RID: 22586
		private const int MaxMessages = 500;

		// Token: 0x0400583B RID: 22587
		private int _showCount;

		// Token: 0x0400583C RID: 22588
		private int _startChatLine;

		// Token: 0x0400583D RID: 22589
		private List<ChatMessageContainer> _messages;

		// Token: 0x0400583E RID: 22590
		private bool _recalculateOnNextUpdate;
	}
}
