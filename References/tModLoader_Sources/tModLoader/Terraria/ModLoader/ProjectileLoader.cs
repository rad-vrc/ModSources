using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as the central class from which projectile-related functions are carried out. It also stores a list of mod projectiles by ID.
	/// </summary>
	// Token: 0x020001ED RID: 493
	public static class ProjectileLoader
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x004FE67F File Offset: 0x004FC87F
		// (set) Token: 0x0600269C RID: 9884 RVA: 0x004FE686 File Offset: 0x004FC886
		public static int ProjectileCount { get; private set; } = (int)ProjectileID.Count;

		// Token: 0x0600269D RID: 9885 RVA: 0x004FE690 File Offset: 0x004FC890
		private static GlobalHookList<GlobalProjectile> AddHook<F>(Expression<Func<GlobalProjectile, F>> func) where F : Delegate
		{
			GlobalHookList<GlobalProjectile> hook = GlobalHookList<GlobalProjectile>.Create<F>(func);
			ProjectileLoader.hooks.Add(hook);
			return hook;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x004FE6B0 File Offset: 0x004FC8B0
		public static T AddModHook<T>(T hook) where T : GlobalHookList<GlobalProjectile>
		{
			ProjectileLoader.modHooks.Add(hook);
			return hook;
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x004FE6C3 File Offset: 0x004FC8C3
		internal static int Register(ModProjectile projectile)
		{
			ProjectileLoader.projectiles.Add(projectile);
			return ProjectileLoader.ProjectileCount++;
		}

		/// <summary>
		/// Gets the ModProjectile template instance corresponding to the specified type (not the clone/new instance which gets added to Projectiles as the game is played).
		/// </summary>
		/// <param name="type">The type of the projectile</param>
		/// <returns>The ModProjectile instance in the projectiles array, null if not found.</returns>
		// Token: 0x060026A0 RID: 9888 RVA: 0x004FE6DD File Offset: 0x004FC8DD
		public static ModProjectile GetProjectile(int type)
		{
			if (type < (int)ProjectileID.Count || type >= ProjectileLoader.ProjectileCount)
			{
				return null;
			}
			return ProjectileLoader.projectiles[type - (int)ProjectileID.Count];
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x004FE704 File Offset: 0x004FC904
		internal static void ResizeArrays(bool unloading)
		{
			if (!unloading)
			{
				GlobalList<GlobalProjectile>.FinishLoading(ProjectileLoader.ProjectileCount);
			}
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.Projectile, ProjectileLoader.ProjectileCount);
			LoaderUtils.ResetStaticMembers(typeof(ProjectileID), true);
			Array.Resize<bool>(ref Main.projHostile, ProjectileLoader.ProjectileCount);
			Array.Resize<bool>(ref Main.projHook, ProjectileLoader.ProjectileCount);
			Array.Resize<int>(ref Main.projFrames, ProjectileLoader.ProjectileCount);
			Array.Resize<bool>(ref Main.projPet, ProjectileLoader.ProjectileCount);
			Array.Resize<LocalizedText>(ref Lang._projectileNameCache, ProjectileLoader.ProjectileCount);
			for (int i = (int)ProjectileID.Count; i < ProjectileLoader.ProjectileCount; i++)
			{
				Main.projFrames[i] = 1;
				Lang._projectileNameCache[i] = LocalizedText.Empty;
			}
			Array.Resize<uint[]>(ref Projectile.perIDStaticNPCImmunity, ProjectileLoader.ProjectileCount);
			for (int j = 0; j < ProjectileLoader.ProjectileCount; j++)
			{
				Projectile.perIDStaticNPCImmunity[j] = new uint[200];
			}
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x004FE7E0 File Offset: 0x004FC9E0
		internal static void FinishSetup()
		{
			GlobalLoaderUtils<GlobalProjectile, Projectile>.BuildTypeLookups(new Action<int>(new Projectile().SetDefaults));
			ProjectileLoader.UpdateHookLists();
			GlobalTypeLookups<GlobalProjectile>.LogStats();
			foreach (ModProjectile proj in ProjectileLoader.projectiles)
			{
				Lang._projectileNameCache[proj.Type] = proj.DisplayName;
			}
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x004FE858 File Offset: 0x004FCA58
		private static void UpdateHookLists()
		{
			foreach (GlobalHookList<GlobalProjectile> globalHookList in ProjectileLoader.hooks.Union(ProjectileLoader.modHooks))
			{
				globalHookList.Update();
			}
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x004FE8AC File Offset: 0x004FCAAC
		internal static void Unload()
		{
			ProjectileLoader.ProjectileCount = (int)ProjectileID.Count;
			ProjectileLoader.projectiles.Clear();
			GlobalList<GlobalProjectile>.Reset();
			ProjectileLoader.modHooks.Clear();
			ProjectileLoader.UpdateHookLists();
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x004FE8D6 File Offset: 0x004FCAD6
		internal static bool IsModProjectile(Projectile projectile)
		{
			return projectile.type >= (int)ProjectileID.Count;
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x004FE8E8 File Offset: 0x004FCAE8
		internal static void SetDefaults(Projectile projectile, bool createModProjectile = true)
		{
			if (ProjectileLoader.IsModProjectile(projectile) && createModProjectile)
			{
				projectile.ModProjectile = ProjectileLoader.GetProjectile(projectile.type).NewInstance(projectile);
			}
			GlobalLoaderUtils<GlobalProjectile, Projectile>.SetDefaults(projectile, ref projectile._globals, delegate(Projectile e)
			{
				ModProjectile modProjectile = e.ModProjectile;
				if (modProjectile == null)
				{
					return;
				}
				modProjectile.SetDefaults();
			});
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x004FE944 File Offset: 0x004FCB44
		internal static void OnSpawn(Projectile projectile, IEntitySource source)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.OnSpawn(source);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookOnSpawn.Enumerate(projectile))
			{
				globalProjectile.OnSpawn(projectile, source);
			}
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x004FE990 File Offset: 0x004FCB90
		public static void ProjectileAI(Projectile projectile)
		{
			if (ProjectileLoader.PreAI(projectile))
			{
				int type = projectile.type;
				bool flag = projectile.ModProjectile != null && projectile.ModProjectile.AIType > 0;
				if (flag)
				{
					projectile.type = projectile.ModProjectile.AIType;
				}
				projectile.VanillaAI();
				if (flag)
				{
					projectile.type = type;
				}
				ProjectileLoader.AI(projectile);
			}
			ProjectileLoader.PostAI(projectile);
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x004FE9F4 File Offset: 0x004FCBF4
		public static bool PreAI(Projectile projectile)
		{
			bool result = true;
			foreach (GlobalProjectile g in ProjectileLoader.HookPreAI.Enumerate(projectile))
			{
				result &= g.PreAI(projectile);
			}
			if (result && projectile.ModProjectile != null)
			{
				return projectile.ModProjectile.PreAI();
			}
			return result;
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x004FEA4C File Offset: 0x004FCC4C
		public static void AI(Projectile projectile)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.AI();
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookAI.Enumerate(projectile))
			{
				globalProjectile.AI(projectile);
			}
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x004FEA98 File Offset: 0x004FCC98
		public static void PostAI(Projectile projectile)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.PostAI();
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookPostAI.Enumerate(projectile))
			{
				globalProjectile.PostAI(projectile);
			}
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x004FEAE2 File Offset: 0x004FCCE2
		public static void SendExtraAI(BinaryWriter writer, byte[] extraAI)
		{
			writer.Write7BitEncodedInt(extraAI.Length);
			if (extraAI.Length != 0)
			{
				writer.Write(extraAI);
			}
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x004FEAF8 File Offset: 0x004FCCF8
		public static byte[] WriteExtraAI(Projectile projectile)
		{
			byte[] result;
			using (MemoryStream stream = new MemoryStream())
			{
				using (BinaryWriter modWriter = new BinaryWriter(stream))
				{
					using (MemoryStream bufferedStream = new MemoryStream())
					{
						using (BinaryWriter binaryWriter = new BinaryWriter(bufferedStream))
						{
							BitWriter bitWriter = new BitWriter();
							ModProjectile modProjectile = projectile.ModProjectile;
							if (modProjectile != null)
							{
								modProjectile.SendExtraAI(binaryWriter);
							}
							foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookSendExtraAI.Enumerate(projectile))
							{
								globalProjectile.SendExtraAI(projectile, bitWriter, binaryWriter);
							}
							bitWriter.Flush(modWriter);
							modWriter.Write(bufferedStream.ToArray());
							byte[] bytes = stream.ToArray();
							if (bytes.Length == 1)
							{
								result = null;
							}
							else
							{
								result = bytes;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x004FEBF8 File Offset: 0x004FCDF8
		public static byte[] ReadExtraAI(BinaryReader reader)
		{
			return reader.ReadBytes(reader.Read7BitEncodedInt());
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x004FEC08 File Offset: 0x004FCE08
		public static void ReceiveExtraAI(Projectile projectile, byte[] extraAI)
		{
			using (MemoryStream stream = extraAI.ToMemoryStream(false))
			{
				using (BinaryReader modReader = new BinaryReader(stream))
				{
					GlobalProjectile lastGlobalProjectile = null;
					try
					{
						BitReader bitReader = new BitReader(modReader);
						long bitReaderEnd = stream.Position;
						ModProjectile modProjectile = projectile.ModProjectile;
						if (modProjectile != null)
						{
							modProjectile.ReceiveExtraAI(modReader);
						}
						foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookReceiveExtraAI.Enumerate(projectile))
						{
							(lastGlobalProjectile = globalProjectile).ReceiveExtraAI(projectile, bitReader, modReader);
						}
						if (bitReader.BitsRead < bitReader.MaxBits)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(70, 2);
							defaultInterpolatedStringHandler.AppendLiteral("Read underflow ");
							defaultInterpolatedStringHandler.AppendFormatted<int>(bitReader.MaxBits - bitReader.BitsRead);
							defaultInterpolatedStringHandler.AppendLiteral(" of ");
							defaultInterpolatedStringHandler.AppendFormatted<int>(bitReader.MaxBits);
							defaultInterpolatedStringHandler.AppendLiteral(" compressed bits in ReceiveExtraAI, more info below");
							throw new IOException(defaultInterpolatedStringHandler.ToStringAndClear());
						}
						if (stream.Position < stream.Length)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(60, 2);
							defaultInterpolatedStringHandler.AppendLiteral("Read underflow ");
							defaultInterpolatedStringHandler.AppendFormatted<long>(stream.Length - stream.Position);
							defaultInterpolatedStringHandler.AppendLiteral(" of ");
							defaultInterpolatedStringHandler.AppendFormatted<long>(stream.Length - bitReaderEnd);
							defaultInterpolatedStringHandler.AppendLiteral(" bytes in ReceiveExtraAI, more info below");
							throw new IOException(defaultInterpolatedStringHandler.ToStringAndClear());
						}
					}
					catch (Exception)
					{
						string str = "Error in ReceiveExtraAI for Projectile ";
						ModProjectile modProjectile2 = projectile.ModProjectile;
						string message = str + (((modProjectile2 != null) ? modProjectile2.FullName : null) ?? projectile.Name);
						if (lastGlobalProjectile != null)
						{
							message += ", may be caused by one of these GlobalProjectiles:";
							foreach (GlobalProjectile g in ProjectileLoader.HookReceiveExtraAI.Enumerate(projectile))
							{
								message = message + "\n\t" + g.FullName;
								if (lastGlobalProjectile == g)
								{
									break;
								}
							}
						}
						Logging.tML.Error(message);
					}
				}
			}
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x004FEE44 File Offset: 0x004FD044
		public static bool ShouldUpdatePosition(Projectile projectile)
		{
			if (ProjectileLoader.IsModProjectile(projectile) && !projectile.ModProjectile.ShouldUpdatePosition())
			{
				return false;
			}
			EntityGlobalsEnumerator<GlobalProjectile> enumerator = ProjectileLoader.HookShouldUpdatePosition.Enumerate(projectile).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.ShouldUpdatePosition(projectile))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x004FEE9C File Offset: 0x004FD09C
		public static bool TileCollideStyle(Projectile projectile, ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			if (ProjectileLoader.IsModProjectile(projectile) && !projectile.ModProjectile.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac))
			{
				return false;
			}
			EntityGlobalsEnumerator<GlobalProjectile> enumerator = ProjectileLoader.HookTileCollideStyle.Enumerate(projectile).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.TileCollideStyle(projectile, ref width, ref height, ref fallThrough, ref hitboxCenterFrac))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x004FEEFC File Offset: 0x004FD0FC
		public static bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
		{
			bool result = true;
			foreach (GlobalProjectile g in ProjectileLoader.HookOnTileCollide.Enumerate(projectile))
			{
				result &= g.OnTileCollide(projectile, oldVelocity);
			}
			if (result && projectile.ModProjectile != null)
			{
				return projectile.ModProjectile.OnTileCollide(oldVelocity);
			}
			return result;
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x004FEF58 File Offset: 0x004FD158
		public static bool? CanCutTiles(Projectile projectile)
		{
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookCanCutTiles.Enumerate(projectile))
			{
				bool? canCutTiles = globalProjectile.CanCutTiles(projectile);
				if (canCutTiles != null)
				{
					return new bool?(canCutTiles.Value);
				}
			}
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile == null)
			{
				return null;
			}
			return modProjectile.CanCutTiles();
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x004FEFC4 File Offset: 0x004FD1C4
		public static void CutTiles(Projectile projectile)
		{
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookCutTiles.Enumerate(projectile))
			{
				globalProjectile.CutTiles(projectile);
			}
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile == null)
			{
				return;
			}
			modProjectile.CutTiles();
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x004FF010 File Offset: 0x004FD210
		public static bool PreKill(Projectile projectile, int timeLeft)
		{
			bool result = true;
			foreach (GlobalProjectile g in ProjectileLoader.HookPreKill.Enumerate(projectile))
			{
				result &= g.PreKill(projectile, timeLeft);
			}
			if (result && projectile.ModProjectile != null)
			{
				return projectile.ModProjectile.PreKill(timeLeft);
			}
			return result;
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x004FF06C File Offset: 0x004FD26C
		[Obsolete("Renamed to OnKill")]
		public static void Kill_Obsolete(Projectile projectile, int timeLeft)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.Kill(timeLeft);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookKill.Enumerate(projectile))
			{
				globalProjectile.Kill(projectile, timeLeft);
			}
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x004FF0B8 File Offset: 0x004FD2B8
		public static void OnKill(Projectile projectile, int timeLeft)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.OnKill(timeLeft);
			}
			ProjectileLoader.Kill_Obsolete(projectile, timeLeft);
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookOnKill.Enumerate(projectile))
			{
				globalProjectile.OnKill(projectile, timeLeft);
			}
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x004FF10C File Offset: 0x004FD30C
		public static bool? CanDamage(Projectile projectile)
		{
			bool? result = null;
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookCanDamage.Enumerate(projectile))
			{
				bool? canDamage = globalProjectile.CanDamage(projectile);
				if (canDamage != null)
				{
					if (!canDamage.Value)
					{
						return new bool?(false);
					}
					result = new bool?(true);
				}
			}
			bool? result2 = result;
			if (result2 != null)
			{
				return result2;
			}
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile == null)
			{
				return null;
			}
			return modProjectile.CanDamage();
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x004FF198 File Offset: 0x004FD398
		public static bool MinionContactDamage(Projectile projectile)
		{
			if (projectile.ModProjectile != null && projectile.ModProjectile.MinionContactDamage())
			{
				return true;
			}
			EntityGlobalsEnumerator<GlobalProjectile> enumerator = ProjectileLoader.HookMinionContactDamage.Enumerate(projectile).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.MinionContactDamage(projectile))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x004FF1F0 File Offset: 0x004FD3F0
		public static void ModifyDamageHitbox(Projectile projectile, ref Rectangle hitbox)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.ModifyDamageHitbox(ref hitbox);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookModifyDamageHitbox.Enumerate(projectile))
			{
				globalProjectile.ModifyDamageHitbox(projectile, ref hitbox);
			}
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x004FF23C File Offset: 0x004FD43C
		public static bool? CanHitNPC(Projectile projectile, NPC target)
		{
			bool? flag = null;
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookCanHitNPC.Enumerate(projectile))
			{
				bool? canHit = globalProjectile.CanHitNPC(projectile, target);
				if (canHit != null && !canHit.Value)
				{
					return new bool?(false);
				}
				if (canHit != null)
				{
					flag = new bool?(canHit.Value);
				}
			}
			if (projectile.ModProjectile != null)
			{
				bool? canHit2 = projectile.ModProjectile.CanHitNPC(target);
				if (canHit2 != null && !canHit2.Value)
				{
					return new bool?(false);
				}
				if (canHit2 != null)
				{
					flag = new bool?(canHit2.Value);
				}
			}
			return flag;
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x004FF2F8 File Offset: 0x004FD4F8
		public static void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.ModifyHitNPC(target, ref modifiers);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookModifyHitNPC.Enumerate(projectile))
			{
				globalProjectile.ModifyHitNPC(projectile, target, ref modifiers);
			}
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x004FF348 File Offset: 0x004FD548
		public static void OnHitNPC(Projectile projectile, NPC target, in NPC.HitInfo hit, int damageDone)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.OnHitNPC(target, hit, damageDone);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookOnHitNPC.Enumerate(projectile))
			{
				globalProjectile.OnHitNPC(projectile, target, hit, damageDone);
			}
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x004FF3A4 File Offset: 0x004FD5A4
		public static bool CanHitPvp(Projectile projectile, Player target)
		{
			EntityGlobalsEnumerator<GlobalProjectile> enumerator = ProjectileLoader.HookCanHitPvp.Enumerate(projectile).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanHitPvp(projectile, target))
				{
					return false;
				}
			}
			return projectile.ModProjectile == null || projectile.ModProjectile.CanHitPvp(target);
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x004FF3F8 File Offset: 0x004FD5F8
		public static bool CanHitPlayer(Projectile projectile, Player target)
		{
			EntityGlobalsEnumerator<GlobalProjectile> enumerator = ProjectileLoader.HookCanHitPlayer.Enumerate(projectile).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanHitPlayer(projectile, target))
				{
					return false;
				}
			}
			return projectile.ModProjectile == null || projectile.ModProjectile.CanHitPlayer(target);
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x004FF44C File Offset: 0x004FD64C
		public static void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.ModifyHitPlayer(target, ref modifiers);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookModifyHitPlayer.Enumerate(projectile))
			{
				globalProjectile.ModifyHitPlayer(projectile, target, ref modifiers);
			}
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x004FF49C File Offset: 0x004FD69C
		public static void OnHitPlayer(Projectile projectile, Player target, in Player.HurtInfo hurtInfo)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.OnHitPlayer(target, hurtInfo);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookOnHitPlayer.Enumerate(projectile))
			{
				globalProjectile.OnHitPlayer(projectile, target, hurtInfo);
			}
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x004FF4F4 File Offset: 0x004FD6F4
		public static bool? Colliding(Projectile projectile, Rectangle projHitbox, Rectangle targetHitbox)
		{
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookColliding.Enumerate(projectile))
			{
				bool? colliding = globalProjectile.Colliding(projectile, projHitbox, targetHitbox);
				if (colliding != null)
				{
					return new bool?(colliding.Value);
				}
			}
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile == null)
			{
				return null;
			}
			return modProjectile.Colliding(projHitbox, targetHitbox);
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x004FF561 File Offset: 0x004FD761
		public static void DrawHeldProjInFrontOfHeldItemAndArms(Projectile projectile, ref bool flag)
		{
			if (projectile.ModProjectile != null)
			{
				flag = projectile.ModProjectile.DrawHeldProjInFrontOfHeldItemAndArms;
			}
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x004FF578 File Offset: 0x004FD778
		[Obsolete("Moved to ItemLoader. Fishing line position and color are now set by the pole used.")]
		public static void ModifyFishingLine(Projectile projectile, ref float polePosX, ref float polePosY, ref Color lineColor)
		{
			if (projectile.ModProjectile == null)
			{
				return;
			}
			Vector2 lineOriginOffset = Vector2.Zero;
			Player player = Main.player[projectile.owner];
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.ModifyFishingLine(ref lineOriginOffset, ref lineColor);
			}
			polePosX += lineOriginOffset.X * (float)player.direction;
			polePosY += lineOriginOffset.Y * player.gravDir;
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x004FF5DC File Offset: 0x004FD7DC
		public static Color? GetAlpha(Projectile projectile, Color lightColor)
		{
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookGetAlpha.Enumerate(projectile))
			{
				Color? color = globalProjectile.GetAlpha(projectile, lightColor);
				if (color != null)
				{
					return color;
				}
			}
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile == null)
			{
				return null;
			}
			return modProjectile.GetAlpha(lightColor);
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x004FF63C File Offset: 0x004FD83C
		public static void DrawOffset(Projectile projectile, ref int offsetX, ref int offsetY, ref float originX)
		{
			if (projectile.ModProjectile != null)
			{
				offsetX = projectile.ModProjectile.DrawOffsetX;
				offsetY = -projectile.ModProjectile.DrawOriginOffsetY;
				originX += projectile.ModProjectile.DrawOriginOffsetX;
			}
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x004FF674 File Offset: 0x004FD874
		public static bool PreDrawExtras(Projectile projectile)
		{
			bool result = true;
			foreach (GlobalProjectile g in ProjectileLoader.HookPreDrawExtras.Enumerate(projectile))
			{
				result &= g.PreDrawExtras(projectile);
			}
			if (result && projectile.ModProjectile != null)
			{
				return projectile.ModProjectile.PreDrawExtras();
			}
			return result;
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x004FF6CC File Offset: 0x004FD8CC
		public static bool PreDraw(Projectile projectile, ref Color lightColor)
		{
			bool result = true;
			foreach (GlobalProjectile g in ProjectileLoader.HookPreDraw.Enumerate(projectile))
			{
				result &= g.PreDraw(projectile, ref lightColor);
			}
			if (result && projectile.ModProjectile != null)
			{
				return projectile.ModProjectile.PreDraw(ref lightColor);
			}
			return result;
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x004FF728 File Offset: 0x004FD928
		public static void PostDraw(Projectile projectile, Color lightColor)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.PostDraw(lightColor);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookPostDraw.Enumerate(projectile))
			{
				globalProjectile.PostDraw(projectile, lightColor);
			}
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x004FF774 File Offset: 0x004FD974
		public unsafe static bool? CanUseGrapple(int type, Player player)
		{
			ModProjectile projectile = ProjectileLoader.GetProjectile(type);
			bool? flag = (projectile != null) ? projectile.CanUseGrapple(player) : null;
			ReadOnlySpan<GlobalProjectile> readOnlySpan = ProjectileLoader.HookCanUseGrapple.Enumerate(type);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				bool? canGrapple = readOnlySpan[i]->CanUseGrapple(type, player);
				if (canGrapple != null)
				{
					flag = canGrapple;
				}
			}
			return flag;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x004FF7DC File Offset: 0x004FD9DC
		public unsafe static void UseGrapple(Player player, ref int type)
		{
			ModProjectile projectile = ProjectileLoader.GetProjectile(type);
			if (projectile != null)
			{
				projectile.UseGrapple(player, ref type);
			}
			ReadOnlySpan<GlobalProjectile> readOnlySpan = ProjectileLoader.HookUseGrapple.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->UseGrapple(player, ref type);
			}
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x004FF82C File Offset: 0x004FDA2C
		public static bool GrappleOutOfRange(float distance, Projectile projectile)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			float? num = (modProjectile != null) ? new float?(modProjectile.GrappleRange()) : null;
			return distance > num.GetValueOrDefault() & num != null;
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x004FF86C File Offset: 0x004FDA6C
		public static void NumGrappleHooks(Projectile projectile, Player player, ref int numHooks)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.NumGrappleHooks(player, ref numHooks);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookNumGrappleHooks.Enumerate(projectile))
			{
				globalProjectile.NumGrappleHooks(projectile, player, ref numHooks);
			}
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x004FF8BC File Offset: 0x004FDABC
		public static void GrappleRetreatSpeed(Projectile projectile, Player player, ref float speed)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.GrappleRetreatSpeed(player, ref speed);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookGrappleRetreatSpeed.Enumerate(projectile))
			{
				globalProjectile.GrappleRetreatSpeed(projectile, player, ref speed);
			}
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x004FF90C File Offset: 0x004FDB0C
		public static void GrapplePullSpeed(Projectile projectile, Player player, ref float speed)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.GrapplePullSpeed(player, ref speed);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookGrapplePullSpeed.Enumerate(projectile))
			{
				globalProjectile.GrapplePullSpeed(projectile, player, ref speed);
			}
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x004FF95C File Offset: 0x004FDB5C
		public static void GrappleTargetPoint(Projectile projectile, Player player, ref float grappleX, ref float grappleY)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.GrappleTargetPoint(player, ref grappleX, ref grappleY);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookGrappleTargetPoint.Enumerate(projectile))
			{
				globalProjectile.GrappleTargetPoint(projectile, player, ref grappleX, ref grappleY);
			}
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x004FF9AC File Offset: 0x004FDBAC
		public static bool? GrappleCanLatchOnTo(Projectile projectile, Player player, int x, int y)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			bool? flag = (modProjectile != null) ? modProjectile.GrappleCanLatchOnTo(player, x, y) : null;
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookGrappleCanLatchOnTo.Enumerate(projectile))
			{
				bool? globalFlag = globalProjectile.GrappleCanLatchOnTo(projectile, player, x, y);
				if (globalFlag != null)
				{
					if (!globalFlag.Value)
					{
						return new bool?(false);
					}
					flag = globalFlag;
				}
			}
			return flag;
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x004FFA28 File Offset: 0x004FDC28
		internal static void DrawBehind(Projectile projectile, int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookDrawBehind.Enumerate(projectile))
			{
				globalProjectile.DrawBehind(projectile, index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
			}
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x004FFA84 File Offset: 0x004FDC84
		internal static void PrepareBombToBlow(Projectile projectile)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.PrepareBombToBlow();
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookPrepareBombToBlow.Enumerate(projectile))
			{
				globalProjectile.PrepareBombToBlow(projectile);
			}
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x004FFAD0 File Offset: 0x004FDCD0
		internal static void EmitEnchantmentVisualsAt(Projectile projectile, Vector2 boxPosition, int boxWidth, int boxHeight)
		{
			ModProjectile modProjectile = projectile.ModProjectile;
			if (modProjectile != null)
			{
				modProjectile.EmitEnchantmentVisualsAt(boxPosition, boxWidth, boxHeight);
			}
			foreach (GlobalProjectile globalProjectile in ProjectileLoader.HookEmitEnchantmentVisualsAt.Enumerate(projectile))
			{
				globalProjectile.EmitEnchantmentVisualsAt(projectile, boxPosition, boxWidth, boxHeight);
			}
		}

		// Token: 0x0400186A RID: 6250
		private static readonly IList<ModProjectile> projectiles = new List<ModProjectile>();

		// Token: 0x0400186B RID: 6251
		private static readonly List<GlobalHookList<GlobalProjectile>> hooks = new List<GlobalHookList<GlobalProjectile>>();

		// Token: 0x0400186C RID: 6252
		private static readonly List<GlobalHookList<GlobalProjectile>> modHooks = new List<GlobalHookList<GlobalProjectile>>();

		// Token: 0x0400186D RID: 6253
		private static GlobalHookList<GlobalProjectile> HookOnSpawn = ProjectileLoader.AddHook<Action<Projectile, IEntitySource>>((GlobalProjectile g) => (Action<Projectile, IEntitySource>)methodof(GlobalProjectile.OnSpawn(Projectile, IEntitySource)).CreateDelegate(typeof(Action<Projectile, IEntitySource>), g));

		// Token: 0x0400186E RID: 6254
		private static GlobalHookList<GlobalProjectile> HookPreAI = ProjectileLoader.AddHook<Func<Projectile, bool>>((GlobalProjectile g) => (Func<Projectile, bool>)methodof(GlobalProjectile.PreAI(Projectile)).CreateDelegate(typeof(Func<Projectile, bool>), g));

		// Token: 0x0400186F RID: 6255
		private static GlobalHookList<GlobalProjectile> HookAI = ProjectileLoader.AddHook<Action<Projectile>>((GlobalProjectile g) => (Action<Projectile>)methodof(GlobalProjectile.AI(Projectile)).CreateDelegate(typeof(Action<Projectile>), g));

		// Token: 0x04001870 RID: 6256
		private static GlobalHookList<GlobalProjectile> HookPostAI = ProjectileLoader.AddHook<Action<Projectile>>((GlobalProjectile g) => (Action<Projectile>)methodof(GlobalProjectile.PostAI(Projectile)).CreateDelegate(typeof(Action<Projectile>), g));

		// Token: 0x04001871 RID: 6257
		private static GlobalHookList<GlobalProjectile> HookSendExtraAI = ProjectileLoader.AddHook<Action<Projectile, BitWriter, BinaryWriter>>((GlobalProjectile g) => (Action<Projectile, BitWriter, BinaryWriter>)methodof(GlobalProjectile.SendExtraAI(Projectile, BitWriter, BinaryWriter)).CreateDelegate(typeof(Action<Projectile, BitWriter, BinaryWriter>), g));

		// Token: 0x04001872 RID: 6258
		private static GlobalHookList<GlobalProjectile> HookReceiveExtraAI = ProjectileLoader.AddHook<Action<Projectile, BitReader, BinaryReader>>((GlobalProjectile g) => (Action<Projectile, BitReader, BinaryReader>)methodof(GlobalProjectile.ReceiveExtraAI(Projectile, BitReader, BinaryReader)).CreateDelegate(typeof(Action<Projectile, BitReader, BinaryReader>), g));

		// Token: 0x04001873 RID: 6259
		private static GlobalHookList<GlobalProjectile> HookShouldUpdatePosition = ProjectileLoader.AddHook<Func<Projectile, bool>>((GlobalProjectile g) => (Func<Projectile, bool>)methodof(GlobalProjectile.ShouldUpdatePosition(Projectile)).CreateDelegate(typeof(Func<Projectile, bool>), g));

		// Token: 0x04001874 RID: 6260
		private static GlobalHookList<GlobalProjectile> HookTileCollideStyle = ProjectileLoader.AddHook<ProjectileLoader.DelegateTileCollideStyle>((GlobalProjectile g) => (ProjectileLoader.DelegateTileCollideStyle)methodof(GlobalProjectile.TileCollideStyle(Projectile, int*, int*, bool*, Vector2*)).CreateDelegate(typeof(ProjectileLoader.DelegateTileCollideStyle), g));

		// Token: 0x04001875 RID: 6261
		private static GlobalHookList<GlobalProjectile> HookOnTileCollide = ProjectileLoader.AddHook<Func<Projectile, Vector2, bool>>((GlobalProjectile g) => (Func<Projectile, Vector2, bool>)methodof(GlobalProjectile.OnTileCollide(Projectile, Vector2)).CreateDelegate(typeof(Func<Projectile, Vector2, bool>), g));

		// Token: 0x04001876 RID: 6262
		private static GlobalHookList<GlobalProjectile> HookCanCutTiles = ProjectileLoader.AddHook<Func<Projectile, bool?>>((GlobalProjectile g) => (Func<Projectile, bool?>)methodof(GlobalProjectile.CanCutTiles(Projectile)).CreateDelegate(typeof(Func<Projectile, bool?>), g));

		// Token: 0x04001877 RID: 6263
		private static GlobalHookList<GlobalProjectile> HookCutTiles = ProjectileLoader.AddHook<Action<Projectile>>((GlobalProjectile g) => (Action<Projectile>)methodof(GlobalProjectile.CutTiles(Projectile)).CreateDelegate(typeof(Action<Projectile>), g));

		// Token: 0x04001878 RID: 6264
		private static GlobalHookList<GlobalProjectile> HookPreKill = ProjectileLoader.AddHook<Func<Projectile, int, bool>>((GlobalProjectile g) => (Func<Projectile, int, bool>)methodof(GlobalProjectile.PreKill(Projectile, int)).CreateDelegate(typeof(Func<Projectile, int, bool>), g));

		// Token: 0x04001879 RID: 6265
		[Obsolete]
		private static GlobalHookList<GlobalProjectile> HookKill = ProjectileLoader.AddHook<Action<Projectile, int>>((GlobalProjectile g) => (Action<Projectile, int>)methodof(GlobalProjectile.Kill(Projectile, int)).CreateDelegate(typeof(Action<Projectile, int>), g));

		// Token: 0x0400187A RID: 6266
		private static GlobalHookList<GlobalProjectile> HookOnKill = ProjectileLoader.AddHook<Action<Projectile, int>>((GlobalProjectile g) => (Action<Projectile, int>)methodof(GlobalProjectile.OnKill(Projectile, int)).CreateDelegate(typeof(Action<Projectile, int>), g));

		// Token: 0x0400187B RID: 6267
		private static GlobalHookList<GlobalProjectile> HookCanDamage = ProjectileLoader.AddHook<Func<Projectile, bool?>>((GlobalProjectile g) => (Func<Projectile, bool?>)methodof(GlobalProjectile.CanDamage(Projectile)).CreateDelegate(typeof(Func<Projectile, bool?>), g));

		// Token: 0x0400187C RID: 6268
		private static GlobalHookList<GlobalProjectile> HookMinionContactDamage = ProjectileLoader.AddHook<Func<Projectile, bool>>((GlobalProjectile g) => (Func<Projectile, bool>)methodof(GlobalProjectile.MinionContactDamage(Projectile)).CreateDelegate(typeof(Func<Projectile, bool>), g));

		// Token: 0x0400187D RID: 6269
		private static GlobalHookList<GlobalProjectile> HookModifyDamageHitbox = ProjectileLoader.AddHook<ProjectileLoader.DelegateModifyDamageHitbox>((GlobalProjectile g) => (ProjectileLoader.DelegateModifyDamageHitbox)methodof(GlobalProjectile.ModifyDamageHitbox(Projectile, Rectangle*)).CreateDelegate(typeof(ProjectileLoader.DelegateModifyDamageHitbox), g));

		// Token: 0x0400187E RID: 6270
		private static GlobalHookList<GlobalProjectile> HookCanHitNPC = ProjectileLoader.AddHook<Func<Projectile, NPC, bool?>>((GlobalProjectile g) => (Func<Projectile, NPC, bool?>)methodof(GlobalProjectile.CanHitNPC(Projectile, NPC)).CreateDelegate(typeof(Func<Projectile, NPC, bool?>), g));

		// Token: 0x0400187F RID: 6271
		private static GlobalHookList<GlobalProjectile> HookModifyHitNPC = ProjectileLoader.AddHook<ProjectileLoader.DelegateModifyHitNPC>((GlobalProjectile g) => (ProjectileLoader.DelegateModifyHitNPC)methodof(GlobalProjectile.ModifyHitNPC(Projectile, NPC, NPC.HitModifiers*)).CreateDelegate(typeof(ProjectileLoader.DelegateModifyHitNPC), g));

		// Token: 0x04001880 RID: 6272
		private static GlobalHookList<GlobalProjectile> HookOnHitNPC = ProjectileLoader.AddHook<Action<Projectile, NPC, NPC.HitInfo, int>>((GlobalProjectile g) => (Action<Projectile, NPC, NPC.HitInfo, int>)methodof(GlobalProjectile.OnHitNPC(Projectile, NPC, NPC.HitInfo, int)).CreateDelegate(typeof(Action<Projectile, NPC, NPC.HitInfo, int>), g));

		// Token: 0x04001881 RID: 6273
		private static GlobalHookList<GlobalProjectile> HookCanHitPvp = ProjectileLoader.AddHook<Func<Projectile, Player, bool>>((GlobalProjectile g) => (Func<Projectile, Player, bool>)methodof(GlobalProjectile.CanHitPvp(Projectile, Player)).CreateDelegate(typeof(Func<Projectile, Player, bool>), g));

		// Token: 0x04001882 RID: 6274
		private static GlobalHookList<GlobalProjectile> HookCanHitPlayer = ProjectileLoader.AddHook<Func<Projectile, Player, bool>>((GlobalProjectile g) => (Func<Projectile, Player, bool>)methodof(GlobalProjectile.CanHitPlayer(Projectile, Player)).CreateDelegate(typeof(Func<Projectile, Player, bool>), g));

		// Token: 0x04001883 RID: 6275
		private static GlobalHookList<GlobalProjectile> HookModifyHitPlayer = ProjectileLoader.AddHook<ProjectileLoader.DelegateModifyHitPlayer>((GlobalProjectile g) => (ProjectileLoader.DelegateModifyHitPlayer)methodof(GlobalProjectile.ModifyHitPlayer(Projectile, Player, Player.HurtModifiers*)).CreateDelegate(typeof(ProjectileLoader.DelegateModifyHitPlayer), g));

		// Token: 0x04001884 RID: 6276
		private static GlobalHookList<GlobalProjectile> HookOnHitPlayer = ProjectileLoader.AddHook<Action<Projectile, Player, Player.HurtInfo>>((GlobalProjectile g) => (Action<Projectile, Player, Player.HurtInfo>)methodof(GlobalProjectile.OnHitPlayer(Projectile, Player, Player.HurtInfo)).CreateDelegate(typeof(Action<Projectile, Player, Player.HurtInfo>), g));

		// Token: 0x04001885 RID: 6277
		private static GlobalHookList<GlobalProjectile> HookColliding = ProjectileLoader.AddHook<Func<Projectile, Rectangle, Rectangle, bool?>>((GlobalProjectile g) => (Func<Projectile, Rectangle, Rectangle, bool?>)methodof(GlobalProjectile.Colliding(Projectile, Rectangle, Rectangle)).CreateDelegate(typeof(Func<Projectile, Rectangle, Rectangle, bool?>), g));

		// Token: 0x04001886 RID: 6278
		private static GlobalHookList<GlobalProjectile> HookGetAlpha = ProjectileLoader.AddHook<Func<Projectile, Color, Color?>>((GlobalProjectile g) => (Func<Projectile, Color, Color?>)methodof(GlobalProjectile.GetAlpha(Projectile, Color)).CreateDelegate(typeof(Func<Projectile, Color, Color?>), g));

		// Token: 0x04001887 RID: 6279
		private static GlobalHookList<GlobalProjectile> HookPreDrawExtras = ProjectileLoader.AddHook<Func<Projectile, bool>>((GlobalProjectile g) => (Func<Projectile, bool>)methodof(GlobalProjectile.PreDrawExtras(Projectile)).CreateDelegate(typeof(Func<Projectile, bool>), g));

		// Token: 0x04001888 RID: 6280
		private static GlobalHookList<GlobalProjectile> HookPreDraw = ProjectileLoader.AddHook<ProjectileLoader.DelegatePreDraw>((GlobalProjectile g) => (ProjectileLoader.DelegatePreDraw)methodof(GlobalProjectile.PreDraw(Projectile, Color*)).CreateDelegate(typeof(ProjectileLoader.DelegatePreDraw), g));

		// Token: 0x04001889 RID: 6281
		private static GlobalHookList<GlobalProjectile> HookPostDraw = ProjectileLoader.AddHook<Action<Projectile, Color>>((GlobalProjectile g) => (Action<Projectile, Color>)methodof(GlobalProjectile.PostDraw(Projectile, Color)).CreateDelegate(typeof(Action<Projectile, Color>), g));

		// Token: 0x0400188A RID: 6282
		private static GlobalHookList<GlobalProjectile> HookCanUseGrapple = ProjectileLoader.AddHook<Func<int, Player, bool?>>((GlobalProjectile g) => (Func<int, Player, bool?>)methodof(GlobalProjectile.CanUseGrapple(int, Player)).CreateDelegate(typeof(Func<int, Player, bool?>), g));

		// Token: 0x0400188B RID: 6283
		private static GlobalHookList<GlobalProjectile> HookUseGrapple = ProjectileLoader.AddHook<ProjectileLoader.DelegateUseGrapple>((GlobalProjectile g) => (ProjectileLoader.DelegateUseGrapple)methodof(GlobalProjectile.UseGrapple(Player, int*)).CreateDelegate(typeof(ProjectileLoader.DelegateUseGrapple), g));

		// Token: 0x0400188C RID: 6284
		private static GlobalHookList<GlobalProjectile> HookNumGrappleHooks = ProjectileLoader.AddHook<ProjectileLoader.DelegateNumGrappleHooks>((GlobalProjectile g) => (ProjectileLoader.DelegateNumGrappleHooks)methodof(GlobalProjectile.NumGrappleHooks(Projectile, Player, int*)).CreateDelegate(typeof(ProjectileLoader.DelegateNumGrappleHooks), g));

		// Token: 0x0400188D RID: 6285
		private static GlobalHookList<GlobalProjectile> HookGrappleRetreatSpeed = ProjectileLoader.AddHook<ProjectileLoader.DelegateGrappleRetreatSpeed>((GlobalProjectile g) => (ProjectileLoader.DelegateGrappleRetreatSpeed)methodof(GlobalProjectile.GrappleRetreatSpeed(Projectile, Player, float*)).CreateDelegate(typeof(ProjectileLoader.DelegateGrappleRetreatSpeed), g));

		// Token: 0x0400188E RID: 6286
		private static GlobalHookList<GlobalProjectile> HookGrapplePullSpeed = ProjectileLoader.AddHook<ProjectileLoader.DelegateGrapplePullSpeed>((GlobalProjectile g) => (ProjectileLoader.DelegateGrapplePullSpeed)methodof(GlobalProjectile.GrapplePullSpeed(Projectile, Player, float*)).CreateDelegate(typeof(ProjectileLoader.DelegateGrapplePullSpeed), g));

		// Token: 0x0400188F RID: 6287
		private static GlobalHookList<GlobalProjectile> HookGrappleTargetPoint = ProjectileLoader.AddHook<ProjectileLoader.DelegateGrappleTargetPoint>((GlobalProjectile g) => (ProjectileLoader.DelegateGrappleTargetPoint)methodof(GlobalProjectile.GrappleTargetPoint(Projectile, Player, float*, float*)).CreateDelegate(typeof(ProjectileLoader.DelegateGrappleTargetPoint), g));

		// Token: 0x04001890 RID: 6288
		private static GlobalHookList<GlobalProjectile> HookGrappleCanLatchOnTo = ProjectileLoader.AddHook<Func<Projectile, Player, int, int, bool?>>((GlobalProjectile g) => (Func<Projectile, Player, int, int, bool?>)methodof(GlobalProjectile.GrappleCanLatchOnTo(Projectile, Player, int, int)).CreateDelegate(typeof(Func<Projectile, Player, int, int, bool?>), g));

		// Token: 0x04001891 RID: 6289
		private static GlobalHookList<GlobalProjectile> HookDrawBehind = ProjectileLoader.AddHook<Action<Projectile, int, List<int>, List<int>, List<int>, List<int>, List<int>>>((GlobalProjectile g) => (Action<Projectile, int, List<int>, List<int>, List<int>, List<int>, List<int>>)methodof(GlobalProjectile.DrawBehind(Projectile, int, List<int>, List<int>, List<int>, List<int>, List<int>)).CreateDelegate(typeof(Action<Projectile, int, List<int>, List<int>, List<int>, List<int>, List<int>>), g));

		// Token: 0x04001892 RID: 6290
		private static GlobalHookList<GlobalProjectile> HookPrepareBombToBlow = ProjectileLoader.AddHook<Action<Projectile>>((GlobalProjectile g) => (Action<Projectile>)methodof(GlobalProjectile.PrepareBombToBlow(Projectile)).CreateDelegate(typeof(Action<Projectile>), g));

		// Token: 0x04001893 RID: 6291
		private static GlobalHookList<GlobalProjectile> HookEmitEnchantmentVisualsAt = ProjectileLoader.AddHook<Action<Projectile, Vector2, int, int>>((GlobalProjectile g) => (Action<Projectile, Vector2, int, int>)methodof(GlobalProjectile.EmitEnchantmentVisualsAt(Projectile, Vector2, int, int)).CreateDelegate(typeof(Action<Projectile, Vector2, int, int>), g));

		// Token: 0x020009B5 RID: 2485
		// (Invoke) Token: 0x060055F3 RID: 22003
		private delegate bool DelegateTileCollideStyle(Projectile projectile, ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac);

		// Token: 0x020009B6 RID: 2486
		// (Invoke) Token: 0x060055F7 RID: 22007
		private delegate void DelegateModifyDamageHitbox(Projectile projectile, ref Rectangle hitbox);

		// Token: 0x020009B7 RID: 2487
		// (Invoke) Token: 0x060055FB RID: 22011
		private delegate void DelegateModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers);

		// Token: 0x020009B8 RID: 2488
		// (Invoke) Token: 0x060055FF RID: 22015
		private delegate void DelegateModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers);

		// Token: 0x020009B9 RID: 2489
		// (Invoke) Token: 0x06005603 RID: 22019
		private delegate bool DelegatePreDraw(Projectile projectile, ref Color lightColor);

		// Token: 0x020009BA RID: 2490
		// (Invoke) Token: 0x06005607 RID: 22023
		private delegate void DelegateUseGrapple(Player player, ref int type);

		// Token: 0x020009BB RID: 2491
		// (Invoke) Token: 0x0600560B RID: 22027
		private delegate void DelegateNumGrappleHooks(Projectile projectile, Player player, ref int numHooks);

		// Token: 0x020009BC RID: 2492
		// (Invoke) Token: 0x0600560F RID: 22031
		private delegate void DelegateGrappleRetreatSpeed(Projectile projectile, Player player, ref float speed);

		// Token: 0x020009BD RID: 2493
		// (Invoke) Token: 0x06005613 RID: 22035
		private delegate void DelegateGrapplePullSpeed(Projectile projectile, Player player, ref float speed);

		// Token: 0x020009BE RID: 2494
		// (Invoke) Token: 0x06005617 RID: 22039
		private delegate void DelegateGrappleTargetPoint(Projectile projectile, Player player, ref float grappleX, ref float grappleY);
	}
}
