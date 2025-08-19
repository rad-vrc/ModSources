using System;
using Terraria.Chat.Commands;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Chat;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Terraria.Initializers
{
	// Token: 0x020003EA RID: 1002
	public static class ChatInitializer
	{
		// Token: 0x060034BC RID: 13500 RVA: 0x00562500 File Offset: 0x00560700
		public static void Load()
		{
			ChatManager.Register<ColorTagHandler>(new string[]
			{
				"c",
				"color"
			});
			ChatManager.Register<ItemTagHandler>(new string[]
			{
				"i",
				"item"
			});
			ChatManager.Register<NameTagHandler>(new string[]
			{
				"n",
				"name"
			});
			ChatManager.Register<AchievementTagHandler>(new string[]
			{
				"a",
				"achievement"
			});
			ChatManager.Register<GlyphTagHandler>(new string[]
			{
				"g",
				"glyph"
			});
			ChatManager.Commands.AddCommand<PartyChatCommand>().AddCommand<RollCommand>().AddCommand<EmoteCommand>().AddCommand<ListPlayersCommand>().AddCommand<RockPaperScissorsCommand>().AddCommand<EmojiCommand>().AddCommand<HelpCommand>().AddCommand<DeathCommand>().AddCommand<PVPDeathCommand>().AddCommand<AllDeathCommand>().AddCommand<AllPVPDeathCommand>().AddDefaultCommand<SayChatCommand>();
			ChatInitializer.PrepareAliases();
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x005625DC File Offset: 0x005607DC
		public static void PrepareAliases()
		{
			ChatManager.Commands.ClearAliases();
			for (int i = 0; i < EmoteID.Count; i++)
			{
				string name = EmoteID.Search.GetName(i);
				string key = "EmojiCommand." + name;
				ChatManager.Commands.AddAlias(Language.GetText(key), NetworkText.FromFormattable("{0} {1}", new object[]
				{
					Language.GetText("ChatCommand.Emoji_1"),
					Language.GetText("EmojiName." + name)
				}));
			}
			foreach (ModEmoteBubble modEmoteBubble in EmoteBubbleLoader.emoteBubbles)
			{
				LocalizedText command = new LocalizedText(modEmoteBubble.Command.Key, "/" + modEmoteBubble.Command.Value.ToLower());
				ChatManager.Commands.AddAlias(command, NetworkText.FromFormattable("{0} {1}", new object[]
				{
					Language.GetText("ChatCommand.Emoji_1"),
					modEmoteBubble.Command
				}));
			}
		}
	}
}
