using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as the central place from which mounts are stored and mount-related functions are carried out.
	/// </summary>
	// Token: 0x020001D9 RID: 473
	public static class MountLoader
	{
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x004EAD23 File Offset: 0x004E8F23
		// (set) Token: 0x060024DC RID: 9436 RVA: 0x004EAD2A File Offset: 0x004E8F2A
		public static int MountCount { get; private set; } = MountID.Count;

		/// <summary>
		/// Gets the ModMount instance corresponding to the given type. Returns null if no ModMount has the given type.
		/// </summary>
		/// <param name="type">The type of the mount.</param>
		/// <returns>Null if not found, otherwise the ModMount associated with the mount.</returns>
		// Token: 0x060024DD RID: 9437 RVA: 0x004EAD32 File Offset: 0x004E8F32
		public static ModMount GetMount(int type)
		{
			if (MountLoader.mountDatas.ContainsKey(type))
			{
				return MountLoader.mountDatas[type];
			}
			return null;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x004EAD4E File Offset: 0x004E8F4E
		internal static int ReserveMountID()
		{
			return MountLoader.MountCount++;
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x004EAD5D File Offset: 0x004E8F5D
		internal static void ResizeArrays()
		{
			LoaderUtils.ResetStaticMembers(typeof(MountID), true);
			Array.Resize<Mount.MountData>(ref Mount.mounts, MountLoader.MountCount);
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x004EAD7E File Offset: 0x004E8F7E
		internal static void Unload()
		{
			MountLoader.mountDatas.Clear();
			MountLoader.MountCount = MountID.Count;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x004EAD94 File Offset: 0x004E8F94
		internal static bool IsModMount(Mount.MountData mountData)
		{
			return mountData.ModMount != null;
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x004EAD9F File Offset: 0x004E8F9F
		public static void JumpHeight(Player mountedPlayer, Mount.MountData mount, ref int jumpHeight, float xVelocity)
		{
			if (MountLoader.IsModMount(mount))
			{
				mount.ModMount.JumpHeight(mountedPlayer, ref jumpHeight, xVelocity);
			}
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x004EADB7 File Offset: 0x004E8FB7
		public static void JumpSpeed(Player mountedPlayer, Mount.MountData mount, ref float jumpSpeed, float xVelocity)
		{
			if (MountLoader.IsModMount(mount))
			{
				mount.ModMount.JumpSpeed(mountedPlayer, ref jumpSpeed, xVelocity);
			}
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x004EADCF File Offset: 0x004E8FCF
		internal static void UpdateEffects(Player mountedPlayer)
		{
			if (MountLoader.IsModMount(Mount.mounts[mountedPlayer.mount.Type]))
			{
				MountLoader.GetMount(mountedPlayer.mount.Type).UpdateEffects(mountedPlayer);
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x004EADFF File Offset: 0x004E8FFF
		internal static bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
		{
			return !MountLoader.IsModMount(Mount.mounts[mountedPlayer.mount.Type]) || MountLoader.GetMount(mountedPlayer.mount.Type).UpdateFrame(mountedPlayer, state, velocity);
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x004EAE33 File Offset: 0x004E9033
		internal static bool CustomBodyFrame(Mount.MountData mount)
		{
			return MountLoader.IsModMount(mount) && mount.ModMount.CustomBodyFrame();
		}

		/// <summary>
		/// Allows you to make things happen while the mouse is pressed while the mount is active. Called each tick the mouse is pressed.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="mousePosition"></param>
		/// <param name="toggleOn">Does nothing yet</param>
		// Token: 0x060024E7 RID: 9447 RVA: 0x004EAE4D File Offset: 0x004E904D
		public static void UseAbility(Player player, Vector2 mousePosition, bool toggleOn)
		{
			if (MountLoader.IsModMount(player.mount._data))
			{
				player.mount._data.ModMount.UseAbility(player, mousePosition, toggleOn);
			}
		}

		/// <summary>
		/// Allows you to make things happen when the mount ability is aiming (while charging).
		/// </summary>
		/// <param name="mount"></param>
		/// <param name="player"></param>
		/// <param name="mousePosition"></param>
		// Token: 0x060024E8 RID: 9448 RVA: 0x004EAE79 File Offset: 0x004E9079
		public static void AimAbility(Mount mount, Player player, Vector2 mousePosition)
		{
			if (MountLoader.IsModMount(mount._data))
			{
				mount._data.ModMount.AimAbility(player, mousePosition);
			}
		}

		/// <summary>
		/// Allows you to make things happen when this mount is spawned in. Useful for player-specific initialization, utilizing player.mount._mountSpecificData or a ModPlayer class since ModMount is shared between all players.
		/// Custom dust spawning logic is also possible via the skipDust parameter.
		/// </summary>
		/// <param name="mount"></param>
		/// <param name="player"></param>
		/// <param name="skipDust">Set to true to skip the vanilla dust spawning logic</param>
		// Token: 0x060024E9 RID: 9449 RVA: 0x004EAE9A File Offset: 0x004E909A
		public static void SetMount(Mount mount, Player player, ref bool skipDust)
		{
			if (MountLoader.IsModMount(mount._data))
			{
				mount._data.ModMount.SetMount(player, ref skipDust);
			}
		}

		/// <summary>
		/// Allows you to make things happen when this mount is de-spawned. Useful for player-specific cleanup, see SetMount.
		/// Custom dust spawning logic is also possible via the skipDust parameter.
		/// </summary>
		/// <param name="mount"></param>
		/// <param name="player"></param>
		/// <param name="skipDust">Set to true to skip the vanilla dust spawning logic</param>
		// Token: 0x060024EA RID: 9450 RVA: 0x004EAEBB File Offset: 0x004E90BB
		public static void Dismount(Mount mount, Player player, ref bool skipDust)
		{
			if (MountLoader.IsModMount(mount._data))
			{
				mount._data.ModMount.Dismount(player, ref skipDust);
			}
		}

		/// <summary>
		/// See <see cref="M:Terraria.ModLoader.ModMount.Draw(System.Collections.Generic.List{Terraria.DataStructures.DrawData},System.Int32,Terraria.Player,Microsoft.Xna.Framework.Graphics.Texture2D@,Microsoft.Xna.Framework.Graphics.Texture2D@,Microsoft.Xna.Framework.Vector2@,Microsoft.Xna.Framework.Rectangle@,Microsoft.Xna.Framework.Color@,Microsoft.Xna.Framework.Color@,System.Single@,Microsoft.Xna.Framework.Graphics.SpriteEffects@,Microsoft.Xna.Framework.Vector2@,System.Single@,System.Single)" />
		/// </summary>
		// Token: 0x060024EB RID: 9451 RVA: 0x004EAEDC File Offset: 0x004E90DC
		public static bool Draw(Mount mount, List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
		{
			return !MountLoader.IsModMount(mount._data) || mount._data.ModMount.Draw(playerDrawData, drawType, drawPlayer, ref texture, ref glowTexture, ref drawPosition, ref frame, ref drawColor, ref glowColor, ref rotation, ref spriteEffects, ref drawOrigin, ref drawScale, shadow);
		}

		// Token: 0x04001742 RID: 5954
		internal static readonly IDictionary<int, ModMount> mountDatas = new Dictionary<int, ModMount>();
	}
}
