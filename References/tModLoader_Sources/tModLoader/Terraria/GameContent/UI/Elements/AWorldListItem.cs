using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020004F8 RID: 1272
	public abstract class AWorldListItem : UIPanel
	{
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06003D96 RID: 15766 RVA: 0x005CB276 File Offset: 0x005C9476
		public WorldFileData Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x005CB280 File Offset: 0x005C9480
		private void UpdateGlitchAnimation(UIElement affectedElement)
		{
			int glitchFrame = this._glitchFrame;
			int minValue = 3;
			int num = 3;
			if (this._glitchFrame == 0)
			{
				minValue = 15;
				num = 120;
			}
			int num2 = this._glitchFrameCounter + 1;
			this._glitchFrameCounter = num2;
			if (num2 >= Main.rand.Next(minValue, num + 1))
			{
				this._glitchFrameCounter = 0;
				this._glitchFrame = (this._glitchFrame + 1) % 16;
				if ((this._glitchFrame == 4 || this._glitchFrame == 8 || this._glitchFrame == 12) && Main.rand.Next(3) == 0)
				{
					this._glitchVariation = Main.rand.Next(7);
				}
			}
			(affectedElement as UIImageFramed).SetFrame(7, 16, this._glitchVariation, this._glitchFrame, 0, 0);
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x005CB338 File Offset: 0x005C9538
		protected void GetDifficulty(out string expertText, out Color gameModeColor)
		{
			expertText = "";
			gameModeColor = Color.White;
			if (this._data.GameMode == 3)
			{
				expertText = Language.GetTextValue("UI.Creative");
				gameModeColor = Main.creativeModeColor;
				return;
			}
			int num = 1;
			int gameMode = this._data.GameMode;
			if (gameMode != 1)
			{
				if (gameMode == 2)
				{
					num = 3;
				}
			}
			else
			{
				num = 2;
			}
			if (this._data.ForTheWorthy)
			{
				num++;
			}
			switch (num)
			{
			case 2:
				expertText = Language.GetTextValue("UI.Expert");
				gameModeColor = Main.mcColor;
				return;
			case 3:
				expertText = Language.GetTextValue("UI.Master");
				gameModeColor = Main.hcColor;
				return;
			case 4:
				expertText = Language.GetTextValue("UI.Legendary");
				gameModeColor = Main.legendaryModeColor;
				return;
			default:
				expertText = Language.GetTextValue("UI.Normal");
				return;
			}
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x005CB414 File Offset: 0x005C9614
		protected Asset<Texture2D> GetIcon()
		{
			if (this._data.ZenithWorld)
			{
				return Main.Assets.Request<Texture2D>("Images/UI/Icon" + (this._data.IsHardMode ? "Hallow" : "") + "Everything");
			}
			if (this._data.DrunkWorld)
			{
				return Main.Assets.Request<Texture2D>("Images/UI/Icon" + (this._data.IsHardMode ? "Hallow" : "") + "CorruptionCrimson");
			}
			if (this._data.ForTheWorthy)
			{
				return this.GetSeedIcon("FTW");
			}
			if (this._data.NotTheBees)
			{
				return this.GetSeedIcon("NotTheBees");
			}
			if (this._data.Anniversary)
			{
				return this.GetSeedIcon("Anniversary");
			}
			if (this._data.DontStarve)
			{
				return this.GetSeedIcon("DontStarve");
			}
			if (this._data.RemixWorld)
			{
				return this.GetSeedIcon("Remix");
			}
			if (this._data.NoTrapsWorld)
			{
				return this.GetSeedIcon("Traps");
			}
			return Main.Assets.Request<Texture2D>("Images/UI/Icon" + (this._data.IsHardMode ? "Hallow" : "") + (this._data.HasCorruption ? "Corruption" : "Crimson"));
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x005CB580 File Offset: 0x005C9780
		protected UIElement GetIconElement()
		{
			if (this._data.DrunkWorld && this._data.RemixWorld)
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/IconEverythingAnimated");
				UIImageFramed uiimageFramed = new UIImageFramed(asset, asset.Frame(7, 16, 0, 0, 0, 0));
				uiimageFramed.Left = new StyleDimension(4f, 0f);
				uiimageFramed.OnUpdate += this.UpdateGlitchAnimation;
				return uiimageFramed;
			}
			return new UIImage(this.GetIcon())
			{
				Left = new StyleDimension(4f, 0f)
			};
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x005CB610 File Offset: 0x005C9810
		private Asset<Texture2D> GetSeedIcon(string seed)
		{
			return Main.Assets.Request<Texture2D>("Images/UI/Icon" + (this._data.IsHardMode ? "Hallow" : "") + (this._data.HasCorruption ? "Corruption" : "Crimson") + seed);
		}

		// Token: 0x0400565E RID: 22110
		protected WorldFileData _data;

		// Token: 0x0400565F RID: 22111
		protected int _glitchFrameCounter;

		// Token: 0x04005660 RID: 22112
		protected int _glitchFrame;

		// Token: 0x04005661 RID: 22113
		protected int _glitchVariation;
	}
}
