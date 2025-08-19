using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Default;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This is where all ModPlayer hooks are gathered and called.
	/// </summary>
	// Token: 0x020001E9 RID: 489
	public static class PlayerLoader
	{
		// Token: 0x060025FA RID: 9722 RVA: 0x004F6FF4 File Offset: 0x004F51F4
		private static HookList<ModPlayer> AddHook<F>(Expression<Func<ModPlayer, F>> func) where F : Delegate
		{
			HookList<ModPlayer> hook = HookList<ModPlayer>.Create<F>(func);
			PlayerLoader.hooks.Add(hook);
			return hook;
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x004F7014 File Offset: 0x004F5214
		public static T AddModHook<T>(T hook) where T : HookList<ModPlayer>
		{
			hook.Update(PlayerLoader.players);
			PlayerLoader.modHooks.Add(hook);
			return hook;
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x004F7037 File Offset: 0x004F5237
		internal static void Add(ModPlayer player)
		{
			player.Index = (ushort)PlayerLoader.players.Count;
			PlayerLoader.players.Add(player);
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x004F7058 File Offset: 0x004F5258
		internal static void ResizeArrays()
		{
			foreach (HookList<ModPlayer> hookList in PlayerLoader.hooks.Union(PlayerLoader.modHooks))
			{
				hookList.Update(PlayerLoader.players);
			}
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x004F70B0 File Offset: 0x004F52B0
		internal static void Unload()
		{
			PlayerLoader.players.Clear();
			PlayerLoader.modHooks.Clear();
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x004F70C8 File Offset: 0x004F52C8
		internal static void SetupPlayer(Player player)
		{
			player.modPlayers = PlayerLoader.NewInstances(player, CollectionsMarshal.AsSpan<ModPlayer>(PlayerLoader.players));
			foreach (ModPlayer modPlayer in PlayerLoader.HookInitialize.Enumerate(player))
			{
				modPlayer.Initialize();
			}
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x004F7118 File Offset: 0x004F5318
		private unsafe static ModPlayer[] NewInstances(Player player, Span<ModPlayer> modPlayers)
		{
			ModPlayer[] arr = new ModPlayer[modPlayers.Length];
			for (int i = 0; i < modPlayers.Length; i++)
			{
				arr[i] = modPlayers[i]->NewInstance(player);
			}
			return arr;
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x004F7158 File Offset: 0x004F5358
		public static void ResetEffects(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookResetEffects.Enumerate(player))
			{
				modPlayer.ResetEffects();
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x004F7190 File Offset: 0x004F5390
		public static void ResetInfoAccessories(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookResetInfoAccessories.Enumerate(player))
			{
				modPlayer.ResetInfoAccessories();
			}
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x004F71C8 File Offset: 0x004F53C8
		public static void RefreshInfoAccessoriesFromTeamPlayers(Player player, Player otherPlayer)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookRefreshInfoAccessoriesFromTeamPlayers.Enumerate(player))
			{
				modPlayer.RefreshInfoAccessoriesFromTeamPlayers(otherPlayer);
			}
		}

		/// <summary>
		/// Resets <see cref="F:Terraria.Player.statLifeMax" /> and <see cref="F:Terraria.Player.statManaMax" /> to their expected values by vanilla
		/// </summary>
		/// <param name="player"></param>
		// Token: 0x06002604 RID: 9732 RVA: 0x004F7201 File Offset: 0x004F5401
		public static void ResetMaxStatsToVanilla(Player player)
		{
			player.statLifeMax = 100 + player.ConsumedLifeCrystals * 20 + player.ConsumedLifeFruit * 5;
			player.statManaMax = 20 + player.ConsumedManaCrystals * 20;
		}

		/// <summary>
		/// Reset this player's <see cref="F:Terraria.Player.statLifeMax" /> and <see cref="F:Terraria.Player.statManaMax" /> to their vanilla defaults,
		/// applies <see cref="M:Terraria.ModLoader.ModPlayer.ModifyMaxStats(Terraria.ModLoader.StatModifier@,Terraria.ModLoader.StatModifier@)" /> to them,
		/// then modifies <see cref="F:Terraria.Player.statLifeMax2" /> and <see cref="F:Terraria.Player.statManaMax2" />
		/// </summary>
		/// <param name="player"></param>
		// Token: 0x06002605 RID: 9733 RVA: 0x004F7230 File Offset: 0x004F5430
		public static void ModifyMaxStats(Player player)
		{
			PlayerLoader.ResetMaxStatsToVanilla(player);
			StatModifier cumulativeHealth = StatModifier.Default;
			StatModifier cumulativeMana = StatModifier.Default;
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyMaxStats.Enumerate(player))
			{
				StatModifier health;
				StatModifier mana;
				modPlayer.ModifyMaxStats(out health, out mana);
				cumulativeHealth = cumulativeHealth.CombineWith(health);
				cumulativeMana = cumulativeMana.CombineWith(mana);
			}
			player.statLifeMax = (int)cumulativeHealth.ApplyTo((float)player.statLifeMax);
			player.statManaMax = (int)cumulativeMana.ApplyTo((float)player.statManaMax);
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x004F72BC File Offset: 0x004F54BC
		public static void UpdateDead(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookUpdateDead.Enumerate(player))
			{
				try
				{
					modPlayer.UpdateDead();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x004F730C File Offset: 0x004F550C
		public static void SetStartInventory(Player player, IList<Item> items)
		{
			if (items.Count <= 50)
			{
				for (int i = 0; i < 50; i++)
				{
					if (i < items.Count)
					{
						player.inventory[i] = items[i];
					}
					else
					{
						player.inventory[i].SetDefaults(0);
					}
				}
				return;
			}
			for (int j = 0; j < 49; j++)
			{
				player.inventory[j] = items[j];
			}
			Item bag = new Item();
			bag.SetDefaults(ModContent.ItemType<StartBag>());
			for (int k = 49; k < items.Count; k++)
			{
				((StartBag)bag.ModItem).AddItem(items[k]);
			}
			player.inventory[49] = bag;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x004F73BC File Offset: 0x004F55BC
		public static void PreSavePlayer(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPreSavePlayer.Enumerate(player))
			{
				modPlayer.PreSavePlayer();
			}
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x004F73F4 File Offset: 0x004F55F4
		public static void PostSavePlayer(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPostSavePlayer.Enumerate(player))
			{
				modPlayer.PostSavePlayer();
			}
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x004F742C File Offset: 0x004F562C
		public static void CopyClientState(Player player, Player targetCopy)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookCopyClientState.Enumerate(player))
			{
				try
				{
					modPlayer.CopyClientState(targetCopy.modPlayers[(int)modPlayer.Index]);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x004F7488 File Offset: 0x004F5688
		public static void SyncPlayer(Player player, int toWho, int fromWho, bool newPlayer)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookSyncPlayer.Enumerate(player))
			{
				try
				{
					modPlayer.SyncPlayer(toWho, fromWho, newPlayer);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x004F74DC File Offset: 0x004F56DC
		public static void SendClientChanges(Player player, Player clientPlayer)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookSendClientChanges.Enumerate(player))
			{
				try
				{
					modPlayer.SendClientChanges(clientPlayer.modPlayers[(int)modPlayer.Index]);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x004F7538 File Offset: 0x004F5738
		public static void UpdateBadLifeRegen(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookUpdateBadLifeRegen.Enumerate(player))
			{
				try
				{
					modPlayer.UpdateBadLifeRegen();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x004F7588 File Offset: 0x004F5788
		public static void UpdateLifeRegen(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookUpdateLifeRegen.Enumerate(player))
			{
				try
				{
					modPlayer.UpdateLifeRegen();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x004F75D8 File Offset: 0x004F57D8
		public static void NaturalLifeRegen(Player player, ref float regen)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookNaturalLifeRegen.Enumerate(player))
			{
				try
				{
					modPlayer.NaturalLifeRegen(ref regen);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x004F7628 File Offset: 0x004F5828
		public static void UpdateAutopause(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookUpdateAutopause.Enumerate(player))
			{
				modPlayer.UpdateAutopause();
			}
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x004F7660 File Offset: 0x004F5860
		public static void PreUpdate(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPreUpdate.Enumerate(player))
			{
				try
				{
					modPlayer.PreUpdate();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x004F76B0 File Offset: 0x004F58B0
		public static void SetControls(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookSetControls.Enumerate(player))
			{
				try
				{
					modPlayer.SetControls();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x004F7700 File Offset: 0x004F5900
		public static void PreUpdateBuffs(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPreUpdateBuffs.Enumerate(player))
			{
				try
				{
					modPlayer.PreUpdateBuffs();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x004F7750 File Offset: 0x004F5950
		public static void PostUpdateBuffs(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPostUpdateBuffs.Enumerate(player))
			{
				try
				{
					modPlayer.PostUpdateBuffs();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x004F77A0 File Offset: 0x004F59A0
		public static void UpdateEquips(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookUpdateEquips.Enumerate(player))
			{
				try
				{
					modPlayer.UpdateEquips();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x004F77F0 File Offset: 0x004F59F0
		public static void PostUpdateEquips(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPostUpdateEquips.Enumerate(player))
			{
				try
				{
					modPlayer.PostUpdateEquips();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x004F7840 File Offset: 0x004F5A40
		public static void UpdateVisibleAccessories(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookUpdateVisibleAccessories.Enumerate(player))
			{
				try
				{
					modPlayer.UpdateVisibleAccessories();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x004F7890 File Offset: 0x004F5A90
		public static void UpdateVisibleVanityAccessories(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookUpdateVisibleVanityAccessories.Enumerate(player))
			{
				try
				{
					modPlayer.UpdateVisibleVanityAccessories();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x004F78E0 File Offset: 0x004F5AE0
		public static void UpdateDyes(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookUpdateDyes.Enumerate(player))
			{
				try
				{
					modPlayer.UpdateDyes();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x004F7930 File Offset: 0x004F5B30
		public static void PostUpdateMiscEffects(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPostUpdateMiscEffects.Enumerate(player))
			{
				try
				{
					modPlayer.PostUpdateMiscEffects();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x004F7980 File Offset: 0x004F5B80
		public static void PostUpdateRunSpeeds(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPostUpdateRunSpeeds.Enumerate(player))
			{
				try
				{
					modPlayer.PostUpdateRunSpeeds();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x004F79D0 File Offset: 0x004F5BD0
		public static void PreUpdateMovement(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPreUpdateMovement.Enumerate(player))
			{
				try
				{
					modPlayer.PreUpdateMovement();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x004F7A20 File Offset: 0x004F5C20
		public static void PostUpdate(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPostUpdate.Enumerate(player))
			{
				try
				{
					modPlayer.PostUpdate();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x004F7A70 File Offset: 0x004F5C70
		public static void ModifyExtraJumpDurationMultiplier(ExtraJump jump, Player player, ref float duration)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyExtraJumpDurationMultiplier.Enumerate(player))
			{
				try
				{
					modPlayer.ModifyExtraJumpDurationMultiplier(jump, ref duration);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x004F7AC4 File Offset: 0x004F5CC4
		public static bool CanStartExtraJump(ExtraJump jump, Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookCanStartExtraJump.Enumerate(player))
			{
				try
				{
					if (!modPlayer.CanStartExtraJump(jump))
					{
						return false;
					}
				}
				catch
				{
				}
			}
			return true;
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x004F7B20 File Offset: 0x004F5D20
		public static void OnExtraJumpStarted(ExtraJump jump, Player player, ref bool playSound)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnExtraJumpStarted.Enumerate(player))
			{
				try
				{
					modPlayer.OnExtraJumpStarted(jump, ref playSound);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x004F7B74 File Offset: 0x004F5D74
		public static void OnExtraJumpEnded(ExtraJump jump, Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnExtraJumpEnded.Enumerate(player))
			{
				try
				{
					modPlayer.OnExtraJumpEnded(jump);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x004F7BC4 File Offset: 0x004F5DC4
		public static void OnExtraJumpRefreshed(ExtraJump jump, Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnExtraJumpRefreshed.Enumerate(player))
			{
				try
				{
					modPlayer.OnExtraJumpRefreshed(jump);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x004F7C14 File Offset: 0x004F5E14
		public static void ExtraJumpVisuals(ExtraJump jump, Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookExtraJumpVisuals.Enumerate(player))
			{
				try
				{
					modPlayer.ExtraJumpVisuals(jump);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x004F7C64 File Offset: 0x004F5E64
		public static bool CanShowExtraJumpVisuals(ExtraJump jump, Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookCanShowExtraJumpVisuals.Enumerate(player))
			{
				try
				{
					if (!modPlayer.CanShowExtraJumpVisuals(jump))
					{
						return false;
					}
				}
				catch
				{
				}
			}
			return true;
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x004F7CC0 File Offset: 0x004F5EC0
		public static void FrameEffects(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookFrameEffects.Enumerate(player))
			{
				try
				{
					modPlayer.FrameEffects();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x004F7D10 File Offset: 0x004F5F10
		public static bool ImmuneTo(Player player, PlayerDeathReason damageSource, int cooldownCounter, bool dodgeable)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookImmuneTo.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ImmuneTo(damageSource, cooldownCounter, dodgeable))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x004F7D50 File Offset: 0x004F5F50
		public static bool FreeDodge(Player player, in Player.HurtInfo info)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookFreeDodge.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.FreeDodge(info))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x004F7D94 File Offset: 0x004F5F94
		public static bool ConsumableDodge(Player player, in Player.HurtInfo info)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookConsumableDodge.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ConsumableDodge(info))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x004F7DD8 File Offset: 0x004F5FD8
		public unsafe static void ModifyHurt(Player player, ref Player.HurtModifiers modifiers)
		{
			Entity sourceEntity;
			if (modifiers.DamageSource.TryGetCausingEntity(out sourceEntity))
			{
				Entity entity = sourceEntity;
				Projectile proj = entity as Projectile;
				if (proj == null)
				{
					NPC npc = entity as NPC;
					if (npc == null)
					{
						Player sourcePlayer = entity as Player;
						if (sourcePlayer != null)
						{
							Item item = *modifiers.DamageSource.SourceItem;
							if (item != null && modifiers.PvP)
							{
								ItemLoader.ModifyHitPvp(item, sourcePlayer, player, ref modifiers);
							}
						}
					}
					else
					{
						CombinedHooks.ModifyHitByNPC(player, npc, ref modifiers);
					}
				}
				else
				{
					CombinedHooks.ModifyHitByProjectile(player, proj, ref modifiers);
				}
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyHurt.Enumerate(player))
			{
				try
				{
					modPlayer.ModifyHurt(ref modifiers);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x004F7E9C File Offset: 0x004F609C
		public unsafe static void OnHurt(Player player, Player.HurtInfo info)
		{
			Entity sourceEntity;
			if (info.DamageSource.TryGetCausingEntity(out sourceEntity))
			{
				Entity entity = sourceEntity;
				Projectile proj = entity as Projectile;
				if (proj == null)
				{
					NPC npc = entity as NPC;
					if (npc == null)
					{
						Player sourcePlayer = entity as Player;
						if (sourcePlayer != null)
						{
							Item item = *info.DamageSource.SourceItem;
							if (item != null && info.PvP)
							{
								ItemLoader.OnHitPvp(item, sourcePlayer, player, info);
							}
						}
					}
					else if (player == Main.LocalPlayer)
					{
						CombinedHooks.OnHitByNPC(player, npc, info);
					}
				}
				else if (player == Main.LocalPlayer)
				{
					CombinedHooks.OnHitByProjectile(player, proj, info);
				}
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookHurt.Enumerate(player))
			{
				modPlayer.OnHurt(info);
			}
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x004F7F58 File Offset: 0x004F6158
		public static void PostHurt(Player player, Player.HurtInfo info)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPostHurt.Enumerate(player))
			{
				modPlayer.PostHurt(info);
			}
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x004F7F94 File Offset: 0x004F6194
		public static bool PreKill(Player player, double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			bool ret = true;
			foreach (ModPlayer modPlayer in PlayerLoader.HookPreKill.Enumerate(player))
			{
				ret &= modPlayer.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
			}
			return ret;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x004F7FE0 File Offset: 0x004F61E0
		public static void Kill(Player player, double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookKill.Enumerate(player))
			{
				try
				{
					modPlayer.Kill(damage, hitDirection, pvp, damageSource);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x004F8034 File Offset: 0x004F6234
		public static bool PreModifyLuck(Player player, ref float luck)
		{
			bool ret = true;
			foreach (ModPlayer modPlayer in PlayerLoader.HookPreModifyLuck.Enumerate(player))
			{
				ret &= modPlayer.PreModifyLuck(ref luck);
			}
			return ret;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x004F8078 File Offset: 0x004F6278
		public static void ModifyLuck(Player player, ref float luck)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyLuck.Enumerate(player))
			{
				modPlayer.ModifyLuck(ref luck);
			}
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x004F80B4 File Offset: 0x004F62B4
		public static bool PreItemCheck(Player player)
		{
			bool ret = true;
			foreach (ModPlayer modPlayer in PlayerLoader.HookPreItemCheck.Enumerate(player))
			{
				try
				{
					ret &= modPlayer.PreItemCheck();
				}
				catch
				{
				}
			}
			return ret;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x004F810C File Offset: 0x004F630C
		public static void PostItemCheck(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPostItemCheck.Enumerate(player))
			{
				try
				{
					modPlayer.PostItemCheck();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x004F815C File Offset: 0x004F635C
		public static float UseTimeMultiplier(Player player, Item item)
		{
			float multiplier = 1f;
			if (item.IsAir)
			{
				return multiplier;
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookUseTimeMultiplier.Enumerate(player))
			{
				multiplier *= modPlayer.UseTimeMultiplier(item);
			}
			return multiplier;
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x004F81AC File Offset: 0x004F63AC
		public static float UseAnimationMultiplier(Player player, Item item)
		{
			float multiplier = 1f;
			if (item.IsAir)
			{
				return multiplier;
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookUseAnimationMultiplier.Enumerate(player))
			{
				multiplier *= modPlayer.UseAnimationMultiplier(item);
			}
			return multiplier;
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x004F81FC File Offset: 0x004F63FC
		public static float UseSpeedMultiplier(Player player, Item item)
		{
			float multiplier = 1f;
			if (item.IsAir)
			{
				return multiplier;
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookUseSpeedMultiplier.Enumerate(player))
			{
				multiplier *= modPlayer.UseSpeedMultiplier(item);
			}
			return multiplier;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x004F824C File Offset: 0x004F644C
		public static void GetHealLife(Player player, Item item, bool quickHeal, ref int healValue)
		{
			if (item.IsAir)
			{
				return;
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookGetHealLife.Enumerate(player))
			{
				modPlayer.GetHealLife(item, quickHeal, ref healValue);
			}
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x004F8290 File Offset: 0x004F6490
		public static void GetHealMana(Player player, Item item, bool quickHeal, ref int healValue)
		{
			if (item.IsAir)
			{
				return;
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookGetHealMana.Enumerate(player))
			{
				modPlayer.GetHealMana(item, quickHeal, ref healValue);
			}
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x004F82D4 File Offset: 0x004F64D4
		public static void ModifyManaCost(Player player, Item item, ref float reduce, ref float mult)
		{
			if (item.IsAir)
			{
				return;
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyManaCost.Enumerate(player))
			{
				modPlayer.ModifyManaCost(item, ref reduce, ref mult);
			}
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x004F8318 File Offset: 0x004F6518
		public static void OnMissingMana(Player player, Item item, int manaNeeded)
		{
			if (item.IsAir)
			{
				return;
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnMissingMana.Enumerate(player))
			{
				modPlayer.OnMissingMana(item, manaNeeded);
			}
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x004F835C File Offset: 0x004F655C
		public static void OnConsumeMana(Player player, Item item, int manaConsumed)
		{
			if (item.IsAir)
			{
				return;
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnConsumeMana.Enumerate(player))
			{
				modPlayer.OnConsumeMana(item, manaConsumed);
			}
		}

		/// <summary>
		/// Calls ModItem.HookModifyWeaponDamage, then all GlobalItem.HookModifyWeaponDamage hooks.
		/// </summary>
		// Token: 0x0600263A RID: 9786 RVA: 0x004F83A0 File Offset: 0x004F65A0
		public static void ModifyWeaponDamage(Player player, Item item, ref StatModifier damage)
		{
			if (item.IsAir)
			{
				return;
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyWeaponDamage.Enumerate(player))
			{
				modPlayer.ModifyWeaponDamage(item, ref damage);
			}
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x004F83E4 File Offset: 0x004F65E4
		public static void ProcessTriggers(Player player, TriggersSet triggersSet)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookProcessTriggers.Enumerate(player))
			{
				try
				{
					modPlayer.ProcessTriggers(triggersSet);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x004F8434 File Offset: 0x004F6634
		public static void ModifyWeaponKnockback(Player player, Item item, ref StatModifier knockback)
		{
			if (item.IsAir)
			{
				return;
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyWeaponKnockback.Enumerate(player))
			{
				modPlayer.ModifyWeaponKnockback(item, ref knockback);
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x004F8478 File Offset: 0x004F6678
		public static void ModifyWeaponCrit(Player player, Item item, ref float crit)
		{
			if (item.IsAir)
			{
				return;
			}
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyWeaponCrit.Enumerate(player))
			{
				modPlayer.ModifyWeaponCrit(item, ref crit);
			}
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x004F84BC File Offset: 0x004F66BC
		public static bool CanConsumeAmmo(Player player, Item weapon, Item ammo)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookCanConsumeAmmo.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanConsumeAmmo(weapon, ammo))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x004F84FC File Offset: 0x004F66FC
		public static void OnConsumeAmmo(Player player, Item weapon, Item ammo)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnConsumeAmmo.Enumerate(player))
			{
				modPlayer.OnConsumeAmmo(weapon, ammo);
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x004F8538 File Offset: 0x004F6738
		public static bool CanShoot(Player player, Item item)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookCanShoot.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanShoot(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x004F8578 File Offset: 0x004F6778
		public static void ModifyShootStats(Player player, Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyShootStats.Enumerate(player))
			{
				modPlayer.ModifyShootStats(item, ref position, ref velocity, ref type, ref damage, ref knockback);
			}
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x004F85BC File Offset: 0x004F67BC
		public static bool Shoot(Player player, Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			bool defaultResult = true;
			foreach (ModPlayer modPlayer in PlayerLoader.HookShoot.Enumerate(player))
			{
				defaultResult &= modPlayer.Shoot(item, source, position, velocity, type, damage, knockback);
			}
			return defaultResult;
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x004F8608 File Offset: 0x004F6808
		public static void MeleeEffects(Player player, Item item, Rectangle hitbox)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookMeleeEffects.Enumerate(player))
			{
				modPlayer.MeleeEffects(item, hitbox);
			}
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x004F8644 File Offset: 0x004F6844
		public static void EmitEnchantmentVisualsAt(Player player, Projectile projectile, Vector2 boxPosition, int boxWidth, int boxHeight)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookEmitEnchantmentVisualsAt.Enumerate(player))
			{
				modPlayer.EmitEnchantmentVisualsAt(projectile, boxPosition, boxWidth, boxHeight);
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x004F8684 File Offset: 0x004F6884
		public static bool? CanCatchNPC(Player player, NPC target, Item item)
		{
			bool? returnValue = null;
			foreach (ModPlayer modPlayer in PlayerLoader.HookCanCatchNPC.Enumerate(player))
			{
				bool? canCatch = modPlayer.CanCatchNPC(target, item);
				if (canCatch != null)
				{
					if (!canCatch.Value)
					{
						return new bool?(false);
					}
					returnValue = new bool?(true);
				}
			}
			return returnValue;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x004F86EC File Offset: 0x004F68EC
		public static void OnCatchNPC(Player player, NPC target, Item item, bool failed)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnCatchNPC.Enumerate(player))
			{
				modPlayer.OnCatchNPC(target, item, failed);
			}
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x004F8728 File Offset: 0x004F6928
		public static void ModifyItemScale(Player player, Item item, ref float scale)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyItemScale.Enumerate(player))
			{
				modPlayer.ModifyItemScale(item, ref scale);
			}
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x004F8764 File Offset: 0x004F6964
		public static void OnHitAnything(Player player, float x, float y, Entity victim)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnHitAnything.Enumerate(player))
			{
				modPlayer.OnHitAnything(x, y, victim);
			}
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x004F87A0 File Offset: 0x004F69A0
		public static bool CanHitNPC(Player player, NPC target)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookCanHitNPC.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanHitNPC(target))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x004F87E0 File Offset: 0x004F69E0
		public static bool? CanMeleeAttackCollideWithNPC(Player player, Item item, Rectangle meleeAttackHitbox, NPC target)
		{
			bool? flag = null;
			foreach (ModPlayer modPlayer in PlayerLoader.HookCanCollideNPCWithItem.Enumerate(player))
			{
				bool? canHit = modPlayer.CanMeleeAttackCollideWithNPC(item, meleeAttackHitbox, target);
				if (canHit != null)
				{
					if (!canHit.Value)
					{
						return new bool?(false);
					}
					flag = new bool?(true);
				}
			}
			return flag;
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x004F8848 File Offset: 0x004F6A48
		public static void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyHitNPC.Enumerate(player))
			{
				modPlayer.ModifyHitNPC(target, ref modifiers);
			}
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x004F8884 File Offset: 0x004F6A84
		public static void OnHitNPC(Player player, NPC target, in NPC.HitInfo hit, int damageDone)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnHitNPC.Enumerate(player))
			{
				modPlayer.OnHitNPC(target, hit, damageDone);
			}
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x004F88C4 File Offset: 0x004F6AC4
		public static bool? CanHitNPCWithItem(Player player, Item item, NPC target)
		{
			if (!PlayerLoader.CanHitNPC(player, target))
			{
				return new bool?(false);
			}
			bool? ret = null;
			foreach (ModPlayer modPlayer in PlayerLoader.HookCanHitNPCWithItem.Enumerate(player))
			{
				bool? flag = modPlayer.CanHitNPCWithItem(item, target);
				if (flag != null)
				{
					if (!flag.GetValueOrDefault())
					{
						return new bool?(false);
					}
					ret = new bool?(true);
				}
			}
			return ret;
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x004F893C File Offset: 0x004F6B3C
		public static void ModifyHitNPCWithItem(Player player, Item item, NPC target, ref NPC.HitModifiers modifiers)
		{
			PlayerLoader.ModifyHitNPC(player, target, ref modifiers);
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyHitNPCWithItem.Enumerate(player))
			{
				modPlayer.ModifyHitNPCWithItem(item, target, ref modifiers);
			}
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x004F8980 File Offset: 0x004F6B80
		public static void OnHitNPCWithItem(Player player, Item item, NPC target, in NPC.HitInfo hit, int damageDone)
		{
			PlayerLoader.OnHitNPC(player, target, hit, damageDone);
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnHitNPCWithItem.Enumerate(player))
			{
				modPlayer.OnHitNPCWithItem(item, target, hit, damageDone);
			}
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x004F89CC File Offset: 0x004F6BCC
		public static bool? CanHitNPCWithProj(Player player, Projectile proj, NPC target)
		{
			if (!PlayerLoader.CanHitNPC(player, target))
			{
				return new bool?(false);
			}
			bool? ret = null;
			foreach (ModPlayer modPlayer in PlayerLoader.HookCanHitNPCWithProj.Enumerate(player))
			{
				bool? flag = modPlayer.CanHitNPCWithProj(proj, target);
				if (flag != null)
				{
					if (!flag.GetValueOrDefault())
					{
						return new bool?(false);
					}
					ret = new bool?(true);
				}
			}
			return ret;
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x004F8A44 File Offset: 0x004F6C44
		public static void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
		{
			PlayerLoader.ModifyHitNPC(player, target, ref modifiers);
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyHitNPCWithProj.Enumerate(player))
			{
				modPlayer.ModifyHitNPCWithProj(proj, target, ref modifiers);
			}
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x004F8A88 File Offset: 0x004F6C88
		public static void OnHitNPCWithProj(Player player, Projectile proj, NPC target, in NPC.HitInfo hit, int damageDone)
		{
			PlayerLoader.OnHitNPC(player, target, hit, damageDone);
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnHitNPCWithProj.Enumerate(player))
			{
				modPlayer.OnHitNPCWithProj(proj, target, hit, damageDone);
			}
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x004F8AD4 File Offset: 0x004F6CD4
		public static bool CanHitPvp(Player player, Item item, Player target)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookCanHitPvp.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanHitPvp(item, target))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x004F8B14 File Offset: 0x004F6D14
		public static bool CanHitPvpWithProj(Projectile proj, Player target)
		{
			Player player = Main.player[proj.owner];
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookCanHitPvpWithProj.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanHitPvpWithProj(proj, target))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x004F8B60 File Offset: 0x004F6D60
		public static bool CanBeHitByNPC(Player player, NPC npc, ref int cooldownSlot)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookCanBeHitByNPC.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanBeHitByNPC(npc, ref cooldownSlot))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x004F8BA0 File Offset: 0x004F6DA0
		public static void ModifyHitByNPC(Player player, NPC npc, ref Player.HurtModifiers modifiers)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyHitByNPC.Enumerate(player))
			{
				modPlayer.ModifyHitByNPC(npc, ref modifiers);
			}
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x004F8BDC File Offset: 0x004F6DDC
		public static void OnHitByNPC(Player player, NPC npc, in Player.HurtInfo hurtInfo)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnHitByNPC.Enumerate(player))
			{
				modPlayer.OnHitByNPC(npc, hurtInfo);
			}
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x004F8C1C File Offset: 0x004F6E1C
		public static bool CanBeHitByProjectile(Player player, Projectile proj)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookCanBeHitByProjectile.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanBeHitByProjectile(proj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x004F8C5C File Offset: 0x004F6E5C
		public static void ModifyHitByProjectile(Player player, Projectile proj, ref Player.HurtModifiers modifiers)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyHitByProjectile.Enumerate(player))
			{
				modPlayer.ModifyHitByProjectile(proj, ref modifiers);
			}
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x004F8C98 File Offset: 0x004F6E98
		public static void OnHitByProjectile(Player player, Projectile proj, in Player.HurtInfo hurtInfo)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnHitByProjectile.Enumerate(player))
			{
				modPlayer.OnHitByProjectile(proj, hurtInfo);
			}
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x004F8CD8 File Offset: 0x004F6ED8
		public static void ModifyFishingAttempt(Player player, ref FishingAttempt attempt)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyFishingAttempt.Enumerate(player))
			{
				modPlayer.ModifyFishingAttempt(ref attempt);
			}
			attempt.rolledItemDrop = (attempt.rolledEnemySpawn = 0);
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x004F8D24 File Offset: 0x004F6F24
		public static void CatchFish(Player player, FishingAttempt attempt, ref int itemDrop, ref int enemySpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookCatchFish.Enumerate(player))
			{
				modPlayer.CatchFish(attempt, ref itemDrop, ref enemySpawn, ref sonar, ref sonarPosition);
			}
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x004F8D64 File Offset: 0x004F6F64
		public static void ModifyCaughtFish(Player player, Item fish)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookCaughtFish.Enumerate(player))
			{
				modPlayer.ModifyCaughtFish(fish);
			}
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x004F8DA0 File Offset: 0x004F6FA0
		public static bool? CanConsumeBait(Player player, Item bait)
		{
			bool? ret = null;
			foreach (ModPlayer modPlayer in PlayerLoader.HookCaughtFish.Enumerate(player))
			{
				bool? flag = modPlayer.CanConsumeBait(bait);
				if (flag != null)
				{
					bool b = flag.GetValueOrDefault();
					ret = new bool?(ret.GetValueOrDefault(true) && b);
				}
			}
			return ret;
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x004F8E08 File Offset: 0x004F7008
		public static void GetFishingLevel(Player player, Item fishingRod, Item bait, ref float fishingLevel)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookGetFishingLevel.Enumerate(player))
			{
				modPlayer.GetFishingLevel(fishingRod, bait, ref fishingLevel);
			}
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x004F8E44 File Offset: 0x004F7044
		public static void AnglerQuestReward(Player player, float rareMultiplier, List<Item> rewardItems)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookAnglerQuestReward.Enumerate(player))
			{
				modPlayer.AnglerQuestReward(rareMultiplier, rewardItems);
			}
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x004F8E80 File Offset: 0x004F7080
		public static void GetDyeTraderReward(Player player, List<int> rewardPool)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookGetDyeTraderReward.Enumerate(player))
			{
				modPlayer.GetDyeTraderReward(rewardPool);
			}
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x004F8EBC File Offset: 0x004F70BC
		public static void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			Player player = drawInfo.drawPlayer;
			foreach (ModPlayer modPlayer in PlayerLoader.HookDrawEffects.Enumerate(player))
			{
				try
				{
					modPlayer.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x004F8F1C File Offset: 0x004F711C
		public static void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
		{
			Player player = drawInfo.drawPlayer;
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyDrawInfo.Enumerate(player))
			{
				try
				{
					modPlayer.ModifyDrawInfo(ref drawInfo);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x004F8F74 File Offset: 0x004F7174
		public unsafe static void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
		{
			ReadOnlySpan<ModPlayer> readOnlySpan = PlayerLoader.HookModifyDrawLayerOrdering.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ModPlayer modPlayer = *readOnlySpan[i];
				try
				{
					modPlayer.ModifyDrawLayerOrdering(positions);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x004F8FC4 File Offset: 0x004F71C4
		public static void HideDrawLayers(PlayerDrawSet drawInfo)
		{
			Player player = drawInfo.drawPlayer;
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyDrawLayers.Enumerate(player))
			{
				try
				{
					modPlayer.HideDrawLayers(drawInfo);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x004F901C File Offset: 0x004F721C
		public static void ModifyScreenPosition(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyScreenPosition.Enumerate(player))
			{
				try
				{
					modPlayer.ModifyScreenPosition();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x004F906C File Offset: 0x004F726C
		public static void ModifyZoom(Player player, ref float zoom)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyZoom.Enumerate(player))
			{
				try
				{
					modPlayer.ModifyZoom(ref zoom);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x004F90BC File Offset: 0x004F72BC
		public static void PlayerConnect(int playerIndex)
		{
			Player player = Main.player[playerIndex];
			foreach (ModPlayer modPlayer in PlayerLoader.HookPlayerConnect.Enumerate(player))
			{
				modPlayer.PlayerConnect();
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x004F90FC File Offset: 0x004F72FC
		public static void PlayerDisconnect(int playerIndex)
		{
			Player player = Main.player[playerIndex];
			foreach (ModPlayer modPlayer in PlayerLoader.HookPlayerDisconnect.Enumerate(player))
			{
				modPlayer.PlayerDisconnect();
			}
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x004F913C File Offset: 0x004F733C
		public static void OnEnterWorld(int playerIndex)
		{
			Player player = Main.player[playerIndex];
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnEnterWorld.Enumerate(player))
			{
				modPlayer.OnEnterWorld();
			}
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x004F917C File Offset: 0x004F737C
		public static void OnRespawn(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnRespawn.Enumerate(player))
			{
				try
				{
					modPlayer.OnRespawn();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x004F91CC File Offset: 0x004F73CC
		public static bool ShiftClickSlot(Player player, Item[] inventory, int context, int slot)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookShiftClickSlot.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ShiftClickSlot(inventory, context, slot))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x004F920C File Offset: 0x004F740C
		public static bool HoverSlot(Player player, Item[] inventory, int context, int slot)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookHoverSlot.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HoverSlot(inventory, context, slot))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x004F924C File Offset: 0x004F744C
		public static void PostSellItem(Player player, NPC npc, Item[] shopInventory, Item item)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPostSellItem.Enumerate(player))
			{
				modPlayer.PostSellItem(npc, shopInventory, item);
			}
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x004F9288 File Offset: 0x004F7488
		public static bool CanSellItem(Player player, NPC npc, Item[] shopInventory, Item item)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookCanSellItem.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanSellItem(npc, shopInventory, item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x004F92C8 File Offset: 0x004F74C8
		public static void PostBuyItem(Player player, NPC npc, Item[] shopInventory, Item item)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPostBuyItem.Enumerate(player))
			{
				modPlayer.PostBuyItem(npc, shopInventory, item);
			}
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x004F9304 File Offset: 0x004F7504
		public static bool CanBuyItem(Player player, NPC npc, Item[] shopInventory, Item item)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookCanBuyItem.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanBuyItem(npc, shopInventory, item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x004F9344 File Offset: 0x004F7544
		public static bool CanUseItem(Player player, Item item)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookCanUseItem.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanUseItem(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x004F9384 File Offset: 0x004F7584
		public static bool? CanAutoReuseItem(Player player, Item item)
		{
			bool? flag = null;
			foreach (ModPlayer modPlayer in PlayerLoader.HookCanAutoReuseItem.Enumerate(player))
			{
				bool? allow = modPlayer.CanAutoReuseItem(item);
				if (allow != null)
				{
					if (!allow.Value)
					{
						return new bool?(false);
					}
					flag = new bool?(true);
				}
			}
			return flag;
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x004F93E8 File Offset: 0x004F75E8
		public static bool ModifyNurseHeal(Player player, NPC npc, ref int health, ref bool removeDebuffs, ref string chat)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookModifyNurseHeal.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.ModifyNurseHeal(npc, ref health, ref removeDebuffs, ref chat))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x004F942C File Offset: 0x004F762C
		public static void ModifyNursePrice(Player player, NPC npc, int health, bool removeDebuffs, ref int price)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookModifyNursePrice.Enumerate(player))
			{
				modPlayer.ModifyNursePrice(npc, health, removeDebuffs, ref price);
			}
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x004F946C File Offset: 0x004F766C
		public static void PostNurseHeal(Player player, NPC npc, int health, bool removeDebuffs, int price)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookPostNurseHeal.Enumerate(player))
			{
				modPlayer.PostNurseHeal(npc, health, removeDebuffs, price);
			}
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x004F94AC File Offset: 0x004F76AC
		public static List<Item> GetStartingItems(Player player, IEnumerable<Item> vanillaItems, bool mediumCoreDeath = false)
		{
			Dictionary<string, List<Item>> dictionary = new Dictionary<string, List<Item>>();
			dictionary["Terraria"] = vanillaItems.ToList<Item>();
			Dictionary<string, List<Item>> itemsByMod = dictionary;
			foreach (ModPlayer modPlayer in PlayerLoader.HookAddStartingItems.Enumerate(player))
			{
				itemsByMod[modPlayer.Mod.Name] = modPlayer.AddStartingItems(mediumCoreDeath).ToList<Item>();
			}
			foreach (ModPlayer modPlayer2 in PlayerLoader.HookModifyStartingInventory.Enumerate(player))
			{
				modPlayer2.ModifyStartingInventory(itemsByMod, mediumCoreDeath);
			}
			return itemsByMod.OrderBy(delegate(KeyValuePair<string, List<Item>> kv)
			{
				if (!(kv.Key == "Terraria"))
				{
					return kv.Key;
				}
				return "";
			}).SelectMany((KeyValuePair<string, List<Item>> kv) => kv.Value).ToList<Item>();
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x004F958F File Offset: 0x004F778F
		public static IEnumerable<ValueTuple<IEnumerable<Item>, ModPlayer.ItemConsumedCallback>> GetModdedCraftingMaterials(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookAddCraftingMaterials.EnumerateSlow(player.modPlayers))
			{
				ModPlayer.ItemConsumedCallback onUsedForCrafting;
				IEnumerable<Item> items = modPlayer.AddMaterialsForCrafting(out onUsedForCrafting);
				if (items != null)
				{
					yield return new ValueTuple<IEnumerable<Item>, ModPlayer.ItemConsumedCallback>(items, onUsedForCrafting);
				}
			}
			IEnumerator<ModPlayer> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x004F95A0 File Offset: 0x004F77A0
		public static bool OnPickup(Player player, Item item)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookOnPickup.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.OnPickup(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x004F95E0 File Offset: 0x004F77E0
		public static void ArmorSetBonusActivated(Player player)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookArmorSetBonusActivated.Enumerate(player))
			{
				modPlayer.ArmorSetBonusActivated();
			}
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x004F9618 File Offset: 0x004F7818
		public static void ArmorSetBonusHeld(Player player, int holdTime)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookArmorSetBonusHeld.Enumerate(player))
			{
				modPlayer.ArmorSetBonusHeld(holdTime);
			}
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x004F9654 File Offset: 0x004F7854
		public static bool CanBeTeleportedTo(Player player, Vector2 teleportPosition, string context)
		{
			FilteredSpanEnumerator<ModPlayer> enumerator = PlayerLoader.HookCanBeTeleportedTo.Enumerate(player).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanBeTeleportedTo(teleportPosition, context))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x004F9694 File Offset: 0x004F7894
		public static void OnEquipmentLoadoutSwitched(Player player, int oldLoadoutIndex, int loadoutIndex)
		{
			foreach (ModPlayer modPlayer in PlayerLoader.HookOnEquipmentLoadoutSwitched.Enumerate(player))
			{
				modPlayer.OnEquipmentLoadoutSwitched(oldLoadoutIndex, loadoutIndex);
			}
		}

		// Token: 0x040017E2 RID: 6114
		private static readonly List<ModPlayer> players = new List<ModPlayer>();

		// Token: 0x040017E3 RID: 6115
		private static readonly List<HookList<ModPlayer>> hooks = new List<HookList<ModPlayer>>();

		// Token: 0x040017E4 RID: 6116
		private static readonly List<HookList<ModPlayer>> modHooks = new List<HookList<ModPlayer>>();

		// Token: 0x040017E5 RID: 6117
		private static HookList<ModPlayer> HookInitialize = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.Initialize()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017E6 RID: 6118
		private static HookList<ModPlayer> HookResetEffects = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.ResetEffects()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017E7 RID: 6119
		private static HookList<ModPlayer> HookResetInfoAccessories = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.ResetInfoAccessories()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017E8 RID: 6120
		private static HookList<ModPlayer> HookRefreshInfoAccessoriesFromTeamPlayers = PlayerLoader.AddHook<Action<Player>>((ModPlayer p) => (Action<Player>)methodof(ModPlayer.RefreshInfoAccessoriesFromTeamPlayers(Player)).CreateDelegate(typeof(Action<Player>), p));

		// Token: 0x040017E9 RID: 6121
		private static HookList<ModPlayer> HookModifyMaxStats = PlayerLoader.AddHook<PlayerLoader.DelegateModifyMaxStats>((ModPlayer p) => (PlayerLoader.DelegateModifyMaxStats)methodof(ModPlayer.ModifyMaxStats(StatModifier*, StatModifier*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyMaxStats), p));

		// Token: 0x040017EA RID: 6122
		private static HookList<ModPlayer> HookUpdateDead = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.UpdateDead()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017EB RID: 6123
		private static HookList<ModPlayer> HookPreSavePlayer = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PreSavePlayer()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017EC RID: 6124
		private static HookList<ModPlayer> HookPostSavePlayer = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PostSavePlayer()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017ED RID: 6125
		private static HookList<ModPlayer> HookCopyClientState = PlayerLoader.AddHook<Action<ModPlayer>>((ModPlayer p) => (Action<ModPlayer>)methodof(ModPlayer.CopyClientState(ModPlayer)).CreateDelegate(typeof(Action<ModPlayer>), p));

		// Token: 0x040017EE RID: 6126
		private static HookList<ModPlayer> HookSyncPlayer = PlayerLoader.AddHook<Action<int, int, bool>>((ModPlayer p) => (Action<int, int, bool>)methodof(ModPlayer.SyncPlayer(int, int, bool)).CreateDelegate(typeof(Action<int, int, bool>), p));

		// Token: 0x040017EF RID: 6127
		private static HookList<ModPlayer> HookSendClientChanges = PlayerLoader.AddHook<Action<ModPlayer>>((ModPlayer p) => (Action<ModPlayer>)methodof(ModPlayer.SendClientChanges(ModPlayer)).CreateDelegate(typeof(Action<ModPlayer>), p));

		// Token: 0x040017F0 RID: 6128
		private static HookList<ModPlayer> HookUpdateBadLifeRegen = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.UpdateBadLifeRegen()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017F1 RID: 6129
		private static HookList<ModPlayer> HookUpdateLifeRegen = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.UpdateLifeRegen()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017F2 RID: 6130
		private static HookList<ModPlayer> HookNaturalLifeRegen = PlayerLoader.AddHook<PlayerLoader.DelegateNaturalLifeRegen>((ModPlayer p) => (PlayerLoader.DelegateNaturalLifeRegen)methodof(ModPlayer.NaturalLifeRegen(float*)).CreateDelegate(typeof(PlayerLoader.DelegateNaturalLifeRegen), p));

		// Token: 0x040017F3 RID: 6131
		private static HookList<ModPlayer> HookUpdateAutopause = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.UpdateAutopause()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017F4 RID: 6132
		private static HookList<ModPlayer> HookPreUpdate = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PreUpdate()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017F5 RID: 6133
		private static HookList<ModPlayer> HookSetControls = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.SetControls()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017F6 RID: 6134
		private static HookList<ModPlayer> HookPreUpdateBuffs = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PreUpdateBuffs()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017F7 RID: 6135
		private static HookList<ModPlayer> HookPostUpdateBuffs = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PostUpdateBuffs()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017F8 RID: 6136
		private static HookList<ModPlayer> HookUpdateEquips = PlayerLoader.AddHook<PlayerLoader.DelegateUpdateEquips>((ModPlayer p) => (PlayerLoader.DelegateUpdateEquips)methodof(ModPlayer.UpdateEquips()).CreateDelegate(typeof(PlayerLoader.DelegateUpdateEquips), p));

		// Token: 0x040017F9 RID: 6137
		private static HookList<ModPlayer> HookPostUpdateEquips = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PostUpdateEquips()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017FA RID: 6138
		private static HookList<ModPlayer> HookUpdateVisibleAccessories = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.UpdateVisibleAccessories()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017FB RID: 6139
		private static HookList<ModPlayer> HookUpdateVisibleVanityAccessories = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.UpdateVisibleVanityAccessories()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017FC RID: 6140
		private static HookList<ModPlayer> HookUpdateDyes = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.UpdateDyes()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017FD RID: 6141
		private static HookList<ModPlayer> HookPostUpdateMiscEffects = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PostUpdateMiscEffects()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017FE RID: 6142
		private static HookList<ModPlayer> HookPostUpdateRunSpeeds = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PostUpdateRunSpeeds()).CreateDelegate(typeof(Action), p));

		// Token: 0x040017FF RID: 6143
		private static HookList<ModPlayer> HookPreUpdateMovement = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PreUpdateMovement()).CreateDelegate(typeof(Action), p));

		// Token: 0x04001800 RID: 6144
		private static HookList<ModPlayer> HookPostUpdate = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PostUpdate()).CreateDelegate(typeof(Action), p));

		// Token: 0x04001801 RID: 6145
		private static HookList<ModPlayer> HookModifyExtraJumpDurationMultiplier = PlayerLoader.AddHook<PlayerLoader.DelegateModifyExtraJumpDuration>((ModPlayer p) => (PlayerLoader.DelegateModifyExtraJumpDuration)methodof(ModPlayer.ModifyExtraJumpDurationMultiplier(ExtraJump, float*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyExtraJumpDuration), p));

		// Token: 0x04001802 RID: 6146
		private static HookList<ModPlayer> HookCanStartExtraJump = PlayerLoader.AddHook<Func<ExtraJump, bool>>((ModPlayer p) => (Func<ExtraJump, bool>)methodof(ModPlayer.CanStartExtraJump(ExtraJump)).CreateDelegate(typeof(Func<ExtraJump, bool>), p));

		// Token: 0x04001803 RID: 6147
		private static HookList<ModPlayer> HookOnExtraJumpStarted = PlayerLoader.AddHook<PlayerLoader.DelegateOnExtraJumpStarted>((ModPlayer p) => (PlayerLoader.DelegateOnExtraJumpStarted)methodof(ModPlayer.OnExtraJumpStarted(ExtraJump, bool*)).CreateDelegate(typeof(PlayerLoader.DelegateOnExtraJumpStarted), p));

		// Token: 0x04001804 RID: 6148
		private static HookList<ModPlayer> HookOnExtraJumpEnded = PlayerLoader.AddHook<Action<ExtraJump>>((ModPlayer p) => (Action<ExtraJump>)methodof(ModPlayer.OnExtraJumpEnded(ExtraJump)).CreateDelegate(typeof(Action<ExtraJump>), p));

		// Token: 0x04001805 RID: 6149
		private static HookList<ModPlayer> HookOnExtraJumpRefreshed = PlayerLoader.AddHook<Action<ExtraJump>>((ModPlayer p) => (Action<ExtraJump>)methodof(ModPlayer.OnExtraJumpRefreshed(ExtraJump)).CreateDelegate(typeof(Action<ExtraJump>), p));

		// Token: 0x04001806 RID: 6150
		private static HookList<ModPlayer> HookExtraJumpVisuals = PlayerLoader.AddHook<Action<ExtraJump>>((ModPlayer p) => (Action<ExtraJump>)methodof(ModPlayer.ExtraJumpVisuals(ExtraJump)).CreateDelegate(typeof(Action<ExtraJump>), p));

		// Token: 0x04001807 RID: 6151
		private static HookList<ModPlayer> HookCanShowExtraJumpVisuals = PlayerLoader.AddHook<Func<ExtraJump, bool>>((ModPlayer p) => (Func<ExtraJump, bool>)methodof(ModPlayer.CanShowExtraJumpVisuals(ExtraJump)).CreateDelegate(typeof(Func<ExtraJump, bool>), p));

		// Token: 0x04001808 RID: 6152
		private static HookList<ModPlayer> HookFrameEffects = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.FrameEffects()).CreateDelegate(typeof(Action), p));

		// Token: 0x04001809 RID: 6153
		private static HookList<ModPlayer> HookImmuneTo = PlayerLoader.AddHook<Func<PlayerDeathReason, int, bool, bool>>((ModPlayer p) => (Func<PlayerDeathReason, int, bool, bool>)methodof(ModPlayer.ImmuneTo(PlayerDeathReason, int, bool)).CreateDelegate(typeof(Func<PlayerDeathReason, int, bool, bool>), p));

		// Token: 0x0400180A RID: 6154
		private static HookList<ModPlayer> HookFreeDodge = PlayerLoader.AddHook<Func<Player.HurtInfo, bool>>((ModPlayer p) => (Func<Player.HurtInfo, bool>)methodof(ModPlayer.FreeDodge(Player.HurtInfo)).CreateDelegate(typeof(Func<Player.HurtInfo, bool>), p));

		// Token: 0x0400180B RID: 6155
		private static HookList<ModPlayer> HookConsumableDodge = PlayerLoader.AddHook<Func<Player.HurtInfo, bool>>((ModPlayer p) => (Func<Player.HurtInfo, bool>)methodof(ModPlayer.ConsumableDodge(Player.HurtInfo)).CreateDelegate(typeof(Func<Player.HurtInfo, bool>), p));

		// Token: 0x0400180C RID: 6156
		private static HookList<ModPlayer> HookModifyHurt = PlayerLoader.AddHook<PlayerLoader.DelegateModifyHurt>((ModPlayer p) => (PlayerLoader.DelegateModifyHurt)methodof(ModPlayer.ModifyHurt(Player.HurtModifiers*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyHurt), p));

		// Token: 0x0400180D RID: 6157
		private static HookList<ModPlayer> HookHurt = PlayerLoader.AddHook<Action<Player.HurtInfo>>((ModPlayer p) => (Action<Player.HurtInfo>)methodof(ModPlayer.OnHurt(Player.HurtInfo)).CreateDelegate(typeof(Action<Player.HurtInfo>), p));

		// Token: 0x0400180E RID: 6158
		private static HookList<ModPlayer> HookPostHurt = PlayerLoader.AddHook<Action<Player.HurtInfo>>((ModPlayer p) => (Action<Player.HurtInfo>)methodof(ModPlayer.PostHurt(Player.HurtInfo)).CreateDelegate(typeof(Action<Player.HurtInfo>), p));

		// Token: 0x0400180F RID: 6159
		private static HookList<ModPlayer> HookPreKill = PlayerLoader.AddHook<PlayerLoader.DelegatePreKill>((ModPlayer p) => (PlayerLoader.DelegatePreKill)methodof(ModPlayer.PreKill(double, int, bool, bool*, bool*, PlayerDeathReason*)).CreateDelegate(typeof(PlayerLoader.DelegatePreKill), p));

		// Token: 0x04001810 RID: 6160
		private static HookList<ModPlayer> HookKill = PlayerLoader.AddHook<Action<double, int, bool, PlayerDeathReason>>((ModPlayer p) => (Action<double, int, bool, PlayerDeathReason>)methodof(ModPlayer.Kill(double, int, bool, PlayerDeathReason)).CreateDelegate(typeof(Action<double, int, bool, PlayerDeathReason>), p));

		// Token: 0x04001811 RID: 6161
		private static HookList<ModPlayer> HookPreModifyLuck = PlayerLoader.AddHook<PlayerLoader.DelegatePreModifyLuck>((ModPlayer p) => (PlayerLoader.DelegatePreModifyLuck)methodof(ModPlayer.PreModifyLuck(float*)).CreateDelegate(typeof(PlayerLoader.DelegatePreModifyLuck), p));

		// Token: 0x04001812 RID: 6162
		private static HookList<ModPlayer> HookModifyLuck = PlayerLoader.AddHook<PlayerLoader.DelegateModifyLuck>((ModPlayer p) => (PlayerLoader.DelegateModifyLuck)methodof(ModPlayer.ModifyLuck(float*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyLuck), p));

		// Token: 0x04001813 RID: 6163
		private static HookList<ModPlayer> HookPreItemCheck = PlayerLoader.AddHook<Func<bool>>((ModPlayer p) => (Func<bool>)methodof(ModPlayer.PreItemCheck()).CreateDelegate(typeof(Func<bool>), p));

		// Token: 0x04001814 RID: 6164
		private static HookList<ModPlayer> HookPostItemCheck = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PostItemCheck()).CreateDelegate(typeof(Action), p));

		// Token: 0x04001815 RID: 6165
		private static HookList<ModPlayer> HookUseTimeMultiplier = PlayerLoader.AddHook<Func<Item, float>>((ModPlayer p) => (Func<Item, float>)methodof(ModPlayer.UseTimeMultiplier(Item)).CreateDelegate(typeof(Func<Item, float>), p));

		// Token: 0x04001816 RID: 6166
		private static HookList<ModPlayer> HookUseAnimationMultiplier = PlayerLoader.AddHook<Func<Item, float>>((ModPlayer p) => (Func<Item, float>)methodof(ModPlayer.UseAnimationMultiplier(Item)).CreateDelegate(typeof(Func<Item, float>), p));

		// Token: 0x04001817 RID: 6167
		private static HookList<ModPlayer> HookUseSpeedMultiplier = PlayerLoader.AddHook<Func<Item, float>>((ModPlayer p) => (Func<Item, float>)methodof(ModPlayer.UseSpeedMultiplier(Item)).CreateDelegate(typeof(Func<Item, float>), p));

		// Token: 0x04001818 RID: 6168
		private static HookList<ModPlayer> HookGetHealLife = PlayerLoader.AddHook<PlayerLoader.DelegateGetHealLife>((ModPlayer p) => (PlayerLoader.DelegateGetHealLife)methodof(ModPlayer.GetHealLife(Item, bool, int*)).CreateDelegate(typeof(PlayerLoader.DelegateGetHealLife), p));

		// Token: 0x04001819 RID: 6169
		private static HookList<ModPlayer> HookGetHealMana = PlayerLoader.AddHook<PlayerLoader.DelegateGetHealMana>((ModPlayer p) => (PlayerLoader.DelegateGetHealMana)methodof(ModPlayer.GetHealMana(Item, bool, int*)).CreateDelegate(typeof(PlayerLoader.DelegateGetHealMana), p));

		// Token: 0x0400181A RID: 6170
		private static HookList<ModPlayer> HookModifyManaCost = PlayerLoader.AddHook<PlayerLoader.DelegateModifyManaCost>((ModPlayer p) => (PlayerLoader.DelegateModifyManaCost)methodof(ModPlayer.ModifyManaCost(Item, float*, float*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyManaCost), p));

		// Token: 0x0400181B RID: 6171
		private static HookList<ModPlayer> HookOnMissingMana = PlayerLoader.AddHook<Action<Item, int>>((ModPlayer p) => (Action<Item, int>)methodof(ModPlayer.OnMissingMana(Item, int)).CreateDelegate(typeof(Action<Item, int>), p));

		// Token: 0x0400181C RID: 6172
		private static HookList<ModPlayer> HookOnConsumeMana = PlayerLoader.AddHook<Action<Item, int>>((ModPlayer p) => (Action<Item, int>)methodof(ModPlayer.OnConsumeMana(Item, int)).CreateDelegate(typeof(Action<Item, int>), p));

		// Token: 0x0400181D RID: 6173
		private static HookList<ModPlayer> HookModifyWeaponDamage = PlayerLoader.AddHook<PlayerLoader.DelegateModifyWeaponDamage>((ModPlayer p) => (PlayerLoader.DelegateModifyWeaponDamage)methodof(ModPlayer.ModifyWeaponDamage(Item, StatModifier*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyWeaponDamage), p));

		// Token: 0x0400181E RID: 6174
		private static HookList<ModPlayer> HookProcessTriggers = PlayerLoader.AddHook<Action<TriggersSet>>((ModPlayer p) => (Action<TriggersSet>)methodof(ModPlayer.ProcessTriggers(TriggersSet)).CreateDelegate(typeof(Action<TriggersSet>), p));

		// Token: 0x0400181F RID: 6175
		private static HookList<ModPlayer> HookModifyWeaponKnockback = PlayerLoader.AddHook<PlayerLoader.DelegateModifyWeaponKnockback>((ModPlayer p) => (PlayerLoader.DelegateModifyWeaponKnockback)methodof(ModPlayer.ModifyWeaponKnockback(Item, StatModifier*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyWeaponKnockback), p));

		// Token: 0x04001820 RID: 6176
		private static HookList<ModPlayer> HookModifyWeaponCrit = PlayerLoader.AddHook<PlayerLoader.DelegateModifyWeaponCrit>((ModPlayer p) => (PlayerLoader.DelegateModifyWeaponCrit)methodof(ModPlayer.ModifyWeaponCrit(Item, float*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyWeaponCrit), p));

		// Token: 0x04001821 RID: 6177
		private static HookList<ModPlayer> HookCanConsumeAmmo = PlayerLoader.AddHook<Func<Item, Item, bool>>((ModPlayer p) => (Func<Item, Item, bool>)methodof(ModPlayer.CanConsumeAmmo(Item, Item)).CreateDelegate(typeof(Func<Item, Item, bool>), p));

		// Token: 0x04001822 RID: 6178
		private static HookList<ModPlayer> HookOnConsumeAmmo = PlayerLoader.AddHook<Action<Item, Item>>((ModPlayer p) => (Action<Item, Item>)methodof(ModPlayer.OnConsumeAmmo(Item, Item)).CreateDelegate(typeof(Action<Item, Item>), p));

		// Token: 0x04001823 RID: 6179
		private static HookList<ModPlayer> HookCanShoot = PlayerLoader.AddHook<Func<Item, bool>>((ModPlayer p) => (Func<Item, bool>)methodof(ModPlayer.CanShoot(Item)).CreateDelegate(typeof(Func<Item, bool>), p));

		// Token: 0x04001824 RID: 6180
		private static HookList<ModPlayer> HookModifyShootStats = PlayerLoader.AddHook<PlayerLoader.DelegateModifyShootStats>((ModPlayer p) => (PlayerLoader.DelegateModifyShootStats)methodof(ModPlayer.ModifyShootStats(Item, Vector2*, Vector2*, int*, int*, float*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyShootStats), p));

		// Token: 0x04001825 RID: 6181
		private static HookList<ModPlayer> HookShoot = PlayerLoader.AddHook<Func<Item, EntitySource_ItemUse_WithAmmo, Vector2, Vector2, int, int, float, bool>>((ModPlayer p) => (Func<Item, EntitySource_ItemUse_WithAmmo, Vector2, Vector2, int, int, float, bool>)methodof(ModPlayer.Shoot(Item, EntitySource_ItemUse_WithAmmo, Vector2, Vector2, int, int, float)).CreateDelegate(typeof(Func<Item, EntitySource_ItemUse_WithAmmo, Vector2, Vector2, int, int, float, bool>), p));

		// Token: 0x04001826 RID: 6182
		private static HookList<ModPlayer> HookMeleeEffects = PlayerLoader.AddHook<Action<Item, Rectangle>>((ModPlayer p) => (Action<Item, Rectangle>)methodof(ModPlayer.MeleeEffects(Item, Rectangle)).CreateDelegate(typeof(Action<Item, Rectangle>), p));

		// Token: 0x04001827 RID: 6183
		private static HookList<ModPlayer> HookEmitEnchantmentVisualsAt = PlayerLoader.AddHook<Action<Projectile, Vector2, int, int>>((ModPlayer p) => (Action<Projectile, Vector2, int, int>)methodof(ModPlayer.EmitEnchantmentVisualsAt(Projectile, Vector2, int, int)).CreateDelegate(typeof(Action<Projectile, Vector2, int, int>), p));

		// Token: 0x04001828 RID: 6184
		private static HookList<ModPlayer> HookCanCatchNPC = PlayerLoader.AddHook<Func<NPC, Item, bool?>>((ModPlayer p) => (Func<NPC, Item, bool?>)methodof(ModPlayer.CanCatchNPC(NPC, Item)).CreateDelegate(typeof(Func<NPC, Item, bool?>), p));

		// Token: 0x04001829 RID: 6185
		private static HookList<ModPlayer> HookOnCatchNPC = PlayerLoader.AddHook<Action<NPC, Item, bool>>((ModPlayer p) => (Action<NPC, Item, bool>)methodof(ModPlayer.OnCatchNPC(NPC, Item, bool)).CreateDelegate(typeof(Action<NPC, Item, bool>), p));

		// Token: 0x0400182A RID: 6186
		private static HookList<ModPlayer> HookModifyItemScale = PlayerLoader.AddHook<PlayerLoader.DelegateModifyItemScale>((ModPlayer p) => (PlayerLoader.DelegateModifyItemScale)methodof(ModPlayer.ModifyItemScale(Item, float*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyItemScale), p));

		// Token: 0x0400182B RID: 6187
		private static HookList<ModPlayer> HookOnHitAnything = PlayerLoader.AddHook<Action<float, float, Entity>>((ModPlayer p) => (Action<float, float, Entity>)methodof(ModPlayer.OnHitAnything(float, float, Entity)).CreateDelegate(typeof(Action<float, float, Entity>), p));

		// Token: 0x0400182C RID: 6188
		private static HookList<ModPlayer> HookCanHitNPC = PlayerLoader.AddHook<Func<NPC, bool>>((ModPlayer p) => (Func<NPC, bool>)methodof(ModPlayer.CanHitNPC(NPC)).CreateDelegate(typeof(Func<NPC, bool>), p));

		// Token: 0x0400182D RID: 6189
		private static HookList<ModPlayer> HookCanCollideNPCWithItem = PlayerLoader.AddHook<Func<Item, Rectangle, NPC, bool?>>((ModPlayer p) => (Func<Item, Rectangle, NPC, bool?>)methodof(ModPlayer.CanMeleeAttackCollideWithNPC(Item, Rectangle, NPC)).CreateDelegate(typeof(Func<Item, Rectangle, NPC, bool?>), p));

		// Token: 0x0400182E RID: 6190
		private static HookList<ModPlayer> HookModifyHitNPC = PlayerLoader.AddHook<PlayerLoader.DelegateModifyHitNPC>((ModPlayer p) => (PlayerLoader.DelegateModifyHitNPC)methodof(ModPlayer.ModifyHitNPC(NPC, NPC.HitModifiers*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyHitNPC), p));

		// Token: 0x0400182F RID: 6191
		private static HookList<ModPlayer> HookOnHitNPC = PlayerLoader.AddHook<Action<NPC, NPC.HitInfo, int>>((ModPlayer p) => (Action<NPC, NPC.HitInfo, int>)methodof(ModPlayer.OnHitNPC(NPC, NPC.HitInfo, int)).CreateDelegate(typeof(Action<NPC, NPC.HitInfo, int>), p));

		// Token: 0x04001830 RID: 6192
		private static HookList<ModPlayer> HookCanHitNPCWithItem = PlayerLoader.AddHook<Func<Item, NPC, bool?>>((ModPlayer p) => (Func<Item, NPC, bool?>)methodof(ModPlayer.CanHitNPCWithItem(Item, NPC)).CreateDelegate(typeof(Func<Item, NPC, bool?>), p));

		// Token: 0x04001831 RID: 6193
		private static HookList<ModPlayer> HookModifyHitNPCWithItem = PlayerLoader.AddHook<PlayerLoader.DelegateModifyHitNPCWithItem>((ModPlayer p) => (PlayerLoader.DelegateModifyHitNPCWithItem)methodof(ModPlayer.ModifyHitNPCWithItem(Item, NPC, NPC.HitModifiers*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyHitNPCWithItem), p));

		// Token: 0x04001832 RID: 6194
		private static HookList<ModPlayer> HookOnHitNPCWithItem = PlayerLoader.AddHook<Action<Item, NPC, NPC.HitInfo, int>>((ModPlayer p) => (Action<Item, NPC, NPC.HitInfo, int>)methodof(ModPlayer.OnHitNPCWithItem(Item, NPC, NPC.HitInfo, int)).CreateDelegate(typeof(Action<Item, NPC, NPC.HitInfo, int>), p));

		// Token: 0x04001833 RID: 6195
		private static HookList<ModPlayer> HookCanHitNPCWithProj = PlayerLoader.AddHook<Func<Projectile, NPC, bool?>>((ModPlayer p) => (Func<Projectile, NPC, bool?>)methodof(ModPlayer.CanHitNPCWithProj(Projectile, NPC)).CreateDelegate(typeof(Func<Projectile, NPC, bool?>), p));

		// Token: 0x04001834 RID: 6196
		private static HookList<ModPlayer> HookModifyHitNPCWithProj = PlayerLoader.AddHook<PlayerLoader.DelegateModifyHitNPCWithProj>((ModPlayer p) => (PlayerLoader.DelegateModifyHitNPCWithProj)methodof(ModPlayer.ModifyHitNPCWithProj(Projectile, NPC, NPC.HitModifiers*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyHitNPCWithProj), p));

		// Token: 0x04001835 RID: 6197
		private static HookList<ModPlayer> HookOnHitNPCWithProj = PlayerLoader.AddHook<Action<Projectile, NPC, NPC.HitInfo, int>>((ModPlayer p) => (Action<Projectile, NPC, NPC.HitInfo, int>)methodof(ModPlayer.OnHitNPCWithProj(Projectile, NPC, NPC.HitInfo, int)).CreateDelegate(typeof(Action<Projectile, NPC, NPC.HitInfo, int>), p));

		// Token: 0x04001836 RID: 6198
		private static HookList<ModPlayer> HookCanHitPvp = PlayerLoader.AddHook<Func<Item, Player, bool>>((ModPlayer p) => (Func<Item, Player, bool>)methodof(ModPlayer.CanHitPvp(Item, Player)).CreateDelegate(typeof(Func<Item, Player, bool>), p));

		// Token: 0x04001837 RID: 6199
		private static HookList<ModPlayer> HookCanHitPvpWithProj = PlayerLoader.AddHook<Func<Projectile, Player, bool>>((ModPlayer p) => (Func<Projectile, Player, bool>)methodof(ModPlayer.CanHitPvpWithProj(Projectile, Player)).CreateDelegate(typeof(Func<Projectile, Player, bool>), p));

		// Token: 0x04001838 RID: 6200
		private static HookList<ModPlayer> HookCanBeHitByNPC = PlayerLoader.AddHook<PlayerLoader.DelegateCanBeHitByNPC>((ModPlayer p) => (PlayerLoader.DelegateCanBeHitByNPC)methodof(ModPlayer.CanBeHitByNPC(NPC, int*)).CreateDelegate(typeof(PlayerLoader.DelegateCanBeHitByNPC), p));

		// Token: 0x04001839 RID: 6201
		private static HookList<ModPlayer> HookModifyHitByNPC = PlayerLoader.AddHook<PlayerLoader.DelegateModifyHitByNPC>((ModPlayer p) => (PlayerLoader.DelegateModifyHitByNPC)methodof(ModPlayer.ModifyHitByNPC(NPC, Player.HurtModifiers*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyHitByNPC), p));

		// Token: 0x0400183A RID: 6202
		private static HookList<ModPlayer> HookOnHitByNPC = PlayerLoader.AddHook<Action<NPC, Player.HurtInfo>>((ModPlayer p) => (Action<NPC, Player.HurtInfo>)methodof(ModPlayer.OnHitByNPC(NPC, Player.HurtInfo)).CreateDelegate(typeof(Action<NPC, Player.HurtInfo>), p));

		// Token: 0x0400183B RID: 6203
		private static HookList<ModPlayer> HookCanBeHitByProjectile = PlayerLoader.AddHook<Func<Projectile, bool>>((ModPlayer p) => (Func<Projectile, bool>)methodof(ModPlayer.CanBeHitByProjectile(Projectile)).CreateDelegate(typeof(Func<Projectile, bool>), p));

		// Token: 0x0400183C RID: 6204
		private static HookList<ModPlayer> HookModifyHitByProjectile = PlayerLoader.AddHook<PlayerLoader.DelegateModifyHitByProjectile>((ModPlayer p) => (PlayerLoader.DelegateModifyHitByProjectile)methodof(ModPlayer.ModifyHitByProjectile(Projectile, Player.HurtModifiers*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyHitByProjectile), p));

		// Token: 0x0400183D RID: 6205
		private static HookList<ModPlayer> HookOnHitByProjectile = PlayerLoader.AddHook<Action<Projectile, Player.HurtInfo>>((ModPlayer p) => (Action<Projectile, Player.HurtInfo>)methodof(ModPlayer.OnHitByProjectile(Projectile, Player.HurtInfo)).CreateDelegate(typeof(Action<Projectile, Player.HurtInfo>), p));

		// Token: 0x0400183E RID: 6206
		private static HookList<ModPlayer> HookModifyFishingAttempt = PlayerLoader.AddHook<PlayerLoader.DelegateModifyFishingAttempt>((ModPlayer p) => (PlayerLoader.DelegateModifyFishingAttempt)methodof(ModPlayer.ModifyFishingAttempt(FishingAttempt*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyFishingAttempt), p));

		// Token: 0x0400183F RID: 6207
		private static HookList<ModPlayer> HookCatchFish = PlayerLoader.AddHook<PlayerLoader.DelegateCatchFish>((ModPlayer p) => (PlayerLoader.DelegateCatchFish)methodof(ModPlayer.CatchFish(FishingAttempt, int*, int*, AdvancedPopupRequest*, Vector2*)).CreateDelegate(typeof(PlayerLoader.DelegateCatchFish), p));

		// Token: 0x04001840 RID: 6208
		private static HookList<ModPlayer> HookCaughtFish = PlayerLoader.AddHook<PlayerLoader.DelegateModifyCaughtFish>((ModPlayer p) => (PlayerLoader.DelegateModifyCaughtFish)methodof(ModPlayer.ModifyCaughtFish(Item)).CreateDelegate(typeof(PlayerLoader.DelegateModifyCaughtFish), p));

		// Token: 0x04001841 RID: 6209
		private static HookList<ModPlayer> HookCanConsumeBait = PlayerLoader.AddHook<PlayerLoader.DelegateCanConsumeBait>((ModPlayer p) => (PlayerLoader.DelegateCanConsumeBait)methodof(ModPlayer.CanConsumeBait(Item)).CreateDelegate(typeof(PlayerLoader.DelegateCanConsumeBait), p));

		// Token: 0x04001842 RID: 6210
		private static HookList<ModPlayer> HookGetFishingLevel = PlayerLoader.AddHook<PlayerLoader.DelegateGetFishingLevel>((ModPlayer p) => (PlayerLoader.DelegateGetFishingLevel)methodof(ModPlayer.GetFishingLevel(Item, Item, float*)).CreateDelegate(typeof(PlayerLoader.DelegateGetFishingLevel), p));

		// Token: 0x04001843 RID: 6211
		private static HookList<ModPlayer> HookAnglerQuestReward = PlayerLoader.AddHook<Action<float, List<Item>>>((ModPlayer p) => (Action<float, List<Item>>)methodof(ModPlayer.AnglerQuestReward(float, List<Item>)).CreateDelegate(typeof(Action<float, List<Item>>), p));

		// Token: 0x04001844 RID: 6212
		private static HookList<ModPlayer> HookGetDyeTraderReward = PlayerLoader.AddHook<Action<List<int>>>((ModPlayer p) => (Action<List<int>>)methodof(ModPlayer.GetDyeTraderReward(List<int>)).CreateDelegate(typeof(Action<List<int>>), p));

		// Token: 0x04001845 RID: 6213
		private static HookList<ModPlayer> HookDrawEffects = PlayerLoader.AddHook<PlayerLoader.DelegateDrawEffects>((ModPlayer p) => (PlayerLoader.DelegateDrawEffects)methodof(ModPlayer.DrawEffects(PlayerDrawSet, float*, float*, float*, float*, bool*)).CreateDelegate(typeof(PlayerLoader.DelegateDrawEffects), p));

		// Token: 0x04001846 RID: 6214
		private static HookList<ModPlayer> HookModifyDrawInfo = PlayerLoader.AddHook<PlayerLoader.DelegateModifyDrawInfo>((ModPlayer p) => (PlayerLoader.DelegateModifyDrawInfo)methodof(ModPlayer.ModifyDrawInfo(PlayerDrawSet*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyDrawInfo), p));

		// Token: 0x04001847 RID: 6215
		private static HookList<ModPlayer> HookModifyDrawLayerOrdering = PlayerLoader.AddHook<Action<IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position>>>((ModPlayer p) => (Action<IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position>>)methodof(ModPlayer.ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position>)).CreateDelegate(typeof(Action<IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position>>), p));

		// Token: 0x04001848 RID: 6216
		private static HookList<ModPlayer> HookModifyDrawLayers = PlayerLoader.AddHook<Action<PlayerDrawSet>>((ModPlayer p) => (Action<PlayerDrawSet>)methodof(ModPlayer.HideDrawLayers(PlayerDrawSet)).CreateDelegate(typeof(Action<PlayerDrawSet>), p));

		// Token: 0x04001849 RID: 6217
		private static HookList<ModPlayer> HookModifyScreenPosition = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.ModifyScreenPosition()).CreateDelegate(typeof(Action), p));

		// Token: 0x0400184A RID: 6218
		private static HookList<ModPlayer> HookModifyZoom = PlayerLoader.AddHook<PlayerLoader.DelegateModifyZoom>((ModPlayer p) => (PlayerLoader.DelegateModifyZoom)methodof(ModPlayer.ModifyZoom(float*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyZoom), p));

		// Token: 0x0400184B RID: 6219
		private static HookList<ModPlayer> HookPlayerConnect = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PlayerConnect()).CreateDelegate(typeof(Action), p));

		// Token: 0x0400184C RID: 6220
		private static HookList<ModPlayer> HookPlayerDisconnect = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.PlayerDisconnect()).CreateDelegate(typeof(Action), p));

		// Token: 0x0400184D RID: 6221
		private static HookList<ModPlayer> HookOnEnterWorld = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.OnEnterWorld()).CreateDelegate(typeof(Action), p));

		// Token: 0x0400184E RID: 6222
		private static HookList<ModPlayer> HookOnRespawn = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.OnRespawn()).CreateDelegate(typeof(Action), p));

		// Token: 0x0400184F RID: 6223
		private static HookList<ModPlayer> HookShiftClickSlot = PlayerLoader.AddHook<Func<Item[], int, int, bool>>((ModPlayer p) => (Func<Item[], int, int, bool>)methodof(ModPlayer.ShiftClickSlot(Item[], int, int)).CreateDelegate(typeof(Func<Item[], int, int, bool>), p));

		// Token: 0x04001850 RID: 6224
		private static HookList<ModPlayer> HookHoverSlot = PlayerLoader.AddHook<Func<Item[], int, int, bool>>((ModPlayer p) => (Func<Item[], int, int, bool>)methodof(ModPlayer.HoverSlot(Item[], int, int)).CreateDelegate(typeof(Func<Item[], int, int, bool>), p));

		// Token: 0x04001851 RID: 6225
		private static HookList<ModPlayer> HookPostSellItem = PlayerLoader.AddHook<Action<NPC, Item[], Item>>((ModPlayer p) => (Action<NPC, Item[], Item>)methodof(ModPlayer.PostSellItem(NPC, Item[], Item)).CreateDelegate(typeof(Action<NPC, Item[], Item>), p));

		// Token: 0x04001852 RID: 6226
		private static HookList<ModPlayer> HookCanSellItem = PlayerLoader.AddHook<Func<NPC, Item[], Item, bool>>((ModPlayer p) => (Func<NPC, Item[], Item, bool>)methodof(ModPlayer.CanSellItem(NPC, Item[], Item)).CreateDelegate(typeof(Func<NPC, Item[], Item, bool>), p));

		// Token: 0x04001853 RID: 6227
		private static HookList<ModPlayer> HookPostBuyItem = PlayerLoader.AddHook<Action<NPC, Item[], Item>>((ModPlayer p) => (Action<NPC, Item[], Item>)methodof(ModPlayer.PostBuyItem(NPC, Item[], Item)).CreateDelegate(typeof(Action<NPC, Item[], Item>), p));

		// Token: 0x04001854 RID: 6228
		private static HookList<ModPlayer> HookCanBuyItem = PlayerLoader.AddHook<Func<NPC, Item[], Item, bool>>((ModPlayer p) => (Func<NPC, Item[], Item, bool>)methodof(ModPlayer.CanBuyItem(NPC, Item[], Item)).CreateDelegate(typeof(Func<NPC, Item[], Item, bool>), p));

		// Token: 0x04001855 RID: 6229
		private static HookList<ModPlayer> HookCanUseItem = PlayerLoader.AddHook<Func<Item, bool>>((ModPlayer p) => (Func<Item, bool>)methodof(ModPlayer.CanUseItem(Item)).CreateDelegate(typeof(Func<Item, bool>), p));

		// Token: 0x04001856 RID: 6230
		private static HookList<ModPlayer> HookCanAutoReuseItem = PlayerLoader.AddHook<Func<Item, bool?>>((ModPlayer p) => (Func<Item, bool?>)methodof(ModPlayer.CanAutoReuseItem(Item)).CreateDelegate(typeof(Func<Item, bool?>), p));

		// Token: 0x04001857 RID: 6231
		private static readonly HookList<ModPlayer> HookModifyNurseHeal = PlayerLoader.AddHook<PlayerLoader.DelegateModifyNurseHeal>((ModPlayer p) => (PlayerLoader.DelegateModifyNurseHeal)methodof(ModPlayer.ModifyNurseHeal(NPC, int*, bool*, string*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyNurseHeal), p));

		// Token: 0x04001858 RID: 6232
		private static HookList<ModPlayer> HookModifyNursePrice = PlayerLoader.AddHook<PlayerLoader.DelegateModifyNursePrice>((ModPlayer p) => (PlayerLoader.DelegateModifyNursePrice)methodof(ModPlayer.ModifyNursePrice(NPC, int, bool, int*)).CreateDelegate(typeof(PlayerLoader.DelegateModifyNursePrice), p));

		// Token: 0x04001859 RID: 6233
		private static HookList<ModPlayer> HookPostNurseHeal = PlayerLoader.AddHook<Action<NPC, int, bool, int>>((ModPlayer p) => (Action<NPC, int, bool, int>)methodof(ModPlayer.PostNurseHeal(NPC, int, bool, int)).CreateDelegate(typeof(Action<NPC, int, bool, int>), p));

		// Token: 0x0400185A RID: 6234
		private static HookList<ModPlayer> HookAddStartingItems = PlayerLoader.AddHook<Func<bool, IEnumerable<Item>>>((ModPlayer p) => (Func<bool, IEnumerable<Item>>)methodof(ModPlayer.AddStartingItems(bool)).CreateDelegate(typeof(Func<bool, IEnumerable<Item>>), p));

		// Token: 0x0400185B RID: 6235
		private static HookList<ModPlayer> HookModifyStartingInventory = PlayerLoader.AddHook<Action<IReadOnlyDictionary<string, List<Item>>, bool>>((ModPlayer p) => (Action<IReadOnlyDictionary<string, List<Item>>, bool>)methodof(ModPlayer.ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>>, bool)).CreateDelegate(typeof(Action<IReadOnlyDictionary<string, List<Item>>, bool>), p));

		// Token: 0x0400185C RID: 6236
		private static HookList<ModPlayer> HookAddCraftingMaterials = PlayerLoader.AddHook<PlayerLoader.DelegateFindMaterialsFrom>((ModPlayer p) => (PlayerLoader.DelegateFindMaterialsFrom)methodof(ModPlayer.AddMaterialsForCrafting(ModPlayer.ItemConsumedCallback*)).CreateDelegate(typeof(PlayerLoader.DelegateFindMaterialsFrom), p));

		// Token: 0x0400185D RID: 6237
		private static HookList<ModPlayer> HookOnPickup = PlayerLoader.AddHook<Func<Item, bool>>((ModPlayer p) => (Func<Item, bool>)methodof(ModPlayer.OnPickup(Item)).CreateDelegate(typeof(Func<Item, bool>), p));

		// Token: 0x0400185E RID: 6238
		private static HookList<ModPlayer> HookArmorSetBonusActivated = PlayerLoader.AddHook<Action>((ModPlayer p) => (Action)methodof(ModPlayer.ArmorSetBonusActivated()).CreateDelegate(typeof(Action), p));

		// Token: 0x0400185F RID: 6239
		private static HookList<ModPlayer> HookArmorSetBonusHeld = PlayerLoader.AddHook<Action<int>>((ModPlayer p) => (Action<int>)methodof(ModPlayer.ArmorSetBonusHeld(int)).CreateDelegate(typeof(Action<int>), p));

		// Token: 0x04001860 RID: 6240
		private static HookList<ModPlayer> HookCanBeTeleportedTo = PlayerLoader.AddHook<Func<Vector2, string, bool>>((ModPlayer p) => (Func<Vector2, string, bool>)methodof(ModPlayer.CanBeTeleportedTo(Vector2, string)).CreateDelegate(typeof(Func<Vector2, string, bool>), p));

		// Token: 0x04001861 RID: 6241
		private static HookList<ModPlayer> HookOnEquipmentLoadoutSwitched = PlayerLoader.AddHook<Action<int, int>>((ModPlayer p) => (Action<int, int>)methodof(ModPlayer.OnEquipmentLoadoutSwitched(int, int)).CreateDelegate(typeof(Action<int, int>), p));

		// Token: 0x0200098E RID: 2446
		// (Invoke) Token: 0x06005554 RID: 21844
		private delegate void DelegateModifyMaxStats(out StatModifier health, out StatModifier mana);

		// Token: 0x0200098F RID: 2447
		// (Invoke) Token: 0x06005558 RID: 21848
		private delegate void DelegateNaturalLifeRegen(ref float regen);

		// Token: 0x02000990 RID: 2448
		// (Invoke) Token: 0x0600555C RID: 21852
		private delegate void DelegateUpdateEquips();

		// Token: 0x02000991 RID: 2449
		// (Invoke) Token: 0x06005560 RID: 21856
		private delegate void DelegateModifyExtraJumpDuration(ExtraJump jump, ref float duration);

		// Token: 0x02000992 RID: 2450
		// (Invoke) Token: 0x06005564 RID: 21860
		private delegate void DelegateOnExtraJumpStarted(ExtraJump jump, ref bool playSound);

		// Token: 0x02000993 RID: 2451
		// (Invoke) Token: 0x06005568 RID: 21864
		private delegate void DelegateModifyHurt(ref Player.HurtModifiers modifiers);

		// Token: 0x02000994 RID: 2452
		// (Invoke) Token: 0x0600556C RID: 21868
		private delegate bool DelegatePreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource);

		// Token: 0x02000995 RID: 2453
		// (Invoke) Token: 0x06005570 RID: 21872
		private delegate bool DelegatePreModifyLuck(ref float luck);

		// Token: 0x02000996 RID: 2454
		// (Invoke) Token: 0x06005574 RID: 21876
		private delegate void DelegateModifyLuck(ref float luck);

		// Token: 0x02000997 RID: 2455
		// (Invoke) Token: 0x06005578 RID: 21880
		private delegate void DelegateGetHealLife(Item item, bool quickHeal, ref int healValue);

		// Token: 0x02000998 RID: 2456
		// (Invoke) Token: 0x0600557C RID: 21884
		private delegate void DelegateGetHealMana(Item item, bool quickHeal, ref int healValue);

		// Token: 0x02000999 RID: 2457
		// (Invoke) Token: 0x06005580 RID: 21888
		private delegate void DelegateModifyManaCost(Item item, ref float reduce, ref float mult);

		// Token: 0x0200099A RID: 2458
		// (Invoke) Token: 0x06005584 RID: 21892
		private delegate void DelegateModifyWeaponDamage(Item item, ref StatModifier damage);

		// Token: 0x0200099B RID: 2459
		// (Invoke) Token: 0x06005588 RID: 21896
		private delegate void DelegateModifyWeaponKnockback(Item item, ref StatModifier knockback);

		// Token: 0x0200099C RID: 2460
		// (Invoke) Token: 0x0600558C RID: 21900
		private delegate void DelegateModifyWeaponCrit(Item item, ref float crit);

		// Token: 0x0200099D RID: 2461
		// (Invoke) Token: 0x06005590 RID: 21904
		private delegate void DelegateModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback);

		// Token: 0x0200099E RID: 2462
		// (Invoke) Token: 0x06005594 RID: 21908
		private delegate void DelegateModifyItemScale(Item item, ref float scale);

		// Token: 0x0200099F RID: 2463
		// (Invoke) Token: 0x06005598 RID: 21912
		private delegate void DelegateModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers);

		// Token: 0x020009A0 RID: 2464
		// (Invoke) Token: 0x0600559C RID: 21916
		private delegate void DelegateModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers);

		// Token: 0x020009A1 RID: 2465
		// (Invoke) Token: 0x060055A0 RID: 21920
		private delegate void DelegateModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers);

		// Token: 0x020009A2 RID: 2466
		// (Invoke) Token: 0x060055A4 RID: 21924
		private delegate bool DelegateCanBeHitByNPC(NPC npc, ref int cooldownSlot);

		// Token: 0x020009A3 RID: 2467
		// (Invoke) Token: 0x060055A8 RID: 21928
		private delegate void DelegateModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers);

		// Token: 0x020009A4 RID: 2468
		// (Invoke) Token: 0x060055AC RID: 21932
		private delegate void DelegateModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers);

		// Token: 0x020009A5 RID: 2469
		// (Invoke) Token: 0x060055B0 RID: 21936
		private delegate void DelegateModifyFishingAttempt(ref FishingAttempt attempt);

		// Token: 0x020009A6 RID: 2470
		// (Invoke) Token: 0x060055B4 RID: 21940
		private delegate void DelegateCatchFish(FishingAttempt attempt, ref int itemDrop, ref int enemySpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition);

		// Token: 0x020009A7 RID: 2471
		// (Invoke) Token: 0x060055B8 RID: 21944
		private delegate void DelegateModifyCaughtFish(Item fish);

		// Token: 0x020009A8 RID: 2472
		// (Invoke) Token: 0x060055BC RID: 21948
		private delegate bool? DelegateCanConsumeBait(Item bait);

		// Token: 0x020009A9 RID: 2473
		// (Invoke) Token: 0x060055C0 RID: 21952
		private delegate void DelegateGetFishingLevel(Item fishingRod, Item bait, ref float fishingLevel);

		// Token: 0x020009AA RID: 2474
		// (Invoke) Token: 0x060055C4 RID: 21956
		private delegate void DelegateDrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright);

		// Token: 0x020009AB RID: 2475
		// (Invoke) Token: 0x060055C8 RID: 21960
		private delegate void DelegateModifyDrawInfo(ref PlayerDrawSet drawInfo);

		// Token: 0x020009AC RID: 2476
		// (Invoke) Token: 0x060055CC RID: 21964
		private delegate void DelegateModifyZoom(ref float zoom);

		// Token: 0x020009AD RID: 2477
		// (Invoke) Token: 0x060055D0 RID: 21968
		private delegate bool DelegateModifyNurseHeal(NPC npc, ref int health, ref bool removeDebuffs, ref string chatText);

		// Token: 0x020009AE RID: 2478
		// (Invoke) Token: 0x060055D4 RID: 21972
		private delegate void DelegateModifyNursePrice(NPC npc, int health, bool removeDebuffs, ref int price);

		// Token: 0x020009AF RID: 2479
		// (Invoke) Token: 0x060055D8 RID: 21976
		private delegate IEnumerable<Item> DelegateFindMaterialsFrom(out ModPlayer.ItemConsumedCallback onUsedForCrafting);
	}
}
