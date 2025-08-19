using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Terraria.ModLoader
{
	// Token: 0x02000160 RID: 352
	public static class ExtraJumpLoader
	{
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x004D2951 File Offset: 0x004D0B51
		public static int ExtraJumpCount
		{
			get
			{
				return ExtraJumpLoader.ExtraJumps.Count;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x004D295D File Offset: 0x004D0B5D
		private static IEnumerable<ExtraJump> ModdedExtraJumps
		{
			get
			{
				return ExtraJumpLoader.ExtraJumps.Skip(ExtraJumpLoader.DefaultExtraJumpCount);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x004D296E File Offset: 0x004D0B6E
		public static IReadOnlyList<ExtraJump> OrderedJumps
		{
			get
			{
				return ExtraJumpLoader.orderedJumps;
			}
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x004D2978 File Offset: 0x004D0B78
		static ExtraJumpLoader()
		{
			ExtraJumpLoader.RegisterDefaultJumps();
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x004D2A11 File Offset: 0x004D0C11
		internal static int Add(ExtraJump jump)
		{
			ExtraJumpLoader.ExtraJumps.Add(jump);
			return ExtraJumpLoader.ExtraJumps.Count - 1;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x004D2A2A File Offset: 0x004D0C2A
		public static ExtraJump Get(int index)
		{
			if (index >= 0 && index < ExtraJumpLoader.ExtraJumpCount)
			{
				return ExtraJumpLoader.ExtraJumps[index];
			}
			return null;
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x004D2A45 File Offset: 0x004D0C45
		internal static void Unload()
		{
			ExtraJumpLoader.ExtraJumps.RemoveRange(ExtraJumpLoader.DefaultExtraJumpCount, ExtraJumpLoader.ExtraJumpCount - ExtraJumpLoader.DefaultExtraJumpCount);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x004D2A64 File Offset: 0x004D0C64
		internal static void ResizeArrays()
		{
			if (!ExtraJumpLoader.ModdedExtraJumps.Any<ExtraJump>())
			{
				ExtraJumpLoader.orderedJumps = ExtraJumpLoader.ExtraJumps.ToArray();
				return;
			}
			List<ExtraJump>[] sortingSlots = new List<ExtraJump>[ExtraJumpLoader.DefaultExtraJumpCount + 1];
			for (int i = 0; i < sortingSlots.Length; i++)
			{
				sortingSlots[i] = new List<ExtraJump>();
			}
			foreach (ExtraJump jump in ExtraJumpLoader.ModdedExtraJumps)
			{
				ExtraJump.Position position = jump.GetDefaultPosition();
				ExtraJump.After after = position as ExtraJump.After;
				if (after == null)
				{
					ExtraJump.Before before = position as ExtraJump.Before;
					if (before == null)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 2);
						defaultInterpolatedStringHandler.AppendLiteral("ExtraJump ");
						defaultInterpolatedStringHandler.AppendFormatted<ExtraJump>(jump);
						defaultInterpolatedStringHandler.AppendLiteral(" has unknown Position ");
						defaultInterpolatedStringHandler.AppendFormatted<ExtraJump.Position>(position);
						throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					ExtraJump target = before.Target;
					if (target != null && !(target is VanillaExtraJump))
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(71, 1);
						defaultInterpolatedStringHandler.AppendLiteral("ExtraJump ");
						defaultInterpolatedStringHandler.AppendFormatted<ExtraJump>(jump);
						defaultInterpolatedStringHandler.AppendLiteral(" did not refer to a vanilla ExtraJump in GetDefaultPosition()");
						throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					ExtraJump target2 = before.Target;
					int? num = (target2 != null) ? new int?(target2.Type) : null;
					int num2;
					if (num != null)
					{
						int beforeType = num.GetValueOrDefault();
						num2 = beforeType;
					}
					else
					{
						num2 = sortingSlots.Length - 1;
					}
					int beforeParent = num2;
					sortingSlots[beforeParent].Add(jump);
				}
				else
				{
					ExtraJump target = after.Target;
					if (target != null && !(target is VanillaExtraJump))
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(71, 1);
						defaultInterpolatedStringHandler.AppendLiteral("ExtraJump ");
						defaultInterpolatedStringHandler.AppendFormatted<ExtraJump>(jump);
						defaultInterpolatedStringHandler.AppendLiteral(" did not refer to a vanilla ExtraJump in GetDefaultPosition()");
						throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					ExtraJump target3 = after.Target;
					int? num = (target3 != null) ? new int?(target3.Type) : null;
					int num3;
					if (num != null)
					{
						int afterType = num.GetValueOrDefault();
						num3 = afterType + 1;
					}
					else
					{
						num3 = 0;
					}
					int afterParent = num3;
					sortingSlots[afterParent].Add(jump);
				}
			}
			List<ExtraJump> sorted = new List<ExtraJump>();
			for (int k = 0; k < ExtraJumpLoader.DefaultExtraJumpCount + 1; k++)
			{
				List<ExtraJump> elements = sortingSlots[k];
				foreach (ExtraJump jump2 in new TopoSort<ExtraJump>(elements, delegate(ExtraJump j)
				{
					IEnumerable<ExtraJump.Position> moddedConstraints = j.GetModdedConstraints();
					IEnumerable<ExtraJump> enumerable;
					if (moddedConstraints == null)
					{
						enumerable = null;
					}
					else
					{
						enumerable = (from a in moddedConstraints.OfType<ExtraJump.After>()
						select a.Target).Where(new Func<ExtraJump, bool>(elements.Contains));
					}
					return enumerable ?? Array.Empty<ExtraJump>();
				}, delegate(ExtraJump j)
				{
					IEnumerable<ExtraJump.Position> moddedConstraints = j.GetModdedConstraints();
					IEnumerable<ExtraJump> enumerable;
					if (moddedConstraints == null)
					{
						enumerable = null;
					}
					else
					{
						enumerable = (from b in moddedConstraints.OfType<ExtraJump.Before>()
						select b.Target).Where(new Func<ExtraJump, bool>(elements.Contains));
					}
					return enumerable ?? Array.Empty<ExtraJump>();
				}).Sort())
				{
					sorted.Add(jump2);
				}
				if (k < ExtraJumpLoader.DefaultExtraJumpCount)
				{
					sorted.Add(ExtraJumpLoader.ExtraJumps[k]);
				}
			}
			ExtraJumpLoader.orderedJumps = sorted.ToArray();
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x004D2D74 File Offset: 0x004D0F74
		internal static void RegisterDefaultJumps()
		{
			int i = 0;
			foreach (ExtraJump extraJump in ExtraJumpLoader.ExtraJumps)
			{
				extraJump.Type = i++;
				ContentInstance.Register(extraJump);
				ModTypeLookup<ExtraJump>.Register(extraJump);
			}
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x004D2DD8 File Offset: 0x004D0FD8
		public static void UpdateHorizontalSpeeds(Player player)
		{
			foreach (ExtraJump moddedExtraJump in ExtraJumpLoader.orderedJumps)
			{
				if (player.GetJumpState<ExtraJump>(moddedExtraJump).Active)
				{
					moddedExtraJump.UpdateHorizontalSpeeds(player);
				}
			}
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x004D2E14 File Offset: 0x004D1014
		public static void JumpVisuals(Player player)
		{
			foreach (ExtraJump jump in ExtraJumpLoader.orderedJumps)
			{
				if (player.GetJumpState<ExtraJump>(jump).Active && jump.CanShowVisuals(player) && PlayerLoader.CanShowExtraJumpVisuals(jump, player))
				{
					jump.ShowVisuals(player);
					PlayerLoader.ExtraJumpVisuals(jump, player);
				}
			}
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x004D2E68 File Offset: 0x004D1068
		public static void ProcessJumps(Player player)
		{
			foreach (ExtraJump jump in ExtraJumpLoader.orderedJumps)
			{
				ref ExtraJumpState state = ref player.GetJumpState<ExtraJump>(jump);
				if (state.Available && jump.CanStart(player) && PlayerLoader.CanStartExtraJump(jump, player))
				{
					state.Start();
					ExtraJumpLoader.PerformJump(jump, player);
					return;
				}
			}
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x004D2EC0 File Offset: 0x004D10C0
		public static void RefreshJumps(Player player)
		{
			foreach (ExtraJump jump in ExtraJumpLoader.orderedJumps)
			{
				ref ExtraJumpState state = ref player.GetJumpState<ExtraJump>(jump);
				if (state.Enabled)
				{
					jump.OnRefreshed(player);
					PlayerLoader.OnExtraJumpRefreshed(jump, player);
					state.Available = true;
				}
			}
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x004D2F0C File Offset: 0x004D110C
		internal static void StopActiveJump(Player player, out bool anyJumpCancelled)
		{
			anyJumpCancelled = false;
			foreach (ExtraJump jump in ExtraJumpLoader.orderedJumps)
			{
				if (player.GetJumpState<ExtraJump>(jump).Active)
				{
					ExtraJumpLoader.StopJump(jump, player);
					anyJumpCancelled = true;
				}
			}
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x004D2F4C File Offset: 0x004D114C
		internal static void ResetEnableFlags(Player player)
		{
			foreach (ExtraJump jump in ExtraJumpLoader.ExtraJumps)
			{
				player.GetJumpState<ExtraJump>(jump).ResetEnabled();
			}
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x004D2FA4 File Offset: 0x004D11A4
		internal static void ConsumeAndStopUnavailableJumps(Player player)
		{
			foreach (ExtraJump jump in ExtraJumpLoader.ExtraJumps)
			{
				bool jumpEnded;
				player.GetJumpState<ExtraJump>(jump).CommitEnabledState(out jumpEnded);
				if (jumpEnded)
				{
					ExtraJumpLoader.StopJump(jump, player);
					player.jump = 0;
				}
			}
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x004D3010 File Offset: 0x004D1210
		internal static void ConsumeAllJumps(Player player)
		{
			foreach (ExtraJump jump in ExtraJumpLoader.ExtraJumps)
			{
				player.GetJumpState<ExtraJump>(jump).Available = false;
			}
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x004D3068 File Offset: 0x004D1268
		private static void PerformJump(ExtraJump jump, Player player)
		{
			float duration = jump.GetDurationMultiplier(player);
			PlayerLoader.ModifyExtraJumpDurationMultiplier(jump, player, ref duration);
			player.velocity.Y = -Player.jumpSpeed * player.gravDir;
			player.jump = (int)((float)Player.jumpHeight * duration);
			bool playSound = true;
			jump.OnStarted(player, ref playSound);
			PlayerLoader.OnExtraJumpStarted(jump, player, ref playSound);
			if (playSound)
			{
				SoundEngine.PlaySound(16, (int)player.position.X, (int)player.position.Y, 1, 1f, 0f);
			}
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x004D30EF File Offset: 0x004D12EF
		private static void StopJump(ExtraJump jump, Player player)
		{
			jump.OnEnded(player);
			PlayerLoader.OnExtraJumpEnded(jump, player);
			player.GetJumpState<ExtraJump>(jump).Stop();
		}

		// Token: 0x0400151B RID: 5403
		internal static readonly List<ExtraJump> ExtraJumps = new List<ExtraJump>
		{
			ExtraJump.Flipper,
			ExtraJump.BasiliskMount,
			ExtraJump.GoatMount,
			ExtraJump.SantankMount,
			ExtraJump.UnicornMount,
			ExtraJump.SandstormInABottle,
			ExtraJump.BlizzardInABottle,
			ExtraJump.FartInAJar,
			ExtraJump.TsunamiInABottle,
			ExtraJump.CloudInABottle
		};

		// Token: 0x0400151C RID: 5404
		private static readonly int DefaultExtraJumpCount = ExtraJumpLoader.ExtraJumps.Count;

		// Token: 0x0400151D RID: 5405
		private static ExtraJump[] orderedJumps;
	}
}
