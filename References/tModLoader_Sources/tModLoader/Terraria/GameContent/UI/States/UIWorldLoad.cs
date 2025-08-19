using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004E7 RID: 1255
	public class UIWorldLoad : UIState
	{
		// Token: 0x06003CEE RID: 15598 RVA: 0x005C5B58 File Offset: 0x005C3D58
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

		// Token: 0x06003CEF RID: 15599 RVA: 0x005C5C19 File Offset: 0x005C3E19
		public override void OnActivate()
		{
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.Points[3000].Unlink();
				UILinkPointNavigator.ChangePoint(3000);
			}
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x005C5C40 File Offset: 0x005C3E40
		public override void Update(GameTime gameTime)
		{
			this._progressBar.Top.Pixels = MathHelper.Lerp(270f, 370f, Utils.GetLerpValue(600f, 700f, (float)Main.screenHeight, true));
			this._progressMessage.Top.Pixels = this._progressBar.Top.Pixels - 70f;
			this._progressBar.Recalculate();
			this._progressMessage.Recalculate();
			base.Update(gameTime);
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x005C5CC5 File Offset: 0x005C3EC5
		public override void Draw(SpriteBatch spriteBatch)
		{
			this._progress = WorldGenerator.CurrentGenerationProgress;
			if (this._progress != null)
			{
				base.Draw(spriteBatch);
			}
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x005C5CE4 File Offset: 0x005C3EE4
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
			if (text != this.lastLoggedProgressMessage)
			{
				Logging.tML.Info(text + "...");
				this.lastLoggedProgressMessage = text;
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
						UIHeader progressMessage = this._progressMessage;
						progressMessage.Text += Main.rand.Next(999999999).ToString();
					}
				}
			}
			if (WorldGen.getGoodWorldGen)
			{
				if (!WorldGen.noTrapsWorldGen || !WorldGen.placingTraps)
				{
					string text2 = "";
					for (int num = this._progressMessage.Text.Length - 1; num >= 0; num--)
					{
						text2 += this._progressMessage.Text.Substring(num, 1);
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

		// Token: 0x06003CF3 RID: 15603 RVA: 0x005C5E94 File Offset: 0x005C4094
		private void UpdateGamepadSquiggle()
		{
			Vector2 vector = new Vector2((float)Math.Cos((double)(Main.GlobalTimeWrappedHourly * 6.2831855f)), (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f * 2f))) * new Vector2(30f, 15f) + Vector2.UnitY * 20f;
			UILinkPointNavigator.Points[3000].Unlink();
			UILinkPointNavigator.SetPosition(3000, new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f + vector);
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x005C5F38 File Offset: 0x005C4138
		public string GetStatusText()
		{
			if (this._progress == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
				defaultInterpolatedStringHandler.AppendFormatted<int>(0, "0.0%");
				defaultInterpolatedStringHandler.AppendLiteral(" - ... - ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(0, "0.0%");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
			return string.Format("{0:0.0%} - " + this._progress.Message + " - {1:0.0%}", this._progress.TotalProgress, this._progress.Value);
		}

		// Token: 0x040055E7 RID: 21991
		private UIGenProgressBar _progressBar = new UIGenProgressBar();

		// Token: 0x040055E8 RID: 21992
		private UIHeader _progressMessage = new UIHeader();

		// Token: 0x040055E9 RID: 21993
		private GenerationProgress _progress;

		// Token: 0x040055EA RID: 21994
		private string lastLoggedProgressMessage = string.Empty;
	}
}
