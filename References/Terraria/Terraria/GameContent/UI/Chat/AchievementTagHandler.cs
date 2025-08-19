using System;
using Microsoft.Xna.Framework;
using Terraria.Achievements;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x0200039D RID: 925
	public class AchievementTagHandler : ITagHandler
	{
		// Token: 0x060029B1 RID: 10673 RVA: 0x00594A24 File Offset: 0x00592C24
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			Achievement achievement = Main.Achievements.GetAchievement(text);
			if (achievement == null)
			{
				return new TextSnippet(text);
			}
			return new AchievementTagHandler.AchievementSnippet(achievement);
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x00594A4D File Offset: 0x00592C4D
		public static string GenerateTag(Achievement achievement)
		{
			return "[a:" + achievement.Name + "]";
		}

		// Token: 0x02000758 RID: 1880
		private class AchievementSnippet : TextSnippet
		{
			// Token: 0x060038BC RID: 14524 RVA: 0x00614240 File Offset: 0x00612440
			public AchievementSnippet(Achievement achievement) : base(achievement.FriendlyName.Value, Color.LightBlue, 1f)
			{
				this.CheckForHover = true;
				this._achievement = achievement;
			}

			// Token: 0x060038BD RID: 14525 RVA: 0x0061426B File Offset: 0x0061246B
			public override void OnClick()
			{
				IngameOptions.Close();
				IngameFancyUI.OpenAchievementsAndGoto(this._achievement);
			}

			// Token: 0x0400643F RID: 25663
			private Achievement _achievement;
		}
	}
}
