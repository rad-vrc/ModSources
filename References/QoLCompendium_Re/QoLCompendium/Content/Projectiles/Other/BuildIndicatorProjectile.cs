using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Other
{
	// Token: 0x02000022 RID: 34
	public class BuildIndicatorProjectile : ModProjectile
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00004050 File Offset: 0x00002250
		public override void SetDefaults()
		{
			base.Projectile.width = 16;
			base.Projectile.height = 16;
			base.Projectile.ignoreWater = true;
			base.Projectile.tileCollide = false;
			base.Projectile.timeLeft = 10;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000409C File Offset: 0x0000229C
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(new Color(0, 0, 0, 100));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000040B0 File Offset: 0x000022B0
		public override void OnSpawn(IEntitySource source)
		{
			this.oldMouse = Main.MouseWorld;
			EntitySource_ItemUse itemuseSource = source as EntitySource_ItemUse;
			if (itemuseSource != null)
			{
				this.item = itemuseSource.Item;
				this.previouslyLookingLeft = (itemuseSource.Player.direction < 0);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000040F4 File Offset: 0x000022F4
		public override void AI()
		{
			Player player = Main.player[base.Projectile.owner];
			Vector2 mouse = Main.MouseWorld;
			Vector2 delta = mouse - this.oldMouse;
			base.Projectile.position += delta;
			this.oldMouse = mouse;
			if (this.previouslyLookingLeft && player.direction == 1)
			{
				Projectile projectile = base.Projectile;
				projectile.position.X = projectile.position.X + (base.Projectile.position.X + this.oldMouse.X);
				this.previouslyLookingLeft = false;
			}
			else if (!this.previouslyLookingLeft && player.direction == -1)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.position.X = projectile2.position.X + (base.Projectile.position.X + this.oldMouse.X);
				this.previouslyLookingLeft = true;
			}
			base.Projectile.timeLeft++;
			if (player.HeldItem.type != this.item.type)
			{
				base.Projectile.Kill();
			}
			base.Projectile.hide = (base.Projectile.owner != Main.myPlayer);
		}

		// Token: 0x04000025 RID: 37
		private Vector2 oldMouse;

		// Token: 0x04000026 RID: 38
		private Item item;

		// Token: 0x04000027 RID: 39
		private bool previouslyLookingLeft;
	}
}
