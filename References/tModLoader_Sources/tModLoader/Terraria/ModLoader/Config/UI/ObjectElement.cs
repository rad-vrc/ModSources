using System;
using System.Collections;
using System.Reflection;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003AF RID: 943
	internal class ObjectElement : ConfigElement<object>
	{
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x00543CAF File Offset: 0x00541EAF
		// (set) Token: 0x06003272 RID: 12914 RVA: 0x00543CB7 File Offset: 0x00541EB7
		protected Func<string> AbridgedTextDisplayFunction { get; set; }

		// Token: 0x06003273 RID: 12915 RVA: 0x00543CC0 File Offset: 0x00541EC0
		public ObjectElement(bool ignoreSeparatePage = false)
		{
			this.ignoreSeparatePage = ignoreSeparatePage;
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x00543CD8 File Offset: 0x00541ED8
		public override void OnBind()
		{
			base.OnBind();
			if (base.List != null)
			{
				Type listType;
				if (base.MemberInfo.Type.IsArray)
				{
					listType = base.MemberInfo.Type.GetElementType();
				}
				else
				{
					listType = base.MemberInfo.Type.GetGenericArguments()[0];
				}
				MethodInfo methodInfo = listType.GetMethod("ToString", Array.Empty<Type>());
				if (methodInfo != null && methodInfo.DeclaringType != typeof(object))
				{
					base.TextDisplayFunction = delegate()
					{
						string str = (base.Index + 1).ToString();
						string str2 = ": ";
						object obj = base.List[base.Index];
						return str + str2 + (((obj != null) ? obj.ToString() : null) ?? "null");
					};
					this.AbridgedTextDisplayFunction = delegate()
					{
						object obj = base.List[base.Index];
						return ((obj != null) ? obj.ToString() : null) ?? "null";
					};
				}
				else
				{
					base.TextDisplayFunction = (() => (base.Index + 1).ToString() + ": ");
				}
			}
			else if (base.MemberInfo.Type.GetMethod("ToString", Array.Empty<Type>()).DeclaringType != typeof(object))
			{
				base.TextDisplayFunction = (() => this.Label + ((this.Value == null) ? "" : (": " + this.Value.ToString())));
				this.AbridgedTextDisplayFunction = delegate()
				{
					object value = this.Value;
					return ((value != null) ? value.ToString() : null) ?? "";
				};
			}
			if (this.Value == null)
			{
				IList list = base.List;
			}
			this.separatePage = (ConfigManager.GetCustomAttributeFromMemberThenMemberType<SeparatePageAttribute>(base.MemberInfo, base.Item, base.List) != null);
			if (this.separatePage && !this.ignoreSeparatePage)
			{
				this.separatePageButton = new UITextPanel<FuncStringWrapper>(new FuncStringWrapper(base.TextDisplayFunction), 1f, false);
				this.separatePageButton.HAlign = 0.5f;
				this.separatePageButton.OnLeftClick += delegate(UIMouseEvent a, UIElement c)
				{
					UIModConfig.SwitchToSubConfig(this.separatePagePanel);
				};
			}
			if (base.List == null)
			{
				ExpandAttribute expandAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<ExpandAttribute>(base.MemberInfo, base.Item, base.List);
				if (expandAttribute != null)
				{
					this.expanded = expandAttribute.Expand;
				}
			}
			else
			{
				Type listType2;
				if (base.MemberInfo.Type.IsArray)
				{
					listType2 = base.MemberInfo.Type.GetElementType();
				}
				else
				{
					listType2 = base.MemberInfo.Type.GetGenericArguments()[0];
				}
				ExpandAttribute expandAttribute2 = (ExpandAttribute)Attribute.GetCustomAttribute(listType2, typeof(ExpandAttribute), true);
				if (expandAttribute2 != null)
				{
					this.expanded = expandAttribute2.Expand;
				}
				expandAttribute2 = (ExpandAttribute)Attribute.GetCustomAttribute(base.MemberInfo.MemberInfo, typeof(ExpandAttribute), true);
				if (expandAttribute2 != null && expandAttribute2.ExpandListElements != null)
				{
					this.expanded = expandAttribute2.ExpandListElements.Value;
				}
			}
			this.dataList = new NestedUIList();
			this.dataList.Width.Set(-14f, 1f);
			this.dataList.Left.Set(14f, 0f);
			this.dataList.Height.Set(-30f, 1f);
			this.dataList.Top.Set(30f, 0f);
			this.dataList.ListPadding = 5f;
			if (this.expanded)
			{
				base.Append(this.dataList);
			}
			IList list2 = base.List;
			this.initializeButton = new UIModConfigHoverImage(base.PlayTexture, Language.GetTextValue("tModLoader.ModConfigInitialize"));
			UIModConfigHoverImage uimodConfigHoverImage = this.initializeButton;
			uimodConfigHoverImage.Top.Pixels = uimodConfigHoverImage.Top.Pixels + 4f;
			UIModConfigHoverImage uimodConfigHoverImage2 = this.initializeButton;
			uimodConfigHoverImage2.Left.Pixels = uimodConfigHoverImage2.Left.Pixels - 3f;
			this.initializeButton.HAlign = 1f;
			this.initializeButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				SoundEngine.PlaySound(21, -1, -1, 1, 1f, 0f);
				object data = Activator.CreateInstance(base.MemberInfo.Type, true);
				JsonDefaultValueAttribute jsonDefaultValueAttribute = this.JsonDefaultValueAttribute;
				JsonConvert.PopulateObject(((jsonDefaultValueAttribute != null) ? jsonDefaultValueAttribute.Json : null) ?? "{}", data, ConfigManager.serializerSettings);
				this.Value = data;
				this.pendingChanges = true;
				this.SetupList();
				Interface.modConfig.RecalculateChildren();
				Interface.modConfig.SetPendingChanges(true);
			};
			this.expandButton = new UIModConfigHoverImage(this.expanded ? base.ExpandedTexture : base.CollapsedTexture, this.expanded ? Language.GetTextValue("tModLoader.ModConfigCollapse") : Language.GetTextValue("tModLoader.ModConfigExpand"));
			this.expandButton.Top.Set(4f, 0f);
			this.expandButton.Left.Set(-52f, 1f);
			this.expandButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.expanded = !this.expanded;
				this.pendingChanges = true;
			};
			this.deleteButton = new UIModConfigHoverImage(base.DeleteTexture, Language.GetTextValue("tModLoader.ModConfigClear"));
			this.deleteButton.Top.Set(4f, 0f);
			this.deleteButton.Left.Set(-25f, 1f);
			this.deleteButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.Value = null;
				this.pendingChanges = true;
				this.SetupList();
				Interface.modConfig.SetPendingChanges(true);
			};
			if (this.Value != null)
			{
				this.SetupList();
			}
			else
			{
				base.Append(this.initializeButton);
			}
			this.pendingChanges = true;
			this.Recalculate();
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x00544184 File Offset: 0x00542384
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (!this.pendingChanges)
			{
				return;
			}
			this.pendingChanges = false;
			base.DrawLabel = (!this.separatePage || this.ignoreSeparatePage);
			base.RemoveChild(this.deleteButton);
			base.RemoveChild(this.expandButton);
			base.RemoveChild(this.initializeButton);
			base.RemoveChild(this.dataList);
			if (this.separatePage && !this.ignoreSeparatePage)
			{
				base.RemoveChild(this.separatePageButton);
			}
			if (this.Value == null)
			{
				base.Append(this.initializeButton);
				base.DrawLabel = true;
				return;
			}
			if (base.List == null && (!this.separatePage || !this.ignoreSeparatePage) && base.NullAllowed)
			{
				base.Append(this.deleteButton);
			}
			if (this.separatePage && !this.ignoreSeparatePage)
			{
				base.Append(this.separatePageButton);
				return;
			}
			if (!this.ignoreSeparatePage)
			{
				base.Append(this.expandButton);
			}
			if (this.expanded)
			{
				base.Append(this.dataList);
				this.expandButton.HoverText = Language.GetTextValue("tModLoader.ModConfigCollapse");
				this.expandButton.SetImage(base.ExpandedTexture);
				return;
			}
			base.RemoveChild(this.dataList);
			this.expandButton.HoverText = Language.GetTextValue("tModLoader.ModConfigExpand");
			this.expandButton.SetImage(base.CollapsedTexture);
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x005442F4 File Offset: 0x005424F4
		private void SetupList()
		{
			this.dataList.Clear();
			object data = this.Value;
			if (data != null)
			{
				if (this.separatePage && !this.ignoreSeparatePage)
				{
					this.separatePagePanel = UIModConfig.MakeSeparateListPanel(base.Item, data, base.MemberInfo, base.List, base.Index, this.AbridgedTextDisplayFunction);
					return;
				}
				int order = 0;
				foreach (PropertyFieldWrapper variable in ConfigManager.GetFieldsAndProperties(data))
				{
					if (!Attribute.IsDefined(variable.MemberInfo, typeof(JsonIgnoreAttribute)) || Attribute.IsDefined(variable.MemberInfo, typeof(ShowDespiteJsonIgnoreAttribute)))
					{
						int top = 0;
						UIModConfig.HandleHeader(this.dataList, ref top, ref order, variable);
						Tuple<UIElement, UIElement> wrapped = UIModConfig.WrapIt(this.dataList, ref top, variable, data, order++, null, null, -1);
						if (base.List != null)
						{
							UIElement item = wrapped.Item1;
							item.Width.Pixels = item.Width.Pixels + 20f;
						}
					}
				}
			}
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x00544414 File Offset: 0x00542614
		public override void Recalculate()
		{
			base.Recalculate();
			float defaultHeight = (float)(this.separatePage ? 40 : 30);
			float h = (this.dataList.Parent != null) ? (this.dataList.GetTotalHeight() + defaultHeight) : defaultHeight;
			this.Height.Set(h, 0f);
			if (base.Parent != null && base.Parent is UISortableElement)
			{
				base.Parent.Height.Set(h, 0f);
			}
		}

		// Token: 0x04001DA1 RID: 7585
		private readonly bool ignoreSeparatePage;

		// Token: 0x04001DA2 RID: 7586
		private bool separatePage;

		// Token: 0x04001DA3 RID: 7587
		private bool pendingChanges;

		// Token: 0x04001DA4 RID: 7588
		private bool expanded = true;

		// Token: 0x04001DA5 RID: 7589
		private NestedUIList dataList;

		// Token: 0x04001DA6 RID: 7590
		private UIModConfigHoverImage initializeButton;

		// Token: 0x04001DA7 RID: 7591
		private UIModConfigHoverImage deleteButton;

		// Token: 0x04001DA8 RID: 7592
		private UIModConfigHoverImage expandButton;

		// Token: 0x04001DA9 RID: 7593
		internal UIPanel separatePagePanel;

		// Token: 0x04001DAA RID: 7594
		private UITextPanel<FuncStringWrapper> separatePageButton;
	}
}
