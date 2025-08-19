using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002F7 RID: 759
	public class UnlockableNPCEntryIcon : IEntryIcon
	{
		// Token: 0x060023B3 RID: 9139 RVA: 0x0055784C File Offset: 0x00555A4C
		public UnlockableNPCEntryIcon(int npcNetId, float ai0 = 0f, float ai1 = 0f, float ai2 = 0f, float ai3 = 0f, string overrideNameKey = null)
		{
			this._npcNetId = npcNetId;
			this._npcCache = new NPC();
			this._npcCache.IsABestiaryIconDummy = true;
			this._npcCache.SetDefaults(this._npcNetId, default(NPCSpawnParams));
			this._firstUpdateDone = false;
			this._npcCache.ai[0] = ai0;
			this._npcCache.ai[1] = ai1;
			this._npcCache.ai[2] = ai2;
			this._npcCache.ai[3] = ai3;
			this._customTexture = null;
			this._overrideNameKey = overrideNameKey;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x005578E7 File Offset: 0x00555AE7
		public IEntryIcon CreateClone()
		{
			return new UnlockableNPCEntryIcon(this._npcNetId, 0f, 0f, 0f, 0f, this._overrideNameKey);
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x00557910 File Offset: 0x00555B10
		public void Update(BestiaryUICollectionInfo providedInfo, Rectangle hitbox, EntryIconDrawSettings settings)
		{
			Vector2 positionOffsetCache = default(Vector2);
			int? num = null;
			int? num2 = null;
			int? num3 = null;
			bool wet = false;
			float num4 = 0f;
			Asset<Texture2D> asset = null;
			NPCID.Sets.NPCBestiaryDrawModifiers npcbestiaryDrawModifiers;
			if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(this._npcNetId, out npcbestiaryDrawModifiers))
			{
				this._npcCache.rotation = npcbestiaryDrawModifiers.Rotation;
				this._npcCache.scale = npcbestiaryDrawModifiers.Scale;
				if (npcbestiaryDrawModifiers.PortraitScale != null && settings.IsPortrait)
				{
					this._npcCache.scale = npcbestiaryDrawModifiers.PortraitScale.Value;
				}
				positionOffsetCache = npcbestiaryDrawModifiers.Position;
				num = npcbestiaryDrawModifiers.Frame;
				num2 = npcbestiaryDrawModifiers.Direction;
				num3 = npcbestiaryDrawModifiers.SpriteDirection;
				num4 = npcbestiaryDrawModifiers.Velocity;
				wet = npcbestiaryDrawModifiers.IsWet;
				if (npcbestiaryDrawModifiers.PortraitPositionXOverride != null && settings.IsPortrait)
				{
					positionOffsetCache.X = npcbestiaryDrawModifiers.PortraitPositionXOverride.Value;
				}
				if (npcbestiaryDrawModifiers.PortraitPositionYOverride != null && settings.IsPortrait)
				{
					positionOffsetCache.Y = npcbestiaryDrawModifiers.PortraitPositionYOverride.Value;
				}
				if (npcbestiaryDrawModifiers.CustomTexturePath != null)
				{
					asset = Main.Assets.Request<Texture2D>(npcbestiaryDrawModifiers.CustomTexturePath, 1);
				}
				if (asset != null && asset.IsLoaded)
				{
					this._customTexture = asset;
				}
			}
			this._positionOffsetCache = positionOffsetCache;
			this.UpdatePosition(settings);
			if (NPCID.Sets.TrailingMode[this._npcCache.type] != -1)
			{
				for (int i = 0; i < this._npcCache.oldPos.Length; i++)
				{
					this._npcCache.oldPos[i] = this._npcCache.position;
				}
			}
			this._npcCache.direction = (this._npcCache.spriteDirection = ((num2 != null) ? num2.Value : -1));
			if (num3 != null)
			{
				this._npcCache.spriteDirection = num3.Value;
			}
			this._npcCache.wet = wet;
			this.AdjustSpecialSpawnRulesForVisuals(settings);
			this.SimulateFirstHover(num4);
			if (num == null && (settings.IsPortrait || settings.IsHovered))
			{
				this._npcCache.velocity.X = (float)this._npcCache.direction * num4;
				this._npcCache.FindFrame();
				return;
			}
			if (num != null)
			{
				this._npcCache.FindFrame();
				this._npcCache.frame.Y = this._npcCache.frame.Height * num.Value;
			}
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x00557BA0 File Offset: 0x00555DA0
		private void UpdatePosition(EntryIconDrawSettings settings)
		{
			if (this._npcCache.noGravity)
			{
				this._npcCache.Center = settings.iconbox.Center.ToVector2() + this._positionOffsetCache;
			}
			else
			{
				this._npcCache.Bottom = settings.iconbox.TopLeft() + settings.iconbox.Size() * new Vector2(0.5f, 1f) + new Vector2(0f, -8f) + this._positionOffsetCache;
			}
			this._npcCache.position = this._npcCache.position.Floor();
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x00557C58 File Offset: 0x00555E58
		private void AdjustSpecialSpawnRulesForVisuals(EntryIconDrawSettings settings)
		{
			int num;
			if (NPCID.Sets.SpecialSpawningRules.TryGetValue(this._npcNetId, out num) && num == 0)
			{
				Point point = (this._npcCache.position - this._npcCache.rotation.ToRotationVector2() * -1600f).ToTileCoordinates();
				this._npcCache.ai[0] = (float)point.X;
				this._npcCache.ai[1] = (float)point.Y;
			}
			int npcNetId = this._npcNetId;
			if (npcNetId <= 539)
			{
				if (npcNetId <= 330)
				{
					if (npcNetId == 244)
					{
						this._npcCache.AI_001_SetRainbowSlimeColor();
						return;
					}
					if (npcNetId == 299)
					{
						goto IL_135;
					}
					if (npcNetId != 330)
					{
						return;
					}
				}
				else
				{
					if (npcNetId == 356)
					{
						this._npcCache.ai[2] = 1f;
						return;
					}
					if (npcNetId != 372)
					{
						if (npcNetId - 538 > 1)
						{
							return;
						}
						goto IL_135;
					}
				}
			}
			else if (npcNetId <= 636)
			{
				if (npcNetId - 586 > 1 && npcNetId - 619 > 1)
				{
					if (npcNetId != 636)
					{
						return;
					}
					this._npcCache.Opacity = 1f;
					float[] localAI = this._npcCache.localAI;
					int num2 = 0;
					float num3 = localAI[num2] + 1f;
					localAI[num2] = num3;
					if (num3 >= 44f)
					{
						this._npcCache.localAI[0] = 0f;
						return;
					}
					return;
				}
			}
			else
			{
				if (npcNetId - 639 <= 6)
				{
					goto IL_135;
				}
				if (npcNetId == 656)
				{
					this._npcCache.townNpcVariationIndex = 1;
					return;
				}
				if (npcNetId != 670)
				{
					return;
				}
				this._npcCache.townNpcVariationIndex = 0;
				return;
			}
			this._npcCache.alpha = 0;
			return;
			IL_135:
			if (settings.IsPortrait && this._npcCache.frame.Y == 0)
			{
				this._npcCache.frame.Y = this._npcCache.frame.Height;
				return;
			}
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x00557E3C File Offset: 0x0055603C
		private void SimulateFirstHover(float velocity)
		{
			if (!this._firstUpdateDone)
			{
				this._firstUpdateDone = true;
				this._npcCache.SetFrameSize();
				this._npcCache.velocity.X = (float)this._npcCache.direction * velocity;
				for (int i = 0; i < 1; i++)
				{
					this._npcCache.FindFrame();
				}
			}
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x00557E98 File Offset: 0x00556098
		public void Draw(BestiaryUICollectionInfo providedInfo, SpriteBatch spriteBatch, EntryIconDrawSettings settings)
		{
			this.UpdatePosition(settings);
			if (this._customTexture != null)
			{
				spriteBatch.Draw(this._customTexture.Value, this._npcCache.Center, null, Color.White, 0f, this._customTexture.Size() / 2f, this._npcCache.scale, SpriteEffects.None, 0f);
				return;
			}
			ITownNPCProfile townNPCProfile;
			if (this._npcCache.townNPC && TownNPCProfiles.Instance.GetProfile(this._npcCache.type, out townNPCProfile))
			{
				TextureAssets.Npc[this._npcCache.type] = townNPCProfile.GetTextureNPCShouldUse(this._npcCache);
			}
			Main.instance.DrawNPCDirect(spriteBatch, this._npcCache, this._npcCache.behindTiles, Vector2.Zero);
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x00557F70 File Offset: 0x00556170
		public string GetHoverText(BestiaryUICollectionInfo providedInfo)
		{
			string result = Lang.GetNPCNameValue(this._npcCache.netID);
			if (!string.IsNullOrWhiteSpace(this._overrideNameKey))
			{
				result = Language.GetTextValue(this._overrideNameKey);
			}
			if (this.GetUnlockState(providedInfo))
			{
				return result;
			}
			return "???";
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x00557FB7 File Offset: 0x005561B7
		public bool GetUnlockState(BestiaryUICollectionInfo providedInfo)
		{
			return providedInfo.UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0;
		}

		// Token: 0x0400483D RID: 18493
		private int _npcNetId;

		// Token: 0x0400483E RID: 18494
		private NPC _npcCache;

		// Token: 0x0400483F RID: 18495
		private bool _firstUpdateDone;

		// Token: 0x04004840 RID: 18496
		private Asset<Texture2D> _customTexture;

		// Token: 0x04004841 RID: 18497
		private Vector2 _positionOffsetCache;

		// Token: 0x04004842 RID: 18498
		private string _overrideNameKey;
	}
}
