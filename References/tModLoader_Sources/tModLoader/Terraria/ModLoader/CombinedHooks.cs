using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace Terraria.ModLoader
{
	// Token: 0x02000150 RID: 336
	public static class CombinedHooks
	{
		// Token: 0x06001B52 RID: 6994 RVA: 0x004CFDF8 File Offset: 0x004CDFF8
		public static void ModifyWeaponDamage(Player player, Item item, ref StatModifier damage)
		{
			ItemLoader.ModifyWeaponDamage(item, player, ref damage);
			PlayerLoader.ModifyWeaponDamage(player, item, ref damage);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x004CFE0A File Offset: 0x004CE00A
		public static void ModifyWeaponCrit(Player player, Item item, ref float crit)
		{
			ItemLoader.ModifyWeaponCrit(item, player, ref crit);
			PlayerLoader.ModifyWeaponCrit(player, item, ref crit);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x004CFE1C File Offset: 0x004CE01C
		public static void ModifyWeaponKnockback(Player player, Item item, ref StatModifier knockback)
		{
			ItemLoader.ModifyWeaponKnockback(item, player, ref knockback);
			PlayerLoader.ModifyWeaponKnockback(player, item, ref knockback);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x004CFE2E File Offset: 0x004CE02E
		public static void ModifyManaCost(Player player, Item item, ref float reduce, ref float mult)
		{
			ItemLoader.ModifyManaCost(item, player, ref reduce, ref mult);
			PlayerLoader.ModifyManaCost(player, item, ref reduce, ref mult);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x004CFE42 File Offset: 0x004CE042
		public static void OnConsumeMana(Player player, Item item, int manaConsumed)
		{
			ItemLoader.OnConsumeMana(item, player, manaConsumed);
			PlayerLoader.OnConsumeMana(player, item, manaConsumed);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x004CFE54 File Offset: 0x004CE054
		public static void OnMissingMana(Player player, Item item, int neededMana)
		{
			ItemLoader.OnMissingMana(item, player, neededMana);
			PlayerLoader.OnMissingMana(player, item, neededMana);
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x004CFE66 File Offset: 0x004CE066
		public static bool CanConsumeAmmo(Player player, Item weapon, Item ammo)
		{
			return PlayerLoader.CanConsumeAmmo(player, weapon, ammo) && ItemLoader.CanConsumeAmmo(weapon, ammo, player);
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x004CFE7C File Offset: 0x004CE07C
		public static void OnConsumeAmmo(Player player, Item weapon, Item ammo)
		{
			PlayerLoader.OnConsumeAmmo(player, weapon, ammo);
			ItemLoader.OnConsumeAmmo(weapon, ammo, player);
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x004CFE8E File Offset: 0x004CE08E
		public static bool CanUseItem(Player player, Item item)
		{
			return PlayerLoader.CanUseItem(player, item) && ItemLoader.CanUseItem(item, player);
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x004CFEA4 File Offset: 0x004CE0A4
		public static bool? CanAutoReuseItem(Player player, Item item)
		{
			CombinedHooks.<>c__DisplayClass9_0 CS$<>8__locals1;
			CS$<>8__locals1.ret = null;
			if (CombinedHooks.<CanAutoReuseItem>g__Update|9_0(PlayerLoader.CanAutoReuseItem(player, item), ref CS$<>8__locals1))
			{
				CombinedHooks.<CanAutoReuseItem>g__Update|9_0(ItemLoader.CanAutoReuseItem(item, player), ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.ret;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x004CFEE6 File Offset: 0x004CE0E6
		public static bool CanShoot(Player player, Item item)
		{
			return PlayerLoader.CanShoot(player, item) && ItemLoader.CanShoot(item, player);
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x004CFEFA File Offset: 0x004CE0FA
		public static void ModifyShootStats(Player player, Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			ItemLoader.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
			PlayerLoader.ModifyShootStats(player, item, ref position, ref velocity, ref type, ref damage, ref knockback);
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x004CFF1C File Offset: 0x004CE11C
		public static bool Shoot(Player player, Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			bool defaultResult = PlayerLoader.Shoot(player, item, source, position, velocity, type, damage, knockback);
			return ItemLoader.Shoot(item, player, source, position, velocity, type, damage, knockback, defaultResult);
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x004CFF50 File Offset: 0x004CE150
		public static bool? CanPlayerHitNPCWithItem(Player player, Item item, NPC npc)
		{
			CombinedHooks.<>c__DisplayClass13_0 CS$<>8__locals1;
			CS$<>8__locals1.ret = null;
			if (CombinedHooks.<CanPlayerHitNPCWithItem>g__Update|13_0(PlayerLoader.CanHitNPCWithItem(player, item, npc), ref CS$<>8__locals1) && CombinedHooks.<CanPlayerHitNPCWithItem>g__Update|13_0(ItemLoader.CanHitNPC(item, player, npc), ref CS$<>8__locals1))
			{
				CombinedHooks.<CanPlayerHitNPCWithItem>g__Update|13_0(NPCLoader.CanBeHitByItem(npc, player, item), ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.ret;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x004CFFA5 File Offset: 0x004CE1A5
		public static void ModifyPlayerHitNPCWithItem(Player player, Item sItem, NPC nPC, ref NPC.HitModifiers modifiers)
		{
			ItemLoader.ModifyHitNPC(sItem, player, nPC, ref modifiers);
			NPCLoader.ModifyHitByItem(nPC, player, sItem, ref modifiers);
			PlayerLoader.ModifyHitNPCWithItem(player, sItem, nPC, ref modifiers);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x004CFFC2 File Offset: 0x004CE1C2
		public static void OnPlayerHitNPCWithItem(Player player, Item sItem, NPC nPC, in NPC.HitInfo hit, int damageDone)
		{
			ItemLoader.OnHitNPC(sItem, player, nPC, hit, damageDone);
			NPCLoader.OnHitByItem(nPC, player, sItem, hit, damageDone);
			PlayerLoader.OnHitNPCWithItem(player, sItem, nPC, hit, damageDone);
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x004CFFE5 File Offset: 0x004CE1E5
		public static bool CanHitPvp(Player player, Item sItem, Player target)
		{
			return ItemLoader.CanHitPvp(sItem, player, target) && PlayerLoader.CanHitPvp(player, sItem, target);
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x004CFFFB File Offset: 0x004CE1FB
		public static void MeleeEffects(Player player, Item sItem, Rectangle itemRectangle)
		{
			ItemLoader.MeleeEffects(sItem, player, itemRectangle);
			PlayerLoader.MeleeEffects(player, sItem, itemRectangle);
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x004D0010 File Offset: 0x004CE210
		public static void EmitEnchantmentVisualsAt(Projectile projectile, Vector2 boxPosition, int boxWidth, int boxHeight)
		{
			ProjectileLoader.EmitEnchantmentVisualsAt(projectile, boxPosition, boxWidth, boxHeight);
			Player realPlayer;
			if (projectile.TryGetOwner(out realPlayer))
			{
				PlayerLoader.EmitEnchantmentVisualsAt(realPlayer, projectile, boxPosition, boxWidth, boxHeight);
			}
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x004D003C File Offset: 0x004CE23C
		public static bool? CanHitNPCWithProj(Projectile proj, NPC npc)
		{
			CombinedHooks.<>c__DisplayClass19_0 CS$<>8__locals1;
			CS$<>8__locals1.ret = null;
			Player player;
			if (CombinedHooks.<CanHitNPCWithProj>g__Update|19_0(proj.TryGetOwner(out player) ? PlayerLoader.CanHitNPCWithProj(player, proj, npc) : null, ref CS$<>8__locals1) && CombinedHooks.<CanHitNPCWithProj>g__Update|19_0(ProjectileLoader.CanHitNPC(proj, npc), ref CS$<>8__locals1))
			{
				CombinedHooks.<CanHitNPCWithProj>g__Update|19_0(NPCLoader.CanBeHitByProjectile(npc, proj), ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.ret;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x004D00A4 File Offset: 0x004CE2A4
		public static void ModifyHitNPCWithProj(Projectile projectile, NPC nPC, ref NPC.HitModifiers modifiers)
		{
			ProjectileLoader.ModifyHitNPC(projectile, nPC, ref modifiers);
			NPCLoader.ModifyHitByProjectile(nPC, projectile, ref modifiers);
			Player player;
			if (projectile.TryGetOwner(out player))
			{
				PlayerLoader.ModifyHitNPCWithProj(player, projectile, nPC, ref modifiers);
			}
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x004D00D4 File Offset: 0x004CE2D4
		public static void OnHitNPCWithProj(Projectile projectile, NPC nPC, in NPC.HitInfo hit, int damageDone)
		{
			ProjectileLoader.OnHitNPC(projectile, nPC, hit, damageDone);
			NPCLoader.OnHitByProjectile(nPC, projectile, hit, damageDone);
			Player player;
			if (projectile.TryGetOwner(out player))
			{
				PlayerLoader.OnHitNPCWithProj(player, projectile, nPC, hit, damageDone);
			}
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x004D0107 File Offset: 0x004CE307
		public static bool CanBeHitByProjectile(Player player, Projectile projectile)
		{
			return ProjectileLoader.CanHitPlayer(projectile, player) && PlayerLoader.CanBeHitByProjectile(player, projectile);
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x004D011C File Offset: 0x004CE31C
		public static void ModifyHitByProjectile(Player player, Projectile projectile, ref Player.HurtModifiers modifiers)
		{
			ProjectileLoader.ModifyHitPlayer(projectile, player, ref modifiers);
			PlayerLoader.ModifyHitByProjectile(player, projectile, ref modifiers);
			player.ApplyBannerDefenseBuff(projectile.bannerIdToRespondTo, ref modifiers);
			if (player.resistCold && projectile.coldDamage)
			{
				modifiers.IncomingDamageMultiplier *= 0.7f;
			}
			if (!projectile.reflected && !ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[projectile.type])
			{
				float damageMult = Main.GameModeInfo.EnemyDamageMultiplier;
				if (Main.GameModeInfo.IsJourneyMode)
				{
					CreativePowers.DifficultySliderPower power = CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>();
					if (power.GetIsUnlocked())
					{
						damageMult = power.StrengthMultiplierToGiveNPCs;
					}
				}
				modifiers.SourceDamage *= damageMult;
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x004D01D5 File Offset: 0x004CE3D5
		public static void OnHitByProjectile(Player player, Projectile projectile, in Player.HurtInfo hurtInfo)
		{
			ProjectileLoader.OnHitPlayer(projectile, player, hurtInfo);
			PlayerLoader.OnHitByProjectile(player, projectile, hurtInfo);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x004D01E7 File Offset: 0x004CE3E7
		public static bool CanHitPvpWithProj(Projectile projectile, Player target)
		{
			return ProjectileLoader.CanHitPvp(projectile, target) && PlayerLoader.CanHitPvpWithProj(projectile, target);
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x004D01FC File Offset: 0x004CE3FC
		public static bool? CanPlayerMeleeAttackCollideWithNPC(Player player, Item item, Rectangle meleeAttackHitbox, NPC target)
		{
			CombinedHooks.<>c__DisplayClass26_0 CS$<>8__locals1;
			CS$<>8__locals1.ret = null;
			if (CombinedHooks.<CanPlayerMeleeAttackCollideWithNPC>g__Update|26_0(PlayerLoader.CanMeleeAttackCollideWithNPC(player, item, meleeAttackHitbox, target), ref CS$<>8__locals1) && CombinedHooks.<CanPlayerMeleeAttackCollideWithNPC>g__Update|26_0(ItemLoader.CanMeleeAttackCollideWithNPC(item, meleeAttackHitbox, player, target), ref CS$<>8__locals1))
			{
				CombinedHooks.<CanPlayerMeleeAttackCollideWithNPC>g__Update|26_0(NPCLoader.CanCollideWithPlayerMeleeAttack(target, player, item, meleeAttackHitbox), ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.ret;
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x004D0254 File Offset: 0x004CE454
		public static void ModifyItemScale(Player player, Item item, ref float scale)
		{
			ItemLoader.ModifyItemScale(item, player, ref scale);
			PlayerLoader.ModifyItemScale(player, item, ref scale);
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x004D0266 File Offset: 0x004CE466
		public static float TotalUseSpeedMultiplier(Player player, Item item)
		{
			return PlayerLoader.UseSpeedMultiplier(player, item) * ItemLoader.UseSpeedMultiplier(item, player) * player.GetWeaponAttackSpeed(item);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x004D0280 File Offset: 0x004CE480
		public static float TotalUseTimeMultiplier(Player player, Item item)
		{
			float useTimeMult = PlayerLoader.UseTimeMultiplier(player, item) * ItemLoader.UseTimeMultiplier(item, player);
			if (!item.attackSpeedOnlyAffectsWeaponAnimation)
			{
				useTimeMult /= CombinedHooks.TotalUseSpeedMultiplier(player, item);
			}
			return useTimeMult;
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x004D02B0 File Offset: 0x004CE4B0
		public static int TotalUseTime(float useTime, Player player, Item item)
		{
			return Math.Max(1, (int)(useTime * CombinedHooks.TotalUseTimeMultiplier(player, item)));
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x004D02C4 File Offset: 0x004CE4C4
		public static float TotalUseAnimationMultiplier(Player player, Item item)
		{
			float num = PlayerLoader.UseAnimationMultiplier(player, item) * ItemLoader.UseAnimationMultiplier(item, player);
			int multipliedUseTime = Math.Max(1, (int)((float)item.useTime * (1f / CombinedHooks.TotalUseSpeedMultiplier(player, item))));
			int relativeUseAnimation = Math.Max(1, multipliedUseTime * item.useAnimation / item.useTime);
			return num * ((float)relativeUseAnimation / (float)item.useAnimation);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x004D031E File Offset: 0x004CE51E
		public static int TotalAnimationTime(float useAnimation, Player player, Item item)
		{
			return Math.Max(1, (int)(useAnimation * CombinedHooks.TotalUseAnimationMultiplier(player, item)));
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x004D0330 File Offset: 0x004CE530
		public static bool? CanConsumeBait(Player player, Item item)
		{
			bool? ret = PlayerLoader.CanConsumeBait(player, item);
			bool? flag = ItemLoader.CanConsumeBait(player, item);
			if (flag != null)
			{
				bool b = flag.GetValueOrDefault();
				ret = new bool?(ret.GetValueOrDefault(true) && b);
			}
			return ret;
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x004D0370 File Offset: 0x004CE570
		public static bool? CanCatchNPC(Player player, NPC npc, Item item)
		{
			CombinedHooks.<>c__DisplayClass34_0 CS$<>8__locals1;
			CS$<>8__locals1.ret = null;
			if (CombinedHooks.<CanCatchNPC>g__Update|34_0(PlayerLoader.CanCatchNPC(player, npc, item), ref CS$<>8__locals1) && CombinedHooks.<CanCatchNPC>g__Update|34_0(ItemLoader.CanCatchNPC(item, npc, player), ref CS$<>8__locals1))
			{
				CombinedHooks.<CanCatchNPC>g__Update|34_0(NPCLoader.CanBeCaughtBy(npc, item, player), ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.ret;
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x004D03C5 File Offset: 0x004CE5C5
		public static void OnCatchNPC(Player player, NPC npc, Item item, bool failed)
		{
			PlayerLoader.OnCatchNPC(player, npc, item, failed);
			ItemLoader.OnCatchNPC(item, npc, player, failed);
			NPCLoader.OnCaughtBy(npc, player, item, failed);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x004D03E2 File Offset: 0x004CE5E2
		public static bool CanNPCHitPlayer(NPC nPC, Player player, ref int specialHitSetter)
		{
			return NPCLoader.CanHitPlayer(nPC, player, ref specialHitSetter) && PlayerLoader.CanBeHitByNPC(player, nPC, ref specialHitSetter);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x004D03F8 File Offset: 0x004CE5F8
		public static void ModifyHitByNPC(Player player, NPC nPC, ref Player.HurtModifiers modifiers)
		{
			NPCLoader.ModifyHitPlayer(nPC, player, ref modifiers);
			PlayerLoader.ModifyHitByNPC(player, nPC, ref modifiers);
			player.ApplyBannerDefenseBuff(nPC, ref modifiers);
			if (player.resistCold && nPC.coldDamage)
			{
				modifiers.IncomingDamageMultiplier *= 0.7f;
			}
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x004D0448 File Offset: 0x004CE648
		public static void OnHitByNPC(Player player, NPC nPC, in Player.HurtInfo hurtInfo)
		{
			NPCLoader.OnHitPlayer(nPC, player, hurtInfo);
			PlayerLoader.OnHitByNPC(player, nPC, hurtInfo);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x004D045F File Offset: 0x004CE65F
		public static void PlayerFrameEffects(Player player)
		{
			PlayerLoader.FrameEffects(player);
			EquipLoader.EquipFrameEffects(player);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x004D046D File Offset: 0x004CE66D
		public static bool OnPickup(Item item, Player player)
		{
			return ItemLoader.OnPickup(item, player) && PlayerLoader.OnPickup(player, item);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x004D0484 File Offset: 0x004CE684
		public unsafe static bool CanBeTeleportedTo(Player player, Vector2 teleportPosition, int i, int j, string context)
		{
			return PlayerLoader.CanBeTeleportedTo(player, teleportPosition, context) && WallLoader.CanBeTeleportedTo(i, j, (int)(*Main.tile[i, j].WallType), player, context);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x004D04C0 File Offset: 0x004CE6C0
		[CompilerGenerated]
		internal static bool <CanAutoReuseItem>g__Update|9_0(bool? b, ref CombinedHooks.<>c__DisplayClass9_0 A_1)
		{
			bool? ret = A_1.ret;
			bool? flag;
			if (ret == null)
			{
				A_1.ret = b;
				flag = b;
			}
			else
			{
				flag = ret;
			}
			bool? flag2 = flag;
			return flag2 == null || flag2.GetValueOrDefault();
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x004D0504 File Offset: 0x004CE704
		[CompilerGenerated]
		internal static bool <CanPlayerHitNPCWithItem>g__Update|13_0(bool? b, ref CombinedHooks.<>c__DisplayClass13_0 A_1)
		{
			bool? ret = A_1.ret;
			bool? flag;
			if (ret == null)
			{
				A_1.ret = b;
				flag = b;
			}
			else
			{
				flag = ret;
			}
			bool? flag2 = flag;
			return flag2 == null || flag2.GetValueOrDefault();
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x004D0548 File Offset: 0x004CE748
		[CompilerGenerated]
		internal static bool <CanHitNPCWithProj>g__Update|19_0(bool? b, ref CombinedHooks.<>c__DisplayClass19_0 A_1)
		{
			bool? ret = A_1.ret;
			bool? flag;
			if (ret == null)
			{
				A_1.ret = b;
				flag = b;
			}
			else
			{
				flag = ret;
			}
			bool? flag2 = flag;
			return flag2 == null || flag2.GetValueOrDefault();
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x004D058C File Offset: 0x004CE78C
		[CompilerGenerated]
		internal static bool <CanPlayerMeleeAttackCollideWithNPC>g__Update|26_0(bool? b, ref CombinedHooks.<>c__DisplayClass26_0 A_1)
		{
			bool? ret = A_1.ret;
			bool? flag;
			if (ret == null)
			{
				A_1.ret = b;
				flag = b;
			}
			else
			{
				flag = ret;
			}
			bool? flag2 = flag;
			return flag2 == null || flag2.GetValueOrDefault();
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x004D05D0 File Offset: 0x004CE7D0
		[CompilerGenerated]
		internal static bool <CanCatchNPC>g__Update|34_0(bool? b, ref CombinedHooks.<>c__DisplayClass34_0 A_1)
		{
			bool? ret = A_1.ret;
			bool? flag;
			if (ret == null)
			{
				A_1.ret = b;
				flag = b;
			}
			else
			{
				flag = ret;
			}
			bool? flag2 = flag;
			return flag2 == null || flag2.GetValueOrDefault();
		}
	}
}
