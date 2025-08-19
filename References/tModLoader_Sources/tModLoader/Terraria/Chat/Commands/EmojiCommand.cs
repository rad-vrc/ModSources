using System;
using System.Collections.Generic;
using Terraria.GameContent.UI;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000751 RID: 1873
	[ChatCommand("Emoji")]
	public class EmojiCommand : IChatCommand
	{
		// Token: 0x06004BEA RID: 19434 RVA: 0x0066C58B File Offset: 0x0066A78B
		public EmojiCommand()
		{
			this.Initialize();
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x0066C59C File Offset: 0x0066A79C
		public void Initialize()
		{
			EmojiCommand._byName.Clear();
			for (int i = 0; i < EmoteID.Count; i++)
			{
				LocalizedText emojiName = Lang.GetEmojiName(i);
				if (emojiName != LocalizedText.Empty)
				{
					EmojiCommand._byName[emojiName] = i;
				}
			}
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x0066C5DE File Offset: 0x0066A7DE
		public void ProcessIncomingMessage(string text, byte clientId)
		{
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x0066C5E0 File Offset: 0x0066A7E0
		public void ProcessOutgoingMessage(ChatMessage message)
		{
			int result = -1;
			if (int.TryParse(message.Text, out result))
			{
				if (result < 0 || result >= EmoteBubbleLoader.EmoteBubbleCount)
				{
					return;
				}
			}
			else
			{
				result = -1;
			}
			if (result == -1)
			{
				foreach (LocalizedText key in EmojiCommand._byName.Keys)
				{
					if (message.Text == key.Value)
					{
						result = EmojiCommand._byName[key];
						break;
					}
				}
			}
			if (result != -1)
			{
				if (Main.netMode == 0)
				{
					EmoteBubble.NewBubble(result, new WorldUIAnchor(Main.LocalPlayer), 360);
					EmoteBubble.CheckForNPCsToReactToEmoteBubble(result, Main.LocalPlayer);
				}
				else
				{
					NetMessage.SendData(120, -1, -1, null, Main.myPlayer, (float)result, 0f, 0f, 0, 0, 0);
				}
			}
			message.Consume();
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x0066C6C8 File Offset: 0x0066A8C8
		public void PrintWarning(string text)
		{
			throw new Exception("This needs localized text!");
		}

		// Token: 0x040060A0 RID: 24736
		public const int PlayerEmojiDuration = 360;

		// Token: 0x040060A1 RID: 24737
		internal static Dictionary<LocalizedText, int> _byName = new Dictionary<LocalizedText, int>();
	}
}
