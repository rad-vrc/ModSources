using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003C1 RID: 961
	internal class UIModConfig : UIState, IHaveBackButtonCommand
	{
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x005463BC File Offset: 0x005445BC
		// (set) Token: 0x060032EB RID: 13035 RVA: 0x005463C4 File Offset: 0x005445C4
		public UIState PreviousUIState { get; set; }

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x005463CD File Offset: 0x005445CD
		// (set) Token: 0x060032ED RID: 13037 RVA: 0x005463D5 File Offset: 0x005445D5
		public int UpdateCount { get; set; }

		// Token: 0x060032EE RID: 13038 RVA: 0x005463E0 File Offset: 0x005445E0
		public override void OnInitialize()
		{
			this.uIElement = new UIElement();
			this.uIElement.Width.Set(0f, 0.8f);
			this.uIElement.MaxWidth.Set(600f, 0f);
			this.uIElement.Top.Set(160f, 0f);
			this.uIElement.Height.Set(-180f, 1f);
			this.uIElement.HAlign = 0.5f;
			this.uIPanel = new UIPanel();
			this.uIPanel.Width.Set(0f, 1f);
			this.uIPanel.Height.Set(-140f, 1f);
			this.uIPanel.Top.Set(30f, 0f);
			this.uIPanel.BackgroundColor = UICommon.MainPanelBackground;
			this.uIElement.Append(this.uIPanel);
			UIPanel textBoxBackground = new UIPanel();
			textBoxBackground.SetPadding(0f);
			this.filterTextField = new UIFocusInputTextField(Language.GetTextValue("tModLoader.ModConfigFilterOptions"));
			textBoxBackground.Top.Set(2f, 0f);
			textBoxBackground.Left.Set(-190f, 1f);
			textBoxBackground.Width.Set(180f, 0f);
			textBoxBackground.Height.Set(30f, 0f);
			this.uIElement.Append(textBoxBackground);
			this.filterTextField.SetText("");
			this.filterTextField.Top.Set(5f, 0f);
			this.filterTextField.Left.Set(10f, 0f);
			this.filterTextField.Width.Set(-20f, 1f);
			this.filterTextField.Height.Set(20f, 0f);
			this.filterTextField.OnTextChange += delegate(object a, EventArgs b)
			{
				this.updateNeeded = true;
			};
			this.filterTextField.OnRightClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.filterTextField.SetText("");
			};
			textBoxBackground.Append(this.filterTextField);
			this.message = new UITextPanel<string>(Language.GetTextValue("tModLoader.ModConfigNotification"), 1f, false);
			this.message.Width.Set(-80f, 1f);
			this.message.Height.Set(20f, 0f);
			this.message.HAlign = 0.5f;
			this.message.VAlign = 1f;
			this.message.Top.Set(-65f, 0f);
			this.uIElement.Append(this.message);
			this.mainConfigList = new UIList();
			this.mainConfigList.Width.Set(-25f, 1f);
			this.mainConfigList.Height.Set(0f, 1f);
			this.mainConfigList.ListPadding = 5f;
			this.uIPanel.Append(this.mainConfigList);
			this.configPanelStack.Push(this.uIPanel);
			this.uIScrollbar = new UIScrollbar();
			this.uIScrollbar.SetView(100f, 1000f);
			this.uIScrollbar.Height.Set(0f, 1f);
			this.uIScrollbar.HAlign = 1f;
			this.uIPanel.Append(this.uIScrollbar);
			this.mainConfigList.SetScrollbar(this.uIScrollbar);
			this.headerTextPanel = new UITextPanel<string>(Language.GetTextValue("tModLoader.ModConfigModConfig"), 0.8f, true);
			this.headerTextPanel.HAlign = 0.5f;
			this.headerTextPanel.Top.Set(-50f, 0f);
			this.headerTextPanel.SetPadding(15f);
			this.headerTextPanel.BackgroundColor = UICommon.DefaultUIBlue;
			this.uIElement.Append(this.headerTextPanel);
			this.previousConfigButton = new UITextPanel<string>("<", 1f, false);
			this.previousConfigButton.Width.Set(25f, 0f);
			this.previousConfigButton.Height.Set(25f, 0f);
			this.previousConfigButton.VAlign = 1f;
			this.previousConfigButton.Top.Set(-65f, 0f);
			this.previousConfigButton.HAlign = 0f;
			this.previousConfigButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.previousConfigButton.OnLeftClick += this.PreviousConfig;
			this.nextConfigButton = new UITextPanel<string>(">", 1f, false);
			this.nextConfigButton.CopyStyle(this.previousConfigButton);
			this.nextConfigButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.nextConfigButton.HAlign = 1f;
			this.nextConfigButton.OnLeftClick += this.NextConfig;
			this.saveConfigButton = new UITextPanel<string>(Language.GetTextValue("tModLoader.ModConfigSaveConfig"), 1f, false);
			this.saveConfigButton.Width.Set(-10f, 0.25f);
			this.saveConfigButton.Height.Set(25f, 0f);
			this.saveConfigButton.Top.Set(-20f, 0f);
			this.saveConfigButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.saveConfigButton.HAlign = 0.33f;
			this.saveConfigButton.VAlign = 1f;
			this.saveConfigButton.OnLeftClick += this.SaveConfig;
			this.backButton = new UITextPanel<string>(Language.GetTextValue("tModLoader.ModConfigBack"), 1f, false);
			this.backButton.CopyStyle(this.saveConfigButton);
			this.backButton.HAlign = 0f;
			this.backButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.backButton.OnMouseOver += delegate(UIMouseEvent a, UIElement b)
			{
				if (this.pendingChanges)
				{
					this.backButton.BackgroundColor = Color.Red;
				}
			};
			this.backButton.OnMouseOut += delegate(UIMouseEvent a, UIElement b)
			{
				if (this.pendingChanges)
				{
					this.backButton.BackgroundColor = Color.Red * 0.7f;
				}
			};
			this.backButton.OnLeftClick += this.BackClick;
			this.uIElement.Append(this.backButton);
			this.revertConfigButton = new UITextPanel<string>(Language.GetTextValue("tModLoader.ModConfigRevertChanges"), 1f, false);
			this.revertConfigButton.CopyStyle(this.saveConfigButton);
			this.revertConfigButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.revertConfigButton.HAlign = 0.66f;
			this.revertConfigButton.OnLeftClick += this.RevertConfig;
			this.restoreDefaultsConfigButton = new UITextPanel<string>(Language.GetTextValue("tModLoader.ModConfigRestoreDefaults"), 1f, false);
			this.restoreDefaultsConfigButton.CopyStyle(this.saveConfigButton);
			this.restoreDefaultsConfigButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.restoreDefaultsConfigButton.HAlign = 1f;
			this.restoreDefaultsConfigButton.OnLeftClick += this.RestoreDefaults;
			this.uIElement.Append(this.restoreDefaultsConfigButton);
			this.uIPanel.BackgroundColor = UICommon.MainPanelBackground;
			base.Append(this.uIElement);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x00546C1D File Offset: 0x00544E1D
		private void BackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x00546C28 File Offset: 0x00544E28
		public void HandleBackButtonUsage()
		{
			if (Main.gameMenu || !this.openedFromModder)
			{
				SoundEngine.PlaySound(SoundID.MenuClose, null, null);
			}
			if (Main.gameMenu)
			{
				Main.menuMode = 10027;
				this.HandleOnCloseCallback();
				return;
			}
			if (this.openedFromModder)
			{
				IngameFancyUI.Close();
				return;
			}
			Main.InGameUI.SetState(Interface.modConfigList);
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x00546C90 File Offset: 0x00544E90
		internal void Unload()
		{
			UIList uilist = this.mainConfigList;
			if (uilist != null)
			{
				uilist.Clear();
			}
			List<Tuple<UIElement, UIElement>> list = this.mainConfigItems;
			if (list != null)
			{
				list.Clear();
			}
			this.mod = null;
			this.sortedModConfigs = null;
			this.modConfig = null;
			this.pendingConfig = null;
			while (this.configPanelStack.Count > 1)
			{
				this.uIElement.RemoveChild(this.configPanelStack.Pop());
			}
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x00546D04 File Offset: 0x00544F04
		private void PreviousConfig(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			int index = this.sortedModConfigs.IndexOf(this.modConfig);
			this.modConfig = this.sortedModConfigs[(index - 1 < 0) ? (this.sortedModConfigs.Count - 1) : (index - 1)];
			this.DoMenuModeState(false);
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x00546D68 File Offset: 0x00544F68
		private void NextConfig(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			int index = this.sortedModConfigs.IndexOf(this.modConfig);
			this.modConfig = this.sortedModConfigs[(index + 1 > this.sortedModConfigs.Count) ? 0 : (index + 1)];
			this.DoMenuModeState(false);
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x00546DCA File Offset: 0x00544FCA
		private void DoMenuModeState(bool preserveNotificationMessage = false)
		{
			this.preserveNotificationMessage = preserveNotificationMessage;
			if (Main.gameMenu)
			{
				Main.MenuUI.SetState(null);
				Main.menuMode = 10024;
				return;
			}
			Main.InGameUI.SetState(null);
			Main.InGameUI.SetState(Interface.modConfig);
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x00546E0A File Offset: 0x0054500A
		private void SaveConfig(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this.modConfig.SaveChanges(this.pendingConfig, new Action<string, Color>(this.SetMessage), false, true) == ConfigSaveResult.Success)
			{
				this.DoMenuModeState(true);
			}
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x00546E34 File Offset: 0x00545034
		private void RestoreDefaults(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			UIModConfig.pendingRevertDefaults = true;
			this.SetMessage(Language.GetTextValue("tModLoader.ModConfigDefaultsRestored"), Color.Green);
			this.DoMenuModeState(true);
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x00546E78 File Offset: 0x00545078
		private void RevertConfig(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			this.DiscardChanges();
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x00546EA0 File Offset: 0x005450A0
		private void DiscardChanges()
		{
			this.SetMessage(Language.GetTextValue("tModLoader.ModConfigChangesReverted"), Color.Green);
			this.DoMenuModeState(true);
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x00546EBE File Offset: 0x005450BE
		public void SetPendingChanges(bool changes = true)
		{
			this.pendingChangesUIUpdate = (this.pendingChangesUIUpdate || changes);
			this.pendingChanges = (this.pendingChanges || changes);
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x00546EDC File Offset: 0x005450DC
		public void SetMessage(string text, Color color)
		{
			this.message.TextScale = 1f;
			UITextPanel<string> uitextPanel = this.message;
			LocalizedText text2 = Language.GetText("tModLoader.ModConfigNotification");
			uitextPanel.SetText(((text2 != null) ? text2.ToString() : null) + text);
			float width = FontAssets.MouseText.Value.MeasureString(text).X;
			if (width > 400f)
			{
				this.message.TextScale = 400f / width;
				this.message.Recalculate();
			}
			this.message.TextColor = color;
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x00546F68 File Offset: 0x00545168
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (this.scrollToOption != null)
			{
				bool header = false;
				if (this.scrollToOption.StartsWith("Header:"))
				{
					this.scrollToOption = this.scrollToOption.Split("Header:", StringSplitOptions.RemoveEmptyEntries)[0];
					header = true;
				}
				UIElement desiredElement = this.mainConfigList._items.Find(delegate(UIElement x)
				{
					UISortableElement sortableElement2 = x as UISortableElement;
					if (sortableElement2 != null)
					{
						ConfigElement configElement = sortableElement2.Children.FirstOrDefault<UIElement>() as ConfigElement;
						if (configElement != null && configElement.MemberInfo.Name == this.scrollToOption)
						{
							ObjectElement objectElement = configElement as ObjectElement;
							if (objectElement != null && objectElement.separatePagePanel != null)
							{
								UIModConfig.SwitchToSubConfig(objectElement.separatePagePanel);
								return true;
							}
							configElement.Flashing = true;
							return true;
						}
					}
					return false;
				});
				if (header)
				{
					for (int i = this.mainConfigList._items.IndexOf(desiredElement) - 1; i >= 0; i--)
					{
						UISortableElement sortableElement = this.mainConfigList._items[i] as UISortableElement;
						if (sortableElement != null && sortableElement.Children.FirstOrDefault<UIElement>() is HeaderElement)
						{
							desiredElement = sortableElement;
							break;
						}
					}
				}
				this.mainConfigList.Goto((UIElement element) => element == desiredElement, this.centerScrolledOption);
				this.scrollToOption = null;
				this.centerScrolledOption = false;
			}
			int updateCount = this.UpdateCount;
			this.UpdateCount = updateCount + 1;
			if (this.pendingChangesUIUpdate)
			{
				this.uIElement.Append(this.saveConfigButton);
				this.uIElement.Append(this.revertConfigButton);
				this.backButton.BackgroundColor = Color.Red * 0.7f;
				this.pendingChangesUIUpdate = false;
			}
			if (this.netUpdate)
			{
				this.DoMenuModeState(false);
				this.netUpdate = false;
			}
			if (!this.updateNeeded)
			{
				return;
			}
			this.updateNeeded = false;
			this.mainConfigList.Clear();
			this.mainConfigList.AddRange(from x in this.mainConfigItems.Where(delegate(Tuple<UIElement, UIElement> item)
			{
				ConfigElement configElement = item.Item2 as ConfigElement;
				return configElement == null || configElement.TextDisplayFunction().IndexOf(this.filterTextField.CurrentString, StringComparison.OrdinalIgnoreCase) != -1;
			})
			select x.Item1);
			this.Recalculate();
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x0054713E File Offset: 0x0054533E
		// (set) Token: 0x060032FD RID: 13053 RVA: 0x00547145 File Offset: 0x00545345
		public static string Tooltip { get; set; }

		// Token: 0x060032FE RID: 13054 RVA: 0x0054714D File Offset: 0x0054534D
		public override void Draw(SpriteBatch spriteBatch)
		{
			UIModConfig.Tooltip = null;
			base.Draw(spriteBatch);
			if (!string.IsNullOrEmpty(UIModConfig.Tooltip))
			{
				UICommon.TooltipMouseText(UIModConfig.Tooltip);
			}
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x00547178 File Offset: 0x00545378
		internal void SetMod(Mod mod, ModConfig config = null, bool openedFromModder = false, Action onClose = null, string scrollToOption = null, bool centerScrolledOption = true)
		{
			this.mod = mod;
			this.openedFromModder = openedFromModder;
			this.modderOnClose = onClose;
			this.scrollToOption = scrollToOption;
			this.centerScrolledOption = centerScrolledOption;
			if (!ConfigManager.Configs.ContainsKey(mod))
			{
				throw new Exception("There are no ModConfig for " + mod.DisplayNameClean + ", how did this happen?");
			}
			this.sortedModConfigs = (from x in ConfigManager.Configs[mod]
			orderby x.DisplayName.Value
			select x).ToList<ModConfig>();
			this.modConfig = this.sortedModConfigs[0];
			if (config != null)
			{
				this.modConfig = ConfigManager.Configs[mod].First((ModConfig x) => x == config);
				return;
			}
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x00547258 File Offset: 0x00545458
		public override void OnActivate()
		{
			Interface.modConfigList.ModToSelectOnOpen = this.mod;
			this.filterTextField.SetText("");
			this.updateNeeded = false;
			if (!this.preserveNotificationMessage)
			{
				this.SetMessage("", Color.White);
			}
			this.preserveNotificationMessage = false;
			string configDisplayName = this.modConfig.DisplayName.Value;
			this.headerTextPanel.SetText(string.IsNullOrEmpty(configDisplayName) ? this.modConfig.Mod.DisplayName : (this.modConfig.Mod.DisplayName + ": " + configDisplayName));
			this.pendingConfig = ConfigManager.GeneratePopulatedClone(this.modConfig);
			this.pendingChanges = UIModConfig.pendingRevertDefaults;
			if (UIModConfig.pendingRevertDefaults)
			{
				UIModConfig.pendingRevertDefaults = false;
				ConfigManager.Reset(this.pendingConfig);
				this.pendingChangesUIUpdate = true;
			}
			int num = this.sortedModConfigs.IndexOf(this.modConfig);
			int count = this.sortedModConfigs.Count;
			this.backButton.BackgroundColor = UICommon.DefaultUIBlueMouseOver;
			this.uIElement.RemoveChild(this.saveConfigButton);
			this.uIElement.RemoveChild(this.revertConfigButton);
			this.uIElement.RemoveChild(this.previousConfigButton);
			this.uIElement.RemoveChild(this.nextConfigButton);
			if (num + 1 < count)
			{
				this.uIElement.Append(this.nextConfigButton);
			}
			if (num - 1 >= 0)
			{
				this.uIElement.Append(this.previousConfigButton);
			}
			this.uIElement.RemoveChild(this.configPanelStack.Peek());
			this.uIElement.Append(this.uIPanel);
			this.mainConfigItems.Clear();
			this.mainConfigList.Clear();
			this.configPanelStack.Clear();
			this.configPanelStack.Push(this.uIPanel);
			this.subPageStack.Clear();
			int top = 0;
			this.uIPanel.BackgroundColor = UICommon.MainPanelBackground;
			BackgroundColorAttribute backgroundColorAttribute = (BackgroundColorAttribute)Attribute.GetCustomAttribute(this.pendingConfig.GetType(), typeof(BackgroundColorAttribute));
			if (backgroundColorAttribute != null)
			{
				this.uIPanel.BackgroundColor = backgroundColorAttribute.Color;
			}
			int order = 0;
			foreach (PropertyFieldWrapper variable in ConfigManager.GetFieldsAndProperties(this.pendingConfig))
			{
				if ((!variable.IsProperty || !(variable.Name == "Mode")) && (!Attribute.IsDefined(variable.MemberInfo, typeof(JsonIgnoreAttribute)) || Attribute.IsDefined(variable.MemberInfo, typeof(ShowDespiteJsonIgnoreAttribute))))
				{
					UIModConfig.HandleHeader(this.mainConfigList, ref top, ref order, variable);
					UIModConfig.WrapIt(this.mainConfigList, ref top, variable, this.pendingConfig, order++, null, null, -1);
				}
			}
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x0054754C File Offset: 0x0054574C
		public static Tuple<UIElement, UIElement> WrapIt(UIElement parent, ref int top, PropertyFieldWrapper memberInfo, object item, int order, object list = null, Type arrayType = null, int index = -1)
		{
			Type type = memberInfo.Type;
			if (arrayType != null)
			{
				type = arrayType;
			}
			CustomModConfigItemAttribute customUI = ConfigManager.GetCustomAttributeFromMemberThenMemberType<CustomModConfigItemAttribute>(memberInfo, null, null);
			UIElement e;
			if (customUI != null)
			{
				Type customUIType = customUI.Type;
				if (typeof(ConfigElement).IsAssignableFrom(customUIType))
				{
					ConstructorInfo ctor = customUIType.GetConstructor(Array.Empty<Type>());
					if (ctor != null)
					{
						e = (ctor.Invoke(new object[0]) as UIElement);
					}
					else
					{
						e = new UIText(customUIType.Name + " specified via CustomModConfigItem for " + memberInfo.Name + " does not have an empty constructor.", 1f, false);
					}
				}
				else
				{
					e = new UIText(customUIType.Name + " specified via CustomModConfigItem for " + memberInfo.Name + " does not inherit from ConfigElement.", 1f, false);
				}
			}
			else if (item.GetType() == typeof(HeaderAttribute))
			{
				e = new HeaderElement((string)memberInfo.GetValue(item));
			}
			else if (type == typeof(ItemDefinition))
			{
				e = new ItemDefinitionElement();
			}
			else if (type == typeof(ProjectileDefinition))
			{
				e = new ProjectileDefinitionElement();
			}
			else if (type == typeof(NPCDefinition))
			{
				e = new NPCDefinitionElement();
			}
			else if (type == typeof(PrefixDefinition))
			{
				e = new PrefixDefinitionElement();
			}
			else if (type == typeof(BuffDefinition))
			{
				e = new BuffDefinitionElement();
			}
			else if (type == typeof(TileDefinition))
			{
				e = new TileDefinitionElement();
			}
			else if (type == typeof(Color))
			{
				e = new ColorElement();
			}
			else if (type == typeof(Vector2))
			{
				e = new Vector2Element();
			}
			else if (type == typeof(bool))
			{
				e = new BooleanElement();
			}
			else if (type == typeof(float))
			{
				e = new FloatElement();
			}
			else if (type == typeof(byte))
			{
				e = new ByteElement();
			}
			else if (type == typeof(uint))
			{
				e = new UIntElement();
			}
			else if (type == typeof(int))
			{
				if (ConfigManager.GetCustomAttributeFromMemberThenMemberType<SliderAttribute>(memberInfo, item, list) != null)
				{
					e = new IntRangeElement();
				}
				else
				{
					e = new IntInputElement();
				}
			}
			else if (type == typeof(string))
			{
				if (ConfigManager.GetCustomAttributeFromMemberThenMemberType<OptionStringsAttribute>(memberInfo, item, list) != null)
				{
					e = new StringOptionElement();
				}
				else
				{
					e = new StringInputElement();
				}
			}
			else if (type == typeof(long))
			{
				e = new LongElement();
			}
			else if (type == typeof(ulong))
			{
				e = new ULongElement();
			}
			else if (type.IsEnum)
			{
				if (list != null)
				{
					e = new UIText(memberInfo.Name + " not handled yet (" + type.Name + ").", 1f, false);
				}
				else
				{
					e = new EnumElement();
				}
			}
			else if (type.IsArray)
			{
				e = new ArrayElement();
			}
			else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
			{
				e = new ListElement();
			}
			else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(HashSet<>))
			{
				e = new SetElement();
			}
			else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<, >))
			{
				e = new DictionaryElement();
			}
			else if (type == typeof(object))
			{
				e = new UIText(memberInfo.Name + " can't be of the Type Object.", 1f, false);
			}
			else if (type.IsClass)
			{
				e = new ObjectElement(false);
			}
			else if (type.IsValueType && !type.IsPrimitive)
			{
				e = new UIText(memberInfo.Name + " not handled yet (" + type.Name + ") Structs need special UI.", 1f, false);
				UIElement uielement = e;
				uielement.Height.Pixels = uielement.Height.Pixels + 6f;
				UIElement uielement2 = e;
				uielement2.Left.Pixels = uielement2.Left.Pixels + 4f;
			}
			else
			{
				e = new UIText(memberInfo.Name + " not handled yet (" + type.Name + ")", 1f, false);
				UIElement uielement3 = e;
				uielement3.Top.Pixels = uielement3.Top.Pixels + 6f;
				UIElement uielement4 = e;
				uielement4.Left.Pixels = uielement4.Left.Pixels + 4f;
			}
			if (e != null)
			{
				ConfigElement configElement = e as ConfigElement;
				if (configElement != null)
				{
					configElement.Bind(memberInfo, item, (IList)list, index);
					configElement.OnBind();
				}
				e.Recalculate();
				int elementHeight = (int)e.GetOuterDimensions().Height;
				UIElement container = UIModConfig.GetContainer(e, (index == -1) ? order : index);
				container.Height.Pixels = (float)elementHeight;
				UIList uiList = parent as UIList;
				if (uiList != null)
				{
					uiList.Add(container);
					uiList.GetTotalHeight();
				}
				else
				{
					container.Top.Pixels = (float)top;
					container.Width.Pixels = -20f;
					container.Left.Pixels = 20f;
					top += elementHeight + 4;
					parent.Append(container);
					parent.Height.Set((float)top, 0f);
				}
				Tuple<UIElement, UIElement> tuple = new Tuple<UIElement, UIElement>(container, e);
				if (parent == Interface.modConfig.mainConfigList)
				{
					Interface.modConfig.mainConfigItems.Add(tuple);
				}
				return tuple;
			}
			return null;
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x00547B14 File Offset: 0x00545D14
		internal static UIElement GetContainer(UIElement containee, int sortid)
		{
			UISortableElement uisortableElement = new UISortableElement(sortid);
			uisortableElement.Width.Set(0f, 1f);
			uisortableElement.Height.Set(30f, 0f);
			uisortableElement.Append(containee);
			return uisortableElement;
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x00547B50 File Offset: 0x00545D50
		internal static UIPanel MakeSeparateListPanel(object item, object subitem, PropertyFieldWrapper memberInfo, IList array, int index, Func<string> AbridgedTextDisplayFunction)
		{
			UIPanel uIPanel = new UIPanel();
			uIPanel.CopyStyle(Interface.modConfig.uIPanel);
			uIPanel.BackgroundColor = UICommon.MainPanelBackground;
			BackgroundColorAttribute bca = ConfigManager.GetCustomAttributeFromMemberThenMemberType<BackgroundColorAttribute>(memberInfo, subitem, null);
			if (bca != null)
			{
				uIPanel.BackgroundColor = bca.Color;
			}
			UIList separateList = new UIList();
			separateList.CopyStyle(Interface.modConfig.mainConfigList);
			separateList.Height.Set(-40f, 1f);
			separateList.Top.Set(40f, 0f);
			uIPanel.Append(separateList);
			int top = 0;
			UIScrollbar uIScrollbar = new UIScrollbar();
			uIScrollbar.SetView(100f, 1000f);
			uIScrollbar.Height.Set(-40f, 1f);
			uIScrollbar.Top.Set(40f, 0f);
			uIScrollbar.HAlign = 1f;
			uIPanel.Append(uIScrollbar);
			separateList.SetScrollbar(uIScrollbar);
			string name = ConfigManager.GetLocalizedLabel(memberInfo);
			if (index != -1)
			{
				name = name + " #" + (index + 1).ToString();
			}
			Interface.modConfig.subPageStack.Push(name);
			name = string.Join(" > ", Interface.modConfig.subPageStack.Reverse<string>());
			UITextPanel<string> heading = new UITextPanel<string>(name, 1f, false);
			heading.HAlign = 0f;
			heading.Top.Set(-6f, 0f);
			heading.Height.Set(40f, 0f);
			uIPanel.Append(heading);
			UITextPanel<string> back = new UITextPanel<string>(Language.GetTextValue("tModLoader.ModConfigBack"), 1f, false)
			{
				HAlign = 1f
			};
			back.Width.Set(50f, 0f);
			back.Top.Set(-6f, 0f);
			back.OnLeftClick += delegate(UIMouseEvent a, UIElement c)
			{
				Interface.modConfig.uIElement.RemoveChild(uIPanel);
				Interface.modConfig.configPanelStack.Pop();
				Interface.modConfig.uIElement.Append(Interface.modConfig.configPanelStack.Peek());
			};
			back.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			uIPanel.Append(back);
			int order = 0;
			if (array != null)
			{
				memberInfo.Type.GetGenericArguments()[0].GetMethod("ToString", new Type[0]).DeclaringType != typeof(object);
			}
			else
			{
				memberInfo.Type.GetMethod("ToString", new Type[0]).DeclaringType != typeof(object);
			}
			if (AbridgedTextDisplayFunction != null)
			{
				UITextPanel<FuncStringWrapper> display = new UITextPanel<FuncStringWrapper>(new FuncStringWrapper(AbridgedTextDisplayFunction), 1f, false)
				{
					DrawPanel = true
				};
				display.Recalculate();
				UIElement container = UIModConfig.GetContainer(display, order++);
				container.Height.Pixels = (float)((int)display.GetOuterDimensions().Height);
				separateList.Add(container);
			}
			foreach (PropertyFieldWrapper variable in ConfigManager.GetFieldsAndProperties(subitem))
			{
				if (!Attribute.IsDefined(variable.MemberInfo, typeof(JsonIgnoreAttribute)) || Attribute.IsDefined(variable.MemberInfo, typeof(ShowDespiteJsonIgnoreAttribute)))
				{
					UIModConfig.HandleHeader(separateList, ref top, ref order, variable);
					UIModConfig.WrapIt(separateList, ref top, variable, subitem, order++, null, null, -1);
				}
			}
			Interface.modConfig.subPageStack.Pop();
			return uIPanel;
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x00547F10 File Offset: 0x00546110
		public static void HandleHeader(UIElement parent, ref int top, ref int order, PropertyFieldWrapper variable)
		{
			HeaderAttribute header = ConfigManager.GetLocalizedHeader(variable.MemberInfo);
			if (header != null)
			{
				PropertyFieldWrapper wrapper = new PropertyFieldWrapper(typeof(HeaderAttribute).GetProperty("Header"));
				PropertyFieldWrapper memberInfo = wrapper;
				object item = header;
				int num = order;
				order = num + 1;
				UIModConfig.WrapIt(parent, ref top, memberInfo, item, num, null, null, -1);
			}
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x00547F5C File Offset: 0x0054615C
		internal static void SwitchToSubConfig(UIPanel separateListPanel)
		{
			Interface.modConfig.uIElement.RemoveChild(Interface.modConfig.configPanelStack.Peek());
			Interface.modConfig.uIElement.Append(separateListPanel);
			Interface.modConfig.configPanelStack.Push(separateListPanel);
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x00547F9C File Offset: 0x0054619C
		internal void HandleOnCloseCallback()
		{
			if (this.modderOnClose != null)
			{
				this.modderOnClose();
				this.modderOnClose = null;
			}
		}

		// Token: 0x04001DC6 RID: 7622
		private UIElement uIElement;

		// Token: 0x04001DC7 RID: 7623
		private UITextPanel<string> headerTextPanel;

		// Token: 0x04001DC8 RID: 7624
		private UITextPanel<string> message;

		// Token: 0x04001DC9 RID: 7625
		private UITextPanel<string> previousConfigButton;

		// Token: 0x04001DCA RID: 7626
		private UITextPanel<string> nextConfigButton;

		// Token: 0x04001DCB RID: 7627
		private UITextPanel<string> saveConfigButton;

		// Token: 0x04001DCC RID: 7628
		private UITextPanel<string> backButton;

		// Token: 0x04001DCD RID: 7629
		private UITextPanel<string> revertConfigButton;

		// Token: 0x04001DCE RID: 7630
		private UITextPanel<string> restoreDefaultsConfigButton;

		// Token: 0x04001DCF RID: 7631
		private UIPanel uIPanel;

		// Token: 0x04001DD0 RID: 7632
		private readonly List<Tuple<UIElement, UIElement>> mainConfigItems = new List<Tuple<UIElement, UIElement>>();

		// Token: 0x04001DD1 RID: 7633
		private UIList mainConfigList;

		// Token: 0x04001DD2 RID: 7634
		private UIScrollbar uIScrollbar;

		// Token: 0x04001DD3 RID: 7635
		private readonly Stack<UIPanel> configPanelStack = new Stack<UIPanel>();

		// Token: 0x04001DD4 RID: 7636
		private readonly Stack<string> subPageStack = new Stack<string>();

		// Token: 0x04001DD5 RID: 7637
		private Mod mod;

		// Token: 0x04001DD6 RID: 7638
		private List<ModConfig> sortedModConfigs;

		// Token: 0x04001DD7 RID: 7639
		private ModConfig modConfig;

		// Token: 0x04001DD8 RID: 7640
		internal ModConfig pendingConfig;

		// Token: 0x04001DD9 RID: 7641
		private bool updateNeeded;

		// Token: 0x04001DDA RID: 7642
		private bool preserveNotificationMessage;

		// Token: 0x04001DDB RID: 7643
		private UIFocusInputTextField filterTextField;

		// Token: 0x04001DDC RID: 7644
		internal string scrollToOption;

		// Token: 0x04001DDD RID: 7645
		internal bool centerScrolledOption;

		// Token: 0x04001DDE RID: 7646
		private bool openedFromModder;

		// Token: 0x04001DDF RID: 7647
		private Action modderOnClose;

		// Token: 0x04001DE0 RID: 7648
		private bool pendingChanges;

		// Token: 0x04001DE1 RID: 7649
		private bool pendingChangesUIUpdate;

		// Token: 0x04001DE2 RID: 7650
		private bool netUpdate;

		// Token: 0x04001DE4 RID: 7652
		private static bool pendingRevertDefaults;
	}
}
