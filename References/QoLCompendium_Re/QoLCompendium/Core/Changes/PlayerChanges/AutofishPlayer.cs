using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.PlayerChanges
{
	// Token: 0x0200022C RID: 556
	public class AutofishPlayer : ModPlayer
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x00067FFC File Offset: 0x000661FC
		public override void PreUpdate()
		{
			if (base.Player.whoAmI != Main.myPlayer || !QoLCompendium.mainConfig.AutoFishing)
			{
				return;
			}
			this.ActivatedByMod = false;
			if (this.PullTimer > 0)
			{
				this.PullTimer--;
				if (this.PullTimer == 0)
				{
					base.Player.controlUseItem = true;
					base.Player.releaseUseItem = true;
					this.ActivatedByMod = true;
					base.Player.ItemCheck();
				}
			}
			if (!this.Autocast)
			{
				return;
			}
			this.AutocastDelay--;
			if (base.Player.HeldItem.fishingPole == 0)
			{
				this.Autocast = false;
				return;
			}
			if (this.AutocastDelay <= 0 && !AutofishPlayer.CheckBobbersActive(base.Player.whoAmI))
			{
				int mouseX = Main.mouseX;
				int mouseY = Main.mouseY;
				if (this.Lockcast)
				{
					Main.mouseX = this.CastPosition.X - (int)Main.screenPosition.X;
					Main.mouseY = this.CastPosition.Y - (int)Main.screenPosition.Y;
				}
				base.Player.controlUseItem = true;
				base.Player.releaseUseItem = true;
				this.ActivatedByMod = true;
				base.Player.ItemCheck();
				this.AutocastDelay = 10;
				if (this.Lockcast)
				{
					Main.mouseX = mouseX;
					Main.mouseY = mouseY;
				}
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0006815C File Offset: 0x0006635C
		public static bool CheckBobbersActive(int whoAmI)
		{
			bool result;
			using (IEnumerator<Projectile> enumerator = (from p in Main.projectile
			where p.active && p.owner == whoAmI && p.bobber
			select p).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Projectile projectile = enumerator.Current;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000681C4 File Offset: 0x000663C4
		public override void OnEnterWorld()
		{
			this.Lockcast = false;
			this.CastPosition = default(Point);
			this.Autocast = false;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x000681E0 File Offset: 0x000663E0
		public override void Load()
		{
			if (QoLCompendium.mainConfig.AutoFishing)
			{
				On_Player.ItemCheck_CheckFishingBobbers += new On_Player.hook_ItemCheck_CheckFishingBobbers(this.Player_ItemCheck_CheckFishingBobbers);
				On_Player.ItemCheck_Shoot += new On_Player.hook_ItemCheck_Shoot(this.Player_ItemCheck_Shoot);
				IL_Projectile.FishingCheck += new ILContext.Manipulator(this.Projectile_FishingCheck);
			}
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0006822C File Offset: 0x0006642C
		private bool Player_ItemCheck_CheckFishingBobbers(On_Player.orig_ItemCheck_CheckFishingBobbers orig, Player player, bool canUse)
		{
			bool flag = orig.Invoke(player, canUse);
			AutofishPlayer autofishPlayer;
			if (!flag && player.whoAmI == Main.myPlayer && player.TryGetModPlayer<AutofishPlayer>(out autofishPlayer) && !autofishPlayer.ActivatedByMod)
			{
				autofishPlayer.Autocast = false;
			}
			return flag;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0006826C File Offset: 0x0006646C
		private void Player_ItemCheck_Shoot(On_Player.orig_ItemCheck_Shoot orig, Player player, int i, Item sItem, int weaponDamage)
		{
			AutofishPlayer autofishPlayer;
			if (player.whoAmI == Main.myPlayer && player.TryGetModPlayer<AutofishPlayer>(out autofishPlayer) && !autofishPlayer.ActivatedByMod && sItem.fishingPole > 0)
			{
				autofishPlayer.Autocast = true;
			}
			orig.Invoke(player, i, sItem, weaponDamage);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000682B8 File Offset: 0x000664B8
		private void Projectile_FishingCheck(ILContext il)
		{
			ILCursor val = new ILCursor(il);
			ILCursor ilcursor = val;
			MoveType moveType = 2;
			Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
			array[0] = ((Instruction i) => ILPatternMatchingExt.MatchLdfld(i, typeof(FishingAttempt), "rolledItemDrop"));
			if (!ilcursor.TryGotoNext(moveType, array))
			{
				throw new Exception("Hook location not found, if (fisher.rolledItemDrop > 0)");
			}
			val.Emit(OpCodes.Ldarg_0);
			val.EmitDelegate<Func<int, Projectile, int>>(delegate(int caughtType, Projectile projectile)
			{
				if (projectile.owner != Main.myPlayer || !Main.player[projectile.owner].active || Main.player[projectile.owner].dead)
				{
					return caughtType;
				}
				AutofishPlayer modPlayer2 = Main.player[projectile.owner].GetModPlayer<AutofishPlayer>();
				if (modPlayer2.PullTimer == 0 && caughtType > 0)
				{
					modPlayer2.PullTimer = 31;
					return caughtType;
				}
				return caughtType;
			});
			val = new ILCursor(il);
			ILCursor ilcursor2 = val;
			MoveType moveType2 = 2;
			Func<Instruction, bool>[] array2 = new Func<Instruction, bool>[1];
			array2[0] = ((Instruction i) => ILPatternMatchingExt.MatchLdfld(i, typeof(FishingAttempt), "rolledEnemySpawn"));
			if (!ilcursor2.TryGotoNext(moveType2, array2))
			{
				throw new Exception("Hook location not found, if (fisher.rolledEnemySpawn > 0)");
			}
			val.Emit(OpCodes.Ldarg_0);
			val.EmitDelegate<Func<int, Projectile, int>>(delegate(int caughtType, Projectile projectile)
			{
				if (projectile.owner != Main.myPlayer || !Main.player[projectile.owner].active || Main.player[projectile.owner].dead)
				{
					return caughtType;
				}
				AutofishPlayer modPlayer = Main.player[projectile.owner].GetModPlayer<AutofishPlayer>();
				if (caughtType > 0 && modPlayer.PullTimer == 0)
				{
					modPlayer.PullTimer = 31;
				}
				return caughtType;
			});
		}

		// Token: 0x0400058D RID: 1421
		internal bool Lockcast;

		// Token: 0x0400058E RID: 1422
		internal Point CastPosition;

		// Token: 0x0400058F RID: 1423
		internal int PullTimer;

		// Token: 0x04000590 RID: 1424
		internal bool ActivatedByMod;

		// Token: 0x04000591 RID: 1425
		internal bool Autocast;

		// Token: 0x04000592 RID: 1426
		internal int AutocastDelay;
	}
}
