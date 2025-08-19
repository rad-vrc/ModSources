using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.ModLoader;

namespace Terraria.GameInput
{
	// Token: 0x02000484 RID: 1156
	public class PlayerInputProfile
	{
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06003817 RID: 14359 RVA: 0x00591D2F File Offset: 0x0058FF2F
		public string ShowName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x00591D37 File Offset: 0x0058FF37
		public bool HotbarAllowsRadial
		{
			get
			{
				return this.HotbarRadialHoldTimeRequired != -1;
			}
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x00591D48 File Offset: 0x0058FF48
		public PlayerInputProfile(string name)
		{
			this.Name = name;
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x00591DEC File Offset: 0x0058FFEC
		public void Initialize(PresetProfiles style)
		{
			foreach (KeyValuePair<InputMode, KeyConfiguration> inputMode in this.InputModes)
			{
				inputMode.Value.SetupKeys();
				PlayerInput.Reset(inputMode.Value, style, inputMode.Key);
			}
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x00591E58 File Offset: 0x00590058
		public bool Load(Dictionary<string, object> dict)
		{
			int num = 0;
			object value;
			if (dict.TryGetValue("Last Launched Version", out value))
			{
				num = (int)((long)value);
			}
			if (dict.TryGetValue("Mouse And Keyboard", out value))
			{
				this.InputModes[InputMode.Keyboard].ReadPreferences(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(((JObject)value).ToString()));
			}
			if (dict.TryGetValue("Gamepad", out value))
			{
				this.InputModes[InputMode.XBoxGamepad].ReadPreferences(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(((JObject)value).ToString()));
			}
			if (dict.TryGetValue("Mouse And Keyboard UI", out value))
			{
				this.InputModes[InputMode.KeyboardUI].ReadPreferences(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(((JObject)value).ToString()));
			}
			if (dict.TryGetValue("Gamepad UI", out value))
			{
				this.InputModes[InputMode.XBoxGamepadUI].ReadPreferences(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(((JObject)value).ToString()));
			}
			if (num < 190)
			{
				this.InputModes[InputMode.Keyboard].KeyStatus["ViewZoomIn"] = new List<string>();
				this.InputModes[InputMode.Keyboard].KeyStatus["ViewZoomIn"].AddRange(PlayerInput.OriginalProfiles["Redigit's Pick"].InputModes[InputMode.Keyboard].KeyStatus["ViewZoomIn"]);
				this.InputModes[InputMode.Keyboard].KeyStatus["ViewZoomOut"] = new List<string>();
				this.InputModes[InputMode.Keyboard].KeyStatus["ViewZoomOut"].AddRange(PlayerInput.OriginalProfiles["Redigit's Pick"].InputModes[InputMode.Keyboard].KeyStatus["ViewZoomOut"]);
			}
			if (num < 218)
			{
				this.InputModes[InputMode.Keyboard].KeyStatus["ToggleCreativeMenu"] = new List<string>();
				this.InputModes[InputMode.Keyboard].KeyStatus["ToggleCreativeMenu"].AddRange(PlayerInput.OriginalProfiles["Redigit's Pick"].InputModes[InputMode.Keyboard].KeyStatus["ToggleCreativeMenu"]);
			}
			if (num < 227)
			{
				List<string> list = this.InputModes[InputMode.KeyboardUI].KeyStatus["MouseLeft"];
				string item = "Mouse1";
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			if (num < 265)
			{
				foreach (string key in new string[]
				{
					"Loadout1",
					"Loadout2",
					"Loadout3",
					"ToggleCameraMode"
				})
				{
					this.InputModes[InputMode.Keyboard].KeyStatus[key] = new List<string>(PlayerInput.OriginalProfiles["Redigit's Pick"].InputModes[InputMode.Keyboard].KeyStatus[key]);
				}
			}
			if (dict.TryGetValue("Settings", out value))
			{
				Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(((JObject)value).ToString());
				if (dictionary.TryGetValue("Edittable", out value))
				{
					this.AllowEditting = (bool)value;
				}
				if (dictionary.TryGetValue("Gamepad - HotbarRadialHoldTime", out value))
				{
					this.HotbarRadialHoldTimeRequired = (int)((long)value);
				}
				if (dictionary.TryGetValue("Gamepad - LeftThumbstickDeadzoneX", out value))
				{
					this.LeftThumbstickDeadzoneX = (float)((double)value);
				}
				if (dictionary.TryGetValue("Gamepad - LeftThumbstickDeadzoneY", out value))
				{
					this.LeftThumbstickDeadzoneY = (float)((double)value);
				}
				if (dictionary.TryGetValue("Gamepad - RightThumbstickDeadzoneX", out value))
				{
					this.RightThumbstickDeadzoneX = (float)((double)value);
				}
				if (dictionary.TryGetValue("Gamepad - RightThumbstickDeadzoneY", out value))
				{
					this.RightThumbstickDeadzoneY = (float)((double)value);
				}
				if (dictionary.TryGetValue("Gamepad - LeftThumbstickInvertX", out value))
				{
					this.LeftThumbstickInvertX = (bool)value;
				}
				if (dictionary.TryGetValue("Gamepad - LeftThumbstickInvertY", out value))
				{
					this.LeftThumbstickInvertY = (bool)value;
				}
				if (dictionary.TryGetValue("Gamepad - RightThumbstickInvertX", out value))
				{
					this.RightThumbstickInvertX = (bool)value;
				}
				if (dictionary.TryGetValue("Gamepad - RightThumbstickInvertY", out value))
				{
					this.RightThumbstickInvertY = (bool)value;
				}
				if (dictionary.TryGetValue("Gamepad - TriggersDeadzone", out value))
				{
					this.TriggersDeadzone = (float)((double)value);
				}
				if (dictionary.TryGetValue("Gamepad - InterfaceDeadzoneX", out value))
				{
					this.InterfaceDeadzoneX = (float)((double)value);
				}
				if (dictionary.TryGetValue("Gamepad - InventoryMoveCD", out value))
				{
					this.InventoryMoveCD = (int)((long)value);
				}
			}
			return true;
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x005922E0 File Offset: 0x005904E0
		public Dictionary<string, object> Save()
		{
			Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary3.Add("Last Launched Version", 279);
			dictionary2.Add("Edittable", this.AllowEditting);
			dictionary2.Add("Gamepad - HotbarRadialHoldTime", this.HotbarRadialHoldTimeRequired);
			dictionary2.Add("Gamepad - LeftThumbstickDeadzoneX", this.LeftThumbstickDeadzoneX);
			dictionary2.Add("Gamepad - LeftThumbstickDeadzoneY", this.LeftThumbstickDeadzoneY);
			dictionary2.Add("Gamepad - RightThumbstickDeadzoneX", this.RightThumbstickDeadzoneX);
			dictionary2.Add("Gamepad - RightThumbstickDeadzoneY", this.RightThumbstickDeadzoneY);
			dictionary2.Add("Gamepad - LeftThumbstickInvertX", this.LeftThumbstickInvertX);
			dictionary2.Add("Gamepad - LeftThumbstickInvertY", this.LeftThumbstickInvertY);
			dictionary2.Add("Gamepad - RightThumbstickInvertX", this.RightThumbstickInvertX);
			dictionary2.Add("Gamepad - RightThumbstickInvertY", this.RightThumbstickInvertY);
			dictionary2.Add("Gamepad - TriggersDeadzone", this.TriggersDeadzone);
			dictionary2.Add("Gamepad - InterfaceDeadzoneX", this.InterfaceDeadzoneX);
			dictionary2.Add("Gamepad - InventoryMoveCD", this.InventoryMoveCD);
			dictionary3.Add("Settings", dictionary2);
			dictionary3.Add("Mouse And Keyboard", this.InputModes[InputMode.Keyboard].WritePreferences());
			dictionary3.Add("Gamepad", this.InputModes[InputMode.XBoxGamepad].WritePreferences());
			dictionary3.Add("Mouse And Keyboard UI", this.InputModes[InputMode.KeyboardUI].WritePreferences());
			dictionary3.Add("Gamepad UI", this.InputModes[InputMode.XBoxGamepadUI].WritePreferences());
			return dictionary3;
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x005924A8 File Offset: 0x005906A8
		public void ConditionalAddProfile(Dictionary<string, object> dicttouse, string k, InputMode nm, Dictionary<string, List<string>> dict)
		{
			if (PlayerInput.OriginalProfiles.ContainsKey(this.Name))
			{
				foreach (KeyValuePair<string, List<string>> item in PlayerInput.OriginalProfiles[this.Name].InputModes[nm].WritePreferences())
				{
					bool flag = true;
					List<string> value;
					if (dict.TryGetValue(item.Key, out value))
					{
						if (value.Count != item.Value.Count)
						{
							flag = false;
						}
						if (!flag)
						{
							for (int i = 0; i < value.Count; i++)
							{
								if (value[i] != item.Value[i])
								{
									flag = false;
									break;
								}
							}
						}
					}
					else
					{
						flag = false;
					}
					if (flag)
					{
						dict.Remove(item.Key);
					}
				}
			}
			if (dict.Count > 0)
			{
				dicttouse.Add(k, dict);
			}
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x005925B8 File Offset: 0x005907B8
		public void ConditionalAdd(Dictionary<string, object> dicttouse, string a, object b, Func<PlayerInputProfile, bool> check)
		{
			if (!PlayerInput.OriginalProfiles.ContainsKey(this.Name) || !check(PlayerInput.OriginalProfiles[this.Name]))
			{
				dicttouse.Add(a, b);
			}
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x005925F0 File Offset: 0x005907F0
		public void CopyGameplaySettingsFrom(PlayerInputProfile profile, InputMode mode)
		{
			string[] keysToCopy = new string[]
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
				"ViewZoomIn",
				"ViewZoomOut",
				"Loadout1",
				"Loadout2",
				"Loadout3",
				"ToggleCreativeMenu",
				"ToggleCameraMode"
			};
			this.CopyKeysFrom(profile, mode, keysToCopy);
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x005926F0 File Offset: 0x005908F0
		public void CopyHotbarSettingsFrom(PlayerInputProfile profile, InputMode mode)
		{
			string[] keysToCopy = new string[]
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
				"Hotbar10"
			};
			this.CopyKeysFrom(profile, mode, keysToCopy);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x00592774 File Offset: 0x00590974
		public void CopyMapSettingsFrom(PlayerInputProfile profile, InputMode mode)
		{
			string[] keysToCopy = new string[]
			{
				"MapZoomIn",
				"MapZoomOut",
				"MapAlphaUp",
				"MapAlphaDown",
				"MapFull",
				"MapStyle"
			};
			this.CopyKeysFrom(profile, mode, keysToCopy);
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x005927C4 File Offset: 0x005909C4
		public void CopyGamepadSettingsFrom(PlayerInputProfile profile, InputMode mode)
		{
			string[] keysToCopy = new string[]
			{
				"RadialHotbar",
				"RadialQuickbar",
				"DpadSnap1",
				"DpadSnap2",
				"DpadSnap3",
				"DpadSnap4",
				"DpadRadial1",
				"DpadRadial2",
				"DpadRadial3",
				"DpadRadial4"
			};
			this.CopyKeysFrom(profile, InputMode.XBoxGamepad, keysToCopy);
			this.CopyKeysFrom(profile, InputMode.XBoxGamepadUI, keysToCopy);
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x0059283C File Offset: 0x00590A3C
		public void CopyGamepadAdvancedSettingsFrom(PlayerInputProfile profile, InputMode mode)
		{
			this.TriggersDeadzone = profile.TriggersDeadzone;
			this.InterfaceDeadzoneX = profile.InterfaceDeadzoneX;
			this.LeftThumbstickDeadzoneX = profile.LeftThumbstickDeadzoneX;
			this.LeftThumbstickDeadzoneY = profile.LeftThumbstickDeadzoneY;
			this.RightThumbstickDeadzoneX = profile.RightThumbstickDeadzoneX;
			this.RightThumbstickDeadzoneY = profile.RightThumbstickDeadzoneY;
			this.LeftThumbstickInvertX = profile.LeftThumbstickInvertX;
			this.LeftThumbstickInvertY = profile.LeftThumbstickInvertY;
			this.RightThumbstickInvertX = profile.RightThumbstickInvertX;
			this.RightThumbstickInvertY = profile.RightThumbstickInvertY;
			this.InventoryMoveCD = profile.InventoryMoveCD;
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x005928D0 File Offset: 0x00590AD0
		private void CopyKeysFrom(PlayerInputProfile profile, InputMode mode, string[] keysToCopy)
		{
			for (int i = 0; i < keysToCopy.Length; i++)
			{
				List<string> value;
				if (profile.InputModes[mode].KeyStatus.TryGetValue(keysToCopy[i], out value))
				{
					this.InputModes[mode].KeyStatus[keysToCopy[i]].Clear();
					this.InputModes[mode].KeyStatus[keysToCopy[i]].AddRange(value);
				}
			}
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x00592948 File Offset: 0x00590B48
		public bool UsingDpadHotbar()
		{
			return this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"].Contains(1.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"].Contains(8.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"].Contains(2.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"].Contains(4.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"].Contains(1.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"].Contains(8.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"].Contains(2.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"].Contains(4.ToString());
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x00592AEC File Offset: 0x00590CEC
		public bool UsingDpadMovekeys()
		{
			return this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"].Contains(1.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"].Contains(8.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"].Contains(2.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"].Contains(4.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"].Contains(1.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"].Contains(8.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"].Contains(2.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"].Contains(4.ToString());
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x00592C90 File Offset: 0x00590E90
		public void CopyModKeybindSettingsFrom(PlayerInputProfile profile, InputMode mode)
		{
			foreach (ModKeybind modKeybind in KeybindLoader.Keybinds)
			{
				this.InputModes[mode].KeyStatus[modKeybind.FullName].Clear();
				if (!string.IsNullOrEmpty(modKeybind.DefaultBinding))
				{
					this.InputModes[mode].KeyStatus[modKeybind.FullName].Add(modKeybind.DefaultBinding);
				}
			}
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x00592D2C File Offset: 0x00590F2C
		public void CopyIndividualModKeybindSettingsFrom(PlayerInputProfile profile, InputMode mode, string uniqueName)
		{
			ModKeybind modKeybind = KeybindLoader.modKeybinds[uniqueName];
			this.InputModes[mode].KeyStatus[uniqueName].Clear();
			if (!string.IsNullOrEmpty(modKeybind.DefaultBinding))
			{
				this.InputModes[mode].KeyStatus[uniqueName].Add(modKeybind.DefaultBinding);
			}
		}

		// Token: 0x040051A5 RID: 20901
		public Dictionary<InputMode, KeyConfiguration> InputModes = new Dictionary<InputMode, KeyConfiguration>
		{
			{
				InputMode.Keyboard,
				new KeyConfiguration()
			},
			{
				InputMode.KeyboardUI,
				new KeyConfiguration()
			},
			{
				InputMode.XBoxGamepad,
				new KeyConfiguration()
			},
			{
				InputMode.XBoxGamepadUI,
				new KeyConfiguration()
			}
		};

		// Token: 0x040051A6 RID: 20902
		public string Name = "";

		// Token: 0x040051A7 RID: 20903
		public bool AllowEditting = true;

		// Token: 0x040051A8 RID: 20904
		public int HotbarRadialHoldTimeRequired = 16;

		// Token: 0x040051A9 RID: 20905
		public float TriggersDeadzone = 0.3f;

		// Token: 0x040051AA RID: 20906
		public float InterfaceDeadzoneX = 0.2f;

		// Token: 0x040051AB RID: 20907
		public float LeftThumbstickDeadzoneX = 0.25f;

		// Token: 0x040051AC RID: 20908
		public float LeftThumbstickDeadzoneY = 0.4f;

		// Token: 0x040051AD RID: 20909
		public float RightThumbstickDeadzoneX;

		// Token: 0x040051AE RID: 20910
		public float RightThumbstickDeadzoneY;

		// Token: 0x040051AF RID: 20911
		public bool LeftThumbstickInvertX;

		// Token: 0x040051B0 RID: 20912
		public bool LeftThumbstickInvertY;

		// Token: 0x040051B1 RID: 20913
		public bool RightThumbstickInvertX;

		// Token: 0x040051B2 RID: 20914
		public bool RightThumbstickInvertY;

		// Token: 0x040051B3 RID: 20915
		public int InventoryMoveCD = 6;
	}
}
