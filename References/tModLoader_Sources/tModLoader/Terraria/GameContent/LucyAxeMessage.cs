using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent
{
	// Token: 0x020004A4 RID: 1188
	public static class LucyAxeMessage
	{
		// Token: 0x06003955 RID: 14677 RVA: 0x00597A6C File Offset: 0x00595C6C
		private static string GetCategoryName(LucyAxeMessage.MessageSource source)
		{
			switch (source)
			{
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
			default:
				return "LucyTheAxe_Idle";
			}
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x00597AC4 File Offset: 0x00595CC4
		public static void Initialize()
		{
			ItemSlot.ItemTransferEvent value;
			if ((value = LucyAxeMessage.<>O.<0>__ItemSlot_OnItemTransferred) == null)
			{
				value = (LucyAxeMessage.<>O.<0>__ItemSlot_OnItemTransferred = new ItemSlot.ItemTransferEvent(LucyAxeMessage.ItemSlot_OnItemTransferred));
			}
			ItemSlot.OnItemTransferred += value;
			Action<Player> value2;
			if ((value2 = LucyAxeMessage.<>O.<1>__Hooks_OnEnterWorld) == null)
			{
				value2 = (LucyAxeMessage.<>O.<1>__Hooks_OnEnterWorld = new Action<Player>(LucyAxeMessage.Hooks_OnEnterWorld));
			}
			Player.Hooks.OnEnterWorld += value2;
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x00597B11 File Offset: 0x00595D11
		private static void Hooks_OnEnterWorld(Player player)
		{
			if (player == Main.LocalPlayer)
			{
				LucyAxeMessage.GiveIdleMessageCooldown();
			}
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x00597B20 File Offset: 0x00595D20
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

		// Token: 0x06003959 RID: 14681 RVA: 0x00597B5C File Offset: 0x00595D5C
		public static void TryPlayingIdleMessage()
		{
			LucyAxeMessage.MessageSource messageSource = LucyAxeMessage.MessageSource.Idle;
			if (LucyAxeMessage._messageCooldownsByType[(int)messageSource] <= 0)
			{
				Player localPlayer = Main.LocalPlayer;
				LucyAxeMessage.Create(messageSource, localPlayer.Top, new Vector2(Main.rand.NextFloatDirection() * 7f, -2f + Main.rand.NextFloat() * -2f));
			}
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x00597BB4 File Offset: 0x00595DB4
		private static void ItemSlot_OnItemTransferred(ItemSlot.ItemTransferInfo info)
		{
			if (info.ItemType != 5095)
			{
				return;
			}
			bool flag = LucyAxeMessage.CountsAsStorage(info.FromContenxt);
			bool flag2 = LucyAxeMessage.CountsAsStorage(info.ToContext);
			if (flag != flag2)
			{
				LucyAxeMessage.MessageSource messageSource = (!flag) ? LucyAxeMessage.MessageSource.Storage : LucyAxeMessage.MessageSource.PickedUp;
				if (LucyAxeMessage._messageCooldownsByType[(int)messageSource] <= 0)
				{
					LucyAxeMessage.PutMessageTypeOnCooldown(messageSource, 420);
					Player localPlayer = Main.LocalPlayer;
					LucyAxeMessage.Create(messageSource, localPlayer.Top, new Vector2((float)(localPlayer.direction * 7), -2f));
				}
			}
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x00597C2D File Offset: 0x00595E2D
		private static void GiveIdleMessageCooldown()
		{
			LucyAxeMessage.PutMessageTypeOnCooldown(LucyAxeMessage.MessageSource.Idle, Main.rand.Next(7200, 14400));
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x00597C49 File Offset: 0x00595E49
		public static void PutMessageTypeOnCooldown(LucyAxeMessage.MessageSource source, int timeInFrames)
		{
			LucyAxeMessage._messageCooldownsByType[(int)source] = timeInFrames;
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x00597C53 File Offset: 0x00595E53
		private static bool CountsAsStorage(int itemSlotContext)
		{
			return itemSlotContext == 3 || itemSlotContext == 6 || itemSlotContext == 15;
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x00597C65 File Offset: 0x00595E65
		public static void TryCreatingMessageWithCooldown(LucyAxeMessage.MessageSource messageSource, Vector2 position, Vector2 velocity, int cooldownTimeInTicks)
		{
			if (Main.netMode != 2 && LucyAxeMessage._messageCooldownsByType[(int)messageSource] <= 0)
			{
				LucyAxeMessage.PutMessageTypeOnCooldown(messageSource, cooldownTimeInTicks);
				LucyAxeMessage.Create(messageSource, position, velocity);
			}
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x00597C88 File Offset: 0x00595E88
		public static void Create(LucyAxeMessage.MessageSource source, Vector2 position, Vector2 velocity)
		{
			if (Main.netMode != 2)
			{
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
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x00597CFF File Offset: 0x00595EFF
		private static void SpawnEmoteBubble()
		{
			EmoteBubble.MakeLocalPlayerEmote(149);
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x00597D0B File Offset: 0x00595F0B
		public static void CreateFromNet(LucyAxeMessage.MessageSource source, byte variation, Vector2 position, Vector2 velocity)
		{
			LucyAxeMessage.SpawnPopupText(source, (int)variation, position, velocity);
			LucyAxeMessage.PlaySound(source, position);
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x00597D1D File Offset: 0x00595F1D
		private static void PlaySound(LucyAxeMessage.MessageSource source, Vector2 position)
		{
			SoundEngine.PlaySound(SoundID.LucyTheAxeTalk, new Vector2?(position), null);
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x00597D34 File Offset: 0x00595F34
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

		// Token: 0x06003964 RID: 14692 RVA: 0x00597D94 File Offset: 0x00595F94
		private static string GetTextForVariation(LucyAxeMessage.MessageSource source, int variationUnwrapped)
		{
			string categoryName = LucyAxeMessage.GetCategoryName(source);
			return LanguageManager.Instance.IndexedFromCategory(categoryName, variationUnwrapped).Value;
		}

		// Token: 0x04005259 RID: 21081
		private static byte _variation;

		// Token: 0x0400525A RID: 21082
		private static int[] _messageCooldownsByType = new int[7];

		// Token: 0x02000BAE RID: 2990
		public enum MessageSource
		{
			// Token: 0x040076C4 RID: 30404
			Idle,
			// Token: 0x040076C5 RID: 30405
			Storage,
			// Token: 0x040076C6 RID: 30406
			ThrownAway,
			// Token: 0x040076C7 RID: 30407
			PickedUp,
			// Token: 0x040076C8 RID: 30408
			ChoppedTree,
			// Token: 0x040076C9 RID: 30409
			ChoppedGemTree,
			// Token: 0x040076CA RID: 30410
			ChoppedCactus,
			// Token: 0x040076CB RID: 30411
			Count
		}

		// Token: 0x02000BAF RID: 2991
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040076CC RID: 30412
			public static ItemSlot.ItemTransferEvent <0>__ItemSlot_OnItemTransferred;

			// Token: 0x040076CD RID: 30413
			public static Action<Player> <1>__Hooks_OnEnterWorld;
		}
	}
}
