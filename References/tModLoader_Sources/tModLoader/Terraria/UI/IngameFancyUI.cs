using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Achievements;
using Terraria.Audio;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI.Gamepad;

namespace Terraria.UI
{
	/// <summary>
	/// Provides convenient access to the <see cref="F:Terraria.Main.InGameUI" /> <see cref="T:Terraria.UI.UserInterface" /> class. Use this for in-game non-gameplay fullscreen user interfaces. User interfaces shown using IngameFancyUI hide all other UI. Some examples include the bestiary, emote menu, and settings menus. There can only be one active fullscreen user interface and the user can't play the game normally when active, so this is only useful for non-gameplay UI.
	/// <para /> Use <see cref="M:Terraria.UI.IngameFancyUI.OpenUIState(Terraria.UI.UIState)" /> to show a <see cref="T:Terraria.UI.UIState" /> and <see cref="M:Terraria.UI.IngameFancyUI.Close" /> to close it. There is no need to manage a <see cref="T:Terraria.UI.UserInterface" /> or interface layers when using this approach, but it is more limited in flexibility.
	/// </summary>
	// Token: 0x020000A6 RID: 166
	public class IngameFancyUI
	{
		// Token: 0x060014FB RID: 5371 RVA: 0x004A6BCA File Offset: 0x004A4DCA
		public static void CoverNextFrame()
		{
			IngameFancyUI.CoverForOneUIFrame = true;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x004A6BD2 File Offset: 0x004A4DD2
		public static bool CanCover()
		{
			if (IngameFancyUI.CoverForOneUIFrame)
			{
				IngameFancyUI.CoverForOneUIFrame = false;
				return true;
			}
			return false;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x004A6BE4 File Offset: 0x004A4DE4
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

		// Token: 0x060014FE RID: 5374 RVA: 0x004A6C1B File Offset: 0x004A4E1B
		public static void OpenAchievementsAndGoto(Achievement achievement)
		{
			IngameFancyUI.OpenAchievements();
			Main.AchievementsMenu.GotoAchievement(achievement);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x004A6C2D File Offset: 0x004A4E2D
		private static void ClearChat()
		{
			Main.ClosePlayerChat();
			Main.chatText = "";
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x004A6C3E File Offset: 0x004A4E3E
		public static void OpenKeybinds()
		{
			IngameFancyUI.OpenUIState(Main.ManageControlsMenu);
		}

		/// <summary>
		/// Sets the current <see cref="T:Terraria.UI.UIState" /> for <see cref="F:Terraria.Main.InGameUI" />. Modders can use this to display a non-gameplay UI without managing and updating a <see cref="T:Terraria.UI.UserInterface" />.
		/// </summary>
		/// <param name="uiState"></param>
		// Token: 0x06001501 RID: 5377 RVA: 0x004A6C4A File Offset: 0x004A4E4A
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

		// Token: 0x06001502 RID: 5378 RVA: 0x004A6C7D File Offset: 0x004A4E7D
		public static bool CanShowVirtualKeyboard(int context)
		{
			return UIVirtualKeyboard.CanDisplay(context);
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x004A6C88 File Offset: 0x004A4E88
		public unsafe static void OpenVirtualKeyboard(int keyboardContext)
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
					if (*tile.type == 21)
					{
						Main.defaultChestName = Lang.chestType[(int)(*tile.frameX / 36)].Value;
					}
					else if (*tile.type == 467 && *tile.frameX / 36 == 4)
					{
						Main.defaultChestName = Lang.GetItemNameValue(3988);
					}
					else if (*tile.type == 467)
					{
						Main.defaultChestName = Lang.chestType2[(int)(*tile.frameX / 36)].Value;
					}
					else if (*tile.type == 88)
					{
						Main.defaultChestName = Lang.dresserType[(int)(*tile.frameX / 54)].Value;
					}
					if (*tile.type >= TileID.Count && (TileID.Sets.BasicChest[(int)(*tile.type)] || TileID.Sets.BasicDresser[(int)(*tile.type)]))
					{
						Main.defaultChestName = TileLoader.DefaultContainerName((int)(*tile.type), (int)(*tile.TileFrameX), (int)(*tile.TileFrameY));
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
					Main.InGameUI.SetState(new UIVirtualKeyboard(labelText, Main.npcChatText, delegate(string <p0>)
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
				Main.InGameUI.SetState(new UIVirtualKeyboard(labelText, Main.npcChatText, delegate(string <p0>)
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

		// Token: 0x06001504 RID: 5380 RVA: 0x004A6F14 File Offset: 0x004A5114
		public static void Close()
		{
			Main.inFancyUI = false;
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			bool flag = !Main.gameMenu;
			bool flag2 = !(Main.InGameUI.CurrentState is UIVirtualKeyboard);
			bool flag3 = false;
			if (UIVirtualKeyboard.KeyboardContext - 2 <= 1)
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
			bool flag4 = Main.InGameUI.CurrentState == Interface.modConfig;
			Main.InGameUI.SetState(null);
			UILinkPointNavigator.Shortcuts.FANCYUI_SPECIAL_INSTRUCTIONS = 0;
			if (flag4)
			{
				Interface.modConfig.HandleOnCloseCallback();
			}
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x004A6FD4 File Offset: 0x004A51D4
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

		// Token: 0x06001506 RID: 5382 RVA: 0x004A70AA File Offset: 0x004A52AA
		public static void MouseOver()
		{
			if (Main.inFancyUI && Main.InGameUI.IsElementUnderMouse())
			{
				Main.mouseText = true;
			}
		}

		// Token: 0x040010E2 RID: 4322
		private static bool CoverForOneUIFrame;
	}
}
