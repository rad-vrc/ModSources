using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000325 RID: 805
	public class MoonLordSky : CustomSky
	{
		// Token: 0x0600247E RID: 9342 RVA: 0x0055BCBD File Offset: 0x00559EBD
		public MoonLordSky(bool forPlayer)
		{
			this._forPlayer = forPlayer;
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void OnLoad()
		{
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x0055BCE0 File Offset: 0x00559EE0
		public override void Update(GameTime gameTime)
		{
			if (this._forPlayer)
			{
				if (this._isActive)
				{
					this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
					return;
				}
				this._fadeOpacity = Math.Max(0f, this._fadeOpacity - 0.01f);
			}
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x0055BD38 File Offset: 0x00559F38
		private float GetIntensity()
		{
			if (this._forPlayer)
			{
				return this._fadeOpacity;
			}
			if (this.UpdateMoonLordIndex())
			{
				float x = 0f;
				if (this._moonLordIndex != -1)
				{
					x = Vector2.Distance(Main.player[Main.myPlayer].Center, Main.npc[this._moonLordIndex].Center);
				}
				return 1f - Utils.SmoothStep(3000f, 6000f, x);
			}
			return 0f;
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x0055BDB0 File Offset: 0x00559FB0
		public override Color OnTileColor(Color inColor)
		{
			float intensity = this.GetIntensity();
			return new Color(Vector4.Lerp(new Vector4(0.5f, 0.8f, 1f, 1f), inColor.ToVector4(), 1f - intensity));
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x0055BDF8 File Offset: 0x00559FF8
		private bool UpdateMoonLordIndex()
		{
			if (this._moonLordIndex >= 0 && Main.npc[this._moonLordIndex].active && Main.npc[this._moonLordIndex].type == 398)
			{
				return true;
			}
			int num = -1;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == 398)
				{
					num = i;
					break;
				}
			}
			this._moonLordIndex = num;
			return num != -1;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x0055BE84 File Offset: 0x0055A084
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 0f && minDepth < 0f)
			{
				float intensity = this.GetIntensity();
				spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * intensity);
			}
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x0055BED4 File Offset: 0x0055A0D4
		public override float GetCloudAlpha()
		{
			return 1f - this._fadeOpacity;
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x0055BEE2 File Offset: 0x0055A0E2
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
			if (this._forPlayer)
			{
				this._fadeOpacity = 0.002f;
				return;
			}
			this._fadeOpacity = 1f;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x0055BF0A File Offset: 0x0055A10A
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
			if (!this._forPlayer)
			{
				this._fadeOpacity = 0f;
			}
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x0055BF26 File Offset: 0x0055A126
		public override void Reset()
		{
			this._isActive = false;
			this._fadeOpacity = 0f;
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x0055BF3A File Offset: 0x0055A13A
		public override bool IsActive()
		{
			return this._isActive || this._fadeOpacity > 0.001f;
		}

		// Token: 0x040048A5 RID: 18597
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040048A6 RID: 18598
		private bool _isActive;

		// Token: 0x040048A7 RID: 18599
		private int _moonLordIndex = -1;

		// Token: 0x040048A8 RID: 18600
		private bool _forPlayer;

		// Token: 0x040048A9 RID: 18601
		private float _fadeOpacity;
	}
}
