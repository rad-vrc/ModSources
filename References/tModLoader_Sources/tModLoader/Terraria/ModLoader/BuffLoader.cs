using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as the central class from which buff-related functions are supported and carried out.
	/// </summary>
	// Token: 0x0200014A RID: 330
	public static class BuffLoader
	{
		// Token: 0x06001AF7 RID: 6903 RVA: 0x004CE801 File Offset: 0x004CCA01
		internal static int ReserveBuffID()
		{
			int result = BuffLoader.nextBuff;
			BuffLoader.nextBuff++;
			return result;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x004CE814 File Offset: 0x004CCA14
		public static int BuffCount
		{
			get
			{
				return BuffLoader.nextBuff;
			}
		}

		/// <summary>
		/// Gets the ModBuff instance with the given type. If no ModBuff with the given type exists, returns null.
		/// </summary>
		// Token: 0x06001AF9 RID: 6905 RVA: 0x004CE81B File Offset: 0x004CCA1B
		public static ModBuff GetBuff(int type)
		{
			if (type < BuffID.Count || type >= BuffLoader.BuffCount)
			{
				return null;
			}
			return BuffLoader.buffs[type - BuffID.Count];
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x004CE840 File Offset: 0x004CCA40
		internal unsafe static void ResizeArrays()
		{
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.Buff, BuffLoader.nextBuff);
			LoaderUtils.ResetStaticMembers(typeof(BuffID), true);
			Array.Resize<bool>(ref Main.pvpBuff, BuffLoader.nextBuff);
			Array.Resize<bool>(ref Main.persistentBuff, BuffLoader.nextBuff);
			Array.Resize<bool>(ref Main.vanityPet, BuffLoader.nextBuff);
			Array.Resize<bool>(ref Main.lightPet, BuffLoader.nextBuff);
			Array.Resize<bool>(ref Main.meleeBuff, BuffLoader.nextBuff);
			Array.Resize<bool>(ref Main.debuff, BuffLoader.nextBuff);
			Array.Resize<bool>(ref Main.buffNoSave, BuffLoader.nextBuff);
			Array.Resize<bool>(ref Main.buffNoTimeDisplay, BuffLoader.nextBuff);
			Array.Resize<bool>(ref Main.buffDoubleApply, BuffLoader.nextBuff);
			Array.Resize<float>(ref Main.buffAlpha, BuffLoader.nextBuff);
			Array.Resize<LocalizedText>(ref Lang._buffNameCache, BuffLoader.nextBuff);
			Array.Resize<LocalizedText>(ref Lang._buffDescriptionCache, BuffLoader.nextBuff);
			for (int i = BuffID.Count; i < BuffLoader.nextBuff; i++)
			{
				Lang._buffNameCache[i] = LocalizedText.Empty;
				Lang._buffDescriptionCache[i] = LocalizedText.Empty;
			}
			int num;
			if (!ModLoader.Mods.Any<Mod>())
			{
				num = 0;
			}
			else
			{
				num = ModLoader.Mods.Max((Mod m) => (int)m.ExtraPlayerBuffSlots);
			}
			BuffLoader.extraPlayerBuffCount = num;
			ModLoader.BuildGlobalHook<GlobalBuff, BuffLoader.DelegateUpdatePlayer>(ref BuffLoader.HookUpdatePlayer, BuffLoader.globalBuffs, (GlobalBuff g) => (BuffLoader.DelegateUpdatePlayer)methodof(GlobalBuff.Update(int, Player, int*)).CreateDelegate(typeof(BuffLoader.DelegateUpdatePlayer), g));
			ModLoader.BuildGlobalHook<GlobalBuff, BuffLoader.DelegateUpdateNPC>(ref BuffLoader.HookUpdateNPC, BuffLoader.globalBuffs, (GlobalBuff g) => (BuffLoader.DelegateUpdateNPC)methodof(GlobalBuff.Update(int, NPC, int*)).CreateDelegate(typeof(BuffLoader.DelegateUpdateNPC), g));
			ModLoader.BuildGlobalHook<GlobalBuff, Func<int, Player, int, int, bool>>(ref BuffLoader.HookReApplyPlayer, BuffLoader.globalBuffs, (GlobalBuff g) => (Func<int, Player, int, int, bool>)methodof(GlobalBuff.ReApply(int, Player, int, int)).CreateDelegate(typeof(Func<int, Player, int, int, bool>), g));
			ModLoader.BuildGlobalHook<GlobalBuff, Func<int, NPC, int, int, bool>>(ref BuffLoader.HookReApplyNPC, BuffLoader.globalBuffs, (GlobalBuff g) => (Func<int, NPC, int, int, bool>)methodof(GlobalBuff.ReApply(int, NPC, int, int)).CreateDelegate(typeof(Func<int, NPC, int, int, bool>), g));
			ModLoader.BuildGlobalHook<GlobalBuff, BuffLoader.DelegateModifyBuffText>(ref BuffLoader.HookModifyBuffText, BuffLoader.globalBuffs, (GlobalBuff g) => (BuffLoader.DelegateModifyBuffText)methodof(GlobalBuff.ModifyBuffText(int, string*, string*, int*)).CreateDelegate(typeof(BuffLoader.DelegateModifyBuffText), g));
			ModLoader.BuildGlobalHook<GlobalBuff, Action<string, List<Vector2>>>(ref BuffLoader.HookCustomBuffTipSize, BuffLoader.globalBuffs, (GlobalBuff g) => (Action<string, List<Vector2>>)methodof(GlobalBuff.CustomBuffTipSize(string, List<Vector2>)).CreateDelegate(typeof(Action<string, List<Vector2>>), g));
			ModLoader.BuildGlobalHook<GlobalBuff, Action<string, SpriteBatch, int, int>>(ref BuffLoader.HookDrawCustomBuffTip, BuffLoader.globalBuffs, (GlobalBuff g) => (Action<string, SpriteBatch, int, int>)methodof(GlobalBuff.DrawCustomBuffTip(string, SpriteBatch, int, int)).CreateDelegate(typeof(Action<string, SpriteBatch, int, int>), g));
			ModLoader.BuildGlobalHook<GlobalBuff, BuffLoader.DelegatePreDraw>(ref BuffLoader.HookPreDraw, BuffLoader.globalBuffs, (GlobalBuff g) => (BuffLoader.DelegatePreDraw)methodof(GlobalBuff.PreDraw(SpriteBatch, int, int, BuffDrawParams*)).CreateDelegate(typeof(BuffLoader.DelegatePreDraw), g));
			ModLoader.BuildGlobalHook<GlobalBuff, BuffLoader.DelegatePostDraw>(ref BuffLoader.HookPostDraw, BuffLoader.globalBuffs, (GlobalBuff g) => (BuffLoader.DelegatePostDraw)methodof(GlobalBuff.PostDraw(SpriteBatch, int, int, BuffDrawParams)).CreateDelegate(typeof(BuffLoader.DelegatePostDraw), g));
			ModLoader.BuildGlobalHook<GlobalBuff, BuffLoader.DelegateRightClick>(ref BuffLoader.HookRightClick, BuffLoader.globalBuffs, (GlobalBuff g) => (BuffLoader.DelegateRightClick)methodof(GlobalBuff.RightClick(int, int)).CreateDelegate(typeof(BuffLoader.DelegateRightClick), g));
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x004CEF8D File Offset: 0x004CD18D
		internal static void PostSetupContent()
		{
			Main.Initialize_BuffDataFromMountData();
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x004CEF94 File Offset: 0x004CD194
		internal static void FinishSetup()
		{
			foreach (ModBuff buff in BuffLoader.buffs)
			{
				Lang._buffNameCache[buff.Type] = buff.DisplayName;
				Lang._buffDescriptionCache[buff.Type] = buff.Description;
			}
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x004CF000 File Offset: 0x004CD200
		internal static void Unload()
		{
			BuffLoader.buffs.Clear();
			BuffLoader.nextBuff = BuffID.Count;
			BuffLoader.globalBuffs.Clear();
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x004CF020 File Offset: 0x004CD220
		internal static bool IsModBuff(int type)
		{
			return type >= BuffID.Count;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x004CF030 File Offset: 0x004CD230
		public static void Update(int buff, Player player, ref int buffIndex)
		{
			int originalIndex = buffIndex;
			if (BuffLoader.IsModBuff(buff))
			{
				BuffLoader.GetBuff(buff).Update(player, ref buffIndex);
			}
			foreach (BuffLoader.DelegateUpdatePlayer hook in BuffLoader.HookUpdatePlayer)
			{
				if (buffIndex != originalIndex)
				{
					break;
				}
				hook(buff, player, ref buffIndex);
			}
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x004CF07C File Offset: 0x004CD27C
		public static void Update(int buff, NPC npc, ref int buffIndex)
		{
			int originalIndex = buffIndex;
			if (BuffLoader.IsModBuff(buff))
			{
				BuffLoader.GetBuff(buff).Update(npc, ref buffIndex);
			}
			foreach (BuffLoader.DelegateUpdateNPC hook in BuffLoader.HookUpdateNPC)
			{
				if (buffIndex != originalIndex)
				{
					break;
				}
				hook(buff, npc, ref buffIndex);
			}
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x004CF0C8 File Offset: 0x004CD2C8
		public static bool ReApply(int buff, Player player, int time, int buffIndex)
		{
			Func<int, Player, int, int, bool>[] hookReApplyPlayer = BuffLoader.HookReApplyPlayer;
			for (int i = 0; i < hookReApplyPlayer.Length; i++)
			{
				if (hookReApplyPlayer[i](buff, player, time, buffIndex))
				{
					return true;
				}
			}
			return BuffLoader.IsModBuff(buff) && BuffLoader.GetBuff(buff).ReApply(player, time, buffIndex);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x004CF114 File Offset: 0x004CD314
		public static bool ReApply(int buff, NPC npc, int time, int buffIndex)
		{
			Func<int, NPC, int, int, bool>[] hookReApplyNPC = BuffLoader.HookReApplyNPC;
			for (int i = 0; i < hookReApplyNPC.Length; i++)
			{
				if (hookReApplyNPC[i](buff, npc, time, buffIndex))
				{
					return true;
				}
			}
			return BuffLoader.IsModBuff(buff) && BuffLoader.GetBuff(buff).ReApply(npc, time, buffIndex);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x004CF160 File Offset: 0x004CD360
		public static void ModifyBuffText(int buff, ref string buffName, ref string tip, ref int rare)
		{
			if (BuffLoader.IsModBuff(buff))
			{
				BuffLoader.GetBuff(buff).ModifyBuffText(ref buffName, ref tip, ref rare);
			}
			BuffLoader.DelegateModifyBuffText[] hookModifyBuffText = BuffLoader.HookModifyBuffText;
			for (int i = 0; i < hookModifyBuffText.Length; i++)
			{
				hookModifyBuffText[i](buff, ref buffName, ref tip, ref rare);
			}
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x004CF1A4 File Offset: 0x004CD3A4
		public static void CustomBuffTipSize(string buffTip, List<Vector2> sizes)
		{
			Action<string, List<Vector2>>[] hookCustomBuffTipSize = BuffLoader.HookCustomBuffTipSize;
			for (int i = 0; i < hookCustomBuffTipSize.Length; i++)
			{
				hookCustomBuffTipSize[i](buffTip, sizes);
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x004CF1D0 File Offset: 0x004CD3D0
		public static void DrawCustomBuffTip(string buffTip, SpriteBatch spriteBatch, int originX, int originY)
		{
			Action<string, SpriteBatch, int, int>[] hookDrawCustomBuffTip = BuffLoader.HookDrawCustomBuffTip;
			for (int i = 0; i < hookDrawCustomBuffTip.Length; i++)
			{
				hookDrawCustomBuffTip[i](buffTip, spriteBatch, originX, originY);
			}
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x004CF200 File Offset: 0x004CD400
		public static bool PreDraw(SpriteBatch spriteBatch, int type, int buffIndex, ref BuffDrawParams drawParams)
		{
			bool result = true;
			foreach (BuffLoader.DelegatePreDraw hook in BuffLoader.HookPreDraw)
			{
				result &= hook(spriteBatch, type, buffIndex, ref drawParams);
			}
			if (result && BuffLoader.IsModBuff(type))
			{
				return BuffLoader.GetBuff(type).PreDraw(spriteBatch, buffIndex, ref drawParams);
			}
			return result;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x004CF250 File Offset: 0x004CD450
		public static void PostDraw(SpriteBatch spriteBatch, int type, int buffIndex, BuffDrawParams drawParams)
		{
			if (BuffLoader.IsModBuff(type))
			{
				BuffLoader.GetBuff(type).PostDraw(spriteBatch, buffIndex, drawParams);
			}
			BuffLoader.DelegatePostDraw[] hookPostDraw = BuffLoader.HookPostDraw;
			for (int i = 0; i < hookPostDraw.Length; i++)
			{
				hookPostDraw[i](spriteBatch, type, buffIndex, drawParams);
			}
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x004CF294 File Offset: 0x004CD494
		public static bool RightClick(int type, int buffIndex)
		{
			bool result = true;
			foreach (BuffLoader.DelegateRightClick hook in BuffLoader.HookRightClick)
			{
				result &= hook(type, buffIndex);
			}
			if (BuffLoader.IsModBuff(type))
			{
				result &= BuffLoader.GetBuff(type).RightClick(buffIndex);
			}
			return result;
		}

		// Token: 0x04001490 RID: 5264
		internal static int extraPlayerBuffCount;

		// Token: 0x04001491 RID: 5265
		private static int nextBuff = BuffID.Count;

		// Token: 0x04001492 RID: 5266
		internal static readonly IList<ModBuff> buffs = new List<ModBuff>();

		// Token: 0x04001493 RID: 5267
		internal static readonly IList<GlobalBuff> globalBuffs = new List<GlobalBuff>();

		// Token: 0x04001494 RID: 5268
		private static BuffLoader.DelegateUpdatePlayer[] HookUpdatePlayer;

		// Token: 0x04001495 RID: 5269
		private static BuffLoader.DelegateUpdateNPC[] HookUpdateNPC;

		// Token: 0x04001496 RID: 5270
		private static Func<int, Player, int, int, bool>[] HookReApplyPlayer;

		// Token: 0x04001497 RID: 5271
		private static Func<int, NPC, int, int, bool>[] HookReApplyNPC;

		// Token: 0x04001498 RID: 5272
		private static BuffLoader.DelegateModifyBuffText[] HookModifyBuffText;

		// Token: 0x04001499 RID: 5273
		private static Action<string, List<Vector2>>[] HookCustomBuffTipSize;

		// Token: 0x0400149A RID: 5274
		private static Action<string, SpriteBatch, int, int>[] HookDrawCustomBuffTip;

		// Token: 0x0400149B RID: 5275
		private static BuffLoader.DelegatePreDraw[] HookPreDraw;

		// Token: 0x0400149C RID: 5276
		private static BuffLoader.DelegatePostDraw[] HookPostDraw;

		// Token: 0x0400149D RID: 5277
		private static BuffLoader.DelegateRightClick[] HookRightClick;

		// Token: 0x020008B0 RID: 2224
		// (Invoke) Token: 0x06005229 RID: 21033
		private delegate void DelegateUpdatePlayer(int type, Player player, ref int buffIndex);

		// Token: 0x020008B1 RID: 2225
		// (Invoke) Token: 0x0600522D RID: 21037
		private delegate void DelegateUpdateNPC(int type, NPC npc, ref int buffIndex);

		// Token: 0x020008B2 RID: 2226
		// (Invoke) Token: 0x06005231 RID: 21041
		private delegate void DelegateModifyBuffText(int type, ref string buffName, ref string tip, ref int rare);

		// Token: 0x020008B3 RID: 2227
		// (Invoke) Token: 0x06005235 RID: 21045
		private delegate bool DelegatePreDraw(SpriteBatch spriteBatch, int type, int buffIndex, ref BuffDrawParams drawParams);

		// Token: 0x020008B4 RID: 2228
		// (Invoke) Token: 0x06005239 RID: 21049
		private delegate void DelegatePostDraw(SpriteBatch spriteBatch, int type, int buffIndex, BuffDrawParams drawParams);

		// Token: 0x020008B5 RID: 2229
		// (Invoke) Token: 0x0600523D RID: 21053
		private delegate bool DelegateRightClick(int type, int buffIndex);
	}
}
