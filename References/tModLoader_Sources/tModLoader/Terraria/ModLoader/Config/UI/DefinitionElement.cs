using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x0200039C RID: 924
	internal abstract class DefinitionElement<T> : ConfigElement<T> where T : EntityDefinition
	{
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x005412D6 File Offset: 0x0053F4D6
		// (set) Token: 0x060031CB RID: 12747 RVA: 0x005412DE File Offset: 0x0053F4DE
		protected bool UpdateNeeded { get; set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x005412E7 File Offset: 0x0053F4E7
		// (set) Token: 0x060031CD RID: 12749 RVA: 0x005412EF File Offset: 0x0053F4EF
		protected bool SelectionExpanded { get; set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x005412F8 File Offset: 0x0053F4F8
		// (set) Token: 0x060031CF RID: 12751 RVA: 0x00541300 File Offset: 0x0053F500
		protected UIPanel ChooserPanel { get; set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x00541309 File Offset: 0x0053F509
		// (set) Token: 0x060031D1 RID: 12753 RVA: 0x00541311 File Offset: 0x0053F511
		protected NestedUIGrid ChooserGrid { get; set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060031D2 RID: 12754 RVA: 0x0054131A File Offset: 0x0053F51A
		// (set) Token: 0x060031D3 RID: 12755 RVA: 0x00541322 File Offset: 0x0053F522
		protected UIFocusInputTextField ChooserFilter { get; set; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060031D4 RID: 12756 RVA: 0x0054132B File Offset: 0x0053F52B
		// (set) Token: 0x060031D5 RID: 12757 RVA: 0x00541333 File Offset: 0x0053F533
		protected UIFocusInputTextField ChooserFilterMod { get; set; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060031D6 RID: 12758 RVA: 0x0054133C File Offset: 0x0053F53C
		// (set) Token: 0x060031D7 RID: 12759 RVA: 0x00541344 File Offset: 0x0053F544
		protected float OptionScale { get; set; } = 0.5f;

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x0054134D File Offset: 0x0053F54D
		// (set) Token: 0x060031D9 RID: 12761 RVA: 0x00541355 File Offset: 0x0053F555
		protected List<DefinitionOptionElement<T>> Options { get; set; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060031DA RID: 12762 RVA: 0x0054135E File Offset: 0x0053F55E
		// (set) Token: 0x060031DB RID: 12763 RVA: 0x00541366 File Offset: 0x0053F566
		protected DefinitionOptionElement<T> OptionChoice { get; set; }

		// Token: 0x060031DC RID: 12764 RVA: 0x00541370 File Offset: 0x0053F570
		public override void OnBind()
		{
			base.OnBind();
			base.TextDisplayFunction = (() => this.Label + ": " + this.OptionChoice.Tooltip);
			if (base.List != null)
			{
				base.TextDisplayFunction = (() => (base.Index + 1).ToString() + ": " + this.OptionChoice.Tooltip);
			}
			this.Height.Set(30f, 0f);
			this.OptionChoice = this.CreateDefinitionOptionElement();
			this.OptionChoice.Top.Set(2f, 0f);
			this.OptionChoice.Left.Set(-30f, 1f);
			this.OptionChoice.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.SelectionExpanded = !this.SelectionExpanded;
				this.UpdateNeeded = true;
			};
			this.TweakDefinitionOptionElement(this.OptionChoice);
			base.Append(this.OptionChoice);
			this.ChooserPanel = new UIPanel();
			this.ChooserPanel.Top.Set(30f, 0f);
			this.ChooserPanel.Height.Set(200f, 0f);
			this.ChooserPanel.Width.Set(0f, 1f);
			this.ChooserPanel.BackgroundColor = Color.CornflowerBlue;
			UIPanel textBoxBackgroundA = new UIPanel();
			textBoxBackgroundA.Width.Set(160f, 0f);
			textBoxBackgroundA.Height.Set(30f, 0f);
			textBoxBackgroundA.Top.Set(-6f, 0f);
			textBoxBackgroundA.PaddingTop = 0f;
			textBoxBackgroundA.PaddingBottom = 0f;
			this.ChooserFilter = new UIFocusInputTextField(Language.GetTextValue("tModLoader.ModConfigFilterByName"));
			this.ChooserFilter.OnTextChange += delegate(object a, EventArgs b)
			{
				this.UpdateNeeded = true;
			};
			this.ChooserFilter.OnRightClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.ChooserFilter.SetText("");
			};
			this.ChooserFilter.Width = StyleDimension.Fill;
			this.ChooserFilter.Height.Set(-6f, 1f);
			this.ChooserFilter.Top.Set(6f, 0f);
			textBoxBackgroundA.Append(this.ChooserFilter);
			this.ChooserPanel.Append(textBoxBackgroundA);
			UIPanel textBoxBackgroundB = new UIPanel();
			textBoxBackgroundB.CopyStyle(textBoxBackgroundA);
			textBoxBackgroundB.Left.Set(180f, 0f);
			this.ChooserFilterMod = new UIFocusInputTextField(Language.GetTextValue("tModLoader.ModConfigFilterByMod"));
			this.ChooserFilterMod.OnTextChange += delegate(object a, EventArgs b)
			{
				this.UpdateNeeded = true;
			};
			this.ChooserFilterMod.OnRightClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.ChooserFilterMod.SetText("");
			};
			this.ChooserFilterMod.Width = StyleDimension.Fill;
			this.ChooserFilterMod.Height.Set(-6f, 1f);
			this.ChooserFilterMod.Top.Set(6f, 0f);
			textBoxBackgroundB.Append(this.ChooserFilterMod);
			this.ChooserPanel.Append(textBoxBackgroundB);
			this.ChooserGrid = new NestedUIGrid();
			this.ChooserGrid.Top.Set(30f, 0f);
			this.ChooserGrid.Height.Set(-30f, 1f);
			this.ChooserGrid.Width.Set(-12f, 1f);
			this.ChooserPanel.Append(this.ChooserGrid);
			UIScrollbar scrollbar = new UIScrollbar();
			scrollbar.SetView(100f, 1000f);
			scrollbar.Height.Set(-30f, 1f);
			scrollbar.Top.Set(30f, 0f);
			UIScrollbar uiscrollbar = scrollbar;
			uiscrollbar.Left.Pixels = uiscrollbar.Left.Pixels + 8f;
			scrollbar.HAlign = 1f;
			this.ChooserGrid.SetScrollbar(scrollbar);
			this.ChooserPanel.Append(scrollbar);
			UIModConfigHoverImageSplit upDownButton = new UIModConfigHoverImageSplit(base.UpDownTexture, Language.GetTextValue("LegacyMenu.168"), Language.GetTextValue("LegacyMenu.169"));
			upDownButton.Recalculate();
			upDownButton.Top.Set(-4f, 0f);
			upDownButton.Left.Set(-18f, 1f);
			upDownButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				Rectangle r = b.GetDimensions().ToRectangle();
				if (a.MousePosition.Y < (float)(r.Y + r.Height / 2))
				{
					this.OptionScale = Math.Min(1f, this.OptionScale + 0.1f);
				}
				else
				{
					this.OptionScale = Math.Max(0.5f, this.OptionScale - 0.1f);
				}
				foreach (DefinitionOptionElement<T> definitionOptionElement in this.Options)
				{
					definitionOptionElement.SetScale(this.OptionScale);
				}
			};
			this.ChooserPanel.Append(upDownButton);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x005417A8 File Offset: 0x0053F9A8
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (!this.UpdateNeeded)
			{
				return;
			}
			this.UpdateNeeded = false;
			if (this.SelectionExpanded && this.Options == null)
			{
				this.Options = this.CreateDefinitionOptionElementList();
			}
			if (!this.SelectionExpanded)
			{
				this.ChooserPanel.Remove();
			}
			else
			{
				base.Append(this.ChooserPanel);
			}
			float newHeight = (float)(this.SelectionExpanded ? 240 : 30);
			this.Height.Set(newHeight, 0f);
			if (base.Parent != null && base.Parent is UISortableElement)
			{
				base.Parent.Height.Pixels = newHeight;
			}
			if (this.SelectionExpanded)
			{
				List<DefinitionOptionElement<T>> passed = this.GetPassedOptionElements();
				this.ChooserGrid.Clear();
				this.ChooserGrid.AddRange(passed);
			}
			this.OptionChoice.SetItem(this.Value);
		}

		// Token: 0x060031DE RID: 12766
		protected abstract List<DefinitionOptionElement<T>> GetPassedOptionElements();

		// Token: 0x060031DF RID: 12767
		protected abstract List<DefinitionOptionElement<T>> CreateDefinitionOptionElementList();

		// Token: 0x060031E0 RID: 12768
		protected abstract DefinitionOptionElement<T> CreateDefinitionOptionElement();

		// Token: 0x060031E1 RID: 12769 RVA: 0x0054188A File Offset: 0x0053FA8A
		protected virtual void TweakDefinitionOptionElement(DefinitionOptionElement<T> optionElement)
		{
		}
	}
}
