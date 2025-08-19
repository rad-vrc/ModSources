using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x02000354 RID: 852
	public class UIWorldLoad : UIState
	{
		// Token: 0x0600275B RID: 10075 RVA: 0x00582CEC File Offset: 0x00580EEC
		public UIWorldLoad()
		{
			this._progressBar.Top.Pixels = 270f;
			this._progressBar.HAlign = 0.5f;
			this._progressBar.VAlign = 0f;
			this._progressBar.Recalculate();
			this._progressMessage.CopyStyle(this._progressBar);
			UIHeader progressMessage = this._progressMessage;
			progressMessage.Top.Pixels = progressMessage.Top.Pixels - 70f;
			this._progressMessage.Recalculate();
			base.Append(this._progressBar);
			base.Append(this._progressMessage);
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x00582DA2 File Offset: 0x00580FA2
		public override void OnActivate()
		{
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.Points[3000].Unlink();
				UILinkPointNavigator.ChangePoint(3000);
			}
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x00582DCC File Offset: 0x00580FCC
		public override void Update(GameTime gameTime)
		{
			this._progressBar.Top.Pixels = MathHelper.Lerp(270f, 370f, Utils.GetLerpValue(600f, 700f, (float)Main.screenHeight, true));
			this._progressMessage.Top.Pixels = this._progressBar.Top.Pixels - 70f;
			this._progressBar.Recalculate();
			this._progressMessage.Recalculate();
			base.Update(gameTime);
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x00582E51 File Offset: 0x00581051
		public override void Draw(SpriteBatch spriteBatch)
		{
			this._progress = WorldGenerator.CurrentGenerationProgress;
			if (this._progress == null)
			{
				return;
			}
			base.Draw(spriteBatch);
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x00582E70 File Offset: 0x00581070
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float overallProgress = 0f;
			float currentProgress = 0f;
			string text = string.Empty;
			if (this._progress != null)
			{
				overallProgress = (float)this._progress.TotalProgress;
				currentProgress = (float)this._progress.Value;
				text = this._progress.Message;
			}
			this._progressBar.SetProgress(overallProgress, currentProgress);
			this._progressMessage.Text = text;
			if (WorldGen.drunkWorldGenText && !WorldGen.placingTraps && !WorldGen.getGoodWorldGen)
			{
				this._progressMessage.Text = string.Concat(Main.rand.Next(999999999));
				for (int i = 0; i < 3; i++)
				{
					if (Main.rand.Next(2) == 0)
					{
						this._progressMessage.Text = this._progressMessage.Text + Main.rand.Next(999999999);
					}
				}
			}
			if (WorldGen.getGoodWorldGen)
			{
				if (!WorldGen.noTrapsWorldGen || !WorldGen.placingTraps)
				{
					string text2 = "";
					for (int j = this._progressMessage.Text.Length - 1; j >= 0; j--)
					{
						text2 += this._progressMessage.Text.Substring(j, 1);
					}
					this._progressMessage.Text = text2;
				}
			}
			else if (WorldGen.notTheBees)
			{
				this._progressMessage.Text = Language.GetTextValue("UI.WorldGenEasterEgg_GeneratingBees");
			}
			Main.gameTips.Update();
			Main.gameTips.Draw();
			this.UpdateGamepadSquiggle();
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x00582FF8 File Offset: 0x005811F8
		private void UpdateGamepadSquiggle()
		{
			Vector2 value = new Vector2((float)Math.Cos((double)(Main.GlobalTimeWrappedHourly * 6.2831855f)), (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f * 2f))) * new Vector2(30f, 15f) + Vector2.UnitY * 20f;
			UILinkPointNavigator.Points[3000].Unlink();
			UILinkPointNavigator.SetPosition(3000, new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f + value);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x0058309C File Offset: 0x0058129C
		public string GetStatusText()
		{
			if (this._progress == null)
			{
				return string.Format("{0:0.0%} - ... - {1:0.0%}", 0, 0);
			}
			return string.Format("{0:0.0%} - " + this._progress.Message + " - {1:0.0%}", this._progress.TotalProgress, this._progress.Value);
		}

		// Token: 0x04004ACE RID: 19150
		private UIGenProgressBar _progressBar = new UIGenProgressBar();

		// Token: 0x04004ACF RID: 19151
		private UIHeader _progressMessage = new UIHeader();

		// Token: 0x04004AD0 RID: 19152
		private GenerationProgress _progress;
	}
}
