using System;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200024E RID: 590
	internal class UIMessageBox : UIPanel
	{
		// Token: 0x060029C6 RID: 10694 RVA: 0x005149E3 File Offset: 0x00512BE3
		public UIMessageBox(string text)
		{
			this._text = text;
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x005149F2 File Offset: 0x00512BF2
		public void SetText(string text)
		{
			this._text = text;
			UIText textElement = this._textElement;
			if (textElement != null)
			{
				textElement.SetText(this._text);
			}
			this.ResetScrollbar();
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x00514A18 File Offset: 0x00512C18
		private void ResetScrollbar()
		{
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition = 0f;
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x00514A34 File Offset: 0x00512C34
		public override void OnInitialize()
		{
			UIList uilist = new UIList();
			uilist.Left = StyleDimension.Empty;
			uilist.Top = StyleDimension.Empty;
			uilist.Width = StyleDimension.Fill;
			uilist.Height = StyleDimension.Fill;
			UIList element = uilist;
			this._list = uilist;
			base.Append(element);
			this._list.SetScrollbar(this._scrollbar);
			UIList list = this._list;
			UIText uitext = new UIText(this._text, 1f, false);
			uitext.Width = StyleDimension.Fill;
			uitext.IsWrapped = true;
			uitext.MinWidth = StyleDimension.Empty;
			uitext.TextOriginX = 0f;
			UIText item = uitext;
			this._textElement = uitext;
			list.Add(item);
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x00514ADF File Offset: 0x00512CDF
		public void SetScrollbar(UIScrollbar scrollbar)
		{
			this._scrollbar = scrollbar;
		}

		// Token: 0x04001A8C RID: 6796
		private string _text;

		// Token: 0x04001A8D RID: 6797
		private UIScrollbar _scrollbar;

		// Token: 0x04001A8E RID: 6798
		private UIList _list;

		// Token: 0x04001A8F RID: 6799
		private UIText _textElement;
	}
}
