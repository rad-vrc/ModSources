using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.GameInput
{
	// Token: 0x02000482 RID: 1154
	public class LockOnHelper
	{
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x0058A3BA File Offset: 0x005885BA
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

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060037AB RID: 14251 RVA: 0x0058A3E8 File Offset: 0x005885E8
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
				int index;
				Vector2 pos;
				if (NPC.GetNPCLocation(LockOnHelper._targets[LockOnHelper._pickedTarget], true, false, out index, out pos))
				{
					vector = pos;
					vector += Main.npc[index].Distance(Main.player[Main.myPlayer].Center) / 2000f * Main.npc[index].velocity * 45f;
				}
				Player player = Main.player[Main.myPlayer];
				int num = ItemID.Sets.LockOnAimAbove[player.inventory[player.selectedItem].type];
				while (num > 0 && vector.Y > 100f)
				{
					Point point = vector.ToTileCoordinates();
					point.Y -= 4;
					if (!WorldGen.InWorld(point.X, point.Y, 10) || WorldGen.SolidTile(point.X, point.Y, false))
					{
						break;
					}
					vector.Y -= 16f;
					num--;
				}
				float? num2 = ItemID.Sets.LockOnAimCompensation[player.inventory[player.selectedItem].type];
				if (num2 != null)
				{
					vector.Y -= (float)(aimedTarget.height / 2);
					Vector2 v = vector - player.Center;
					Vector2 vector2 = v.SafeNormalize(Vector2.Zero);
					vector2.Y -= 1f;
					float num3 = v.Length();
					num3 = (float)Math.Pow((double)(num3 / 700f), 2.0) * 700f;
					vector.Y += vector2.Y * num3 * num2.Value * 1f;
					vector.X += (0f - vector2.X) * num3 * num2.Value * 1f;
				}
				return vector;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x0058A5E4 File Offset: 0x005887E4
		public static bool Enabled
		{
			get
			{
				return LockOnHelper._enabled;
			}
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x0058A5EC File Offset: 0x005887EC
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

		// Token: 0x060037AE RID: 14254 RVA: 0x0058A628 File Offset: 0x00588828
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
			if (LockOnHelper._enabled)
			{
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
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x0058A7A4 File Offset: 0x005889A4
		public static bool CanUseLockonSystem()
		{
			return LockOnHelper.ForceUsability || PlayerInput.UsingGamepad;
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x0058A7B4 File Offset: 0x005889B4
		public static void SetUP()
		{
			if (LockOnHelper._canLockOn)
			{
				NPC aimedTarget = LockOnHelper.AimedTarget;
				LockOnHelper.SetLockPosition(Main.ReverseGravitySupport(LockOnHelper.PredictedPosition - Main.screenPosition, 0f));
			}
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x0058A7E1 File Offset: 0x005889E1
		public static void SetDOWN()
		{
			if (LockOnHelper._canLockOn)
			{
				LockOnHelper.ResetLockPosition();
			}
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x0058A7EF File Offset: 0x005889EF
		private static bool ShouldLockOn(Player p)
		{
			return p.inventory[p.selectedItem].type != 496;
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x0058A80D File Offset: 0x00588A0D
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

		// Token: 0x060037B4 RID: 14260 RVA: 0x0058A82C File Offset: 0x00588A2C
		private static void FindMostViableTarget(LockOnHelper.LockOnMode context, ref int targetVar)
		{
			targetVar = -1;
			if (LockOnHelper.UseMode == context && LockOnHelper.CanUseLockonSystem())
			{
				List<int> t = new List<int>();
				int t2 = -1;
				Utils.Swap<List<int>>(ref t, ref LockOnHelper._targets);
				Utils.Swap<int>(ref t2, ref LockOnHelper._pickedTarget);
				LockOnHelper.RefreshTargets(Main.MouseWorld, 2000f);
				LockOnHelper.GetClosestTarget(Main.MouseWorld);
				Utils.Swap<List<int>>(ref t, ref LockOnHelper._targets);
				Utils.Swap<int>(ref t2, ref LockOnHelper._pickedTarget);
				if (t2 >= 0)
				{
					targetVar = t[t2];
				}
				t.Clear();
			}
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x0058A8B0 File Offset: 0x00588AB0
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

		// Token: 0x060037B6 RID: 14262 RVA: 0x0058A904 File Offset: 0x00588B04
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

		// Token: 0x060037B7 RID: 14263 RVA: 0x0058A990 File Offset: 0x00588B90
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

		// Token: 0x060037B8 RID: 14264 RVA: 0x0058AA06 File Offset: 0x00588C06
		private static bool CanEnable()
		{
			return !Main.player[Main.myPlayer].dead;
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x0058AA20 File Offset: 0x00588C20
		private static void SetActive(bool on)
		{
			if (on)
			{
				if (LockOnHelper.CanEnable())
				{
					LockOnHelper.RefreshTargets(Main.MouseWorld, 2000f);
					LockOnHelper.GetClosestTarget(Main.MouseWorld);
					if (LockOnHelper._pickedTarget >= 0)
					{
						LockOnHelper._enabled = true;
						return;
					}
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

		// Token: 0x060037BA RID: 14266 RVA: 0x0058AA84 File Offset: 0x00588C84
		private static void RefreshTargets(Vector2 position, float radius)
		{
			LockOnHelper._targets.Clear();
			Rectangle rectangle = Utils.CenteredRectangle(Main.player[Main.myPlayer].Center, new Vector2(1920f, 1200f));
			Vector2 center = Main.player[Main.myPlayer].Center;
			Main.player[Main.myPlayer].DirectionTo(Main.MouseWorld);
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC nPC = Main.npc[i];
				if (LockOnHelper.ValidTarget(nPC) && nPC.Distance(position) <= radius && rectangle.Intersects(nPC.Hitbox) && Lighting.GetSubLight(nPC.Center).Length() / 3f >= 0.03f)
				{
					LockOnHelper._targets.Add(i);
				}
			}
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x0058AB50 File Offset: 0x00588D50
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
				NPC nPC = Main.npc[num4];
				if (LockOnHelper.ValidTarget(nPC) && (LockOnHelper._pickedTarget == -1 || nPC.Distance(position) < num))
				{
					LockOnHelper._pickedTarget = j;
					num = nPC.Distance(position);
				}
			}
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x0058AC4C File Offset: 0x00588E4C
		private static bool ValidTarget(NPC n)
		{
			return n != null && n.active && !n.dontTakeDamage && !n.friendly && !n.isLikeATownNPC && n.life >= 1 && !n.immortal && (n.aiStyle != 25 || n.ai[0] != 0f);
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x0058ACAB File Offset: 0x00588EAB
		private static void SetLockPosition(Vector2 position)
		{
			PlayerInput.LockOnCachePosition();
			Main.mouseX = (PlayerInput.MouseX = (int)position.X);
			Main.mouseY = (PlayerInput.MouseY = (int)position.Y);
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x0058ACD6 File Offset: 0x00588ED6
		private static void ResetLockPosition()
		{
			PlayerInput.LockOnUnCachePosition();
			Main.mouseX = PlayerInput.MouseX;
			Main.mouseY = PlayerInput.MouseY;
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x0058ACF4 File Offset: 0x00588EF4
		public static void Draw(SpriteBatch spriteBatch)
		{
			if (Main.gameMenu)
			{
				return;
			}
			Texture2D value = TextureAssets.LockOnCursor.Value;
			Rectangle rectangle;
			rectangle..ctor(0, 0, value.Width, 12);
			Rectangle rectangle2;
			rectangle2..ctor(0, 16, value.Width, 12);
			Color t = Main.OurFavoriteColor.MultiplyRGBA(new Color(0.75f, 0.75f, 0.75f, 1f));
			t.A = 220;
			Color t2 = Main.OurFavoriteColor;
			t2.A = 220;
			float num = 0.94f + (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f)) * 0.06f;
			t2 *= num;
			t *= num;
			Utils.Swap<Color>(ref t, ref t2);
			Color color = t.MultiplyRGBA(new Color(0.8f, 0.8f, 0.8f, 0.8f));
			Color color2 = t.MultiplyRGBA(new Color(0.8f, 0.8f, 0.8f, 0.8f));
			float gravDir = Main.player[Main.myPlayer].gravDir;
			float num2 = 1f;
			float num3 = 0.1f;
			float num4 = 0.8f;
			float num5 = 1f;
			float num6 = 10f;
			float num7 = 10f;
			bool flag = false;
			for (int i = 0; i < LockOnHelper._drawProgress.GetLength(0); i++)
			{
				int num8 = 0;
				if (LockOnHelper._pickedTarget != -1 && LockOnHelper._targets.Count > 0 && i == LockOnHelper._targets[LockOnHelper._pickedTarget])
				{
					num8 = 2;
				}
				else if ((flag && LockOnHelper._targets.Contains(i)) || (LockOnHelper.UseMode == LockOnHelper.LockOnMode.ThreeDS && LockOnHelper._threeDSTarget == i) || (LockOnHelper.UseMode == LockOnHelper.LockOnMode.TargetClosest && LockOnHelper._targetClosestTarget == i))
				{
					num8 = 1;
				}
				LockOnHelper._drawProgress[i, 0] = MathHelper.Clamp(LockOnHelper._drawProgress[i, 0] + ((num8 == 1) ? num3 : (0f - num3)), 0f, 1f);
				LockOnHelper._drawProgress[i, 1] = MathHelper.Clamp(LockOnHelper._drawProgress[i, 1] + ((num8 == 2) ? num3 : (0f - num3)), 0f, 1f);
				float num9 = LockOnHelper._drawProgress[i, 0];
				if (num9 > 0f)
				{
					float num10 = 1f - num9 * num9;
					Vector2 pos = Main.npc[i].Top + new Vector2(0f, 0f - num7 - num10 * num6) * gravDir - Main.screenPosition;
					pos = Main.ReverseGravitySupport(pos, (float)Main.npc[i].height);
					spriteBatch.Draw(value, pos, new Rectangle?(rectangle), color * num9, 0f, rectangle.Size() / 2f, new Vector2(0.58f, 1f) * num2 * num4 * (1f + num9) / 2f, 0, 0f);
					spriteBatch.Draw(value, pos, new Rectangle?(rectangle2), color2 * num9 * num9, 0f, rectangle2.Size() / 2f, new Vector2(0.58f, 1f) * num2 * num4 * (1f + num9) / 2f, 0, 0f);
				}
				float num11 = LockOnHelper._drawProgress[i, 1];
				if (num11 > 0f)
				{
					int num12 = Main.npc[i].width;
					if (Main.npc[i].height > num12)
					{
						num12 = Main.npc[i].height;
					}
					num12 += 20;
					if ((float)num12 < 70f)
					{
						num5 *= (float)num12 / 70f;
					}
					float num13 = 3f;
					Vector2 vector = Main.npc[i].Center;
					int num15;
					Vector2 pos2;
					if (LockOnHelper._targets.Count >= 0 && LockOnHelper._pickedTarget >= 0 && LockOnHelper._pickedTarget < LockOnHelper._targets.Count && i == LockOnHelper._targets[LockOnHelper._pickedTarget] && NPC.GetNPCLocation(i, true, false, out num15, out pos2))
					{
						vector = pos2;
					}
					int j = 0;
					while ((float)j < num13)
					{
						float num14 = 6.2831855f / num13 * (float)j + Main.GlobalTimeWrappedHourly * 6.2831855f * 0.25f;
						Vector2 vector2 = new Vector2(0f, (float)(num12 / 2)).RotatedBy((double)num14, default(Vector2));
						Vector2 pos3 = vector + vector2 - Main.screenPosition;
						pos3 = Main.ReverseGravitySupport(pos3, 0f);
						float rotation = num14 * (float)((gravDir == 1f) ? 1 : -1) + 3.1415927f * (gravDir == 1f);
						spriteBatch.Draw(value, pos3, new Rectangle?(rectangle), t * num11, rotation, rectangle.Size() / 2f, new Vector2(0.58f, 1f) * num2 * num5 * (1f + num11) / 2f, 0, 0f);
						spriteBatch.Draw(value, pos3, new Rectangle?(rectangle2), t2 * num11 * num11, rotation, rectangle2.Size() / 2f, new Vector2(0.58f, 1f) * num2 * num5 * (1f + num11) / 2f, 0, 0f);
						j++;
					}
				}
			}
		}

		// Token: 0x0400515E RID: 20830
		private const float LOCKON_RANGE = 2000f;

		// Token: 0x0400515F RID: 20831
		private const int LOCKON_HOLD_LIFETIME = 40;

		// Token: 0x04005160 RID: 20832
		public static LockOnHelper.LockOnMode UseMode = LockOnHelper.LockOnMode.ThreeDS;

		// Token: 0x04005161 RID: 20833
		private static bool _enabled;

		// Token: 0x04005162 RID: 20834
		private static bool _canLockOn;

		// Token: 0x04005163 RID: 20835
		private static List<int> _targets = new List<int>();

		// Token: 0x04005164 RID: 20836
		private static int _pickedTarget;

		// Token: 0x04005165 RID: 20837
		private static int _lifeTimeCounter;

		// Token: 0x04005166 RID: 20838
		private static int _lifeTimeArrowDisplay;

		// Token: 0x04005167 RID: 20839
		private static int _threeDSTarget = -1;

		// Token: 0x04005168 RID: 20840
		private static int _targetClosestTarget = -1;

		// Token: 0x04005169 RID: 20841
		public static bool ForceUsability = false;

		// Token: 0x0400516A RID: 20842
		private static float[,] _drawProgress = new float[200, 2];

		// Token: 0x02000B8E RID: 2958
		public enum LockOnMode
		{
			// Token: 0x0400765B RID: 30299
			FocusTarget,
			// Token: 0x0400765C RID: 30300
			TargetClosest,
			// Token: 0x0400765D RID: 30301
			ThreeDS
		}
	}
}
