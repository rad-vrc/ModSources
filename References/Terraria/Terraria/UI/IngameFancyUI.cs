using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Achievements;
using Terraria.Audio;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI.Gamepad;

namespace Terraria.UI
{
	// Token: 0x02000091 RID: 145
	public class IngameFancyUI
	{
		// Token: 0x0600123F RID: 4671 RVA: 0x004916DA File Offset: 0x0048F8DA
		public static void CoverNextFrame()
		{
			IngameFancyUI.CoverForOneUIFrame = true;
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x004916E2 File Offset: 0x0048F8E2
		public static bool CanCover()
		{
			if (IngameFancyUI.CoverForOneUIFrame)
			{
				IngameFancyUI.CoverForOneUIFrame = false;
				return true;
			}
			return false;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x004916F4 File Offset: 0x0048F8F4
		public static void OpenAchievements()
		{
			IngameFancyUI.CoverNextFrame();
			Main.playerInventory = false;
			Main.editChest = false;
			Main.npcChatText = "";
			Main.inFancyUI = true;
			IngameFancyUI.ClearChat();
			Main.InGameUI.SetState(Main.AchievementsMenu);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0049172B File Offset: 0x0048F92B
		public static void OpenAchievementsAndGoto(Achievement achievement)
		{
			IngameFancyUI.OpenAchievements();
			Main.AchievementsMenu.GotoAchievement(achievement);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0049173D File Offset: 0x0048F93D
		private static void ClearChat()
		{
			Main.ClosePlayerChat();
			Main.chatText = "";
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0049174E File Offset: 0x0048F94E
		public static void OpenKeybinds()
		{
			IngameFancyUI.OpenUIState(Main.ManageControlsMenu);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0049175A File Offset: 0x0048F95A
		public static void OpenUIState(UIState uiState)
		{
			IngameFancyUI.CoverNextFrame();
			Main.playerInventory = false;
			Main.editChest = false;
			Main.npcChatText = "";
			Main.inFancyUI = true;
			IngameFancyUI.ClearChat();
			Main.InGameUI.SetState(uiState);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0049178D File Offset: 0x0048F98D
		public static bool CanShowVirtualKeyboard(int context)
		{
			return UIVirtualKeyboard.CanDisplay(context);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00491798 File Offset: 0x0048F998
		public static void OpenVirtualKeyboard(int keyboardContext)
		{
			IngameFancyUI.CoverNextFrame();
			IngameFancyUI.ClearChat();
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			string labelText = "";
			if (keyboardContext != 1)
			{
				if (keyboardContext == 2)
				{
					labelText = Language.GetTextValue("UI.EnterNewName");
					Player player = Main.player[Main.myPlayer];
					Main.npcChatText = Main.chest[player.chest].name;
					Tile tile = Main.tile[player.chestX, player.chestY];
					if (tile.type == 21)
					{
						Main.defaultChestName = Lang.chestType[(int)(tile.frameX / 36)].Value;
					}
					else if (tile.type == 467 && tile.frameX / 36 == 4)
					{
						Main.defaultChestName = Lang.GetItemNameValue(3988);
					}
					else if (tile.type == 467)
					{
						Main.defaultChestName = Lang.chestType2[(int)(tile.frameX / 36)].Value;
					}
					else if (tile.type == 88)
					{
						Main.defaultChestName = Lang.dresserType[(int)(tile.frameX / 54)].Value;
					}
					if (Main.npcChatText == "")
					{
						Main.npcChatText = Main.defaultChestName;
					}
					Main.editChest = true;
				}
			}
			else
			{
				Main.editSign = true;
				labelText = Language.GetTextValue("UI.EnterMessage");
			}
			Main.clrInput();
			if (!IngameFancyUI.CanShowVirtualKeyboard(keyboardContext))
			{
				return;
			}
			Main.inFancyUI = true;
			if (keyboardContext != 1)
			{
				if (keyboardContext == 2)
				{
					Main.InGameUI.SetState(new UIVirtualKeyboard(labelText, Main.npcChatText, delegate(string s)
					{
						ChestUI.RenameChestSubmit(Main.player[Main.myPlayer]);
						IngameFancyUI.Close();
					}, delegate()
					{
						ChestUI.RenameChestCancel();
						IngameFancyUI.Close();
					}, keyboardContext, false));
				}
			}
			else
			{
				Main.InGameUI.SetState(new UIVirtualKeyboard(labelText, Main.npcChatText, delegate(string s)
				{
					Main.SubmitSignText();
					IngameFancyUI.Close();
				}, delegate()
				{
					Main.InputTextSignCancel();
					IngameFancyUI.Close();
				}, keyboardContext, false));
			}
			UILinkPointNavigator.GoToDefaultPage(1);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x004919C0 File Offset: 0x0048FBC0
		public static void Close()
		{
			Main.inFancyUI = false;
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			bool flag = !Main.gameMenu;
			bool flag2 = !(Main.InGameUI.CurrentState is UIVirtualKeyboard);
			bool flag3 = false;
			int keyboardContext = UIVirtualKeyboard.KeyboardContext;
			if (keyboardContext - 2 <= 1)
			{
				flag3 = true;
			}
			if (flag && (!flag2 && !flag3))
			{
				flag = false;
			}
			if (flag)
			{
				Main.playerInventory = true;
			}
			if (!Main.gameMenu && Main.InGameUI.CurrentState is UIEmotesMenu)
			{
				Main.playerInventory = false;
			}
			Main.LocalPlayer.releaseInventory = false;
			Main.InGameUI.SetState(null);
			UILinkPointNavigator.Shortcuts.FANCYUI_SPECIAL_INSTRUCTIONS = 0;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00491A64 File Offset: 0x0048FC64
		public static bool Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			bool result = false;
			if (Main.InGameUI.CurrentState is UIVirtualKeyboard && UIVirtualKeyboard.KeyboardContext > 0)
			{
				if (!Main.inFancyUI)
				{
					Main.InGameUI.SetState(null);
				}
				if (Main.screenWidth >= 1705 || !PlayerInput.UsingGamepad)
				{
					result = true;
				}
			}
			if (!Main.gameMenu)
			{
				Main.mouseText = false;
				if (Main.InGameUI != null && Main.InGameUI.IsElementUnderMouse())
				{
					Main.player[Main.myPlayer].mouseInterface = true;
				}
				Main.instance.GUIBarsDraw();
				if (Main.InGameUI.CurrentState is UIVirtualKeyboard && UIVirtualKeyboard.KeyboardContext > 0)
				{
					Main.instance.GUIChatDraw();
				}
				if (!Main.inFancyUI)
				{
					Main.InGameUI.SetState(null);
				}
				Main.instance.DrawMouseOver();
				Main.DrawCursor(Main.DrawThickCursor(false), false);
			}
			return result;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00491B3A File Offset: 0x0048FD3A
		public static void MouseOver()
		{
			if (!Main.inFancyUI)
			{
				return;
			}
			if (Main.InGameUI.IsElementUnderMouse())
			{
				Main.mouseText = true;
			}
		}

		// Token: 0x04001013 RID: 4115
		private static bool CoverForOneUIFrame;
	}
}
