using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000244 RID: 580
	internal class UIExpandablePanel : UIPanel
	{
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x00512942 File Offset: 0x00510B42
		// (set) Token: 0x0600297D RID: 10621 RVA: 0x0051294A File Offset: 0x00510B4A
		protected Asset<Texture2D> CollapsedTexture { get; set; } = UICommon.ButtonCollapsedTexture;

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x00512953 File Offset: 0x00510B53
		// (set) Token: 0x0600297F RID: 10623 RVA: 0x0051295B File Offset: 0x00510B5B
		protected Asset<Texture2D> ExpandedTexture { get; set; } = UICommon.ButtonExpandedTexture;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06002980 RID: 10624 RVA: 0x00512964 File Offset: 0x00510B64
		// (remove) Token: 0x06002981 RID: 10625 RVA: 0x0051299C File Offset: 0x00510B9C
		public event Action OnExpanded;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06002982 RID: 10626 RVA: 0x005129D4 File Offset: 0x00510BD4
		// (remove) Token: 0x06002983 RID: 10627 RVA: 0x00512A0C File Offset: 0x00510C0C
		public event Action OnCollapsed;

		// Token: 0x06002984 RID: 10628 RVA: 0x00512A44 File Offset: 0x00510C44
		public UIExpandablePanel()
		{
			this.Width.Set(0f, 1f);
			this.Height.Set(this.defaultHeight, 0f);
			base.SetPadding(6f);
			this.expandButton = new UIHoverImage(this.CollapsedTexture, Language.GetTextValue("tModLoader.ModConfigExpand"));
			this.expandButton.UseTooltipMouseText = true;
			this.expandButton.Top.Set(3f, 0f);
			this.expandButton.Left.Set(-25f, 1f);
			this.expandButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.expanded = !this.expanded;
				this.pendingChanges = true;
			};
			base.Append(this.expandButton);
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x00512B37 File Offset: 0x00510D37
		public void Collapse()
		{
			if (this.expanded)
			{
				this.expanded = false;
				this.pendingChanges = true;
			}
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x00512B50 File Offset: 0x00510D50
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (!this.pendingChanges)
			{
				return;
			}
			this.pendingChanges = false;
			float newHeight = this.defaultHeight;
			if (this.expanded)
			{
				foreach (UIElement item in this.VisibleWhenExpanded)
				{
					base.Append(item);
					CalculatedStyle innerDimensions = item.GetInnerDimensions();
					if (innerDimensions.Height > newHeight)
					{
						newHeight = 30f + innerDimensions.Height + this.PaddingBottom + this.PaddingTop;
					}
				}
				this.expandButton.HoverText = Language.GetTextValue("tModLoader.ModConfigCollapse");
				this.expandButton.SetImage(this.ExpandedTexture);
				Action onExpanded = this.OnExpanded;
				if (onExpanded != null)
				{
					onExpanded();
				}
			}
			else
			{
				foreach (UIElement item2 in this.VisibleWhenExpanded)
				{
					base.RemoveChild(item2);
				}
				Action onCollapsed = this.OnCollapsed;
				if (onCollapsed != null)
				{
					onCollapsed();
				}
				this.expandButton.HoverText = Language.GetTextValue("tModLoader.ModConfigExpand");
				this.expandButton.SetImage(this.CollapsedTexture);
			}
			this.Height.Set(newHeight, 0f);
		}

		// Token: 0x04001A53 RID: 6739
		private bool pendingChanges;

		// Token: 0x04001A54 RID: 6740
		private bool expanded;

		// Token: 0x04001A55 RID: 6741
		private float defaultHeight = 40f;

		// Token: 0x04001A56 RID: 6742
		private UIHoverImage expandButton;

		// Token: 0x04001A59 RID: 6745
		public List<UIElement> VisibleWhenExpanded = new List<UIElement>();
	}
}
