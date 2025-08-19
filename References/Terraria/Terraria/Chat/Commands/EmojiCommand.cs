using System;
using System.Collections.Generic;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000471 RID: 1137
	[ChatCommand("Emoji")]
	public class EmojiCommand : IChatCommand
	{
		// Token: 0x06002D6E RID: 11630 RVA: 0x005BE899 File Offset: 0x005BCA99
		public EmojiCommand()
		{
			this.Initialize();
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x005BE8B4 File Offset: 0x005BCAB4
		public void Initialize()
		{
			this._byName.Clear();
			for (int i = 0; i < EmoteID.Count; i++)
			{
				LocalizedText emojiName = Lang.GetEmojiName(i);
				if (emojiName != LocalizedText.Empty)
				{
					this._byName[emojiName] = i;
				}
			}
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ProcessIncomingMessage(string text, byte clientId)
		{
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x005BE8F8 File Offset: 0x005BCAF8
		public void ProcessOutgoingMessage(ChatMessage message)
		{
			int num = -1;
			if (int.TryParse(message.Text, out num))
			{
				if (num < 0 || num >= EmoteID.Count)
				{
					return;
				}
			}
			else
			{
				num = -1;
			}
			if (num == -1)
			{
				foreach (LocalizedText localizedText in this._byName.Keys)
				{
					if (message.Text == localizedText.Value)
					{
						num = this._byName[localizedText];
						break;
					}
				}
			}
			if (num != -1)
			{
				if (Main.netMode == 0)
				{
					EmoteBubble.NewBubble(num, new WorldUIAnchor(Main.LocalPlayer), 360);
					EmoteBubble.CheckForNPCsToReactToEmoteBubble(num, Main.LocalPlayer);
				}
				else
				{
					NetMessage.SendData(120, -1, -1, null, Main.myPlayer, (float)num, 0f, 0f, 0, 0, 0);
				}
			}
			message.Consume();
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x005BE9E4 File Offset: 0x005BCBE4
		public void PrintWarning(string text)
		{
			throw new Exception("This needs localized text!");
		}

		// Token: 0x04005140 RID: 20800
		public const int PlayerEmojiDuration = 360;

		// Token: 0x04005141 RID: 20801
		private readonly Dictionary<LocalizedText, int> _byName = new Dictionary<LocalizedText, int>();
	}
}
