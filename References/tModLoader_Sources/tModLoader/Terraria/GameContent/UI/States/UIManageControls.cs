using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004DC RID: 1244
	public class UIManageControls : UIState
	{
		// Token: 0x06003BEA RID: 15338 RVA: 0x005B9598 File Offset: 0x005B7798
		public override void OnInitialize()
		{
			this._KeyboardGamepadTexture = Main.Assets.Request<Texture2D>("Images/UI/Settings_Inputs");
			this._keyboardGamepadBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Settings_Inputs_Border");
			this._GameplayVsUITexture = Main.Assets.Request<Texture2D>("Images/UI/Settings_Inputs_2");
			this._GameplayVsUIBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Settings_Inputs_2_Border");
			UIElement uIElement = new UIElement();
			uIElement.Width.Set(0f, 0.8f);
			uIElement.MaxWidth.Set(600f, 0f);
			uIElement.Top.Set(220f, 0f);
			uIElement.Height.Set(-200f, 1f);
			uIElement.HAlign = 0.5f;
			this._outerContainer = uIElement;
			UIPanel uIPanel = new UIPanel();
			uIPanel.Width.Set(0f, 1f);
			uIPanel.Height.Set(-110f, 1f);
			uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uIElement.Append(uIPanel);
			this._buttonKeyboard = new UIImageFramed(this._KeyboardGamepadTexture, this._KeyboardGamepadTexture.Frame(2, 2, 0, 0, 0, 0));
			this._buttonKeyboard.VAlign = 0f;
			this._buttonKeyboard.HAlign = 0f;
			this._buttonKeyboard.Left.Set(0f, 0f);
			this._buttonKeyboard.Top.Set(8f, 0f);
			this._buttonKeyboard.OnLeftClick += this.KeyboardButtonClick;
			this._buttonKeyboard.OnMouseOver += this.ManageBorderKeyboardOn;
			this._buttonKeyboard.OnMouseOut += this.ManageBorderKeyboardOff;
			uIPanel.Append(this._buttonKeyboard);
			this._buttonGamepad = new UIImageFramed(this._KeyboardGamepadTexture, this._KeyboardGamepadTexture.Frame(2, 2, 1, 1, 0, 0));
			this._buttonGamepad.VAlign = 0f;
			this._buttonGamepad.HAlign = 0f;
			this._buttonGamepad.Left.Set(76f, 0f);
			this._buttonGamepad.Top.Set(8f, 0f);
			this._buttonGamepad.OnLeftClick += this.GamepadButtonClick;
			this._buttonGamepad.OnMouseOver += this.ManageBorderGamepadOn;
			this._buttonGamepad.OnMouseOut += this.ManageBorderGamepadOff;
			uIPanel.Append(this._buttonGamepad);
			this._buttonBorder1 = new UIImageFramed(this._keyboardGamepadBorderTexture, this._keyboardGamepadBorderTexture.Frame(1, 1, 0, 0, 0, 0));
			this._buttonBorder1.VAlign = 0f;
			this._buttonBorder1.HAlign = 0f;
			this._buttonBorder1.Left.Set(0f, 0f);
			this._buttonBorder1.Top.Set(8f, 0f);
			this._buttonBorder1.Color = Color.Silver;
			this._buttonBorder1.IgnoresMouseInteraction = true;
			uIPanel.Append(this._buttonBorder1);
			this._buttonBorder2 = new UIImageFramed(this._keyboardGamepadBorderTexture, this._keyboardGamepadBorderTexture.Frame(1, 1, 0, 0, 0, 0));
			this._buttonBorder2.VAlign = 0f;
			this._buttonBorder2.HAlign = 0f;
			this._buttonBorder2.Left.Set(76f, 0f);
			this._buttonBorder2.Top.Set(8f, 0f);
			this._buttonBorder2.Color = Color.Transparent;
			this._buttonBorder2.IgnoresMouseInteraction = true;
			uIPanel.Append(this._buttonBorder2);
			this._buttonVs1 = new UIImageFramed(this._GameplayVsUITexture, this._GameplayVsUITexture.Frame(2, 2, 0, 0, 0, 0));
			this._buttonVs1.VAlign = 0f;
			this._buttonVs1.HAlign = 0f;
			this._buttonVs1.Left.Set(172f, 0f);
			this._buttonVs1.Top.Set(8f, 0f);
			this._buttonVs1.OnLeftClick += this.VsGameplayButtonClick;
			this._buttonVs1.OnMouseOver += this.ManageBorderGameplayOn;
			this._buttonVs1.OnMouseOut += this.ManageBorderGameplayOff;
			uIPanel.Append(this._buttonVs1);
			this._buttonVs2 = new UIImageFramed(this._GameplayVsUITexture, this._GameplayVsUITexture.Frame(2, 2, 1, 1, 0, 0));
			this._buttonVs2.VAlign = 0f;
			this._buttonVs2.HAlign = 0f;
			this._buttonVs2.Left.Set(212f, 0f);
			this._buttonVs2.Top.Set(8f, 0f);
			this._buttonVs2.OnLeftClick += this.VsMenuButtonClick;
			this._buttonVs2.OnMouseOver += this.ManageBorderMenuOn;
			this._buttonVs2.OnMouseOut += this.ManageBorderMenuOff;
			uIPanel.Append(this._buttonVs2);
			this._buttonBorderVs1 = new UIImageFramed(this._GameplayVsUIBorderTexture, this._GameplayVsUIBorderTexture.Frame(1, 1, 0, 0, 0, 0));
			this._buttonBorderVs1.VAlign = 0f;
			this._buttonBorderVs1.HAlign = 0f;
			this._buttonBorderVs1.Left.Set(172f, 0f);
			this._buttonBorderVs1.Top.Set(8f, 0f);
			this._buttonBorderVs1.Color = Color.Silver;
			this._buttonBorderVs1.IgnoresMouseInteraction = true;
			uIPanel.Append(this._buttonBorderVs1);
			this._buttonBorderVs2 = new UIImageFramed(this._GameplayVsUIBorderTexture, this._GameplayVsUIBorderTexture.Frame(1, 1, 0, 0, 0, 0));
			this._buttonBorderVs2.VAlign = 0f;
			this._buttonBorderVs2.HAlign = 0f;
			this._buttonBorderVs2.Left.Set(212f, 0f);
			this._buttonBorderVs2.Top.Set(8f, 0f);
			this._buttonBorderVs2.Color = Color.Transparent;
			this._buttonBorderVs2.IgnoresMouseInteraction = true;
			uIPanel.Append(this._buttonBorderVs2);
			this._buttonProfile = new UIKeybindingSimpleListItem(() => PlayerInput.CurrentProfile.ShowName, new Color(73, 94, 171, 255) * 0.9f);
			this._buttonProfile.VAlign = 0f;
			this._buttonProfile.HAlign = 1f;
			this._buttonProfile.Width.Set(180f, 0f);
			this._buttonProfile.Height.Set(30f, 0f);
			this._buttonProfile.MarginRight = 30f;
			this._buttonProfile.Left.Set(0f, 0f);
			this._buttonProfile.Top.Set(8f, 0f);
			this._buttonProfile.OnLeftClick += this.profileButtonClick;
			uIPanel.Append(this._buttonProfile);
			this._uilist = new UIList();
			this._uilist.Width.Set(-25f, 1f);
			this._uilist.Height.Set(-50f, 1f);
			this._uilist.VAlign = 1f;
			this._uilist.PaddingBottom = 5f;
			this._uilist.ListPadding = 20f;
			uIPanel.Append(this._uilist);
			this.AssembleBindPanels();
			this.FillList();
			UIScrollbar uIScrollbar = new UIScrollbar();
			uIScrollbar.SetView(100f, 1000f);
			uIScrollbar.Height.Set(-67f, 1f);
			uIScrollbar.HAlign = 1f;
			uIScrollbar.VAlign = 1f;
			uIScrollbar.MarginBottom = 11f;
			uIPanel.Append(uIScrollbar);
			this._uilist.SetScrollbar(uIScrollbar);
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Keybindings"), 0.7f, true);
			uITextPanel.HAlign = 0.5f;
			uITextPanel.Top.Set(-45f, 0f);
			uITextPanel.Left.Set(-10f, 0f);
			uITextPanel.SetPadding(15f);
			uITextPanel.BackgroundColor = new Color(73, 94, 171);
			uIElement.Append(uITextPanel);
			UITextPanel<LocalizedText> uITextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uITextPanel2.Width.Set(-10f, 0.5f);
			uITextPanel2.Height.Set(50f, 0f);
			uITextPanel2.VAlign = 1f;
			uITextPanel2.HAlign = 0.5f;
			uITextPanel2.Top.Set(-45f, 0f);
			uITextPanel2.OnMouseOver += this.FadedMouseOver;
			uITextPanel2.OnMouseOut += this.FadedMouseOut;
			uITextPanel2.OnLeftClick += this.GoBackClick;
			uIElement.Append(uITextPanel2);
			this._buttonBack = uITextPanel2;
			base.Append(uIElement);
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x005B9F68 File Offset: 0x005B8168
		private void AssembleBindPanels()
		{
			List<string> bindings = new List<string>
			{
				"MouseLeft",
				"MouseRight",
				"MouseMiddle",
				"MouseXButton1",
				"MouseXButton2",
				"Up",
				"Down",
				"Left",
				"Right",
				"Jump",
				"Grapple",
				"SmartSelect",
				"SmartCursor",
				"QuickMount",
				"QuickHeal",
				"QuickMana",
				"QuickBuff",
				"Throw",
				"Inventory",
				"ToggleCreativeMenu",
				"ViewZoomIn",
				"ViewZoomOut",
				"Loadout1",
				"Loadout2",
				"Loadout3",
				"ToggleCameraMode",
				"sp9"
			};
			List<string> bindings2 = new List<string>
			{
				"MouseLeft",
				"MouseRight",
				"MouseMiddle",
				"MouseXButton1",
				"MouseXButton2",
				"Up",
				"Down",
				"Left",
				"Right",
				"Jump",
				"Grapple",
				"SmartSelect",
				"SmartCursor",
				"QuickMount",
				"QuickHeal",
				"QuickMana",
				"QuickBuff",
				"LockOn",
				"Throw",
				"Inventory",
				"Loadout1",
				"Loadout2",
				"Loadout3",
				"ToggleCameraMode",
				"sp9"
			};
			List<string> bindings3 = new List<string>
			{
				"HotbarMinus",
				"HotbarPlus",
				"Hotbar1",
				"Hotbar2",
				"Hotbar3",
				"Hotbar4",
				"Hotbar5",
				"Hotbar6",
				"Hotbar7",
				"Hotbar8",
				"Hotbar9",
				"Hotbar10",
				"sp10"
			};
			List<string> bindings4 = new List<string>
			{
				"MapZoomIn",
				"MapZoomOut",
				"MapAlphaUp",
				"MapAlphaDown",
				"MapFull",
				"MapStyle",
				"sp11"
			};
			List<string> bindings5 = new List<string>
			{
				"sp1",
				"sp2",
				"RadialHotbar",
				"RadialQuickbar",
				"sp12"
			};
			List<string> bindings6 = new List<string>
			{
				"sp3",
				"sp4",
				"sp5",
				"sp6",
				"sp7",
				"sp8",
				"sp14",
				"sp15",
				"sp16",
				"sp17",
				"sp18",
				"sp19",
				"sp13"
			};
			this.OnAssembleBindPanels();
			InputMode currentInputMode = InputMode.Keyboard;
			this._bindsKeyboard.Add(this.CreateBindingGroup(0, bindings, currentInputMode));
			this._bindsKeyboard.Add(this.CreateBindingGroup(1, bindings4, currentInputMode));
			this._bindsKeyboard.Add(this.CreateBindingGroup(2, bindings3, currentInputMode));
			currentInputMode = InputMode.XBoxGamepad;
			this._bindsGamepad.Add(this.CreateBindingGroup(0, bindings2, currentInputMode));
			this._bindsGamepad.Add(this.CreateBindingGroup(1, bindings4, currentInputMode));
			this._bindsGamepad.Add(this.CreateBindingGroup(2, bindings3, currentInputMode));
			this._bindsGamepad.Add(this.CreateBindingGroup(3, bindings5, currentInputMode));
			this._bindsGamepad.Add(this.CreateBindingGroup(4, bindings6, currentInputMode));
			currentInputMode = InputMode.KeyboardUI;
			this._bindsKeyboardUI.Add(this.CreateBindingGroup(0, bindings, currentInputMode));
			this._bindsKeyboardUI.Add(this.CreateBindingGroup(1, bindings4, currentInputMode));
			this._bindsKeyboardUI.Add(this.CreateBindingGroup(2, bindings3, currentInputMode));
			currentInputMode = InputMode.XBoxGamepadUI;
			this._bindsGamepadUI.Add(this.CreateBindingGroup(0, bindings2, currentInputMode));
			this._bindsGamepadUI.Add(this.CreateBindingGroup(1, bindings4, currentInputMode));
			this._bindsGamepadUI.Add(this.CreateBindingGroup(2, bindings3, currentInputMode));
			this._bindsGamepadUI.Add(this.CreateBindingGroup(3, bindings5, currentInputMode));
			this._bindsGamepadUI.Add(this.CreateBindingGroup(4, bindings6, currentInputMode));
			this.AddModBindingGroups();
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x005BA4E8 File Offset: 0x005B86E8
		private UISortableElement CreateBindingGroup(int elementIndex, List<string> bindings, InputMode currentInputMode)
		{
			UISortableElement uISortableElement = new UISortableElement(elementIndex);
			uISortableElement.HAlign = 0.5f;
			uISortableElement.Width.Set(0f, 1f);
			uISortableElement.Height.Set(2000f, 0f);
			UIPanel uIPanel = new UIPanel();
			uIPanel.Width.Set(0f, 1f);
			uIPanel.Height.Set(-16f, 1f);
			uIPanel.VAlign = 1f;
			uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uISortableElement.Append(uIPanel);
			UIList uIList = new UIList();
			uIList.OverflowHidden = false;
			uIList.Width.Set(0f, 1f);
			uIList.Height.Set(-8f, 1f);
			uIList.VAlign = 1f;
			uIList.ListPadding = 5f;
			uIPanel.Append(uIList);
			Color backgroundColor = uIPanel.BackgroundColor;
			switch (elementIndex)
			{
			case 0:
				uIPanel.BackgroundColor = Color.Lerp(uIPanel.BackgroundColor, Color.Green, 0.18f);
				break;
			case 1:
				uIPanel.BackgroundColor = Color.Lerp(uIPanel.BackgroundColor, Color.Goldenrod, 0.18f);
				break;
			case 2:
				uIPanel.BackgroundColor = Color.Lerp(uIPanel.BackgroundColor, Color.HotPink, 0.18f);
				break;
			case 3:
				uIPanel.BackgroundColor = Color.Lerp(uIPanel.BackgroundColor, Color.Indigo, 0.18f);
				break;
			case 4:
				uIPanel.BackgroundColor = Color.Lerp(uIPanel.BackgroundColor, Color.Turquoise, 0.18f);
				break;
			case 5:
				uIPanel.BackgroundColor = Color.Lerp(uIPanel.BackgroundColor, Color.Yellow, 0.18f);
				break;
			}
			this.CreateElementGroup(uIList, bindings, currentInputMode, uIPanel.BackgroundColor);
			uIPanel.BackgroundColor = uIPanel.BackgroundColor.MultiplyRGBA(new Color(111, 111, 111));
			LocalizedText text = LocalizedText.Empty;
			switch (elementIndex)
			{
			case 0:
				text = ((currentInputMode == InputMode.Keyboard || currentInputMode == InputMode.XBoxGamepad) ? Lang.menu[164] : Lang.menu[243]);
				break;
			case 1:
				text = Lang.menu[165];
				break;
			case 2:
				text = Lang.menu[166];
				break;
			case 3:
				text = Lang.menu[167];
				break;
			case 4:
				text = Lang.menu[198];
				break;
			case 5:
				text = Language.GetText("tModLoader.ModControls");
				break;
			}
			UITextPanel<LocalizedText> element = new UITextPanel<LocalizedText>(text, 0.7f, false)
			{
				VAlign = 0f,
				HAlign = 0.5f
			};
			uISortableElement.Append(element);
			uISortableElement.Recalculate();
			float totalHeight = uIList.GetTotalHeight();
			uISortableElement.Width.Set(0f, 1f);
			uISortableElement.Height.Set(totalHeight + 30f + 16f, 0f);
			return uISortableElement;
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x005BA7E0 File Offset: 0x005B89E0
		private void CreateElementGroup(UIList parent, List<string> bindings, InputMode currentInputMode, Color color)
		{
			for (int i = 0; i < bindings.Count; i++)
			{
				string text = bindings[i];
				UISortableElement uISortableElement = new UISortableElement(i);
				uISortableElement.Width.Set(0f, 1f);
				uISortableElement.Height.Set(30f, 0f);
				uISortableElement.HAlign = 0.5f;
				parent.Add(uISortableElement);
				if (UIManageControls._BindingsHalfSingleLine.Contains(bindings[i]))
				{
					UIElement uIElement = this.CreatePanel(bindings[i], currentInputMode, color);
					uIElement.Width.Set(0f, 0.5f);
					uIElement.HAlign = 0.5f;
					uIElement.Height.Set(0f, 1f);
					uIElement.SetSnapPoint("Wide", UIManageControls.SnapPointIndex++, null, null);
					uISortableElement.Append(uIElement);
				}
				else if (UIManageControls._BindingsFullLine.Contains(bindings[i]))
				{
					UIElement uIElement2 = this.CreatePanel(bindings[i], currentInputMode, color);
					uIElement2.Width.Set(0f, 1f);
					uIElement2.Height.Set(0f, 1f);
					uIElement2.SetSnapPoint("Wide", UIManageControls.SnapPointIndex++, null, null);
					uISortableElement.Append(uIElement2);
				}
				else if (UIManageControls._ModNames.Contains(bindings[i]))
				{
					UIElement header = new HeaderElement(bindings[i]);
					header.Width.Set(0f, 1f);
					header.Height.Set(0f, 1f);
					header.SetSnapPoint("Wide", UIManageControls.SnapPointIndex++, null, null);
					uISortableElement.Append(header);
				}
				else
				{
					UIElement uIElement3 = this.CreatePanel(bindings[i], currentInputMode, color);
					uIElement3.Width.Set(-5f, 0.5f);
					uIElement3.Height.Set(0f, 1f);
					uIElement3.SetSnapPoint("Thin", UIManageControls.SnapPointIndex++, null, null);
					uISortableElement.Append(uIElement3);
					i++;
					if (i < bindings.Count)
					{
						uIElement3 = this.CreatePanel(bindings[i], currentInputMode, color);
						uIElement3.Width.Set(-5f, 0.5f);
						uIElement3.Height.Set(0f, 1f);
						uIElement3.HAlign = 1f;
						uIElement3.SetSnapPoint("Thin", UIManageControls.SnapPointIndex++, null, null);
						uISortableElement.Append(uIElement3);
					}
				}
			}
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x005BAAE8 File Offset: 0x005B8CE8
		public UIElement CreatePanel(string bind, InputMode currentInputMode, Color color)
		{
			if (bind != null)
			{
				int length = bind.Length;
				if (length != 3)
				{
					if (length == 4)
					{
						switch (bind[3])
						{
						case '0':
							if (bind == "sp10")
							{
								UIKeybindingSimpleListItem uikeybindingSimpleListItem = new UIKeybindingSimpleListItem(() => Lang.menu[86].Value, color);
								uikeybindingSimpleListItem.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
								{
									string copyableProfileName = UIManageControls.GetCopyableProfileName();
									PlayerInput.CurrentProfile.CopyHotbarSettingsFrom(PlayerInput.OriginalProfiles[copyableProfileName], currentInputMode);
								};
								return uikeybindingSimpleListItem;
							}
							break;
						case '1':
							if (bind == "sp11")
							{
								UIKeybindingSimpleListItem uikeybindingSimpleListItem2 = new UIKeybindingSimpleListItem(() => Lang.menu[86].Value, color);
								uikeybindingSimpleListItem2.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
								{
									string copyableProfileName4 = UIManageControls.GetCopyableProfileName();
									PlayerInput.CurrentProfile.CopyMapSettingsFrom(PlayerInput.OriginalProfiles[copyableProfileName4], currentInputMode);
								};
								return uikeybindingSimpleListItem2;
							}
							break;
						case '2':
							if (bind == "sp12")
							{
								UIKeybindingSimpleListItem uikeybindingSimpleListItem3 = new UIKeybindingSimpleListItem(() => Lang.menu[86].Value, color);
								uikeybindingSimpleListItem3.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
								{
									string copyableProfileName5 = UIManageControls.GetCopyableProfileName();
									PlayerInput.CurrentProfile.CopyGamepadSettingsFrom(PlayerInput.OriginalProfiles[copyableProfileName5], currentInputMode);
								};
								return uikeybindingSimpleListItem3;
							}
							break;
						case '3':
							if (bind == "sp13")
							{
								UIKeybindingSimpleListItem uikeybindingSimpleListItem4 = new UIKeybindingSimpleListItem(() => Lang.menu[86].Value, color);
								uikeybindingSimpleListItem4.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
								{
									string copyableProfileName2 = UIManageControls.GetCopyableProfileName();
									PlayerInput.CurrentProfile.CopyGamepadAdvancedSettingsFrom(PlayerInput.OriginalProfiles[copyableProfileName2], currentInputMode);
								};
								return uikeybindingSimpleListItem4;
							}
							break;
						case '4':
							if (bind == "sp14")
							{
								UIKeybindingToggleListItem uikeybindingToggleListItem = new UIKeybindingToggleListItem(() => Lang.menu[205].Value, () => PlayerInput.CurrentProfile.LeftThumbstickInvertX, color);
								uikeybindingToggleListItem.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
								{
									if (PlayerInput.CurrentProfile.AllowEditting)
									{
										PlayerInput.CurrentProfile.LeftThumbstickInvertX = !PlayerInput.CurrentProfile.LeftThumbstickInvertX;
									}
								};
								return uikeybindingToggleListItem;
							}
							break;
						case '5':
							if (bind == "sp15")
							{
								UIKeybindingToggleListItem uikeybindingToggleListItem2 = new UIKeybindingToggleListItem(() => Lang.menu[206].Value, () => PlayerInput.CurrentProfile.LeftThumbstickInvertY, color);
								uikeybindingToggleListItem2.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
								{
									if (PlayerInput.CurrentProfile.AllowEditting)
									{
										PlayerInput.CurrentProfile.LeftThumbstickInvertY = !PlayerInput.CurrentProfile.LeftThumbstickInvertY;
									}
								};
								return uikeybindingToggleListItem2;
							}
							break;
						case '6':
							if (bind == "sp16")
							{
								UIKeybindingToggleListItem uikeybindingToggleListItem3 = new UIKeybindingToggleListItem(() => Lang.menu[207].Value, () => PlayerInput.CurrentProfile.RightThumbstickInvertX, color);
								uikeybindingToggleListItem3.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
								{
									if (PlayerInput.CurrentProfile.AllowEditting)
									{
										PlayerInput.CurrentProfile.RightThumbstickInvertX = !PlayerInput.CurrentProfile.RightThumbstickInvertX;
									}
								};
								return uikeybindingToggleListItem3;
							}
							break;
						case '7':
							if (bind == "sp17")
							{
								UIKeybindingToggleListItem uikeybindingToggleListItem4 = new UIKeybindingToggleListItem(() => Lang.menu[208].Value, () => PlayerInput.CurrentProfile.RightThumbstickInvertY, color);
								uikeybindingToggleListItem4.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
								{
									if (PlayerInput.CurrentProfile.AllowEditting)
									{
										PlayerInput.CurrentProfile.RightThumbstickInvertY = !PlayerInput.CurrentProfile.RightThumbstickInvertY;
									}
								};
								return uikeybindingToggleListItem4;
							}
							break;
						case '8':
							if (bind == "sp18")
							{
								return new UIKeybindingSliderItem(delegate()
								{
									int hotbarRadialHoldTimeRequired = PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired;
									if (hotbarRadialHoldTimeRequired != -1)
									{
										return Lang.menu[227].Value + " (" + ((float)hotbarRadialHoldTimeRequired / 60f).ToString("F2") + "s)";
									}
									return Lang.menu[228].Value;
								}, delegate()
								{
									if (PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired != -1)
									{
										return (float)PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired / 301f;
									}
									return 1f;
								}, delegate(float f)
								{
									PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired = (int)(f * 301f);
									if ((float)PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired == 301f)
									{
										PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired = -1;
									}
								}, delegate()
								{
									float currentValue = (PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired == -1) ? 1f : ((float)PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired / 301f);
									currentValue = UILinksInitializer.HandleSliderHorizontalInput(currentValue, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f);
									PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired = (int)(currentValue * 301f);
									if ((float)PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired == 301f)
									{
										PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired = -1;
									}
								}, 1007, color);
							}
							break;
						case '9':
							if (bind == "sp19")
							{
								return new UIKeybindingSliderItem(delegate()
								{
									int inventoryMoveCD = PlayerInput.CurrentProfile.InventoryMoveCD;
									return Lang.menu[252].Value + " (" + ((float)inventoryMoveCD / 60f).ToString("F2") + "s)";
								}, () => Utils.GetLerpValue(4f, 12f, (float)PlayerInput.CurrentProfile.InventoryMoveCD, true), delegate(float f)
								{
									PlayerInput.CurrentProfile.InventoryMoveCD = (int)Math.Round((double)MathHelper.Lerp(4f, 12f, f));
								}, delegate()
								{
									if (UILinkPointNavigator.Shortcuts.INV_MOVE_OPTION_CD > 0)
									{
										UILinkPointNavigator.Shortcuts.INV_MOVE_OPTION_CD--;
									}
									if (UILinkPointNavigator.Shortcuts.INV_MOVE_OPTION_CD == 0)
									{
										float lerpValue = Utils.GetLerpValue(4f, 12f, (float)PlayerInput.CurrentProfile.InventoryMoveCD, true);
										float num = UILinksInitializer.HandleSliderHorizontalInput(lerpValue, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f);
										if (lerpValue != num)
										{
											UILinkPointNavigator.Shortcuts.INV_MOVE_OPTION_CD = 8;
											int num2 = Math.Sign(num - lerpValue);
											PlayerInput.CurrentProfile.InventoryMoveCD = (int)MathHelper.Clamp((float)(PlayerInput.CurrentProfile.InventoryMoveCD + num2), 4f, 12f);
										}
									}
								}, 1008, color);
							}
							break;
						}
					}
				}
				else
				{
					switch (bind[2])
					{
					case '1':
						if (bind == "sp1")
						{
							UIKeybindingToggleListItem uikeybindingToggleListItem5 = new UIKeybindingToggleListItem(() => Lang.menu[196].Value, () => PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"].Contains(1.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"].Contains(8.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"].Contains(2.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"].Contains(4.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"].Contains(1.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"].Contains(8.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"].Contains(2.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"].Contains(4.ToString()), color);
							uikeybindingToggleListItem5.OnLeftClick += this.SnapButtonClick;
							return uikeybindingToggleListItem5;
						}
						break;
					case '2':
						if (bind == "sp2")
						{
							UIKeybindingToggleListItem uikeybindingToggleListItem6 = new UIKeybindingToggleListItem(() => Lang.menu[197].Value, () => PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"].Contains(1.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"].Contains(8.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"].Contains(2.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"].Contains(4.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"].Contains(1.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"].Contains(8.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"].Contains(2.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"].Contains(4.ToString()), color);
							uikeybindingToggleListItem6.OnLeftClick += this.RadialButtonClick;
							return uikeybindingToggleListItem6;
						}
						break;
					case '3':
						if (bind == "sp3")
						{
							return new UIKeybindingSliderItem(() => Lang.menu[199].Value + " (" + PlayerInput.CurrentProfile.TriggersDeadzone.ToString("P1") + ")", () => PlayerInput.CurrentProfile.TriggersDeadzone, delegate(float f)
							{
								PlayerInput.CurrentProfile.TriggersDeadzone = f;
							}, delegate()
							{
								PlayerInput.CurrentProfile.TriggersDeadzone = UILinksInitializer.HandleSliderHorizontalInput(PlayerInput.CurrentProfile.TriggersDeadzone, 0f, 0.95f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
							}, 1000, color);
						}
						break;
					case '4':
						if (bind == "sp4")
						{
							return new UIKeybindingSliderItem(() => Lang.menu[200].Value + " (" + PlayerInput.CurrentProfile.InterfaceDeadzoneX.ToString("P1") + ")", () => PlayerInput.CurrentProfile.InterfaceDeadzoneX, delegate(float f)
							{
								PlayerInput.CurrentProfile.InterfaceDeadzoneX = f;
							}, delegate()
							{
								PlayerInput.CurrentProfile.InterfaceDeadzoneX = UILinksInitializer.HandleSliderHorizontalInput(PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0f, 0.95f, 0.35f, 0.35f);
							}, 1001, color);
						}
						break;
					case '5':
						if (bind == "sp5")
						{
							return new UIKeybindingSliderItem(() => Lang.menu[201].Value + " (" + PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX.ToString("P1") + ")", () => PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX, delegate(float f)
							{
								PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX = f;
							}, delegate()
							{
								PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX = UILinksInitializer.HandleSliderHorizontalInput(PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX, 0f, 0.95f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
							}, 1002, color);
						}
						break;
					case '6':
						if (bind == "sp6")
						{
							return new UIKeybindingSliderItem(() => Lang.menu[202].Value + " (" + PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY.ToString("P1") + ")", () => PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY, delegate(float f)
							{
								PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY = f;
							}, delegate()
							{
								PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY = UILinksInitializer.HandleSliderHorizontalInput(PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY, 0f, 0.95f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
							}, 1003, color);
						}
						break;
					case '7':
						if (bind == "sp7")
						{
							return new UIKeybindingSliderItem(() => Lang.menu[203].Value + " (" + PlayerInput.CurrentProfile.RightThumbstickDeadzoneX.ToString("P1") + ")", () => PlayerInput.CurrentProfile.RightThumbstickDeadzoneX, delegate(float f)
							{
								PlayerInput.CurrentProfile.RightThumbstickDeadzoneX = f;
							}, delegate()
							{
								PlayerInput.CurrentProfile.RightThumbstickDeadzoneX = UILinksInitializer.HandleSliderHorizontalInput(PlayerInput.CurrentProfile.RightThumbstickDeadzoneX, 0f, 0.95f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
							}, 1004, color);
						}
						break;
					case '8':
						if (bind == "sp8")
						{
							return new UIKeybindingSliderItem(() => Lang.menu[204].Value + " (" + PlayerInput.CurrentProfile.RightThumbstickDeadzoneY.ToString("P1") + ")", () => PlayerInput.CurrentProfile.RightThumbstickDeadzoneY, delegate(float f)
							{
								PlayerInput.CurrentProfile.RightThumbstickDeadzoneY = f;
							}, delegate()
							{
								PlayerInput.CurrentProfile.RightThumbstickDeadzoneY = UILinksInitializer.HandleSliderHorizontalInput(PlayerInput.CurrentProfile.RightThumbstickDeadzoneY, 0f, 0.95f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
							}, 1005, color);
						}
						break;
					case '9':
						if (bind == "sp9")
						{
							UIKeybindingSimpleListItem uikeybindingSimpleListItem5 = new UIKeybindingSimpleListItem(() => Lang.menu[86].Value, color);
							uikeybindingSimpleListItem5.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
							{
								string copyableProfileName3 = UIManageControls.GetCopyableProfileName();
								PlayerInput.CurrentProfile.CopyGameplaySettingsFrom(PlayerInput.OriginalProfiles[copyableProfileName3], currentInputMode);
							};
							return uikeybindingSimpleListItem5;
						}
						break;
					}
				}
			}
			UIElement tmlResult = this.HandlePanelCreation(bind, currentInputMode, color);
			if (tmlResult != null)
			{
				return tmlResult;
			}
			return new UIKeybindingListItem(bind, currentInputMode, color);
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x005BB4F4 File Offset: 0x005B96F4
		public override void OnActivate()
		{
			this._bindsKeyboard.Clear();
			this._bindsGamepad.Clear();
			this._bindsKeyboardUI.Clear();
			this._bindsGamepadUI.Clear();
			this.AssembleBindPanels();
			this.FillList();
			if (Main.gameMenu)
			{
				this._outerContainer.Top.Set(220f, 0f);
				this._outerContainer.Height.Set(-220f, 1f);
			}
			else
			{
				this._outerContainer.Top.Set(120f, 0f);
				this._outerContainer.Height.Set(-120f, 1f);
			}
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3002);
			}
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x005BB5BC File Offset: 0x005B97BC
		private static string GetCopyableProfileName()
		{
			string result = "Redigit's Pick";
			if (PlayerInput.OriginalProfiles.ContainsKey(PlayerInput.CurrentProfile.Name))
			{
				result = PlayerInput.CurrentProfile.Name;
			}
			return result;
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x005BB5F4 File Offset: 0x005B97F4
		private void FillList()
		{
			List<UIElement> list = this._bindsKeyboard;
			if (!this.OnKeyboard)
			{
				list = this._bindsGamepad;
			}
			if (!this.OnGameplay)
			{
				list = (this.OnKeyboard ? this._bindsKeyboardUI : this._bindsGamepadUI);
			}
			this._uilist.Clear();
			foreach (UIElement item in list)
			{
				this._uilist.Add(item);
			}
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x005BB688 File Offset: 0x005B9888
		private void SnapButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (PlayerInput.CurrentProfile.AllowEditting)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"].Contains(1.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"].Contains(8.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"].Contains(2.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"].Contains(4.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"].Contains(1.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"].Contains(8.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"].Contains(2.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"].Contains(4.ToString()))
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"].Clear();
					return;
				}
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"] = new List<string>
				{
					1.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"] = new List<string>
				{
					8.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"] = new List<string>
				{
					2.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"] = new List<string>
				{
					4.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"] = new List<string>
				{
					1.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"] = new List<string>
				{
					8.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"] = new List<string>
				{
					2.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"] = new List<string>
				{
					4.ToString()
				};
			}
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x005BBC84 File Offset: 0x005B9E84
		private void RadialButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (PlayerInput.CurrentProfile.AllowEditting)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"].Contains(1.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"].Contains(8.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"].Contains(2.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"].Contains(4.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"].Contains(1.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"].Contains(8.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"].Contains(2.ToString()) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"].Contains(4.ToString()))
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"].Clear();
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"].Clear();
					return;
				}
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"].Clear();
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"] = new List<string>
				{
					1.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"] = new List<string>
				{
					8.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"] = new List<string>
				{
					2.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"] = new List<string>
				{
					4.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"] = new List<string>
				{
					1.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"] = new List<string>
				{
					8.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"] = new List<string>
				{
					2.ToString()
				};
				PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"] = new List<string>
				{
					4.ToString()
				};
			}
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x005BC280 File Offset: 0x005BA480
		private void KeyboardButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonKeyboard.SetFrame(this._KeyboardGamepadTexture.Frame(2, 2, 0, 0, 0, 0));
			this._buttonGamepad.SetFrame(this._KeyboardGamepadTexture.Frame(2, 2, 1, 1, 0, 0));
			this.OnKeyboard = true;
			this.FillList();
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x005BC2D4 File Offset: 0x005BA4D4
		private void GamepadButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonKeyboard.SetFrame(this._KeyboardGamepadTexture.Frame(2, 2, 0, 1, 0, 0));
			this._buttonGamepad.SetFrame(this._KeyboardGamepadTexture.Frame(2, 2, 1, 0, 0, 0));
			this.OnKeyboard = false;
			this.FillList();
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x005BC326 File Offset: 0x005BA526
		private void ManageBorderKeyboardOn(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonBorder2.Color = ((!this.OnKeyboard) ? Color.Silver : Color.Black);
			this._buttonBorder1.Color = Main.OurFavoriteColor;
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x005BC357 File Offset: 0x005BA557
		private void ManageBorderKeyboardOff(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonBorder2.Color = ((!this.OnKeyboard) ? Color.Silver : Color.Black);
			this._buttonBorder1.Color = (this.OnKeyboard ? Color.Silver : Color.Black);
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x005BC397 File Offset: 0x005BA597
		private void ManageBorderGamepadOn(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonBorder1.Color = (this.OnKeyboard ? Color.Silver : Color.Black);
			this._buttonBorder2.Color = Main.OurFavoriteColor;
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x005BC3C8 File Offset: 0x005BA5C8
		private void ManageBorderGamepadOff(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonBorder1.Color = (this.OnKeyboard ? Color.Silver : Color.Black);
			this._buttonBorder2.Color = ((!this.OnKeyboard) ? Color.Silver : Color.Black);
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x005BC408 File Offset: 0x005BA608
		private void VsGameplayButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonVs1.SetFrame(this._GameplayVsUITexture.Frame(2, 2, 0, 0, 0, 0));
			this._buttonVs2.SetFrame(this._GameplayVsUITexture.Frame(2, 2, 1, 1, 0, 0));
			this.OnGameplay = true;
			this.FillList();
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x005BC45C File Offset: 0x005BA65C
		private void VsMenuButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonVs1.SetFrame(this._GameplayVsUITexture.Frame(2, 2, 0, 1, 0, 0));
			this._buttonVs2.SetFrame(this._GameplayVsUITexture.Frame(2, 2, 1, 0, 0, 0));
			this.OnGameplay = false;
			this.FillList();
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x005BC4AE File Offset: 0x005BA6AE
		private void ManageBorderGameplayOn(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonBorderVs2.Color = ((!this.OnGameplay) ? Color.Silver : Color.Black);
			this._buttonBorderVs1.Color = Main.OurFavoriteColor;
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x005BC4DF File Offset: 0x005BA6DF
		private void ManageBorderGameplayOff(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonBorderVs2.Color = ((!this.OnGameplay) ? Color.Silver : Color.Black);
			this._buttonBorderVs1.Color = (this.OnGameplay ? Color.Silver : Color.Black);
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x005BC51F File Offset: 0x005BA71F
		private void ManageBorderMenuOn(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonBorderVs1.Color = (this.OnGameplay ? Color.Silver : Color.Black);
			this._buttonBorderVs2.Color = Main.OurFavoriteColor;
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x005BC550 File Offset: 0x005BA750
		private void ManageBorderMenuOff(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonBorderVs1.Color = (this.OnGameplay ? Color.Silver : Color.Black);
			this._buttonBorderVs2.Color = ((!this.OnGameplay) ? Color.Silver : Color.Black);
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x005BC590 File Offset: 0x005BA790
		private void profileButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			string name = PlayerInput.CurrentProfile.Name;
			List<string> list = PlayerInput.Profiles.Keys.ToList<string>();
			int num = list.IndexOf(name);
			num++;
			if (num >= list.Count)
			{
				num -= list.Count;
			}
			PlayerInput.SetSelectedProfile(list[num]);
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x005BC5E4 File Offset: 0x005BA7E4
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x005BC639 File Offset: 0x005BA839
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x005BC678 File Offset: 0x005BA878
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.menuMode = 1127;
			IngameFancyUI.Close();
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x005BC689 File Offset: 0x005BA889
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x005BC69C File Offset: 0x005BA89C
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 4;
			int num = 3000;
			UILinkPointNavigator.SetPosition(num, this._buttonBack.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(num + 1, this._buttonKeyboard.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(num + 2, this._buttonGamepad.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(num + 3, this._buttonProfile.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(num + 4, this._buttonVs1.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(num + 5, this._buttonVs2.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int key = num;
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[key];
			uilinkPoint.Unlink();
			uilinkPoint.Up = num + 6;
			key = num + 1;
			UILinkPoint uilinkPoint2 = UILinkPointNavigator.Points[key];
			uilinkPoint2.Unlink();
			uilinkPoint2.Right = num + 2;
			uilinkPoint2.Down = num + 6;
			key = num + 2;
			UILinkPoint uilinkPoint3 = UILinkPointNavigator.Points[key];
			uilinkPoint3.Unlink();
			uilinkPoint3.Left = num + 1;
			uilinkPoint3.Right = num + 4;
			uilinkPoint3.Down = num + 6;
			key = num + 4;
			UILinkPoint uilinkPoint4 = UILinkPointNavigator.Points[key];
			uilinkPoint4.Unlink();
			uilinkPoint4.Left = num + 2;
			uilinkPoint4.Right = num + 5;
			uilinkPoint4.Down = num + 6;
			key = num + 5;
			UILinkPoint uilinkPoint5 = UILinkPointNavigator.Points[key];
			uilinkPoint5.Unlink();
			uilinkPoint5.Left = num + 4;
			uilinkPoint5.Right = num + 3;
			uilinkPoint5.Down = num + 6;
			key = num + 3;
			UILinkPoint uilinkPoint6 = UILinkPointNavigator.Points[key];
			uilinkPoint6.Unlink();
			uilinkPoint6.Left = num + 5;
			uilinkPoint6.Down = num + 6;
			float num2 = 1f / Main.UIScale;
			Rectangle clippingRectangle = this._uilist.GetClippingRectangle(spriteBatch);
			Vector2 minimum = clippingRectangle.TopLeft() * num2;
			Vector2 maximum = clippingRectangle.BottomRight() * num2;
			List<SnapPoint> snapPoints = this._uilist.GetSnapPoints();
			for (int i = 0; i < snapPoints.Count; i++)
			{
				if (!snapPoints[i].Position.Between(minimum, maximum))
				{
					Vector2 position = snapPoints[i].Position;
					snapPoints.Remove(snapPoints[i]);
					i--;
				}
			}
			snapPoints.Sort((SnapPoint x, SnapPoint y) => x.Id.CompareTo(y.Id));
			for (int j = 0; j < snapPoints.Count; j++)
			{
				key = num + 6 + j;
				if (snapPoints[j].Name == "Thin")
				{
					UILinkPoint uilinkPoint7 = UILinkPointNavigator.Points[key];
					uilinkPoint7.Unlink();
					UILinkPointNavigator.SetPosition(key, snapPoints[j].Position);
					uilinkPoint7.Right = key + 1;
					uilinkPoint7.Down = ((j < snapPoints.Count - 2) ? (key + 2) : num);
					uilinkPoint7.Up = ((j < 2) ? (num + 1) : ((snapPoints[j - 1].Name == "Wide") ? (key - 1) : (key - 2)));
					UILinkPointNavigator.Points[num].Up = key;
					UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = key;
					j++;
					if (j < snapPoints.Count)
					{
						key = num + 6 + j;
						UILinkPoint uilinkPoint8 = UILinkPointNavigator.Points[key];
						uilinkPoint8.Unlink();
						UILinkPointNavigator.SetPosition(key, snapPoints[j].Position);
						uilinkPoint8.Left = key - 1;
						uilinkPoint8.Down = ((j >= snapPoints.Count - 1) ? num : ((snapPoints[j + 1].Name == "Wide") ? (key + 1) : (key + 2)));
						uilinkPoint8.Up = ((j < 2) ? (num + 1) : (key - 2));
						UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = key;
					}
				}
				else
				{
					UILinkPoint uilinkPoint9 = UILinkPointNavigator.Points[key];
					uilinkPoint9.Unlink();
					UILinkPointNavigator.SetPosition(key, snapPoints[j].Position);
					uilinkPoint9.Down = ((j < snapPoints.Count - 1) ? (key + 1) : num);
					uilinkPoint9.Up = ((j < 1) ? (num + 1) : ((snapPoints[j - 1].Name == "Wide") ? (key - 1) : (key - 2)));
					UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = key;
					UILinkPointNavigator.Points[num].Up = key;
				}
			}
			if (UIManageControls.ForceMoveTo != -1)
			{
				UILinkPointNavigator.ChangePoint((int)MathHelper.Clamp((float)UIManageControls.ForceMoveTo, (float)num, (float)UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX));
				UIManageControls.ForceMoveTo = -1;
			}
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x005BCB7C File Offset: 0x005BAD7C
		static UIManageControls()
		{
			UIManageControls._BindingsFullLine.AddRange(UIManageControls.tmlBindings);
			UIManageControls._BindingsHalfSingleLine.AddRange(UIManageControls.tmlBindings);
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x005BCD24 File Offset: 0x005BAF24
		private void OnAssembleBindPanels()
		{
			UIManageControls._BindingsFullLine.RemoveAll((string x) => x.Contains('/'));
			UIManageControls._ModBindings.Clear();
			UIManageControls._ModNames.Clear();
			Mod currentMod = null;
			foreach (ModKeybind keybind in KeybindLoader.Keybinds)
			{
				if (currentMod != keybind.Mod)
				{
					currentMod = keybind.Mod;
					UIManageControls._ModBindings.Add(keybind.Mod.DisplayName);
					UIManageControls._ModNames.Add(keybind.Mod.DisplayName);
				}
				UIManageControls._ModBindings.Add(keybind.FullName);
				UIManageControls._BindingsFullLine.Add(keybind.FullName);
			}
			UIManageControls._ModBindings.AddRange(UIManageControls.tmlBindings);
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x005BCE14 File Offset: 0x005BB014
		private void AddModBindingGroups()
		{
			this._bindsKeyboard.Add(this.CreateBindingGroup(5, UIManageControls._ModBindings, InputMode.Keyboard));
			this._bindsGamepad.Add(this.CreateBindingGroup(5, UIManageControls._ModBindings, InputMode.XBoxGamepad));
			this._bindsKeyboardUI.Add(this.CreateBindingGroup(5, UIManageControls._ModBindings, InputMode.KeyboardUI));
			this._bindsGamepadUI.Add(this.CreateBindingGroup(5, UIManageControls._ModBindings, InputMode.XBoxGamepadUI));
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x005BCE84 File Offset: 0x005BB084
		[NullableContext(1)]
		[return: Nullable(2)]
		private UIElement HandlePanelCreation(string bind, InputMode currentInputMode, Color color)
		{
			string bind2 = bind;
			if (bind2 == "ResetModKeybinds")
			{
				UIKeybindingSimpleListItem uikeybindingSimpleListItem = new UIKeybindingSimpleListItem(() => Lang.menu[86].Value, color);
				uikeybindingSimpleListItem.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
				{
					string copyableProfileName = UIManageControls.GetCopyableProfileName();
					PlayerInput.CurrentProfile.CopyModKeybindSettingsFrom(PlayerInput.OriginalProfiles[copyableProfileName], currentInputMode);
				};
				return uikeybindingSimpleListItem;
			}
			if (bind2 == "ClearModKeybinds")
			{
				UIKeybindingSimpleListItem uikeybindingSimpleListItem2 = new UIKeybindingSimpleListItem(() => Language.GetTextValue("tModLoader.ModConfigClear"), color);
				uikeybindingSimpleListItem2.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
				{
					foreach (ModKeybind modKeybind in KeybindLoader.Keybinds)
					{
						PlayerInput.CurrentProfile.InputModes[currentInputMode].KeyStatus[modKeybind.FullName].Clear();
					}
				};
				return uikeybindingSimpleListItem2;
			}
			if (!UIManageControls._ModBindings.Contains(bind))
			{
				return null;
			}
			string defaultKey = KeybindLoader.modKeybinds[bind].DefaultBinding;
			UIElement uielement = new UIElement();
			UIKeybindingListItem left = new UIKeybindingListItem(bind, currentInputMode, color);
			left.Width.Precent = 0.58f;
			left.Height.Precent = 1f;
			uielement.Append(left);
			UIKeybindingSimpleListItem right = new UIKeybindingSimpleListItem(() => Lang.menu[86].Value + " (" + defaultKey + ")", color);
			right.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				string copyableProfileName = UIManageControls.GetCopyableProfileName();
				PlayerInput.CurrentProfile.CopyIndividualModKeybindSettingsFrom(PlayerInput.OriginalProfiles[copyableProfileName], currentInputMode, bind);
			};
			right.Left.Precent = 0.6f;
			right.Width.Precent = 0.4f;
			right.Height.Precent = 1f;
			uielement.Append(right);
			return uielement;
		}

		// Token: 0x04005544 RID: 21828
		public static int ForceMoveTo = -1;

		// Token: 0x04005545 RID: 21829
		private const float PanelTextureHeight = 30f;

		// Token: 0x04005546 RID: 21830
		private static List<string> _BindingsFullLine = new List<string>
		{
			"Throw",
			"Inventory",
			"RadialHotbar",
			"RadialQuickbar",
			"LockOn",
			"ToggleCreativeMenu",
			"Loadout1",
			"Loadout2",
			"Loadout3",
			"ToggleCameraMode",
			"sp3",
			"sp4",
			"sp5",
			"sp6",
			"sp7",
			"sp8",
			"sp18",
			"sp19",
			"sp9",
			"sp10",
			"sp11",
			"sp12",
			"sp13"
		};

		// Token: 0x04005547 RID: 21831
		private static List<string> _BindingsHalfSingleLine = new List<string>
		{
			"sp9",
			"sp10",
			"sp11",
			"sp12",
			"sp13"
		};

		// Token: 0x04005548 RID: 21832
		private bool OnKeyboard = true;

		// Token: 0x04005549 RID: 21833
		private bool OnGameplay = true;

		// Token: 0x0400554A RID: 21834
		private List<UIElement> _bindsKeyboard = new List<UIElement>();

		// Token: 0x0400554B RID: 21835
		private List<UIElement> _bindsGamepad = new List<UIElement>();

		// Token: 0x0400554C RID: 21836
		private List<UIElement> _bindsKeyboardUI = new List<UIElement>();

		// Token: 0x0400554D RID: 21837
		private List<UIElement> _bindsGamepadUI = new List<UIElement>();

		// Token: 0x0400554E RID: 21838
		private UIElement _outerContainer;

		// Token: 0x0400554F RID: 21839
		private UIList _uilist;

		// Token: 0x04005550 RID: 21840
		private UIImageFramed _buttonKeyboard;

		// Token: 0x04005551 RID: 21841
		private UIImageFramed _buttonGamepad;

		// Token: 0x04005552 RID: 21842
		private UIImageFramed _buttonBorder1;

		// Token: 0x04005553 RID: 21843
		private UIImageFramed _buttonBorder2;

		// Token: 0x04005554 RID: 21844
		private UIKeybindingSimpleListItem _buttonProfile;

		// Token: 0x04005555 RID: 21845
		private UIElement _buttonBack;

		// Token: 0x04005556 RID: 21846
		private UIImageFramed _buttonVs1;

		// Token: 0x04005557 RID: 21847
		private UIImageFramed _buttonVs2;

		// Token: 0x04005558 RID: 21848
		private UIImageFramed _buttonBorderVs1;

		// Token: 0x04005559 RID: 21849
		private UIImageFramed _buttonBorderVs2;

		// Token: 0x0400555A RID: 21850
		private Asset<Texture2D> _KeyboardGamepadTexture;

		// Token: 0x0400555B RID: 21851
		private Asset<Texture2D> _keyboardGamepadBorderTexture;

		// Token: 0x0400555C RID: 21852
		private Asset<Texture2D> _GameplayVsUITexture;

		// Token: 0x0400555D RID: 21853
		private Asset<Texture2D> _GameplayVsUIBorderTexture;

		// Token: 0x0400555E RID: 21854
		private static int SnapPointIndex;

		// Token: 0x0400555F RID: 21855
		[Nullable(1)]
		private const string ResetModKeybinds = "ResetModKeybinds";

		// Token: 0x04005560 RID: 21856
		[Nullable(1)]
		private const string ClearModKeybinds = "ClearModKeybinds";

		// Token: 0x04005561 RID: 21857
		private const int TmlBindingGroupId = 5;

		// Token: 0x04005562 RID: 21858
		[Nullable(1)]
		private static readonly string[] tmlBindings = new string[]
		{
			"ResetModKeybinds",
			"ClearModKeybinds"
		};

		// Token: 0x04005563 RID: 21859
		[Nullable(1)]
		private static readonly List<string> _ModBindings = new List<string>();

		// Token: 0x04005564 RID: 21860
		[Nullable(1)]
		private static readonly List<string> _ModNames = new List<string>();
	}
}
