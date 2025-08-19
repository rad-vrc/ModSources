using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Animations;
using Terraria.GameContent.Skies.CreditsRoll;
using Terraria.Graphics.Effects;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000566 RID: 1382
	public class CreditsRollSky : CustomSky
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x005E579B File Offset: 0x005E399B
		public int AmountOfTimeNeededForFullPlay
		{
			get
			{
				return this._endTime;
			}
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x005E57A3 File Offset: 0x005E39A3
		public CreditsRollSky()
		{
			this.EnsureSegmentsAreMade();
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x005E57D4 File Offset: 0x005E39D4
		public override void Update(GameTime gameTime)
		{
			if (Main.gamePaused || !Main.hasFocus)
			{
				return;
			}
			this._currentTime++;
			float num = 0.008333334f;
			if (Main.gameMenu)
			{
				num = 0.06666667f;
			}
			this._opacity = MathHelper.Clamp(this._opacity + num * (float)this._wantsToBeSeen.ToDirectionInt(), 0f, 1f);
			if (this._opacity == 0f && !this._wantsToBeSeen)
			{
				this._isActive = false;
				return;
			}
			bool flag = true;
			if (!Main.CanPlayCreditsRoll())
			{
				flag = false;
			}
			if (this._currentTime >= this._endTime)
			{
				flag = false;
			}
			if (!flag)
			{
				SkyManager.Instance.Deactivate("CreditsRoll", Array.Empty<object>());
			}
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x005E588C File Offset: 0x005E3A8C
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			float num = 1f;
			if (num >= minDepth && num <= maxDepth)
			{
				Vector2 anchorPositionOnScreen = Main.ScreenSize.ToVector2() / 2f;
				if (Main.gameMenu)
				{
					anchorPositionOnScreen.Y = 300f;
				}
				GameAnimationSegment info = new GameAnimationSegment
				{
					SpriteBatch = spriteBatch,
					AnchorPositionOnScreen = anchorPositionOnScreen,
					TimeInAnimation = this._currentTime,
					DisplayOpacity = this._opacity
				};
				List<IAnimationSegment> list = this._segmentsInGame;
				if (Main.gameMenu)
				{
					list = this._segmentsInMainMenu;
				}
				for (int i = 0; i < list.Count; i++)
				{
					list[i].Draw(ref info);
				}
			}
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x005E5946 File Offset: 0x005E3B46
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x005E594E File Offset: 0x005E3B4E
		public override void Reset()
		{
			this._currentTime = 0;
			this.EnsureSegmentsAreMade();
			this._isActive = false;
			this._wantsToBeSeen = false;
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x005E596B File Offset: 0x005E3B6B
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
			this._wantsToBeSeen = true;
			if (this._opacity == 0f)
			{
				this.EnsureSegmentsAreMade();
				this._currentTime = 0;
			}
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x005E5998 File Offset: 0x005E3B98
		private void EnsureSegmentsAreMade()
		{
			if (this._segmentsInMainMenu.Count <= 0 || this._segmentsInGame.Count <= 0)
			{
				this._segmentsInGame.Clear();
				this._composer.FillSegments(this._segmentsInGame, out this._endTime, true);
				this._segmentsInMainMenu.Clear();
				this._composer.FillSegments(this._segmentsInMainMenu, out this._endTime, false);
			}
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x005E5A07 File Offset: 0x005E3C07
		public override void Deactivate(params object[] args)
		{
			this._wantsToBeSeen = false;
		}

		// Token: 0x040058AE RID: 22702
		private int _endTime;

		// Token: 0x040058AF RID: 22703
		private int _currentTime;

		// Token: 0x040058B0 RID: 22704
		private CreditsRollComposer _composer = new CreditsRollComposer();

		// Token: 0x040058B1 RID: 22705
		private List<IAnimationSegment> _segmentsInGame = new List<IAnimationSegment>();

		// Token: 0x040058B2 RID: 22706
		private List<IAnimationSegment> _segmentsInMainMenu = new List<IAnimationSegment>();

		// Token: 0x040058B3 RID: 22707
		private bool _isActive;

		// Token: 0x040058B4 RID: 22708
		private bool _wantsToBeSeen;

		// Token: 0x040058B5 RID: 22709
		private float _opacity;
	}
}
