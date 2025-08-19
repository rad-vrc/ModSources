using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Chat;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameInput
{
	// Token: 0x02000483 RID: 1155
	public class PlayerInput
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x0058B2FD File Offset: 0x005894FD
		public static string ListeningTrigger
		{
			get
			{
				return PlayerInput._listeningTrigger;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060037C3 RID: 14275 RVA: 0x0058B304 File Offset: 0x00589504
		public static bool CurrentlyRebinding
		{
			get
			{
				return PlayerInput._listeningTrigger != null;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060037C4 RID: 14276 RVA: 0x0058B310 File Offset: 0x00589510
		public static bool InvisibleGamepadInMenus
		{
			get
			{
				return ((Main.gameMenu || Main.ingameOptionsWindow || Main.playerInventory || Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1 || Main.InGameUI.CurrentState != null) && !PlayerInput._InBuildingMode && Main.InvisibleCursorForGamepad) || (PlayerInput.CursorIsBusy && !PlayerInput._InBuildingMode);
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060037C5 RID: 14277 RVA: 0x0058B385 File Offset: 0x00589585
		public static PlayerInputProfile CurrentProfile
		{
			get
			{
				return PlayerInput._currentProfile;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x0058B38C File Offset: 0x0058958C
		public static KeyConfiguration ProfileGamepadUI
		{
			get
			{
				return PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI];
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060037C7 RID: 14279 RVA: 0x0058B39E File Offset: 0x0058959E
		public static bool UsingGamepad
		{
			get
			{
				return PlayerInput.CurrentInputMode == InputMode.XBoxGamepad || PlayerInput.CurrentInputMode == InputMode.XBoxGamepadUI;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060037C8 RID: 14280 RVA: 0x0058B3B2 File Offset: 0x005895B2
		public static bool UsingGamepadUI
		{
			get
			{
				return PlayerInput.CurrentInputMode == InputMode.XBoxGamepadUI;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060037C9 RID: 14281 RVA: 0x0058B3BC File Offset: 0x005895BC
		public static bool IgnoreMouseInterface
		{
			get
			{
				if (Main.LocalPlayer.itemAnimation > 0 && !PlayerInput.UsingGamepad)
				{
					return true;
				}
				bool flag = PlayerInput.UsingGamepad && !UILinkPointNavigator.Available;
				return (!flag || !PlayerInput.SteamDeckIsUsed || PlayerInput.SettingsForUI.CurrentCursorMode != CursorMode.Mouse || Main.mouseRight) && flag;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x0058B40D File Offset: 0x0058960D
		public static bool SteamDeckIsUsed
		{
			get
			{
				return PlayerInput.UseSteamDeckIfPossible;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060037CB RID: 14283 RVA: 0x0058B414 File Offset: 0x00589614
		public static bool ShouldFastUseItem
		{
			get
			{
				return PlayerInput._fastUseMemory.CanFastUse();
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060037CC RID: 14284 RVA: 0x0058B420 File Offset: 0x00589620
		public static bool InBuildingMode
		{
			get
			{
				return PlayerInput._InBuildingMode;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060037CD RID: 14285 RVA: 0x0058B427 File Offset: 0x00589627
		public static int RealScreenWidth
		{
			get
			{
				return PlayerInput._originalScreenWidth;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060037CE RID: 14286 RVA: 0x0058B42E File Offset: 0x0058962E
		public static int RealScreenHeight
		{
			get
			{
				return PlayerInput._originalScreenHeight;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060037CF RID: 14287 RVA: 0x0058B435 File Offset: 0x00589635
		public static bool CursorIsBusy
		{
			get
			{
				return ItemSlot.CircularRadialOpacity > 0f || ItemSlot.QuicksRadialOpacity > 0f;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x0058B451 File Offset: 0x00589651
		public static Vector2 OriginalScreenSize
		{
			get
			{
				return new Vector2((float)PlayerInput._originalScreenWidth, (float)PlayerInput._originalScreenHeight);
			}
		}

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060037D1 RID: 14289 RVA: 0x0058B464 File Offset: 0x00589664
		// (remove) Token: 0x060037D2 RID: 14290 RVA: 0x0058B498 File Offset: 0x00589698
		public static event Action OnBindingChange;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x060037D3 RID: 14291 RVA: 0x0058B4CC File Offset: 0x005896CC
		// (remove) Token: 0x060037D4 RID: 14292 RVA: 0x0058B500 File Offset: 0x00589700
		public static event Action OnActionableInput;

		// Token: 0x060037D5 RID: 14293 RVA: 0x0058B533 File Offset: 0x00589733
		public static void ListenFor(string triggerName, InputMode inputmode)
		{
			PlayerInput._listeningTrigger = triggerName;
			PlayerInput._listeningInputMode = inputmode;
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x0058B544 File Offset: 0x00589744
		private static bool InvalidateKeyboardSwap()
		{
			if (PlayerInput._invalidatorCheck.Length == 0)
			{
				return false;
			}
			string text = "";
			List<Keys> pressedKeys = PlayerInput.GetPressedKeys();
			for (int i = 0; i < pressedKeys.Count; i++)
			{
				text = text + pressedKeys[i] + ", ";
			}
			if (text == PlayerInput._invalidatorCheck)
			{
				return true;
			}
			PlayerInput._invalidatorCheck = "";
			return false;
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x0058B5B0 File Offset: 0x005897B0
		public static void ResetInputsOnActiveStateChange()
		{
			bool isActive = Main.instance.IsActive;
			if (PlayerInput._lastActivityState != isActive)
			{
				PlayerInput.MouseInfo = default(MouseState);
				PlayerInput.MouseInfoOld = default(MouseState);
				Main.keyState = Keyboard.GetState();
				Main.inputText = Keyboard.GetState();
				Main.oldInputText = Keyboard.GetState();
				Main.keyCount = 0;
				PlayerInput.Triggers.Reset();
				PlayerInput.Triggers.Reset();
				string text = "";
				List<Keys> pressedKeys = PlayerInput.GetPressedKeys();
				for (int i = 0; i < pressedKeys.Count; i++)
				{
					text = text + pressedKeys[i] + ", ";
				}
				PlayerInput._invalidatorCheck = text;
			}
			PlayerInput._lastActivityState = isActive;
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x0058B664 File Offset: 0x00589864
		public static List<Keys> GetPressedKeys()
		{
			List<Keys> list = Main.keyState.GetPressedKeys().ToList<Keys>();
			for (int num = list.Count - 1; num >= 0; num--)
			{
				if (list[num] == null || list[num] == 25)
				{
					list.RemoveAt(num);
				}
			}
			return list;
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x0058B6B0 File Offset: 0x005898B0
		public static void TryEnteringFastUseModeForInventorySlot(int inventorySlot)
		{
			PlayerInput._fastUseMemory.TryStartForItemSlot(Main.LocalPlayer, inventorySlot);
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x0058B6C3 File Offset: 0x005898C3
		public static void TryEnteringFastUseModeForMouseItem()
		{
			PlayerInput._fastUseMemory.TryStartForMouse(Main.LocalPlayer);
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x0058B6D5 File Offset: 0x005898D5
		public static void TryEndingFastUse()
		{
			PlayerInput._fastUseMemory.EndFastUse();
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x0058B6E4 File Offset: 0x005898E4
		public static void EnterBuildingMode()
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			PlayerInput._InBuildingMode = true;
			PlayerInput._UIPointForBuildingMode = UILinkPointNavigator.CurrentPoint;
			if (Main.mouseItem.stack <= 0)
			{
				int uIPointForBuildingMode = PlayerInput._UIPointForBuildingMode;
				if (uIPointForBuildingMode < 50 && uIPointForBuildingMode >= 0 && Main.player[Main.myPlayer].inventory[uIPointForBuildingMode].stack > 0)
				{
					Utils.Swap<Item>(ref Main.mouseItem, ref Main.player[Main.myPlayer].inventory[uIPointForBuildingMode]);
				}
			}
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x0058B76C File Offset: 0x0058996C
		public static void ExitBuildingMode()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			PlayerInput._InBuildingMode = false;
			UILinkPointNavigator.ChangePoint(PlayerInput._UIPointForBuildingMode);
			if (Main.mouseItem.stack > 0 && Main.player[Main.myPlayer].itemAnimation == 0)
			{
				int uIPointForBuildingMode = PlayerInput._UIPointForBuildingMode;
				if (uIPointForBuildingMode < 50 && uIPointForBuildingMode >= 0 && Main.player[Main.myPlayer].inventory[uIPointForBuildingMode].stack <= 0)
				{
					Utils.Swap<Item>(ref Main.mouseItem, ref Main.player[Main.myPlayer].inventory[uIPointForBuildingMode]);
				}
			}
			PlayerInput._UIPointForBuildingMode = -1;
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x0058B80C File Offset: 0x00589A0C
		public static void VerifyBuildingMode()
		{
			if (PlayerInput._InBuildingMode)
			{
				Player player = Main.player[Main.myPlayer];
				bool flag = false;
				if (Main.mouseItem.stack <= 0)
				{
					flag = true;
				}
				if (player.dead)
				{
					flag = true;
				}
				if (flag)
				{
					PlayerInput.ExitBuildingMode();
				}
			}
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x0058B84D File Offset: 0x00589A4D
		public static void SetSelectedProfile(string name)
		{
			if (PlayerInput.Profiles.ContainsKey(name))
			{
				PlayerInput._selectedProfile = name;
				PlayerInput._currentProfile = PlayerInput.Profiles[PlayerInput._selectedProfile];
			}
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x0058B876 File Offset: 0x00589A76
		private static void ReInitialize()
		{
			PlayerInput.Profiles.Clear();
			PlayerInput.OriginalProfiles.Clear();
			PlayerInput.Initialize_Inner();
			PlayerInput.Load();
			PlayerInput.reinitialize = false;
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x0058B89C File Offset: 0x00589A9C
		public static void Initialize()
		{
			Preferences inputProfiles = Main.InputProfiles;
			Preferences.TextProcessAction value;
			if ((value = PlayerInput.<>O.<0>__PrettyPrintProfiles) == null)
			{
				value = (PlayerInput.<>O.<0>__PrettyPrintProfiles = new Preferences.TextProcessAction(PlayerInput.PrettyPrintProfiles));
			}
			inputProfiles.OnProcessText += value;
			Action<Player> value2;
			if ((value2 = PlayerInput.<>O.<1>__Hook_OnEnterWorld) == null)
			{
				value2 = (PlayerInput.<>O.<1>__Hook_OnEnterWorld = new Action<Player>(PlayerInput.Hook_OnEnterWorld));
			}
			Player.Hooks.OnEnterWorld += value2;
			PlayerInput.Initialize_Inner();
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x0058B8F4 File Offset: 0x00589AF4
		private static void Initialize_Inner()
		{
			PlayerInputProfile playerInputProfile = new PlayerInputProfile("Redigit's Pick");
			playerInputProfile.Initialize(PresetProfiles.Redigit);
			PlayerInput.Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Yoraiz0r's Pick");
			playerInputProfile.Initialize(PresetProfiles.Yoraiz0r);
			PlayerInput.Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Console (Playstation)");
			playerInputProfile.Initialize(PresetProfiles.ConsolePS);
			PlayerInput.Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Console (Xbox)");
			playerInputProfile.Initialize(PresetProfiles.ConsoleXBox);
			PlayerInput.Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Custom");
			playerInputProfile.Initialize(PresetProfiles.Redigit);
			PlayerInput.Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Redigit's Pick");
			playerInputProfile.Initialize(PresetProfiles.Redigit);
			PlayerInput.OriginalProfiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Yoraiz0r's Pick");
			playerInputProfile.Initialize(PresetProfiles.Yoraiz0r);
			PlayerInput.OriginalProfiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Console (Playstation)");
			playerInputProfile.Initialize(PresetProfiles.ConsolePS);
			PlayerInput.OriginalProfiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Console (Xbox)");
			playerInputProfile.Initialize(PresetProfiles.ConsoleXBox);
			PlayerInput.OriginalProfiles.Add(playerInputProfile.Name, playerInputProfile);
			PlayerInput.SetSelectedProfile("Custom");
			PlayerInput.Triggers.Initialize();
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x0058BA50 File Offset: 0x00589C50
		public static void Hook_OnEnterWorld(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				Main.SmartCursorWanted_GamePad = true;
			}
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x0058BA68 File Offset: 0x00589C68
		public static bool Save()
		{
			Main.InputProfiles.Clear();
			Main.InputProfiles.Put("Selected Profile", PlayerInput._selectedProfile);
			foreach (KeyValuePair<string, PlayerInputProfile> profile in PlayerInput.Profiles)
			{
				Main.InputProfiles.Put(profile.Value.Name, profile.Value.Save());
			}
			return Main.InputProfiles.Save(true);
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x0058BB00 File Offset: 0x00589D00
		public static void Load()
		{
			if (!Main.InputProfiles.Load())
			{
				return;
			}
			Dictionary<string, PlayerInputProfile> dictionary = new Dictionary<string, PlayerInputProfile>();
			string currentValue = null;
			Main.InputProfiles.Get<string>("Selected Profile", ref currentValue);
			List<string> allKeys = Main.InputProfiles.GetAllKeys();
			for (int i = 0; i < allKeys.Count; i++)
			{
				string text = allKeys[i];
				if (!(text == "Selected Profile") && !string.IsNullOrEmpty(text))
				{
					Dictionary<string, object> currentValue2 = new Dictionary<string, object>();
					Main.InputProfiles.Get<Dictionary<string, object>>(text, ref currentValue2);
					if (currentValue2.Count > 0)
					{
						PlayerInputProfile playerInputProfile = new PlayerInputProfile(text);
						playerInputProfile.Initialize(PresetProfiles.None);
						if (playerInputProfile.Load(currentValue2))
						{
							dictionary.Add(text, playerInputProfile);
						}
					}
				}
			}
			if (dictionary.Count > 0)
			{
				PlayerInput.Profiles = dictionary;
				if (!string.IsNullOrEmpty(currentValue) && PlayerInput.Profiles.ContainsKey(currentValue))
				{
					PlayerInput.SetSelectedProfile(currentValue);
					return;
				}
				PlayerInput.SetSelectedProfile(PlayerInput.Profiles.Keys.First<string>());
			}
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x0058BBF4 File Offset: 0x00589DF4
		public static void ManageVersion_1_3()
		{
			PlayerInputProfile playerInputProfile = PlayerInput.Profiles["Custom"];
			string[,] array2 = new string[20, 2];
			array2[0, 0] = "KeyUp";
			array2[0, 1] = "Up";
			array2[1, 0] = "KeyDown";
			array2[1, 1] = "Down";
			array2[2, 0] = "KeyLeft";
			array2[2, 1] = "Left";
			array2[3, 0] = "KeyRight";
			array2[3, 1] = "Right";
			array2[4, 0] = "KeyJump";
			array2[4, 1] = "Jump";
			array2[5, 0] = "KeyThrowItem";
			array2[5, 1] = "Throw";
			array2[6, 0] = "KeyInventory";
			array2[6, 1] = "Inventory";
			array2[7, 0] = "KeyQuickHeal";
			array2[7, 1] = "QuickHeal";
			array2[8, 0] = "KeyQuickMana";
			array2[8, 1] = "QuickMana";
			array2[9, 0] = "KeyQuickBuff";
			array2[9, 1] = "QuickBuff";
			array2[10, 0] = "KeyUseHook";
			array2[10, 1] = "Grapple";
			array2[11, 0] = "KeyAutoSelect";
			array2[11, 1] = "SmartSelect";
			array2[12, 0] = "KeySmartCursor";
			array2[12, 1] = "SmartCursor";
			array2[13, 0] = "KeyMount";
			array2[13, 1] = "QuickMount";
			array2[14, 0] = "KeyMapStyle";
			array2[14, 1] = "MapStyle";
			array2[15, 0] = "KeyFullscreenMap";
			array2[15, 1] = "MapFull";
			array2[16, 0] = "KeyMapZoomIn";
			array2[16, 1] = "MapZoomIn";
			array2[17, 0] = "KeyMapZoomOut";
			array2[17, 1] = "MapZoomOut";
			array2[18, 0] = "KeyMapAlphaUp";
			array2[18, 1] = "MapAlphaUp";
			array2[19, 0] = "KeyMapAlphaDown";
			array2[19, 1] = "MapAlphaDown";
			string[,] array = array2;
			for (int i = 0; i < array.GetLength(0); i++)
			{
				string currentValue = null;
				Main.Configuration.Get<string>(array[i, 0], ref currentValue);
				if (currentValue != null)
				{
					playerInputProfile.InputModes[InputMode.Keyboard].KeyStatus[array[i, 1]] = new List<string>
					{
						currentValue
					};
					playerInputProfile.InputModes[InputMode.KeyboardUI].KeyStatus[array[i, 1]] = new List<string>
					{
						currentValue
					};
				}
			}
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x0058BEB8 File Offset: 0x0058A0B8
		public static void LockGamepadButtons(string TriggerName)
		{
			List<string> value = null;
			KeyConfiguration value2 = null;
			if (PlayerInput.CurrentProfile.InputModes.TryGetValue(PlayerInput.CurrentInputMode, out value2) && value2.KeyStatus.TryGetValue(TriggerName, out value))
			{
				PlayerInput._buttonsLocked.AddRange(value);
			}
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x0058BEFC File Offset: 0x0058A0FC
		public static bool IsGamepadButtonLockedFromUse(string keyName)
		{
			return PlayerInput._buttonsLocked.Contains(keyName);
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x0058BF0C File Offset: 0x0058A10C
		public static void UpdateInput()
		{
			if (PlayerInput.reinitialize)
			{
				PlayerInput.ReInitialize();
			}
			PlayerInput.SettingsForUI.UpdateCounters();
			PlayerInput.Triggers.Reset();
			PlayerInput.ScrollWheelValueOld = PlayerInput.ScrollWheelValue;
			PlayerInput.ScrollWheelValue = 0;
			PlayerInput.GamepadThumbstickLeft = Vector2.Zero;
			PlayerInput.GamepadThumbstickRight = Vector2.Zero;
			PlayerInput.GrappleAndInteractAreShared = ((PlayerInput.UsingGamepad || PlayerInput.SteamDeckIsUsed) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].DoGrappleAndInteractShareTheSameKey);
			if (PlayerInput.InBuildingMode && !PlayerInput.UsingGamepad)
			{
				PlayerInput.ExitBuildingMode();
			}
			if (PlayerInput._canReleaseRebindingLock && PlayerInput.NavigatorRebindingLock > 0)
			{
				PlayerInput.NavigatorRebindingLock--;
				PlayerInput.Triggers.Current.UsedMovementKey = false;
				if (PlayerInput.NavigatorRebindingLock == 0 && PlayerInput._memoOfLastPoint != -1)
				{
					UIManageControls.ForceMoveTo = PlayerInput._memoOfLastPoint;
					PlayerInput._memoOfLastPoint = -1;
				}
			}
			PlayerInput._canReleaseRebindingLock = true;
			PlayerInput.VerifyBuildingMode();
			PlayerInput.MouseInput();
			bool flag = false | PlayerInput.KeyboardInput() > false | PlayerInput.GamePadInput() > false;
			PlayerInput.Triggers.Update();
			PlayerInput.PostInput();
			PlayerInput.ScrollWheelDelta = PlayerInput.ScrollWheelValue - PlayerInput.ScrollWheelValueOld;
			PlayerInput.ScrollWheelDeltaForUI = PlayerInput.ScrollWheelDelta;
			PlayerInput.WritingText = false;
			PlayerInput.UpdateMainMouse();
			Main.mouseLeft = PlayerInput.Triggers.Current.MouseLeft;
			Main.mouseRight = PlayerInput.Triggers.Current.MouseRight;
			Main.mouseMiddle = PlayerInput.Triggers.Current.MouseMiddle;
			Main.mouseXButton1 = PlayerInput.Triggers.Current.MouseXButton1;
			Main.mouseXButton2 = PlayerInput.Triggers.Current.MouseXButton2;
			PlayerInput.CacheZoomableValues();
			if (flag && PlayerInput.OnActionableInput != null)
			{
				PlayerInput.OnActionableInput();
			}
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x0058C0B3 File Offset: 0x0058A2B3
		public static void UpdateMainMouse()
		{
			Main.lastMouseX = Main.mouseX;
			Main.lastMouseY = Main.mouseY;
			Main.mouseX = PlayerInput.MouseX;
			Main.mouseY = PlayerInput.MouseY;
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x0058C0DD File Offset: 0x0058A2DD
		public static void CacheZoomableValues()
		{
			PlayerInput.CacheOriginalInput();
			PlayerInput.CacheOriginalScreenDimensions();
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x0058C0EC File Offset: 0x0058A2EC
		public static void CacheMousePositionForZoom()
		{
			float num = 1f;
			PlayerInput._originalMouseX = (int)((float)Main.mouseX * num);
			PlayerInput._originalMouseY = (int)((float)Main.mouseY * num);
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x0058C11B File Offset: 0x0058A31B
		private static void CacheOriginalInput()
		{
			PlayerInput._originalMouseX = Main.mouseX;
			PlayerInput._originalMouseY = Main.mouseY;
			PlayerInput._originalLastMouseX = Main.lastMouseX;
			PlayerInput._originalLastMouseY = Main.lastMouseY;
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x0058C145 File Offset: 0x0058A345
		public static void CacheOriginalScreenDimensions()
		{
			PlayerInput._originalScreenWidth = Main.screenWidth;
			PlayerInput._originalScreenHeight = Main.screenHeight;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x0058C15C File Offset: 0x0058A35C
		private static bool GamePadInput()
		{
			bool flag = false;
			PlayerInput.ScrollWheelValue += PlayerInput.GamepadScrollValue;
			GamePadState gamePadState = default(GamePadState);
			bool flag2 = false;
			for (int i = 0; i < 4; i++)
			{
				GamePadState state = GamePad.GetState(i);
				if (state.IsConnected)
				{
					flag2 = true;
					gamePadState = state;
					break;
				}
			}
			if (Main.SettingBlockGamepadsEntirely)
			{
				return false;
			}
			if (!flag2)
			{
				return false;
			}
			if (!Main.instance.IsActive && !Main.AllowUnfocusedInputOnGamepad)
			{
				return false;
			}
			Player player = Main.player[Main.myPlayer];
			bool flag3 = UILinkPointNavigator.Available && !PlayerInput.InBuildingMode;
			InputMode inputMode = InputMode.XBoxGamepad;
			if (Main.gameMenu || flag3 || player.talkNPC != -1 || player.sign != -1 || IngameFancyUI.CanCover())
			{
				inputMode = InputMode.XBoxGamepadUI;
			}
			if (!Main.gameMenu && PlayerInput.InBuildingMode)
			{
				inputMode = InputMode.XBoxGamepad;
			}
			if (PlayerInput.CurrentInputMode == InputMode.XBoxGamepad && inputMode == InputMode.XBoxGamepadUI)
			{
				flag = true;
			}
			if (PlayerInput.CurrentInputMode == InputMode.XBoxGamepadUI && inputMode == InputMode.XBoxGamepad)
			{
				flag = true;
			}
			if (flag)
			{
				PlayerInput.CurrentInputMode = inputMode;
			}
			KeyConfiguration keyConfiguration = PlayerInput.CurrentProfile.InputModes[inputMode];
			int num = 2145386496;
			for (int j = 0; j < PlayerInput.ButtonsGamepad.Length; j++)
			{
				if ((num & PlayerInput.ButtonsGamepad[j]) <= 0)
				{
					string text = PlayerInput.ButtonsGamepad[j].ToString();
					bool flag4 = PlayerInput._buttonsLocked.Contains(text);
					if (gamePadState.IsButtonDown(PlayerInput.ButtonsGamepad[j]))
					{
						if (!flag4)
						{
							if (PlayerInput.CheckRebindingProcessGamepad(text))
							{
								return false;
							}
							keyConfiguration.Processkey(PlayerInput.Triggers.Current, text, inputMode);
							flag = true;
						}
					}
					else
					{
						PlayerInput._buttonsLocked.Remove(text);
					}
				}
			}
			PlayerInput.GamepadThumbstickLeft = gamePadState.ThumbSticks.Left * new Vector2(1f, -1f) * new Vector2((float)(PlayerInput.CurrentProfile.LeftThumbstickInvertX.ToDirectionInt() * -1), (float)(PlayerInput.CurrentProfile.LeftThumbstickInvertY.ToDirectionInt() * -1));
			PlayerInput.GamepadThumbstickRight = gamePadState.ThumbSticks.Right * new Vector2(1f, -1f) * new Vector2((float)(PlayerInput.CurrentProfile.RightThumbstickInvertX.ToDirectionInt() * -1), (float)(PlayerInput.CurrentProfile.RightThumbstickInvertY.ToDirectionInt() * -1));
			Vector2 gamepadThumbstickRight = PlayerInput.GamepadThumbstickRight;
			Vector2 gamepadThumbstickLeft = PlayerInput.GamepadThumbstickLeft;
			Vector2 vector = gamepadThumbstickRight;
			if (vector != Vector2.Zero)
			{
				vector.Normalize();
			}
			Vector2 vector2 = gamepadThumbstickLeft;
			if (vector2 != Vector2.Zero)
			{
				vector2.Normalize();
			}
			float num2 = 0.6f;
			float triggersDeadzone = PlayerInput.CurrentProfile.TriggersDeadzone;
			if (inputMode == InputMode.XBoxGamepadUI)
			{
				num2 = 0.4f;
				if (PlayerInput.GamepadAllowScrolling)
				{
					PlayerInput.GamepadScrollValue -= (int)(gamepadThumbstickRight.Y * 16f);
				}
				PlayerInput.GamepadAllowScrolling = false;
			}
			if (Vector2.Dot(-Vector2.UnitX, vector2) >= num2 && gamepadThumbstickLeft.X < 0f - PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(2097152.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, 2097152.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitX, vector2) >= num2 && gamepadThumbstickLeft.X > PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(1073741824.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, 1073741824.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(-Vector2.UnitY, vector2) >= num2 && gamepadThumbstickLeft.Y < 0f - PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(268435456.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, 268435456.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitY, vector2) >= num2 && gamepadThumbstickLeft.Y > PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(536870912.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, 536870912.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(-Vector2.UnitX, vector) >= num2 && gamepadThumbstickRight.X < 0f - PlayerInput.CurrentProfile.RightThumbstickDeadzoneX)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(134217728.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, 134217728.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitX, vector) >= num2 && gamepadThumbstickRight.X > PlayerInput.CurrentProfile.RightThumbstickDeadzoneX)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(67108864.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, 67108864.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(-Vector2.UnitY, vector) >= num2 && gamepadThumbstickRight.Y < 0f - PlayerInput.CurrentProfile.RightThumbstickDeadzoneY)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(16777216.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, 16777216.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitY, vector) >= num2 && gamepadThumbstickRight.Y > PlayerInput.CurrentProfile.RightThumbstickDeadzoneY)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(33554432.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, 33554432.ToString(), inputMode);
				flag = true;
			}
			if (gamePadState.Triggers.Left > triggersDeadzone)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(8388608.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, 8388608.ToString(), inputMode);
				flag = true;
			}
			if (gamePadState.Triggers.Right > triggersDeadzone)
			{
				string newKey = 4194304.ToString();
				if (PlayerInput.CheckRebindingProcessGamepad(newKey))
				{
					return false;
				}
				if (inputMode == InputMode.XBoxGamepadUI && PlayerInput.SteamDeckIsUsed && PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Mouse)
				{
					PlayerInput.Triggers.Current.MouseLeft = true;
				}
				else
				{
					keyConfiguration.Processkey(PlayerInput.Triggers.Current, newKey, inputMode);
					flag = true;
				}
			}
			if (player.HeldItem.type >= ItemID.Sets.GamepadWholeScreenUseRange.Length)
			{
				return false;
			}
			bool flag5 = ItemID.Sets.GamepadWholeScreenUseRange[player.inventory[player.selectedItem].type] || player.scope;
			Item item = player.inventory[player.selectedItem];
			int num3 = item.tileBoost + ItemID.Sets.GamepadExtraRange[item.type];
			if (player.yoyoString && ItemID.Sets.Yoyo[item.type])
			{
				num3 += 5;
			}
			else if (item.createTile < 0 && item.createWall <= 0 && item.shoot > 0)
			{
				num3 += 10;
			}
			else if (player.controlTorch)
			{
				num3++;
			}
			if (item.createWall > 0 || item.createTile > 0 || item.tileWand > 0)
			{
				num3 += player.blockRange;
			}
			if (flag5)
			{
				num3 += 30;
			}
			if (player.mount.Active && player.mount.Type == 8)
			{
				num3 = 10;
			}
			bool flag6 = false;
			bool flag7 = !Main.gameMenu && !flag3 && Main.SmartCursorWanted;
			if (!PlayerInput.CursorIsBusy)
			{
				bool flag8 = Main.mapFullscreen || (!Main.gameMenu && !flag3);
				int num4 = Main.screenWidth / 2;
				int num5 = Main.screenHeight / 2;
				if (!Main.mapFullscreen && flag8 && !flag5)
				{
					Point point = Main.ReverseGravitySupport(player.Center - Main.screenPosition, 0f).ToPoint();
					num4 = point.X;
					num5 = point.Y;
				}
				if (player.velocity == Vector2.Zero && gamepadThumbstickLeft == Vector2.Zero && gamepadThumbstickRight == Vector2.Zero && flag7)
				{
					num4 += player.direction * 10;
				}
				float k = Main.GameViewMatrix.ZoomMatrix.M11;
				PlayerInput.smartSelectPointer.UpdateSize(new Vector2((float)(Player.tileRangeX * 16 + num3 * 16), (float)(Player.tileRangeY * 16 + num3 * 16)) * k);
				if (flag5)
				{
					PlayerInput.smartSelectPointer.UpdateSize(new Vector2((float)(Math.Max(Main.screenWidth, Main.screenHeight) / 2)));
				}
				PlayerInput.smartSelectPointer.UpdateCenter(new Vector2((float)num4, (float)num5));
				if (gamepadThumbstickRight != Vector2.Zero && flag8)
				{
					Vector2 vector3;
					vector3..ctor(8f);
					if (!Main.gameMenu && Main.mapFullscreen)
					{
						vector3..ctor(16f);
					}
					if (flag7)
					{
						vector3..ctor((float)(Player.tileRangeX * 16), (float)(Player.tileRangeY * 16));
						if (num3 != 0)
						{
							vector3 += new Vector2((float)(num3 * 16), (float)(num3 * 16));
						}
						if (flag5)
						{
							vector3..ctor((float)(Math.Max(Main.screenWidth, Main.screenHeight) / 2));
						}
					}
					else if (!Main.mapFullscreen)
					{
						if (player.inventory[player.selectedItem].mech)
						{
							vector3 += Vector2.Zero;
						}
						else
						{
							vector3 += new Vector2((float)num3) / 4f;
						}
					}
					float m2 = Main.GameViewMatrix.ZoomMatrix.M11;
					Vector2 vector4 = gamepadThumbstickRight * vector3 * m2;
					int num6 = PlayerInput.MouseX - num4;
					int num7 = PlayerInput.MouseY - num5;
					if (flag7)
					{
						num6 = 0;
						num7 = 0;
					}
					num6 += (int)vector4.X;
					num7 += (int)vector4.Y;
					PlayerInput.MouseX = num6 + num4;
					PlayerInput.MouseY = num7 + num5;
					flag = true;
					flag6 = true;
					PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Gamepad);
				}
				bool allowSecondaryGamepadAim = PlayerInput.SettingsForUI.AllowSecondaryGamepadAim;
				if (gamepadThumbstickLeft != Vector2.Zero && flag8)
				{
					float num8 = 8f;
					if (!Main.gameMenu && Main.mapFullscreen)
					{
						num8 = 3f;
					}
					if (Main.mapFullscreen)
					{
						Vector2 vector5 = gamepadThumbstickLeft * num8;
						Main.mapFullscreenPos += vector5 * num8 * (1f / Main.mapFullscreenScale);
					}
					else if (!flag6 && Main.SmartCursorWanted && allowSecondaryGamepadAim)
					{
						float m3 = Main.GameViewMatrix.ZoomMatrix.M11;
						Vector2 vector6 = gamepadThumbstickLeft * new Vector2((float)(Player.tileRangeX * 16), (float)(Player.tileRangeY * 16)) * m3;
						if (num3 != 0)
						{
							vector6 = gamepadThumbstickLeft * new Vector2((float)((Player.tileRangeX + num3) * 16), (float)((Player.tileRangeY + num3) * 16)) * m3;
						}
						if (flag5)
						{
							vector6 = new Vector2((float)(Math.Max(Main.screenWidth, Main.screenHeight) / 2)) * gamepadThumbstickLeft;
						}
						int num9 = (int)vector6.X;
						int num18 = (int)vector6.Y;
						PlayerInput.MouseX = num9 + num4;
						PlayerInput.MouseY = num18 + num5;
					}
					flag = true;
				}
				if (PlayerInput.CurrentInputMode == InputMode.XBoxGamepad)
				{
					PlayerInput.HandleDpadSnap();
					if (PlayerInput.SettingsForUI.AllowSecondaryGamepadAim)
					{
						int num10 = PlayerInput.MouseX - num4;
						int num11 = PlayerInput.MouseY - num5;
						if (!Main.gameMenu && !flag3)
						{
							if (flag5 && !Main.mapFullscreen)
							{
								float num12 = 1f;
								int num13 = Main.screenWidth / 2;
								int num14 = Main.screenHeight / 2;
								num10 = (int)Utils.Clamp<float>((float)num10, (float)(-(float)num13) * num12, (float)num13 * num12);
								num11 = (int)Utils.Clamp<float>((float)num11, (float)(-(float)num14) * num12, (float)num14 * num12);
							}
							else
							{
								float num15 = 0f;
								if (player.HeldItem.createTile >= 0 || player.HeldItem.createWall > 0 || player.HeldItem.tileWand >= 0)
								{
									num15 = 0.5f;
								}
								float m4 = Main.GameViewMatrix.ZoomMatrix.M11;
								float num16 = (0f - ((float)(Player.tileRangeY + num3) - num15)) * 16f * m4;
								float max = ((float)(Player.tileRangeY + num3) - num15) * 16f * m4;
								num16 -= (float)(player.height / 16 / 2 * 16);
								num10 = (int)Utils.Clamp<float>((float)num10, (0f - ((float)(Player.tileRangeX + num3) - num15)) * 16f * m4, ((float)(Player.tileRangeX + num3) - num15) * 16f * m4);
								num11 = (int)Utils.Clamp<float>((float)num11, num16, max);
							}
							if (flag7 && (!flag || flag5))
							{
								float num17 = 0.81f;
								if (flag5)
								{
									num17 = 0.95f;
								}
								num10 = (int)((float)num10 * num17);
								num11 = (int)((float)num11 * num17);
							}
						}
						else
						{
							num10 = Utils.Clamp<int>(num10, -num4 + 10, num4 - 10);
							num11 = Utils.Clamp<int>(num11, -num5 + 10, num5 - 10);
						}
						PlayerInput.MouseX = num10 + num4;
						PlayerInput.MouseY = num11 + num5;
					}
				}
			}
			if (flag)
			{
				PlayerInput.CurrentInputMode = inputMode;
			}
			if (PlayerInput.CurrentInputMode == InputMode.XBoxGamepad)
			{
				Main.SetCameraGamepadLerp(0.1f);
			}
			if (PlayerInput.CurrentInputMode != InputMode.XBoxGamepadUI && flag)
			{
				PlayerInput.PreventCursorModeSwappingToGamepad = true;
			}
			if (!flag)
			{
				PlayerInput.PreventCursorModeSwappingToGamepad = false;
			}
			if (PlayerInput.CurrentInputMode == InputMode.XBoxGamepadUI && flag && !PlayerInput.PreventCursorModeSwappingToGamepad)
			{
				PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Gamepad);
			}
			return flag;
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x0058CF9C File Offset: 0x0058B19C
		private static void MouseInput()
		{
			bool flag = false;
			PlayerInput.MouseInfoOld = PlayerInput.MouseInfo;
			PlayerInput.MouseInfo = Mouse.GetState();
			PlayerInput.ScrollWheelValue += PlayerInput.MouseInfo.ScrollWheelValue;
			if (PlayerInput.MouseInfo.X != PlayerInput.MouseInfoOld.X || PlayerInput.MouseInfo.Y != PlayerInput.MouseInfoOld.Y || PlayerInput.MouseInfo.ScrollWheelValue != PlayerInput.MouseInfoOld.ScrollWheelValue)
			{
				PlayerInput.MouseX = (int)((float)PlayerInput.MouseInfo.X * PlayerInput.RawMouseScale.X);
				PlayerInput.MouseY = (int)((float)PlayerInput.MouseInfo.Y * PlayerInput.RawMouseScale.Y);
				if (!PlayerInput.PreventFirstMousePositionGrab)
				{
					flag = true;
					PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Mouse);
				}
				PlayerInput.PreventFirstMousePositionGrab = false;
			}
			PlayerInput.MouseKeys.Clear();
			if (Main.instance.IsActive)
			{
				if (PlayerInput.MouseInfo.LeftButton == 1)
				{
					PlayerInput.MouseKeys.Add("Mouse1");
					flag = true;
				}
				if (PlayerInput.MouseInfo.RightButton == 1)
				{
					PlayerInput.MouseKeys.Add("Mouse2");
					flag = true;
				}
				if (PlayerInput.MouseInfo.MiddleButton == 1)
				{
					PlayerInput.MouseKeys.Add("Mouse3");
					flag = true;
				}
				if (PlayerInput.MouseInfo.XButton1 == 1)
				{
					PlayerInput.MouseKeys.Add("Mouse4");
					flag = true;
				}
				if (PlayerInput.MouseInfo.XButton2 == 1)
				{
					PlayerInput.MouseKeys.Add("Mouse5");
					flag = true;
				}
			}
			if (flag)
			{
				PlayerInput.CurrentInputMode = InputMode.Mouse;
				PlayerInput.Triggers.Current.UsedMovementKey = false;
			}
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x0058D12C File Offset: 0x0058B32C
		private static bool KeyboardInput()
		{
			bool flag = false;
			bool flag2 = false;
			List<Keys> pressedKeys = PlayerInput.GetPressedKeys();
			PlayerInput.DebugKeys(pressedKeys);
			if (pressedKeys.Count == 0 && PlayerInput.MouseKeys.Count == 0)
			{
				Main.blockKey = 0.ToString();
				return false;
			}
			for (int i = 0; i < pressedKeys.Count; i++)
			{
				if (pressedKeys[i] == 160 || pressedKeys[i] == 161)
				{
					flag = true;
				}
				else if (pressedKeys[i] == 164 || pressedKeys[i] == 165)
				{
					flag2 = true;
				}
				Main.ChromaPainter.PressKey(pressedKeys[i]);
			}
			if (Main.blockKey != 0.ToString())
			{
				bool flag3 = false;
				for (int j = 0; j < pressedKeys.Count; j++)
				{
					if (pressedKeys[j].ToString() == Main.blockKey)
					{
						pressedKeys[j] = 0;
						flag3 = true;
					}
				}
				if (!flag3)
				{
					Main.blockKey = 0.ToString();
				}
			}
			KeyConfiguration keyConfiguration = PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard];
			if (Main.gameMenu && !PlayerInput.WritingText)
			{
				keyConfiguration = PlayerInput.CurrentProfile.InputModes[InputMode.KeyboardUI];
			}
			List<string> list = new List<string>(pressedKeys.Count);
			for (int k = 0; k < pressedKeys.Count; k++)
			{
				list.Add(pressedKeys[k].ToString());
			}
			if (PlayerInput.WritingText)
			{
				list.Clear();
			}
			int count = list.Count;
			list.AddRange(PlayerInput.MouseKeys);
			bool flag4 = false;
			for (int l = 0; l < list.Count; l++)
			{
				if (l >= count || pressedKeys[l] != null)
				{
					string newKey = list[l];
					if (!(list[l] == 9.ToString()) || ((!flag || SocialAPI.Mode != SocialMode.Steam) && !flag2))
					{
						if (PlayerInput.CheckRebindingProcessKeyboard(newKey))
						{
							return false;
						}
						KeyboardState oldKeyState = Main.oldKeyState;
						if (l >= count || !Main.oldKeyState.IsKeyDown(pressedKeys[l]))
						{
							keyConfiguration.Processkey(PlayerInput.Triggers.Current, newKey, InputMode.Keyboard);
						}
						else
						{
							keyConfiguration.CopyKeyState(PlayerInput.Triggers.Old, PlayerInput.Triggers.Current, newKey);
						}
						if (l >= count || pressedKeys[l] != null)
						{
							flag4 = true;
						}
					}
				}
			}
			if (flag4)
			{
				PlayerInput.CurrentInputMode = InputMode.Keyboard;
			}
			return flag4;
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x0058D3D7 File Offset: 0x0058B5D7
		private static void DebugKeys(List<Keys> keys)
		{
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x0058D3DC File Offset: 0x0058B5DC
		private static void FixDerpedRebinds()
		{
			List<string> list = new List<string>
			{
				"MouseLeft",
				"MouseRight",
				"Inventory"
			};
			foreach (object obj in Enum.GetValues(typeof(InputMode)))
			{
				InputMode value = (InputMode)obj;
				if (value != InputMode.Mouse)
				{
					PlayerInput.FixKeysConflict(value, list);
					foreach (string item in list)
					{
						if (PlayerInput.CurrentProfile.InputModes[value].KeyStatus[item].Count < 1)
						{
							PlayerInput.ResetKeyBinding(value, item);
						}
					}
				}
			}
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x0058D4D0 File Offset: 0x0058B6D0
		private static void FixKeysConflict(InputMode inputMode, List<string> triggers)
		{
			for (int i = 0; i < triggers.Count; i++)
			{
				for (int j = i + 1; j < triggers.Count; j++)
				{
					List<string> list = PlayerInput.CurrentProfile.InputModes[inputMode].KeyStatus[triggers[i]];
					List<string> list2 = PlayerInput.CurrentProfile.InputModes[inputMode].KeyStatus[triggers[j]];
					foreach (string item in list.Intersect(list2).ToList<string>())
					{
						list.Remove(item);
						list2.Remove(item);
					}
				}
			}
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x0058D5AC File Offset: 0x0058B7AC
		private static void ResetKeyBinding(InputMode inputMode, string trigger)
		{
			string key = "Redigit's Pick";
			if (PlayerInput.OriginalProfiles.ContainsKey(PlayerInput._selectedProfile))
			{
				key = PlayerInput._selectedProfile;
			}
			PlayerInput.CurrentProfile.InputModes[inputMode].KeyStatus[trigger].Clear();
			PlayerInput.CurrentProfile.InputModes[inputMode].KeyStatus[trigger].AddRange(PlayerInput.OriginalProfiles[key].InputModes[inputMode].KeyStatus[trigger]);
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x0058D638 File Offset: 0x0058B838
		private static bool CheckRebindingProcessGamepad(string newKey)
		{
			PlayerInput._canReleaseRebindingLock = false;
			if (PlayerInput.CurrentlyRebinding && PlayerInput._listeningInputMode == InputMode.XBoxGamepad)
			{
				PlayerInput.NavigatorRebindingLock = 3;
				PlayerInput._memoOfLastPoint = UILinkPointNavigator.CurrentPoint;
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[PlayerInput.ListeningTrigger].Contains(newKey))
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[PlayerInput.ListeningTrigger].Remove(newKey);
				}
				else
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[PlayerInput.ListeningTrigger] = new List<string>
					{
						newKey
					};
				}
				PlayerInput.ListenFor(null, InputMode.XBoxGamepad);
			}
			if (PlayerInput.CurrentlyRebinding && PlayerInput._listeningInputMode == InputMode.XBoxGamepadUI)
			{
				PlayerInput.NavigatorRebindingLock = 3;
				PlayerInput._memoOfLastPoint = UILinkPointNavigator.CurrentPoint;
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[PlayerInput.ListeningTrigger].Contains(newKey))
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[PlayerInput.ListeningTrigger].Remove(newKey);
				}
				else
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[PlayerInput.ListeningTrigger] = new List<string>
					{
						newKey
					};
				}
				PlayerInput.ListenFor(null, InputMode.XBoxGamepadUI);
			}
			PlayerInput.FixDerpedRebinds();
			if (PlayerInput.OnBindingChange != null)
			{
				PlayerInput.OnBindingChange();
			}
			return PlayerInput.NavigatorRebindingLock > 0;
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x0058D7E0 File Offset: 0x0058B9E0
		private static bool CheckRebindingProcessKeyboard(string newKey)
		{
			PlayerInput._canReleaseRebindingLock = false;
			if (PlayerInput.CurrentlyRebinding && PlayerInput._listeningInputMode == InputMode.Keyboard)
			{
				PlayerInput.NavigatorRebindingLock = 3;
				PlayerInput._memoOfLastPoint = UILinkPointNavigator.CurrentPoint;
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[PlayerInput.ListeningTrigger].Contains(newKey))
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[PlayerInput.ListeningTrigger].Remove(newKey);
				}
				else
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[PlayerInput.ListeningTrigger] = new List<string>
					{
						newKey
					};
				}
				PlayerInput.ListenFor(null, InputMode.Keyboard);
				Main.blockKey = newKey;
				Main.blockInput = false;
				Main.ChromaPainter.CollectBoundKeys();
			}
			if (PlayerInput.CurrentlyRebinding && PlayerInput._listeningInputMode == InputMode.KeyboardUI)
			{
				PlayerInput.NavigatorRebindingLock = 3;
				PlayerInput._memoOfLastPoint = UILinkPointNavigator.CurrentPoint;
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (PlayerInput.CurrentProfile.InputModes[InputMode.KeyboardUI].KeyStatus[PlayerInput.ListeningTrigger].Contains(newKey))
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.KeyboardUI].KeyStatus[PlayerInput.ListeningTrigger].Remove(newKey);
				}
				else
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.KeyboardUI].KeyStatus[PlayerInput.ListeningTrigger] = new List<string>
					{
						newKey
					};
				}
				PlayerInput.ListenFor(null, InputMode.KeyboardUI);
				Main.blockKey = newKey;
				Main.blockInput = false;
				Main.ChromaPainter.CollectBoundKeys();
			}
			PlayerInput.FixDerpedRebinds();
			if (PlayerInput.OnBindingChange != null)
			{
				PlayerInput.OnBindingChange();
			}
			return PlayerInput.NavigatorRebindingLock > 0;
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x0058D9B4 File Offset: 0x0058BBB4
		private static void PostInput()
		{
			Main.GamepadCursorAlpha = MathHelper.Clamp(Main.GamepadCursorAlpha + ((Main.SmartCursorIsUsed && !UILinkPointNavigator.Available && PlayerInput.GamepadThumbstickLeft == Vector2.Zero && PlayerInput.GamepadThumbstickRight == Vector2.Zero) ? -0.05f : 0.05f), 0f, 1f);
			if (PlayerInput.CurrentProfile.HotbarAllowsRadial)
			{
				int num = PlayerInput.Triggers.Current.HotbarPlus.ToInt() - PlayerInput.Triggers.Current.HotbarMinus.ToInt();
				if (PlayerInput.MiscSettingsTEMP.HotbarRadialShouldBeUsed)
				{
					if (num != -1)
					{
						if (num == 1)
						{
							PlayerInput.Triggers.Current.RadialHotbar = true;
							PlayerInput.Triggers.JustReleased.RadialHotbar = false;
						}
					}
					else
					{
						PlayerInput.Triggers.Current.RadialQuickbar = true;
						PlayerInput.Triggers.JustReleased.RadialQuickbar = false;
					}
				}
			}
			PlayerInput.MiscSettingsTEMP.HotbarRadialShouldBeUsed = false;
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x0058DAA4 File Offset: 0x0058BCA4
		private static void HandleDpadSnap()
		{
			Vector2 zero = Vector2.Zero;
			Player player = Main.player[Main.myPlayer];
			for (int i = 0; i < 4; i++)
			{
				bool flag = false;
				Vector2 vector = Vector2.Zero;
				if (Main.gameMenu || (UILinkPointNavigator.Available && !PlayerInput.InBuildingMode))
				{
					return;
				}
				switch (i)
				{
				case 0:
					flag = PlayerInput.Triggers.Current.DpadMouseSnap1;
					vector = -Vector2.UnitY;
					break;
				case 1:
					flag = PlayerInput.Triggers.Current.DpadMouseSnap2;
					vector = Vector2.UnitX;
					break;
				case 2:
					flag = PlayerInput.Triggers.Current.DpadMouseSnap3;
					vector = Vector2.UnitY;
					break;
				case 3:
					flag = PlayerInput.Triggers.Current.DpadMouseSnap4;
					vector = -Vector2.UnitX;
					break;
				}
				if (PlayerInput.DpadSnapCooldown[i] > 0)
				{
					PlayerInput.DpadSnapCooldown[i]--;
				}
				if (flag)
				{
					if (PlayerInput.DpadSnapCooldown[i] == 0)
					{
						int num = 6;
						if (ItemSlot.IsABuildingItem(player.inventory[player.selectedItem]))
						{
							num = CombinedHooks.TotalUseTime((float)player.inventory[player.selectedItem].useTime, player, player.inventory[player.selectedItem]);
						}
						PlayerInput.DpadSnapCooldown[i] = num;
						zero += vector;
					}
				}
				else
				{
					PlayerInput.DpadSnapCooldown[i] = 0;
				}
			}
			if (zero != Vector2.Zero)
			{
				Main.SmartCursorWanted_GamePad = false;
				Matrix zoomMatrix = Main.GameViewMatrix.ZoomMatrix;
				Matrix matrix = Matrix.Invert(zoomMatrix);
				Vector2 mouseScreen = Main.MouseScreen;
				Vector2.Transform(Main.screenPosition, matrix);
				Vector2 vector2 = Vector2.Transform((Vector2.Transform(mouseScreen, matrix) + zero * new Vector2(16f) + Main.screenPosition).ToTileCoordinates().ToWorldCoordinates(8f, 8f) - Main.screenPosition, zoomMatrix);
				PlayerInput.MouseX = (int)vector2.X;
				PlayerInput.MouseY = (int)vector2.Y;
				PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Gamepad);
			}
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x0058DC9E File Offset: 0x0058BE9E
		private static bool ShouldShowInstructionsForGamepad()
		{
			return PlayerInput.UsingGamepad || PlayerInput.SteamDeckIsUsed;
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x0058DCB0 File Offset: 0x0058BEB0
		public unsafe static string ComposeInstructionsForGamepad()
		{
			string empty = string.Empty;
			InputMode inputMode = InputMode.XBoxGamepad;
			if (Main.gameMenu || UILinkPointNavigator.Available)
			{
				inputMode = InputMode.XBoxGamepadUI;
			}
			if (PlayerInput.InBuildingMode && !Main.gameMenu)
			{
				inputMode = InputMode.XBoxGamepad;
			}
			KeyConfiguration keyConfiguration = PlayerInput.CurrentProfile.InputModes[inputMode];
			if (Main.mapFullscreen && !Main.gameMenu)
			{
				empty += "          ";
				empty += PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
				});
				empty += PlayerInput.BuildCommand(Lang.inter[118].Value, false, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
				empty += PlayerInput.BuildCommand(Lang.inter[119].Value, false, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"]
				});
				if (Main.netMode == 1 && Main.player[Main.myPlayer].HasItem(2997))
				{
					empty += PlayerInput.BuildCommand(Lang.inter[120].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
					});
				}
			}
			else if (inputMode == InputMode.XBoxGamepadUI && !PlayerInput.InBuildingMode)
			{
				empty = UILinkPointNavigator.GetInstructions();
			}
			else
			{
				empty += PlayerInput.BuildCommand(Lang.misc[58].Value, false, new List<string>[]
				{
					keyConfiguration.KeyStatus["Jump"]
				});
				empty += PlayerInput.BuildCommand(Lang.misc[59].Value, false, new List<string>[]
				{
					keyConfiguration.KeyStatus["HotbarMinus"],
					keyConfiguration.KeyStatus["HotbarPlus"]
				});
				if (PlayerInput.InBuildingMode)
				{
					empty += PlayerInput.BuildCommand(Lang.menu[6].Value, false, new List<string>[]
					{
						keyConfiguration.KeyStatus["Inventory"],
						keyConfiguration.KeyStatus["MouseRight"]
					});
				}
				if (WiresUI.Open)
				{
					empty += PlayerInput.BuildCommand(Lang.misc[53].Value, false, new List<string>[]
					{
						keyConfiguration.KeyStatus["MouseLeft"]
					});
					empty += PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
					{
						keyConfiguration.KeyStatus["MouseRight"]
					});
				}
				else
				{
					Item item = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem];
					empty = ((item.damage > 0 && item.ammo == 0) ? (empty + PlayerInput.BuildCommand(Lang.misc[60].Value, false, new List<string>[]
					{
						keyConfiguration.KeyStatus["MouseLeft"]
					})) : ((item.createTile < 0 && item.createWall <= 0) ? (empty + PlayerInput.BuildCommand(Lang.misc[63].Value, false, new List<string>[]
					{
						keyConfiguration.KeyStatus["MouseLeft"]
					})) : (empty + PlayerInput.BuildCommand(Lang.misc[61].Value, false, new List<string>[]
					{
						keyConfiguration.KeyStatus["MouseLeft"]
					}))));
					bool flag = true;
					bool flag2 = Main.SmartInteractProj != -1 || Main.HasInteractibleObjectThatIsNotATile;
					bool flag3 = !Main.SmartInteractShowingGenuine && Main.SmartInteractShowingFake;
					if (Main.SmartInteractShowingGenuine || Main.SmartInteractShowingFake || flag2)
					{
						if (Main.SmartInteractNPC != -1)
						{
							if (flag3)
							{
								flag = false;
							}
							empty += PlayerInput.BuildCommand(Lang.misc[80].Value, false, new List<string>[]
							{
								keyConfiguration.KeyStatus["MouseRight"]
							});
						}
						else if (flag2)
						{
							if (flag3)
							{
								flag = false;
							}
							empty += PlayerInput.BuildCommand(Lang.misc[79].Value, false, new List<string>[]
							{
								keyConfiguration.KeyStatus["MouseRight"]
							});
						}
						else if (Main.SmartInteractX != -1 && Main.SmartInteractY != -1)
						{
							if (flag3)
							{
								flag = false;
							}
							Tile tile = Main.tile[Main.SmartInteractX, Main.SmartInteractY];
							empty = ((!TileID.Sets.TileInteractRead[(int)(*tile.type)]) ? (empty + PlayerInput.BuildCommand(Lang.misc[79].Value, false, new List<string>[]
							{
								keyConfiguration.KeyStatus["MouseRight"]
							})) : (empty + PlayerInput.BuildCommand(Lang.misc[81].Value, false, new List<string>[]
							{
								keyConfiguration.KeyStatus["MouseRight"]
							})));
						}
					}
					else if (WiresUI.Settings.DrawToolModeUI)
					{
						empty += PlayerInput.BuildCommand(Lang.misc[89].Value, false, new List<string>[]
						{
							keyConfiguration.KeyStatus["MouseRight"]
						});
					}
					if ((!PlayerInput.GrappleAndInteractAreShared || (!WiresUI.Settings.DrawToolModeUI && (!Main.SmartInteractShowingGenuine || !Main.HasSmartInteractTarget) && (!Main.SmartInteractShowingFake || flag))) && Main.LocalPlayer.QuickGrapple_GetItemToUse() != null)
					{
						empty += PlayerInput.BuildCommand(Lang.misc[57].Value, false, new List<string>[]
						{
							keyConfiguration.KeyStatus["Grapple"]
						});
					}
				}
			}
			return empty;
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x0058E260 File Offset: 0x0058C460
		public static string BuildCommand(string CommandText, bool Last, params List<string>[] Bindings)
		{
			string text = "";
			if (Bindings.Length == 0)
			{
				return text;
			}
			text += PlayerInput.GenerateGlyphList(Bindings[0]);
			for (int i = 1; i < Bindings.Length; i++)
			{
				string text2 = PlayerInput.GenerateGlyphList(Bindings[i]);
				if (text2.Length > 0)
				{
					text = text + "/" + text2;
				}
			}
			if (text.Length > 0)
			{
				text = text + ": " + CommandText;
				if (!Last)
				{
					text += "   ";
				}
			}
			return text;
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x0058E2DC File Offset: 0x0058C4DC
		public static string GenerateInputTag_ForCurrentGamemode_WithHacks(bool tagForGameplay, string triggerName)
		{
			InputMode inputMode = PlayerInput.CurrentInputMode;
			if (inputMode == InputMode.Mouse || inputMode == InputMode.KeyboardUI)
			{
				inputMode = InputMode.Keyboard;
			}
			if (!(triggerName == "SmartSelect"))
			{
				if (triggerName == "SmartCursor" && inputMode == InputMode.Keyboard)
				{
					return PlayerInput.GenerateRawInputList(new List<string>
					{
						164.ToString()
					});
				}
			}
			else if (inputMode == InputMode.Keyboard)
			{
				return PlayerInput.GenerateRawInputList(new List<string>
				{
					162.ToString()
				});
			}
			return PlayerInput.GenerateInputTag_ForCurrentGamemode(tagForGameplay, triggerName);
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x0058E368 File Offset: 0x0058C568
		public static string GenerateInputTag_ForCurrentGamemode(bool tagForGameplay, string triggerName)
		{
			InputMode inputMode = PlayerInput.CurrentInputMode;
			if (inputMode == InputMode.Mouse || inputMode == InputMode.KeyboardUI)
			{
				inputMode = InputMode.Keyboard;
			}
			if (tagForGameplay)
			{
				if (inputMode - InputMode.XBoxGamepad > 1)
				{
					return PlayerInput.GenerateRawInputList(PlayerInput.CurrentProfile.InputModes[inputMode].KeyStatus[triggerName]);
				}
				return PlayerInput.GenerateGlyphList(PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[triggerName]);
			}
			else
			{
				if (inputMode - InputMode.XBoxGamepad > 1)
				{
					return PlayerInput.GenerateRawInputList(PlayerInput.CurrentProfile.InputModes[inputMode].KeyStatus[triggerName]);
				}
				return PlayerInput.GenerateGlyphList(PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[triggerName]);
			}
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x0058E417 File Offset: 0x0058C617
		public static string GenerateInputTags_GamepadUI(string triggerName)
		{
			return PlayerInput.GenerateGlyphList(PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[triggerName]);
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x0058E439 File Offset: 0x0058C639
		public static string GenerateInputTags_Gamepad(string triggerName)
		{
			return PlayerInput.GenerateGlyphList(PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[triggerName]);
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x0058E45C File Offset: 0x0058C65C
		private static string GenerateGlyphList(List<string> list)
		{
			if (list.Count == 0)
			{
				return "";
			}
			string text = GlyphTagHandler.GenerateTag(list[0]);
			for (int i = 1; i < list.Count; i++)
			{
				text = text + "/" + GlyphTagHandler.GenerateTag(list[i]);
			}
			return text;
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x0058E4B0 File Offset: 0x0058C6B0
		private static string GenerateRawInputList(List<string> list)
		{
			if (list.Count == 0)
			{
				return "";
			}
			string text = list[0];
			for (int i = 1; i < list.Count; i++)
			{
				text = text + "/" + list[i];
			}
			return text;
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x0058E4F8 File Offset: 0x0058C6F8
		public static void NavigatorCachePosition()
		{
			PlayerInput.PreUIX = PlayerInput.MouseX;
			PlayerInput.PreUIY = PlayerInput.MouseY;
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x0058E50E File Offset: 0x0058C70E
		public static void NavigatorUnCachePosition()
		{
			PlayerInput.MouseX = PlayerInput.PreUIX;
			PlayerInput.MouseY = PlayerInput.PreUIY;
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x0058E524 File Offset: 0x0058C724
		public static void LockOnCachePosition()
		{
			PlayerInput.PreLockOnX = PlayerInput.MouseX;
			PlayerInput.PreLockOnY = PlayerInput.MouseY;
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x0058E53A File Offset: 0x0058C73A
		public static void LockOnUnCachePosition()
		{
			PlayerInput.MouseX = PlayerInput.PreLockOnX;
			PlayerInput.MouseY = PlayerInput.PreLockOnY;
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x0058E550 File Offset: 0x0058C750
		public static void PrettyPrintProfiles(ref string text)
		{
			foreach (string text2 in text.Split(new string[]
			{
				"\r\n"
			}, StringSplitOptions.None))
			{
				if (text2.Contains(": {"))
				{
					string text3 = text2.Substring(0, text2.IndexOf('"'));
					string text4 = text2 + "\r\n  ";
					string newValue = text4.Replace(": {\r\n  ", ": \r\n" + text3 + "{\r\n  ");
					text = text.Replace(text4, newValue);
				}
			}
			text = text.Replace("[\r\n        ", "[");
			text = text.Replace("[\r\n      ", "[");
			text = text.Replace("\"\r\n      ", "\"");
			text = text.Replace("\",\r\n        ", "\", ");
			text = text.Replace("\",\r\n      ", "\", ");
			text = text.Replace("\r\n    ]", "]");
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x0058E650 File Offset: 0x0058C850
		public static void PrettyPrintProfilesOld(ref string text)
		{
			text = text.Replace(": {\r\n  ", ": \r\n  {\r\n  ");
			text = text.Replace("[\r\n      ", "[");
			text = text.Replace("\"\r\n      ", "\"");
			text = text.Replace("\",\r\n      ", "\", ");
			text = text.Replace("\r\n    ]", "]");
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x0058E6BC File Offset: 0x0058C8BC
		public static void Reset(KeyConfiguration c, PresetProfiles style, InputMode mode)
		{
			PlayerInput.OnReset(c, mode);
			switch (style)
			{
			case PresetProfiles.Redigit:
				switch (mode)
				{
				case InputMode.Keyboard:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Jump"].Add("Space");
					c.KeyStatus["Inventory"].Add("Escape");
					c.KeyStatus["Grapple"].Add("E");
					c.KeyStatus["SmartSelect"].Add("LeftShift");
					c.KeyStatus["SmartCursor"].Add("LeftControl");
					c.KeyStatus["QuickMount"].Add("R");
					c.KeyStatus["QuickHeal"].Add("H");
					c.KeyStatus["QuickMana"].Add("J");
					c.KeyStatus["QuickBuff"].Add("B");
					c.KeyStatus["MapStyle"].Add("Tab");
					c.KeyStatus["MapFull"].Add("M");
					c.KeyStatus["MapZoomIn"].Add("Add");
					c.KeyStatus["MapZoomOut"].Add("Subtract");
					c.KeyStatus["MapAlphaUp"].Add("PageUp");
					c.KeyStatus["MapAlphaDown"].Add("PageDown");
					c.KeyStatus["Hotbar1"].Add("D1");
					c.KeyStatus["Hotbar2"].Add("D2");
					c.KeyStatus["Hotbar3"].Add("D3");
					c.KeyStatus["Hotbar4"].Add("D4");
					c.KeyStatus["Hotbar5"].Add("D5");
					c.KeyStatus["Hotbar6"].Add("D6");
					c.KeyStatus["Hotbar7"].Add("D7");
					c.KeyStatus["Hotbar8"].Add("D8");
					c.KeyStatus["Hotbar9"].Add("D9");
					c.KeyStatus["Hotbar10"].Add("D0");
					c.KeyStatus["ViewZoomOut"].Add("OemMinus");
					c.KeyStatus["ViewZoomIn"].Add("OemPlus");
					c.KeyStatus["ToggleCreativeMenu"].Add("C");
					c.KeyStatus["Loadout1"].Add("F1");
					c.KeyStatus["Loadout2"].Add("F2");
					c.KeyStatus["Loadout3"].Add("F3");
					c.KeyStatus["ToggleCameraMode"].Add("F4");
					return;
				case InputMode.KeyboardUI:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseLeft"].Add("Space");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Up"].Add("Up");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Down"].Add("Down");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Left"].Add("Left");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Right"].Add("Right");
					c.KeyStatus["Inventory"].Add(27.ToString());
					c.KeyStatus["MenuUp"].Add(string.Concat(1));
					c.KeyStatus["MenuDown"].Add(string.Concat(2));
					c.KeyStatus["MenuLeft"].Add(string.Concat(4));
					c.KeyStatus["MenuRight"].Add(string.Concat(8));
					return;
				case InputMode.Mouse:
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(4194304));
					c.KeyStatus["MouseRight"].Add(string.Concat(8192));
					c.KeyStatus["Up"].Add(string.Concat(268435456));
					c.KeyStatus["Down"].Add(string.Concat(536870912));
					c.KeyStatus["Left"].Add(string.Concat(2097152));
					c.KeyStatus["Right"].Add(string.Concat(1073741824));
					c.KeyStatus["Jump"].Add(string.Concat(8388608));
					c.KeyStatus["Inventory"].Add(string.Concat(32768));
					c.KeyStatus["Grapple"].Add(string.Concat(8192));
					c.KeyStatus["LockOn"].Add(string.Concat(16384));
					c.KeyStatus["QuickMount"].Add(string.Concat(4096));
					c.KeyStatus["SmartSelect"].Add(string.Concat(128));
					c.KeyStatus["SmartCursor"].Add(string.Concat(64));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(256));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(512));
					c.KeyStatus["MapFull"].Add(string.Concat(16));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(1));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(2));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(4));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(8));
					c.KeyStatus["MapStyle"].Add(string.Concat(32));
					return;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(4096));
					c.KeyStatus["MouseRight"].Add(string.Concat(256));
					c.KeyStatus["SmartCursor"].Add(string.Concat(512));
					c.KeyStatus["Up"].Add(string.Concat(268435456));
					c.KeyStatus["Down"].Add(string.Concat(536870912));
					c.KeyStatus["Left"].Add(string.Concat(2097152));
					c.KeyStatus["Right"].Add(string.Concat(1073741824));
					c.KeyStatus["Inventory"].Add(string.Concat(8192));
					c.KeyStatus["Inventory"].Add(string.Concat(32768));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(8388608));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(4194304));
					c.KeyStatus["Grapple"].Add(string.Concat(16384));
					c.KeyStatus["MapFull"].Add(string.Concat(16));
					c.KeyStatus["SmartSelect"].Add(string.Concat(32));
					c.KeyStatus["QuickMount"].Add(string.Concat(128));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(1));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(2));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(4));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(8));
					c.KeyStatus["MenuUp"].Add(string.Concat(1));
					c.KeyStatus["MenuDown"].Add(string.Concat(2));
					c.KeyStatus["MenuLeft"].Add(string.Concat(4));
					c.KeyStatus["MenuRight"].Add(string.Concat(8));
					return;
				default:
					return;
				}
				break;
			case PresetProfiles.Yoraiz0r:
				switch (mode)
				{
				case InputMode.Keyboard:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Jump"].Add("Space");
					c.KeyStatus["Inventory"].Add("Escape");
					c.KeyStatus["Grapple"].Add("E");
					c.KeyStatus["SmartSelect"].Add("LeftShift");
					c.KeyStatus["SmartCursor"].Add("LeftControl");
					c.KeyStatus["QuickMount"].Add("R");
					c.KeyStatus["QuickHeal"].Add("H");
					c.KeyStatus["QuickMana"].Add("J");
					c.KeyStatus["QuickBuff"].Add("B");
					c.KeyStatus["MapStyle"].Add("Tab");
					c.KeyStatus["MapFull"].Add("M");
					c.KeyStatus["MapZoomIn"].Add("Add");
					c.KeyStatus["MapZoomOut"].Add("Subtract");
					c.KeyStatus["MapAlphaUp"].Add("PageUp");
					c.KeyStatus["MapAlphaDown"].Add("PageDown");
					c.KeyStatus["Hotbar1"].Add("D1");
					c.KeyStatus["Hotbar2"].Add("D2");
					c.KeyStatus["Hotbar3"].Add("D3");
					c.KeyStatus["Hotbar4"].Add("D4");
					c.KeyStatus["Hotbar5"].Add("D5");
					c.KeyStatus["Hotbar6"].Add("D6");
					c.KeyStatus["Hotbar7"].Add("D7");
					c.KeyStatus["Hotbar8"].Add("D8");
					c.KeyStatus["Hotbar9"].Add("D9");
					c.KeyStatus["Hotbar10"].Add("D0");
					c.KeyStatus["ViewZoomOut"].Add("OemMinus");
					c.KeyStatus["ViewZoomIn"].Add("OemPlus");
					c.KeyStatus["ToggleCreativeMenu"].Add("C");
					c.KeyStatus["Loadout1"].Add("F1");
					c.KeyStatus["Loadout2"].Add("F2");
					c.KeyStatus["Loadout3"].Add("F3");
					c.KeyStatus["ToggleCameraMode"].Add("F4");
					return;
				case InputMode.KeyboardUI:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseLeft"].Add("Space");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Up"].Add("Up");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Down"].Add("Down");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Left"].Add("Left");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Right"].Add("Right");
					c.KeyStatus["Inventory"].Add(27.ToString());
					c.KeyStatus["MenuUp"].Add(string.Concat(1));
					c.KeyStatus["MenuDown"].Add(string.Concat(2));
					c.KeyStatus["MenuLeft"].Add(string.Concat(4));
					c.KeyStatus["MenuRight"].Add(string.Concat(8));
					return;
				case InputMode.Mouse:
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(4194304));
					c.KeyStatus["MouseRight"].Add(string.Concat(8192));
					c.KeyStatus["Up"].Add(string.Concat(268435456));
					c.KeyStatus["Down"].Add(string.Concat(536870912));
					c.KeyStatus["Left"].Add(string.Concat(2097152));
					c.KeyStatus["Right"].Add(string.Concat(1073741824));
					c.KeyStatus["Jump"].Add(string.Concat(8388608));
					c.KeyStatus["Inventory"].Add(string.Concat(32768));
					c.KeyStatus["Grapple"].Add(string.Concat(256));
					c.KeyStatus["SmartSelect"].Add(string.Concat(64));
					c.KeyStatus["SmartCursor"].Add(string.Concat(128));
					c.KeyStatus["QuickMount"].Add(string.Concat(16384));
					c.KeyStatus["QuickHeal"].Add(string.Concat(4096));
					c.KeyStatus["RadialHotbar"].Add(string.Concat(512));
					c.KeyStatus["MapFull"].Add(string.Concat(16));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(1));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(2));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(4));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(8));
					c.KeyStatus["MapStyle"].Add(string.Concat(32));
					return;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(4096));
					c.KeyStatus["MouseRight"].Add(string.Concat(256));
					c.KeyStatus["SmartCursor"].Add(string.Concat(512));
					c.KeyStatus["Up"].Add(string.Concat(268435456));
					c.KeyStatus["Down"].Add(string.Concat(536870912));
					c.KeyStatus["Left"].Add(string.Concat(2097152));
					c.KeyStatus["Right"].Add(string.Concat(1073741824));
					c.KeyStatus["LockOn"].Add(string.Concat(8192));
					c.KeyStatus["Inventory"].Add(string.Concat(32768));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(8388608));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(4194304));
					c.KeyStatus["Grapple"].Add(string.Concat(16384));
					c.KeyStatus["MapFull"].Add(string.Concat(16));
					c.KeyStatus["SmartSelect"].Add(string.Concat(32));
					c.KeyStatus["QuickMount"].Add(string.Concat(128));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(1));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(2));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(4));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(8));
					c.KeyStatus["MenuUp"].Add(string.Concat(1));
					c.KeyStatus["MenuDown"].Add(string.Concat(2));
					c.KeyStatus["MenuLeft"].Add(string.Concat(4));
					c.KeyStatus["MenuRight"].Add(string.Concat(8));
					return;
				default:
					return;
				}
				break;
			case PresetProfiles.ConsolePS:
				switch (mode)
				{
				case InputMode.Keyboard:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Jump"].Add("Space");
					c.KeyStatus["Inventory"].Add("Escape");
					c.KeyStatus["Grapple"].Add("E");
					c.KeyStatus["SmartSelect"].Add("LeftShift");
					c.KeyStatus["SmartCursor"].Add("LeftControl");
					c.KeyStatus["QuickMount"].Add("R");
					c.KeyStatus["QuickHeal"].Add("H");
					c.KeyStatus["QuickMana"].Add("J");
					c.KeyStatus["QuickBuff"].Add("B");
					c.KeyStatus["MapStyle"].Add("Tab");
					c.KeyStatus["MapFull"].Add("M");
					c.KeyStatus["MapZoomIn"].Add("Add");
					c.KeyStatus["MapZoomOut"].Add("Subtract");
					c.KeyStatus["MapAlphaUp"].Add("PageUp");
					c.KeyStatus["MapAlphaDown"].Add("PageDown");
					c.KeyStatus["Hotbar1"].Add("D1");
					c.KeyStatus["Hotbar2"].Add("D2");
					c.KeyStatus["Hotbar3"].Add("D3");
					c.KeyStatus["Hotbar4"].Add("D4");
					c.KeyStatus["Hotbar5"].Add("D5");
					c.KeyStatus["Hotbar6"].Add("D6");
					c.KeyStatus["Hotbar7"].Add("D7");
					c.KeyStatus["Hotbar8"].Add("D8");
					c.KeyStatus["Hotbar9"].Add("D9");
					c.KeyStatus["Hotbar10"].Add("D0");
					c.KeyStatus["ViewZoomOut"].Add("OemMinus");
					c.KeyStatus["ViewZoomIn"].Add("OemPlus");
					c.KeyStatus["ToggleCreativeMenu"].Add("C");
					c.KeyStatus["Loadout1"].Add("F1");
					c.KeyStatus["Loadout2"].Add("F2");
					c.KeyStatus["Loadout3"].Add("F3");
					c.KeyStatus["ToggleCameraMode"].Add("F4");
					return;
				case InputMode.KeyboardUI:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseLeft"].Add("Space");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Up"].Add("Up");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Down"].Add("Down");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Left"].Add("Left");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Right"].Add("Right");
					c.KeyStatus["MenuUp"].Add(string.Concat(1));
					c.KeyStatus["MenuDown"].Add(string.Concat(2));
					c.KeyStatus["MenuLeft"].Add(string.Concat(4));
					c.KeyStatus["MenuRight"].Add(string.Concat(8));
					c.KeyStatus["Inventory"].Add(27.ToString());
					return;
				case InputMode.Mouse:
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(512));
					c.KeyStatus["MouseRight"].Add(string.Concat(8192));
					c.KeyStatus["Up"].Add(string.Concat(268435456));
					c.KeyStatus["Down"].Add(string.Concat(536870912));
					c.KeyStatus["Left"].Add(string.Concat(2097152));
					c.KeyStatus["Right"].Add(string.Concat(1073741824));
					c.KeyStatus["Jump"].Add(string.Concat(4096));
					c.KeyStatus["LockOn"].Add(string.Concat(16384));
					c.KeyStatus["Inventory"].Add(string.Concat(32768));
					c.KeyStatus["Grapple"].Add(string.Concat(256));
					c.KeyStatus["SmartSelect"].Add(string.Concat(64));
					c.KeyStatus["SmartCursor"].Add(string.Concat(128));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(8388608));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(4194304));
					c.KeyStatus["MapFull"].Add(string.Concat(16));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(1));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(2));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(4));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(8));
					c.KeyStatus["QuickMount"].Add(string.Concat(32));
					return;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(4096));
					c.KeyStatus["MouseRight"].Add(string.Concat(256));
					c.KeyStatus["SmartCursor"].Add(string.Concat(512));
					c.KeyStatus["Up"].Add(string.Concat(268435456));
					c.KeyStatus["Down"].Add(string.Concat(536870912));
					c.KeyStatus["Left"].Add(string.Concat(2097152));
					c.KeyStatus["Right"].Add(string.Concat(1073741824));
					c.KeyStatus["Inventory"].Add(string.Concat(8192));
					c.KeyStatus["Inventory"].Add(string.Concat(32768));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(8388608));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(4194304));
					c.KeyStatus["Grapple"].Add(string.Concat(16384));
					c.KeyStatus["MapFull"].Add(string.Concat(16));
					c.KeyStatus["SmartSelect"].Add(string.Concat(32));
					c.KeyStatus["QuickMount"].Add(string.Concat(128));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(1));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(2));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(4));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(8));
					c.KeyStatus["MenuUp"].Add(string.Concat(1));
					c.KeyStatus["MenuDown"].Add(string.Concat(2));
					c.KeyStatus["MenuLeft"].Add(string.Concat(4));
					c.KeyStatus["MenuRight"].Add(string.Concat(8));
					return;
				default:
					return;
				}
				break;
			case PresetProfiles.ConsoleXBox:
				switch (mode)
				{
				case InputMode.Keyboard:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Jump"].Add("Space");
					c.KeyStatus["Inventory"].Add("Escape");
					c.KeyStatus["Grapple"].Add("E");
					c.KeyStatus["SmartSelect"].Add("LeftShift");
					c.KeyStatus["SmartCursor"].Add("LeftControl");
					c.KeyStatus["QuickMount"].Add("R");
					c.KeyStatus["QuickHeal"].Add("H");
					c.KeyStatus["QuickMana"].Add("J");
					c.KeyStatus["QuickBuff"].Add("B");
					c.KeyStatus["MapStyle"].Add("Tab");
					c.KeyStatus["MapFull"].Add("M");
					c.KeyStatus["MapZoomIn"].Add("Add");
					c.KeyStatus["MapZoomOut"].Add("Subtract");
					c.KeyStatus["MapAlphaUp"].Add("PageUp");
					c.KeyStatus["MapAlphaDown"].Add("PageDown");
					c.KeyStatus["Hotbar1"].Add("D1");
					c.KeyStatus["Hotbar2"].Add("D2");
					c.KeyStatus["Hotbar3"].Add("D3");
					c.KeyStatus["Hotbar4"].Add("D4");
					c.KeyStatus["Hotbar5"].Add("D5");
					c.KeyStatus["Hotbar6"].Add("D6");
					c.KeyStatus["Hotbar7"].Add("D7");
					c.KeyStatus["Hotbar8"].Add("D8");
					c.KeyStatus["Hotbar9"].Add("D9");
					c.KeyStatus["Hotbar10"].Add("D0");
					c.KeyStatus["ViewZoomOut"].Add("OemMinus");
					c.KeyStatus["ViewZoomIn"].Add("OemPlus");
					c.KeyStatus["ToggleCreativeMenu"].Add("C");
					c.KeyStatus["Loadout1"].Add("F1");
					c.KeyStatus["Loadout2"].Add("F2");
					c.KeyStatus["Loadout3"].Add("F3");
					c.KeyStatus["ToggleCameraMode"].Add("F4");
					return;
				case InputMode.KeyboardUI:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseLeft"].Add("Space");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Up"].Add("Up");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Down"].Add("Down");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Left"].Add("Left");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Right"].Add("Right");
					c.KeyStatus["MenuUp"].Add(string.Concat(1));
					c.KeyStatus["MenuDown"].Add(string.Concat(2));
					c.KeyStatus["MenuLeft"].Add(string.Concat(4));
					c.KeyStatus["MenuRight"].Add(string.Concat(8));
					c.KeyStatus["Inventory"].Add(27.ToString());
					return;
				case InputMode.Mouse:
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(4194304));
					c.KeyStatus["MouseRight"].Add(string.Concat(8192));
					c.KeyStatus["Up"].Add(string.Concat(268435456));
					c.KeyStatus["Down"].Add(string.Concat(536870912));
					c.KeyStatus["Left"].Add(string.Concat(2097152));
					c.KeyStatus["Right"].Add(string.Concat(1073741824));
					c.KeyStatus["Jump"].Add(string.Concat(4096));
					c.KeyStatus["LockOn"].Add(string.Concat(16384));
					c.KeyStatus["Inventory"].Add(string.Concat(32768));
					c.KeyStatus["Grapple"].Add(string.Concat(8388608));
					c.KeyStatus["SmartSelect"].Add(string.Concat(64));
					c.KeyStatus["SmartCursor"].Add(string.Concat(128));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(256));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(512));
					c.KeyStatus["MapFull"].Add(string.Concat(16));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(1));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(2));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(4));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(8));
					c.KeyStatus["QuickMount"].Add(string.Concat(32));
					return;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(4096));
					c.KeyStatus["MouseRight"].Add(string.Concat(256));
					c.KeyStatus["SmartCursor"].Add(string.Concat(512));
					c.KeyStatus["Up"].Add(string.Concat(268435456));
					c.KeyStatus["Down"].Add(string.Concat(536870912));
					c.KeyStatus["Left"].Add(string.Concat(2097152));
					c.KeyStatus["Right"].Add(string.Concat(1073741824));
					c.KeyStatus["Inventory"].Add(string.Concat(8192));
					c.KeyStatus["Inventory"].Add(string.Concat(32768));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(8388608));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(4194304));
					c.KeyStatus["Grapple"].Add(string.Concat(16384));
					c.KeyStatus["MapFull"].Add(string.Concat(16));
					c.KeyStatus["SmartSelect"].Add(string.Concat(32));
					c.KeyStatus["QuickMount"].Add(string.Concat(128));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(1));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(2));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(4));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(8));
					c.KeyStatus["MenuUp"].Add(string.Concat(1));
					c.KeyStatus["MenuDown"].Add(string.Concat(2));
					c.KeyStatus["MenuLeft"].Add(string.Concat(4));
					c.KeyStatus["MenuRight"].Add(string.Concat(8));
					break;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x00591538 File Offset: 0x0058F738
		public static void SetZoom_UI()
		{
			float uIScale = Main.UIScale;
			PlayerInput.SetZoom_Scaled(1f / uIScale);
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x00591557 File Offset: 0x0058F757
		public static void SetZoom_World()
		{
			PlayerInput.SetZoom_Scaled(1f);
			PlayerInput.SetZoom_MouseInWorld();
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x00591568 File Offset: 0x0058F768
		public static void SetZoom_Unscaled()
		{
			Main.lastMouseX = PlayerInput._originalLastMouseX;
			Main.lastMouseY = PlayerInput._originalLastMouseY;
			Main.mouseX = PlayerInput._originalMouseX;
			Main.mouseY = PlayerInput._originalMouseY;
			Main.screenWidth = PlayerInput._originalScreenWidth;
			Main.screenHeight = PlayerInput._originalScreenHeight;
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x005915A8 File Offset: 0x0058F7A8
		public static void SetZoom_Test()
		{
			Vector2 vector = Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f;
			Vector2 vector2 = Main.screenPosition + new Vector2((float)PlayerInput._originalMouseX, (float)PlayerInput._originalMouseY);
			Vector2 vector3 = Main.screenPosition + new Vector2((float)PlayerInput._originalLastMouseX, (float)PlayerInput._originalLastMouseY);
			Vector2 vector4 = Main.screenPosition + new Vector2(0f, 0f);
			Vector2 vector9 = Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight);
			Vector2 vector5 = vector2 - vector;
			Vector2 vector6 = vector3 - vector;
			Vector2 vector7 = vector4 - vector;
			vector9 - vector;
			float num = 1f / Main.GameViewMatrix.Zoom.X;
			float num2 = 1f;
			Vector2 vector10 = vector - Main.screenPosition + vector5 * num;
			Vector2 vector8 = vector - Main.screenPosition + vector6 * num;
			Vector2 screenPosition = vector + vector7 * num2;
			Main.mouseX = (int)vector10.X;
			Main.mouseY = (int)vector10.Y;
			Main.lastMouseX = (int)vector8.X;
			Main.lastMouseY = (int)vector8.Y;
			Main.screenPosition = screenPosition;
			Main.screenWidth = (int)((float)PlayerInput._originalScreenWidth * num2);
			Main.screenHeight = (int)((float)PlayerInput._originalScreenHeight * num2);
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x00591724 File Offset: 0x0058F924
		public static void SetZoom_MouseInWorld()
		{
			Vector2 vector = Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f;
			Vector2 vector2 = Main.screenPosition + new Vector2((float)PlayerInput._originalMouseX, (float)PlayerInput._originalMouseY);
			Vector2 vector5 = Main.screenPosition + new Vector2((float)PlayerInput._originalLastMouseX, (float)PlayerInput._originalLastMouseY);
			Vector2 vector3 = vector2 - vector;
			Vector2 vector4 = vector5 - vector;
			float num = 1f / Main.GameViewMatrix.Zoom.X;
			Vector2 vector6 = vector - Main.screenPosition + vector3 * num;
			Main.mouseX = (int)vector6.X;
			Main.mouseY = (int)vector6.Y;
			Vector2 vector7 = vector - Main.screenPosition + vector4 * num;
			Main.lastMouseX = (int)vector7.X;
			Main.lastMouseY = (int)vector7.Y;
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x00591812 File Offset: 0x0058FA12
		public static void SetDesiredZoomContext(ZoomContext context)
		{
			PlayerInput._currentWantedZoom = context;
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x0059181C File Offset: 0x0058FA1C
		public static void SetZoom_Context()
		{
			switch (PlayerInput._currentWantedZoom)
			{
			case ZoomContext.Unscaled:
				PlayerInput.SetZoom_Unscaled();
				Main.SetRecommendedZoomContext(Matrix.Identity);
				return;
			case ZoomContext.World:
				PlayerInput.SetZoom_World();
				Main.SetRecommendedZoomContext(Main.GameViewMatrix.ZoomMatrix);
				return;
			case ZoomContext.Unscaled_MouseInWorld:
				PlayerInput.SetZoom_Unscaled();
				PlayerInput.SetZoom_MouseInWorld();
				Main.SetRecommendedZoomContext(Main.GameViewMatrix.ZoomMatrix);
				return;
			case ZoomContext.UI:
				PlayerInput.SetZoom_UI();
				Main.SetRecommendedZoomContext(Main.UIScaleMatrix);
				return;
			default:
				return;
			}
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x00591894 File Offset: 0x0058FA94
		private static void SetZoom_Scaled(float scale)
		{
			Main.lastMouseX = (int)((float)PlayerInput._originalLastMouseX * scale);
			Main.lastMouseY = (int)((float)PlayerInput._originalLastMouseY * scale);
			Main.mouseX = (int)((float)PlayerInput._originalMouseX * scale);
			Main.mouseY = (int)((float)PlayerInput._originalMouseY * scale);
			Main.screenWidth = (int)((float)PlayerInput._originalScreenWidth * scale);
			Main.screenHeight = (int)((float)PlayerInput._originalScreenHeight * scale);
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x005918F8 File Offset: 0x0058FAF8
		static PlayerInput()
		{
			PlayerInput.InsertExtraMouseButtonsIntoTriggerList(PlayerInput.KnownTriggers);
		}

		/// <summary>
		/// Locks the vanilla scrollbar for the upcoming cycle when called. Autoclears in Player.
		/// Takes a string to denote that your UI has registered to lock vanilla scrolling. String does not need to be unique.
		/// </summary>
		// Token: 0x06003813 RID: 14355 RVA: 0x00591C4D File Offset: 0x0058FE4D
		public static void LockVanillaMouseScroll(string myUI)
		{
			if (!PlayerInput.MouseInModdedUI.Contains(myUI))
			{
				PlayerInput.MouseInModdedUI.Add(myUI);
			}
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x00591C68 File Offset: 0x0058FE68
		internal static void InsertExtraMouseButtonsIntoTriggerList(List<string> list)
		{
			int insertionIndex = list.FindLastIndex((string s) => s.Contains("Mouse")) + 1;
			list.InsertRange(insertionIndex, new string[]
			{
				"MouseMiddle",
				"MouseXButton1",
				"MouseXButton2"
			});
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x00591CC4 File Offset: 0x0058FEC4
		private static void OnReset(KeyConfiguration c, InputMode mode)
		{
			if (mode != InputMode.Keyboard && mode != InputMode.KeyboardUI)
			{
				return;
			}
			c.KeyStatus["MouseMiddle"].Add("Mouse3");
			c.KeyStatus["MouseXButton1"].Add("Mouse4");
			c.KeyStatus["MouseXButton2"].Add("Mouse5");
		}

		// Token: 0x0400516B RID: 20843
		public static Vector2 RawMouseScale = Vector2.One;

		// Token: 0x0400516C RID: 20844
		public static TriggersPack Triggers = new TriggersPack();

		// Token: 0x0400516D RID: 20845
		public static List<string> KnownTriggers = new List<string>
		{
			"MouseLeft",
			"MouseRight",
			"Up",
			"Down",
			"Left",
			"Right",
			"Jump",
			"Throw",
			"Inventory",
			"Grapple",
			"SmartSelect",
			"SmartCursor",
			"QuickMount",
			"QuickHeal",
			"QuickMana",
			"QuickBuff",
			"MapZoomIn",
			"MapZoomOut",
			"MapAlphaUp",
			"MapAlphaDown",
			"MapFull",
			"MapStyle",
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
			"HotbarMinus",
			"HotbarPlus",
			"DpadRadial1",
			"DpadRadial2",
			"DpadRadial3",
			"DpadRadial4",
			"RadialHotbar",
			"RadialQuickbar",
			"DpadSnap1",
			"DpadSnap2",
			"DpadSnap3",
			"DpadSnap4",
			"MenuUp",
			"MenuDown",
			"MenuLeft",
			"MenuRight",
			"LockOn",
			"ViewZoomIn",
			"ViewZoomOut",
			"Loadout1",
			"Loadout2",
			"Loadout3",
			"ToggleCameraMode",
			"ToggleCreativeMenu"
		};

		// Token: 0x0400516E RID: 20846
		private static bool _canReleaseRebindingLock = true;

		// Token: 0x0400516F RID: 20847
		private static int _memoOfLastPoint = -1;

		// Token: 0x04005170 RID: 20848
		public static int NavigatorRebindingLock;

		// Token: 0x04005171 RID: 20849
		public static string BlockedKey = "";

		// Token: 0x04005172 RID: 20850
		private static string _listeningTrigger;

		// Token: 0x04005173 RID: 20851
		private static InputMode _listeningInputMode;

		// Token: 0x04005174 RID: 20852
		public static Dictionary<string, PlayerInputProfile> Profiles = new Dictionary<string, PlayerInputProfile>();

		// Token: 0x04005175 RID: 20853
		public static Dictionary<string, PlayerInputProfile> OriginalProfiles = new Dictionary<string, PlayerInputProfile>();

		// Token: 0x04005176 RID: 20854
		private static string _selectedProfile;

		// Token: 0x04005177 RID: 20855
		private static PlayerInputProfile _currentProfile;

		// Token: 0x04005178 RID: 20856
		public static InputMode CurrentInputMode = InputMode.Keyboard;

		// Token: 0x04005179 RID: 20857
		private static Buttons[] ButtonsGamepad = (Buttons[])Enum.GetValues(typeof(Buttons));

		// Token: 0x0400517A RID: 20858
		public static bool GrappleAndInteractAreShared;

		// Token: 0x0400517B RID: 20859
		public static SmartSelectGamepadPointer smartSelectPointer = new SmartSelectGamepadPointer();

		// Token: 0x0400517C RID: 20860
		public static bool UseSteamDeckIfPossible;

		// Token: 0x0400517D RID: 20861
		private static string _invalidatorCheck = "";

		// Token: 0x0400517E RID: 20862
		private static bool _lastActivityState;

		// Token: 0x0400517F RID: 20863
		public static MouseState MouseInfo;

		// Token: 0x04005180 RID: 20864
		public static MouseState MouseInfoOld;

		// Token: 0x04005181 RID: 20865
		public static int MouseX;

		// Token: 0x04005182 RID: 20866
		public static int MouseY;

		// Token: 0x04005183 RID: 20867
		public static bool LockGamepadTileUseButton = false;

		// Token: 0x04005184 RID: 20868
		public static List<string> MouseKeys = new List<string>();

		// Token: 0x04005185 RID: 20869
		public static int PreUIX;

		// Token: 0x04005186 RID: 20870
		public static int PreUIY;

		// Token: 0x04005187 RID: 20871
		public static int PreLockOnX;

		// Token: 0x04005188 RID: 20872
		public static int PreLockOnY;

		// Token: 0x04005189 RID: 20873
		public static int ScrollWheelValue;

		// Token: 0x0400518A RID: 20874
		public static int ScrollWheelValueOld;

		// Token: 0x0400518B RID: 20875
		public static int ScrollWheelDelta;

		// Token: 0x0400518C RID: 20876
		public static int ScrollWheelDeltaForUI;

		// Token: 0x0400518D RID: 20877
		public static bool GamepadAllowScrolling;

		// Token: 0x0400518E RID: 20878
		public static int GamepadScrollValue;

		// Token: 0x0400518F RID: 20879
		public static Vector2 GamepadThumbstickLeft = Vector2.Zero;

		// Token: 0x04005190 RID: 20880
		public static Vector2 GamepadThumbstickRight = Vector2.Zero;

		// Token: 0x04005191 RID: 20881
		private static PlayerInput.FastUseItemMemory _fastUseMemory;

		// Token: 0x04005192 RID: 20882
		private static bool _InBuildingMode;

		// Token: 0x04005193 RID: 20883
		private static int _UIPointForBuildingMode = -1;

		// Token: 0x04005194 RID: 20884
		public static bool WritingText;

		// Token: 0x04005195 RID: 20885
		private static int _originalMouseX;

		// Token: 0x04005196 RID: 20886
		private static int _originalMouseY;

		// Token: 0x04005197 RID: 20887
		private static int _originalLastMouseX;

		// Token: 0x04005198 RID: 20888
		private static int _originalLastMouseY;

		// Token: 0x04005199 RID: 20889
		private static int _originalScreenWidth;

		// Token: 0x0400519A RID: 20890
		private static int _originalScreenHeight;

		// Token: 0x0400519B RID: 20891
		private static ZoomContext _currentWantedZoom;

		// Token: 0x0400519C RID: 20892
		private static List<string> _buttonsLocked = new List<string>();

		// Token: 0x0400519D RID: 20893
		public static bool PreventCursorModeSwappingToGamepad = false;

		// Token: 0x0400519E RID: 20894
		public static bool PreventFirstMousePositionGrab = false;

		// Token: 0x0400519F RID: 20895
		private static int[] DpadSnapCooldown = new int[4];

		// Token: 0x040051A0 RID: 20896
		public static bool AllowExecutionOfGamepadInstructions = true;

		// Token: 0x040051A3 RID: 20899
		internal static bool reinitialize;

		// Token: 0x040051A4 RID: 20900
		internal static List<string> MouseInModdedUI = new List<string>();

		// Token: 0x02000B8F RID: 2959
		public class MiscSettingsTEMP
		{
			// Token: 0x0400765E RID: 30302
			public static bool HotbarRadialShouldBeUsed = true;
		}

		// Token: 0x02000B90 RID: 2960
		public static class SettingsForUI
		{
			// Token: 0x17000947 RID: 2375
			// (get) Token: 0x06005D29 RID: 23849 RVA: 0x006C76A6 File Offset: 0x006C58A6
			// (set) Token: 0x06005D2A RID: 23850 RVA: 0x006C76AD File Offset: 0x006C58AD
			public static CursorMode CurrentCursorMode { get; private set; }

			// Token: 0x17000948 RID: 2376
			// (get) Token: 0x06005D2B RID: 23851 RVA: 0x006C76B5 File Offset: 0x006C58B5
			public static bool ShowGamepadHints
			{
				get
				{
					return PlayerInput.UsingGamepad || PlayerInput.SteamDeckIsUsed;
				}
			}

			// Token: 0x17000949 RID: 2377
			// (get) Token: 0x06005D2C RID: 23852 RVA: 0x006C76C5 File Offset: 0x006C58C5
			public static bool AllowSecondaryGamepadAim
			{
				get
				{
					return PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Gamepad || !PlayerInput.SteamDeckIsUsed;
				}
			}

			// Token: 0x1700094A RID: 2378
			// (get) Token: 0x06005D2D RID: 23853 RVA: 0x006C76D9 File Offset: 0x006C58D9
			public static bool PushEquipmentAreaUp
			{
				get
				{
					return PlayerInput.UsingGamepad && !PlayerInput.SteamDeckIsUsed;
				}
			}

			// Token: 0x1700094B RID: 2379
			// (get) Token: 0x06005D2E RID: 23854 RVA: 0x006C76EC File Offset: 0x006C58EC
			public static bool ShowGamepadCursor
			{
				get
				{
					if (!PlayerInput.SteamDeckIsUsed)
					{
						return PlayerInput.UsingGamepad;
					}
					return PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Gamepad;
				}
			}

			// Token: 0x1700094C RID: 2380
			// (get) Token: 0x06005D2F RID: 23855 RVA: 0x006C7703 File Offset: 0x006C5903
			public static bool HighlightThingsForMouse
			{
				get
				{
					return !PlayerInput.UsingGamepadUI || PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Mouse;
				}
			}

			// Token: 0x1700094D RID: 2381
			// (get) Token: 0x06005D30 RID: 23856 RVA: 0x006C7716 File Offset: 0x006C5916
			// (set) Token: 0x06005D31 RID: 23857 RVA: 0x006C771D File Offset: 0x006C591D
			public static int FramesSinceLastTimeInMouseMode { get; private set; }

			// Token: 0x1700094E RID: 2382
			// (get) Token: 0x06005D32 RID: 23858 RVA: 0x006C7725 File Offset: 0x006C5925
			public static bool PreventHighlightsForGamepad
			{
				get
				{
					return PlayerInput.SettingsForUI.FramesSinceLastTimeInMouseMode == 0;
				}
			}

			// Token: 0x06005D33 RID: 23859 RVA: 0x006C772F File Offset: 0x006C592F
			public static void SetCursorMode(CursorMode cursorMode)
			{
				PlayerInput.SettingsForUI.CurrentCursorMode = cursorMode;
				if (PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Mouse)
				{
					PlayerInput.SettingsForUI.FramesSinceLastTimeInMouseMode = 0;
				}
			}

			// Token: 0x06005D34 RID: 23860 RVA: 0x006C7744 File Offset: 0x006C5944
			public static void UpdateCounters()
			{
				if (PlayerInput.SettingsForUI.CurrentCursorMode != CursorMode.Mouse)
				{
					PlayerInput.SettingsForUI.FramesSinceLastTimeInMouseMode++;
				}
			}

			// Token: 0x06005D35 RID: 23861 RVA: 0x006C7759 File Offset: 0x006C5959
			public static void TryRevertingToMouseMode()
			{
				if (PlayerInput.SettingsForUI.FramesSinceLastTimeInMouseMode <= 0)
				{
					PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Mouse);
					PlayerInput.CurrentInputMode = InputMode.Mouse;
					PlayerInput.Triggers.Current.UsedMovementKey = false;
					PlayerInput.NavigatorUnCachePosition();
				}
			}
		}

		// Token: 0x02000B91 RID: 2961
		private struct FastUseItemMemory
		{
			// Token: 0x06005D36 RID: 23862 RVA: 0x006C7784 File Offset: 0x006C5984
			public bool TryStartForItemSlot(Player player, int itemSlot)
			{
				if (itemSlot < 0 || itemSlot >= 50)
				{
					this.Clear();
					return false;
				}
				this._player = player;
				this._slot = itemSlot;
				this._itemType = this._player.inventory[itemSlot].type;
				this._shouldFastUse = true;
				this._isMouseItem = false;
				ItemSlot.PickupItemIntoMouse(player.inventory, 0, itemSlot, player);
				return true;
			}

			// Token: 0x06005D37 RID: 23863 RVA: 0x006C77E5 File Offset: 0x006C59E5
			public bool TryStartForMouse(Player player)
			{
				this._player = player;
				this._slot = -1;
				this._itemType = Main.mouseItem.type;
				this._shouldFastUse = true;
				this._isMouseItem = true;
				return true;
			}

			// Token: 0x06005D38 RID: 23864 RVA: 0x006C7814 File Offset: 0x006C5A14
			public void Clear()
			{
				this._shouldFastUse = false;
			}

			// Token: 0x06005D39 RID: 23865 RVA: 0x006C7820 File Offset: 0x006C5A20
			public bool CanFastUse()
			{
				if (!this._shouldFastUse)
				{
					return false;
				}
				if (this._isMouseItem)
				{
					return Main.mouseItem.type == this._itemType;
				}
				return this._player.inventory[this._slot].type == this._itemType;
			}

			// Token: 0x06005D3A RID: 23866 RVA: 0x006C7874 File Offset: 0x006C5A74
			public void EndFastUse()
			{
				if (this._shouldFastUse)
				{
					if (!this._isMouseItem && this._player.inventory[this._slot].IsAir)
					{
						Utils.Swap<Item>(ref Main.mouseItem, ref this._player.inventory[this._slot]);
					}
					this.Clear();
				}
			}

			// Token: 0x04007661 RID: 30305
			private int _slot;

			// Token: 0x04007662 RID: 30306
			private int _itemType;

			// Token: 0x04007663 RID: 30307
			private bool _shouldFastUse;

			// Token: 0x04007664 RID: 30308
			private bool _isMouseItem;

			// Token: 0x04007665 RID: 30309
			private Player _player;
		}

		// Token: 0x02000B92 RID: 2962
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007666 RID: 30310
			public static Preferences.TextProcessAction <0>__PrettyPrintProfiles;

			// Token: 0x04007667 RID: 30311
			public static Action<Player> <1>__Hook_OnEnterWorld;
		}
	}
}
