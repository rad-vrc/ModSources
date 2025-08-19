using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.GameInput
{
	// Token: 0x02000136 RID: 310
	public class LockOnHelper
	{
		// Token: 0x060017BD RID: 6077 RVA: 0x004D57A0 File Offset: 0x004D39A0
		public static void CycleUseModes()
		{
			switch (LockOnHelper.UseMode)
			{
			case LockOnHelper.LockOnMode.FocusTarget:
				LockOnHelper.UseMode = LockOnHelper.LockOnMode.TargetClosest;
				return;
			case LockOnHelper.LockOnMode.TargetClosest:
				LockOnHelper.UseMode = LockOnHelper.LockOnMode.ThreeDS;
				return;
			case LockOnHelper.LockOnMode.ThreeDS:
				LockOnHelper.UseMode = LockOnHelper.LockOnMode.TargetClosest;
				return;
			default:
				return;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x004D57DA File Offset: 0x004D39DA
		public static NPC AimedTarget
		{
			get
			{
				if (LockOnHelper._pickedTarget == -1 || LockOnHelper._targets.Count < 1)
				{
					return null;
				}
				return Main.npc[LockOnHelper._targets[LockOnHelper._pickedTarget]];
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x004D5808 File Offset: 0x004D3A08
		public static Vector2 PredictedPosition
		{
			get
			{
				NPC aimedTarget = LockOnHelper.AimedTarget;
				if (aimedTarget == null)
				{
					return Vector2.Zero;
				}
				Vector2 vector = aimedTarget.Center;
				int num;
				Vector2 vector2;
				if (NPC.GetNPCLocation(LockOnHelper._targets[LockOnHelper._pickedTarget], true, false, out num, out vector2))
				{
					vector = vector2;
					vector += Main.npc[num].Distance(Main.player[Main.myPlayer].Center) / 2000f * Main.npc[num].velocity * 45f;
				}
				Player player = Main.player[Main.myPlayer];
				int num2 = ItemID.Sets.LockOnAimAbove[player.inventory[player.selectedItem].type];
				while (num2 > 0 && vector.Y > 100f)
				{
					Point point = vector.ToTileCoordinates();
					point.Y -= 4;
					if (!WorldGen.InWorld(point.X, point.Y, 10) || WorldGen.SolidTile(point.X, point.Y, false))
					{
						break;
					}
					vector.Y -= 16f;
					num2--;
				}
				float? num3 = ItemID.Sets.LockOnAimCompensation[player.inventory[player.selectedItem].type];
				if (num3 != null)
				{
					vector.Y -= (float)(aimedTarget.height / 2);
					Vector2 v = vector - player.Center;
					Vector2 vector3 = v.SafeNormalize(Vector2.Zero);
					vector3.Y -= 1f;
					float num4 = v.Length();
					num4 = (float)Math.Pow((double)(num4 / 700f), 2.0) * 700f;
					vector.Y += vector3.Y * num4 * num3.Value * 1f;
					vector.X += -vector3.X * num4 * num3.Value * 1f;
				}
				return vector;
			}
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x004D5A00 File Offset: 0x004D3C00
		public static void Update()
		{
			LockOnHelper._canLockOn = false;
			if (!LockOnHelper.CanUseLockonSystem())
			{
				LockOnHelper.SetActive(false);
				return;
			}
			if (--LockOnHelper._lifeTimeArrowDisplay < 0)
			{
				LockOnHelper._lifeTimeArrowDisplay = 0;
			}
			LockOnHelper.FindMostViableTarget(LockOnHelper.LockOnMode.ThreeDS, ref LockOnHelper._threeDSTarget);
			LockOnHelper.FindMostViableTarget(LockOnHelper.LockOnMode.TargetClosest, ref LockOnHelper._targetClosestTarget);
			if (PlayerInput.Triggers.JustPressed.LockOn && !PlayerInput.WritingText)
			{
				LockOnHelper._lifeTimeCounter = 40;
				LockOnHelper._lifeTimeArrowDisplay = 30;
				LockOnHelper.HandlePressing();
			}
			if (!LockOnHelper._enabled)
			{
				return;
			}
			if (LockOnHelper.UseMode == LockOnHelper.LockOnMode.FocusTarget && PlayerInput.Triggers.Current.LockOn)
			{
				if (LockOnHelper._lifeTimeCounter <= 0)
				{
					LockOnHelper.SetActive(false);
					return;
				}
				LockOnHelper._lifeTimeCounter--;
			}
			NPC aimedTarget = LockOnHelper.AimedTarget;
			if (!LockOnHelper.ValidTarget(aimedTarget))
			{
				LockOnHelper.SetActive(false);
			}
			if (LockOnHelper.UseMode == LockOnHelper.LockOnMode.TargetClosest)
			{
				LockOnHelper.SetActive(false);
				LockOnHelper.SetActive(LockOnHelper.CanEnable());
			}
			if (!LockOnHelper._enabled)
			{
				return;
			}
			Player player = Main.player[Main.myPlayer];
			Vector2 predictedPosition = LockOnHelper.PredictedPosition;
			bool flag = false;
			if (LockOnHelper.ShouldLockOn(player) && (ItemID.Sets.LockOnIgnoresCollision[player.inventory[player.selectedItem].type] || Collision.CanHit(player.Center, 0, 0, predictedPosition, 0, 0) || Collision.CanHitLine(player.Center, 0, 0, predictedPosition, 0, 0) || Collision.CanHit(player.Center, 0, 0, aimedTarget.Center, 0, 0) || Collision.CanHitLine(player.Center, 0, 0, aimedTarget.Center, 0, 0)))
			{
				flag = true;
			}
			if (flag)
			{
				LockOnHelper._canLockOn = true;
			}
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x004D5B7A File Offset: 0x004D3D7A
		public static bool CanUseLockonSystem()
		{
			return LockOnHelper.ForceUsability || PlayerInput.UsingGamepad;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x004D5B8A File Offset: 0x004D3D8A
		public static void SetUP()
		{
			if (!LockOnHelper._canLockOn)
			{
				return;
			}
			NPC aimedTarget = LockOnHelper.AimedTarget;
			LockOnHelper.SetLockPosition(Main.ReverseGravitySupport(LockOnHelper.PredictedPosition - Main.screenPosition, 0f));
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x004D5BB8 File Offset: 0x004D3DB8
		public static void SetDOWN()
		{
			if (!LockOnHelper._canLockOn)
			{
				return;
			}
			LockOnHelper.ResetLockPosition();
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x004D5BC8 File Offset: 0x004D3DC8
		private static bool ShouldLockOn(Player p)
		{
			int type = p.inventory[p.selectedItem].type;
			return type != 496;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x004D5BF3 File Offset: 0x004D3DF3
		public static void Toggle(bool forceOff = false)
		{
			LockOnHelper._lifeTimeCounter = 40;
			LockOnHelper._lifeTimeArrowDisplay = 30;
			LockOnHelper.HandlePressing();
			if (forceOff)
			{
				LockOnHelper._enabled = false;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x004D5C11 File Offset: 0x004D3E11
		public static bool Enabled
		{
			get
			{
				return LockOnHelper._enabled;
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x004D5C18 File Offset: 0x004D3E18
		private static void FindMostViableTarget(LockOnHelper.LockOnMode context, ref int targetVar)
		{
			targetVar = -1;
			if (LockOnHelper.UseMode != context)
			{
				return;
			}
			if (!LockOnHelper.CanUseLockonSystem())
			{
				return;
			}
			List<int> list = new List<int>();
			int num = -1;
			Utils.Swap<List<int>>(ref list, ref LockOnHelper._targets);
			Utils.Swap<int>(ref num, ref LockOnHelper._pickedTarget);
			LockOnHelper.RefreshTargets(Main.MouseWorld, 2000f);
			LockOnHelper.GetClosestTarget(Main.MouseWorld);
			Utils.Swap<List<int>>(ref list, ref LockOnHelper._targets);
			Utils.Swap<int>(ref num, ref LockOnHelper._pickedTarget);
			if (num >= 0)
			{
				targetVar = list[num];
			}
			list.Clear();
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x004D5CA0 File Offset: 0x004D3EA0
		private static void HandlePressing()
		{
			if (LockOnHelper.UseMode == LockOnHelper.LockOnMode.TargetClosest)
			{
				LockOnHelper.SetActive(!LockOnHelper._enabled);
				return;
			}
			if (LockOnHelper.UseMode == LockOnHelper.LockOnMode.ThreeDS)
			{
				if (!LockOnHelper._enabled)
				{
					LockOnHelper.SetActive(true);
					return;
				}
				LockOnHelper.CycleTargetThreeDS();
				return;
			}
			else
			{
				if (!LockOnHelper._enabled)
				{
					LockOnHelper.SetActive(true);
					return;
				}
				LockOnHelper.CycleTargetFocus();
				return;
			}
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x004D5CF4 File Offset: 0x004D3EF4
		private static void CycleTargetFocus()
		{
			int num = LockOnHelper._targets[LockOnHelper._pickedTarget];
			LockOnHelper.RefreshTargets(Main.MouseWorld, 2000f);
			if (LockOnHelper._targets.Count < 1 || (LockOnHelper._targets.Count == 1 && num == LockOnHelper._targets[0]))
			{
				LockOnHelper.SetActive(false);
				return;
			}
			LockOnHelper._pickedTarget = 0;
			for (int i = 0; i < LockOnHelper._targets.Count; i++)
			{
				if (LockOnHelper._targets[i] > num)
				{
					LockOnHelper._pickedTarget = i;
					return;
				}
			}
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x004D5D80 File Offset: 0x004D3F80
		private static void CycleTargetThreeDS()
		{
			int num = LockOnHelper._targets[LockOnHelper._pickedTarget];
			LockOnHelper.RefreshTargets(Main.MouseWorld, 2000f);
			LockOnHelper.GetClosestTarget(Main.MouseWorld);
			if (LockOnHelper._targets.Count < 1 || (LockOnHelper._targets.Count == 1 && num == LockOnHelper._targets[0]) || num == LockOnHelper._targets[LockOnHelper._pickedTarget])
			{
				LockOnHelper.SetActive(false);
			}
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x004D5DF6 File Offset: 0x004D3FF6
		private static bool CanEnable()
		{
			return !Main.player[Main.myPlayer].dead;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x004D5E10 File Offset: 0x004D4010
		private static void SetActive(bool on)
		{
			if (on)
			{
				if (!LockOnHelper.CanEnable())
				{
					return;
				}
				LockOnHelper.RefreshTargets(Main.MouseWorld, 2000f);
				LockOnHelper.GetClosestTarget(Main.MouseWorld);
				if (LockOnHelper._pickedTarget >= 0)
				{
					LockOnHelper._enabled = true;
					return;
				}
			}
			else
			{
				LockOnHelper._enabled = false;
				LockOnHelper._targets.Clear();
				LockOnHelper._lifeTimeCounter = 0;
				LockOnHelper._threeDSTarget = -1;
				LockOnHelper._targetClosestTarget = -1;
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x004D5E74 File Offset: 0x004D4074
		private static void RefreshTargets(Vector2 position, float radius)
		{
			LockOnHelper._targets.Clear();
			Rectangle rectangle = Utils.CenteredRectangle(Main.player[Main.myPlayer].Center, new Vector2(1920f, 1200f));
			Vector2 center = Main.player[Main.myPlayer].Center;
			Main.player[Main.myPlayer].DirectionTo(Main.MouseWorld);
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC npc = Main.npc[i];
				if (LockOnHelper.ValidTarget(npc) && npc.Distance(position) <= radius && rectangle.Intersects(npc.Hitbox) && Lighting.GetSubLight(npc.Center).Length() / 3f >= 0.03f)
				{
					LockOnHelper._targets.Add(i);
				}
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x004D5F40 File Offset: 0x004D4140
		private static void GetClosestTarget(Vector2 position)
		{
			LockOnHelper._pickedTarget = -1;
			float num = -1f;
			if (LockOnHelper.UseMode == LockOnHelper.LockOnMode.ThreeDS)
			{
				Vector2 center = Main.player[Main.myPlayer].Center;
				Vector2 value = Main.player[Main.myPlayer].DirectionTo(Main.MouseWorld);
				for (int i = 0; i < LockOnHelper._targets.Count; i++)
				{
					int num2 = LockOnHelper._targets[i];
					NPC npc = Main.npc[num2];
					float num3 = Vector2.Dot(npc.DirectionFrom(center), value);
					if (LockOnHelper.ValidTarget(npc) && (LockOnHelper._pickedTarget == -1 || num3 > num))
					{
						LockOnHelper._pickedTarget = i;
						num = num3;
					}
				}
				return;
			}
			for (int j = 0; j < LockOnHelper._targets.Count; j++)
			{
				int num4 = LockOnHelper._targets[j];
				NPC npc2 = Main.npc[num4];
				if (LockOnHelper.ValidTarget(npc2) && (LockOnHelper._pickedTarget == -1 || npc2.Distance(position) < num))
				{
					LockOnHelper._pickedTarget = j;
					num = npc2.Distance(position);
				}
			}
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x004D603C File Offset: 0x004D423C
		private static bool ValidTarget(NPC n)
		{
			return n != null && n.active && !n.dontTakeDamage && !n.friendly && !n.isLikeATownNPC && n.life >= 1 && !n.immortal && (n.aiStyle != 25 || n.ai[0] != 0f);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x004D609B File Offset: 0x004D429B
		private static void SetLockPosition(Vector2 position)
		{
			PlayerInput.LockOnCachePosition();
			Main.mouseX = (PlayerInput.MouseX = (int)position.X);
			Main.mouseY = (PlayerInput.MouseY = (int)position.Y);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x004D60C6 File Offset: 0x004D42C6
		private static void ResetLockPosition()
		{
			PlayerInput.LockOnUnCachePosition();
			Main.mouseX = PlayerInput.MouseX;
			Main.mouseY = PlayerInput.MouseY;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x004D60E4 File Offset: 0x004D42E4
		public static void Draw(SpriteBatch spriteBatch)
		{
			if (Main.gameMenu)
			{
				return;
			}
			Texture2D value = TextureAssets.LockOnCursor.Value;
			Rectangle rectangle = new Rectangle(0, 0, value.Width, 12);
			Rectangle rectangle2 = new Rectangle(0, 16, value.Width, 12);
			Color color = Main.OurFavoriteColor.MultiplyRGBA(new Color(0.75f, 0.75f, 0.75f, 1f));
			color.A = 220;
			Color value2 = Main.OurFavoriteColor;
			value2.A = 220;
			float scale = 0.94f + (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f)) * 0.06f;
			value2 *= scale;
			color *= scale;
			Utils.Swap<Color>(ref color, ref value2);
			Color value3 = color.MultiplyRGBA(new Color(0.8f, 0.8f, 0.8f, 0.8f));
			Color value4 = color.MultiplyRGBA(new Color(0.8f, 0.8f, 0.8f, 0.8f));
			float gravDir = Main.player[Main.myPlayer].gravDir;
			float scaleFactor = 1f;
			float num = 0.1f;
			float scaleFactor2 = 0.8f;
			float num2 = 1f;
			float num3 = 10f;
			float num4 = 10f;
			bool flag = false;
			for (int i = 0; i < LockOnHelper._drawProgress.GetLength(0); i++)
			{
				int num5 = 0;
				if (LockOnHelper._pickedTarget != -1 && LockOnHelper._targets.Count > 0 && i == LockOnHelper._targets[LockOnHelper._pickedTarget])
				{
					num5 = 2;
				}
				else if ((flag && LockOnHelper._targets.Contains(i)) || (LockOnHelper.UseMode == LockOnHelper.LockOnMode.ThreeDS && LockOnHelper._threeDSTarget == i) || (LockOnHelper.UseMode == LockOnHelper.LockOnMode.TargetClosest && LockOnHelper._targetClosestTarget == i))
				{
					num5 = 1;
				}
				LockOnHelper._drawProgress[i, 0] = MathHelper.Clamp(LockOnHelper._drawProgress[i, 0] + ((num5 == 1) ? num : (-num)), 0f, 1f);
				LockOnHelper._drawProgress[i, 1] = MathHelper.Clamp(LockOnHelper._drawProgress[i, 1] + ((num5 == 2) ? num : (-num)), 0f, 1f);
				float num6 = LockOnHelper._drawProgress[i, 0];
				if (num6 > 0f)
				{
					float num7 = 1f - num6 * num6;
					Vector2 vector = Main.npc[i].Top + new Vector2(0f, -num4 - num7 * num3) * gravDir - Main.screenPosition;
					vector = Main.ReverseGravitySupport(vector, (float)Main.npc[i].height);
					spriteBatch.Draw(value, vector, new Rectangle?(rectangle), value3 * num6, 0f, rectangle.Size() / 2f, new Vector2(0.58f, 1f) * scaleFactor * scaleFactor2 * (1f + num6) / 2f, SpriteEffects.None, 0f);
					spriteBatch.Draw(value, vector, new Rectangle?(rectangle2), value4 * num6 * num6, 0f, rectangle2.Size() / 2f, new Vector2(0.58f, 1f) * scaleFactor * scaleFactor2 * (1f + num6) / 2f, SpriteEffects.None, 0f);
				}
				float num8 = LockOnHelper._drawProgress[i, 1];
				if (num8 > 0f)
				{
					int num9 = Main.npc[i].width;
					if (Main.npc[i].height > num9)
					{
						num9 = Main.npc[i].height;
					}
					num9 += 20;
					if ((float)num9 < 70f)
					{
						num2 *= (float)num9 / 70f;
					}
					float num10 = 3f;
					Vector2 value5 = Main.npc[i].Center;
					int num11;
					Vector2 vector2;
					if (LockOnHelper._targets.Count >= 0 && LockOnHelper._pickedTarget >= 0 && LockOnHelper._pickedTarget < LockOnHelper._targets.Count && i == LockOnHelper._targets[LockOnHelper._pickedTarget] && NPC.GetNPCLocation(i, true, false, out num11, out vector2))
					{
						value5 = vector2;
					}
					int num12 = 0;
					while ((float)num12 < num10)
					{
						float num13 = 6.2831855f / num10 * (float)num12 + Main.GlobalTimeWrappedHourly * 6.2831855f * 0.25f;
						Vector2 value6 = new Vector2(0f, (float)(num9 / 2)).RotatedBy((double)num13, default(Vector2));
						Vector2 vector3 = value5 + value6 - Main.screenPosition;
						vector3 = Main.ReverseGravitySupport(vector3, 0f);
						float rotation = num13 * (float)((gravDir == 1f) ? 1 : -1) + 3.1415927f * (float)((gravDir == 1f) ? 1 : 0);
						spriteBatch.Draw(value, vector3, new Rectangle?(rectangle), color * num8, rotation, rectangle.Size() / 2f, new Vector2(0.58f, 1f) * scaleFactor * num2 * (1f + num8) / 2f, SpriteEffects.None, 0f);
						spriteBatch.Draw(value, vector3, new Rectangle?(rectangle2), value2 * num8 * num8, rotation, rectangle2.Size() / 2f, new Vector2(0.58f, 1f) * scaleFactor * num2 * (1f + num8) / 2f, SpriteEffects.None, 0f);
						num12++;
					}
				}
			}
		}

		// Token: 0x04001465 RID: 5221
		private const float LOCKON_RANGE = 2000f;

		// Token: 0x04001466 RID: 5222
		private const int LOCKON_HOLD_LIFETIME = 40;

		// Token: 0x04001467 RID: 5223
		public static LockOnHelper.LockOnMode UseMode = LockOnHelper.LockOnMode.ThreeDS;

		// Token: 0x04001468 RID: 5224
		private static bool _enabled;

		// Token: 0x04001469 RID: 5225
		private static bool _canLockOn;

		// Token: 0x0400146A RID: 5226
		private static List<int> _targets = new List<int>();

		// Token: 0x0400146B RID: 5227
		private static int _pickedTarget;

		// Token: 0x0400146C RID: 5228
		private static int _lifeTimeCounter;

		// Token: 0x0400146D RID: 5229
		private static int _lifeTimeArrowDisplay;

		// Token: 0x0400146E RID: 5230
		private static int _threeDSTarget = -1;

		// Token: 0x0400146F RID: 5231
		private static int _targetClosestTarget = -1;

		// Token: 0x04001470 RID: 5232
		public static bool ForceUsability = false;

		// Token: 0x04001471 RID: 5233
		private static float[,] _drawProgress = new float[200, 2];

		// Token: 0x020005A1 RID: 1441
		public enum LockOnMode
		{
			// Token: 0x04005A34 RID: 23092
			FocusTarget,
			// Token: 0x04005A35 RID: 23093
			TargetClosest,
			// Token: 0x04005A36 RID: 23094
			ThreeDS
		}
	}
}
