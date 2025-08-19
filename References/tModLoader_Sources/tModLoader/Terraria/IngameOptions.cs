using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.Social;
using Terraria.Social.Steam;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria
{
	// Token: 0x02000032 RID: 50
	public static class IngameOptions
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00027430 File Offset: 0x00025630
		public static void Open()
		{
			Main.ClosePlayerChat();
			Main.chatText = "";
			Main.playerInventory = false;
			Main.editChest = false;
			Main.npcChatText = "";
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.ingameOptionsWindow = true;
			IngameOptions.category = 0;
			for (int i = 0; i < IngameOptions.leftScale.Length; i++)
			{
				IngameOptions.leftScale[i] = 0f;
			}
			for (int j = 0; j < IngameOptions.rightScale.Length; j++)
			{
				IngameOptions.rightScale[j] = 0f;
			}
			IngameOptions.leftHover = -1;
			IngameOptions.rightHover = -1;
			IngameOptions.oldLeftHover = -1;
			IngameOptions.oldRightHover = -1;
			IngameOptions.rightLock = -1;
			IngameOptions.inBar = false;
			IngameOptions.notBar = false;
			IngameOptions.noSound = false;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000274EF File Offset: 0x000256EF
		public static void Close()
		{
			if (Main.setKey == -1)
			{
				Main.ingameOptionsWindow = false;
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Recipe.FindRecipes(false);
				Main.playerInventory = true;
				Main.SaveSettings();
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00027528 File Offset: 0x00025728
		public static void Draw(Main mainInstance, SpriteBatch sb)
		{
			IngameOptions._canConsumeHover = true;
			for (int i = 0; i < IngameOptions.skipRightSlot.Length; i++)
			{
				IngameOptions.skipRightSlot[i] = false;
			}
			bool flag = GameCulture.FromCultureName(GameCulture.CultureName.Russian).IsActive || GameCulture.FromCultureName(GameCulture.CultureName.Portuguese).IsActive || GameCulture.FromCultureName(GameCulture.CultureName.Polish).IsActive || GameCulture.FromCultureName(GameCulture.CultureName.French).IsActive;
			bool isActive = GameCulture.FromCultureName(GameCulture.CultureName.Polish).IsActive;
			bool isActive2 = GameCulture.FromCultureName(GameCulture.CultureName.German).IsActive;
			bool flag2 = GameCulture.FromCultureName(GameCulture.CultureName.Italian).IsActive || GameCulture.FromCultureName(GameCulture.CultureName.Spanish).IsActive;
			bool flag3 = false;
			int num = 70;
			float scale = 0.75f;
			float num2 = 60f;
			float maxWidth = 300f;
			if (flag)
			{
				flag3 = true;
			}
			if (isActive)
			{
				maxWidth = 200f;
			}
			new Vector2((float)Main.mouseX, (float)Main.mouseY);
			bool flag4 = Main.mouseLeft && Main.mouseLeftRelease;
			Vector2 vector8 = new Vector2((float)Main.screenWidth, (float)Main.screenHeight);
			Vector2 vector2;
			vector2..ctor(670f, 480f);
			Vector2 vector3 = vector8 / 2f - vector2 / 2f;
			int num3 = 20;
			IngameOptions._GUIHover = new Rectangle((int)(vector3.X - (float)num3), (int)(vector3.Y - (float)num3), (int)(vector2.X + (float)(num3 * 2)), (int)(vector2.Y + (float)(num3 * 2)));
			Utils.DrawInvBG(sb, vector3.X - (float)num3, vector3.Y - (float)num3, vector2.X + (float)(num3 * 2), vector2.Y + (float)(num3 * 2), new Color(33, 15, 91, 255) * 0.685f);
			if (new Rectangle((int)vector3.X - num3, (int)vector3.Y - num3, (int)vector2.X + num3 * 2, (int)vector2.Y + num3 * 2).Contains(new Point(Main.mouseX, Main.mouseY)))
			{
				Main.player[Main.myPlayer].mouseInterface = true;
			}
			Utils.DrawBorderString(sb, Language.GetTextValue("GameUI.SettingsMenu"), vector3 + vector2 * new Vector2(0.5f, 0f), Color.White, 1f, 0.5f, 0f, -1);
			if (flag)
			{
				Utils.DrawInvBG(sb, vector3.X + (float)(num3 / 2), vector3.Y + (float)(num3 * 5 / 2), vector2.X / 3f - (float)num3, vector2.Y - (float)(num3 * 3), default(Color));
				Utils.DrawInvBG(sb, vector3.X + vector2.X / 3f + (float)num3, vector3.Y + (float)(num3 * 5 / 2), vector2.X * 2f / 3f - (float)(num3 * 3 / 2), vector2.Y - (float)(num3 * 3), default(Color));
			}
			else
			{
				Utils.DrawInvBG(sb, vector3.X + (float)(num3 / 2), vector3.Y + (float)(num3 * 5 / 2), vector2.X / 2f - (float)num3, vector2.Y - (float)(num3 * 3), default(Color));
				Utils.DrawInvBG(sb, vector3.X + vector2.X / 2f + (float)num3, vector3.Y + (float)(num3 * 5 / 2), vector2.X / 2f - (float)(num3 * 3 / 2), vector2.Y - (float)(num3 * 3), default(Color));
			}
			float num4 = 0.7f;
			float num5 = 0.8f;
			float num6 = 0.01f;
			if (flag)
			{
				num4 = 0.4f;
				num5 = 0.44f;
			}
			if (isActive2)
			{
				num4 = 0.55f;
				num5 = 0.6f;
			}
			if (IngameOptions.oldLeftHover != IngameOptions.leftHover && IngameOptions.leftHover != -1)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}
			if (IngameOptions.oldRightHover != IngameOptions.rightHover && IngameOptions.rightHover != -1)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}
			if (flag4 && IngameOptions.rightHover != -1 && !IngameOptions.noSound)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}
			IngameOptions.oldLeftHover = IngameOptions.leftHover;
			IngameOptions.oldRightHover = IngameOptions.rightHover;
			IngameOptions.noSound = false;
			bool flag5 = SocialAPI.Network != null && SocialAPI.Network.CanInvite();
			int num7 = (flag5 > false) ? 1 : 0;
			int num8 = 5 + num7 + 2;
			num8++;
			Vector2 vector4;
			vector4..ctor(vector3.X + vector2.X / 4f, vector3.Y + (float)(num3 * 5 / 2));
			Vector2 vector5 = new Vector2(0f, vector2.Y - (float)(num3 * 5)) / (float)(num8 + 1);
			if (flag)
			{
				vector4.X -= 55f;
			}
			UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_LEFT = num8 + 1;
			for (int j = 0; j <= num8; j++)
			{
				bool flag6 = false;
				int value;
				if (IngameOptions._leftSideCategoryMapping.TryGetValue(j, out value))
				{
					flag6 = (IngameOptions.category == value);
				}
				if (IngameOptions.leftHover == j || flag6)
				{
					IngameOptions.leftScale[j] += num6;
				}
				else
				{
					IngameOptions.leftScale[j] -= num6;
				}
				if (IngameOptions.leftScale[j] < num4)
				{
					IngameOptions.leftScale[j] = num4;
				}
				if (IngameOptions.leftScale[j] > num5)
				{
					IngameOptions.leftScale[j] = num5;
				}
			}
			IngameOptions.leftHover = -1;
			int num24 = IngameOptions.category;
			int num9 = 0;
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[114].Value, num9, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num9;
				if (flag4)
				{
					IngameOptions.category = 0;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				}
			}
			num9++;
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[210].Value, num9, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num9;
				if (flag4)
				{
					IngameOptions.category = 1;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				}
			}
			num9++;
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[63].Value, num9, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num9;
				if (flag4)
				{
					IngameOptions.category = 2;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				}
			}
			num9++;
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[218].Value, num9, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num9;
				if (flag4)
				{
					IngameOptions.category = 3;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				}
			}
			num9++;
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[66].Value, num9, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num9;
				if (flag4)
				{
					IngameOptions.Close();
					IngameFancyUI.OpenKeybinds();
				}
			}
			num9++;
			if (IngameOptions.DrawLeftSide(sb, Language.GetTextValue("tModLoader.ModConfiguration"), num9, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num9;
				if (flag4)
				{
					IngameOptions.Close();
					IngameFancyUI.CoverNextFrame();
					Main.playerInventory = false;
					Main.editChest = false;
					Main.npcChatText = "";
					Main.inFancyUI = true;
					Main.InGameUI.SetState(Interface.modConfigList);
				}
			}
			num9++;
			if (flag5 && IngameOptions.DrawLeftSide(sb, Lang.menu[147].Value, num9, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num9;
				if (flag4)
				{
					IngameOptions.Close();
					SocialAPI.Network.OpenInviteInterface();
				}
			}
			if (flag5)
			{
				num9++;
			}
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[131].Value, num9, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num9;
				if (flag4)
				{
					IngameOptions.Close();
					IngameFancyUI.OpenAchievements();
				}
			}
			num9++;
			if (IngameOptions.DrawLeftSide(sb, Lang.menu[118].Value, num9, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num9;
				if (flag4)
				{
					IngameOptions.Close();
				}
			}
			num9++;
			if (IngameOptions.DrawLeftSide(sb, Lang.inter[35].Value, num9, vector4, vector5, IngameOptions.leftScale, 0.7f, 0.8f, 0.01f))
			{
				IngameOptions.leftHover = num9;
				if (flag4)
				{
					SteamedWraps.StopPlaytimeTracking();
					SystemLoader.PreSaveAndQuit();
					IngameOptions.Close();
					Main.menuMode = 10;
					Main.gameMenu = true;
					WorldGen.SaveAndQuit(null);
				}
			}
			num9++;
			if (num24 != IngameOptions.category)
			{
				for (int k = 0; k < IngameOptions.rightScale.Length; k++)
				{
					IngameOptions.rightScale[k] = 0f;
				}
			}
			int num10 = 0;
			int num11 = 0;
			switch (IngameOptions.category)
			{
			case 0:
				num11 = 16;
				num4 = 1f;
				num5 = 1.001f;
				num6 = 0.001f;
				break;
			case 1:
				num11 = 11;
				num11++;
				num4 = 1f;
				num5 = 1.001f;
				num6 = 0.001f;
				break;
			case 2:
				num11 = 12;
				num4 = 1f;
				num5 = 1.001f;
				num6 = 0.001f;
				break;
			case 3:
				num11 = 15;
				num4 = 1f;
				num5 = 1.001f;
				num6 = 0.001f;
				break;
			}
			if (flag)
			{
				num4 -= 0.1f;
				num5 -= 0.1f;
			}
			if (isActive2 && IngameOptions.category == 3)
			{
				num4 -= 0.15f;
				num5 -= 0.15f;
			}
			if (flag2 && (IngameOptions.category == 0 || IngameOptions.category == 3))
			{
				num4 -= 0.2f;
				num5 -= 0.2f;
			}
			UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_RIGHT = num11;
			Vector2 vector6;
			vector6..ctor(vector3.X + vector2.X * 3f / 4f, vector3.Y + (float)(num3 * 5 / 2));
			Vector2 vector7 = new Vector2(0f, vector2.Y - (float)(num3 * 3)) / (float)(num11 + 1);
			if (IngameOptions.category == 2)
			{
				vector7.Y -= 2f;
			}
			new Vector2(8f, 0f);
			if (flag)
			{
				vector6.X = vector3.X + vector2.X * 2f / 3f;
			}
			for (int l = 0; l < IngameOptions.rightScale.Length; l++)
			{
				if (IngameOptions.rightLock == l || (IngameOptions.rightHover == l && IngameOptions.rightLock == -1))
				{
					IngameOptions.rightScale[l] += num6;
				}
				else
				{
					IngameOptions.rightScale[l] -= num6;
				}
				if (IngameOptions.rightScale[l] < num4)
				{
					IngameOptions.rightScale[l] = num4;
				}
				if (IngameOptions.rightScale[l] > num5)
				{
					IngameOptions.rightScale[l] = num5;
				}
			}
			IngameOptions.inBar = false;
			IngameOptions.rightHover = -1;
			if (!Main.mouseLeft)
			{
				IngameOptions.rightLock = -1;
			}
			if (IngameOptions.rightLock == -1)
			{
				IngameOptions.notBar = false;
			}
			if (IngameOptions.category == 0)
			{
				int num12 = 0;
				IngameOptions.DrawRightSide(sb, Lang.menu[65].Value, num12, vector6, vector7, IngameOptions.rightScale[num12], 1f, default(Color));
				IngameOptions.skipRightSlot[num12] = true;
				num12++;
				vector6.X -= (float)num;
				if (IngameOptions.DrawRightSide(sb, Lang.menu[99].Value + " " + Math.Round((double)(Main.musicVolume * 100f)).ToString() + "%", num12, vector6, vector7, IngameOptions.rightScale[num12], (IngameOptions.rightScale[num12] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.noSound = true;
					IngameOptions.rightHover = num12;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float musicVolume = IngameOptions.DrawValueBar(sb, scale, Main.musicVolume, 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num12) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num12;
					if (Main.mouseLeft && IngameOptions.rightLock == num12)
					{
						Main.musicVolume = musicVolume;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num12;
				}
				if (IngameOptions.rightHover == num12)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 2;
				}
				num12++;
				if (IngameOptions.DrawRightSide(sb, Lang.menu[98].Value + " " + Math.Round((double)(Main.soundVolume * 100f)).ToString() + "%", num12, vector6, vector7, IngameOptions.rightScale[num12], (IngameOptions.rightScale[num12] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num12;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float soundVolume = IngameOptions.DrawValueBar(sb, scale, Main.soundVolume, 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num12) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num12;
					if (Main.mouseLeft && IngameOptions.rightLock == num12)
					{
						Main.soundVolume = soundVolume;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num12;
				}
				if (IngameOptions.rightHover == num12)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 3;
				}
				num12++;
				if (IngameOptions.DrawRightSide(sb, Lang.menu[119].Value + " " + Math.Round((double)(Main.ambientVolume * 100f)).ToString() + "%", num12, vector6, vector7, IngameOptions.rightScale[num12], (IngameOptions.rightScale[num12] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num12;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float ambientVolume = IngameOptions.DrawValueBar(sb, scale, Main.ambientVolume, 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num12) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num12;
					if (Main.mouseLeft && IngameOptions.rightLock == num12)
					{
						Main.ambientVolume = ambientVolume;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num12;
				}
				if (IngameOptions.rightHover == num12)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 4;
				}
				num12++;
				vector6.X += (float)num;
				IngameOptions.DrawRightSide(sb, "", num12, vector6, vector7, IngameOptions.rightScale[num12], 1f, default(Color));
				IngameOptions.skipRightSlot[num12] = true;
				num12++;
				IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.ZoomCategory"), num12, vector6, vector7, IngameOptions.rightScale[num12], 1f, default(Color));
				IngameOptions.skipRightSlot[num12] = true;
				num12++;
				vector6.X -= (float)num;
				string text = Language.GetTextValue("GameUI.GameZoom", Math.Round((double)(Main.GameZoomTarget * 100f)), Math.Round((double)(Main.GameViewMatrix.Zoom.X * 100f)));
				if (flag3)
				{
					text = FontAssets.ItemStack.Value.CreateWrappedText(text, maxWidth, Language.ActiveCulture.CultureInfo);
				}
				if (IngameOptions.DrawRightSide(sb, text, num12, vector6, vector7, IngameOptions.rightScale[num12] * 0.85f, (IngameOptions.rightScale[num12] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num12;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float num13 = IngameOptions.DrawValueBar(sb, scale, Main.GameZoomTarget - 1f, 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num12) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num12;
					if (Main.mouseLeft && IngameOptions.rightLock == num12)
					{
						Main.GameZoomTarget = num13 + 1f;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num12;
				}
				if (IngameOptions.rightHover == num12)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 10;
				}
				num12++;
				bool flag7 = false;
				if (Main.temporaryGUIScaleSlider == -1f)
				{
					Main.temporaryGUIScaleSlider = Main.UIScaleWanted;
				}
				string text2 = Language.GetTextValue("GameUI.UIScale", Math.Round((double)(Main.temporaryGUIScaleSlider * 100f)), Math.Round((double)(Main.UIScale * 100f)));
				if (flag3)
				{
					text2 = FontAssets.ItemStack.Value.CreateWrappedText(text2, maxWidth, Language.ActiveCulture.CultureInfo);
				}
				if (IngameOptions.DrawRightSide(sb, text2, num12, vector6, vector7, IngameOptions.rightScale[num12] * 0.75f, (IngameOptions.rightScale[num12] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num12;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float num14 = IngameOptions.DrawValueBar(sb, scale, MathHelper.Clamp((Main.temporaryGUIScaleSlider - 0.5f) / 1.5f, 0f, 1f), 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num12) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num12;
					if (Main.mouseLeft && IngameOptions.rightLock == num12)
					{
						Main.temporaryGUIScaleSlider = num14 * 1.5f + 0.5f;
						Main.temporaryGUIScaleSlider = (float)((int)(Main.temporaryGUIScaleSlider * 100f)) / 100f;
						Main.temporaryGUIScaleSliderUpdate = true;
						flag7 = true;
					}
				}
				if (!flag7 && Main.temporaryGUIScaleSliderUpdate && Main.temporaryGUIScaleSlider != -1f)
				{
					Main.UIScale = Main.temporaryGUIScaleSlider;
					Main.temporaryGUIScaleSliderUpdate = false;
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num12;
				}
				if (IngameOptions.rightHover == num12)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 11;
				}
				num12++;
				vector6.X += (float)num;
				IngameOptions.DrawRightSide(sb, "", num12, vector6, vector7, IngameOptions.rightScale[num12], 1f, default(Color));
				IngameOptions.skipRightSlot[num12] = true;
				num12++;
				IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.Gameplay"), num12, vector6, vector7, IngameOptions.rightScale[num12], 1f, default(Color));
				IngameOptions.skipRightSlot[num12] = true;
				num12++;
				if (IngameOptions.DrawRightSide(sb, Main.autoSave ? Lang.menu[67].Value : Lang.menu[68].Value, num12, vector6, vector7, IngameOptions.rightScale[num12], (IngameOptions.rightScale[num12] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num12;
					if (flag4)
					{
						Main.autoSave = !Main.autoSave;
					}
				}
				num12++;
				if (IngameOptions.DrawRightSide(sb, Main.autoPause ? Lang.menu[69].Value : Lang.menu[70].Value, num12, vector6, vector7, IngameOptions.rightScale[num12], (IngameOptions.rightScale[num12] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num12;
					if (flag4)
					{
						Main.autoPause = !Main.autoPause;
					}
				}
				num12++;
				if (IngameOptions.DrawRightSide(sb, Main.ReversedUpDownArmorSetBonuses ? Lang.menu[220].Value : Lang.menu[221].Value, num12, vector6, vector7, IngameOptions.rightScale[num12], (IngameOptions.rightScale[num12] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num12;
					if (flag4)
					{
						Main.ReversedUpDownArmorSetBonuses = !Main.ReversedUpDownArmorSetBonuses;
					}
				}
				num12++;
				DoorOpeningHelper.DoorAutoOpeningPreference preferenceSettings = DoorOpeningHelper.PreferenceSettings;
				string textValue;
				if (preferenceSettings != DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForGamepadOnly)
				{
					if (preferenceSettings != DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForEverything)
					{
						textValue = Language.GetTextValue("UI.SmartDoorsDisabled");
					}
					else
					{
						textValue = Language.GetTextValue("UI.SmartDoorsEnabled");
					}
				}
				else
				{
					textValue = Language.GetTextValue("UI.SmartDoorsGamepad");
				}
				if (IngameOptions.DrawRightSide(sb, textValue, num12, vector6, vector7, IngameOptions.rightScale[num12], (IngameOptions.rightScale[num12] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num12;
					if (flag4)
					{
						DoorOpeningHelper.CyclePreferences();
					}
				}
				num12++;
				string textValue2;
				if (Player.Settings.HoverControl != Player.Settings.HoverControlMode.Hold)
				{
					textValue2 = Language.GetTextValue("UI.HoverControlSettingIsClick");
				}
				else
				{
					textValue2 = Language.GetTextValue("UI.HoverControlSettingIsHold");
				}
				if (IngameOptions.DrawRightSide(sb, textValue2, num12, vector6, vector7, IngameOptions.rightScale[num12], (IngameOptions.rightScale[num12] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num12;
					if (flag4)
					{
						Player.Settings.CycleHoverControl();
					}
				}
				num12++;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue(Main.SettingsEnabled_AutoReuseAllItems ? "UI.AutoReuseAllOn" : "UI.AutoReuseAllOff"), num12, vector6, vector7, IngameOptions.rightScale[num12], (IngameOptions.rightScale[num12] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num12;
					if (flag4)
					{
						Main.SettingsEnabled_AutoReuseAllItems = !Main.SettingsEnabled_AutoReuseAllItems;
					}
				}
				num12++;
				IngameOptions.DrawRightSide(sb, "", num12, vector6, vector7, IngameOptions.rightScale[num12], 1f, default(Color));
				IngameOptions.skipRightSlot[num12] = true;
				num12++;
			}
			if (IngameOptions.category == 1)
			{
				int num15 = 0;
				if (IngameOptions.DrawRightSide(sb, Main.showItemText ? Lang.menu[71].Value : Lang.menu[72].Value, num15, vector6, vector7, IngameOptions.rightScale[num15], (IngameOptions.rightScale[num15] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num15;
					if (flag4)
					{
						Main.showItemText = !Main.showItemText;
					}
				}
				num15++;
				string value2 = Lang.menu[123].Value;
				string str = " ";
				LocalizedText localizedText = Lang.menu[124 + Main.invasionProgressMode];
				if (IngameOptions.DrawRightSide(sb, value2 + str + ((localizedText != null) ? localizedText.ToString() : null), num15, vector6, vector7, IngameOptions.rightScale[num15], (IngameOptions.rightScale[num15] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num15;
					if (flag4)
					{
						Main.invasionProgressMode++;
						if (Main.invasionProgressMode >= 3)
						{
							Main.invasionProgressMode = 0;
						}
					}
				}
				num15++;
				if (IngameOptions.DrawRightSide(sb, Main.placementPreview ? Lang.menu[128].Value : Lang.menu[129].Value, num15, vector6, vector7, IngameOptions.rightScale[num15], (IngameOptions.rightScale[num15] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num15;
					if (flag4)
					{
						Main.placementPreview = !Main.placementPreview;
					}
				}
				num15++;
				if (IngameOptions.DrawRightSide(sb, ItemSlot.Options.HighlightNewItems ? Lang.inter[117].Value : Lang.inter[116].Value, num15, vector6, vector7, IngameOptions.rightScale[num15], (IngameOptions.rightScale[num15] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num15;
					if (flag4)
					{
						ItemSlot.Options.HighlightNewItems = !ItemSlot.Options.HighlightNewItems;
					}
				}
				num15++;
				if (IngameOptions.DrawRightSide(sb, Main.MouseShowBuildingGrid ? Lang.menu[229].Value : Lang.menu[230].Value, num15, vector6, vector7, IngameOptions.rightScale[num15], (IngameOptions.rightScale[num15] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num15;
					if (flag4)
					{
						Main.MouseShowBuildingGrid = !Main.MouseShowBuildingGrid;
					}
				}
				num15++;
				if (IngameOptions.DrawRightSide(sb, Main.GamepadDisableInstructionsDisplay ? Lang.menu[241].Value : Lang.menu[242].Value, num15, vector6, vector7, IngameOptions.rightScale[num15], (IngameOptions.rightScale[num15] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num15;
					if (flag4)
					{
						Main.GamepadDisableInstructionsDisplay = !Main.GamepadDisableInstructionsDisplay;
					}
				}
				num15++;
				Action onClick;
				string text3 = BossBarLoader.InsertMenu(out onClick);
				if (IngameOptions.DrawRightSide(sb, text3, num15, vector6, vector7, IngameOptions.rightScale[num15], (IngameOptions.rightScale[num15] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num15;
					if (flag4)
					{
						onClick();
					}
				}
				num15++;
				string textValue3 = Language.GetTextValue("UI.MinimapFrame_" + Main.MinimapFrameManagerInstance.ActiveSelectionKeyName);
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("UI.SelectMapBorder", textValue3), num15, vector6, vector7, IngameOptions.rightScale[num15], (IngameOptions.rightScale[num15] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num15;
					if (flag4)
					{
						Main.MinimapFrameManagerInstance.CycleSelection();
					}
				}
				num15++;
				vector6.X -= (float)num;
				string text4 = Language.GetTextValue("GameUI.MapScale", Math.Round((double)(Main.MapScale * 100f)));
				if (flag3)
				{
					text4 = FontAssets.ItemStack.Value.CreateWrappedText(text4, maxWidth, Language.ActiveCulture.CultureInfo);
				}
				if (IngameOptions.DrawRightSide(sb, text4, num15, vector6, vector7, IngameOptions.rightScale[num15] * 0.85f, (IngameOptions.rightScale[num15] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num15;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float num16 = IngameOptions.DrawValueBar(sb, scale, (Main.MapScale - 0.5f) / 0.5f, 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num15) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num15;
					if (Main.mouseLeft && IngameOptions.rightLock == num15)
					{
						Main.MapScale = num16 * 0.5f + 0.5f;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num15;
				}
				if (IngameOptions.rightHover == num15)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 12;
				}
				num15++;
				vector6.X += (float)num;
				string textValue4 = Main.ResourceSetsManager.ActiveSet.DisplayedName;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("UI.SelectHealthStyle", textValue4), num15, vector6, vector7, IngameOptions.rightScale[num15], (IngameOptions.rightScale[num15] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num15;
					if (flag4)
					{
						Main.ResourceSetsManager.CycleResourceSet();
					}
				}
				num15++;
				string textValue5 = Language.GetTextValue(BigProgressBarSystem.ShowText ? "UI.ShowBossLifeTextOn" : "UI.ShowBossLifeTextOff");
				if (IngameOptions.DrawRightSide(sb, textValue5, num15, vector6, vector7, IngameOptions.rightScale[num15], (IngameOptions.rightScale[num15] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num15;
					if (flag4)
					{
						BigProgressBarSystem.ToggleShowText();
					}
				}
				num15++;
				if (IngameOptions.DrawRightSide(sb, Main.SettingsEnabled_OpaqueBoxBehindTooltips ? Language.GetTextValue("GameUI.HoverTextBoxesOn") : Language.GetTextValue("GameUI.HoverTextBoxesOff"), num15, vector6, vector7, IngameOptions.rightScale[num15], (IngameOptions.rightScale[num15] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num15;
					if (flag4)
					{
						Main.SettingsEnabled_OpaqueBoxBehindTooltips = !Main.SettingsEnabled_OpaqueBoxBehindTooltips;
					}
				}
				num15++;
			}
			if (IngameOptions.category == 2)
			{
				int num17 = 0;
				if (IngameOptions.DrawRightSide(sb, Main.graphics.IsFullScreen ? Lang.menu[49].Value : Lang.menu[50].Value, num17, vector6, vector7, IngameOptions.rightScale[num17], (IngameOptions.rightScale[num17] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num17;
					if (flag4)
					{
						Main.ToggleFullScreen();
					}
				}
				num17++;
				if (IngameOptions.DrawRightSide(sb, string.Concat(new string[]
				{
					Lang.menu[51].Value,
					": ",
					Main.PendingResolutionWidth.ToString(),
					"x",
					Main.PendingResolutionHeight.ToString()
				}), num17, vector6, vector7, IngameOptions.rightScale[num17], (IngameOptions.rightScale[num17] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num17;
					if (flag4 || (Main.mouseRight && Main.mouseRightRelease))
					{
						int num18 = 0;
						for (int m = 0; m < Main.numDisplayModes; m++)
						{
							if (Main.displayWidth[m] == Main.PendingResolutionWidth && Main.displayHeight[m] == Main.PendingResolutionHeight)
							{
								num18 = m;
								break;
							}
						}
						num18 = Utils.Repeat(num18 + (flag4 ? 1 : -1), Main.numDisplayModes);
						Main.PendingResolutionWidth = Main.displayWidth[num18];
						Main.PendingResolutionHeight = Main.displayHeight[num18];
						Main.SetResolution(Main.PendingResolutionWidth, Main.PendingResolutionHeight);
					}
				}
				num17++;
				vector6.X -= (float)num;
				if (IngameOptions.DrawRightSide(sb, Lang.menu[52].Value + ": " + Main.bgScroll.ToString() + "%", num17, vector6, vector7, IngameOptions.rightScale[num17], (IngameOptions.rightScale[num17] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.noSound = true;
					IngameOptions.rightHover = num17;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				float num19 = IngameOptions.DrawValueBar(sb, scale, (float)Main.bgScroll / 100f, 0, null);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num17) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num17;
					if (Main.mouseLeft && IngameOptions.rightLock == num17)
					{
						Main.bgScroll = (int)(num19 * 100f);
						Main.caveParallax = 1f - (float)Main.bgScroll / 500f;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num17;
				}
				if (IngameOptions.rightHover == num17)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 1;
				}
				num17++;
				vector6.X += (float)num;
				if (IngameOptions.DrawRightSide(sb, Lang.menu[(int)(247 + Main.FrameSkipMode)].Value, num17, vector6, vector7, IngameOptions.rightScale[num17], (IngameOptions.rightScale[num17] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num17;
					if (flag4)
					{
						Main.CycleFrameSkipMode();
					}
				}
				num17++;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("UI.LightMode_" + Lighting.Mode.ToString()), num17, vector6, vector7, IngameOptions.rightScale[num17], (IngameOptions.rightScale[num17] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num17;
					if (flag4)
					{
						Lighting.NextLightMode();
					}
				}
				num17++;
				if (IngameOptions.DrawRightSide(sb, Lang.menu[59 + Main.qaStyle].Value, num17, vector6, vector7, IngameOptions.rightScale[num17], (IngameOptions.rightScale[num17] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num17;
					if (flag4)
					{
						Main.qaStyle++;
						if (Main.qaStyle > 3)
						{
							Main.qaStyle = 0;
						}
					}
				}
				num17++;
				if (IngameOptions.DrawRightSide(sb, Main.BackgroundEnabled ? Lang.menu[100].Value : Lang.menu[101].Value, num17, vector6, vector7, IngameOptions.rightScale[num17], (IngameOptions.rightScale[num17] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num17;
					if (flag4)
					{
						Main.BackgroundEnabled = !Main.BackgroundEnabled;
					}
				}
				num17++;
				if (IngameOptions.DrawRightSide(sb, ChildSafety.Disabled ? Lang.menu[132].Value : Lang.menu[133].Value, num17, vector6, vector7, IngameOptions.rightScale[num17], (IngameOptions.rightScale[num17] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num17;
					if (flag4)
					{
						ChildSafety.Disabled = !ChildSafety.Disabled;
					}
				}
				num17++;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.HeatDistortion", Main.UseHeatDistortion ? Language.GetTextValue("GameUI.Enabled") : Language.GetTextValue("GameUI.Disabled")), num17, vector6, vector7, IngameOptions.rightScale[num17], (IngameOptions.rightScale[num17] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num17;
					if (flag4)
					{
						Main.UseHeatDistortion = !Main.UseHeatDistortion;
					}
				}
				num17++;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.StormEffects", Main.UseStormEffects ? Language.GetTextValue("GameUI.Enabled") : Language.GetTextValue("GameUI.Disabled")), num17, vector6, vector7, IngameOptions.rightScale[num17], (IngameOptions.rightScale[num17] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num17;
					if (flag4)
					{
						Main.UseStormEffects = !Main.UseStormEffects;
					}
				}
				num17++;
				string textValue6;
				switch (Main.WaveQuality)
				{
				case 1:
					textValue6 = Language.GetTextValue("GameUI.QualityLow");
					break;
				case 2:
					textValue6 = Language.GetTextValue("GameUI.QualityMedium");
					break;
				case 3:
					textValue6 = Language.GetTextValue("GameUI.QualityHigh");
					break;
				default:
					textValue6 = Language.GetTextValue("GameUI.QualityOff");
					break;
				}
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("GameUI.WaveQuality", textValue6), num17, vector6, vector7, IngameOptions.rightScale[num17], (IngameOptions.rightScale[num17] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num17;
					if (flag4)
					{
						Main.WaveQuality = (Main.WaveQuality + 1) % 4;
					}
				}
				num17++;
				if (IngameOptions.DrawRightSide(sb, Language.GetTextValue("UI.TilesSwayInWind" + (Main.SettingsEnabled_TilesSwayInWind ? "On" : "Off")), num17, vector6, vector7, IngameOptions.rightScale[num17], (IngameOptions.rightScale[num17] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num17;
					if (flag4)
					{
						Main.SettingsEnabled_TilesSwayInWind = !Main.SettingsEnabled_TilesSwayInWind;
					}
				}
				num17++;
			}
			if (IngameOptions.category == 3)
			{
				int num20 = 0;
				float num21 = (float)num;
				if (flag)
				{
					num2 = 126f;
				}
				Vector3 hSLVector = Main.mouseColorSlider.GetHSLVector();
				Main.mouseColorSlider.ApplyToMainLegacyBars();
				IngameOptions.DrawRightSide(sb, Lang.menu[64].Value, num20, vector6, vector7, IngameOptions.rightScale[num20], 1f, default(Color));
				IngameOptions.skipRightSlot[num20] = true;
				num20++;
				vector6.X -= num21;
				if (IngameOptions.DrawRightSide(sb, "", num20, vector6, vector7, IngameOptions.rightScale[num20], (IngameOptions.rightScale[num20] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num2;
				DelegateMethods.v3_1 = hSLVector;
				float scale2 = scale;
				float x2 = hSLVector.X;
				int lockState = 0;
				Utils.ColorLerpMethod colorMethod;
				if ((colorMethod = IngameOptions.<>O.<0>__ColorLerp_HSL_H) == null)
				{
					colorMethod = (IngameOptions.<>O.<0>__ColorLerp_HSL_H = new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_H));
				}
				float x = IngameOptions.DrawValueBar(sb, scale2, x2, lockState, colorMethod);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num20) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num20;
					if (Main.mouseLeft && IngameOptions.rightLock == num20)
					{
						hSLVector.X = x;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				if (IngameOptions.rightHover == num20)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 5;
					Main.menuMode = 25;
				}
				num20++;
				if (IngameOptions.DrawRightSide(sb, "", num20, vector6, vector7, IngameOptions.rightScale[num20], (IngameOptions.rightScale[num20] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num2;
				DelegateMethods.v3_1 = hSLVector;
				float scale3 = scale;
				float y = hSLVector.Y;
				int lockState2 = 0;
				Utils.ColorLerpMethod colorMethod2;
				if ((colorMethod2 = IngameOptions.<>O.<1>__ColorLerp_HSL_S) == null)
				{
					colorMethod2 = (IngameOptions.<>O.<1>__ColorLerp_HSL_S = new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_S));
				}
				x = IngameOptions.DrawValueBar(sb, scale3, y, lockState2, colorMethod2);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num20) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num20;
					if (Main.mouseLeft && IngameOptions.rightLock == num20)
					{
						hSLVector.Y = x;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				if (IngameOptions.rightHover == num20)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 6;
					Main.menuMode = 25;
				}
				num20++;
				if (IngameOptions.DrawRightSide(sb, "", num20, vector6, vector7, IngameOptions.rightScale[num20], (IngameOptions.rightScale[num20] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num2;
				DelegateMethods.v3_1 = hSLVector;
				DelegateMethods.v3_1.Z = Utils.GetLerpValue(0.15f, 1f, DelegateMethods.v3_1.Z, true);
				float scale4 = scale;
				float z = DelegateMethods.v3_1.Z;
				int lockState3 = 0;
				Utils.ColorLerpMethod colorMethod3;
				if ((colorMethod3 = IngameOptions.<>O.<2>__ColorLerp_HSL_L) == null)
				{
					colorMethod3 = (IngameOptions.<>O.<2>__ColorLerp_HSL_L = new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_L));
				}
				x = IngameOptions.DrawValueBar(sb, scale4, z, lockState3, colorMethod3);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num20) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num20;
					if (Main.mouseLeft && IngameOptions.rightLock == num20)
					{
						hSLVector.Z = x * 0.85f + 0.15f;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				if (IngameOptions.rightHover == num20)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 7;
					Main.menuMode = 25;
				}
				num20++;
				if (hSLVector.Z < 0.15f)
				{
					hSLVector.Z = 0.15f;
				}
				Main.mouseColorSlider.SetHSL(hSLVector);
				Main.mouseColor = Main.mouseColorSlider.GetColor();
				vector6.X += num21;
				IngameOptions.DrawRightSide(sb, "", num20, vector6, vector7, IngameOptions.rightScale[num20], 1f, default(Color));
				IngameOptions.skipRightSlot[num20] = true;
				num20++;
				hSLVector = Main.mouseBorderColorSlider.GetHSLVector();
				if (PlayerInput.UsingGamepad && IngameOptions.rightHover == -1)
				{
					Main.mouseBorderColorSlider.ApplyToMainLegacyBars();
				}
				IngameOptions.DrawRightSide(sb, Lang.menu[217].Value, num20, vector6, vector7, IngameOptions.rightScale[num20], 1f, default(Color));
				IngameOptions.skipRightSlot[num20] = true;
				num20++;
				vector6.X -= num21;
				if (IngameOptions.DrawRightSide(sb, "", num20, vector6, vector7, IngameOptions.rightScale[num20], (IngameOptions.rightScale[num20] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num2;
				DelegateMethods.v3_1 = hSLVector;
				float scale5 = scale;
				float x3 = hSLVector.X;
				int lockState4 = 0;
				Utils.ColorLerpMethod colorMethod4;
				if ((colorMethod4 = IngameOptions.<>O.<0>__ColorLerp_HSL_H) == null)
				{
					colorMethod4 = (IngameOptions.<>O.<0>__ColorLerp_HSL_H = new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_H));
				}
				x = IngameOptions.DrawValueBar(sb, scale5, x3, lockState4, colorMethod4);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num20) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num20;
					if (Main.mouseLeft && IngameOptions.rightLock == num20)
					{
						hSLVector.X = x;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				if (IngameOptions.rightHover == num20)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 5;
					Main.menuMode = 252;
				}
				num20++;
				if (IngameOptions.DrawRightSide(sb, "", num20, vector6, vector7, IngameOptions.rightScale[num20], (IngameOptions.rightScale[num20] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num2;
				DelegateMethods.v3_1 = hSLVector;
				float scale6 = scale;
				float y2 = hSLVector.Y;
				int lockState5 = 0;
				Utils.ColorLerpMethod colorMethod5;
				if ((colorMethod5 = IngameOptions.<>O.<1>__ColorLerp_HSL_S) == null)
				{
					colorMethod5 = (IngameOptions.<>O.<1>__ColorLerp_HSL_S = new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_S));
				}
				x = IngameOptions.DrawValueBar(sb, scale6, y2, lockState5, colorMethod5);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num20) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num20;
					if (Main.mouseLeft && IngameOptions.rightLock == num20)
					{
						hSLVector.Y = x;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				if (IngameOptions.rightHover == num20)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 6;
					Main.menuMode = 252;
				}
				num20++;
				if (IngameOptions.DrawRightSide(sb, "", num20, vector6, vector7, IngameOptions.rightScale[num20], (IngameOptions.rightScale[num20] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num2;
				DelegateMethods.v3_1 = hSLVector;
				float scale7 = scale;
				float z2 = hSLVector.Z;
				int lockState6 = 0;
				Utils.ColorLerpMethod colorMethod6;
				if ((colorMethod6 = IngameOptions.<>O.<2>__ColorLerp_HSL_L) == null)
				{
					colorMethod6 = (IngameOptions.<>O.<2>__ColorLerp_HSL_L = new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_L));
				}
				x = IngameOptions.DrawValueBar(sb, scale7, z2, lockState6, colorMethod6);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num20) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num20;
					if (Main.mouseLeft && IngameOptions.rightLock == num20)
					{
						hSLVector.Z = x;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				if (IngameOptions.rightHover == num20)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 7;
					Main.menuMode = 252;
				}
				num20++;
				if (IngameOptions.DrawRightSide(sb, "", num20, vector6, vector7, IngameOptions.rightScale[num20], (IngameOptions.rightScale[num20] - num4) / (num5 - num4), default(Color)))
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				IngameOptions.valuePosition.X = vector3.X + vector2.X - (float)(num3 / 2) - 20f;
				IngameOptions.valuePosition.Y = IngameOptions.valuePosition.Y - 3f;
				IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - num2;
				DelegateMethods.v3_1 = hSLVector;
				float num22 = Main.mouseBorderColorSlider.Alpha;
				float scale8 = scale;
				float perc = num22;
				int lockState7 = 0;
				Utils.ColorLerpMethod colorMethod7;
				if ((colorMethod7 = IngameOptions.<>O.<3>__ColorLerp_HSL_O) == null)
				{
					colorMethod7 = (IngameOptions.<>O.<3>__ColorLerp_HSL_O = new Utils.ColorLerpMethod(DelegateMethods.ColorLerp_HSL_O));
				}
				x = IngameOptions.DrawValueBar(sb, scale8, perc, lockState7, colorMethod7);
				if ((IngameOptions.inBar || IngameOptions.rightLock == num20) && !IngameOptions.notBar)
				{
					IngameOptions.rightHover = num20;
					if (Main.mouseLeft && IngameOptions.rightLock == num20)
					{
						num22 = x;
						IngameOptions.noSound = true;
					}
				}
				if ((float)Main.mouseX > vector3.X + vector2.X * 2f / 3f + (float)num3 && (float)Main.mouseX < IngameOptions.valuePosition.X + 3.75f && (float)Main.mouseY > IngameOptions.valuePosition.Y - 10f && (float)Main.mouseY <= IngameOptions.valuePosition.Y + 10f)
				{
					if (IngameOptions.rightLock == -1)
					{
						IngameOptions.notBar = true;
					}
					IngameOptions.rightHover = num20;
				}
				if (IngameOptions.rightHover == num20)
				{
					UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 8;
					Main.menuMode = 252;
				}
				num20++;
				Main.mouseBorderColorSlider.SetHSL(hSLVector);
				Main.mouseBorderColorSlider.Alpha = num22;
				Main.MouseBorderColor = Main.mouseBorderColorSlider.GetColor();
				vector6.X += num21;
				IngameOptions.DrawRightSide(sb, "", num20, vector6, vector7, IngameOptions.rightScale[num20], 1f, default(Color));
				IngameOptions.skipRightSlot[num20] = true;
				num20++;
				string txt = "";
				switch (LockOnHelper.UseMode)
				{
				case LockOnHelper.LockOnMode.FocusTarget:
					txt = Lang.menu[232].Value;
					break;
				case LockOnHelper.LockOnMode.TargetClosest:
					txt = Lang.menu[233].Value;
					break;
				case LockOnHelper.LockOnMode.ThreeDS:
					txt = Lang.menu[234].Value;
					break;
				}
				if (IngameOptions.DrawRightSide(sb, txt, num20, vector6, vector7, IngameOptions.rightScale[num20] * 0.9f, (IngameOptions.rightScale[num20] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num20;
					if (flag4)
					{
						LockOnHelper.CycleUseModes();
					}
				}
				num20++;
				if (IngameOptions.DrawRightSide(sb, Player.SmartCursorSettings.SmartBlocksEnabled ? Lang.menu[215].Value : Lang.menu[216].Value, num20, vector6, vector7, IngameOptions.rightScale[num20] * 0.9f, (IngameOptions.rightScale[num20] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num20;
					if (flag4)
					{
						Player.SmartCursorSettings.SmartBlocksEnabled = !Player.SmartCursorSettings.SmartBlocksEnabled;
					}
				}
				num20++;
				if (IngameOptions.DrawRightSide(sb, Main.cSmartCursorModeIsToggleAndNotHold ? Lang.menu[121].Value : Lang.menu[122].Value, num20, vector6, vector7, IngameOptions.rightScale[num20], (IngameOptions.rightScale[num20] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num20;
					if (flag4)
					{
						Main.cSmartCursorModeIsToggleAndNotHold = !Main.cSmartCursorModeIsToggleAndNotHold;
					}
				}
				num20++;
				if (IngameOptions.DrawRightSide(sb, Player.SmartCursorSettings.SmartAxeAfterPickaxe ? Lang.menu[214].Value : Lang.menu[213].Value, num20, vector6, vector7, IngameOptions.rightScale[num20] * 0.9f, (IngameOptions.rightScale[num20] - num4) / (num5 - num4), default(Color)))
				{
					IngameOptions.rightHover = num20;
					if (flag4)
					{
						Player.SmartCursorSettings.SmartAxeAfterPickaxe = !Player.SmartCursorSettings.SmartAxeAfterPickaxe;
					}
				}
				num20++;
			}
			if (IngameOptions.rightHover != -1 && IngameOptions.rightLock == -1)
			{
				IngameOptions.rightLock = IngameOptions.rightHover;
			}
			for (int n = 0; n < num8 + 1; n++)
			{
				UILinkPointNavigator.SetPosition(2900 + n, vector4 + vector5 * (float)(n + 1));
			}
			Vector2 zero = Vector2.Zero;
			if (flag)
			{
				zero.X = -40f;
			}
			for (int num23 = 0; num23 < num11; num23++)
			{
				if (!IngameOptions.skipRightSlot[num23])
				{
					UILinkPointNavigator.SetPosition(2930 + num10, vector6 + zero + vector7 * (float)(num23 + 1));
					num10++;
				}
			}
			UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_RIGHT = num10;
			Main.DrawInterface_29_SettingsButton();
			Main.DrawGamepadInstructions();
			Main.mouseText = false;
			Main.instance.GUIBarsDraw();
			Main.instance.DrawMouseOver();
			Main.DrawCursor(Main.DrawThickCursor(false), false);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0002ACF4 File Offset: 0x00028EF4
		public static void MouseOver()
		{
			if (Main.ingameOptionsWindow)
			{
				if (IngameOptions._GUIHover.Contains(Main.MouseScreen.ToPoint()))
				{
					Main.mouseText = true;
				}
				if (IngameOptions._mouseOverText != null)
				{
					Main.instance.MouseText(IngameOptions._mouseOverText, 0, 0, -1, -1, -1, -1, 0);
				}
				IngameOptions._mouseOverText = null;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0002AD48 File Offset: 0x00028F48
		public static bool DrawLeftSide(SpriteBatch sb, string txt, int i, Vector2 anchor, Vector2 offset, float[] scales, float minscale = 0.7f, float maxscale = 0.8f, float scalespeed = 0.01f)
		{
			bool flag = false;
			int value;
			if (IngameOptions._leftSideCategoryMapping.TryGetValue(i, out value))
			{
				flag = (IngameOptions.category == value);
			}
			Color color = Color.Lerp(Color.Gray, Color.White, (scales[i] - minscale) / (maxscale - minscale));
			if (flag)
			{
				color = Color.Gold;
			}
			Vector2 vector = Utils.DrawBorderStringBig(sb, txt, anchor + offset * (float)(1 + i), color, scales[i], 0.5f, 0.5f, -1);
			bool flag2 = new Rectangle((int)anchor.X - (int)vector.X / 2, (int)anchor.Y + (int)(offset.Y * (float)(1 + i)) - (int)vector.Y / 2, (int)vector.X, (int)vector.Y).Contains(new Point(Main.mouseX, Main.mouseY));
			if (!IngameOptions._canConsumeHover)
			{
				return false;
			}
			if (flag2)
			{
				IngameOptions._canConsumeHover = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0002AE34 File Offset: 0x00029034
		public static bool DrawRightSide(SpriteBatch sb, string txt, int i, Vector2 anchor, Vector2 offset, float scale, float colorScale, Color over = default(Color))
		{
			Color color = Color.Lerp(Color.Gray, Color.White, colorScale);
			if (over != default(Color))
			{
				color = over;
			}
			Vector2 vector = Utils.DrawBorderString(sb, txt, anchor + offset * (float)(1 + i), color, scale, 0.5f, 0.5f, -1);
			IngameOptions.valuePosition = anchor + offset * (float)(1 + i) + vector * new Vector2(0.5f, 0f);
			bool flag = new Rectangle((int)anchor.X - (int)vector.X / 2, (int)anchor.Y + (int)(offset.Y * (float)(1 + i)) - (int)vector.Y / 2, (int)vector.X, (int)vector.Y).Contains(new Point(Main.mouseX, Main.mouseY));
			if (!IngameOptions._canConsumeHover)
			{
				return false;
			}
			if (flag)
			{
				IngameOptions._canConsumeHover = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0002AF30 File Offset: 0x00029130
		public static Rectangle GetExpectedRectangleForNotification(int itemIndex, Vector2 anchor, Vector2 offset, int areaWidth)
		{
			return Utils.CenteredRectangle(anchor + offset * (float)(1 + itemIndex), new Vector2((float)areaWidth, offset.Y - 4f));
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0002AF5C File Offset: 0x0002915C
		public static bool DrawValue(SpriteBatch sb, string txt, int i, float scale, Color over = default(Color))
		{
			Color color = Color.Gray;
			Vector2 vector = FontAssets.MouseText.Value.MeasureString(txt) * scale;
			bool flag = new Rectangle((int)IngameOptions.valuePosition.X, (int)IngameOptions.valuePosition.Y - (int)vector.Y / 2, (int)vector.X, (int)vector.Y).Contains(new Point(Main.mouseX, Main.mouseY));
			if (flag)
			{
				color = Color.White;
			}
			if (over != default(Color))
			{
				color = over;
			}
			Utils.DrawBorderString(sb, txt, IngameOptions.valuePosition, color, scale, 0f, 0.5f, -1);
			IngameOptions.valuePosition.X = IngameOptions.valuePosition.X + vector.X;
			if (!IngameOptions._canConsumeHover)
			{
				return false;
			}
			if (flag)
			{
				IngameOptions._canConsumeHover = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0002B034 File Offset: 0x00029234
		public static float DrawValueBar(SpriteBatch sb, float scale, float perc, int lockState = 0, Utils.ColorLerpMethod colorMethod = null)
		{
			if (colorMethod == null)
			{
				Utils.ColorLerpMethod colorLerpMethod;
				if ((colorLerpMethod = IngameOptions.<>O.<4>__ColorLerp_BlackToWhite) == null)
				{
					colorLerpMethod = (IngameOptions.<>O.<4>__ColorLerp_BlackToWhite = new Utils.ColorLerpMethod(Utils.ColorLerp_BlackToWhite));
				}
				colorMethod = colorLerpMethod;
			}
			Texture2D value = TextureAssets.ColorBar.Value;
			Vector2 vector = new Vector2((float)value.Width, (float)value.Height) * scale;
			IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - (float)((int)vector.X);
			Rectangle rectangle;
			rectangle..ctor((int)IngameOptions.valuePosition.X, (int)IngameOptions.valuePosition.Y - (int)vector.Y / 2, (int)vector.X, (int)vector.Y);
			Rectangle destinationRectangle = rectangle;
			sb.Draw(value, rectangle, Color.White);
			int num = 167;
			float num2 = (float)rectangle.X + 5f * scale;
			float num3 = (float)rectangle.Y + 4f * scale;
			for (float num4 = 0f; num4 < (float)num; num4 += 1f)
			{
				float percent = num4 / (float)num;
				sb.Draw(TextureAssets.ColorBlip.Value, new Vector2(num2 + num4 * scale, num3), null, colorMethod(percent), 0f, Vector2.Zero, scale, 0, 0f);
			}
			rectangle.Inflate((int)(-5f * scale), 0);
			bool flag = rectangle.Contains(new Point(Main.mouseX, Main.mouseY));
			if (lockState == 2)
			{
				flag = false;
			}
			if (flag || lockState == 1)
			{
				sb.Draw(TextureAssets.ColorHighlight.Value, destinationRectangle, Main.OurFavoriteColor);
			}
			sb.Draw(TextureAssets.ColorSlider.Value, new Vector2(num2 + 167f * scale * perc, num3 + 4f * scale), null, Color.White, 0f, new Vector2(0.5f * (float)TextureAssets.ColorSlider.Width(), 0.5f * (float)TextureAssets.ColorSlider.Height()), scale, 0, 0f);
			if (Main.mouseX >= rectangle.X && Main.mouseX <= rectangle.X + rectangle.Width)
			{
				IngameOptions.inBar = flag;
				return (float)(Main.mouseX - rectangle.X) / (float)rectangle.Width;
			}
			IngameOptions.inBar = false;
			if (rectangle.X >= Main.mouseX)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x04000202 RID: 514
		public const int width = 670;

		// Token: 0x04000203 RID: 515
		public const int height = 480;

		// Token: 0x04000204 RID: 516
		public static float[] leftScale = new float[]
		{
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f
		};

		// Token: 0x04000205 RID: 517
		public static float[] rightScale = new float[]
		{
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f,
			0.7f
		};

		// Token: 0x04000206 RID: 518
		private static Dictionary<int, int> _leftSideCategoryMapping = new Dictionary<int, int>
		{
			{
				0,
				0
			},
			{
				1,
				1
			},
			{
				2,
				2
			},
			{
				3,
				3
			}
		};

		// Token: 0x04000207 RID: 519
		public static bool[] skipRightSlot = new bool[20];

		// Token: 0x04000208 RID: 520
		public static int leftHover = -1;

		// Token: 0x04000209 RID: 521
		public static int rightHover = -1;

		// Token: 0x0400020A RID: 522
		public static int oldLeftHover = -1;

		// Token: 0x0400020B RID: 523
		public static int oldRightHover = -1;

		// Token: 0x0400020C RID: 524
		public static int rightLock = -1;

		// Token: 0x0400020D RID: 525
		public static bool inBar;

		// Token: 0x0400020E RID: 526
		public static bool notBar;

		// Token: 0x0400020F RID: 527
		public static bool noSound;

		// Token: 0x04000210 RID: 528
		private static Rectangle _GUIHover;

		// Token: 0x04000211 RID: 529
		public static int category;

		// Token: 0x04000212 RID: 530
		public static Vector2 valuePosition = Vector2.Zero;

		// Token: 0x04000213 RID: 531
		private static string _mouseOverText;

		// Token: 0x04000214 RID: 532
		private static bool _canConsumeHover;

		// Token: 0x02000794 RID: 1940
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040065B3 RID: 26035
			public static Utils.ColorLerpMethod <0>__ColorLerp_HSL_H;

			// Token: 0x040065B4 RID: 26036
			public static Utils.ColorLerpMethod <1>__ColorLerp_HSL_S;

			// Token: 0x040065B5 RID: 26037
			public static Utils.ColorLerpMethod <2>__ColorLerp_HSL_L;

			// Token: 0x040065B6 RID: 26038
			public static Utils.ColorLerpMethod <3>__ColorLerp_HSL_O;

			// Token: 0x040065B7 RID: 26039
			public static Utils.ColorLerpMethod <4>__ColorLerp_BlackToWhite;
		}
	}
}
