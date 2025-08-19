using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000474 RID: 1140
	[ChatCommand("Help")]
	public class HelpCommand : IChatCommand
	{
		// Token: 0x06002D7A RID: 11642 RVA: 0x005BEAB4 File Offset: 0x005BCCB4
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			ChatHelper.SendChatMessageToClient(HelpCommand.ComposeMessage(HelpCommand.GetCommandAliasesByID()), HelpCommand.RESPONSE_COLOR, (int)clientId);
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x005BEACC File Offset: 0x005BCCCC
		private static Dictionary<string, List<LocalizedText>> GetCommandAliasesByID()
		{
			object substitutions = Lang.CreateDialogSubstitutionObject(null);
			LocalizedText[] array = Language.FindAll(Lang.CreateDialogFilter("ChatCommandDescription.", substitutions));
			Dictionary<string, List<LocalizedText>> dictionary = new Dictionary<string, List<LocalizedText>>();
			foreach (LocalizedText localizedText in array)
			{
				string text = localizedText.Key;
				text = text.Replace("ChatCommandDescription.", "");
				int num = text.IndexOf('_');
				if (num != -1)
				{
					text = text.Substring(0, num);
				}
				List<LocalizedText> list;
				if (!dictionary.TryGetValue(text, out list))
				{
					list = new List<LocalizedText>();
					dictionary[text] = list;
				}
				list.Add(localizedText);
			}
			return dictionary;
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x005BEB6C File Offset: 0x005BCD6C
		private static NetworkText ComposeMessage(Dictionary<string, List<LocalizedText>> aliases)
		{
			string text = "";
			for (int i = 0; i < aliases.Count; i++)
			{
				text = string.Concat(new object[]
				{
					text,
					"{",
					i,
					"}\n"
				});
			}
			List<NetworkText> list = new List<NetworkText>();
			foreach (KeyValuePair<string, List<LocalizedText>> keyValuePair in aliases)
			{
				list.Add(Language.GetText("ChatCommandDescription." + keyValuePair.Key).ToNetworkText());
			}
			string text2 = text;
			object[] substitutions = list.ToArray();
			return NetworkText.FromFormattable(text2, substitutions);
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x04005143 RID: 20803
		private static readonly Color RESPONSE_COLOR = new Color(255, 240, 20);
	}
}
