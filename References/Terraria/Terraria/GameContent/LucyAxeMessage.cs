using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent
{
	// Token: 0x020001E1 RID: 481
	public static class LucyAxeMessage
	{
		// Token: 0x06001C45 RID: 7237 RVA: 0x004F2B28 File Offset: 0x004F0D28
		private static string GetCategoryName(LucyAxeMessage.MessageSource source)
		{
			switch (source)
			{
			default:
				return "LucyTheAxe_Idle";
			case LucyAxeMessage.MessageSource.Storage:
				return "LucyTheAxe_Storage";
			case LucyAxeMessage.MessageSource.ThrownAway:
				return "LucyTheAxe_ThrownAway";
			case LucyAxeMessage.MessageSource.PickedUp:
				return "LucyTheAxe_PickedUp";
			case LucyAxeMessage.MessageSource.ChoppedTree:
				return "LucyTheAxe_ChoppedTree";
			case LucyAxeMessage.MessageSource.ChoppedGemTree:
				return "LucyTheAxe_GemTree";
			case LucyAxeMessage.MessageSource.ChoppedCactus:
				return "LucyTheAxe_ChoppedCactus";
			}
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x004F2B80 File Offset: 0x004F0D80
		public static void Initialize()
		{
			ItemSlot.OnItemTransferred += LucyAxeMessage.ItemSlot_OnItemTransferred;
			Player.Hooks.OnEnterWorld += LucyAxeMessage.Hooks_OnEnterWorld;
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x004F2BA4 File Offset: 0x004F0DA4
		private static void Hooks_OnEnterWorld(Player player)
		{
			if (player == Main.LocalPlayer)
			{
				LucyAxeMessage.GiveIdleMessageCooldown();
			}
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x004F2BB4 File Offset: 0x004F0DB4
		public static void UpdateMessageCooldowns()
		{
			for (int i = 0; i < LucyAxeMessage._messageCooldownsByType.Length; i++)
			{
				if (LucyAxeMessage._messageCooldownsByType[i] > 0)
				{
					LucyAxeMessage._messageCooldownsByType[i]--;
				}
			}
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x004F2BF0 File Offset: 0x004F0DF0
		public static void TryPlayingIdleMessage()
		{
			LucyAxeMessage.MessageSource messageSource = LucyAxeMessage.MessageSource.Idle;
			if (LucyAxeMessage._messageCooldownsByType[(int)messageSource] > 0)
			{
				return;
			}
			Player localPlayer = Main.LocalPlayer;
			LucyAxeMessage.Create(messageSource, localPlayer.Top, new Vector2(Main.rand.NextFloatDirection() * 7f, -2f + Main.rand.NextFloat() * -2f));
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x004F2C48 File Offset: 0x004F0E48
		private static void ItemSlot_OnItemTransferred(ItemSlot.ItemTransferInfo info)
		{
			if (info.ItemType != 5095)
			{
				return;
			}
			bool flag = LucyAxeMessage.CountsAsStorage(info.FromContenxt);
			bool flag2 = LucyAxeMessage.CountsAsStorage(info.ToContext);
			if (flag == flag2)
			{
				return;
			}
			LucyAxeMessage.MessageSource messageSource = flag ? LucyAxeMessage.MessageSource.PickedUp : LucyAxeMessage.MessageSource.Storage;
			if (LucyAxeMessage._messageCooldownsByType[(int)messageSource] > 0)
			{
				return;
			}
			LucyAxeMessage.PutMessageTypeOnCooldown(messageSource, 420);
			Player localPlayer = Main.LocalPlayer;
			LucyAxeMessage.Create(messageSource, localPlayer.Top, new Vector2((float)(localPlayer.direction * 7), -2f));
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x004F2CC3 File Offset: 0x004F0EC3
		private static void GiveIdleMessageCooldown()
		{
			LucyAxeMessage.PutMessageTypeOnCooldown(LucyAxeMessage.MessageSource.Idle, Main.rand.Next(7200, 14400));
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x004F2CDF File Offset: 0x004F0EDF
		public static void PutMessageTypeOnCooldown(LucyAxeMessage.MessageSource source, int timeInFrames)
		{
			LucyAxeMessage._messageCooldownsByType[(int)source] = timeInFrames;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x004F2CE9 File Offset: 0x004F0EE9
		private static bool CountsAsStorage(int itemSlotContext)
		{
			return itemSlotContext == 3 || itemSlotContext == 6 || itemSlotContext == 15;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x004F2CFB File Offset: 0x004F0EFB
		public static void TryCreatingMessageWithCooldown(LucyAxeMessage.MessageSource messageSource, Vector2 position, Vector2 velocity, int cooldownTimeInTicks)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			if (LucyAxeMessage._messageCooldownsByType[(int)messageSource] > 0)
			{
				return;
			}
			LucyAxeMessage.PutMessageTypeOnCooldown(messageSource, cooldownTimeInTicks);
			LucyAxeMessage.Create(messageSource, position, velocity);
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x004F2D20 File Offset: 0x004F0F20
		public static void Create(LucyAxeMessage.MessageSource source, Vector2 position, Vector2 velocity)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			LucyAxeMessage.GiveIdleMessageCooldown();
			LucyAxeMessage.SpawnPopupText(source, (int)LucyAxeMessage._variation, position, velocity);
			LucyAxeMessage.PlaySound(source, position);
			LucyAxeMessage.SpawnEmoteBubble();
			if (Main.netMode == 1)
			{
				NetMessage.SendData(141, -1, -1, null, (int)source, (float)LucyAxeMessage._variation, velocity.X, velocity.Y, (int)position.X, (int)position.Y, 0);
			}
			LucyAxeMessage._variation += 1;
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x004F2D98 File Offset: 0x004F0F98
		private static void SpawnEmoteBubble()
		{
			EmoteBubble.MakeLocalPlayerEmote(149);
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x004F2DA4 File Offset: 0x004F0FA4
		public static void CreateFromNet(LucyAxeMessage.MessageSource source, byte variation, Vector2 position, Vector2 velocity)
		{
			LucyAxeMessage.SpawnPopupText(source, (int)variation, position, velocity);
			LucyAxeMessage.PlaySound(source, position);
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x004F2DB6 File Offset: 0x004F0FB6
		private static void PlaySound(LucyAxeMessage.MessageSource source, Vector2 position)
		{
			SoundEngine.PlaySound(SoundID.LucyTheAxeTalk, position);
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x004F2DC4 File Offset: 0x004F0FC4
		private static void SpawnPopupText(LucyAxeMessage.MessageSource source, int variationUnwrapped, Vector2 position, Vector2 velocity)
		{
			string textForVariation = LucyAxeMessage.GetTextForVariation(source, variationUnwrapped);
			PopupText.NewText(new AdvancedPopupRequest
			{
				Text = textForVariation,
				DurationInFrames = 420,
				Velocity = velocity,
				Color = new Color(184, 96, 98) * 1.15f
			}, position);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x004F2E24 File Offset: 0x004F1024
		private static string GetTextForVariation(LucyAxeMessage.MessageSource source, int variationUnwrapped)
		{
			string categoryName = LucyAxeMessage.GetCategoryName(source);
			return LanguageManager.Instance.IndexedFromCategory(categoryName, variationUnwrapped).Value;
		}

		// Token: 0x04004399 RID: 17305
		private static byte _variation;

		// Token: 0x0400439A RID: 17306
		private static int[] _messageCooldownsByType = new int[7];

		// Token: 0x02000604 RID: 1540
		public enum MessageSource
		{
			// Token: 0x04006028 RID: 24616
			Idle,
			// Token: 0x04006029 RID: 24617
			Storage,
			// Token: 0x0400602A RID: 24618
			ThrownAway,
			// Token: 0x0400602B RID: 24619
			PickedUp,
			// Token: 0x0400602C RID: 24620
			ChoppedTree,
			// Token: 0x0400602D RID: 24621
			ChoppedGemTree,
			// Token: 0x0400602E RID: 24622
			ChoppedCactus,
			// Token: 0x0400602F RID: 24623
			Count
		}
	}
}
