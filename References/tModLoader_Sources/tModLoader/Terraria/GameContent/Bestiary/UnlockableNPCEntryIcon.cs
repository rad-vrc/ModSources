using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006AB RID: 1707
	public class UnlockableNPCEntryIcon : IEntryIcon
	{
		// Token: 0x06004877 RID: 18551 RVA: 0x00649204 File Offset: 0x00647404
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

		// Token: 0x06004878 RID: 18552 RVA: 0x0064929F File Offset: 0x0064749F
		public IEntryIcon CreateClone()
		{
			return new UnlockableNPCEntryIcon(this._npcNetId, 0f, 0f, 0f, 0f, this._overrideNameKey);
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x006492C8 File Offset: 0x006474C8
		public void Update(BestiaryUICollectionInfo providedInfo, Rectangle hitbox, EntryIconDrawSettings settings)
		{
			Vector2 positionOffsetCache = default(Vector2);
			int? num = null;
			int? num2 = null;
			int? num3 = null;
			bool wet = false;
			float num4 = 0f;
			Asset<Texture2D> asset = null;
			NPCID.Sets.NPCBestiaryDrawModifiers value;
			if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(this._npcNetId, out value))
			{
				this._npcCache.rotation = value.Rotation;
				this._npcCache.scale = value.Scale;
				if (value.PortraitScale != null && settings.IsPortrait)
				{
					this._npcCache.scale = value.PortraitScale.Value;
				}
				positionOffsetCache = value.Position;
				num = value.Frame;
				num2 = value.Direction;
				num3 = value.SpriteDirection;
				num4 = value.Velocity;
				wet = value.IsWet;
				if (value.PortraitPositionXOverride != null && settings.IsPortrait)
				{
					positionOffsetCache.X = value.PortraitPositionXOverride.Value;
				}
				if (value.PortraitPositionYOverride != null && settings.IsPortrait)
				{
					positionOffsetCache.Y = value.PortraitPositionYOverride.Value;
				}
				if (value.CustomTexturePath != null)
				{
					asset = ModContent.Request<Texture2D>(value.CustomTexturePath, 2);
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

		// Token: 0x0600487A RID: 18554 RVA: 0x0064955C File Offset: 0x0064775C
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

		// Token: 0x0600487B RID: 18555 RVA: 0x00649614 File Offset: 0x00647814
		private void AdjustSpecialSpawnRulesForVisuals(EntryIconDrawSettings settings)
		{
			int value;
			if (NPCID.Sets.SpecialSpawningRules.TryGetValue(this._npcNetId, out value) && value == 0)
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
					if ((this._npcCache.localAI[0] += 1f) >= 44f)
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

		// Token: 0x0600487C RID: 18556 RVA: 0x006497F8 File Offset: 0x006479F8
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

		// Token: 0x0600487D RID: 18557 RVA: 0x00649854 File Offset: 0x00647A54
		public void Draw(BestiaryUICollectionInfo providedInfo, SpriteBatch spriteBatch, EntryIconDrawSettings settings)
		{
			this.UpdatePosition(settings);
			if (this._customTexture != null)
			{
				spriteBatch.Draw(this._customTexture.Value, this._npcCache.Center, null, Color.White, 0f, this._customTexture.Size() / 2f, this._npcCache.scale, 0, 0f);
				return;
			}
			ITownNPCProfile profile;
			if (this._npcCache.townNPC && TownNPCProfiles.Instance.GetProfile(this._npcCache, out profile))
			{
				TextureAssets.Npc[this._npcCache.type] = profile.GetTextureNPCShouldUse(this._npcCache);
			}
			Main.instance.DrawNPCDirect(spriteBatch, this._npcCache, this._npcCache.behindTiles, Vector2.Zero);
		}

		// Token: 0x0600487E RID: 18558 RVA: 0x00649928 File Offset: 0x00647B28
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

		// Token: 0x0600487F RID: 18559 RVA: 0x0064996F File Offset: 0x00647B6F
		public bool GetUnlockState(BestiaryUICollectionInfo providedInfo)
		{
			return providedInfo.UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0;
		}

		// Token: 0x04005C39 RID: 23609
		private int _npcNetId;

		// Token: 0x04005C3A RID: 23610
		private NPC _npcCache;

		// Token: 0x04005C3B RID: 23611
		private bool _firstUpdateDone;

		// Token: 0x04005C3C RID: 23612
		private Asset<Texture2D> _customTexture;

		// Token: 0x04005C3D RID: 23613
		private Vector2 _positionOffsetCache;

		// Token: 0x04005C3E RID: 23614
		private string _overrideNameKey;
	}
}
