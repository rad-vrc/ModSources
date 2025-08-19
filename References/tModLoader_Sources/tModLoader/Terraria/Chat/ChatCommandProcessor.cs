using System;
using System.Collections.Generic;
using System.Linq;
using ReLogic.Utilities;
using Terraria.Chat.Commands;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terraria.Chat
{
	// Token: 0x02000749 RID: 1865
	public class ChatCommandProcessor : IChatProcessor
	{
		// Token: 0x06004BB7 RID: 19383 RVA: 0x0066BC20 File Offset: 0x00669E20
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

		// Token: 0x06004BB8 RID: 19384 RVA: 0x0066BCE0 File Offset: 0x00669EE0
		public void AddAlias(LocalizedText text, NetworkText result)
		{
			this._aliases[text] = result;
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x0066BCEF File Offset: 0x00669EEF
		public void ClearAliases()
		{
			this._aliases.Clear();
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x0066BCFC File Offset: 0x00669EFC
		public ChatCommandProcessor AddDefaultCommand<T>() where T : IChatCommand, new()
		{
			this.AddCommand<T>();
			ChatCommandId key = ChatCommandId.FromType<T>();
			this._defaultCommand = this._commands[key];
			return this;
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x0066BD2C File Offset: 0x00669F2C
		private static bool HasLocalizedCommand(ChatMessage message, LocalizedText command)
		{
			string text = message.Text.ToLower();
			string value = command.Value;
			return text.StartsWith(value) && (text.Length == value.Length || text[value.Length] == ' ');
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x0066BD78 File Offset: 0x00669F78
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

		// Token: 0x06004BBD RID: 19389 RVA: 0x0066BDD4 File Offset: 0x00669FD4
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

		// Token: 0x06004BBE RID: 19390 RVA: 0x0066BEF0 File Offset: 0x0066A0F0
		public void ProcessIncomingMessage(ChatMessage message, int clientId)
		{
			if (message.CommandId._name == "Say" && CommandLoader.HandleCommand(message.Text, new PlayerCommandCaller(Main.player[clientId])))
			{
				message.Consume();
				return;
			}
			IChatCommand value;
			if (this._commands.TryGetValue(message.CommandId, out value))
			{
				value.ProcessIncomingMessage(message.Text, (byte)clientId);
				message.Consume();
				return;
			}
			if (this._defaultCommand != null)
			{
				this._defaultCommand.ProcessIncomingMessage(message.Text, (byte)clientId);
				message.Consume();
			}
		}

		// Token: 0x04006094 RID: 24724
		private readonly Dictionary<LocalizedText, ChatCommandId> _localizedCommands = new Dictionary<LocalizedText, ChatCommandId>();

		// Token: 0x04006095 RID: 24725
		private readonly Dictionary<ChatCommandId, IChatCommand> _commands = new Dictionary<ChatCommandId, IChatCommand>();

		// Token: 0x04006096 RID: 24726
		private readonly Dictionary<LocalizedText, NetworkText> _aliases = new Dictionary<LocalizedText, NetworkText>();

		// Token: 0x04006097 RID: 24727
		private IChatCommand _defaultCommand;
	}
}
