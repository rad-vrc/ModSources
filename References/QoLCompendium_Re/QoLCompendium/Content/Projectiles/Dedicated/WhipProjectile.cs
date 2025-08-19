using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Dedicated
{
	// Token: 0x0200003D RID: 61
	public abstract class WhipProjectile : ModProjectile
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00007DCB File Offset: 0x00005FCB
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.IsAWhip[base.Type] = true;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007F98 File Offset: 0x00006198
		public override void SetDefaults()
		{
			base.Projectile.friendly = true;
			base.Projectile.penetrate = -1;
			base.Projectile.tileCollide = false;
			base.Projectile.ownerHitCheck = true;
			base.Projectile.extraUpdates = 1;
			base.Projectile.usesLocalNPCImmunity = true;
			base.Projectile.localNPCHitCooldown = -1;
			base.Projectile.DamageType = DamageClass.Generic;
			this.whipSegment = (!0)ModContent.Request<Texture2D>("QoLCompendium/Content/Projectiles/Dedicated/SillySlapperWhip_Segment", 2);
			this.whipTip = (!0)ModContent.Request<Texture2D>("QoLCompendium/Content/Projectiles/Dedicated/SillySlapperWhip_Tip", 2);
			this.SetWhipStats();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000803B File Offset: 0x0000623B
		public override bool PreAI()
		{
			if ((double)(this.Timer % 2f) < 0.001)
			{
				this.whipPoints.Clear();
				Projectile.FillWhipControlPoints(base.Projectile, this.whipPoints);
			}
			return true;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00008074 File Offset: 0x00006274
		public virtual void SetWhipStats()
		{
			base.Projectile.width = 20;
			base.Projectile.height = 20;
			base.Projectile.WhipSettings.Segments = 30;
			base.Projectile.WhipSettings.RangeMultiplier = 1f;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000080C2 File Offset: 0x000062C2
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000080D1 File Offset: 0x000062D1
		internal float Timer
		{
			get
			{
				return base.Projectile.ai[0];
			}
			set
			{
				base.Projectile.ai[0] = value;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000080E1 File Offset: 0x000062E1
		public override bool PreDraw(ref Color lightColor)
		{
			return this.DrawWhip(this.fishingLineColor);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000080F0 File Offset: 0x000062F0
		internal bool DrawWhip(Color lineColor)
		{
			if (this.whipPoints == null || this.whipPoints.Count < 1)
			{
				return false;
			}
			WhipProjectile.DrawFishingLineBetweenPoints(this.whipPoints, lineColor, true);
			SpriteEffects spriteEffects = (base.Projectile.spriteDirection > 0) ? 0 : 2;
			Main.instance.LoadProjectile(base.Type);
			Texture2D value = TextureAssets.Projectile[base.Type].Value;
			Rectangle rectangle;
			rectangle..ctor(0, 0, value.Width, value.Height);
			Vector2 vector = rectangle.Size() / 2f;
			Vector2 vector2 = this.whipPoints[0];
			for (int i = 0; i < this.whipPoints.Count - 1; i++)
			{
				float num = 1f;
				if (i == this.whipPoints.Count - 2)
				{
					value = this.whipTip;
					rectangle..ctor(0, 0, value.Width, value.Height);
					vector = rectangle.Size() / 2f;
					float num2;
					int num5;
					float num6;
					Projectile.GetWhipSettings(base.Projectile, out num2, out num5, out num6);
					float num3 = this.Timer / num2;
					num = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, num3, true) * Utils.GetLerpValue(0.9f, 0.7f, num3, true));
				}
				else if (i > 0)
				{
					value = this.whipSegment;
					rectangle..ctor(0, 0, value.Width, value.Height);
					vector = rectangle.Size() / 2f;
				}
				Vector2 vector3 = this.whipPoints[i];
				Vector2 vector4 = this.whipPoints[i + 1] - vector3;
				float num4 = vector4.ToRotation() + this.segmentRotation;
				if (i == 0)
				{
					num4 = vector4.ToRotation();
				}
				Color color = Lighting.GetColor(vector3.ToTileCoordinates());
				if (this.drawColor != null)
				{
					color = this.drawColor.Value;
				}
				Main.EntitySpriteDraw(value, vector2 - Main.screenPosition, new Rectangle?(rectangle), color, num4, vector, num, spriteEffects, 0f);
				vector2 += vector4;
			}
			return false;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008318 File Offset: 0x00006518
		public virtual void WhipAIMotion()
		{
			Player player = Main.player[base.Projectile.owner];
			float num = (float)(player.itemAnimationMax * base.Projectile.MaxUpdates);
			if (this.runOnce)
			{
				base.Projectile.WhipSettings.Segments = (int)((player.whipRangeMultiplier + 1f) * (float)base.Projectile.WhipSettings.Segments);
				this.runOnce = false;
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + 1.5707964f;
			Entity projectile = base.Projectile;
			Vector2 center = base.Projectile.Center;
			List<Vector2> list = this.whipPoints;
			projectile.Center = Vector2.Lerp(center, list[list.Count - 1], 1f);
			base.Projectile.spriteDirection = ((base.Projectile.velocity.X >= 0f) ? 1 : -1);
			float timer = this.Timer;
			this.Timer = timer + 1f;
			if (this.Timer >= num || player.itemAnimation <= 0)
			{
				base.Projectile.Kill();
				return;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00008438 File Offset: 0x00006638
		public virtual void WhipSFX(Color lightingCol, int? dustID, int dustNum, SoundStyle? sound)
		{
			Player player = Main.player[base.Projectile.owner];
			float num = (float)(player.itemAnimationMax * base.Projectile.MaxUpdates);
			player.heldProj = base.Projectile.whoAmI;
			Vector2 tipPosition = this.GetTipPosition();
			if (this.Timer == num / 2f && sound != null)
			{
				SoundEngine.PlaySound(sound, new Vector2?(tipPosition), null);
			}
			if (this.Timer >= num * 0.5f)
			{
				if (dustID != null)
				{
					for (int i = 0; i < dustNum; i++)
					{
						Dust.NewDust(tipPosition, 2, 2, dustID.Value, 0f, 0f, 0, default(Color), 0.5f);
					}
				}
				if (lightingCol != Color.Transparent)
				{
					Lighting.AddLight(tipPosition, (float)lightingCol.R / 255f, (float)lightingCol.G / 255f, (float)lightingCol.B / 255f);
				}
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008532 File Offset: 0x00006732
		public override void AI()
		{
			this.WhipAIMotion();
			this.WhipSFX(this.lightingColor, this.swingDust, this.dustAmount, this.whipCrackSound);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00008558 File Offset: 0x00006758
		internal static void DrawFishingLineBetweenPoints(List<Vector2> list, Color lineCol, bool useLighCol = true)
		{
			Texture2D value = TextureAssets.FishingLine.Value;
			Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			Vector2 vector;
			vector..ctor((float)(rectangle.Width / 2), 2f);
			Vector2 vector2 = list[0];
			for (int i = 0; i < list.Count - 2; i++)
			{
				Vector2 vector3 = list[i];
				Vector2 vector4 = list[i + 1] - vector3;
				float num = vector4.ToRotation() - 1.5707964f;
				Color color = lineCol;
				if (useLighCol)
				{
					color = Lighting.GetColor(vector3.ToTileCoordinates(), lineCol);
				}
				Vector2 vector5;
				vector5..ctor(1f, (vector4.Length() + 2f) / (float)rectangle.Height);
				Main.EntitySpriteDraw(value, vector2 - Main.screenPosition, new Rectangle?(rectangle), color, num, vector, vector5, 0, 0f);
				vector2 += vector4;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008646 File Offset: 0x00006846
		internal Vector2 GetTipPosition()
		{
			List<Vector2> list = this.whipPoints;
			return list[list.Count - 2];
		}

		// Token: 0x04000028 RID: 40
		public Color fishingLineColor = Color.White;

		// Token: 0x04000029 RID: 41
		public Color lightingColor = Color.Transparent;

		// Token: 0x0400002A RID: 42
		public Color? drawColor;

		// Token: 0x0400002B RID: 43
		public int? swingDust;

		// Token: 0x0400002C RID: 44
		public int dustAmount;

		// Token: 0x0400002D RID: 45
		public SoundStyle? whipCrackSound = new SoundStyle?(SoundID.Item153);

		// Token: 0x0400002E RID: 46
		public Texture2D whipSegment;

		// Token: 0x0400002F RID: 47
		public Texture2D whipTip;

		// Token: 0x04000030 RID: 48
		public List<Vector2> whipPoints = new List<Vector2>();

		// Token: 0x04000031 RID: 49
		public int? tagType;

		// Token: 0x04000032 RID: 50
		public int tagDuration = 240;

		// Token: 0x04000033 RID: 51
		public float multihitModifier = 0.8f;

		// Token: 0x04000034 RID: 52
		public float segmentRotation;

		// Token: 0x04000035 RID: 53
		private bool runOnce = true;
	}
}
