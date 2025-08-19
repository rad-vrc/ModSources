using System;
using Microsoft.Xna.Framework;
using Terraria.Achievements;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x0200053D RID: 1341
	public class AchievementTagHandler : ITagHandler
	{
		// Token: 0x06003FE5 RID: 16357 RVA: 0x005DD10C File Offset: 0x005DB30C
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			Achievement achievement = Main.Achievements.GetAchievement(text);
			if (achievement == null)
			{
				return new TextSnippet(text);
			}
			return new AchievementTagHandler.AchievementSnippet(achievement);
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x005DD135 File Offset: 0x005DB335
		public static string GenerateTag(Achievement achievement)
		{
			return "[a:" + achievement.Name + "]";
		}

		// Token: 0x02000C26 RID: 3110
		private class AchievementSnippet : TextSnippet
		{
			// Token: 0x06005F2A RID: 24362 RVA: 0x006CD3E2 File Offset: 0x006CB5E2
			public AchievementSnippet(Achievement achievement) : base(achievement.FriendlyName.Value, Color.LightBlue, 1f)
			{
				this.CheckForHover = true;
				this._achievement = achievement;
			}

			// Token: 0x06005F2B RID: 24363 RVA: 0x006CD40D File Offset: 0x006CB60D
			public override void OnClick()
			{
				IngameOptions.Close();
				IngameFancyUI.OpenAchievementsAndGoto(this._achievement);
			}

			// Token: 0x04007890 RID: 30864
			private Achievement _achievement;
		}
	}
}
