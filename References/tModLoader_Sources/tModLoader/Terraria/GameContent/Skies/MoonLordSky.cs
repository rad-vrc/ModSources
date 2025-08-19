using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000569 RID: 1385
	public class MoonLordSky : CustomSky
	{
		// Token: 0x06004162 RID: 16738 RVA: 0x005E68D7 File Offset: 0x005E4AD7
		public MoonLordSky(bool forPlayer)
		{
			this._forPlayer = forPlayer;
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x005E68F8 File Offset: 0x005E4AF8
		public override void OnLoad()
		{
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x005E68FC File Offset: 0x005E4AFC
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

		// Token: 0x06004165 RID: 16741 RVA: 0x005E6954 File Offset: 0x005E4B54
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

		// Token: 0x06004166 RID: 16742 RVA: 0x005E69CC File Offset: 0x005E4BCC
		public override Color OnTileColor(Color inColor)
		{
			float intensity = this.GetIntensity();
			return new Color(Vector4.Lerp(new Vector4(0.5f, 0.8f, 1f, 1f), inColor.ToVector4(), 1f - intensity));
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x005E6A14 File Offset: 0x005E4C14
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

		// Token: 0x06004168 RID: 16744 RVA: 0x005E6AA0 File Offset: 0x005E4CA0
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 0f && minDepth < 0f)
			{
				float intensity = this.GetIntensity();
				spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * intensity);
			}
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x005E6AF0 File Offset: 0x005E4CF0
		public override float GetCloudAlpha()
		{
			return 1f - this._fadeOpacity;
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x005E6AFE File Offset: 0x005E4CFE
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

		// Token: 0x0600416B RID: 16747 RVA: 0x005E6B26 File Offset: 0x005E4D26
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
			if (!this._forPlayer)
			{
				this._fadeOpacity = 0f;
			}
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x005E6B42 File Offset: 0x005E4D42
		public override void Reset()
		{
			this._isActive = false;
			this._fadeOpacity = 0f;
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x005E6B56 File Offset: 0x005E4D56
		public override bool IsActive()
		{
			return this._isActive || this._fadeOpacity > 0.001f;
		}

		// Token: 0x040058C4 RID: 22724
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040058C5 RID: 22725
		private bool _isActive;

		// Token: 0x040058C6 RID: 22726
		private int _moonLordIndex = -1;

		// Token: 0x040058C7 RID: 22727
		private bool _forPlayer;

		// Token: 0x040058C8 RID: 22728
		private float _fadeOpacity;
	}
}
