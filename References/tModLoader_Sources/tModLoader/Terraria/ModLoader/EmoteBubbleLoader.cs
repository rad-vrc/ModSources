using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Chat.Commands;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x0200015A RID: 346
	public static class EmoteBubbleLoader
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x004D144D File Offset: 0x004CF64D
		public static int EmoteBubbleCount
		{
			get
			{
				return EmoteBubbleLoader.emoteBubbles.Count + EmoteID.Count;
			}
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x004D145F File Offset: 0x004CF65F
		internal static int Add(ModEmoteBubble emoteBubble)
		{
			EmoteBubbleLoader.emoteBubbles.Add(emoteBubble);
			return EmoteBubbleLoader.EmoteBubbleCount - 1;
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x004D1473 File Offset: 0x004CF673
		internal static void Unload()
		{
			EmoteBubbleLoader.emoteBubbles.Clear();
			EmoteBubbleLoader.globalEmoteBubbles.Clear();
			EmoteBubbleLoader.categoryEmoteLookup.Clear();
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x004D1494 File Offset: 0x004CF694
		internal static void ResizeArrays()
		{
			Array.Resize<LocalizedText>(ref Lang._emojiNameCache, EmoteBubbleLoader.EmoteBubbleCount);
			for (int i = EmoteID.Count; i < EmoteBubbleLoader.EmoteBubbleCount; i++)
			{
				Lang._emojiNameCache[i] = LocalizedText.Empty;
			}
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x004D14D0 File Offset: 0x004CF6D0
		internal static void FinishSetup()
		{
			foreach (ModEmoteBubble emoteBubble in EmoteBubbleLoader.emoteBubbles)
			{
				Lang._emojiNameCache[emoteBubble.Type] = emoteBubble.Command;
				if (emoteBubble.Command != LocalizedText.Empty)
				{
					EmojiCommand._byName[emoteBubble.Command] = emoteBubble.Type;
				}
			}
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x004D1550 File Offset: 0x004CF750
		internal static Dictionary<Mod, List<int>> GetAllUnlockedModEmotes()
		{
			Dictionary<Mod, List<int>> result = new Dictionary<Mod, List<int>>();
			foreach (ModEmoteBubble modEmote in from e in EmoteBubbleLoader.emoteBubbles
			where e.IsUnlocked()
			select e)
			{
				List<int> emoteList;
				if (!result.TryGetValue(modEmote.Mod, out emoteList))
				{
					emoteList = (result[modEmote.Mod] = new List<int>());
				}
				emoteList.Add(modEmote.Type);
			}
			return result;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x004D15F0 File Offset: 0x004CF7F0
		internal static List<int> AddEmotesToCategory(this List<int> emotesList, int categoryId)
		{
			List<ModEmoteBubble> modEmotes;
			if (EmoteBubbleLoader.categoryEmoteLookup.TryGetValue(categoryId, out modEmotes))
			{
				emotesList.AddRange(from e in modEmotes
				where e.IsUnlocked()
				select e.Type);
			}
			return emotesList;
		}

		/// <summary>
		/// Gets the <see cref="T:Terraria.ModLoader.ModEmoteBubble" /> instance corresponding to the specified ID.
		/// </summary>
		/// <param name="type">The ID of the emote bubble</param>
		/// <returns>The <see cref="T:Terraria.ModLoader.ModEmoteBubble" /> instance in the emote bubbles array, null if not found.</returns>
		// Token: 0x06001BE4 RID: 7140 RVA: 0x004D165C File Offset: 0x004CF85C
		public static ModEmoteBubble GetEmoteBubble(int type)
		{
			if (type < EmoteID.Count || type >= EmoteBubbleLoader.EmoteBubbleCount)
			{
				return null;
			}
			return EmoteBubbleLoader.emoteBubbles[type - EmoteID.Count];
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x004D1684 File Offset: 0x004CF884
		public static void OnSpawn(EmoteBubble emoteBubble)
		{
			if (emoteBubble.emote >= EmoteID.Count && emoteBubble.emote < EmoteBubbleLoader.EmoteBubbleCount)
			{
				emoteBubble.ModEmoteBubble = EmoteBubbleLoader.GetEmoteBubble(emoteBubble.emote).NewInstance(emoteBubble);
			}
			foreach (GlobalEmoteBubble globalEmoteBubble in EmoteBubbleLoader.globalEmoteBubbles)
			{
				globalEmoteBubble.OnSpawn(emoteBubble);
			}
			ModEmoteBubble modEmoteBubble = emoteBubble.ModEmoteBubble;
			if (modEmoteBubble == null)
			{
				return;
			}
			modEmoteBubble.OnSpawn();
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x004D1718 File Offset: 0x004CF918
		public static bool UpdateFrame(EmoteBubble emoteBubble)
		{
			bool result = true;
			foreach (GlobalEmoteBubble globalEmoteBubble in EmoteBubbleLoader.globalEmoteBubbles)
			{
				result &= globalEmoteBubble.UpdateFrame(emoteBubble);
			}
			if (!result)
			{
				return false;
			}
			ModEmoteBubble modEmoteBubble = emoteBubble.ModEmoteBubble;
			return modEmoteBubble == null || modEmoteBubble.UpdateFrame();
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x004D1788 File Offset: 0x004CF988
		public static bool UpdateFrameInEmoteMenu(int emoteType, ref int frameCounter)
		{
			bool result = true;
			foreach (GlobalEmoteBubble globalEmoteBubble in EmoteBubbleLoader.globalEmoteBubbles)
			{
				result &= globalEmoteBubble.UpdateFrameInEmoteMenu(emoteType, ref frameCounter);
			}
			if (!result)
			{
				return false;
			}
			ModEmoteBubble emoteBubble = EmoteBubbleLoader.GetEmoteBubble(emoteType);
			return emoteBubble == null || emoteBubble.UpdateFrameInEmoteMenu(ref frameCounter);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x004D17F8 File Offset: 0x004CF9F8
		public static bool PreDraw(EmoteBubble emoteBubble, SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle frame, Vector2 origin, SpriteEffects spriteEffects)
		{
			bool result = true;
			foreach (GlobalEmoteBubble globalEmoteBubble in EmoteBubbleLoader.globalEmoteBubbles)
			{
				result &= globalEmoteBubble.PreDraw(emoteBubble, spriteBatch, texture, position, frame, origin, spriteEffects);
			}
			if (!result)
			{
				return false;
			}
			ModEmoteBubble modEmoteBubble = emoteBubble.ModEmoteBubble;
			return modEmoteBubble == null || modEmoteBubble.PreDraw(spriteBatch, texture, position, frame, origin, spriteEffects);
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x004D1878 File Offset: 0x004CFA78
		public static void PostDraw(EmoteBubble emoteBubble, SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle frame, Vector2 origin, SpriteEffects spriteEffects)
		{
			foreach (GlobalEmoteBubble globalEmoteBubble in EmoteBubbleLoader.globalEmoteBubbles)
			{
				globalEmoteBubble.PostDraw(emoteBubble, spriteBatch, texture, position, frame, origin, spriteEffects);
			}
			ModEmoteBubble modEmoteBubble = emoteBubble.ModEmoteBubble;
			if (modEmoteBubble == null)
			{
				return;
			}
			modEmoteBubble.PostDraw(spriteBatch, texture, position, frame, origin, spriteEffects);
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x004D18EC File Offset: 0x004CFAEC
		public static bool PreDrawInEmoteMenu(int emoteType, SpriteBatch spriteBatch, EmoteButton uiEmoteButton, Vector2 position, Rectangle frame, Vector2 origin)
		{
			bool result = true;
			foreach (GlobalEmoteBubble globalEmoteBubble in EmoteBubbleLoader.globalEmoteBubbles)
			{
				result &= globalEmoteBubble.PreDrawInEmoteMenu(emoteType, spriteBatch, uiEmoteButton, position, frame, origin);
			}
			if (!result)
			{
				return false;
			}
			ModEmoteBubble emoteBubble = EmoteBubbleLoader.GetEmoteBubble(emoteType);
			return emoteBubble == null || emoteBubble.PreDrawInEmoteMenu(spriteBatch, uiEmoteButton, position, frame, origin);
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x004D1968 File Offset: 0x004CFB68
		public static void PostDrawInEmoteMenu(int emoteType, SpriteBatch spriteBatch, EmoteButton uiEmoteButton, Vector2 position, Rectangle frame, Vector2 origin)
		{
			foreach (GlobalEmoteBubble globalEmoteBubble in EmoteBubbleLoader.globalEmoteBubbles)
			{
				globalEmoteBubble.PostDrawInEmoteMenu(emoteType, spriteBatch, uiEmoteButton, position, frame, origin);
			}
			ModEmoteBubble emoteBubble = EmoteBubbleLoader.GetEmoteBubble(emoteType);
			if (emoteBubble == null)
			{
				return;
			}
			emoteBubble.PostDrawInEmoteMenu(spriteBatch, uiEmoteButton, position, frame, origin);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x004D19D8 File Offset: 0x004CFBD8
		public static Rectangle? GetFrame(EmoteBubble emoteBubble)
		{
			if (emoteBubble.ModEmoteBubble != null)
			{
				return emoteBubble.ModEmoteBubble.GetFrame();
			}
			Rectangle? result = null;
			foreach (GlobalEmoteBubble globalEmoteBubble in EmoteBubbleLoader.globalEmoteBubbles)
			{
				Rectangle? frameRect = globalEmoteBubble.GetFrame(emoteBubble);
				if (frameRect != null)
				{
					result = frameRect;
				}
			}
			return result;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x004D1A54 File Offset: 0x004CFC54
		public static Rectangle? GetFrameInEmoteMenu(int emoteType, int frame, int frameCounter)
		{
			if (emoteType < EmoteID.Count)
			{
				Rectangle? result = null;
				foreach (GlobalEmoteBubble globalEmoteBubble in EmoteBubbleLoader.globalEmoteBubbles)
				{
					Rectangle? frameRect = globalEmoteBubble.GetFrameInEmoteMenu(emoteType, frame, frameCounter);
					if (frameRect != null)
					{
						result = frameRect;
					}
				}
				return result;
			}
			ModEmoteBubble emoteBubble = EmoteBubbleLoader.GetEmoteBubble(emoteType);
			if (emoteBubble == null)
			{
				return null;
			}
			return emoteBubble.GetFrameInEmoteMenu(frame, frameCounter);
		}

		// Token: 0x040014EE RID: 5358
		internal static readonly List<ModEmoteBubble> emoteBubbles = new List<ModEmoteBubble>();

		// Token: 0x040014EF RID: 5359
		internal static readonly List<GlobalEmoteBubble> globalEmoteBubbles = new List<GlobalEmoteBubble>();

		// Token: 0x040014F0 RID: 5360
		internal static readonly Dictionary<int, List<ModEmoteBubble>> categoryEmoteLookup = new Dictionary<int, List<ModEmoteBubble>>();
	}
}
