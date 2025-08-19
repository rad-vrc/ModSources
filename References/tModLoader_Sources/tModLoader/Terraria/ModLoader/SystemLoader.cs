using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria.Graphics;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Exceptions;
using Terraria.UI;
using Terraria.WorldBuilding;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This is where all <see cref="T:Terraria.ModLoader.ModSystem" /> hooks are gathered and called.
	/// </summary>
	// Token: 0x020001FD RID: 509
	public static class SystemLoader
	{
		// Token: 0x06002730 RID: 10032 RVA: 0x005025A4 File Offset: 0x005007A4
		internal static void Add(ModSystem modSystem)
		{
			List<ModSystem> modsSystems;
			if (!SystemLoader.SystemsByMod.TryGetValue(modSystem.Mod, out modsSystems))
			{
				modsSystems = (SystemLoader.SystemsByMod[modSystem.Mod] = new List<ModSystem>());
			}
			SystemLoader.Systems.Add(modSystem);
			modsSystems.Add(modSystem);
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x005025EE File Offset: 0x005007EE
		internal static void Unload()
		{
			SystemLoader.Systems.Clear();
			SystemLoader.SystemsByMod.Clear();
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x00502604 File Offset: 0x00500804
		internal static IEnumerable<Type> TypesWithResizeArraysAttribute(Assembly assembly)
		{
			return (from t in AssemblyManager.GetLoadableTypes(assembly)
			where AttributeUtilities.GetAttribute<ReinitializeDuringResizeArraysAttribute>(t) != null
			select t).OrderBy((Type type) => type.FullName, StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>
		/// LoaderUtils.ResetStaticMembers does not mark the class constructor as having run via normal means
		/// and running the class constructor after ResizeArrays can cause issues due to duplicate registration.
		/// </summary>
		// Token: 0x06002733 RID: 10035 RVA: 0x00502664 File Offset: 0x00500864
		internal static void EnsureResizeArraysAttributeStaticCtorsRun(Mod mod)
		{
			foreach (Type type in SystemLoader.TypesWithResizeArraysAttribute(mod.Code))
			{
				SystemLoader.<EnsureResizeArraysAttributeStaticCtorsRun>g__RunStaticCtorIfNotAlreadyRun|5_0(type);
			}
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x005026B4 File Offset: 0x005008B4
		internal unsafe static void ResizeArrays(bool unloading)
		{
			SystemLoader.RebuildHooks();
			if (unloading)
			{
				return;
			}
			foreach (Mod mod in ModLoader.Mods)
			{
				using (new ModContent.TrackCurrentlyLoadingMod(mod.Name))
				{
					foreach (Type type in SystemLoader.TypesWithResizeArraysAttribute(mod.Code))
					{
						LoaderUtils.ResetStaticMembers(type, true);
					}
				}
			}
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookResizeArrays.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ModSystem system = *readOnlySpan[i];
				using (new ModContent.TrackCurrentlyLoadingMod(system.Mod.Name))
				{
					system.ResizeArrays();
				}
			}
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x005027A4 File Offset: 0x005009A4
		internal static void OnModLoad(Mod mod)
		{
			List<ModSystem> systems;
			if (SystemLoader.SystemsByMod.TryGetValue(mod, out systems))
			{
				foreach (ModSystem modSystem in systems)
				{
					modSystem.OnModLoad();
				}
			}
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x00502800 File Offset: 0x00500A00
		internal static void OnModUnload(Mod mod)
		{
			List<ModSystem> systems;
			if (SystemLoader.SystemsByMod.TryGetValue(mod, out systems))
			{
				foreach (ModSystem modSystem in systems)
				{
					modSystem.OnModUnload();
				}
			}
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x0050285C File Offset: 0x00500A5C
		internal static void PostSetupContent(Mod mod)
		{
			List<ModSystem> systems;
			if (SystemLoader.SystemsByMod.TryGetValue(mod, out systems))
			{
				foreach (ModSystem modSystem in systems)
				{
					modSystem.PostSetupContent();
				}
			}
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x005028B8 File Offset: 0x00500AB8
		internal unsafe static void OnLocalizationsLoaded()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookOnLocalizationsLoaded.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->OnLocalizationsLoaded();
			}
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x005028F0 File Offset: 0x00500AF0
		internal static void AddRecipes(Mod mod)
		{
			List<ModSystem> systems;
			if (SystemLoader.SystemsByMod.TryGetValue(mod, out systems))
			{
				foreach (ModSystem modSystem in systems)
				{
					modSystem.AddRecipes();
				}
			}
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x0050294C File Offset: 0x00500B4C
		internal static void PostAddRecipes(Mod mod)
		{
			List<ModSystem> systems;
			if (SystemLoader.SystemsByMod.TryGetValue(mod, out systems))
			{
				foreach (ModSystem modSystem in systems)
				{
					modSystem.PostAddRecipes();
				}
			}
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x005029A8 File Offset: 0x00500BA8
		internal static void PostSetupRecipes(Mod mod)
		{
			List<ModSystem> systems;
			if (SystemLoader.SystemsByMod.TryGetValue(mod, out systems))
			{
				foreach (ModSystem modSystem in systems)
				{
					modSystem.PostSetupRecipes();
				}
			}
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x00502A04 File Offset: 0x00500C04
		internal static void AddRecipeGroups(Mod mod)
		{
			List<ModSystem> systems;
			if (SystemLoader.SystemsByMod.TryGetValue(mod, out systems))
			{
				foreach (ModSystem modSystem in systems)
				{
					modSystem.AddRecipeGroups();
				}
			}
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x00502A60 File Offset: 0x00500C60
		public unsafe static void OnWorldLoad()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookOnWorldLoad.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ModSystem system = *readOnlySpan[i];
				try
				{
					system.OnWorldLoad();
				}
				catch (Exception e)
				{
					throw new CustomModDataException(system.Mod, e.Message, e);
				}
			}
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x00502AC0 File Offset: 0x00500CC0
		public unsafe static void OnWorldUnload()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookOnWorldUnload.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ModSystem system = *readOnlySpan[i];
				try
				{
					system.OnWorldUnload();
				}
				catch
				{
					ILog tML = Logging.tML;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(134, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Encountered an error while running the \"");
					defaultInterpolatedStringHandler.AppendFormatted(system.Name);
					defaultInterpolatedStringHandler.AppendLiteral(".OnWorldUnload\" method from the \"");
					defaultInterpolatedStringHandler.AppendFormatted(system.Mod.Name);
					defaultInterpolatedStringHandler.AppendLiteral("\" mod. The game, world, or mod might be in an unstable state.");
					tML.Error(defaultInterpolatedStringHandler.ToStringAndClear());
				}
			}
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x00502B74 File Offset: 0x00500D74
		public unsafe static void ClearWorld()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookClearWorld.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ModSystem system = *readOnlySpan[i];
				try
				{
					system.ClearWorld();
				}
				catch (Exception e)
				{
					throw new CustomModDataException(system.Mod, e.Message, e);
				}
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x00502BD4 File Offset: 0x00500DD4
		public unsafe static bool CanWorldBePlayed(PlayerFileData playerData, WorldFileData worldData, out ModSystem rejector)
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookCanWorldBePlayed.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ModSystem system = *readOnlySpan[i];
				if (!system.CanWorldBePlayed(playerData, worldData))
				{
					rejector = system;
					return false;
				}
			}
			rejector = null;
			return true;
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x00502C1C File Offset: 0x00500E1C
		public unsafe static void ModifyScreenPosition()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookModifyScreenPosition.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ModifyScreenPosition();
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x00502C54 File Offset: 0x00500E54
		public unsafe static void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookModifyTransformMatrix.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ModifyTransformMatrix(ref Transform);
			}
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x00502C90 File Offset: 0x00500E90
		public unsafe static void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
		{
			if (Main.gameMenu)
			{
				return;
			}
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookModifySunLightColor.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ModifySunLightColor(ref tileColor, ref backgroundColor);
			}
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x00502CD4 File Offset: 0x00500ED4
		public unsafe static void ModifyLightingBrightness(ref float negLight, ref float negLight2)
		{
			float scale = 1f;
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookModifyLightingBrightness.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ModifyLightingBrightness(ref scale);
			}
			if (Lighting.NotRetro)
			{
				negLight *= scale;
				negLight2 *= scale;
			}
			else
			{
				negLight -= (scale - 1f) / 2.3076923f;
				negLight2 -= (scale - 1f) / 0.75f;
			}
			negLight = Math.Max(negLight, 0.001f);
			negLight2 = Math.Max(negLight2, 0.001f);
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x00502D6C File Offset: 0x00500F6C
		public unsafe static void PreDrawMapIconOverlay(IReadOnlyList<IMapLayer> layers, MapOverlayDrawContext mapOverlayDrawContext)
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreDrawMapIconOverlay.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreDrawMapIconOverlay(layers, mapOverlayDrawContext);
			}
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x00502DA8 File Offset: 0x00500FA8
		public unsafe static void PostDrawFullscreenMap(ref string mouseText)
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostDrawFullscreenMap.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostDrawFullscreenMap(ref mouseText);
			}
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x00502DE4 File Offset: 0x00500FE4
		public unsafe static void UpdateUI(GameTime gameTime)
		{
			if (Main.gameMenu)
			{
				return;
			}
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookUpdateUI.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->UpdateUI(gameTime);
			}
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x00502E28 File Offset: 0x00501028
		public unsafe static void PreUpdateEntities()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreUpdateEntities.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreUpdateEntities();
			}
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x00502E60 File Offset: 0x00501060
		public unsafe static void PreUpdatePlayers()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreUpdatePlayers.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreUpdatePlayers();
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x00502E98 File Offset: 0x00501098
		public unsafe static void PostUpdatePlayers()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostUpdatePlayers.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostUpdatePlayers();
			}
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x00502ED0 File Offset: 0x005010D0
		public unsafe static void PreUpdateNPCs()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreUpdateNPCs.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreUpdateNPCs();
			}
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x00502F08 File Offset: 0x00501108
		public unsafe static void PostUpdateNPCs()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostUpdateNPCs.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostUpdateNPCs();
			}
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x00502F40 File Offset: 0x00501140
		public unsafe static void PreUpdateGores()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreUpdateGores.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreUpdateGores();
			}
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x00502F78 File Offset: 0x00501178
		public unsafe static void PostUpdateGores()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostUpdateGores.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostUpdateGores();
			}
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x00502FB0 File Offset: 0x005011B0
		public unsafe static void PreUpdateProjectiles()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreUpdateProjectiles.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreUpdateProjectiles();
			}
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x00502FE8 File Offset: 0x005011E8
		public unsafe static void PostUpdateProjectiles()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostUpdateProjectiles.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostUpdateProjectiles();
			}
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x00503020 File Offset: 0x00501220
		public unsafe static void PreUpdateItems()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreUpdateItems.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreUpdateItems();
			}
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x00503058 File Offset: 0x00501258
		public unsafe static void PostUpdateItems()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostUpdateItems.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostUpdateItems();
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x00503090 File Offset: 0x00501290
		public unsafe static void PreUpdateDusts()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreUpdateDusts.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreUpdateDusts();
			}
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x005030C8 File Offset: 0x005012C8
		public unsafe static void PostUpdateDusts()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostUpdateDusts.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostUpdateDusts();
			}
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x00503100 File Offset: 0x00501300
		public unsafe static void PreUpdateTime()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreUpdateTime.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreUpdateTime();
			}
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x00503138 File Offset: 0x00501338
		public unsafe static void PostUpdateTime()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostUpdateTime.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostUpdateTime();
			}
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x00503170 File Offset: 0x00501370
		public unsafe static void PreUpdateWorld()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreUpdateWorld.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreUpdateWorld();
			}
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x005031A8 File Offset: 0x005013A8
		public unsafe static void PostUpdateWorld()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostUpdateWorld.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostUpdateWorld();
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x005031E0 File Offset: 0x005013E0
		public unsafe static void PreUpdateInvasions()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreUpdateInvasions.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreUpdateInvasions();
			}
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x00503218 File Offset: 0x00501418
		public unsafe static void PostUpdateInvasions()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostUpdateInvasions.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostUpdateInvasions();
			}
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x00503250 File Offset: 0x00501450
		public unsafe static void PostUpdateEverything()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostUpdateEverything.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostUpdateEverything();
			}
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x00503288 File Offset: 0x00501488
		public unsafe static void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			foreach (GameInterfaceLayer gameInterfaceLayer in layers)
			{
				gameInterfaceLayer.Active = true;
			}
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookModifyInterfaceLayers.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ModifyInterfaceLayers(layers);
			}
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x00503300 File Offset: 0x00501500
		public unsafe static void ModifyGameTipVisibility(IReadOnlyList<GameTipData> tips)
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookModifyGameTipVisibility.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ModifyGameTipVisibility(tips);
			}
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x0050333C File Offset: 0x0050153C
		public unsafe static void PostDrawInterface(SpriteBatch spriteBatch)
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostDrawInterface.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostDrawInterface(spriteBatch);
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x00503378 File Offset: 0x00501578
		public unsafe static void PostUpdateInput()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostUpdateInput.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostUpdateInput();
			}
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x005033B0 File Offset: 0x005015B0
		public unsafe static void PreSaveAndQuit()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreSaveAndQuit.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreSaveAndQuit();
			}
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x005033E8 File Offset: 0x005015E8
		public unsafe static void PostDrawTiles()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostDrawTiles.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostDrawTiles();
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00503420 File Offset: 0x00501620
		public unsafe static void ModifyTimeRate(ref double timeRate, ref double tileUpdateRate, ref double eventUpdateRate)
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookModifyTimeRate.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ModifyTimeRate(ref timeRate, ref tileUpdateRate, ref eventUpdateRate);
			}
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x0050345C File Offset: 0x0050165C
		public unsafe static void PreWorldGen()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPreWorldGen.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PreWorldGen();
			}
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x00503494 File Offset: 0x00501694
		public unsafe static void ModifyWorldGenTasks(List<GenPass> passes, ref double totalWeight)
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookModifyWorldGenTasks.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ModSystem system = *readOnlySpan[i];
				try
				{
					system.ModifyWorldGenTasks(passes, ref totalWeight);
				}
				catch (Exception e)
				{
					Utils.ShowFancyErrorMessage(string.Join("\n", new object[]
					{
						system.FullName + " : " + Language.GetTextValue("tModLoader.WorldGenError"),
						e
					}), 0, null);
					throw;
				}
			}
			passes.RemoveAll((GenPass x) => !x.Enabled);
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x00503544 File Offset: 0x00501744
		public unsafe static void PostWorldGen()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookPostWorldGen.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->PostWorldGen();
			}
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x0050357C File Offset: 0x0050177C
		public unsafe static void ResetNearbyTileEffects()
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookResetNearbyTileEffects.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ResetNearbyTileEffects();
			}
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x005035B4 File Offset: 0x005017B4
		public unsafe static void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookTileCountsAvailable.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->TileCountsAvailable(tileCounts);
			}
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x005035F0 File Offset: 0x005017F0
		public unsafe static void ModifyHardmodeTasks(List<GenPass> passes)
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookModifyHardmodeTasks.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ModifyHardmodeTasks(passes);
			}
			passes.RemoveAll((GenPass x) => !x.Enabled);
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x00503650 File Offset: 0x00501850
		internal unsafe static bool HijackGetData(ref byte messageType, ref BinaryReader reader, int playerNumber)
		{
			bool hijacked = false;
			long readerPos = reader.BaseStream.Position;
			long biggestReaderPos = readerPos;
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookHijackGetData.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				if (readOnlySpan[i]->HijackGetData(ref messageType, ref reader, playerNumber))
				{
					hijacked = true;
					biggestReaderPos = Math.Max(reader.BaseStream.Position, biggestReaderPos);
				}
				reader.BaseStream.Position = readerPos;
			}
			if (hijacked)
			{
				reader.BaseStream.Position = biggestReaderPos;
			}
			return hijacked;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x005036D8 File Offset: 0x005018D8
		internal unsafe static bool HijackSendData(int whoAmI, int msgType, int remoteClient, int ignoreClient, NetworkText text, int number, float number2, float number3, float number4, int number5, int number6, int number7)
		{
			bool result = false;
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookHijackSendData.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ModSystem system = *readOnlySpan[i];
				result |= system.HijackSendData(whoAmI, msgType, remoteClient, ignoreClient, text, number, number2, number3, number4, number5, number6, number7);
			}
			return result;
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x0050372C File Offset: 0x0050192C
		private static HookList<ModSystem> AddHook<F>(Expression<Func<ModSystem, F>> func) where F : Delegate
		{
			HookList<ModSystem> hook = HookList<ModSystem>.Create<F>(func);
			SystemLoader.hooks.Add(hook);
			return hook;
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x0050374C File Offset: 0x0050194C
		private static void RebuildHooks()
		{
			foreach (HookList<ModSystem> hookList in SystemLoader.hooks)
			{
				hookList.Update(SystemLoader.Systems);
			}
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x005054E8 File Offset: 0x005036E8
		[CompilerGenerated]
		internal static void <EnsureResizeArraysAttributeStaticCtorsRun>g__RunStaticCtorIfNotAlreadyRun|5_0(Type type)
		{
			RuntimeHelpers.RunClassConstructor(type.TypeHandle);
			foreach (Type type2 in type.GetNestedTypes())
			{
				SystemLoader.<EnsureResizeArraysAttributeStaticCtorsRun>g__RunStaticCtorIfNotAlreadyRun|5_0(type);
			}
		}

		// Token: 0x040018C3 RID: 6339
		internal static readonly List<ModSystem> Systems = new List<ModSystem>();

		// Token: 0x040018C4 RID: 6340
		internal static readonly Dictionary<Mod, List<ModSystem>> SystemsByMod = new Dictionary<Mod, List<ModSystem>>();

		// Token: 0x040018C5 RID: 6341
		private static readonly List<HookList<ModSystem>> hooks = new List<HookList<ModSystem>>();

		// Token: 0x040018C6 RID: 6342
		private static HookList<ModSystem> HookOnLocalizationsLoaded = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.OnLocalizationsLoaded()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018C7 RID: 6343
		private static HookList<ModSystem> HookOnWorldLoad = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.OnWorldLoad()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018C8 RID: 6344
		private static HookList<ModSystem> HookOnWorldUnload = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.OnWorldUnload()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018C9 RID: 6345
		private static HookList<ModSystem> HookClearWorld = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.ClearWorld()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018CA RID: 6346
		private static HookList<ModSystem> HookCanWorldBePlayed = SystemLoader.AddHook<Func<PlayerFileData, WorldFileData, bool>>((ModSystem s) => (Func<PlayerFileData, WorldFileData, bool>)methodof(ModSystem.CanWorldBePlayed(PlayerFileData, WorldFileData)).CreateDelegate(typeof(Func<PlayerFileData, WorldFileData, bool>), s));

		// Token: 0x040018CB RID: 6347
		private static HookList<ModSystem> HookModifyScreenPosition = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.ModifyScreenPosition()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018CC RID: 6348
		private static HookList<ModSystem> HookModifyTransformMatrix = SystemLoader.AddHook<SystemLoader.DelegateModifyTransformMatrix>((ModSystem s) => (SystemLoader.DelegateModifyTransformMatrix)methodof(ModSystem.ModifyTransformMatrix(SpriteViewMatrix*)).CreateDelegate(typeof(SystemLoader.DelegateModifyTransformMatrix), s));

		// Token: 0x040018CD RID: 6349
		private static HookList<ModSystem> HookModifySunLightColor = SystemLoader.AddHook<SystemLoader.DelegateModifySunLightColor>((ModSystem s) => (SystemLoader.DelegateModifySunLightColor)methodof(ModSystem.ModifySunLightColor(Color*, Color*)).CreateDelegate(typeof(SystemLoader.DelegateModifySunLightColor), s));

		// Token: 0x040018CE RID: 6350
		private static HookList<ModSystem> HookModifyLightingBrightness = SystemLoader.AddHook<SystemLoader.DelegateModifyLightingBrightness>((ModSystem s) => (SystemLoader.DelegateModifyLightingBrightness)methodof(ModSystem.ModifyLightingBrightness(float*)).CreateDelegate(typeof(SystemLoader.DelegateModifyLightingBrightness), s));

		// Token: 0x040018CF RID: 6351
		private static HookList<ModSystem> HookPreDrawMapIconOverlay = SystemLoader.AddHook<SystemLoader.DelegatePreDrawMapIconOverlay>((ModSystem s) => (SystemLoader.DelegatePreDrawMapIconOverlay)methodof(ModSystem.PreDrawMapIconOverlay(IReadOnlyList<IMapLayer>, MapOverlayDrawContext)).CreateDelegate(typeof(SystemLoader.DelegatePreDrawMapIconOverlay), s));

		// Token: 0x040018D0 RID: 6352
		private static HookList<ModSystem> HookPostDrawFullscreenMap = SystemLoader.AddHook<SystemLoader.DelegatePostDrawFullscreenMap>((ModSystem s) => (SystemLoader.DelegatePostDrawFullscreenMap)methodof(ModSystem.PostDrawFullscreenMap(string*)).CreateDelegate(typeof(SystemLoader.DelegatePostDrawFullscreenMap), s));

		// Token: 0x040018D1 RID: 6353
		private static HookList<ModSystem> HookUpdateUI = SystemLoader.AddHook<Action<GameTime>>((ModSystem s) => (Action<GameTime>)methodof(ModSystem.UpdateUI(GameTime)).CreateDelegate(typeof(Action<GameTime>), s));

		// Token: 0x040018D2 RID: 6354
		private static HookList<ModSystem> HookPreUpdateEntities = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PreUpdateEntities()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018D3 RID: 6355
		private static HookList<ModSystem> HookPreUpdatePlayers = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PreUpdatePlayers()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018D4 RID: 6356
		private static HookList<ModSystem> HookPostUpdatePlayers = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostUpdatePlayers()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018D5 RID: 6357
		private static HookList<ModSystem> HookPreUpdateNPCs = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PreUpdateNPCs()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018D6 RID: 6358
		private static HookList<ModSystem> HookPostUpdateNPCs = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostUpdateNPCs()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018D7 RID: 6359
		private static HookList<ModSystem> HookPreUpdateGores = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PreUpdateGores()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018D8 RID: 6360
		private static HookList<ModSystem> HookPostUpdateGores = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostUpdateGores()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018D9 RID: 6361
		private static HookList<ModSystem> HookPreUpdateProjectiles = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PreUpdateProjectiles()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018DA RID: 6362
		private static HookList<ModSystem> HookPostUpdateProjectiles = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostUpdateProjectiles()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018DB RID: 6363
		private static HookList<ModSystem> HookPreUpdateItems = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PreUpdateItems()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018DC RID: 6364
		private static HookList<ModSystem> HookPostUpdateItems = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostUpdateItems()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018DD RID: 6365
		private static HookList<ModSystem> HookPreUpdateDusts = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PreUpdateDusts()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018DE RID: 6366
		private static HookList<ModSystem> HookPostUpdateDusts = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostUpdateDusts()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018DF RID: 6367
		private static HookList<ModSystem> HookPreUpdateTime = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PreUpdateTime()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018E0 RID: 6368
		private static HookList<ModSystem> HookPostUpdateTime = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostUpdateTime()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018E1 RID: 6369
		private static HookList<ModSystem> HookPreUpdateWorld = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PreUpdateWorld()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018E2 RID: 6370
		private static HookList<ModSystem> HookPostUpdateWorld = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostUpdateWorld()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018E3 RID: 6371
		private static HookList<ModSystem> HookPreUpdateInvasions = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PreUpdateInvasions()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018E4 RID: 6372
		private static HookList<ModSystem> HookPostUpdateInvasions = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostUpdateInvasions()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018E5 RID: 6373
		private static HookList<ModSystem> HookPostUpdateEverything = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostUpdateEverything()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018E6 RID: 6374
		private static HookList<ModSystem> HookModifyInterfaceLayers = SystemLoader.AddHook<Action<List<GameInterfaceLayer>>>((ModSystem s) => (Action<List<GameInterfaceLayer>>)methodof(ModSystem.ModifyInterfaceLayers(List<GameInterfaceLayer>)).CreateDelegate(typeof(Action<List<GameInterfaceLayer>>), s));

		// Token: 0x040018E7 RID: 6375
		private static HookList<ModSystem> HookModifyGameTipVisibility = SystemLoader.AddHook<Action<IReadOnlyList<GameTipData>>>((ModSystem s) => (Action<IReadOnlyList<GameTipData>>)methodof(ModSystem.ModifyGameTipVisibility(IReadOnlyList<GameTipData>)).CreateDelegate(typeof(Action<IReadOnlyList<GameTipData>>), s));

		// Token: 0x040018E8 RID: 6376
		private static HookList<ModSystem> HookPostDrawInterface = SystemLoader.AddHook<Action<SpriteBatch>>((ModSystem s) => (Action<SpriteBatch>)methodof(ModSystem.PostDrawInterface(SpriteBatch)).CreateDelegate(typeof(Action<SpriteBatch>), s));

		// Token: 0x040018E9 RID: 6377
		private static HookList<ModSystem> HookPostUpdateInput = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostUpdateInput()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018EA RID: 6378
		private static HookList<ModSystem> HookPreSaveAndQuit = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PreSaveAndQuit()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018EB RID: 6379
		private static HookList<ModSystem> HookPostDrawTiles = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostDrawTiles()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018EC RID: 6380
		private static HookList<ModSystem> HookModifyTimeRate = SystemLoader.AddHook<SystemLoader.DelegateModifyTimeRate>((ModSystem s) => (SystemLoader.DelegateModifyTimeRate)methodof(ModSystem.ModifyTimeRate(double*, double*, double*)).CreateDelegate(typeof(SystemLoader.DelegateModifyTimeRate), s));

		// Token: 0x040018ED RID: 6381
		private static HookList<ModSystem> HookPreWorldGen = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PreWorldGen()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018EE RID: 6382
		private static HookList<ModSystem> HookModifyWorldGenTasks = SystemLoader.AddHook<SystemLoader.DelegateModifyWorldGenTasks>((ModSystem s) => (SystemLoader.DelegateModifyWorldGenTasks)methodof(ModSystem.ModifyWorldGenTasks(List<GenPass>, double*)).CreateDelegate(typeof(SystemLoader.DelegateModifyWorldGenTasks), s));

		// Token: 0x040018EF RID: 6383
		private static HookList<ModSystem> HookPostWorldGen = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.PostWorldGen()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018F0 RID: 6384
		private static HookList<ModSystem> HookResetNearbyTileEffects = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.ResetNearbyTileEffects()).CreateDelegate(typeof(Action), s));

		// Token: 0x040018F1 RID: 6385
		private static HookList<ModSystem> HookTileCountsAvailable = SystemLoader.AddHook<SystemLoader.DelegateTileCountsAvailable>((ModSystem s) => (SystemLoader.DelegateTileCountsAvailable)methodof(ModSystem.TileCountsAvailable(ReadOnlySpan<int>)).CreateDelegate(typeof(SystemLoader.DelegateTileCountsAvailable), s));

		// Token: 0x040018F2 RID: 6386
		private static HookList<ModSystem> HookModifyHardmodeTasks = SystemLoader.AddHook<Action<List<GenPass>>>((ModSystem s) => (Action<List<GenPass>>)methodof(ModSystem.ModifyHardmodeTasks(List<GenPass>)).CreateDelegate(typeof(Action<List<GenPass>>), s));

		// Token: 0x040018F3 RID: 6387
		private static HookList<ModSystem> HookHijackGetData = SystemLoader.AddHook<SystemLoader.DelegateHijackGetData>((ModSystem s) => (SystemLoader.DelegateHijackGetData)methodof(ModSystem.HijackGetData(byte*, BinaryReader*, int)).CreateDelegate(typeof(SystemLoader.DelegateHijackGetData), s));

		// Token: 0x040018F4 RID: 6388
		private static HookList<ModSystem> HookHijackSendData = SystemLoader.AddHook<Func<int, int, int, int, NetworkText, int, float, float, float, int, int, int, bool>>((ModSystem s) => (Func<int, int, int, int, NetworkText, int, float, float, float, int, int, int, bool>)methodof(ModSystem.HijackSendData(int, int, int, int, NetworkText, int, float, float, float, int, int, int)).CreateDelegate(typeof(Func<int, int, int, int, NetworkText, int, float, float, float, int, int, int, bool>), s));

		// Token: 0x040018F5 RID: 6389
		internal static HookList<ModSystem> HookNetSend = SystemLoader.AddHook<Action<BinaryWriter>>((ModSystem s) => (Action<BinaryWriter>)methodof(ModSystem.NetSend(BinaryWriter)).CreateDelegate(typeof(Action<BinaryWriter>), s));

		// Token: 0x040018F6 RID: 6390
		internal static HookList<ModSystem> HookNetReceive = SystemLoader.AddHook<Action<BinaryReader>>((ModSystem s) => (Action<BinaryReader>)methodof(ModSystem.NetReceive(BinaryReader)).CreateDelegate(typeof(Action<BinaryReader>), s));

		// Token: 0x040018F7 RID: 6391
		private static HookList<ModSystem> HookResizeArrays = SystemLoader.AddHook<Action>((ModSystem s) => (Action)methodof(ModSystem.ResizeArrays()).CreateDelegate(typeof(Action), s));

		// Token: 0x020009C5 RID: 2501
		// (Invoke) Token: 0x06005627 RID: 22055
		private delegate void DelegateModifyTransformMatrix(ref SpriteViewMatrix Transform);

		// Token: 0x020009C6 RID: 2502
		// (Invoke) Token: 0x0600562B RID: 22059
		private delegate void DelegateModifySunLightColor(ref Color tileColor, ref Color backgroundColor);

		// Token: 0x020009C7 RID: 2503
		// (Invoke) Token: 0x0600562F RID: 22063
		private delegate void DelegateModifyLightingBrightness(ref float scale);

		// Token: 0x020009C8 RID: 2504
		// (Invoke) Token: 0x06005633 RID: 22067
		private delegate void DelegatePreDrawMapIconOverlay(IReadOnlyList<IMapLayer> layers, MapOverlayDrawContext mapOverlayDrawContext);

		// Token: 0x020009C9 RID: 2505
		// (Invoke) Token: 0x06005637 RID: 22071
		private delegate void DelegatePostDrawFullscreenMap(ref string mouseText);

		// Token: 0x020009CA RID: 2506
		// (Invoke) Token: 0x0600563B RID: 22075
		private delegate void DelegateModifyTimeRate(ref double timeRate, ref double tileUpdateRate, ref double eventUpdateRate);

		// Token: 0x020009CB RID: 2507
		// (Invoke) Token: 0x0600563F RID: 22079
		private delegate void DelegateModifyWorldGenTasks(List<GenPass> passes, ref double totalWeight);

		// Token: 0x020009CC RID: 2508
		// (Invoke) Token: 0x06005643 RID: 22083
		private delegate bool DelegateHijackGetData(ref byte messageType, ref BinaryReader reader, int playerNumber);

		// Token: 0x020009CD RID: 2509
		// (Invoke) Token: 0x06005647 RID: 22087
		private delegate void DelegateTileCountsAvailable(ReadOnlySpan<int> tileCounts);
	}
}
