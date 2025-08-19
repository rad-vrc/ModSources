using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Default;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.ModLoader
{
	// Token: 0x02000197 RID: 407
	public static class MenuLoader
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x004E12B8 File Offset: 0x004DF4B8
		public static ModMenu CurrentMenu
		{
			get
			{
				return MenuLoader.currentMenu;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x004E12BF File Offset: 0x004DF4BF
		private static string[] KnownMenus
		{
			get
			{
				return MenuLoader.KnownMenuSaveString.Split(',', StringSplitOptions.None);
			}
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x004E12D0 File Offset: 0x004DF4D0
		private static void AddKnownMenu(string name)
		{
			string newSaveString = string.Join(",", MenuLoader.KnownMenus.Concat(new string[]
			{
				name
			}).Distinct<string>());
			if (newSaveString != MenuLoader.KnownMenuSaveString)
			{
				MenuLoader.KnownMenuSaveString = newSaveString;
				Main.SaveSettings();
			}
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x004E131C File Offset: 0x004DF51C
		internal static void Add(ModMenu modMenu)
		{
			List<ModMenu> obj = MenuLoader.menus;
			lock (obj)
			{
				MenuLoader.menus.Add(modMenu);
				ModTypeLookup<ModMenu>.Register(modMenu);
			}
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x004E1368 File Offset: 0x004DF568
		private static void OffsetModMenu(int offset)
		{
			List<ModMenu> obj = MenuLoader.menus;
			lock (obj)
			{
				MenuLoader.switchToMenu = MenuLoader.currentMenu;
				do
				{
					MenuLoader.switchToMenu = MenuLoader.menus[Utils.Repeat(MenuLoader.menus.IndexOf(MenuLoader.switchToMenu) + offset, MenuLoader.menus.Count)];
				}
				while (!MenuLoader.switchToMenu.IsAvailable);
			}
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x004E13E8 File Offset: 0x004DF5E8
		internal static void GotoSavedModMenu()
		{
			if (MenuLoader.LastSelectedModMenu == MenuLoader.MenuOldVanilla.FullName)
			{
				Main.instance.playOldTile = true;
			}
			MenuLoader.switchToMenu = MenuLoader.MenutML;
			ModMenu value;
			if (ModContent.TryFind<ModMenu>(MenuLoader.LastSelectedModMenu, out value) && value.IsAvailable)
			{
				MenuLoader.switchToMenu = value;
			}
			if (MenuLoader.LastSelectedModMenu == MenuLoader.MenuJourneysEnd.FullName)
			{
				MenuLoader.switchToMenu = MenuLoader.MenuJourneysEnd;
			}
			MenuLoader.loading = false;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x004E1462 File Offset: 0x004DF662
		public static void ActivateOldVanillaMenu()
		{
			MenuLoader.switchToMenu = MenuLoader.MenuOldVanilla;
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x004E1470 File Offset: 0x004DF670
		internal static void UpdateAndDrawModMenu(SpriteBatch spriteBatch, GameTime gameTime, Color color, float logoRotation, float logoScale)
		{
			UserInterface activeInterface = UserInterface.ActiveInstance;
			try
			{
				MenuLoader.UpdateAndDrawModMenuInner(spriteBatch, gameTime, color, logoRotation, logoScale);
			}
			finally
			{
				UserInterface.ActiveInstance = activeInterface;
			}
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x004E14A8 File Offset: 0x004DF6A8
		private static void UpdateAndDrawModMenuInner(SpriteBatch spriteBatch, GameTime gameTime, Color color, float logoRotation, float logoScale)
		{
			if (MenuLoader.switchToMenu != null && MenuLoader.switchToMenu != MenuLoader.currentMenu)
			{
				MenuLoader.currentMenu.OnDeselected();
				MenuLoader.currentMenu = MenuLoader.switchToMenu;
				MenuLoader.currentMenu.OnSelected();
				if (MenuLoader.currentMenu.IsNew)
				{
					MenuLoader.currentMenu.IsNew = false;
					MenuLoader.AddKnownMenu(MenuLoader.currentMenu.FullName);
				}
			}
			MenuLoader.switchToMenu = null;
			if (!MenuLoader.loading && MenuLoader.currentMenu.FullName != MenuLoader.LastSelectedModMenu)
			{
				MenuLoader.LastSelectedModMenu = MenuLoader.currentMenu.FullName;
				Main.SaveSettings();
			}
			MenuLoader.currentMenu.UserInterface.Update(gameTime);
			UserInterface.ActiveInstance = MenuLoader.currentMenu.UserInterface;
			MenuLoader.currentMenu.UserInterface.Draw(spriteBatch, gameTime);
			MenuLoader.currentMenu.Update(Main.menuMode == 0);
			Texture2D logo = MenuLoader.currentMenu.Logo.Value;
			Vector2 logoDrawPos;
			logoDrawPos..ctor((float)(Main.screenWidth / 2), 100f);
			float scale = logoScale;
			if (MenuLoader.currentMenu.PreDrawLogo(spriteBatch, ref logoDrawPos, ref logoRotation, ref scale, ref color))
			{
				spriteBatch.Draw(logo, logoDrawPos, new Rectangle?(new Rectangle(0, 0, logo.Width, logo.Height)), color, logoRotation, new Vector2((float)logo.Width * 0.5f, (float)logo.Height * 0.5f), scale, 0, 0f);
			}
			MenuLoader.currentMenu.PostDrawLogo(spriteBatch, logoDrawPos, logoRotation, scale, color);
			List<ModMenu> obj = MenuLoader.menus;
			int newMenus;
			lock (obj)
			{
				string[] knownMenus = MenuLoader.KnownMenus;
				foreach (ModMenu menu in MenuLoader.menus)
				{
					menu.IsNew = (menu.IsAvailable && !knownMenus.Contains(menu.FullName));
				}
				newMenus = MenuLoader.menus.Count((ModMenu m) => m.IsNew);
			}
			string textValue = Language.GetTextValue("tModLoader.ModMenuSwap");
			string str = ": ";
			string displayName = MenuLoader.currentMenu.DisplayName;
			string str2;
			if (newMenus != 0)
			{
				if (!ModLoader.notifyNewMainMenuThemes)
				{
					str2 = "";
				}
				else
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted<int>(newMenus);
					defaultInterpolatedStringHandler.AppendLiteral(" New)");
					str2 = defaultInterpolatedStringHandler.ToStringAndClear();
				}
			}
			else
			{
				str2 = "";
			}
			string text = textValue + str + displayName + str2;
			Vector2 size = ChatManager.GetStringSize(FontAssets.MouseText.Value, ChatManager.ParseMessage(text, color).ToArray(), Vector2.One, -1f);
			Rectangle switchTextRect = (Main.menuMode == 0) ? new Rectangle((int)((float)(Main.screenWidth / 2) - size.X / 2f), (int)((float)(Main.screenHeight - 2) - size.Y), (int)size.X, (int)size.Y) : Rectangle.Empty;
			if (switchTextRect.Contains(Main.mouseX, Main.mouseY) && !Main.alreadyGrabbingSunOrMoon)
			{
				if (Main.mouseLeftRelease && Main.mouseLeft)
				{
					SoundEngine.PlaySound(SoundID.MenuTick, null, null);
					MenuLoader.OffsetModMenu(1);
				}
				else if (Main.mouseRightRelease && Main.mouseRight)
				{
					SoundEngine.PlaySound(SoundID.MenuTick, null, null);
					MenuLoader.OffsetModMenu(-1);
				}
			}
			if (Main.menuMode == 0)
			{
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)switchTextRect.X, (float)switchTextRect.Y), switchTextRect.Contains(Main.mouseX, Main.mouseY) ? Main.OurFavoriteColor : new Color(120, 120, 120, 76), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x004E1898 File Offset: 0x004DFA98
		internal static void Unload()
		{
			MenuLoader.loading = true;
			if (MenuLoader.menus.IndexOf(MenuLoader.currentMenu, 0, MenuLoader.DefaultMenuCount) == -1)
			{
				MenuLoader.switchToMenu = MenuLoader.MenutML;
				while (MenuLoader.currentMenu != MenuLoader.MenutML)
				{
					Thread.Yield();
				}
			}
			List<ModMenu> obj = MenuLoader.menus;
			lock (obj)
			{
				MenuLoader.menus.RemoveRange(MenuLoader.DefaultMenuCount, MenuLoader.menus.Count - MenuLoader.DefaultMenuCount);
			}
		}

		// Token: 0x0400167B RID: 5755
		internal static readonly MenutML MenutML = new MenutML();

		// Token: 0x0400167C RID: 5756
		internal static readonly MenuJourneysEnd MenuJourneysEnd = new MenuJourneysEnd();

		// Token: 0x0400167D RID: 5757
		internal static readonly MenuOldVanilla MenuOldVanilla = new MenuOldVanilla();

		// Token: 0x0400167E RID: 5758
		private static readonly List<ModMenu> menus = new List<ModMenu>
		{
			MenuLoader.MenutML,
			MenuLoader.MenuJourneysEnd,
			MenuLoader.MenuOldVanilla
		};

		// Token: 0x0400167F RID: 5759
		private static readonly int DefaultMenuCount = MenuLoader.menus.Count;

		// Token: 0x04001680 RID: 5760
		private static ModMenu currentMenu = MenuLoader.MenutML;

		// Token: 0x04001681 RID: 5761
		private static ModMenu switchToMenu = null;

		// Token: 0x04001682 RID: 5762
		internal static bool loading = true;

		// Token: 0x04001683 RID: 5763
		internal static string LastSelectedModMenu = MenuLoader.MenutML.FullName;

		// Token: 0x04001684 RID: 5764
		internal static string KnownMenuSaveString = string.Join(",", from m in MenuLoader.menus
		select m.FullName);
	}
}
