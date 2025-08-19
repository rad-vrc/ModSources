using System;
using System.Collections.Generic;
using System.Linq;
using ReLogic.Utilities;
using Terraria.Chat.Commands;
using Terraria.Localization;

namespace Terraria.Chat
{
	// Token: 0x02000467 RID: 1127
	public class ChatCommandProcessor : IChatProcessor
	{
		// Token: 0x06002D35 RID: 11573 RVA: 0x005BDEE8 File Offset: 0x005BC0E8
		public ChatCommandProcessor AddCommand<T>() where T : IChatCommand, new()
		{
			ChatCommandAttribute cacheableAttribute = AttributeUtilities.GetCacheableAttribute<T, ChatCommandAttribute>();
			string commandKey = "ChatCommand." + cacheableAttribute.Name;
			ChatCommandId chatCommandId = ChatCommandId.FromType<T>();
			this._commands[chatCommandId] = Activator.CreateInstance<T>();
			if (Language.Exists(commandKey))
			{
				this._localizedCommands.Add(Language.GetText(commandKey), chatCommandId);
			}
			else
			{
				commandKey += "_";
				foreach (LocalizedText key2 in Language.FindAll((string key, LocalizedText text) => key.StartsWith(commandKey)))
				{
					this._localizedCommands.Add(key2, chatCommandId);
				}
			}
			return this;
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x005BDFA8 File Offset: 0x005BC1A8
		public void AddAlias(LocalizedText text, NetworkText result)
		{
			this._aliases[text] = result;
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x005BDFB7 File Offset: 0x005BC1B7
		public void ClearAliases()
		{
			this._aliases.Clear();
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x005BDFC4 File Offset: 0x005BC1C4
		public ChatCommandProcessor AddDefaultCommand<T>() where T : IChatCommand, new()
		{
			this.AddCommand<T>();
			ChatCommandId key = ChatCommandId.FromType<T>();
			this._defaultCommand = this._commands[key];
			return this;
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x005BDFF4 File Offset: 0x005BC1F4
		private static bool HasLocalizedCommand(ChatMessage message, LocalizedText command)
		{
			string text = message.Text.ToLower();
			string value = command.Value;
			return text.StartsWith(value) && (text.Length == value.Length || text[value.Length] == ' ');
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x005BE040 File Offset: 0x005BC240
		private static string RemoveCommandPrefix(string messageText, LocalizedText command)
		{
			string value = command.Value;
			if (!messageText.StartsWith(value))
			{
				return "";
			}
			if (messageText.Length == value.Length)
			{
				return "";
			}
			if (messageText[value.Length] == ' ')
			{
				return messageText.Substring(value.Length + 1);
			}
			return "";
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x005BE09C File Offset: 0x005BC29C
		public ChatMessage CreateOutgoingMessage(string text)
		{
			ChatMessage message = new ChatMessage(text);
			KeyValuePair<LocalizedText, ChatCommandId> keyValuePair = this._localizedCommands.FirstOrDefault((KeyValuePair<LocalizedText, ChatCommandId> pair) => ChatCommandProcessor.HasLocalizedCommand(message, pair.Key));
			ChatCommandId value = keyValuePair.Value;
			if (keyValuePair.Key != null)
			{
				message.SetCommand(value);
				message.Text = ChatCommandProcessor.RemoveCommandPrefix(message.Text, keyValuePair.Key);
				this._commands[value].ProcessOutgoingMessage(message);
			}
			else
			{
				bool flag = false;
				KeyValuePair<LocalizedText, NetworkText> keyValuePair2 = this._aliases.FirstOrDefault((KeyValuePair<LocalizedText, NetworkText> pair) => ChatCommandProcessor.HasLocalizedCommand(message, pair.Key));
				Func<KeyValuePair<LocalizedText, NetworkText>, bool> <>9__2;
				while (keyValuePair2.Key != null)
				{
					flag = true;
					message = new ChatMessage(keyValuePair2.Value.ToString());
					IEnumerable<KeyValuePair<LocalizedText, NetworkText>> aliases = this._aliases;
					Func<KeyValuePair<LocalizedText, NetworkText>, bool> predicate;
					if ((predicate = <>9__2) == null)
					{
						predicate = (<>9__2 = ((KeyValuePair<LocalizedText, NetworkText> pair) => ChatCommandProcessor.HasLocalizedCommand(message, pair.Key)));
					}
					keyValuePair2 = aliases.FirstOrDefault(predicate);
				}
				if (flag)
				{
					return this.CreateOutgoingMessage(message.Text);
				}
			}
			return message;
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x005BE1B8 File Offset: 0x005BC3B8
		public void ProcessIncomingMessage(ChatMessage message, int clientId)
		{
			IChatCommand chatCommand;
			if (this._commands.TryGetValue(message.CommandId, out chatCommand))
			{
				chatCommand.ProcessIncomingMessage(message.Text, (byte)clientId);
				message.Consume();
				return;
			}
			if (this._defaultCommand != null)
			{
				this._defaultCommand.ProcessIncomingMessage(message.Text, (byte)clientId);
				message.Consume();
			}
		}

		// Token: 0x04005133 RID: 20787
		private readonly Dictionary<LocalizedText, ChatCommandId> _localizedCommands = new Dictionary<LocalizedText, ChatCommandId>();

		// Token: 0x04005134 RID: 20788
		private readonly Dictionary<ChatCommandId, IChatCommand> _commands = new Dictionary<ChatCommandId, IChatCommand>();

		// Token: 0x04005135 RID: 20789
		private readonly Dictionary<LocalizedText, NetworkText> _aliases = new Dictionary<LocalizedText, NetworkText>();

		// Token: 0x04005136 RID: 20790
		private IChatCommand _defaultCommand;
	}
}
