using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Chat;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000522 RID: 1314
	public class UIKeybindingListItem : UIElement
	{
		// Token: 0x06003EE6 RID: 16102 RVA: 0x005D5B90 File Offset: 0x005D3D90
		public UIKeybindingListItem(string bind, InputMode mode, Color color)
		{
			this._keybind = bind;
			this._inputmode = mode;
			this._color = color;
			base.OnLeftClick += this.OnClickMethod;
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x005D5BBF File Offset: 0x005D3DBF
		public void OnClickMethod(UIMouseEvent evt, UIElement listeningElement)
		{
			if (PlayerInput.ListeningTrigger != this._keybind)
			{
				if (PlayerInput.CurrentProfile.AllowEditting)
				{
					PlayerInput.ListenFor(this._keybind, this._inputmode);
					return;
				}
				PlayerInput.ListenFor(null, this._inputmode);
			}
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x005D5C00 File Offset: 0x005D3E00
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float num = 6f;
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			float num2 = dimensions.Width + 1f;
			Vector2 vector = new Vector2(dimensions.X, dimensions.Y);
			bool flag = PlayerInput.ListeningTrigger == this._keybind;
			Vector2 baseScale;
			baseScale..ctor(0.8f);
			Color value = flag ? Color.Gold : (base.IsMouseHovering ? Color.White : Color.Silver);
			value = Color.Lerp(value, Color.White, base.IsMouseHovering ? 0.5f : 0f);
			Color color = base.IsMouseHovering ? this._color : this._color.MultiplyRGBA(new Color(180, 180, 180));
			Vector2 position = vector;
			Utils.DrawSettingsPanel(spriteBatch, position, num2, color);
			position.X += 8f;
			position.Y += 2f + num;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, this.GetFriendlyName(), position, value, 0f, Vector2.Zero, baseScale, num2, 2f);
			position.X -= 17f;
			List<string> list = PlayerInput.CurrentProfile.InputModes[this._inputmode].KeyStatus[this._keybind];
			string text = this.GenInput(list);
			if (string.IsNullOrEmpty(text))
			{
				text = Lang.menu[195].Value;
				if (!flag)
				{
					value..ctor(80, 80, 80);
				}
			}
			Vector2 stringSize = ChatManager.GetStringSize(FontAssets.ItemStack.Value, text, baseScale, -1f);
			position..ctor(dimensions.X + dimensions.Width - stringSize.X - 10f, dimensions.Y + 2f + num);
			if (this._inputmode == InputMode.XBoxGamepad || this._inputmode == InputMode.XBoxGamepadUI)
			{
				position += new Vector2(0f, -3f);
			}
			GlyphTagHandler.GlyphsScale = 0.85f;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, position, value, 0f, Vector2.Zero, baseScale, num2, 2f);
			GlyphTagHandler.GlyphsScale = 1f;
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x005D5E48 File Offset: 0x005D4048
		private string GenInput(List<string> list)
		{
			if (list.Count == 0)
			{
				return "";
			}
			string text = "";
			InputMode inputmode = this._inputmode;
			if (inputmode > InputMode.Mouse)
			{
				if (inputmode - InputMode.XBoxGamepad <= 1)
				{
					text = GlyphTagHandler.GenerateTag(list[0]);
					for (int i = 1; i < list.Count; i++)
					{
						text = text + "/" + GlyphTagHandler.GenerateTag(list[i]);
					}
				}
			}
			else
			{
				text = list[0];
				for (int j = 1; j < list.Count; j++)
				{
					text = text + "/" + list[j];
				}
			}
			return text;
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x005D5EE0 File Offset: 0x005D40E0
		private string GetFriendlyName()
		{
			string keybind = this._keybind;
			if (keybind != null)
			{
				switch (keybind.Length)
				{
				case 2:
					if (keybind == "Up")
					{
						return Lang.menu[148].Value;
					}
					break;
				case 4:
				{
					char c = keybind[0];
					if (c != 'D')
					{
						if (c != 'J')
						{
							if (c == 'L')
							{
								if (keybind == "Left")
								{
									return Lang.menu[150].Value;
								}
							}
						}
						else if (keybind == "Jump")
						{
							return Lang.menu[152].Value;
						}
					}
					else if (keybind == "Down")
					{
						return Lang.menu[149].Value;
					}
					break;
				}
				case 5:
				{
					char c = keybind[0];
					if (c != 'R')
					{
						if (c == 'T')
						{
							if (keybind == "Throw")
							{
								return Lang.menu[153].Value;
							}
						}
					}
					else if (keybind == "Right")
					{
						return Lang.menu[151].Value;
					}
					break;
				}
				case 6:
					if (keybind == "LockOn")
					{
						return Lang.menu[231].Value;
					}
					break;
				case 7:
				{
					char c = keybind[6];
					switch (c)
					{
					case '1':
						if (keybind == "Hotbar1")
						{
							return Lang.menu[176].Value;
						}
						break;
					case '2':
						if (keybind == "Hotbar2")
						{
							return Lang.menu[177].Value;
						}
						break;
					case '3':
						if (keybind == "Hotbar3")
						{
							return Lang.menu[178].Value;
						}
						break;
					case '4':
						if (keybind == "Hotbar4")
						{
							return Lang.menu[179].Value;
						}
						break;
					case '5':
						if (keybind == "Hotbar5")
						{
							return Lang.menu[180].Value;
						}
						break;
					case '6':
						if (keybind == "Hotbar6")
						{
							return Lang.menu[181].Value;
						}
						break;
					case '7':
						if (keybind == "Hotbar7")
						{
							return Lang.menu[182].Value;
						}
						break;
					case '8':
						if (keybind == "Hotbar8")
						{
							return Lang.menu[183].Value;
						}
						break;
					case '9':
						if (keybind == "Hotbar9")
						{
							return Lang.menu[184].Value;
						}
						break;
					default:
						if (c != 'e')
						{
							if (c == 'l')
							{
								if (keybind == "MapFull")
								{
									return Lang.menu[173].Value;
								}
							}
						}
						else if (keybind == "Grapple")
						{
							return Lang.menu[155].Value;
						}
						break;
					}
					break;
				}
				case 8:
				{
					char c = keybind[7];
					switch (c)
					{
					case '0':
						if (keybind == "Hotbar10")
						{
							return Lang.menu[185].Value;
						}
						break;
					case '1':
						if (keybind == "Loadout1")
						{
							return Language.GetTextValue("UI.Loadout1");
						}
						break;
					case '2':
						if (keybind == "Loadout2")
						{
							return Language.GetTextValue("UI.Loadout2");
						}
						break;
					case '3':
						if (keybind == "Loadout3")
						{
							return Language.GetTextValue("UI.Loadout3");
						}
						break;
					default:
						if (c == 'e')
						{
							if (keybind == "MapStyle")
							{
								return Lang.menu[172].Value;
							}
						}
						break;
					}
					break;
				}
				case 9:
				{
					char c = keybind[8];
					if (c <= 'f')
					{
						switch (c)
						{
						case '1':
							if (keybind == "DpadSnap1")
							{
								return Lang.menu[191].Value;
							}
							break;
						case '2':
							if (keybind == "DpadSnap2")
							{
								return Lang.menu[192].Value;
							}
							break;
						case '3':
							if (keybind == "DpadSnap3")
							{
								return Lang.menu[193].Value;
							}
							break;
						case '4':
							if (keybind == "DpadSnap4")
							{
								return Lang.menu[194].Value;
							}
							break;
						default:
							if (c != 'a')
							{
								if (c == 'f')
								{
									if (keybind == "QuickBuff")
									{
										return Lang.menu[157].Value;
									}
								}
							}
							else if (keybind == "QuickMana")
							{
								return Lang.menu[156].Value;
							}
							break;
						}
					}
					else if (c <= 'n')
					{
						if (c != 'l')
						{
							if (c == 'n')
							{
								if (keybind == "MapZoomIn")
								{
									return Lang.menu[168].Value;
								}
							}
						}
						else if (keybind == "QuickHeal")
						{
							return Lang.menu[159].Value;
						}
					}
					else if (c != 't')
					{
						if (c == 'y')
						{
							if (keybind == "Inventory")
							{
								return Lang.menu[154].Value;
							}
						}
					}
					else if (keybind == "MouseLeft")
					{
						return Lang.menu[162].Value;
					}
					break;
				}
				case 10:
				{
					char c = keybind[3];
					if (c <= 'b')
					{
						if (c != 'A')
						{
							if (c != 'Z')
							{
								if (c == 'b')
								{
									if (keybind == "HotbarPlus")
									{
										return Lang.menu[175].Value;
									}
								}
							}
							else if (keybind == "MapZoomOut")
							{
								return Lang.menu[169].Value;
							}
						}
						else if (keybind == "MapAlphaUp")
						{
							return Lang.menu[171].Value;
						}
					}
					else if (c != 'c')
					{
						if (c != 's')
						{
							if (c == 'w')
							{
								if (keybind == "ViewZoomIn")
								{
									return Language.GetTextValue("UI.ZoomIn");
								}
							}
						}
						else if (keybind == "MouseRight")
						{
							return Lang.menu[163].Value;
						}
					}
					else if (keybind == "QuickMount")
					{
						return Lang.menu[158].Value;
					}
					break;
				}
				case 11:
				{
					char c = keybind[10];
					switch (c)
					{
					case '1':
						if (keybind == "DpadRadial1")
						{
							return Lang.menu[186].Value;
						}
						break;
					case '2':
						if (keybind == "DpadRadial2")
						{
							return Lang.menu[187].Value;
						}
						break;
					case '3':
						if (keybind == "DpadRadial3")
						{
							return Lang.menu[188].Value;
						}
						break;
					case '4':
						if (keybind == "DpadRadial4")
						{
							return Lang.menu[189].Value;
						}
						break;
					default:
						if (c != 'e')
						{
							switch (c)
							{
							case 'r':
								if (keybind == "SmartCursor")
								{
									return Lang.menu[161].Value;
								}
								break;
							case 's':
								if (keybind == "HotbarMinus")
								{
									return Lang.menu[174].Value;
								}
								break;
							case 't':
								if (keybind == "SmartSelect")
								{
									return Lang.menu[160].Value;
								}
								if (keybind == "ViewZoomOut")
								{
									return Language.GetTextValue("UI.ZoomOut");
								}
								break;
							}
						}
						else if (keybind == "MouseMiddle")
						{
							return Language.GetTextValue("tModLoader.MouseMiddle");
						}
						break;
					}
					break;
				}
				case 12:
				{
					char c = keybind[0];
					if (c != 'M')
					{
						if (c == 'R')
						{
							if (keybind == "RadialHotbar")
							{
								return Lang.menu[190].Value;
							}
						}
					}
					else if (keybind == "MapAlphaDown")
					{
						return Lang.menu[170].Value;
					}
					break;
				}
				case 13:
				{
					char c = keybind[12];
					if (c != '1')
					{
						if (c == '2')
						{
							if (keybind == "MouseXButton2")
							{
								return Language.GetTextValue("tModLoader.MouseXButton2");
							}
						}
					}
					else if (keybind == "MouseXButton1")
					{
						return Language.GetTextValue("tModLoader.MouseXButton1");
					}
					break;
				}
				case 14:
					if (keybind == "RadialQuickbar")
					{
						return Lang.menu[244].Value;
					}
					break;
				case 16:
					if (keybind == "ToggleCameraMode")
					{
						return Language.GetTextValue("UI.ToggleCameraMode");
					}
					break;
				case 18:
					if (keybind == "ToggleCreativeMenu")
					{
						return Language.GetTextValue("UI.ToggleCreativeMenu");
					}
					break;
				}
			}
			ModKeybind modKeybind;
			if (KeybindLoader.modKeybinds.TryGetValue(this._keybind, out modKeybind))
			{
				return modKeybind.DisplayName.Value;
			}
			return this._keybind;
		}

		// Token: 0x0400576D RID: 22381
		private InputMode _inputmode;

		// Token: 0x0400576E RID: 22382
		private Color _color;

		// Token: 0x0400576F RID: 22383
		private string _keybind;
	}
}
