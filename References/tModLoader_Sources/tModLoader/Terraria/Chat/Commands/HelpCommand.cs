using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000753 RID: 1875
	[ChatCommand("Help")]
	public class HelpCommand : IChatCommand
	{
		// Token: 0x06004BF4 RID: 19444 RVA: 0x0066C73C File Offset: 0x0066A93C
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			ChatHelper.SendChatMessageToClient(HelpCommand.ComposeMessage(HelpCommand.GetCommandAliasesByID()), HelpCommand.RESPONSE_COLOR, (int)clientId);
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x0066C754 File Offset: 0x0066A954
		private static Dictionary<string, List<LocalizedText>> GetCommandAliasesByID()
		{
			object substitutions = Lang.CreateDialogSubstitutionObject(null);
			LocalizedText[] array = Language.FindAll(Lang.CreateDialogFilter("ChatCommandDescription.", substitutions));
			Dictionary<string, List<LocalizedText>> dictionary = new Dictionary<string, List<LocalizedText>>();
			foreach (LocalizedText localizedText in array)
			{
				string key = localizedText.Key;
				key = key.Replace("ChatCommandDescription.", "");
				int num = key.IndexOf('_');
				if (num != -1)
				{
					key = key.Substring(0, num);
				}
				List<LocalizedText> value;
				if (!dictionary.TryGetValue(key, out value))
				{
					value = (dictionary[key] = new List<LocalizedText>());
				}
				value.Add(localizedText);
			}
			return dictionary;
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x0066C7F4 File Offset: 0x0066A9F4
		private static NetworkText ComposeMessage(Dictionary<string, List<LocalizedText>> aliases)
		{
			string text = "";
			for (int i = 0; i < aliases.Count; i++)
			{
				text = text + "{" + i.ToString() + "}\n";
			}
			List<NetworkText> list = new List<NetworkText>();
			foreach (KeyValuePair<string, List<LocalizedText>> alias in aliases)
			{
				list.Add(Language.GetText("ChatCommandDescription." + alias.Key).ToNetworkText());
			}
			string text2 = text;
			object[] array = list.ToArray();
			object[] substitutions = array;
			return NetworkText.FromFormattable(text2, substitutions);
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x0066C8A8 File Offset: 0x0066AAA8
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x040060A3 RID: 24739
		private static readonly Color RESPONSE_COLOR = new Color(255, 240, 20);
	}
}
