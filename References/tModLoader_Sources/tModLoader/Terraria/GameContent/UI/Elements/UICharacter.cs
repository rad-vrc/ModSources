using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200050B RID: 1291
	public class UICharacter : UIElement
	{
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06003E1B RID: 15899 RVA: 0x005CF687 File Offset: 0x005CD887
		public bool IsAnimated
		{
			get
			{
				return this._animated;
			}
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x005CF690 File Offset: 0x005CD890
		public UICharacter(Player player, bool animated = false, bool hasBackPanel = true, float characterScale = 1f, bool useAClone = false)
		{
			this._player = player;
			if (useAClone)
			{
				this._player = player.SerializedClone();
				this._player.dead = false;
				this._player.PlayerFrame();
			}
			this.Width.Set(59f, 0f);
			this.Height.Set(58f, 0f);
			this._texture = Main.Assets.Request<Texture2D>("Images/UI/PlayerBackground");
			this.UseImmediateMode = true;
			this._animated = animated;
			this._drawsBackPanel = hasBackPanel;
			this._characterScale = characterScale;
			this.OverrideSamplerState = SamplerState.PointClamp;
			this._petProjectiles = UICharacter.NoPets;
			this.PreparePetProjectiles();
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x005CF754 File Offset: 0x005CD954
		private void PreparePetProjectiles()
		{
			if (!this._player.hideMisc[0])
			{
				Item item = this._player.miscEquips[0];
				if (!item.IsAir && item.buffType > 0 && Main.vanityPet[item.buffType] && !Main.lightPet[item.buffType])
				{
					int shoot = item.shoot;
					this._petProjectiles = new Projectile[]
					{
						this.PreparePetProjectiles_CreatePetProjectileDummy(shoot)
					};
				}
			}
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x005CF7CC File Offset: 0x005CD9CC
		private Projectile PreparePetProjectiles_CreatePetProjectileDummy(int projectileId)
		{
			Projectile projectile = new Projectile();
			projectile.SetDefaults(projectileId);
			projectile.isAPreviewDummy = true;
			return projectile;
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x005CF7E4 File Offset: 0x005CD9E4
		public override void Update(GameTime gameTime)
		{
			using (new Main.CurrentPlayerOverride(this._player))
			{
				this._player.ResetEffects();
				this._player.ResetVisibleAccessories();
				this._player.UpdateMiscCounter();
				this._player.UpdateDyes();
				this._player.PlayerFrame();
				if (this._animated)
				{
					this._animationCounter++;
				}
				base.Update(gameTime);
			}
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x005CF86C File Offset: 0x005CDA6C
		private void UpdateAnim()
		{
			if (!this._animated)
			{
				this._player.bodyFrame.Y = (this._player.legFrame.Y = (this._player.headFrame.Y = 0));
				return;
			}
			int num = (int)(Main.GlobalTimeWrappedHourly / 0.07f) % 14 + 6;
			this._player.bodyFrame.Y = (this._player.legFrame.Y = (this._player.headFrame.Y = num * 56));
			this._player.WingFrame(false, false);
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x005CF914 File Offset: 0x005CDB14
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			using (new Main.CurrentPlayerOverride(this._player))
			{
				CalculatedStyle dimensions = base.GetDimensions();
				if (this._drawsBackPanel)
				{
					spriteBatch.Draw(this._texture.Value, dimensions.Position(), Color.White);
				}
				this.UpdateAnim();
				this.DrawPets(spriteBatch);
				Vector2 playerPosition = this.GetPlayerPosition(ref dimensions);
				Item item = this._player.inventory[this._player.selectedItem];
				this._player.inventory[this._player.selectedItem] = UICharacter._blankItem;
				Main.PlayerRenderer.DrawPlayer(Main.Camera, this._player, playerPosition + Main.screenPosition, 0f, Vector2.Zero, 0f, this._characterScale);
				this._player.inventory[this._player.selectedItem] = item;
			}
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x005CFA0C File Offset: 0x005CDC0C
		private Vector2 GetPlayerPosition(ref CalculatedStyle dimensions)
		{
			Vector2 result = dimensions.Position() + new Vector2(dimensions.Width * 0.5f - (float)(this._player.width >> 1), dimensions.Height * 0.5f - (float)(this._player.height >> 1));
			if (this._petProjectiles.Length != 0)
			{
				result.X -= 10f;
			}
			return result;
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x005CFA7C File Offset: 0x005CDC7C
		public void DrawPets(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 playerPosition = this.GetPlayerPosition(ref dimensions);
			for (int i = 0; i < this._petProjectiles.Length; i++)
			{
				Projectile projectile = this._petProjectiles[i];
				Vector2 vector = playerPosition + new Vector2(0f, (float)this._player.height) + new Vector2(20f, 0f) + new Vector2(0f, (float)(-(float)projectile.height));
				projectile.position = vector + Main.screenPosition;
				projectile.velocity = new Vector2(0.1f, 0f);
				projectile.direction = 1;
				projectile.owner = Main.myPlayer;
				ProjectileID.Sets.CharacterPreviewAnimations[projectile.type].ApplyTo(projectile, this._animated);
				Player player = Main.player[Main.myPlayer];
				Main.player[Main.myPlayer] = this._player;
				Main.instance.DrawProjDirect(projectile);
				Main.player[Main.myPlayer] = player;
			}
			spriteBatch.End();
			spriteBatch.Begin(1, spriteBatch.GraphicsDevice.BlendState, spriteBatch.GraphicsDevice.SamplerStates[0], spriteBatch.GraphicsDevice.DepthStencilState, spriteBatch.GraphicsDevice.RasterizerState, null, Main.UIScaleMatrix);
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x005CFBD0 File Offset: 0x005CDDD0
		public void SetAnimated(bool animated)
		{
			this._animated = animated;
		}

		// Token: 0x040056BB RID: 22203
		private Player _player;

		// Token: 0x040056BC RID: 22204
		private Projectile[] _petProjectiles;

		// Token: 0x040056BD RID: 22205
		private Asset<Texture2D> _texture;

		// Token: 0x040056BE RID: 22206
		private static Item _blankItem = new Item();

		// Token: 0x040056BF RID: 22207
		private bool _animated;

		// Token: 0x040056C0 RID: 22208
		private bool _drawsBackPanel;

		// Token: 0x040056C1 RID: 22209
		private float _characterScale = 1f;

		// Token: 0x040056C2 RID: 22210
		private int _animationCounter;

		// Token: 0x040056C3 RID: 22211
		private static readonly Projectile[] NoPets = new Projectile[0];
	}
}
