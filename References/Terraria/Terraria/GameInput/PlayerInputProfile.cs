using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Terraria.GameInput
{
	// Token: 0x02000137 RID: 311
	public class PlayerInputProfile
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x004D66DA File Offset: 0x004D48DA
		public string ShowName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x004D66E2 File Offset: 0x004D48E2
		public bool HotbarAllowsRadial
		{
			get
			{
				return this.HotbarRadialHoldTimeRequired != -1;
			}
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x004D66F0 File Offset: 0x004D48F0
		public PlayerInputProfile(string name)
		{
			this.Name = name;
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x004D6794 File Offset: 0x004D4994
		public void Initialize(PresetProfiles style)
		{
			foreach (KeyValuePair<InputMode, KeyConfiguration> keyValuePair in this.InputModes)
			{
				keyValuePair.Value.SetupKeys();
				PlayerInput.Reset(keyValuePair.Value, style, keyValuePair.Key);
			}
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x004D6800 File Offset: 0x004D4A00
		public bool Load(Dictionary<string, object> dict)
		{
			int num = 0;
			object obj;
			if (dict.TryGetValue("Last Launched Version", out obj))
			{
				num = (int)((long)obj);
			}
			if (dict.TryGetValue("Mouse And Keyboard", out obj))
			{
				this.InputModes[InputMode.Keyboard].ReadPreferences(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(((JObject)obj).ToString()));
			}
			if (dict.TryGetValue("Gamepad", out obj))
			{
				this.InputModes[InputMode.XBoxGamepad].ReadPreferences(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(((JObject)obj).ToString()));
			}
			if (dict.TryGetValue("Mouse And Keyboard UI", out obj))
			{
				this.InputModes[InputMode.KeyboardUI].ReadPreferences(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(((JObject)obj).ToString()));
			}
			if (dict.TryGetValue("Gamepad UI", out obj))
			{
				this.InputModes[InputMode.XBoxGamepadUI].ReadPreferences(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(((JObject)obj).ToString()));
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
			if (dict.TryGetValue("Settings", out obj))
			{
				Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(((JObject)obj).ToString());
				if (dictionary.TryGetValue("Edittable", out obj))
				{
					this.AllowEditting = (bool)obj;
				}
				if (dictionary.TryGetValue("Gamepad - HotbarRadialHoldTime", out obj))
				{
					this.HotbarRadialHoldTimeRequired = (int)((long)obj);
				}
				if (dictionary.TryGetValue("Gamepad - LeftThumbstickDeadzoneX", out obj))
				{
					this.LeftThumbstickDeadzoneX = (float)((double)obj);
				}
				if (dictionary.TryGetValue("Gamepad - LeftThumbstickDeadzoneY", out obj))
				{
					this.LeftThumbstickDeadzoneY = (float)((double)obj);
				}
				if (dictionary.TryGetValue("Gamepad - RightThumbstickDeadzoneX", out obj))
				{
					this.RightThumbstickDeadzoneX = (float)((double)obj);
				}
				if (dictionary.TryGetValue("Gamepad - RightThumbstickDeadzoneY", out obj))
				{
					this.RightThumbstickDeadzoneY = (float)((double)obj);
				}
				if (dictionary.TryGetValue("Gamepad - LeftThumbstickInvertX", out obj))
				{
					this.LeftThumbstickInvertX = (bool)obj;
				}
				if (dictionary.TryGetValue("Gamepad - LeftThumbstickInvertY", out obj))
				{
					this.LeftThumbstickInvertY = (bool)obj;
				}
				if (dictionary.TryGetValue("Gamepad - RightThumbstickInvertX", out obj))
				{
					this.RightThumbstickInvertX = (bool)obj;
				}
				if (dictionary.TryGetValue("Gamepad - RightThumbstickInvertY", out obj))
				{
					this.RightThumbstickInvertY = (bool)obj;
				}
				if (dictionary.TryGetValue("Gamepad - TriggersDeadzone", out obj))
				{
					this.TriggersDeadzone = (float)((double)obj);
				}
				if (dictionary.TryGetValue("Gamepad - InterfaceDeadzoneX", out obj))
				{
					this.InterfaceDeadzoneX = (float)((double)obj);
				}
				if (dictionary.TryGetValue("Gamepad - InventoryMoveCD", out obj))
				{
					this.InventoryMoveCD = (int)((long)obj);
				}
			}
			return true;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x004D6C88 File Offset: 0x004D4E88
		public Dictionary<string, object> Save()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary.Add("Last Launched Version", 279);
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
			dictionary.Add("Settings", dictionary2);
			dictionary.Add("Mouse And Keyboard", this.InputModes[InputMode.Keyboard].WritePreferences());
			dictionary.Add("Gamepad", this.InputModes[InputMode.XBoxGamepad].WritePreferences());
			dictionary.Add("Mouse And Keyboard UI", this.InputModes[InputMode.KeyboardUI].WritePreferences());
			dictionary.Add("Gamepad UI", this.InputModes[InputMode.XBoxGamepadUI].WritePreferences());
			return dictionary;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x004D6E50 File Offset: 0x004D5050
		public void ConditionalAddProfile(Dictionary<string, object> dicttouse, string k, InputMode nm, Dictionary<string, List<string>> dict)
		{
			if (PlayerInput.OriginalProfiles.ContainsKey(this.Name))
			{
				foreach (KeyValuePair<string, List<string>> keyValuePair in PlayerInput.OriginalProfiles[this.Name].InputModes[nm].WritePreferences())
				{
					bool flag = true;
					List<string> list;
					if (dict.TryGetValue(keyValuePair.Key, out list))
					{
						if (list.Count != keyValuePair.Value.Count)
						{
							flag = false;
						}
						if (!flag)
						{
							for (int i = 0; i < list.Count; i++)
							{
								if (list[i] != keyValuePair.Value[i])
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
						dict.Remove(keyValuePair.Key);
					}
				}
			}
			if (dict.Count > 0)
			{
				dicttouse.Add(k, dict);
			}
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x004D6F60 File Offset: 0x004D5160
		public void ConditionalAdd(Dictionary<string, object> dicttouse, string a, object b, Func<PlayerInputProfile, bool> check)
		{
			if (PlayerInput.OriginalProfiles.ContainsKey(this.Name) && check(PlayerInput.OriginalProfiles[this.Name]))
			{
				return;
			}
			dicttouse.Add(a, b);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x004D6F98 File Offset: 0x004D5198
		public void CopyGameplaySettingsFrom(PlayerInputProfile profile, InputMode mode)
		{
			string[] keysToCopy = new string[]
			{
				"MouseLeft",
				"MouseRight",
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

		// Token: 0x060017DE RID: 6110 RVA: 0x004D707C File Offset: 0x004D527C
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

		// Token: 0x060017DF RID: 6111 RVA: 0x004D7100 File Offset: 0x004D5300
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

		// Token: 0x060017E0 RID: 6112 RVA: 0x004D7150 File Offset: 0x004D5350
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

		// Token: 0x060017E1 RID: 6113 RVA: 0x004D71C8 File Offset: 0x004D53C8
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

		// Token: 0x060017E2 RID: 6114 RVA: 0x004D725C File Offset: 0x004D545C
		private void CopyKeysFrom(PlayerInputProfile profile, InputMode mode, string[] keysToCopy)
		{
			for (int i = 0; i < keysToCopy.Length; i++)
			{
				List<string> collection;
				if (profile.InputModes[mode].KeyStatus.TryGetValue(keysToCopy[i], out collection))
				{
					this.InputModes[mode].KeyStatus[keysToCopy[i]].Clear();
					this.InputModes[mode].KeyStatus[keysToCopy[i]].AddRange(collection);
				}
			}
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x004D72D4 File Offset: 0x004D54D4
		public bool UsingDpadHotbar()
		{
			return this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"].Contains(Buttons.DPadUp.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"].Contains(Buttons.DPadRight.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"].Contains(Buttons.DPadDown.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"].Contains(Buttons.DPadLeft.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"].Contains(Buttons.DPadUp.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"].Contains(Buttons.DPadRight.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"].Contains(Buttons.DPadDown.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"].Contains(Buttons.DPadLeft.ToString());
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x004D7478 File Offset: 0x004D5678
		public bool UsingDpadMovekeys()
		{
			return this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"].Contains(Buttons.DPadUp.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"].Contains(Buttons.DPadRight.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"].Contains(Buttons.DPadDown.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"].Contains(Buttons.DPadLeft.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"].Contains(Buttons.DPadUp.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"].Contains(Buttons.DPadRight.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"].Contains(Buttons.DPadDown.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"].Contains(Buttons.DPadLeft.ToString());
		}

		// Token: 0x04001472 RID: 5234
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

		// Token: 0x04001473 RID: 5235
		public string Name = "";

		// Token: 0x04001474 RID: 5236
		public bool AllowEditting = true;

		// Token: 0x04001475 RID: 5237
		public int HotbarRadialHoldTimeRequired = 16;

		// Token: 0x04001476 RID: 5238
		public float TriggersDeadzone = 0.3f;

		// Token: 0x04001477 RID: 5239
		public float InterfaceDeadzoneX = 0.2f;

		// Token: 0x04001478 RID: 5240
		public float LeftThumbstickDeadzoneX = 0.25f;

		// Token: 0x04001479 RID: 5241
		public float LeftThumbstickDeadzoneY = 0.4f;

		// Token: 0x0400147A RID: 5242
		public float RightThumbstickDeadzoneX;

		// Token: 0x0400147B RID: 5243
		public float RightThumbstickDeadzoneY;

		// Token: 0x0400147C RID: 5244
		public bool LeftThumbstickInvertX;

		// Token: 0x0400147D RID: 5245
		public bool LeftThumbstickInvertY;

		// Token: 0x0400147E RID: 5246
		public bool RightThumbstickInvertX;

		// Token: 0x0400147F RID: 5247
		public bool RightThumbstickInvertY;

		// Token: 0x04001480 RID: 5248
		public int InventoryMoveCD = 6;
	}
}
