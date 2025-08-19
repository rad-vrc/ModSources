using System;
using Terraria.Chat.Commands;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Chat;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.Initializers
{
	// Token: 0x020000E5 RID: 229
	public static class ChatInitializer
	{
		// Token: 0x06001584 RID: 5508 RVA: 0x004BC778 File Offset: 0x004BA978
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

		// Token: 0x06001585 RID: 5509 RVA: 0x004BC854 File Offset: 0x004BAA54
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
		}
	}
}
