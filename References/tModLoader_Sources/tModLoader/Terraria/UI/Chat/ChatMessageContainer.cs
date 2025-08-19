using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent;

namespace Terraria.UI.Chat
{
	// Token: 0x020000C2 RID: 194
	public class ChatMessageContainer
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x004B4EEA File Offset: 0x004B30EA
		public int LineCount
		{
			get
			{
				return this._parsedText.Count;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x004B4EF7 File Offset: 0x004B30F7
		public bool CanBeShownWhenChatIsClosed
		{
			get
			{
				return this._timeLeft > 0;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x004B4F02 File Offset: 0x004B3102
		public bool Prepared
		{
			get
			{
				return this._prepared;
			}
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x004B4F0A File Offset: 0x004B310A
		public void SetContents(string text, Color color, int widthLimitInPixels)
		{
			this.OriginalText = text;
			this._color = color;
			this._widthLimitInPixels = widthLimitInPixels;
			this.MarkToNeedRefresh();
			this._parsedText = new List<TextSnippet[]>();
			this._timeLeft = 600;
			this.Refresh();
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x004B4F43 File Offset: 0x004B3143
		public void MarkToNeedRefresh()
		{
			this._prepared = false;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x004B4F4C File Offset: 0x004B314C
		public void Update()
		{
			if (this._timeLeft > 0)
			{
				this._timeLeft--;
			}
			this.Refresh();
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x004B4F6C File Offset: 0x004B316C
		public TextSnippet[] GetSnippetWithInversedIndex(int snippetIndex)
		{
			int index = this._parsedText.Count - 1 - snippetIndex;
			return this._parsedText[index];
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x004B4F98 File Offset: 0x004B3198
		public void Refresh()
		{
			if (!this._prepared)
			{
				this._prepared = true;
				int num = this._widthLimitInPixels;
				if (num == -1)
				{
					num = (int)((float)Main.screenWidth / Main.UIScale - 320f);
				}
				List<List<TextSnippet>> list = Utils.WordwrapStringSmart(this.OriginalText, this._color, FontAssets.MouseText.Value, num, 10);
				this._parsedText.Clear();
				for (int i = 0; i < list.Count; i++)
				{
					this._parsedText.Add(list[i].ToArray());
				}
			}
		}

		// Token: 0x040012A3 RID: 4771
		public string OriginalText;

		// Token: 0x040012A4 RID: 4772
		private bool _prepared;

		// Token: 0x040012A5 RID: 4773
		private List<TextSnippet[]> _parsedText;

		// Token: 0x040012A6 RID: 4774
		private Color _color;

		// Token: 0x040012A7 RID: 4775
		private int _widthLimitInPixels;

		// Token: 0x040012A8 RID: 4776
		private int _timeLeft;
	}
}
