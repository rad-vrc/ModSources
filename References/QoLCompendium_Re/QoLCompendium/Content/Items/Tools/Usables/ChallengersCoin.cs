using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x0200017B RID: 379
	public class ChallengersCoin : ModItem
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x00014D80 File Offset: 0x00012F80
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.ChallengersCoin;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00014D9C File Offset: 0x00012F9C
		public override void SetDefaults()
		{
			base.Item.width = 32;
			base.Item.height = 32;
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.useStyle = 4;
			base.Item.UseSound = new SoundStyle?(SoundID.Item4);
			base.Item.consumable = false;
			base.Item.SetShopValues(ItemRarityColor.TrashMinus1, Item.buyPrice(0, 2, 0, 0));
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00014E1F File Offset: 0x0001301F
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.ChallengersCoin);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00002430 File Offset: 0x00000630
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00014E38 File Offset: 0x00013038
		public override bool CanUseItem(Player player)
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].active && Main.npc[i].boss)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00014E74 File Offset: 0x00013074
		public override bool? UseItem(Player player)
		{
			string text;
			if (player.altFunctionUse == 2)
			{
				if (Main.GameMode == 3)
				{
					Main.GameMode = 1;
					ChallengersCoin.ChangeAllPlayerDifficulty(0);
					text = Language.GetTextValue("Mods.QoLCompendium.Messages.WorldExpert");
				}
				else
				{
					Main.GameMode = 3;
					ChallengersCoin.ChangeAllPlayerDifficulty(3);
					text = Language.GetTextValue("Mods.QoLCompendium.Messages.WorldJourney");
				}
			}
			else
			{
				int gameMode = Main.GameMode;
				if (gameMode != 0)
				{
					if (gameMode != 1)
					{
						Main.GameMode = 0;
						ChallengersCoin.ChangeAllPlayerDifficulty(0);
						text = Language.GetTextValue("Mods.QoLCompendium.Messages.WorldNormal");
					}
					else
					{
						Main.GameMode = 2;
						ChallengersCoin.ChangeAllPlayerDifficulty(0);
						text = Language.GetTextValue("Mods.QoLCompendium.Messages.WorldMaster");
					}
				}
				else
				{
					Main.GameMode = 1;
					ChallengersCoin.ChangeAllPlayerDifficulty(0);
					text = Language.GetTextValue("Mods.QoLCompendium.Messages.WorldExpert");
				}
			}
			if (Main.netMode == 0)
			{
				Main.NewText(text, new Color?(new Color(175, 75, 255)));
			}
			else if (Main.netMode == 2)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), new Color(175, 75, 255), -1);
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.Center), null);
			return new bool?(true);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00014FA0 File Offset: 0x000131A0
		private static void ChangeAllPlayerDifficulty(byte diff)
		{
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active)
				{
					player.difficulty = diff;
					NetMessage.SendData(4, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00014FF8 File Offset: 0x000131F8
		public override void UpdateInventory(Player player)
		{
			if (Main.GameMode == 0)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.ChallengersCoin.Normal"));
			}
			if (Main.GameMode == 1)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.ChallengersCoin.Expert"));
			}
			if (Main.GameMode == 2)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.ChallengersCoin.Master"));
			}
			if (Main.GameMode == 3)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.ChallengersCoin.Journey"));
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00015078 File Offset: 0x00013278
		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(43, 1);
			defaultInterpolatedStringHandler.AppendLiteral("QoLCompendium/Assets/Items/ChallengersCoin_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(Main.GameMode);
			Texture2D coin = (!0)ModContent.Request<Texture2D>(defaultInterpolatedStringHandler.ToStringAndClear(), 2);
			spriteBatch.Draw(coin, position, new Rectangle?(frame), drawColor, 0f, origin, scale, 0, 0f);
			return false;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000150DC File Offset: 0x000132DC
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(43, 1);
			defaultInterpolatedStringHandler.AppendLiteral("QoLCompendium/Assets/Items/ChallengersCoin_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(Main.GameMode);
			Texture2D coin = (!0)ModContent.Request<Texture2D>(defaultInterpolatedStringHandler.ToStringAndClear(), 2);
			Vector2 position = base.Item.position - Main.screenPosition + new Vector2(16f, 16f);
			Rectangle hitbox;
			hitbox..ctor(0, 0, 32, 32);
			spriteBatch.Draw(coin, position, new Rectangle?(hitbox), lightColor, rotation, new Vector2(16f, 16f), scale, 0, 0f);
			return false;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00015180 File Offset: 0x00013380
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.ChallengersCoin, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 16);
			itemRecipe.AddIngredient(73, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
