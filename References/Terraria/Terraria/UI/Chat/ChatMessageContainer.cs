using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent;

namespace Terraria.UI.Chat
{
	// Token: 0x0200009C RID: 156
	public class ChatMessageContainer
	{
		// Token: 0x06001312 RID: 4882 RVA: 0x0049D4AC File Offset: 0x0049B6AC
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

		// Token: 0x06001313 RID: 4883 RVA: 0x0049D4E5 File Offset: 0x0049B6E5
		public void MarkToNeedRefresh()
		{
			this._prepared = false;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0049D4EE File Offset: 0x0049B6EE
		public void Update()
		{
			if (this._timeLeft > 0)
			{
				this._timeLeft--;
			}
			this.Refresh();
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0049D510 File Offset: 0x0049B710
		public TextSnippet[] GetSnippetWithInversedIndex(int snippetIndex)
		{
			int index = this._parsedText.Count - 1 - snippetIndex;
			return this._parsedText[index];
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0049D539 File Offset: 0x0049B739
		public int LineCount
		{
			get
			{
				return this._parsedText.Count;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0049D546 File Offset: 0x0049B746
		public bool CanBeShownWhenChatIsClosed
		{
			get
			{
				return this._timeLeft > 0;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0049D551 File Offset: 0x0049B751
		public bool Prepared
		{
			get
			{
				return this._prepared;
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0049D55C File Offset: 0x0049B75C
		public void Refresh()
		{
			if (this._prepared)
			{
				return;
			}
			this._prepared = true;
			int num = this._widthLimitInPixels;
			if (num == -1)
			{
				num = Main.screenWidth - 320;
			}
			List<List<TextSnippet>> list = Utils.WordwrapStringSmart(this.OriginalText, this._color, FontAssets.MouseText.Value, num, 10);
			this._parsedText.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				this._parsedText.Add(list[i].ToArray());
			}
		}

		// Token: 0x0400106E RID: 4206
		public string OriginalText;

		// Token: 0x0400106F RID: 4207
		private bool _prepared;

		// Token: 0x04001070 RID: 4208
		private List<TextSnippet[]> _parsedText;

		// Token: 0x04001071 RID: 4209
		private Color _color;

		// Token: 0x04001072 RID: 4210
		private int _widthLimitInPixels;

		// Token: 0x04001073 RID: 4211
		private int _timeLeft;
	}
}
