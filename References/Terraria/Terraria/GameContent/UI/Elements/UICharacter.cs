using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000388 RID: 904
	public class UICharacter : UIElement
	{
		// Token: 0x060028D7 RID: 10455 RVA: 0x0058F04C File Offset: 0x0058D24C
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
			this._texture = Main.Assets.Request<Texture2D>("Images/UI/PlayerBackground", 1);
			this.UseImmediateMode = true;
			this._animated = animated;
			this._drawsBackPanel = hasBackPanel;
			this._characterScale = characterScale;
			this.OverrideSamplerState = SamplerState.PointClamp;
			this._petProjectiles = UICharacter.NoPets;
			this.PreparePetProjectiles();
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x0058F114 File Offset: 0x0058D314
		private void PreparePetProjectiles()
		{
			if (this._player.hideMisc[0])
			{
				return;
			}
			Item item = this._player.miscEquips[0];
			if (item.IsAir)
			{
				return;
			}
			int shoot = item.shoot;
			this._petProjectiles = new Projectile[]
			{
				this.PreparePetProjectiles_CreatePetProjectileDummy(shoot)
			};
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x0058F169 File Offset: 0x0058D369
		private Projectile PreparePetProjectiles_CreatePetProjectileDummy(int projectileId)
		{
			Projectile projectile = new Projectile();
			projectile.SetDefaults(projectileId);
			projectile.isAPreviewDummy = true;
			return projectile;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x0058F180 File Offset: 0x0058D380
		public override void Update(GameTime gameTime)
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

		// Token: 0x060028DB RID: 10459 RVA: 0x0058F1E4 File Offset: 0x0058D3E4
		private void UpdateAnim()
		{
			if (!this._animated)
			{
				this._player.bodyFrame.Y = (this._player.legFrame.Y = (this._player.headFrame.Y = 0));
				return;
			}
			int num = (int)(Main.GlobalTimeWrappedHourly / 0.07f) % 14 + 6;
			this._player.bodyFrame.Y = (this._player.legFrame.Y = (this._player.headFrame.Y = num * 56));
			this._player.WingFrame(false);
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0058F28C File Offset: 0x0058D48C
		protected override void DrawSelf(SpriteBatch spriteBatch)
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

		// Token: 0x060028DD RID: 10461 RVA: 0x0058F35C File Offset: 0x0058D55C
		private Vector2 GetPlayerPosition(ref CalculatedStyle dimensions)
		{
			Vector2 result = dimensions.Position() + new Vector2(dimensions.Width * 0.5f - (float)(this._player.width >> 1), dimensions.Height * 0.5f - (float)(this._player.height >> 1));
			if (this._petProjectiles.Length != 0)
			{
				result.X -= 10f;
			}
			return result;
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x0058F3CC File Offset: 0x0058D5CC
		public void DrawPets(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 playerPosition = this.GetPlayerPosition(ref dimensions);
			for (int i = 0; i < this._petProjectiles.Length; i++)
			{
				Projectile projectile = this._petProjectiles[i];
				Vector2 value = playerPosition + new Vector2(0f, (float)this._player.height) + new Vector2(20f, 0f) + new Vector2(0f, (float)(-(float)projectile.height));
				projectile.position = value + Main.screenPosition;
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
			spriteBatch.Begin(SpriteSortMode.Immediate, spriteBatch.GraphicsDevice.BlendState, spriteBatch.GraphicsDevice.SamplerStates[0], spriteBatch.GraphicsDevice.DepthStencilState, spriteBatch.GraphicsDevice.RasterizerState, null, Main.UIScaleMatrix);
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x0058F520 File Offset: 0x0058D720
		public void SetAnimated(bool animated)
		{
			this._animated = animated;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x0058F529 File Offset: 0x0058D729
		public bool IsAnimated
		{
			get
			{
				return this._animated;
			}
		}

		// Token: 0x04004C20 RID: 19488
		private Player _player;

		// Token: 0x04004C21 RID: 19489
		private Projectile[] _petProjectiles;

		// Token: 0x04004C22 RID: 19490
		private Asset<Texture2D> _texture;

		// Token: 0x04004C23 RID: 19491
		private static Item _blankItem = new Item();

		// Token: 0x04004C24 RID: 19492
		private bool _animated;

		// Token: 0x04004C25 RID: 19493
		private bool _drawsBackPanel;

		// Token: 0x04004C26 RID: 19494
		private float _characterScale = 1f;

		// Token: 0x04004C27 RID: 19495
		private int _animationCounter;

		// Token: 0x04004C28 RID: 19496
		private static readonly Projectile[] NoPets = new Projectile[0];
	}
}
