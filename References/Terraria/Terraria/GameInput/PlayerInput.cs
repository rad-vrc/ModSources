using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Chat;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameInput
{
	// Token: 0x0200013E RID: 318
	public class PlayerInput
	{
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06001865 RID: 6245 RVA: 0x004D8544 File Offset: 0x004D6744
		// (remove) Token: 0x06001866 RID: 6246 RVA: 0x004D8578 File Offset: 0x004D6778
		public static event Action OnBindingChange;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06001867 RID: 6247 RVA: 0x004D85AC File Offset: 0x004D67AC
		// (remove) Token: 0x06001868 RID: 6248 RVA: 0x004D85E0 File Offset: 0x004D67E0
		public static event Action OnActionableInput;

		// Token: 0x06001869 RID: 6249 RVA: 0x004D8613 File Offset: 0x004D6813
		public static void ListenFor(string triggerName, InputMode inputmode)
		{
			PlayerInput._listeningTrigger = triggerName;
			PlayerInput._listeningInputMode = inputmode;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x004D8621 File Offset: 0x004D6821
		public static string ListeningTrigger
		{
			get
			{
				return PlayerInput._listeningTrigger;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x004D8628 File Offset: 0x004D6828
		public static bool CurrentlyRebinding
		{
			get
			{
				return PlayerInput._listeningTrigger != null;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x004D8634 File Offset: 0x004D6834
		public static bool InvisibleGamepadInMenus
		{
			get
			{
				return ((Main.gameMenu || Main.ingameOptionsWindow || Main.playerInventory || Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1 || Main.InGameUI.CurrentState != null) && !PlayerInput._InBuildingMode && Main.InvisibleCursorForGamepad) || (PlayerInput.CursorIsBusy && !PlayerInput._InBuildingMode);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x004D86B2 File Offset: 0x004D68B2
		public static PlayerInputProfile CurrentProfile
		{
			get
			{
				return PlayerInput._currentProfile;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x004D86B9 File Offset: 0x004D68B9
		public static KeyConfiguration ProfileGamepadUI
		{
			get
			{
				return PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI];
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x004D86CB File Offset: 0x004D68CB
		public static bool UsingGamepad
		{
			get
			{
				return PlayerInput.CurrentInputMode == InputMode.XBoxGamepad || PlayerInput.CurrentInputMode == InputMode.XBoxGamepadUI;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x004D86DF File Offset: 0x004D68DF
		public static bool UsingGamepadUI
		{
			get
			{
				return PlayerInput.CurrentInputMode == InputMode.XBoxGamepadUI;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x004D86EC File Offset: 0x004D68EC
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

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x004D873D File Offset: 0x004D693D
		public static bool SteamDeckIsUsed
		{
			get
			{
				return PlayerInput.UseSteamDeckIfPossible;
			}
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x004D8744 File Offset: 0x004D6944
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

		// Token: 0x06001874 RID: 6260 RVA: 0x004D87B0 File Offset: 0x004D69B0
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

		// Token: 0x06001875 RID: 6261 RVA: 0x004D8864 File Offset: 0x004D6A64
		public static List<Keys> GetPressedKeys()
		{
			List<Keys> list = Main.keyState.GetPressedKeys().ToList<Keys>();
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i] == Keys.None || list[i] == Keys.Kanji)
				{
					list.RemoveAt(i);
				}
			}
			return list;
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x004D88B0 File Offset: 0x004D6AB0
		public static void TryEnteringFastUseModeForInventorySlot(int inventorySlot)
		{
			PlayerInput._fastUseMemory.TryStartForItemSlot(Main.LocalPlayer, inventorySlot);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x004D88C3 File Offset: 0x004D6AC3
		public static void TryEnteringFastUseModeForMouseItem()
		{
			PlayerInput._fastUseMemory.TryStartForMouse(Main.LocalPlayer);
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x004D88D5 File Offset: 0x004D6AD5
		public static bool ShouldFastUseItem
		{
			get
			{
				return PlayerInput._fastUseMemory.CanFastUse();
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x004D88E1 File Offset: 0x004D6AE1
		public static void TryEndingFastUse()
		{
			PlayerInput._fastUseMemory.EndFastUse();
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x004D88ED File Offset: 0x004D6AED
		public static bool InBuildingMode
		{
			get
			{
				return PlayerInput._InBuildingMode;
			}
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x004D88F4 File Offset: 0x004D6AF4
		public static void EnterBuildingMode()
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			PlayerInput._InBuildingMode = true;
			PlayerInput._UIPointForBuildingMode = UILinkPointNavigator.CurrentPoint;
			if (Main.mouseItem.stack <= 0)
			{
				int uipointForBuildingMode = PlayerInput._UIPointForBuildingMode;
				if (uipointForBuildingMode < 50 && uipointForBuildingMode >= 0 && Main.player[Main.myPlayer].inventory[uipointForBuildingMode].stack > 0)
				{
					Utils.Swap<Item>(ref Main.mouseItem, ref Main.player[Main.myPlayer].inventory[uipointForBuildingMode]);
				}
			}
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x004D897C File Offset: 0x004D6B7C
		public static void ExitBuildingMode()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			PlayerInput._InBuildingMode = false;
			UILinkPointNavigator.ChangePoint(PlayerInput._UIPointForBuildingMode);
			if (Main.mouseItem.stack > 0 && Main.player[Main.myPlayer].itemAnimation == 0)
			{
				int uipointForBuildingMode = PlayerInput._UIPointForBuildingMode;
				if (uipointForBuildingMode < 50 && uipointForBuildingMode >= 0 && Main.player[Main.myPlayer].inventory[uipointForBuildingMode].stack <= 0)
				{
					Utils.Swap<Item>(ref Main.mouseItem, ref Main.player[Main.myPlayer].inventory[uipointForBuildingMode]);
				}
			}
			PlayerInput._UIPointForBuildingMode = -1;
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x004D8A1C File Offset: 0x004D6C1C
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

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x004D8A5D File Offset: 0x004D6C5D
		public static int RealScreenWidth
		{
			get
			{
				return PlayerInput._originalScreenWidth;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x004D8A64 File Offset: 0x004D6C64
		public static int RealScreenHeight
		{
			get
			{
				return PlayerInput._originalScreenHeight;
			}
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x004D8A6B File Offset: 0x004D6C6B
		public static void SetSelectedProfile(string name)
		{
			if (PlayerInput.Profiles.ContainsKey(name))
			{
				PlayerInput._selectedProfile = name;
				PlayerInput._currentProfile = PlayerInput.Profiles[PlayerInput._selectedProfile];
			}
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x004D8A94 File Offset: 0x004D6C94
		public static void Initialize()
		{
			Main.InputProfiles.OnProcessText += PlayerInput.PrettyPrintProfiles;
			Player.Hooks.OnEnterWorld += PlayerInput.Hook_OnEnterWorld;
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

		// Token: 0x06001882 RID: 6274 RVA: 0x004D8C17 File Offset: 0x004D6E17
		public static void Hook_OnEnterWorld(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				Main.SmartCursorWanted_GamePad = true;
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x004D8C2C File Offset: 0x004D6E2C
		public static bool Save()
		{
			Main.InputProfiles.Clear();
			Main.InputProfiles.Put("Selected Profile", PlayerInput._selectedProfile);
			foreach (KeyValuePair<string, PlayerInputProfile> keyValuePair in PlayerInput.Profiles)
			{
				Main.InputProfiles.Put(keyValuePair.Value.Name, keyValuePair.Value.Save());
			}
			return Main.InputProfiles.Save(true);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x004D8CC4 File Offset: 0x004D6EC4
		public static void Load()
		{
			Main.InputProfiles.Load();
			Dictionary<string, PlayerInputProfile> dictionary = new Dictionary<string, PlayerInputProfile>();
			string text = null;
			Main.InputProfiles.Get<string>("Selected Profile", ref text);
			List<string> allKeys = Main.InputProfiles.GetAllKeys();
			for (int i = 0; i < allKeys.Count; i++)
			{
				string text2 = allKeys[i];
				if (!(text2 == "Selected Profile") && !string.IsNullOrEmpty(text2))
				{
					Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
					Main.InputProfiles.Get<Dictionary<string, object>>(text2, ref dictionary2);
					if (dictionary2.Count > 0)
					{
						PlayerInputProfile playerInputProfile = new PlayerInputProfile(text2);
						playerInputProfile.Initialize(PresetProfiles.None);
						if (playerInputProfile.Load(dictionary2))
						{
							dictionary.Add(text2, playerInputProfile);
						}
					}
				}
			}
			if (dictionary.Count > 0)
			{
				PlayerInput.Profiles = dictionary;
				if (!string.IsNullOrEmpty(text) && PlayerInput.Profiles.ContainsKey(text))
				{
					PlayerInput.SetSelectedProfile(text);
					return;
				}
				PlayerInput.SetSelectedProfile(PlayerInput.Profiles.Keys.First<string>());
			}
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x004D8DB8 File Offset: 0x004D6FB8
		public static void ManageVersion_1_3()
		{
			PlayerInputProfile playerInputProfile = PlayerInput.Profiles["Custom"];
			string[,] array = new string[20, 2];
			array[0, 0] = "KeyUp";
			array[0, 1] = "Up";
			array[1, 0] = "KeyDown";
			array[1, 1] = "Down";
			array[2, 0] = "KeyLeft";
			array[2, 1] = "Left";
			array[3, 0] = "KeyRight";
			array[3, 1] = "Right";
			array[4, 0] = "KeyJump";
			array[4, 1] = "Jump";
			array[5, 0] = "KeyThrowItem";
			array[5, 1] = "Throw";
			array[6, 0] = "KeyInventory";
			array[6, 1] = "Inventory";
			array[7, 0] = "KeyQuickHeal";
			array[7, 1] = "QuickHeal";
			array[8, 0] = "KeyQuickMana";
			array[8, 1] = "QuickMana";
			array[9, 0] = "KeyQuickBuff";
			array[9, 1] = "QuickBuff";
			array[10, 0] = "KeyUseHook";
			array[10, 1] = "Grapple";
			array[11, 0] = "KeyAutoSelect";
			array[11, 1] = "SmartSelect";
			array[12, 0] = "KeySmartCursor";
			array[12, 1] = "SmartCursor";
			array[13, 0] = "KeyMount";
			array[13, 1] = "QuickMount";
			array[14, 0] = "KeyMapStyle";
			array[14, 1] = "MapStyle";
			array[15, 0] = "KeyFullscreenMap";
			array[15, 1] = "MapFull";
			array[16, 0] = "KeyMapZoomIn";
			array[16, 1] = "MapZoomIn";
			array[17, 0] = "KeyMapZoomOut";
			array[17, 1] = "MapZoomOut";
			array[18, 0] = "KeyMapAlphaUp";
			array[18, 1] = "MapAlphaUp";
			array[19, 0] = "KeyMapAlphaDown";
			array[19, 1] = "MapAlphaDown";
			string[,] array2 = array;
			for (int i = 0; i < array2.GetLength(0); i++)
			{
				string text = null;
				Main.Configuration.Get<string>(array2[i, 0], ref text);
				if (text != null)
				{
					playerInputProfile.InputModes[InputMode.Keyboard].KeyStatus[array2[i, 1]] = new List<string>
					{
						text
					};
					playerInputProfile.InputModes[InputMode.KeyboardUI].KeyStatus[array2[i, 1]] = new List<string>
					{
						text
					};
				}
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x004D907B File Offset: 0x004D727B
		public static bool CursorIsBusy
		{
			get
			{
				return ItemSlot.CircularRadialOpacity > 0f || ItemSlot.QuicksRadialOpacity > 0f;
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x004D9098 File Offset: 0x004D7298
		public static void LockGamepadButtons(string TriggerName)
		{
			List<string> collection = null;
			KeyConfiguration keyConfiguration = null;
			if (PlayerInput.CurrentProfile.InputModes.TryGetValue(PlayerInput.CurrentInputMode, out keyConfiguration) && keyConfiguration.KeyStatus.TryGetValue(TriggerName, out collection))
			{
				PlayerInput._buttonsLocked.AddRange(collection);
			}
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x004D90DC File Offset: 0x004D72DC
		public static bool IsGamepadButtonLockedFromUse(string keyName)
		{
			return PlayerInput._buttonsLocked.Contains(keyName);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x004D90EC File Offset: 0x004D72EC
		public static void UpdateInput()
		{
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
			bool flag = false;
			PlayerInput.MouseInput();
			bool flag2 = flag | PlayerInput.KeyboardInput() | PlayerInput.GamePadInput();
			PlayerInput.Triggers.Update();
			PlayerInput.PostInput();
			PlayerInput.ScrollWheelDelta = PlayerInput.ScrollWheelValue - PlayerInput.ScrollWheelValueOld;
			PlayerInput.ScrollWheelDeltaForUI = PlayerInput.ScrollWheelDelta;
			PlayerInput.WritingText = false;
			PlayerInput.UpdateMainMouse();
			Main.mouseLeft = PlayerInput.Triggers.Current.MouseLeft;
			Main.mouseRight = PlayerInput.Triggers.Current.MouseRight;
			PlayerInput.CacheZoomableValues();
			if (flag2 && PlayerInput.OnActionableInput != null)
			{
				PlayerInput.OnActionableInput();
			}
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x004D9245 File Offset: 0x004D7445
		public static void UpdateMainMouse()
		{
			Main.lastMouseX = Main.mouseX;
			Main.lastMouseY = Main.mouseY;
			Main.mouseX = PlayerInput.MouseX;
			Main.mouseY = PlayerInput.MouseY;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x004D926F File Offset: 0x004D746F
		public static void CacheZoomableValues()
		{
			PlayerInput.CacheOriginalInput();
			PlayerInput.CacheOriginalScreenDimensions();
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x004D927C File Offset: 0x004D747C
		public static void CacheMousePositionForZoom()
		{
			float num = 1f;
			PlayerInput._originalMouseX = (int)((float)Main.mouseX * num);
			PlayerInput._originalMouseY = (int)((float)Main.mouseY * num);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x004D92AB File Offset: 0x004D74AB
		private static void CacheOriginalInput()
		{
			PlayerInput._originalMouseX = Main.mouseX;
			PlayerInput._originalMouseY = Main.mouseY;
			PlayerInput._originalLastMouseX = Main.lastMouseX;
			PlayerInput._originalLastMouseY = Main.lastMouseY;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x004D92D5 File Offset: 0x004D74D5
		public static void CacheOriginalScreenDimensions()
		{
			PlayerInput._originalScreenWidth = Main.screenWidth;
			PlayerInput._originalScreenHeight = Main.screenHeight;
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x004D92EB File Offset: 0x004D74EB
		public static Vector2 OriginalScreenSize
		{
			get
			{
				return new Vector2((float)PlayerInput._originalScreenWidth, (float)PlayerInput._originalScreenHeight);
			}
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x004D9300 File Offset: 0x004D7500
		private static bool GamePadInput()
		{
			bool flag = false;
			PlayerInput.ScrollWheelValue += PlayerInput.GamepadScrollValue;
			GamePadState gamePadState = default(GamePadState);
			bool flag2 = false;
			for (int i = 0; i < 4; i++)
			{
				GamePadState state = GamePad.GetState((PlayerIndex)i);
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
				if ((num & (int)PlayerInput.ButtonsGamepad[j]) <= 0)
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
			if (Vector2.Dot(-Vector2.UnitX, vector2) >= num2 && gamepadThumbstickLeft.X < -PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.LeftThumbstickLeft.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.LeftThumbstickLeft.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitX, vector2) >= num2 && gamepadThumbstickLeft.X > PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.LeftThumbstickRight.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.LeftThumbstickRight.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(-Vector2.UnitY, vector2) >= num2 && gamepadThumbstickLeft.Y < -PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.LeftThumbstickUp.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.LeftThumbstickUp.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitY, vector2) >= num2 && gamepadThumbstickLeft.Y > PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.LeftThumbstickDown.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.LeftThumbstickDown.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(-Vector2.UnitX, vector) >= num2 && gamepadThumbstickRight.X < -PlayerInput.CurrentProfile.RightThumbstickDeadzoneX)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.RightThumbstickLeft.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.RightThumbstickLeft.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitX, vector) >= num2 && gamepadThumbstickRight.X > PlayerInput.CurrentProfile.RightThumbstickDeadzoneX)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.RightThumbstickRight.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.RightThumbstickRight.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(-Vector2.UnitY, vector) >= num2 && gamepadThumbstickRight.Y < -PlayerInput.CurrentProfile.RightThumbstickDeadzoneY)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.RightThumbstickUp.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.RightThumbstickUp.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitY, vector) >= num2 && gamepadThumbstickRight.Y > PlayerInput.CurrentProfile.RightThumbstickDeadzoneY)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.RightThumbstickDown.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.RightThumbstickDown.ToString(), inputMode);
				flag = true;
			}
			if (gamePadState.Triggers.Left > triggersDeadzone)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.LeftTrigger.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.LeftTrigger.ToString(), inputMode);
				flag = true;
			}
			if (gamePadState.Triggers.Right > triggersDeadzone)
			{
				string newKey = Buttons.RightTrigger.ToString();
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
				float m = Main.GameViewMatrix.ZoomMatrix.M11;
				PlayerInput.smartSelectPointer.UpdateSize(new Vector2((float)(Player.tileRangeX * 16 + num3 * 16), (float)(Player.tileRangeY * 16 + num3 * 16)) * m);
				if (flag5)
				{
					PlayerInput.smartSelectPointer.UpdateSize(new Vector2((float)(Math.Max(Main.screenWidth, Main.screenHeight) / 2)));
				}
				PlayerInput.smartSelectPointer.UpdateCenter(new Vector2((float)num4, (float)num5));
				if (gamepadThumbstickRight != Vector2.Zero && flag8)
				{
					Vector2 vector3 = new Vector2(8f);
					if (!Main.gameMenu && Main.mapFullscreen)
					{
						vector3 = new Vector2(16f);
					}
					if (flag7)
					{
						vector3 = new Vector2((float)(Player.tileRangeX * 16), (float)(Player.tileRangeY * 16));
						if (num3 != 0)
						{
							vector3 += new Vector2((float)(num3 * 16), (float)(num3 * 16));
						}
						if (flag5)
						{
							vector3 = new Vector2((float)(Math.Max(Main.screenWidth, Main.screenHeight) / 2));
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
					float scaleFactor = 8f;
					if (!Main.gameMenu && Main.mapFullscreen)
					{
						scaleFactor = 3f;
					}
					if (Main.mapFullscreen)
					{
						Vector2 value = gamepadThumbstickLeft * scaleFactor;
						Main.mapFullscreenPos += value * scaleFactor * (1f / Main.mapFullscreenScale);
					}
					else if (!flag6 && Main.SmartCursorWanted && allowSecondaryGamepadAim)
					{
						float m3 = Main.GameViewMatrix.ZoomMatrix.M11;
						Vector2 vector5 = gamepadThumbstickLeft * new Vector2((float)(Player.tileRangeX * 16), (float)(Player.tileRangeY * 16)) * m3;
						if (num3 != 0)
						{
							vector5 = gamepadThumbstickLeft * new Vector2((float)((Player.tileRangeX + num3) * 16), (float)((Player.tileRangeY + num3) * 16)) * m3;
						}
						if (flag5)
						{
							vector5 = new Vector2((float)(Math.Max(Main.screenWidth, Main.screenHeight) / 2)) * gamepadThumbstickLeft;
						}
						int num8 = (int)vector5.X;
						int num9 = (int)vector5.Y;
						PlayerInput.MouseX = num8 + num4;
						PlayerInput.MouseY = num9 + num5;
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
								float num16 = -((float)(Player.tileRangeY + num3) - num15) * 16f * m4;
								float max = ((float)(Player.tileRangeY + num3) - num15) * 16f * m4;
								num16 -= (float)(player.height / 16 / 2 * 16);
								num10 = (int)Utils.Clamp<float>((float)num10, -((float)(Player.tileRangeX + num3) - num15) * 16f * m4, ((float)(Player.tileRangeX + num3) - num15) * 16f * m4);
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

		// Token: 0x06001891 RID: 6289 RVA: 0x004DA110 File Offset: 0x004D8310
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
				if (PlayerInput.MouseInfo.LeftButton == ButtonState.Pressed)
				{
					PlayerInput.MouseKeys.Add("Mouse1");
					flag = true;
				}
				if (PlayerInput.MouseInfo.RightButton == ButtonState.Pressed)
				{
					PlayerInput.MouseKeys.Add("Mouse2");
					flag = true;
				}
				if (PlayerInput.MouseInfo.MiddleButton == ButtonState.Pressed)
				{
					PlayerInput.MouseKeys.Add("Mouse3");
					flag = true;
				}
				if (PlayerInput.MouseInfo.XButton1 == ButtonState.Pressed)
				{
					PlayerInput.MouseKeys.Add("Mouse4");
					flag = true;
				}
				if (PlayerInput.MouseInfo.XButton2 == ButtonState.Pressed)
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

		// Token: 0x06001892 RID: 6290 RVA: 0x004DA2A0 File Offset: 0x004D84A0
		private static bool KeyboardInput()
		{
			bool flag = false;
			bool flag2 = false;
			List<Keys> pressedKeys = PlayerInput.GetPressedKeys();
			PlayerInput.DebugKeys(pressedKeys);
			if (pressedKeys.Count == 0 && PlayerInput.MouseKeys.Count == 0)
			{
				Main.blockKey = Keys.None.ToString();
				return false;
			}
			for (int i = 0; i < pressedKeys.Count; i++)
			{
				if (pressedKeys[i] == Keys.LeftShift || pressedKeys[i] == Keys.RightShift)
				{
					flag = true;
				}
				else if (pressedKeys[i] == Keys.LeftAlt || pressedKeys[i] == Keys.RightAlt)
				{
					flag2 = true;
				}
				Main.ChromaPainter.PressKey(pressedKeys[i]);
			}
			if (Main.blockKey != Keys.None.ToString())
			{
				bool flag3 = false;
				for (int j = 0; j < pressedKeys.Count; j++)
				{
					if (pressedKeys[j].ToString() == Main.blockKey)
					{
						pressedKeys[j] = Keys.None;
						flag3 = true;
					}
				}
				if (!flag3)
				{
					Main.blockKey = Keys.None.ToString();
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
				if (l >= count || pressedKeys[l] != Keys.None)
				{
					string newKey = list[l];
					if (!(list[l] == Keys.Tab.ToString()) || ((!flag || SocialAPI.Mode != SocialMode.Steam) && !flag2))
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
						if (l >= count || pressedKeys[l] != Keys.None)
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

		// Token: 0x06001893 RID: 6291 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private static void DebugKeys(List<Keys> keys)
		{
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x004DA54C File Offset: 0x004D874C
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
				InputMode inputMode = (InputMode)obj;
				if (inputMode != InputMode.Mouse)
				{
					PlayerInput.FixKeysConflict(inputMode, list);
					foreach (string text in list)
					{
						if (PlayerInput.CurrentProfile.InputModes[inputMode].KeyStatus[text].Count < 1)
						{
							PlayerInput.ResetKeyBinding(inputMode, text);
						}
					}
				}
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x004DA640 File Offset: 0x004D8840
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

		// Token: 0x06001896 RID: 6294 RVA: 0x004DA71C File Offset: 0x004D891C
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

		// Token: 0x06001897 RID: 6295 RVA: 0x004DA7A8 File Offset: 0x004D89A8
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

		// Token: 0x06001898 RID: 6296 RVA: 0x004DA950 File Offset: 0x004D8B50
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

		// Token: 0x06001899 RID: 6297 RVA: 0x004DAB24 File Offset: 0x004D8D24
		private static void PostInput()
		{
			Main.GamepadCursorAlpha = MathHelper.Clamp(Main.GamepadCursorAlpha + ((Main.SmartCursorIsUsed && !UILinkPointNavigator.Available && PlayerInput.GamepadThumbstickLeft == Vector2.Zero && PlayerInput.GamepadThumbstickRight == Vector2.Zero) ? -0.05f : 0.05f), 0f, 1f);
			if (PlayerInput.CurrentProfile.HotbarAllowsRadial)
			{
				int num = PlayerInput.Triggers.Current.HotbarPlus.ToInt() - PlayerInput.Triggers.Current.HotbarMinus.ToInt();
				if (PlayerInput.MiscSettingsTEMP.HotbarRadialShouldBeUsed)
				{
					if (num == 1)
					{
						PlayerInput.Triggers.Current.RadialHotbar = true;
						PlayerInput.Triggers.JustReleased.RadialHotbar = false;
					}
					else if (num == -1)
					{
						PlayerInput.Triggers.Current.RadialQuickbar = true;
						PlayerInput.Triggers.JustReleased.RadialQuickbar = false;
					}
				}
			}
			PlayerInput.MiscSettingsTEMP.HotbarRadialShouldBeUsed = false;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x004DAC14 File Offset: 0x004D8E14
		private static void HandleDpadSnap()
		{
			Vector2 value = Vector2.Zero;
			Player player = Main.player[Main.myPlayer];
			for (int i = 0; i < 4; i++)
			{
				bool flag = false;
				Vector2 value2 = Vector2.Zero;
				if (Main.gameMenu || (UILinkPointNavigator.Available && !PlayerInput.InBuildingMode))
				{
					return;
				}
				switch (i)
				{
				case 0:
					flag = PlayerInput.Triggers.Current.DpadMouseSnap1;
					value2 = -Vector2.UnitY;
					break;
				case 1:
					flag = PlayerInput.Triggers.Current.DpadMouseSnap2;
					value2 = Vector2.UnitX;
					break;
				case 2:
					flag = PlayerInput.Triggers.Current.DpadMouseSnap3;
					value2 = Vector2.UnitY;
					break;
				case 3:
					flag = PlayerInput.Triggers.Current.DpadMouseSnap4;
					value2 = -Vector2.UnitX;
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
							num = player.inventory[player.selectedItem].useTime;
						}
						PlayerInput.DpadSnapCooldown[i] = num;
						value += value2;
					}
				}
				else
				{
					PlayerInput.DpadSnapCooldown[i] = 0;
				}
			}
			if (value != Vector2.Zero)
			{
				Main.SmartCursorWanted_GamePad = false;
				Matrix zoomMatrix = Main.GameViewMatrix.ZoomMatrix;
				Matrix matrix = Matrix.Invert(zoomMatrix);
				Vector2 mouseScreen = Main.MouseScreen;
				Vector2.Transform(Main.screenPosition, matrix);
				Vector2 vector = Vector2.Transform((Vector2.Transform(mouseScreen, matrix) + value * new Vector2(16f) + Main.screenPosition).ToTileCoordinates().ToWorldCoordinates(8f, 8f) - Main.screenPosition, zoomMatrix);
				PlayerInput.MouseX = (int)vector.X;
				PlayerInput.MouseY = (int)vector.Y;
				PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Gamepad);
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x004DADFA File Offset: 0x004D8FFA
		private static bool ShouldShowInstructionsForGamepad()
		{
			return PlayerInput.UsingGamepad || PlayerInput.SteamDeckIsUsed;
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x004DAE0C File Offset: 0x004D900C
		public static string ComposeInstructionsForGamepad()
		{
			string text = string.Empty;
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
				text += "          ";
				text += PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
				});
				text += PlayerInput.BuildCommand(Lang.inter[118].Value, false, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
				text += PlayerInput.BuildCommand(Lang.inter[119].Value, false, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"]
				});
				if (Main.netMode == 1 && Main.player[Main.myPlayer].HasItem(2997))
				{
					text += PlayerInput.BuildCommand(Lang.inter[120].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
					});
				}
			}
			else if (inputMode == InputMode.XBoxGamepadUI && !PlayerInput.InBuildingMode)
			{
				text = UILinkPointNavigator.GetInstructions();
			}
			else
			{
				text += PlayerInput.BuildCommand(Lang.misc[58].Value, false, new List<string>[]
				{
					keyConfiguration.KeyStatus["Jump"]
				});
				text += PlayerInput.BuildCommand(Lang.misc[59].Value, false, new List<string>[]
				{
					keyConfiguration.KeyStatus["HotbarMinus"],
					keyConfiguration.KeyStatus["HotbarPlus"]
				});
				if (PlayerInput.InBuildingMode)
				{
					text += PlayerInput.BuildCommand(Lang.menu[6].Value, false, new List<string>[]
					{
						keyConfiguration.KeyStatus["Inventory"],
						keyConfiguration.KeyStatus["MouseRight"]
					});
				}
				if (WiresUI.Open)
				{
					text += PlayerInput.BuildCommand(Lang.misc[53].Value, false, new List<string>[]
					{
						keyConfiguration.KeyStatus["MouseLeft"]
					});
					text += PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
					{
						keyConfiguration.KeyStatus["MouseRight"]
					});
				}
				else
				{
					Item item = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem];
					if (item.damage > 0 && item.ammo == 0)
					{
						text += PlayerInput.BuildCommand(Lang.misc[60].Value, false, new List<string>[]
						{
							keyConfiguration.KeyStatus["MouseLeft"]
						});
					}
					else if (item.createTile >= 0 || item.createWall > 0)
					{
						text += PlayerInput.BuildCommand(Lang.misc[61].Value, false, new List<string>[]
						{
							keyConfiguration.KeyStatus["MouseLeft"]
						});
					}
					else
					{
						text += PlayerInput.BuildCommand(Lang.misc[63].Value, false, new List<string>[]
						{
							keyConfiguration.KeyStatus["MouseLeft"]
						});
					}
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
							text += PlayerInput.BuildCommand(Lang.misc[80].Value, false, new List<string>[]
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
							text += PlayerInput.BuildCommand(Lang.misc[79].Value, false, new List<string>[]
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
							if (TileID.Sets.TileInteractRead[(int)tile.type])
							{
								text += PlayerInput.BuildCommand(Lang.misc[81].Value, false, new List<string>[]
								{
									keyConfiguration.KeyStatus["MouseRight"]
								});
							}
							else
							{
								text += PlayerInput.BuildCommand(Lang.misc[79].Value, false, new List<string>[]
								{
									keyConfiguration.KeyStatus["MouseRight"]
								});
							}
						}
					}
					else if (WiresUI.Settings.DrawToolModeUI)
					{
						text += PlayerInput.BuildCommand(Lang.misc[89].Value, false, new List<string>[]
						{
							keyConfiguration.KeyStatus["MouseRight"]
						});
					}
					if ((!PlayerInput.GrappleAndInteractAreShared || (!WiresUI.Settings.DrawToolModeUI && (!Main.SmartInteractShowingGenuine || !Main.HasSmartInteractTarget) && (!Main.SmartInteractShowingFake || flag))) && Main.LocalPlayer.QuickGrapple_GetItemToUse() != null)
					{
						text += PlayerInput.BuildCommand(Lang.misc[57].Value, false, new List<string>[]
						{
							keyConfiguration.KeyStatus["Grapple"]
						});
					}
				}
			}
			return text;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x004DB3BC File Offset: 0x004D95BC
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

		// Token: 0x0600189E RID: 6302 RVA: 0x004DB438 File Offset: 0x004D9638
		public static string GenerateInputTag_ForCurrentGamemode_WithHacks(bool tagForGameplay, string triggerName)
		{
			InputMode inputMode = PlayerInput.CurrentInputMode;
			if (inputMode == InputMode.Mouse || inputMode == InputMode.KeyboardUI)
			{
				inputMode = InputMode.Keyboard;
			}
			if (!(triggerName == "SmartSelect"))
			{
				if (triggerName == "SmartCursor")
				{
					if (inputMode == InputMode.Keyboard)
					{
						return PlayerInput.GenerateRawInputList(new List<string>
						{
							Keys.LeftAlt.ToString()
						});
					}
				}
			}
			else if (inputMode == InputMode.Keyboard)
			{
				return PlayerInput.GenerateRawInputList(new List<string>
				{
					Keys.LeftControl.ToString()
				});
			}
			return PlayerInput.GenerateInputTag_ForCurrentGamemode(tagForGameplay, triggerName);
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x004DB4C8 File Offset: 0x004D96C8
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

		// Token: 0x060018A0 RID: 6304 RVA: 0x004DB577 File Offset: 0x004D9777
		public static string GenerateInputTags_GamepadUI(string triggerName)
		{
			return PlayerInput.GenerateGlyphList(PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[triggerName]);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x004DB599 File Offset: 0x004D9799
		public static string GenerateInputTags_Gamepad(string triggerName)
		{
			return PlayerInput.GenerateGlyphList(PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[triggerName]);
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x004DB5BC File Offset: 0x004D97BC
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

		// Token: 0x060018A3 RID: 6307 RVA: 0x004DB610 File Offset: 0x004D9810
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

		// Token: 0x060018A4 RID: 6308 RVA: 0x004DB658 File Offset: 0x004D9858
		public static void NavigatorCachePosition()
		{
			PlayerInput.PreUIX = PlayerInput.MouseX;
			PlayerInput.PreUIY = PlayerInput.MouseY;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x004DB66E File Offset: 0x004D986E
		public static void NavigatorUnCachePosition()
		{
			PlayerInput.MouseX = PlayerInput.PreUIX;
			PlayerInput.MouseY = PlayerInput.PreUIY;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x004DB684 File Offset: 0x004D9884
		public static void LockOnCachePosition()
		{
			PlayerInput.PreLockOnX = PlayerInput.MouseX;
			PlayerInput.PreLockOnY = PlayerInput.MouseY;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x004DB69A File Offset: 0x004D989A
		public static void LockOnUnCachePosition()
		{
			PlayerInput.MouseX = PlayerInput.PreLockOnX;
			PlayerInput.MouseY = PlayerInput.PreLockOnY;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x004DB6B0 File Offset: 0x004D98B0
		public static void PrettyPrintProfiles(ref string text)
		{
			foreach (string text2 in text.Split(new string[]
			{
				"\r\n"
			}, StringSplitOptions.None))
			{
				if (text2.Contains(": {"))
				{
					string str = text2.Substring(0, text2.IndexOf('"'));
					string text3 = text2 + "\r\n  ";
					string newValue = text3.Replace(": {\r\n  ", ": \r\n" + str + "{\r\n  ");
					text = text.Replace(text3, newValue);
				}
			}
			text = text.Replace("[\r\n        ", "[");
			text = text.Replace("[\r\n      ", "[");
			text = text.Replace("\"\r\n      ", "\"");
			text = text.Replace("\",\r\n        ", "\", ");
			text = text.Replace("\",\r\n      ", "\", ");
			text = text.Replace("\r\n    ]", "]");
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x004DB7B0 File Offset: 0x004D99B0
		public static void PrettyPrintProfilesOld(ref string text)
		{
			text = text.Replace(": {\r\n  ", ": \r\n  {\r\n  ");
			text = text.Replace("[\r\n      ", "[");
			text = text.Replace("\"\r\n      ", "\"");
			text = text.Replace("\",\r\n      ", "\", ");
			text = text.Replace("\r\n    ]", "]");
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x004DB81C File Offset: 0x004D9A1C
		public static void Reset(KeyConfiguration c, PresetProfiles style, InputMode mode)
		{
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
					c.KeyStatus["Inventory"].Add(Keys.Escape.ToString());
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					return;
				case InputMode.Mouse:
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Jump"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.B));
					c.KeyStatus["LockOn"].Add(string.Concat(Buttons.X));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.A));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.LeftStick));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MapStyle"].Add(string.Concat(Buttons.Back));
					return;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.A));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.X));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.Back));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
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
					c.KeyStatus["Inventory"].Add(Keys.Escape.ToString());
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					return;
				case InputMode.Mouse:
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Jump"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.LeftStick));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.X));
					c.KeyStatus["QuickHeal"].Add(string.Concat(Buttons.A));
					c.KeyStatus["RadialHotbar"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MapStyle"].Add(string.Concat(Buttons.Back));
					return;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.A));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["LockOn"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.X));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.Back));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
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
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["Inventory"].Add(Keys.Escape.ToString());
					return;
				case InputMode.Mouse:
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Jump"].Add(string.Concat(Buttons.A));
					c.KeyStatus["LockOn"].Add(string.Concat(Buttons.X));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.LeftStick));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.Back));
					return;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.A));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.X));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.Back));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
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
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["Inventory"].Add(Keys.Escape.ToString());
					return;
				case InputMode.Mouse:
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Jump"].Add(string.Concat(Buttons.A));
					c.KeyStatus["LockOn"].Add(string.Concat(Buttons.X));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.LeftStick));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.Back));
					return;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.A));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.X));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.Back));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					break;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x004DE694 File Offset: 0x004DC894
		public static void SetZoom_UI()
		{
			float uiscale = Main.UIScale;
			PlayerInput.SetZoom_Scaled(1f / uiscale);
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x004DE6B3 File Offset: 0x004DC8B3
		public static void SetZoom_World()
		{
			PlayerInput.SetZoom_Scaled(1f);
			PlayerInput.SetZoom_MouseInWorld();
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x004DE6C4 File Offset: 0x004DC8C4
		public static void SetZoom_Unscaled()
		{
			Main.lastMouseX = PlayerInput._originalLastMouseX;
			Main.lastMouseY = PlayerInput._originalLastMouseY;
			Main.mouseX = PlayerInput._originalMouseX;
			Main.mouseY = PlayerInput._originalMouseY;
			Main.screenWidth = PlayerInput._originalScreenWidth;
			Main.screenHeight = PlayerInput._originalScreenHeight;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x004DE704 File Offset: 0x004DC904
		public static void SetZoom_Test()
		{
			Vector2 vector = Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f;
			Vector2 value = Main.screenPosition + new Vector2((float)PlayerInput._originalMouseX, (float)PlayerInput._originalMouseY);
			Vector2 value2 = Main.screenPosition + new Vector2((float)PlayerInput._originalLastMouseX, (float)PlayerInput._originalLastMouseY);
			Vector2 value3 = Main.screenPosition + new Vector2(0f, 0f);
			Vector2 value4 = Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight);
			Vector2 value5 = value - vector;
			Vector2 value6 = value2 - vector;
			Vector2 value7 = value3 - vector;
			value4 - vector;
			float scaleFactor = 1f / Main.GameViewMatrix.Zoom.X;
			float num = 1f;
			Vector2 vector2 = vector - Main.screenPosition + value5 * scaleFactor;
			Vector2 vector3 = vector - Main.screenPosition + value6 * scaleFactor;
			Vector2 screenPosition = vector + value7 * num;
			Main.mouseX = (int)vector2.X;
			Main.mouseY = (int)vector2.Y;
			Main.lastMouseX = (int)vector3.X;
			Main.lastMouseY = (int)vector3.Y;
			Main.screenPosition = screenPosition;
			Main.screenWidth = (int)((float)PlayerInput._originalScreenWidth * num);
			Main.screenHeight = (int)((float)PlayerInput._originalScreenHeight * num);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x004DE880 File Offset: 0x004DCA80
		public static void SetZoom_MouseInWorld()
		{
			Vector2 vector = Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f;
			Vector2 value = Main.screenPosition + new Vector2((float)PlayerInput._originalMouseX, (float)PlayerInput._originalMouseY);
			Vector2 value2 = Main.screenPosition + new Vector2((float)PlayerInput._originalLastMouseX, (float)PlayerInput._originalLastMouseY);
			Vector2 value3 = value - vector;
			Vector2 value4 = value2 - vector;
			float scaleFactor = 1f / Main.GameViewMatrix.Zoom.X;
			Vector2 vector2 = vector - Main.screenPosition + value3 * scaleFactor;
			Main.mouseX = (int)vector2.X;
			Main.mouseY = (int)vector2.Y;
			Vector2 vector3 = vector - Main.screenPosition + value4 * scaleFactor;
			Main.lastMouseX = (int)vector3.X;
			Main.lastMouseY = (int)vector3.Y;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x004DE96E File Offset: 0x004DCB6E
		public static void SetDesiredZoomContext(ZoomContext context)
		{
			PlayerInput._currentWantedZoom = context;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x004DE978 File Offset: 0x004DCB78
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

		// Token: 0x060018B2 RID: 6322 RVA: 0x004DE9F0 File Offset: 0x004DCBF0
		private static void SetZoom_Scaled(float scale)
		{
			Main.lastMouseX = (int)((float)PlayerInput._originalLastMouseX * scale);
			Main.lastMouseY = (int)((float)PlayerInput._originalLastMouseY * scale);
			Main.mouseX = (int)((float)PlayerInput._originalMouseX * scale);
			Main.mouseY = (int)((float)PlayerInput._originalMouseY * scale);
			Main.screenWidth = (int)((float)PlayerInput._originalScreenWidth * scale);
			Main.screenHeight = (int)((float)PlayerInput._originalScreenHeight * scale);
		}

		// Token: 0x040014D3 RID: 5331
		public static Vector2 RawMouseScale = Vector2.One;

		// Token: 0x040014D4 RID: 5332
		public static TriggersPack Triggers = new TriggersPack();

		// Token: 0x040014D5 RID: 5333
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

		// Token: 0x040014D6 RID: 5334
		private static bool _canReleaseRebindingLock = true;

		// Token: 0x040014D7 RID: 5335
		private static int _memoOfLastPoint = -1;

		// Token: 0x040014D8 RID: 5336
		public static int NavigatorRebindingLock;

		// Token: 0x040014D9 RID: 5337
		public static string BlockedKey = "";

		// Token: 0x040014DA RID: 5338
		private static string _listeningTrigger;

		// Token: 0x040014DB RID: 5339
		private static InputMode _listeningInputMode;

		// Token: 0x040014DC RID: 5340
		public static Dictionary<string, PlayerInputProfile> Profiles = new Dictionary<string, PlayerInputProfile>();

		// Token: 0x040014DD RID: 5341
		public static Dictionary<string, PlayerInputProfile> OriginalProfiles = new Dictionary<string, PlayerInputProfile>();

		// Token: 0x040014DE RID: 5342
		private static string _selectedProfile;

		// Token: 0x040014DF RID: 5343
		private static PlayerInputProfile _currentProfile;

		// Token: 0x040014E0 RID: 5344
		public static InputMode CurrentInputMode = InputMode.Keyboard;

		// Token: 0x040014E1 RID: 5345
		private static Buttons[] ButtonsGamepad = (Buttons[])Enum.GetValues(typeof(Buttons));

		// Token: 0x040014E2 RID: 5346
		public static bool GrappleAndInteractAreShared;

		// Token: 0x040014E3 RID: 5347
		public static SmartSelectGamepadPointer smartSelectPointer = new SmartSelectGamepadPointer();

		// Token: 0x040014E4 RID: 5348
		public static bool UseSteamDeckIfPossible;

		// Token: 0x040014E5 RID: 5349
		private static string _invalidatorCheck = "";

		// Token: 0x040014E6 RID: 5350
		private static bool _lastActivityState;

		// Token: 0x040014E7 RID: 5351
		public static MouseState MouseInfo;

		// Token: 0x040014E8 RID: 5352
		public static MouseState MouseInfoOld;

		// Token: 0x040014E9 RID: 5353
		public static int MouseX;

		// Token: 0x040014EA RID: 5354
		public static int MouseY;

		// Token: 0x040014EB RID: 5355
		public static bool LockGamepadTileUseButton = false;

		// Token: 0x040014EC RID: 5356
		public static List<string> MouseKeys = new List<string>();

		// Token: 0x040014ED RID: 5357
		public static int PreUIX;

		// Token: 0x040014EE RID: 5358
		public static int PreUIY;

		// Token: 0x040014EF RID: 5359
		public static int PreLockOnX;

		// Token: 0x040014F0 RID: 5360
		public static int PreLockOnY;

		// Token: 0x040014F1 RID: 5361
		public static int ScrollWheelValue;

		// Token: 0x040014F2 RID: 5362
		public static int ScrollWheelValueOld;

		// Token: 0x040014F3 RID: 5363
		public static int ScrollWheelDelta;

		// Token: 0x040014F4 RID: 5364
		public static int ScrollWheelDeltaForUI;

		// Token: 0x040014F5 RID: 5365
		public static bool GamepadAllowScrolling;

		// Token: 0x040014F6 RID: 5366
		public static int GamepadScrollValue;

		// Token: 0x040014F7 RID: 5367
		public static Vector2 GamepadThumbstickLeft = Vector2.Zero;

		// Token: 0x040014F8 RID: 5368
		public static Vector2 GamepadThumbstickRight = Vector2.Zero;

		// Token: 0x040014F9 RID: 5369
		private static PlayerInput.FastUseItemMemory _fastUseMemory;

		// Token: 0x040014FA RID: 5370
		private static bool _InBuildingMode;

		// Token: 0x040014FB RID: 5371
		private static int _UIPointForBuildingMode = -1;

		// Token: 0x040014FC RID: 5372
		public static bool WritingText;

		// Token: 0x040014FD RID: 5373
		private static int _originalMouseX;

		// Token: 0x040014FE RID: 5374
		private static int _originalMouseY;

		// Token: 0x040014FF RID: 5375
		private static int _originalLastMouseX;

		// Token: 0x04001500 RID: 5376
		private static int _originalLastMouseY;

		// Token: 0x04001501 RID: 5377
		private static int _originalScreenWidth;

		// Token: 0x04001502 RID: 5378
		private static int _originalScreenHeight;

		// Token: 0x04001503 RID: 5379
		private static ZoomContext _currentWantedZoom;

		// Token: 0x04001504 RID: 5380
		private static List<string> _buttonsLocked = new List<string>();

		// Token: 0x04001505 RID: 5381
		public static bool PreventCursorModeSwappingToGamepad = false;

		// Token: 0x04001506 RID: 5382
		public static bool PreventFirstMousePositionGrab = false;

		// Token: 0x04001507 RID: 5383
		private static int[] DpadSnapCooldown = new int[4];

		// Token: 0x04001508 RID: 5384
		public static bool AllowExecutionOfGamepadInstructions = true;

		// Token: 0x020005A2 RID: 1442
		public class MiscSettingsTEMP
		{
			// Token: 0x04005A37 RID: 23095
			public static bool HotbarRadialShouldBeUsed = true;
		}

		// Token: 0x020005A3 RID: 1443
		public static class SettingsForUI
		{
			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x06003251 RID: 12881 RVA: 0x005EBC04 File Offset: 0x005E9E04
			// (set) Token: 0x06003252 RID: 12882 RVA: 0x005EBC0B File Offset: 0x005E9E0B
			public static CursorMode CurrentCursorMode { get; private set; }

			// Token: 0x06003253 RID: 12883 RVA: 0x005EBC13 File Offset: 0x005E9E13
			public static void SetCursorMode(CursorMode cursorMode)
			{
				PlayerInput.SettingsForUI.CurrentCursorMode = cursorMode;
				if (PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Mouse)
				{
					PlayerInput.SettingsForUI.FramesSinceLastTimeInMouseMode = 0;
				}
			}

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x06003254 RID: 12884 RVA: 0x004DADFA File Offset: 0x004D8FFA
			public static bool ShowGamepadHints
			{
				get
				{
					return PlayerInput.UsingGamepad || PlayerInput.SteamDeckIsUsed;
				}
			}

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x06003255 RID: 12885 RVA: 0x005EBC28 File Offset: 0x005E9E28
			public static bool AllowSecondaryGamepadAim
			{
				get
				{
					return PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Gamepad || !PlayerInput.SteamDeckIsUsed;
				}
			}

			// Token: 0x170003B4 RID: 948
			// (get) Token: 0x06003256 RID: 12886 RVA: 0x005EBC3C File Offset: 0x005E9E3C
			public static bool PushEquipmentAreaUp
			{
				get
				{
					return PlayerInput.UsingGamepad && !PlayerInput.SteamDeckIsUsed;
				}
			}

			// Token: 0x170003B5 RID: 949
			// (get) Token: 0x06003257 RID: 12887 RVA: 0x005EBC4F File Offset: 0x005E9E4F
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

			// Token: 0x170003B6 RID: 950
			// (get) Token: 0x06003258 RID: 12888 RVA: 0x005EBC66 File Offset: 0x005E9E66
			public static bool HighlightThingsForMouse
			{
				get
				{
					return !PlayerInput.UsingGamepadUI || PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Mouse;
				}
			}

			// Token: 0x170003B7 RID: 951
			// (get) Token: 0x06003259 RID: 12889 RVA: 0x005EBC79 File Offset: 0x005E9E79
			// (set) Token: 0x0600325A RID: 12890 RVA: 0x005EBC80 File Offset: 0x005E9E80
			public static int FramesSinceLastTimeInMouseMode { get; private set; }

			// Token: 0x170003B8 RID: 952
			// (get) Token: 0x0600325B RID: 12891 RVA: 0x005EBC88 File Offset: 0x005E9E88
			public static bool PreventHighlightsForGamepad
			{
				get
				{
					return PlayerInput.SettingsForUI.FramesSinceLastTimeInMouseMode == 0;
				}
			}

			// Token: 0x0600325C RID: 12892 RVA: 0x005EBC92 File Offset: 0x005E9E92
			public static void UpdateCounters()
			{
				if (PlayerInput.SettingsForUI.CurrentCursorMode != CursorMode.Mouse)
				{
					PlayerInput.SettingsForUI.FramesSinceLastTimeInMouseMode++;
				}
			}

			// Token: 0x0600325D RID: 12893 RVA: 0x005EBCA7 File Offset: 0x005E9EA7
			public static void TryRevertingToMouseMode()
			{
				if (PlayerInput.SettingsForUI.FramesSinceLastTimeInMouseMode > 0)
				{
					return;
				}
				PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Mouse);
				PlayerInput.CurrentInputMode = InputMode.Mouse;
				PlayerInput.Triggers.Current.UsedMovementKey = false;
				PlayerInput.NavigatorUnCachePosition();
			}
		}

		// Token: 0x020005A4 RID: 1444
		private struct FastUseItemMemory
		{
			// Token: 0x0600325E RID: 12894 RVA: 0x005EBCD4 File Offset: 0x005E9ED4
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

			// Token: 0x0600325F RID: 12895 RVA: 0x005EBD35 File Offset: 0x005E9F35
			public bool TryStartForMouse(Player player)
			{
				this._player = player;
				this._slot = -1;
				this._itemType = Main.mouseItem.type;
				this._shouldFastUse = true;
				this._isMouseItem = true;
				return true;
			}

			// Token: 0x06003260 RID: 12896 RVA: 0x005EBD64 File Offset: 0x005E9F64
			public void Clear()
			{
				this._shouldFastUse = false;
			}

			// Token: 0x06003261 RID: 12897 RVA: 0x005EBD70 File Offset: 0x005E9F70
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

			// Token: 0x06003262 RID: 12898 RVA: 0x005EBDC4 File Offset: 0x005E9FC4
			public void EndFastUse()
			{
				if (!this._shouldFastUse)
				{
					return;
				}
				if (!this._isMouseItem && this._player.inventory[this._slot].IsAir)
				{
					Utils.Swap<Item>(ref Main.mouseItem, ref this._player.inventory[this._slot]);
				}
				this.Clear();
			}

			// Token: 0x04005A3A RID: 23098
			private int _slot;

			// Token: 0x04005A3B RID: 23099
			private int _itemType;

			// Token: 0x04005A3C RID: 23100
			private bool _shouldFastUse;

			// Token: 0x04005A3D RID: 23101
			private bool _isMouseItem;

			// Token: 0x04005A3E RID: 23102
			private Player _player;
		}
	}
}
