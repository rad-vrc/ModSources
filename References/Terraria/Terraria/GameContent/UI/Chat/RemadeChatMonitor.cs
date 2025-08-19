using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x0200039B RID: 923
	public class RemadeChatMonitor : IChatMonitor
	{
		// Token: 0x060029A1 RID: 10657 RVA: 0x0059434C File Offset: 0x0059254C
		public RemadeChatMonitor()
		{
			this._showCount = 10;
			this._startChatLine = 0;
			this._messages = new List<ChatMessageContainer>();
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x0059436E File Offset: 0x0059256E
		public void NewText(string newText, byte R = 255, byte G = 255, byte B = 255)
		{
			this.AddNewMessage(newText, new Color((int)R, (int)G, (int)B), -1);
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x00594381 File Offset: 0x00592581
		public void NewTextMultiline(string text, bool force = false, Color c = default(Color), int WidthLimit = -1)
		{
			this.AddNewMessage(text, c, WidthLimit);
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x00594390 File Offset: 0x00592590
		public void AddNewMessage(string text, Color color, int widthLimitInPixels = -1)
		{
			ChatMessageContainer chatMessageContainer = new ChatMessageContainer();
			chatMessageContainer.SetContents(text, color, widthLimitInPixels);
			this._messages.Insert(0, chatMessageContainer);
			while (this._messages.Count > 500)
			{
				this._messages.RemoveAt(this._messages.Count - 1);
			}
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x005943E8 File Offset: 0x005925E8
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
			int num8 = -1;
			while (num5 < this._showCount && num2 < this._messages.Count)
			{
				ChatMessageContainer chatMessageContainer = this._messages[num2];
				if (!chatMessageContainer.Prepared || !(drawingPlayerChat | chatMessageContainer.CanBeShownWhenChatIsClosed))
				{
					break;
				}
				TextSnippet[] snippetWithInversedIndex = chatMessageContainer.GetSnippetWithInversedIndex(num3);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, snippetWithInversedIndex, new Vector2(88f, (float)(Main.screenHeight - 30 - 28 - num5 * 21)), 0f, Vector2.Zero, Vector2.One, out num8, -1f, 2f);
				if (num8 >= 0)
				{
					num7 = new int?(num8);
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

		// Token: 0x060029A6 RID: 10662 RVA: 0x00594591 File Offset: 0x00592791
		public void Clear()
		{
			this._messages.Clear();
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x005945A0 File Offset: 0x005927A0
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

		// Token: 0x060029A8 RID: 10664 RVA: 0x0059460A File Offset: 0x0059280A
		public void Offset(int linesOffset)
		{
			this._startChatLine += linesOffset;
			this.ClampMessageIndex();
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x00594620 File Offset: 0x00592820
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

		// Token: 0x060029AA RID: 10666 RVA: 0x005946CC File Offset: 0x005928CC
		public void ResetOffset()
		{
			this._startChatLine = 0;
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x005946D5 File Offset: 0x005928D5
		public void OnResolutionChange()
		{
			this._recalculateOnNextUpdate = true;
		}

		// Token: 0x04004C9F RID: 19615
		private const int MaxMessages = 500;

		// Token: 0x04004CA0 RID: 19616
		private int _showCount;

		// Token: 0x04004CA1 RID: 19617
		private int _startChatLine;

		// Token: 0x04004CA2 RID: 19618
		private List<ChatMessageContainer> _messages;

		// Token: 0x04004CA3 RID: 19619
		private bool _recalculateOnNextUpdate;
	}
}
