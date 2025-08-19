using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x02000397 RID: 919
	internal abstract class CollectionElement : ConfigElement
	{
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06003175 RID: 12661 RVA: 0x0053FDBA File Offset: 0x0053DFBA
		// (set) Token: 0x06003176 RID: 12662 RVA: 0x0053FDC2 File Offset: 0x0053DFC2
		protected object Data { get; set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x0053FDCB File Offset: 0x0053DFCB
		// (set) Token: 0x06003178 RID: 12664 RVA: 0x0053FDD3 File Offset: 0x0053DFD3
		protected UIElement DataListElement { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x0053FDDC File Offset: 0x0053DFDC
		// (set) Token: 0x0600317A RID: 12666 RVA: 0x0053FDE4 File Offset: 0x0053DFE4
		protected NestedUIList DataList { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x0053FDED File Offset: 0x0053DFED
		// (set) Token: 0x0600317C RID: 12668 RVA: 0x0053FDF5 File Offset: 0x0053DFF5
		protected float Scale { get; set; } = 1f;

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x0053FDFE File Offset: 0x0053DFFE
		// (set) Token: 0x0600317E RID: 12670 RVA: 0x0053FE06 File Offset: 0x0053E006
		protected DefaultListValueAttribute DefaultListValueAttribute { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x0053FE0F File Offset: 0x0053E00F
		// (set) Token: 0x06003180 RID: 12672 RVA: 0x0053FE17 File Offset: 0x0053E017
		protected JsonDefaultListValueAttribute JsonDefaultListValueAttribute { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x0053FE20 File Offset: 0x0053E020
		protected virtual bool CanAdd
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x0053FE24 File Offset: 0x0053E024
		public override void OnBind()
		{
			base.OnBind();
			ExpandAttribute expandAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<ExpandAttribute>(base.MemberInfo, base.Item, base.List);
			if (expandAttribute != null)
			{
				this.expanded = expandAttribute.Expand;
			}
			this.Data = base.MemberInfo.GetValue(base.Item);
			this.DefaultListValueAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<DefaultListValueAttribute>(base.MemberInfo, null, null);
			this.MaxHeight.Set(300f, 0f);
			this.DataListElement = new UIElement();
			this.DataListElement.Width.Set(-10f, 1f);
			this.DataListElement.Left.Set(10f, 0f);
			this.DataListElement.Height.Set(-30f, 1f);
			this.DataListElement.Top.Set(30f, 0f);
			if (this.Data != null && this.expanded)
			{
				base.Append(this.DataListElement);
			}
			this.DataListElement.OverflowHidden = true;
			this.DataList = new NestedUIList();
			this.DataList.Width.Set(-20f, 1f);
			this.DataList.Left.Set(0f, 0f);
			this.DataList.Height.Set(0f, 1f);
			this.DataList.ListPadding = 5f;
			this.DataListElement.Append(this.DataList);
			UIScrollbar scrollbar = new UIScrollbar();
			scrollbar.SetView(100f, 1000f);
			scrollbar.Height.Set(-16f, 1f);
			scrollbar.Top.Set(6f, 0f);
			UIScrollbar uiscrollbar = scrollbar;
			uiscrollbar.Left.Pixels = uiscrollbar.Left.Pixels - 3f;
			scrollbar.HAlign = 1f;
			this.DataList.SetScrollbar(scrollbar);
			this.DataListElement.Append(scrollbar);
			this.PrepareTypes();
			this.SetupList();
			if (this.CanAdd)
			{
				this.initializeButton = new UIModConfigHoverImage(base.PlayTexture, Language.GetTextValue("tModLoader.ModConfigInitialize"));
				UIModConfigHoverImage uimodConfigHoverImage = this.initializeButton;
				uimodConfigHoverImage.Top.Pixels = uimodConfigHoverImage.Top.Pixels + 4f;
				UIModConfigHoverImage uimodConfigHoverImage2 = this.initializeButton;
				uimodConfigHoverImage2.Left.Pixels = uimodConfigHoverImage2.Left.Pixels - 3f;
				this.initializeButton.HAlign = 1f;
				this.initializeButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					SoundEngine.PlaySound(SoundID.Tink, null, null);
					this.InitializeCollection();
					this.SetupList();
					Interface.modConfig.RecalculateChildren();
					Interface.modConfig.SetPendingChanges(true);
					this.expanded = true;
					this.pendingChanges = true;
				};
				this.addButton = new UIModConfigHoverImage(base.PlusTexture, Language.GetTextValue("tModLoader.ModConfigAdd"));
				this.addButton.Top.Set(4f, 0f);
				this.addButton.Left.Set(-52f, 1f);
				this.addButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					SoundEngine.PlaySound(SoundID.Tink, null, null);
					this.AddItem();
					this.SetupList();
					Interface.modConfig.RecalculateChildren();
					Interface.modConfig.SetPendingChanges(true);
					this.expanded = true;
					this.pendingChanges = true;
				};
				this.deleteButton = new UIModConfigHoverImage(base.DeleteTexture, Language.GetTextValue("tModLoader.ModConfigClear"));
				this.deleteButton.Top.Set(4f, 0f);
				this.deleteButton.Left.Set(-25f, 1f);
				this.deleteButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					SoundEngine.PlaySound(SoundID.Tink, null, null);
					if (base.NullAllowed)
					{
						this.NullCollection();
					}
					else
					{
						this.ClearCollection();
					}
					this.SetupList();
					Interface.modConfig.RecalculateChildren();
					Interface.modConfig.SetPendingChanges(true);
					this.pendingChanges = true;
				};
			}
			this.expandButton = new UIModConfigHoverImage(this.expanded ? base.ExpandedTexture : base.CollapsedTexture, this.expanded ? Language.GetTextValue("tModLoader.ModConfigCollapse") : Language.GetTextValue("tModLoader.ModConfigExpand"));
			this.expandButton.Top.Set(4f, 0f);
			this.expandButton.Left.Set(-79f, 1f);
			this.expandButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.expanded = !this.expanded;
				this.pendingChanges = true;
			};
			this.upDownButton = new UIModConfigHoverImageSplit(base.UpDownTexture, Language.GetTextValue("tModLoader.ModConfigScaleUp"), Language.GetTextValue("tModLoader.ModConfigScaleDown"));
			this.upDownButton.Top.Set(4f, 0f);
			this.upDownButton.Left.Set(-106f, 1f);
			this.upDownButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				Rectangle r = b.GetDimensions().ToRectangle();
				if (a.MousePosition.Y < (float)(r.Y + r.Height / 2))
				{
					this.Scale = Math.Min(2f, this.Scale + 0.5f);
					return;
				}
				this.Scale = Math.Max(1f, this.Scale - 0.5f);
			};
			this.pendingChanges = true;
			this.Recalculate();
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x00540288 File Offset: 0x0053E488
		protected object CreateCollectionElementInstance(Type type)
		{
			object toAdd;
			if (this.DefaultListValueAttribute != null)
			{
				toAdd = this.DefaultListValueAttribute.Value;
			}
			else
			{
				toAdd = ConfigManager.AlternateCreateInstance(type);
				if (!type.IsValueType && type != typeof(string))
				{
					JsonDefaultListValueAttribute jsonDefaultListValueAttribute = this.JsonDefaultListValueAttribute;
					JsonConvert.PopulateObject(((jsonDefaultListValueAttribute != null) ? jsonDefaultListValueAttribute.Json : null) ?? "{}", toAdd, ConfigManager.serializerSettings);
				}
			}
			return toAdd;
		}

		// Token: 0x06003184 RID: 12676
		protected abstract void PrepareTypes();

		// Token: 0x06003185 RID: 12677
		protected abstract void AddItem();

		// Token: 0x06003186 RID: 12678
		protected abstract void InitializeCollection();

		// Token: 0x06003187 RID: 12679 RVA: 0x005402F3 File Offset: 0x0053E4F3
		protected virtual void NullCollection()
		{
			this.Data = null;
			this.SetObject(this.Data);
		}

		// Token: 0x06003188 RID: 12680
		protected abstract void ClearCollection();

		// Token: 0x06003189 RID: 12681
		protected abstract void SetupList();

		// Token: 0x0600318A RID: 12682 RVA: 0x00540308 File Offset: 0x0053E508
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (!this.pendingChanges)
			{
				return;
			}
			this.pendingChanges = false;
			if (this.CanAdd)
			{
				base.RemoveChild(this.initializeButton);
				base.RemoveChild(this.addButton);
				base.RemoveChild(this.deleteButton);
			}
			base.RemoveChild(this.expandButton);
			base.RemoveChild(this.upDownButton);
			base.RemoveChild(this.DataListElement);
			if (this.Data == null)
			{
				base.Append(this.initializeButton);
				return;
			}
			if (this.CanAdd)
			{
				base.Append(this.addButton);
				base.Append(this.deleteButton);
			}
			base.Append(this.expandButton);
			if (this.expanded)
			{
				base.Append(this.upDownButton);
				base.Append(this.DataListElement);
				this.expandButton.HoverText = Language.GetTextValue("tModLoader.ModConfigCollapse");
				this.expandButton.SetImage(base.ExpandedTexture);
				return;
			}
			this.expandButton.HoverText = Language.GetTextValue("tModLoader.ModConfigExpand");
			this.expandButton.SetImage(base.CollapsedTexture);
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x0054042C File Offset: 0x0053E62C
		public override void Recalculate()
		{
			base.Recalculate();
			float defaultHeight = 30f;
			float h = (this.DataListElement.Parent != null) ? (this.DataList.GetTotalHeight() + defaultHeight) : defaultHeight;
			h = Utils.Clamp<float>(h, 30f, 300f * this.Scale);
			this.MaxHeight.Set(300f * this.Scale, 0f);
			this.Height.Set(h, 0f);
			if (base.Parent != null && base.Parent is UISortableElement)
			{
				base.Parent.Height.Set(h, 0f);
			}
		}

		// Token: 0x04001D45 RID: 7493
		private UIModConfigHoverImage initializeButton;

		// Token: 0x04001D46 RID: 7494
		private UIModConfigHoverImage addButton;

		// Token: 0x04001D47 RID: 7495
		private UIModConfigHoverImage deleteButton;

		// Token: 0x04001D48 RID: 7496
		private UIModConfigHoverImage expandButton;

		// Token: 0x04001D49 RID: 7497
		private UIModConfigHoverImageSplit upDownButton;

		// Token: 0x04001D4A RID: 7498
		private bool expanded = true;

		// Token: 0x04001D4B RID: 7499
		private bool pendingChanges;
	}
}
