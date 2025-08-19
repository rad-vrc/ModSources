using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000247 RID: 583
	internal class UIForcedDelayInfoMessage : UIInfoMessage
	{
		// Token: 0x0600299C RID: 10652 RVA: 0x005136B1 File Offset: 0x005118B1
		internal void Delay(int seconds)
		{
			Main.menuMode = 10014;
			if (this.timeLeft == -1)
			{
				this.timeLeft = seconds * 60;
			}
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x005136D0 File Offset: 0x005118D0
		public override void OnInitialize()
		{
			base.OnInitialize();
			UITextPanel<string> uitextPanel = new UITextPanel<string>(Language.GetTextValue("tModLoader.WaitXSeconds", this.timeLeft / 60), 0.7f, true);
			uitextPanel.Width.Pixels = -10f;
			uitextPanel.Width.Percent = 0.5f;
			uitextPanel.Height.Pixels = 50f;
			uitextPanel.Left.Percent = 0f;
			uitextPanel.VAlign = 1f;
			uitextPanel.Top.Pixels = -30f;
			uitextPanel.BackgroundColor = Color.Orange;
			this.waitPanel = uitextPanel;
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x00513772 File Offset: 0x00511972
		public override void OnActivate()
		{
			base.OnActivate();
			this._area.AddOrRemoveChild(this._button, false);
			this._area.Append(this.waitPanel);
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x005137A0 File Offset: 0x005119A0
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (this.timeLeft > 0)
			{
				this.timeLeft--;
				this.waitPanel.SetText(Language.GetTextValue("tModLoader.WaitXSeconds", this.timeLeft / 60 + 1));
				if (this.timeLeft == 0)
				{
					this._area.AddOrRemoveChild(this.waitPanel, false);
					this._area.AddOrRemoveChild(this._button, true);
				}
			}
		}

		// Token: 0x04001A69 RID: 6761
		private int timeLeft = -1;

		// Token: 0x04001A6A RID: 6762
		private UITextPanel<string> waitPanel;
	}
}
