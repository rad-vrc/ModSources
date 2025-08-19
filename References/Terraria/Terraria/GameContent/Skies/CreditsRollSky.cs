using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Animations;
using Terraria.GameContent.Skies.CreditsRoll;
using Terraria.Graphics.Effects;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000320 RID: 800
	public class CreditsRollSky : CustomSky
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x0055A60D File Offset: 0x0055880D
		public int AmountOfTimeNeededForFullPlay
		{
			get
			{
				return this._endTime;
			}
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x0055A615 File Offset: 0x00558815
		public CreditsRollSky()
		{
			this.EnsureSegmentsAreMade();
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x0055A644 File Offset: 0x00558844
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
				SkyManager.Instance.Deactivate("CreditsRoll", new object[0]);
			}
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x0055A700 File Offset: 0x00558900
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			float num = 1f;
			if (num < minDepth || num > maxDepth)
			{
				return;
			}
			Vector2 anchorPositionOnScreen = Main.ScreenSize.ToVector2() / 2f;
			if (Main.gameMenu)
			{
				anchorPositionOnScreen.Y = 300f;
			}
			GameAnimationSegment gameAnimationSegment = new GameAnimationSegment
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
				list[i].Draw(ref gameAnimationSegment);
			}
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x0055A7B2 File Offset: 0x005589B2
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x0055A7BA File Offset: 0x005589BA
		public override void Reset()
		{
			this._currentTime = 0;
			this.EnsureSegmentsAreMade();
			this._isActive = false;
			this._wantsToBeSeen = false;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x0055A7D7 File Offset: 0x005589D7
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

		// Token: 0x06002456 RID: 9302 RVA: 0x0055A804 File Offset: 0x00558A04
		private void EnsureSegmentsAreMade()
		{
			if (this._segmentsInMainMenu.Count > 0 && this._segmentsInGame.Count > 0)
			{
				return;
			}
			this._segmentsInGame.Clear();
			this._composer.FillSegments(this._segmentsInGame, out this._endTime, true);
			this._segmentsInMainMenu.Clear();
			this._composer.FillSegments(this._segmentsInMainMenu, out this._endTime, false);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0055A874 File Offset: 0x00558A74
		public override void Deactivate(params object[] args)
		{
			this._wantsToBeSeen = false;
		}

		// Token: 0x04004885 RID: 18565
		private int _endTime;

		// Token: 0x04004886 RID: 18566
		private int _currentTime;

		// Token: 0x04004887 RID: 18567
		private CreditsRollComposer _composer = new CreditsRollComposer();

		// Token: 0x04004888 RID: 18568
		private List<IAnimationSegment> _segmentsInGame = new List<IAnimationSegment>();

		// Token: 0x04004889 RID: 18569
		private List<IAnimationSegment> _segmentsInMainMenu = new List<IAnimationSegment>();

		// Token: 0x0400488A RID: 18570
		private bool _isActive;

		// Token: 0x0400488B RID: 18571
		private bool _wantsToBeSeen;

		// Token: 0x0400488C RID: 18572
		private float _opacity;
	}
}
