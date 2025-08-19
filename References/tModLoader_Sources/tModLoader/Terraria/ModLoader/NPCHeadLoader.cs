using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class serves as a central place from which NPC head slots are stored and NPC head textures are assigned. This can be used to obtain the corresponding slots to head textures.
	/// </summary>
	// Token: 0x020001DE RID: 478
	public static class NPCHeadLoader
	{
		// Token: 0x0600250F RID: 9487 RVA: 0x004EBA49 File Offset: 0x004E9C49
		internal static int ReserveHeadSlot()
		{
			return NPCHeadLoader.nextHead++;
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x004EBA58 File Offset: 0x004E9C58
		public static int NPCHeadCount
		{
			get
			{
				return NPCHeadLoader.nextHead;
			}
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x004EBA60 File Offset: 0x004E9C60
		internal static int ReserveBossHeadSlot(string texture)
		{
			int existing;
			if (NPCHeadLoader.bossHeads.TryGetValue(texture, out existing))
			{
				return existing;
			}
			return NPCHeadLoader.nextBossHead++;
		}

		/// <summary>
		/// Gets the index of the head texture corresponding to the given texture path.
		/// </summary>
		/// <param name="texture">Relative texture path</param>
		/// <returns>The index of the texture in the heads array, -1 if not found.</returns>
		// Token: 0x06002512 RID: 9490 RVA: 0x004EBA8C File Offset: 0x004E9C8C
		public static int GetHeadSlot(string texture)
		{
			int slot;
			if (!NPCHeadLoader.heads.TryGetValue(texture, out slot))
			{
				return -1;
			}
			return slot;
		}

		/// <summary>
		/// Gets the index of the boss head texture corresponding to the given texture path.
		/// </summary>
		/// <param name="texture"></param>
		/// <returns></returns>
		// Token: 0x06002513 RID: 9491 RVA: 0x004EBAAC File Offset: 0x004E9CAC
		public static int GetBossHeadSlot(string texture)
		{
			int slot;
			if (!NPCHeadLoader.bossHeads.TryGetValue(texture, out slot))
			{
				return -1;
			}
			return slot;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x004EBACC File Offset: 0x004E9CCC
		internal static void ResizeAndFillArrays()
		{
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.NpcHead, NPCHeadLoader.nextHead);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.NpcHeadBoss, NPCHeadLoader.nextBossHead);
			NPCHeadLoader.<ResizeAndFillArrays>g__ResetHeadRenderer|14_0(ref Main.TownNPCHeadRenderer, TextureAssets.NpcHead);
			NPCHeadLoader.<ResizeAndFillArrays>g__ResetHeadRenderer|14_0(ref Main.BossNPCHeadRenderer, TextureAssets.NpcHeadBoss);
			foreach (string texture in NPCHeadLoader.heads.Keys)
			{
				TextureAssets.NpcHead[NPCHeadLoader.heads[texture]] = ModContent.Request<Texture2D>(texture, 2);
			}
			foreach (string texture2 in NPCHeadLoader.bossHeads.Keys)
			{
				TextureAssets.NpcHeadBoss[NPCHeadLoader.bossHeads[texture2]] = ModContent.Request<Texture2D>(texture2, 2);
			}
			LoaderUtils.ResetStaticMembers(typeof(NPCHeadID), true);
			foreach (int npc in NPCHeadLoader.npcToBossHead.Keys)
			{
				NPCID.Sets.BossHeadTextures[npc] = NPCHeadLoader.npcToBossHead[npc];
			}
			Array.Resize<int>(ref Main.instance._npcIndexWhoHoldsHeadIndex, NPCHeadLoader.nextHead);
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x004EBC30 File Offset: 0x004E9E30
		internal static void Unload()
		{
			NPCHeadLoader.nextHead = NPCHeadLoader.VanillaHeadCount;
			NPCHeadLoader.nextBossHead = NPCHeadLoader.VanillaBossHeadCount;
			NPCHeadLoader.heads.Clear();
			NPCHeadLoader.bossHeads.Clear();
			NPCHeadLoader.npcToHead.Clear();
			NPCHeadLoader.npcToBossHead.Clear();
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x004EBC70 File Offset: 0x004E9E70
		internal static int GetNPCHeadSlot(int type)
		{
			int slot;
			if (!NPCHeadLoader.npcToHead.TryGetValue(type, out slot))
			{
				return -1;
			}
			return slot;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x004EBCF4 File Offset: 0x004E9EF4
		[CompilerGenerated]
		internal static void <ResizeAndFillArrays>g__ResetHeadRenderer|14_0(ref NPCHeadRenderer renderer, Asset<Texture2D>[] textures)
		{
			Main.ContentThatNeedsRenderTargets.Remove(renderer);
			List<INeedRenderTargetContent> contentThatNeedsRenderTargets = Main.ContentThatNeedsRenderTargets;
			NPCHeadRenderer item;
			renderer = (item = new NPCHeadRenderer(textures));
			contentThatNeedsRenderTargets.Add(item);
		}

		/// <summary>
		/// The number of vanilla town NPC head textures that exist.
		/// </summary>
		// Token: 0x04001759 RID: 5977
		public static readonly int VanillaHeadCount = TextureAssets.NpcHead.Length;

		/// <summary>
		/// The number of vanilla boss head textures that exist.
		/// </summary>
		// Token: 0x0400175A RID: 5978
		public static readonly int VanillaBossHeadCount = TextureAssets.NpcHeadBoss.Length;

		// Token: 0x0400175B RID: 5979
		private static int nextHead = NPCHeadLoader.VanillaHeadCount;

		// Token: 0x0400175C RID: 5980
		private static int nextBossHead = NPCHeadLoader.VanillaBossHeadCount;

		// Token: 0x0400175D RID: 5981
		internal static IDictionary<string, int> heads = new Dictionary<string, int>();

		// Token: 0x0400175E RID: 5982
		internal static IDictionary<string, int> bossHeads = new Dictionary<string, int>();

		// Token: 0x0400175F RID: 5983
		internal static IDictionary<int, int> npcToHead = new Dictionary<int, int>();

		// Token: 0x04001760 RID: 5984
		internal static IDictionary<int, int> npcToBossHead = new Dictionary<int, int>();
	}
}
