using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Drawing;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria
{
	// Token: 0x0200003D RID: 61
	public class Mount
	{
		/// <summary> True if the player is currently using a mount. </summary>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0014C830 File Offset: 0x0014AA30
		public bool Active
		{
			get
			{
				return this._active;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0014C838 File Offset: 0x0014AA38
		public int Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0014C840 File Offset: 0x0014AA40
		public int FlyTime
		{
			get
			{
				return this._flyTime;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0014C848 File Offset: 0x0014AA48
		public int BuffType
		{
			get
			{
				return this._data.buff;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0014C855 File Offset: 0x0014AA55
		public int BodyFrame
		{
			get
			{
				return this._data.bodyFrame;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0014C862 File Offset: 0x0014AA62
		public int XOffset
		{
			get
			{
				return this._data.xOffset;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0014C86F File Offset: 0x0014AA6F
		public int YOffset
		{
			get
			{
				return this._data.yOffset;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0014C87C File Offset: 0x0014AA7C
		public int PlayerXOFfset
		{
			get
			{
				return this._data.playerXOffset;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0014C889 File Offset: 0x0014AA89
		public int PlayerOffset
		{
			get
			{
				if (!this._active)
				{
					return 0;
				}
				if (this._frame >= this._data.totalFrames)
				{
					return 0;
				}
				return this._data.playerYOffsets[this._frame];
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0014C8BC File Offset: 0x0014AABC
		public int PlayerOffsetHitbox
		{
			get
			{
				if (!this._active)
				{
					return 0;
				}
				return -this.PlayerOffset + this._data.heightBoost;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0014C8DB File Offset: 0x0014AADB
		public int PlayerHeadOffset
		{
			get
			{
				if (!this._active)
				{
					return 0;
				}
				return this._data.playerHeadOffset;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0014C8F2 File Offset: 0x0014AAF2
		public int HeightBoost
		{
			get
			{
				return this._data.heightBoost;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0014C900 File Offset: 0x0014AB00
		public float RunSpeed
		{
			get
			{
				if (this._type == 4 && this._frameState == 4)
				{
					return this._data.swimSpeed;
				}
				if ((this._type == 12 || this._type == 44 || this._type == 49) && this._frameState == 4)
				{
					return this._data.swimSpeed;
				}
				if (this._type == 12 && this._frameState == 2)
				{
					return this._data.runSpeed + 13.5f;
				}
				if (this._type == 44 && this._frameState == 2)
				{
					return this._data.runSpeed + 4f;
				}
				if (this._type == 5 && this._frameState == 2)
				{
					float num = this._fatigue / this._fatigueMax;
					return this._data.runSpeed + 4f * (1f - num);
				}
				if (this._type == 50 && this._frameState == 2)
				{
					return this._data.runSpeed + 2f;
				}
				if (this._shouldSuperCart)
				{
					return this._data.MinecartUpgradeRunSpeed;
				}
				return this._data.runSpeed;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0014CA26 File Offset: 0x0014AC26
		public float DashSpeed
		{
			get
			{
				if (this._shouldSuperCart)
				{
					return this._data.MinecartUpgradeDashSpeed;
				}
				return this._data.dashSpeed;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0014CA47 File Offset: 0x0014AC47
		public float Acceleration
		{
			get
			{
				if (this._shouldSuperCart)
				{
					return this._data.MinecartUpgradeAcceleration;
				}
				return this._data.acceleration;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0014CA68 File Offset: 0x0014AC68
		public float FallDamage
		{
			get
			{
				return this._data.fallDamage;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0014CA75 File Offset: 0x0014AC75
		public bool AutoJump
		{
			get
			{
				return this._data.constantJump;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0014CA82 File Offset: 0x0014AC82
		public bool BlockExtraJumps
		{
			get
			{
				return this._data.blockExtraJumps;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0014CA8F File Offset: 0x0014AC8F
		public bool IsConsideredASlimeMount
		{
			get
			{
				return this._type == 3 || this._type == 50;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0014CAA6 File Offset: 0x0014ACA6
		public bool Cart
		{
			get
			{
				return this._data != null && this._active && this._data.Minecart;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0014CAC5 File Offset: 0x0014ACC5
		public bool Directional
		{
			get
			{
				return this._data == null || this._data.MinecartDirectional;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0014CADC File Offset: 0x0014ACDC
		public Mount.MountDelegatesData Delegations
		{
			get
			{
				if (this._data == null)
				{
					return this._defaultDelegatesData;
				}
				return this._data.delegations;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0014CAF8 File Offset: 0x0014ACF8
		public Vector2 Origin
		{
			get
			{
				return new Vector2((float)this._data.textureWidth / 2f, (float)this._data.textureHeight / (2f * (float)this._data.totalFrames));
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0014CB30 File Offset: 0x0014AD30
		public bool AbilityCharging
		{
			get
			{
				return this._abilityCharging;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0014CB38 File Offset: 0x0014AD38
		public bool AbilityActive
		{
			get
			{
				return this._abilityActive;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0014CB40 File Offset: 0x0014AD40
		public float AbilityCharge
		{
			get
			{
				return (float)this._abilityCharge / (float)this._data.abilityChargeMax;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0014CB56 File Offset: 0x0014AD56
		public bool AllowDirectionChange
		{
			get
			{
				return this._type != 9 || this._abilityCooldown < this._data.abilityCooldown / 2;
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0014CB79 File Offset: 0x0014AD79
		private static void MeowcartLandingSound(Player Player, Vector2 Position, int Width, int Height)
		{
			SoundEngine.PlaySound(37, (int)Position.X + Width / 2, (int)Position.Y + Height / 2, 5, 1f, 0f);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0014CBA4 File Offset: 0x0014ADA4
		private static void MeowcartBumperSound(Player Player, Vector2 Position, int Width, int Height)
		{
			SoundEngine.PlaySound(37, (int)Position.X + Width / 2, (int)Position.Y + Height / 2, 3, 1f, 0f);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0014CBCF File Offset: 0x0014ADCF
		public Mount()
		{
			this._debugDraw = new List<DrillDebugDraw>();
			this.Reset();
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0014CBF4 File Offset: 0x0014ADF4
		public void Reset()
		{
			this._active = false;
			this._type = -1;
			this._flipDraw = false;
			this._frame = 0;
			this._frameCounter = 0f;
			this._frameExtra = 0;
			this._frameExtraCounter = 0f;
			this._frameState = 0;
			this._flyTime = 0;
			this._idleTime = 0;
			this._idleTimeNext = -1;
			this._fatigueMax = 0f;
			this._abilityCharging = false;
			this._abilityCharge = 0;
			this._aiming = false;
			this._shouldSuperCart = false;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0014CC80 File Offset: 0x0014AE80
		public static void Initialize()
		{
			Mount.mounts = new Mount.MountData[MountID.Count];
			Mount.MountData mountData = new Mount.MountData();
			Mount.mounts[0] = mountData;
			mountData.spawnDust = 57;
			mountData.spawnDustNoGravity = false;
			mountData.buff = 90;
			mountData.heightBoost = 20;
			mountData.flightTimeMax = 160;
			mountData.runSpeed = 5.5f;
			mountData.dashSpeed = 12f;
			mountData.acceleration = 0.09f;
			mountData.jumpHeight = 17;
			mountData.jumpSpeed = 5.31f;
			mountData.totalFrames = 12;
			int[] array = new int[mountData.totalFrames];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 30;
			}
			array[1] += 2;
			array[11] += 2;
			mountData.playerYOffsets = array;
			mountData.xOffset = 13;
			mountData.bodyFrame = 3;
			mountData.yOffset = -7;
			mountData.playerHeadOffset = 22;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 6;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 6;
			mountData.flyingFrameCount = 6;
			mountData.flyingFrameDelay = 6;
			mountData.flyingFrameStart = 6;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 1;
			mountData.idleFrameCount = 4;
			mountData.idleFrameDelay = 30;
			mountData.idleFrameStart = 2;
			mountData.idleFrameLoop = true;
			mountData.swimFrameCount = mountData.inAirFrameCount;
			mountData.swimFrameDelay = mountData.inAirFrameDelay;
			mountData.swimFrameStart = mountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.RudolphMount[0];
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = TextureAssets.RudolphMount[1];
				mountData.frontTextureExtra = TextureAssets.RudolphMount[2];
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[2] = mountData;
			mountData.spawnDust = 58;
			mountData.buff = 129;
			mountData.heightBoost = 20;
			mountData.flightTimeMax = 160;
			mountData.runSpeed = 5f;
			mountData.dashSpeed = 9f;
			mountData.acceleration = 0.08f;
			mountData.jumpHeight = 10;
			mountData.jumpSpeed = 6.01f;
			mountData.totalFrames = 16;
			array = new int[mountData.totalFrames];
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = 22;
			}
			array[12] += 2;
			array[13] += 4;
			array[14] += 2;
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 8;
			mountData.playerHeadOffset = 22;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 7;
			mountData.runningFrameCount = 5;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 11;
			mountData.flyingFrameCount = 6;
			mountData.flyingFrameDelay = 6;
			mountData.flyingFrameStart = 1;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 3;
			mountData.idleFrameDelay = 30;
			mountData.idleFrameStart = 8;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = mountData.inAirFrameCount;
			mountData.swimFrameDelay = mountData.inAirFrameDelay;
			mountData.swimFrameStart = mountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.PigronMount;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[1] = mountData;
			mountData.spawnDust = 15;
			mountData.buff = 128;
			mountData.heightBoost = 20;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 0.8f;
			mountData.runSpeed = 4f;
			mountData.dashSpeed = 7.8f;
			mountData.acceleration = 0.13f;
			mountData.jumpHeight = 15;
			mountData.jumpSpeed = 5.01f;
			mountData.totalFrames = 7;
			array = new int[mountData.totalFrames];
			for (int k = 0; k < array.Length; k++)
			{
				array[k] = 14;
			}
			array[2] += 2;
			array[3] += 4;
			array[4] += 8;
			array[5] += 8;
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 4;
			mountData.playerHeadOffset = 22;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 7;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 6;
			mountData.flyingFrameDelay = 6;
			mountData.flyingFrameStart = 1;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 5;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = mountData.inAirFrameCount;
			mountData.swimFrameDelay = mountData.inAirFrameDelay;
			mountData.swimFrameStart = mountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.BunnyMount;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[3] = mountData;
			mountData.spawnDust = 56;
			mountData.buff = 130;
			mountData.heightBoost = 20;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 0.5f;
			mountData.runSpeed = 4f;
			mountData.dashSpeed = 4f;
			mountData.acceleration = 0.18f;
			mountData.jumpHeight = 12;
			mountData.jumpSpeed = 8.25f;
			mountData.constantJump = true;
			mountData.totalFrames = 4;
			array = new int[mountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 20;
			}
			array[1] += 2;
			array[3] -= 2;
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 11;
			mountData.playerHeadOffset = 22;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 4;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 1;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.SlimeMount;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[6] = mountData;
			mountData.Minecart = true;
			mountData.MinecartDirectional = true;
			mountData.delegations = new Mount.MountDelegatesData();
			Mount.MountDelegatesData delegations = mountData.delegations;
			Action<Vector2> minecartDust;
			if ((minecartDust = Mount.<>O.<0>__Sparks) == null)
			{
				minecartDust = (Mount.<>O.<0>__Sparks = new Action<Vector2>(DelegateMethods.Minecart.Sparks));
			}
			delegations.MinecartDust = minecartDust;
			mountData.spawnDust = 213;
			mountData.buff = 118;
			mountData.extraBuff = 138;
			mountData.heightBoost = 10;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 1f;
			mountData.runSpeed = 13f;
			mountData.dashSpeed = 13f;
			mountData.acceleration = 0.04f;
			mountData.jumpHeight = 15;
			mountData.jumpSpeed = 5.15f;
			mountData.blockExtraJumps = true;
			mountData.totalFrames = 3;
			array = new int[mountData.totalFrames];
			for (int m = 0; m < array.Length; m++)
			{
				array[m] = 8;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 13;
			mountData.playerHeadOffset = 14;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 3;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 0;
			mountData.inAirFrameDelay = 0;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			if (Main.netMode != 2)
			{
				mountData.backTexture = Asset<Texture2D>.Empty;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = TextureAssets.MinecartMount;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.frontTexture.Width();
				mountData.textureHeight = mountData.frontTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[15] = mountData;
			Mount.SetAsMinecart(mountData, 209, 208, TextureAssets.DesertMinecartMount, 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[18] = mountData;
			Mount.SetAsMinecart(mountData, 221, 220, TextureAssets.Extra[108], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[19] = mountData;
			Mount.SetAsMinecart(mountData, 223, 222, TextureAssets.Extra[109], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[20] = mountData;
			Mount.SetAsMinecart(mountData, 225, 224, TextureAssets.Extra[110], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[21] = mountData;
			Mount.SetAsMinecart(mountData, 227, 226, TextureAssets.Extra[111], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[22] = mountData;
			Mount.SetAsMinecart(mountData, 229, 228, TextureAssets.Extra[112], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[24] = mountData;
			Mount.SetAsMinecart(mountData, 232, 231, TextureAssets.Extra[115], 0, 0);
			mountData.frontTextureGlow = TextureAssets.Extra[116];
			mountData = new Mount.MountData();
			Mount.mounts[25] = mountData;
			Mount.SetAsMinecart(mountData, 234, 233, TextureAssets.Extra[117], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[26] = mountData;
			Mount.SetAsMinecart(mountData, 236, 235, TextureAssets.Extra[118], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[27] = mountData;
			Mount.SetAsMinecart(mountData, 238, 237, TextureAssets.Extra[119], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[28] = mountData;
			Mount.SetAsMinecart(mountData, 240, 239, TextureAssets.Extra[120], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[29] = mountData;
			Mount.SetAsMinecart(mountData, 242, 241, TextureAssets.Extra[121], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[30] = mountData;
			Mount.SetAsMinecart(mountData, 244, 243, TextureAssets.Extra[122], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[31] = mountData;
			Mount.SetAsMinecart(mountData, 246, 245, TextureAssets.Extra[123], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[32] = mountData;
			Mount.SetAsMinecart(mountData, 248, 247, TextureAssets.Extra[124], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[33] = mountData;
			Mount.SetAsMinecart(mountData, 250, 249, TextureAssets.Extra[125], 0, 0);
			Mount.MountDelegatesData delegations2 = mountData.delegations;
			Action<Vector2> minecartDust2;
			if ((minecartDust2 = Mount.<>O.<1>__SparksMeow) == null)
			{
				minecartDust2 = (Mount.<>O.<1>__SparksMeow = new Action<Vector2>(DelegateMethods.Minecart.SparksMeow));
			}
			delegations2.MinecartDust = minecartDust2;
			Mount.MountDelegatesData delegations3 = mountData.delegations;
			Action<Player, Vector2, int, int> minecartLandingSound;
			if ((minecartLandingSound = Mount.<>O.<2>__MeowcartLandingSound) == null)
			{
				minecartLandingSound = (Mount.<>O.<2>__MeowcartLandingSound = new Action<Player, Vector2, int, int>(Mount.MeowcartLandingSound));
			}
			delegations3.MinecartLandingSound = minecartLandingSound;
			Mount.MountDelegatesData delegations4 = mountData.delegations;
			Action<Player, Vector2, int, int> minecartBumperSound;
			if ((minecartBumperSound = Mount.<>O.<3>__MeowcartBumperSound) == null)
			{
				minecartBumperSound = (Mount.<>O.<3>__MeowcartBumperSound = new Action<Player, Vector2, int, int>(Mount.MeowcartBumperSound));
			}
			delegations4.MinecartBumperSound = minecartBumperSound;
			mountData = new Mount.MountData();
			Mount.mounts[34] = mountData;
			Mount.SetAsMinecart(mountData, 252, 251, TextureAssets.Extra[126], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[35] = mountData;
			Mount.SetAsMinecart(mountData, 254, 253, TextureAssets.Extra[127], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[36] = mountData;
			Mount.SetAsMinecart(mountData, 256, 255, TextureAssets.Extra[128], 0, 0);
			mountData = new Mount.MountData();
			Mount.mounts[38] = mountData;
			Mount.SetAsMinecart(mountData, 270, 269, TextureAssets.Extra[150], 0, 0);
			if (Main.netMode != 2)
			{
				mountData.backTexture = mountData.frontTexture;
			}
			mountData = new Mount.MountData();
			Mount.mounts[39] = mountData;
			Mount.SetAsMinecart(mountData, 273, 272, TextureAssets.Extra[155], 0, 0);
			mountData.yOffset -= 2;
			if (Main.netMode != 2)
			{
				mountData.frontTextureExtra = TextureAssets.Extra[165];
			}
			mountData.runSpeed = 6f;
			mountData.dashSpeed = 6f;
			mountData.acceleration = 0.02f;
			mountData = new Mount.MountData();
			Mount.mounts[16] = mountData;
			mountData.Minecart = true;
			mountData.delegations = new Mount.MountDelegatesData();
			Mount.MountDelegatesData delegations5 = mountData.delegations;
			Action<Vector2> minecartDust3;
			if ((minecartDust3 = Mount.<>O.<0>__Sparks) == null)
			{
				minecartDust3 = (Mount.<>O.<0>__Sparks = new Action<Vector2>(DelegateMethods.Minecart.Sparks));
			}
			delegations5.MinecartDust = minecartDust3;
			mountData.spawnDust = 213;
			mountData.buff = 211;
			mountData.extraBuff = 210;
			mountData.heightBoost = 10;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 1f;
			mountData.runSpeed = 13f;
			mountData.dashSpeed = 13f;
			mountData.acceleration = 0.04f;
			mountData.jumpHeight = 15;
			mountData.jumpSpeed = 5.15f;
			mountData.blockExtraJumps = true;
			mountData.totalFrames = 3;
			array = new int[mountData.totalFrames];
			for (int n = 0; n < array.Length; n++)
			{
				array[n] = 8;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 13;
			mountData.playerHeadOffset = 14;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 3;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 0;
			mountData.inAirFrameDelay = 0;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			if (Main.netMode != 2)
			{
				mountData.backTexture = Asset<Texture2D>.Empty;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = TextureAssets.FishMinecartMount;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.frontTexture.Width();
				mountData.textureHeight = mountData.frontTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[51] = mountData;
			Mount.SetAsMinecart(mountData, 339, 338, TextureAssets.Extra[246], -10, -8);
			mountData.spawnDust = 211;
			Mount.MountDelegatesData delegations6 = mountData.delegations;
			Action<Vector2> minecartDust4;
			if ((minecartDust4 = Mount.<>O.<4>__SparksFart) == null)
			{
				minecartDust4 = (Mount.<>O.<4>__SparksFart = new Action<Vector2>(DelegateMethods.Minecart.SparksFart));
			}
			delegations6.MinecartDust = minecartDust4;
			Mount.MountDelegatesData delegations7 = mountData.delegations;
			Action<Player, Vector2, int, int> minecartBumperSound2;
			if ((minecartBumperSound2 = Mount.<>O.<5>__BumperSoundFart) == null)
			{
				minecartBumperSound2 = (Mount.<>O.<5>__BumperSoundFart = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.BumperSoundFart));
			}
			delegations7.MinecartBumperSound = minecartBumperSound2;
			Mount.MountDelegatesData delegations8 = mountData.delegations;
			Action<Player, Vector2, int, int> minecartLandingSound2;
			if ((minecartLandingSound2 = Mount.<>O.<6>__LandingSoundFart) == null)
			{
				minecartLandingSound2 = (Mount.<>O.<6>__LandingSoundFart = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.LandingSoundFart));
			}
			delegations8.MinecartLandingSound = minecartLandingSound2;
			Mount.MountDelegatesData delegations9 = mountData.delegations;
			Action<Player, Vector2, int, int> minecartJumpingSound;
			if ((minecartJumpingSound = Mount.<>O.<7>__JumpingSoundFart) == null)
			{
				minecartJumpingSound = (Mount.<>O.<7>__JumpingSoundFart = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.JumpingSoundFart));
			}
			delegations9.MinecartJumpingSound = minecartJumpingSound;
			mountData = new Mount.MountData();
			Mount.mounts[53] = mountData;
			Mount.SetAsMinecart(mountData, 347, 346, TextureAssets.Extra[251], -10, -8);
			mountData.spawnDust = 211;
			Mount.MountDelegatesData delegations10 = mountData.delegations;
			Action<Vector2> minecartDust5;
			if ((minecartDust5 = Mount.<>O.<8>__SparksTerraFart) == null)
			{
				minecartDust5 = (Mount.<>O.<8>__SparksTerraFart = new Action<Vector2>(DelegateMethods.Minecart.SparksTerraFart));
			}
			delegations10.MinecartDust = minecartDust5;
			Mount.MountDelegatesData delegations11 = mountData.delegations;
			Action<Player, Vector2, int, int> minecartBumperSound3;
			if ((minecartBumperSound3 = Mount.<>O.<5>__BumperSoundFart) == null)
			{
				minecartBumperSound3 = (Mount.<>O.<5>__BumperSoundFart = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.BumperSoundFart));
			}
			delegations11.MinecartBumperSound = minecartBumperSound3;
			Mount.MountDelegatesData delegations12 = mountData.delegations;
			Action<Player, Vector2, int, int> minecartLandingSound3;
			if ((minecartLandingSound3 = Mount.<>O.<6>__LandingSoundFart) == null)
			{
				minecartLandingSound3 = (Mount.<>O.<6>__LandingSoundFart = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.LandingSoundFart));
			}
			delegations12.MinecartLandingSound = minecartLandingSound3;
			Mount.MountDelegatesData delegations13 = mountData.delegations;
			Action<Player, Vector2, int, int> minecartJumpingSound2;
			if ((minecartJumpingSound2 = Mount.<>O.<7>__JumpingSoundFart) == null)
			{
				minecartJumpingSound2 = (Mount.<>O.<7>__JumpingSoundFart = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.JumpingSoundFart));
			}
			delegations13.MinecartJumpingSound = minecartJumpingSound2;
			mountData = new Mount.MountData();
			Mount.mounts[4] = mountData;
			mountData.spawnDust = 56;
			mountData.buff = 131;
			mountData.heightBoost = 26;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 1f;
			mountData.runSpeed = 2f;
			mountData.dashSpeed = 5f;
			mountData.swimSpeed = 10f;
			mountData.acceleration = 0.08f;
			mountData.jumpHeight = 12;
			mountData.jumpSpeed = 3.7f;
			mountData.totalFrames = 12;
			array = new int[mountData.totalFrames];
			for (int num = 0; num < array.Length; num++)
			{
				array[num] = 26;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 13;
			mountData.playerHeadOffset = 28;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 6;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 3;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = 6;
			mountData.swimFrameDelay = 12;
			mountData.swimFrameStart = 6;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.TurtleMount;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[5] = mountData;
			mountData.spawnDust = 152;
			mountData.buff = 132;
			mountData.heightBoost = 16;
			mountData.flightTimeMax = 320;
			mountData.fatigueMax = 320;
			mountData.fallDamage = 0f;
			mountData.usesHover = true;
			mountData.runSpeed = 2f;
			mountData.dashSpeed = 2f;
			mountData.acceleration = 0.16f;
			mountData.jumpHeight = 10;
			mountData.jumpSpeed = 4f;
			mountData.blockExtraJumps = true;
			mountData.totalFrames = 12;
			array = new int[mountData.totalFrames];
			for (int num2 = 0; num2 < array.Length; num2++)
			{
				array[num2] = 16;
			}
			array[8] = 18;
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 4;
			mountData.playerHeadOffset = 18;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 5;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 3;
			mountData.flyingFrameDelay = 12;
			mountData.flyingFrameStart = 5;
			mountData.inAirFrameCount = 3;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 5;
			mountData.idleFrameCount = 4;
			mountData.idleFrameDelay = 12;
			mountData.idleFrameStart = 8;
			mountData.idleFrameLoop = true;
			mountData.swimFrameCount = 0;
			mountData.swimFrameDelay = 12;
			mountData.swimFrameStart = 0;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.BeeMount[0];
				mountData.backTextureExtra = TextureAssets.BeeMount[1];
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[7] = mountData;
			mountData.spawnDust = 226;
			mountData.spawnDustNoGravity = true;
			mountData.buff = 141;
			mountData.heightBoost = 16;
			mountData.flightTimeMax = 320;
			mountData.fatigueMax = 320;
			mountData.fallDamage = 0f;
			mountData.usesHover = true;
			mountData.runSpeed = 8f;
			mountData.dashSpeed = 8f;
			mountData.acceleration = 0.16f;
			mountData.jumpHeight = 10;
			mountData.jumpSpeed = 4f;
			mountData.blockExtraJumps = true;
			mountData.totalFrames = 8;
			array = new int[mountData.totalFrames];
			for (int num3 = 0; num3 < array.Length; num3++)
			{
				array[num3] = 16;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 4;
			mountData.playerHeadOffset = 18;
			mountData.standingFrameCount = 8;
			mountData.standingFrameDelay = 4;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 8;
			mountData.runningFrameDelay = 4;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 8;
			mountData.flyingFrameDelay = 4;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 8;
			mountData.inAirFrameDelay = 4;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 12;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = true;
			mountData.swimFrameCount = 0;
			mountData.swimFrameDelay = 12;
			mountData.swimFrameStart = 0;
			if (Main.netMode != 2)
			{
				mountData.backTexture = Asset<Texture2D>.Empty;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = TextureAssets.UfoMount[0];
				mountData.frontTextureExtra = TextureAssets.UfoMount[1];
				mountData.textureWidth = mountData.frontTexture.Width();
				mountData.textureHeight = mountData.frontTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[8] = mountData;
			mountData.spawnDust = 226;
			mountData.buff = 142;
			mountData.heightBoost = 16;
			mountData.flightTimeMax = 320;
			mountData.fatigueMax = 320;
			mountData.fallDamage = 1f;
			mountData.usesHover = true;
			mountData.swimSpeed = 4f;
			mountData.runSpeed = 6f;
			mountData.dashSpeed = 4f;
			mountData.acceleration = 0.16f;
			mountData.jumpHeight = 10;
			mountData.jumpSpeed = 4f;
			mountData.blockExtraJumps = true;
			mountData.emitsLight = true;
			mountData.lightColor = new Vector3(0.3f, 0.3f, 0.4f);
			mountData.totalFrames = 1;
			array = new int[mountData.totalFrames];
			for (int num4 = 0; num4 < array.Length; num4++)
			{
				array[num4] = 4;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 4;
			mountData.playerHeadOffset = 18;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 1;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 1;
			mountData.flyingFrameDelay = 12;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 12;
			mountData.idleFrameStart = 8;
			mountData.swimFrameCount = 0;
			mountData.swimFrameDelay = 12;
			mountData.swimFrameStart = 0;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.DrillMount[0];
				mountData.backTextureGlow = TextureAssets.DrillMount[3];
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.backTextureExtraGlow = Asset<Texture2D>.Empty;
				mountData.frontTexture = TextureAssets.DrillMount[1];
				mountData.frontTextureGlow = TextureAssets.DrillMount[4];
				mountData.frontTextureExtra = TextureAssets.DrillMount[2];
				mountData.frontTextureExtraGlow = TextureAssets.DrillMount[5];
				mountData.textureWidth = mountData.frontTexture.Width();
				mountData.textureHeight = mountData.frontTexture.Height();
			}
			Mount.drillTextureSize = new Vector2(80f, 80f);
			if (!Main.dedServ)
			{
				Vector2 vector;
				vector..ctor((float)mountData.textureWidth, (float)(mountData.textureHeight / mountData.totalFrames));
				if (Mount.drillTextureSize != vector)
				{
					throw new Exception(string.Concat(new string[]
					{
						"Be sure to update the Drill texture origin to match the actual texture size of ",
						mountData.textureWidth.ToString(),
						", ",
						mountData.textureHeight.ToString(),
						"."
					}));
				}
			}
			mountData = new Mount.MountData();
			Mount.mounts[9] = mountData;
			mountData.spawnDust = 15;
			mountData.buff = 143;
			mountData.heightBoost = 16;
			mountData.flightTimeMax = 0;
			mountData.fatigueMax = 0;
			mountData.fallDamage = 0f;
			mountData.abilityChargeMax = 40;
			mountData.abilityCooldown = 20;
			mountData.abilityDuration = 0;
			mountData.runSpeed = 8f;
			mountData.dashSpeed = 8f;
			mountData.acceleration = 0.4f;
			mountData.jumpHeight = 22;
			mountData.jumpSpeed = 10.01f;
			mountData.blockExtraJumps = false;
			mountData.totalFrames = 12;
			array = new int[mountData.totalFrames];
			for (int num5 = 0; num5 < array.Length; num5++)
			{
				array[num5] = 16;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 6;
			mountData.playerHeadOffset = 18;
			mountData.standingFrameCount = 6;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 6;
			mountData.runningFrameCount = 6;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 12;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 1;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 12;
			mountData.idleFrameStart = 6;
			mountData.idleFrameLoop = true;
			mountData.swimFrameCount = 0;
			mountData.swimFrameDelay = 12;
			mountData.swimFrameStart = 0;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.ScutlixMount[0];
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = TextureAssets.ScutlixMount[1];
				mountData.frontTextureExtra = TextureAssets.ScutlixMount[2];
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			Mount.scutlixEyePositions = new Vector2[10];
			Mount.scutlixEyePositions[0] = new Vector2(60f, 2f);
			Mount.scutlixEyePositions[1] = new Vector2(70f, 6f);
			Mount.scutlixEyePositions[2] = new Vector2(68f, 6f);
			Mount.scutlixEyePositions[3] = new Vector2(76f, 12f);
			Mount.scutlixEyePositions[4] = new Vector2(80f, 10f);
			Mount.scutlixEyePositions[5] = new Vector2(84f, 18f);
			Mount.scutlixEyePositions[6] = new Vector2(74f, 20f);
			Mount.scutlixEyePositions[7] = new Vector2(76f, 24f);
			Mount.scutlixEyePositions[8] = new Vector2(70f, 34f);
			Mount.scutlixEyePositions[9] = new Vector2(76f, 34f);
			Mount.scutlixTextureSize = new Vector2(45f, 54f);
			if (!Main.dedServ)
			{
				Vector2 vector2;
				vector2..ctor((float)(mountData.textureWidth / 2), (float)(mountData.textureHeight / mountData.totalFrames));
				if (Mount.scutlixTextureSize != vector2)
				{
					throw new Exception(string.Concat(new string[]
					{
						"Be sure to update the Scutlix texture origin to match the actual texture size of ",
						mountData.textureWidth.ToString(),
						", ",
						mountData.textureHeight.ToString(),
						"."
					}));
				}
			}
			for (int num6 = 0; num6 < Mount.scutlixEyePositions.Length; num6++)
			{
				Mount.scutlixEyePositions[num6] -= Mount.scutlixTextureSize;
			}
			mountData = new Mount.MountData();
			Mount.mounts[10] = mountData;
			mountData.spawnDust = 15;
			mountData.buff = 162;
			mountData.heightBoost = 34;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 0.2f;
			mountData.runSpeed = 4f;
			mountData.dashSpeed = 12f;
			mountData.acceleration = 0.3f;
			mountData.jumpHeight = 10;
			mountData.jumpSpeed = 8.01f;
			mountData.totalFrames = 16;
			array = new int[mountData.totalFrames];
			for (int num7 = 0; num7 < array.Length; num7++)
			{
				array[num7] = 28;
			}
			array[3] += 2;
			array[4] += 2;
			array[7] += 2;
			array[8] += 2;
			array[12] += 2;
			array[13] += 2;
			array[15] += 4;
			mountData.playerYOffsets = array;
			mountData.xOffset = 5;
			mountData.bodyFrame = 3;
			mountData.yOffset = 1;
			mountData.playerHeadOffset = 34;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 7;
			mountData.runningFrameDelay = 15;
			mountData.runningFrameStart = 1;
			mountData.dashingFrameCount = 6;
			mountData.dashingFrameDelay = 40;
			mountData.dashingFrameStart = 9;
			mountData.flyingFrameCount = 6;
			mountData.flyingFrameDelay = 6;
			mountData.flyingFrameStart = 1;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 15;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = mountData.inAirFrameCount;
			mountData.swimFrameDelay = mountData.inAirFrameDelay;
			mountData.swimFrameStart = mountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.UnicornMount;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[11] = mountData;
			mountData.Minecart = true;
			mountData.delegations = new Mount.MountDelegatesData();
			Mount.MountDelegatesData delegations14 = mountData.delegations;
			Action<Vector2> minecartDust6;
			if ((minecartDust6 = Mount.<>O.<9>__SparksMech) == null)
			{
				minecartDust6 = (Mount.<>O.<9>__SparksMech = new Action<Vector2>(DelegateMethods.Minecart.SparksMech));
			}
			delegations14.MinecartDust = minecartDust6;
			mountData.spawnDust = 213;
			mountData.buff = 167;
			mountData.extraBuff = 166;
			mountData.heightBoost = 12;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 1f;
			mountData.runSpeed = 13f;
			mountData.dashSpeed = 13f;
			mountData.acceleration = 0.04f;
			mountData.jumpHeight = 15;
			mountData.jumpSpeed = 5.15f;
			mountData.blockExtraJumps = true;
			mountData.totalFrames = 3;
			array = new int[mountData.totalFrames];
			for (int num8 = 0; num8 < array.Length; num8++)
			{
				array[num8] = 9;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = -1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 11;
			mountData.playerHeadOffset = 14;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 3;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 0;
			mountData.inAirFrameDelay = 0;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			if (Main.netMode != 2)
			{
				mountData.backTexture = Asset<Texture2D>.Empty;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = TextureAssets.MinecartMechMount[0];
				mountData.frontTextureGlow = TextureAssets.MinecartMechMount[1];
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.frontTexture.Width();
				mountData.textureHeight = mountData.frontTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[12] = mountData;
			mountData.spawnDust = 15;
			mountData.buff = 168;
			mountData.heightBoost = 14;
			mountData.flightTimeMax = 320;
			mountData.fatigueMax = 320;
			mountData.fallDamage = 0f;
			mountData.usesHover = true;
			mountData.runSpeed = 2f;
			mountData.dashSpeed = 1f;
			mountData.acceleration = 0.2f;
			mountData.jumpHeight = 4;
			mountData.jumpSpeed = 3f;
			mountData.swimSpeed = 16f;
			mountData.blockExtraJumps = true;
			mountData.totalFrames = 23;
			array = new int[mountData.totalFrames];
			for (int num9 = 0; num9 < array.Length; num9++)
			{
				array[num9] = 12;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 2;
			mountData.bodyFrame = 3;
			mountData.yOffset = 16;
			mountData.playerHeadOffset = 16;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 8;
			mountData.runningFrameCount = 7;
			mountData.runningFrameDelay = 14;
			mountData.runningFrameStart = 8;
			mountData.flyingFrameCount = 8;
			mountData.flyingFrameDelay = 16;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 8;
			mountData.inAirFrameDelay = 6;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = 8;
			mountData.swimFrameDelay = 4;
			mountData.swimFrameStart = 15;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.CuteFishronMount[0];
				mountData.backTextureGlow = TextureAssets.CuteFishronMount[1];
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[13] = mountData;
			mountData.Minecart = true;
			mountData.MinecartDirectional = true;
			mountData.delegations = new Mount.MountDelegatesData();
			Mount.MountDelegatesData delegations15 = mountData.delegations;
			Action<Vector2> minecartDust7;
			if ((minecartDust7 = Mount.<>O.<0>__Sparks) == null)
			{
				minecartDust7 = (Mount.<>O.<0>__Sparks = new Action<Vector2>(DelegateMethods.Minecart.Sparks));
			}
			delegations15.MinecartDust = minecartDust7;
			mountData.spawnDust = 213;
			mountData.buff = 184;
			mountData.extraBuff = 185;
			mountData.heightBoost = 10;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 1f;
			mountData.runSpeed = 10f;
			mountData.dashSpeed = 10f;
			mountData.acceleration = 0.03f;
			mountData.jumpHeight = 12;
			mountData.jumpSpeed = 5.15f;
			mountData.blockExtraJumps = true;
			mountData.totalFrames = 3;
			array = new int[mountData.totalFrames];
			for (int num10 = 0; num10 < array.Length; num10++)
			{
				array[num10] = 8;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 13;
			mountData.playerHeadOffset = 14;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 3;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 0;
			mountData.inAirFrameDelay = 0;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			if (Main.netMode != 2)
			{
				mountData.backTexture = Asset<Texture2D>.Empty;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = TextureAssets.MinecartWoodMount;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.frontTexture.Width();
				mountData.textureHeight = mountData.frontTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[14] = mountData;
			mountData.spawnDust = 15;
			mountData.buff = 193;
			mountData.heightBoost = 8;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 0.2f;
			mountData.runSpeed = 8f;
			mountData.acceleration = 0.25f;
			mountData.jumpHeight = 20;
			mountData.jumpSpeed = 8.01f;
			mountData.totalFrames = 8;
			array = new int[mountData.totalFrames];
			for (int num11 = 0; num11 < array.Length; num11++)
			{
				array[num11] = 8;
			}
			array[1] += 2;
			array[3] += 2;
			array[6] += 2;
			mountData.playerYOffsets = array;
			mountData.xOffset = 4;
			mountData.bodyFrame = 3;
			mountData.yOffset = 9;
			mountData.playerHeadOffset = 10;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 6;
			mountData.runningFrameDelay = 30;
			mountData.runningFrameStart = 2;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 1;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = mountData.inAirFrameCount;
			mountData.swimFrameDelay = mountData.inAirFrameDelay;
			mountData.swimFrameStart = mountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.BasiliskMount;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[17] = mountData;
			mountData.spawnDust = 15;
			mountData.buff = 212;
			mountData.heightBoost = 16;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 0.2f;
			mountData.runSpeed = 8f;
			mountData.acceleration = 0.25f;
			mountData.jumpHeight = 20;
			mountData.jumpSpeed = 8.01f;
			mountData.totalFrames = 4;
			array = new int[mountData.totalFrames];
			for (int num12 = 0; num12 < array.Length; num12++)
			{
				array[num12] = 8;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 2;
			mountData.bodyFrame = 3;
			mountData.yOffset = 17 - mountData.heightBoost;
			mountData.playerHeadOffset = 18;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 4;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 1;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = mountData.inAirFrameCount;
			mountData.swimFrameDelay = mountData.inAirFrameDelay;
			mountData.swimFrameStart = mountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.Extra[97];
				mountData.backTextureExtra = TextureAssets.Extra[96];
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTextureExtra.Width();
				mountData.textureHeight = mountData.backTextureExtra.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[23] = mountData;
			mountData.spawnDust = 43;
			mountData.spawnDustNoGravity = true;
			mountData.buff = 230;
			mountData.heightBoost = 0;
			mountData.flightTimeMax = 320;
			mountData.fatigueMax = 320;
			mountData.fallDamage = 0f;
			mountData.usesHover = true;
			mountData.runSpeed = 9f;
			mountData.dashSpeed = 9f;
			mountData.acceleration = 0.16f;
			mountData.jumpHeight = 10;
			mountData.jumpSpeed = 4f;
			mountData.blockExtraJumps = true;
			mountData.totalFrames = 6;
			array = new int[mountData.totalFrames];
			for (int num13 = 0; num13 < array.Length; num13++)
			{
				array[num13] = 6;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = -2;
			mountData.bodyFrame = 0;
			mountData.yOffset = 8;
			mountData.playerHeadOffset = 0;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 0;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 1;
			mountData.runningFrameDelay = 0;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 1;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 6;
			mountData.inAirFrameDelay = 8;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = true;
			mountData.swimFrameCount = 0;
			mountData.swimFrameDelay = 0;
			mountData.swimFrameStart = 0;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.Extra[113];
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[37] = mountData;
			mountData.spawnDust = 282;
			mountData.buff = 265;
			mountData.heightBoost = 12;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 0.2f;
			mountData.runSpeed = 6f;
			mountData.acceleration = 0.15f;
			mountData.jumpHeight = 14;
			mountData.jumpSpeed = 6.01f;
			mountData.totalFrames = 10;
			array = new int[mountData.totalFrames];
			for (int num14 = 0; num14 < array.Length; num14++)
			{
				array[num14] = 20;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 5;
			mountData.bodyFrame = 4;
			mountData.yOffset = 1;
			mountData.playerHeadOffset = 20;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 7;
			mountData.runningFrameDelay = 20;
			mountData.runningFrameStart = 2;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 1;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = mountData.runningFrameCount;
			mountData.swimFrameDelay = 10;
			mountData.swimFrameStart = mountData.runningFrameStart;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.Extra[149];
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[40] = mountData;
			Mount.SetAsHorse(mountData, 275, TextureAssets.Extra[161]);
			mountData = new Mount.MountData();
			Mount.mounts[41] = mountData;
			Mount.SetAsHorse(mountData, 276, TextureAssets.Extra[162]);
			mountData = new Mount.MountData();
			Mount.mounts[42] = mountData;
			Mount.SetAsHorse(mountData, 277, TextureAssets.Extra[163]);
			mountData = new Mount.MountData();
			Mount.mounts[43] = mountData;
			mountData.spawnDust = 15;
			mountData.buff = 278;
			mountData.heightBoost = 12;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 0.4f;
			mountData.runSpeed = 5f;
			mountData.acceleration = 0.1f;
			mountData.jumpHeight = 8;
			mountData.jumpSpeed = 8f;
			mountData.constantJump = true;
			mountData.totalFrames = 4;
			array = new int[mountData.totalFrames];
			for (int num15 = 0; num15 < array.Length; num15++)
			{
				array[num15] = 14;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 5;
			mountData.bodyFrame = 4;
			mountData.yOffset = 10;
			mountData.playerHeadOffset = 10;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 5;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 4;
			mountData.runningFrameDelay = 5;
			mountData.runningFrameStart = 0;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 5;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = 1;
			mountData.swimFrameDelay = 5;
			mountData.swimFrameStart = 0;
			if (Main.netMode != 2)
			{
				mountData.backTexture = Asset<Texture2D>.Empty;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = TextureAssets.Extra[164];
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.frontTexture.Width();
				mountData.textureHeight = mountData.frontTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[44] = mountData;
			mountData.spawnDust = 228;
			mountData.buff = 279;
			mountData.heightBoost = 24;
			mountData.flightTimeMax = 320;
			mountData.fatigueMax = 320;
			mountData.fallDamage = 0f;
			mountData.usesHover = true;
			mountData.runSpeed = 3f;
			mountData.dashSpeed = 6f;
			mountData.acceleration = 0.12f;
			mountData.jumpHeight = 3;
			mountData.jumpSpeed = 1f;
			mountData.swimSpeed = mountData.runSpeed;
			mountData.blockExtraJumps = true;
			mountData.totalFrames = 10;
			array = new int[mountData.totalFrames];
			for (int num16 = 0; num16 < array.Length; num16++)
			{
				array[num16] = 9;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 0;
			mountData.bodyFrame = 3;
			mountData.yOffset = 8;
			mountData.playerHeadOffset = 16;
			mountData.runningFrameCount = 10;
			mountData.runningFrameDelay = 8;
			mountData.runningFrameStart = 0;
			if (Main.netMode != 2)
			{
				mountData.backTexture = Asset<Texture2D>.Empty;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = TextureAssets.Extra[166];
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.frontTexture.Width();
				mountData.textureHeight = mountData.frontTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[45] = mountData;
			mountData.spawnDust = 6;
			mountData.buff = 280;
			mountData.heightBoost = 25;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 0.1f;
			mountData.runSpeed = 12f;
			mountData.dashSpeed = 16f;
			mountData.acceleration = 0.5f;
			mountData.jumpHeight = 14;
			mountData.jumpSpeed = 7f;
			mountData.emitsLight = true;
			mountData.lightColor = new Vector3(0.6f, 0.4f, 0.35f);
			mountData.totalFrames = 8;
			array = new int[mountData.totalFrames];
			for (int num17 = 0; num17 < array.Length; num17++)
			{
				array[num17] = 30;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 0;
			mountData.bodyFrame = 0;
			mountData.xOffset = 2;
			mountData.yOffset = 1;
			mountData.playerHeadOffset = 20;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 20;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 6;
			mountData.runningFrameDelay = 20;
			mountData.runningFrameStart = 2;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 20;
			mountData.inAirFrameStart = 1;
			mountData.swimFrameCount = mountData.runningFrameCount;
			mountData.swimFrameDelay = 20;
			mountData.swimFrameStart = mountData.runningFrameStart;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.Extra[167];
				mountData.backTextureGlow = TextureAssets.GlowMask[283];
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[46] = mountData;
			mountData.spawnDust = 15;
			mountData.buff = 281;
			mountData.heightBoost = 0;
			mountData.flightTimeMax = 0;
			mountData.fatigueMax = 0;
			mountData.fallDamage = 0f;
			mountData.abilityChargeMax = 40;
			mountData.abilityCooldown = 40;
			mountData.abilityDuration = 0;
			mountData.runSpeed = 8f;
			mountData.dashSpeed = 8f;
			mountData.acceleration = 0.4f;
			mountData.jumpHeight = 8;
			mountData.jumpSpeed = 9.01f;
			mountData.blockExtraJumps = false;
			mountData.totalFrames = 27;
			array = new int[mountData.totalFrames];
			for (int num18 = 0; num18 < array.Length; num18++)
			{
				array[num18] = 4;
				if (num18 == 1 || num18 == 2 || num18 == 7 || num18 == 8)
				{
					array[num18] += 2;
				}
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = 1;
			mountData.playerHeadOffset = 2;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 11;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.inAirFrameCount = 11;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 1;
			mountData.swimFrameCount = mountData.runningFrameCount;
			mountData.swimFrameDelay = mountData.runningFrameDelay;
			mountData.swimFrameStart = mountData.runningFrameStart;
			Mount.santankTextureSize = new Vector2(23f, 2f);
			if (Main.netMode != 2)
			{
				mountData.backTexture = Asset<Texture2D>.Empty;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = TextureAssets.Extra[168];
				mountData.frontTextureExtra = TextureAssets.Extra[168];
				mountData.textureWidth = mountData.frontTexture.Width();
				mountData.textureHeight = mountData.frontTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[47] = mountData;
			mountData.spawnDust = 5;
			mountData.buff = 282;
			mountData.heightBoost = 34;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 0.2f;
			mountData.runSpeed = 4f;
			mountData.dashSpeed = 12f;
			mountData.acceleration = 0.3f;
			mountData.jumpHeight = 10;
			mountData.jumpSpeed = 8.01f;
			mountData.totalFrames = 16;
			array = new int[mountData.totalFrames];
			for (int num19 = 0; num19 < array.Length; num19++)
			{
				array[num19] = 30;
			}
			array[3] += 2;
			array[4] += 2;
			array[7] += 2;
			array[8] += 2;
			array[12] += 2;
			array[13] += 2;
			array[15] += 4;
			mountData.playerYOffsets = array;
			mountData.xOffset = 5;
			mountData.bodyFrame = 3;
			mountData.yOffset = -1;
			mountData.playerHeadOffset = 34;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 7;
			mountData.runningFrameDelay = 15;
			mountData.runningFrameStart = 1;
			mountData.dashingFrameCount = 6;
			mountData.dashingFrameDelay = 40;
			mountData.dashingFrameStart = 9;
			mountData.flyingFrameCount = 6;
			mountData.flyingFrameDelay = 6;
			mountData.flyingFrameStart = 1;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 15;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = mountData.inAirFrameCount;
			mountData.swimFrameDelay = mountData.inAirFrameDelay;
			mountData.swimFrameStart = mountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.Extra[169];
				mountData.backTextureGlow = TextureAssets.GlowMask[284];
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[48] = mountData;
			mountData.spawnDust = 62;
			mountData.buff = 283;
			mountData.heightBoost = 14;
			mountData.flightTimeMax = 320;
			mountData.fallDamage = 0f;
			mountData.usesHover = true;
			mountData.runSpeed = 8f;
			mountData.dashSpeed = 8f;
			mountData.acceleration = 0.2f;
			mountData.jumpHeight = 5;
			mountData.jumpSpeed = 6f;
			mountData.swimSpeed = mountData.runSpeed;
			mountData.totalFrames = 6;
			array = new int[mountData.totalFrames];
			for (int num20 = 0; num20 < array.Length; num20++)
			{
				array[num20] = 9;
			}
			array[0] += 6;
			array[1] += 6;
			array[2] += 4;
			array[3] += 4;
			array[4] += 4;
			array[5] += 6;
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 0;
			mountData.yOffset = 16;
			mountData.playerHeadOffset = 16;
			mountData.runningFrameCount = 6;
			mountData.runningFrameDelay = 8;
			mountData.runningFrameStart = 0;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.Extra[170];
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[49] = mountData;
			mountData.spawnDust = 35;
			mountData.buff = 305;
			mountData.heightBoost = 8;
			mountData.runSpeed = 2f;
			mountData.dashSpeed = 1f;
			mountData.acceleration = 0.4f;
			mountData.jumpHeight = 4;
			mountData.jumpSpeed = 3f;
			mountData.swimSpeed = 14f;
			mountData.blockExtraJumps = true;
			mountData.flightTimeMax = 0;
			mountData.fatigueMax = 320;
			mountData.usesHover = true;
			mountData.emitsLight = true;
			mountData.lightColor = new Vector3(0.3f, 0.15f, 0.1f);
			mountData.totalFrames = 8;
			array = new int[mountData.totalFrames];
			for (int num21 = 0; num21 < array.Length; num21++)
			{
				array[num21] = 10;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 2;
			mountData.bodyFrame = 3;
			mountData.yOffset = 1;
			mountData.playerHeadOffset = 16;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 4;
			mountData.runningFrameCount = 4;
			mountData.runningFrameDelay = 14;
			mountData.runningFrameStart = 4;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 6;
			mountData.inAirFrameStart = 4;
			mountData.swimFrameCount = 4;
			mountData.swimFrameDelay = 16;
			mountData.swimFrameStart = 0;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.Extra[172];
				mountData.backTextureGlow = TextureAssets.GlowMask[285];
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[50] = mountData;
			mountData.spawnDust = 243;
			mountData.buff = 318;
			mountData.heightBoost = 20;
			mountData.flightTimeMax = 160;
			mountData.fallDamage = 0.5f;
			mountData.runSpeed = 5.5f;
			mountData.dashSpeed = 5.5f;
			mountData.acceleration = 0.2f;
			mountData.jumpHeight = 10;
			mountData.jumpSpeed = 7.25f;
			mountData.constantJump = true;
			mountData.totalFrames = 8;
			array = new int[mountData.totalFrames];
			for (int num22 = 0; num22 < array.Length; num22++)
			{
				array[num22] = 20;
			}
			array[1] += 2;
			array[4] += 2;
			array[5] += 2;
			mountData.playerYOffsets = array;
			mountData.xOffset = 1;
			mountData.bodyFrame = 3;
			mountData.yOffset = -1;
			mountData.playerHeadOffset = 22;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 5;
			mountData.runningFrameDelay = 16;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 5;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			if (Main.netMode != 2)
			{
				mountData.backTexture = TextureAssets.Extra[204];
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = Asset<Texture2D>.Empty;
				mountData.frontTextureExtra = Asset<Texture2D>.Empty;
				mountData.textureWidth = mountData.backTexture.Width();
				mountData.textureHeight = mountData.backTexture.Height();
			}
			mountData = new Mount.MountData();
			Mount.mounts[52] = mountData;
			mountData.delegations = new Mount.MountDelegatesData();
			Mount.MountDelegatesData delegations16 = mountData.delegations;
			Mount.MountDelegatesData.OverridePositionMethod mouthPosition;
			if ((mouthPosition = Mount.<>O.<10>__WolfMouthPosition) == null)
			{
				mouthPosition = (Mount.<>O.<10>__WolfMouthPosition = new Mount.MountDelegatesData.OverridePositionMethod(DelegateMethods.Mount.WolfMouthPosition));
			}
			delegations16.MouthPosition = mouthPosition;
			Mount.MountDelegatesData delegations17 = mountData.delegations;
			Mount.MountDelegatesData.OverridePositionMethod handPosition;
			if ((handPosition = Mount.<>O.<11>__NoHandPosition) == null)
			{
				handPosition = (Mount.<>O.<11>__NoHandPosition = new Mount.MountDelegatesData.OverridePositionMethod(DelegateMethods.Mount.NoHandPosition));
			}
			delegations17.HandPosition = handPosition;
			mountData.spawnDust = 31;
			mountData.buff = 342;
			mountData.flightTimeMax = 0;
			mountData.fallDamage = 0.1f;
			mountData.runSpeed = 9.5f;
			mountData.acceleration = 0.18f;
			mountData.jumpHeight = 18;
			mountData.jumpSpeed = 9.01f;
			mountData.totalFrames = 15;
			array = new int[mountData.totalFrames];
			for (int num23 = 0; num23 < array.Length; num23++)
			{
				array[num23] = 0;
			}
			array[1] += -14;
			array[2] += -10;
			array[3] += -8;
			array[4] += 18;
			array[5] += -2;
			int[] array2 = array;
			int num24 = 6;
			array2[num24] = array2[num24];
			array[7] += 2;
			array[8] += 4;
			array[9] += 4;
			array[10] += 2;
			int[] array3 = array;
			int num25 = 11;
			array3[num25] = array3[num25];
			array[12] += 4;
			array[13] += 2;
			array[14] += -4;
			mountData.playerYOffsets = array;
			mountData.xOffset = 4;
			mountData.bodyFrame = 3;
			mountData.yOffset = -1;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 6;
			mountData.runningFrameDelay = 20;
			mountData.runningFrameStart = 5;
			mountData.inAirFrameCount = 3;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 12;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = mountData.inAirFrameCount;
			mountData.swimFrameDelay = mountData.inAirFrameDelay;
			mountData.swimFrameStart = mountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				mountData.backTexture = Asset<Texture2D>.Empty;
				mountData.backTextureExtra = Asset<Texture2D>.Empty;
				mountData.frontTexture = TextureAssets.Extra[253];
				mountData.frontTextureExtra = TextureAssets.Extra[254];
				mountData.textureWidth = mountData.frontTexture.Width();
				mountData.textureHeight = mountData.frontTexture.Height();
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0015078C File Offset: 0x0014E98C
		public static void SetAsHorse(Mount.MountData newMount, int buff, Asset<Texture2D> texture)
		{
			newMount.spawnDust = 3;
			newMount.buff = buff;
			newMount.heightBoost = 34;
			newMount.flightTimeMax = 0;
			newMount.fallDamage = 0.5f;
			newMount.runSpeed = 3f;
			newMount.dashSpeed = 9f;
			newMount.acceleration = 0.25f;
			newMount.jumpHeight = 6;
			newMount.jumpSpeed = 7.01f;
			newMount.totalFrames = 16;
			int[] array = new int[newMount.totalFrames];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 28;
			}
			array[3] += 2;
			array[4] += 2;
			array[7] += 2;
			array[8] += 2;
			array[12] += 2;
			array[13] += 2;
			array[15] += 4;
			newMount.playerYOffsets = array;
			newMount.xOffset = 5;
			newMount.bodyFrame = 3;
			newMount.yOffset = 1;
			newMount.playerHeadOffset = 34;
			newMount.standingFrameCount = 1;
			newMount.standingFrameDelay = 12;
			newMount.standingFrameStart = 0;
			newMount.runningFrameCount = 7;
			newMount.runningFrameDelay = 15;
			newMount.runningFrameStart = 1;
			newMount.dashingFrameCount = 6;
			newMount.dashingFrameDelay = 40;
			newMount.dashingFrameStart = 9;
			newMount.flyingFrameCount = 6;
			newMount.flyingFrameDelay = 6;
			newMount.flyingFrameStart = 1;
			newMount.inAirFrameCount = 1;
			newMount.inAirFrameDelay = 12;
			newMount.inAirFrameStart = 15;
			newMount.idleFrameCount = 0;
			newMount.idleFrameDelay = 0;
			newMount.idleFrameStart = 0;
			newMount.idleFrameLoop = false;
			newMount.swimFrameCount = newMount.inAirFrameCount;
			newMount.swimFrameDelay = newMount.inAirFrameDelay;
			newMount.swimFrameStart = newMount.inAirFrameStart;
			if (Main.netMode != 2)
			{
				newMount.backTexture = texture;
				newMount.backTextureExtra = Asset<Texture2D>.Empty;
				newMount.frontTexture = Asset<Texture2D>.Empty;
				newMount.frontTextureExtra = Asset<Texture2D>.Empty;
				newMount.textureWidth = newMount.backTexture.Width();
				newMount.textureHeight = newMount.backTexture.Height();
			}
		}

		/// <summary>
		/// This method sets a variety of MountData values common to most minecarts.
		/// <para />Notably:<code>
		/// runSpeed = 13f;
		/// dashSpeed = 13f;
		/// acceleration = 0.04f;
		/// jumpHeight = 15;
		/// jumpSpeed = 5.15f;
		/// </code>
		/// </summary>
		// Token: 0x060006BE RID: 1726 RVA: 0x00150997 File Offset: 0x0014EB97
		public static void SetAsMinecart(Mount.MountData newMount, int buff, Asset<Texture2D> texture, int verticalOffset = 0, int playerVerticalOffset = 0)
		{
			Mount.SetAsMinecart(newMount, buff, buff, texture, verticalOffset, playerVerticalOffset);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x001509A8 File Offset: 0x0014EBA8
		private static void SetAsMinecart(Mount.MountData newMount, int buffToLeft, int buffToRight, Asset<Texture2D> texture, int verticalOffset = 0, int playerVerticalOffset = 0)
		{
			newMount.Minecart = true;
			newMount.delegations = new Mount.MountDelegatesData();
			Mount.MountDelegatesData delegations = newMount.delegations;
			Action<Vector2> minecartDust;
			if ((minecartDust = Mount.<>O.<0>__Sparks) == null)
			{
				minecartDust = (Mount.<>O.<0>__Sparks = new Action<Vector2>(DelegateMethods.Minecart.Sparks));
			}
			delegations.MinecartDust = minecartDust;
			newMount.spawnDust = 213;
			newMount.buff = buffToLeft;
			newMount.extraBuff = buffToRight;
			newMount.heightBoost = 10;
			newMount.flightTimeMax = 0;
			newMount.fallDamage = 1f;
			newMount.runSpeed = 13f;
			newMount.dashSpeed = 13f;
			newMount.acceleration = 0.04f;
			newMount.jumpHeight = 15;
			newMount.jumpSpeed = 5.15f;
			newMount.blockExtraJumps = true;
			newMount.totalFrames = 3;
			int[] array = new int[newMount.totalFrames];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 8 - verticalOffset + playerVerticalOffset;
			}
			newMount.playerYOffsets = array;
			newMount.xOffset = 1;
			newMount.bodyFrame = 3;
			newMount.yOffset = 13 + verticalOffset;
			newMount.playerHeadOffset = 14;
			newMount.standingFrameCount = 1;
			newMount.standingFrameDelay = 12;
			newMount.standingFrameStart = 0;
			newMount.runningFrameCount = 3;
			newMount.runningFrameDelay = 12;
			newMount.runningFrameStart = 0;
			newMount.flyingFrameCount = 0;
			newMount.flyingFrameDelay = 0;
			newMount.flyingFrameStart = 0;
			newMount.inAirFrameCount = 0;
			newMount.inAirFrameDelay = 0;
			newMount.inAirFrameStart = 0;
			newMount.idleFrameCount = 0;
			newMount.idleFrameDelay = 0;
			newMount.idleFrameStart = 0;
			newMount.idleFrameLoop = false;
			if (Main.netMode != 2)
			{
				newMount.backTexture = Asset<Texture2D>.Empty;
				newMount.backTextureExtra = Asset<Texture2D>.Empty;
				newMount.frontTexture = texture;
				newMount.frontTextureExtra = Asset<Texture2D>.Empty;
				newMount.textureWidth = newMount.frontTexture.Width();
				newMount.textureHeight = newMount.frontTexture.Height();
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00150B72 File Offset: 0x0014ED72
		public static int GetHeightBoost(int MountType)
		{
			if (MountType <= -1 || MountType >= Mount.mounts.Length)
			{
				return 0;
			}
			return Mount.mounts[MountType].heightBoost;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00150B90 File Offset: 0x0014ED90
		public int JumpHeight(Player player, float xVelocity)
		{
			int num = this._data.jumpHeight;
			int type = this._type;
			switch (type)
			{
			case 0:
				num += (int)(Math.Abs(xVelocity) / 4f);
				goto IL_65;
			case 1:
				num += (int)(Math.Abs(xVelocity) / 2.5f);
				goto IL_65;
			case 2:
			case 3:
				goto IL_65;
			case 4:
				break;
			default:
				if (type != 49)
				{
					goto IL_65;
				}
				break;
			}
			if (this._frameState == 4)
			{
				num += 5;
			}
			IL_65:
			if (this._shouldSuperCart)
			{
				num = this._data.MinecartUpgradeJumpHeight;
			}
			MountLoader.JumpHeight(player, this._data, ref num, xVelocity);
			return num;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00150C28 File Offset: 0x0014EE28
		public float JumpSpeed(Player player, float xVelocity)
		{
			float num = this._data.jumpSpeed;
			int type = this._type;
			if (type > 1)
			{
				if (type == 4 || type == 49)
				{
					if (this._frameState == 4)
					{
						num += 2.5f;
					}
				}
			}
			else
			{
				num += Math.Abs(xVelocity) / 7f;
			}
			if (this._shouldSuperCart)
			{
				num = this._data.MinecartUpgradeJumpSpeed;
			}
			MountLoader.JumpSpeed(player, this._data, ref num, xVelocity);
			return num;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00150C9D File Offset: 0x0014EE9D
		public bool CanFly()
		{
			return this._active && this._data.flightTimeMax != 0 && this._type != 48;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00150CC3 File Offset: 0x0014EEC3
		public bool CanHover()
		{
			if (!this._active || !this._data.usesHover)
			{
				return false;
			}
			if (this._type == 49)
			{
				return this._frameState == 4;
			}
			return this._data.usesHover;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00150D00 File Offset: 0x0014EF00
		public IEntitySource GetProjectileSpawnSource(Player mountedPlayer)
		{
			return new EntitySource_Mount(mountedPlayer, this.Type, null);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00150D10 File Offset: 0x0014EF10
		public void StartAbilityCharge(Player mountedPlayer)
		{
			if (Main.myPlayer == mountedPlayer.whoAmI)
			{
				if (this._type == 9)
				{
					int type2 = 441;
					float num = Main.screenPosition.X + (float)Main.mouseX;
					float num2 = Main.screenPosition.Y + (float)Main.mouseY;
					float ai = num - mountedPlayer.position.X;
					float ai2 = num2 - mountedPlayer.position.Y;
					Projectile.NewProjectile(this.GetProjectileSpawnSource(mountedPlayer), num, num2, 0f, 0f, type2, 0, 0f, mountedPlayer.whoAmI, ai, ai2, 0f);
					this._abilityCharging = true;
					return;
				}
			}
			else if (this._type == 9)
			{
				this._abilityCharging = true;
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00150DC8 File Offset: 0x0014EFC8
		public void StopAbilityCharge()
		{
			int type = this._type;
			if (type == 9 || type == 46)
			{
				this._abilityCharging = false;
				this._abilityCooldown = this._data.abilityCooldown;
				this._abilityDuration = this._data.abilityDuration;
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00150E0F File Offset: 0x0014F00F
		public bool CheckBuff(int buffID)
		{
			return this._data.buff == buffID || this._data.extraBuff == buffID;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00150E30 File Offset: 0x0014F030
		public void AbilityRecovery()
		{
			if (this._abilityCharging)
			{
				if (this._abilityCharge < this._data.abilityChargeMax)
				{
					this._abilityCharge++;
				}
			}
			else if (this._abilityCharge > 0)
			{
				this._abilityCharge--;
			}
			if (this._abilityCooldown > 0)
			{
				this._abilityCooldown--;
			}
			if (this._abilityDuration > 0)
			{
				this._abilityDuration--;
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00150EAD File Offset: 0x0014F0AD
		public void FatigueRecovery()
		{
			if (this._fatigue > 2f)
			{
				this._fatigue -= 2f;
				return;
			}
			this._fatigue = 0f;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00150EDA File Offset: 0x0014F0DA
		public bool Flight()
		{
			if (this._flyTime <= 0)
			{
				return false;
			}
			this._flyTime--;
			return true;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00150EF8 File Offset: 0x0014F0F8
		public void UpdateDrill(Player mountedPlayer, bool controlUp, bool controlDown)
		{
			Mount.DrillMountData drillMountData = (Mount.DrillMountData)this._mountSpecificData;
			for (int i = 0; i < drillMountData.beams.Length; i++)
			{
				Mount.DrillBeam drillBeam = drillMountData.beams[i];
				if (drillBeam.cooldown > 1)
				{
					drillBeam.cooldown--;
				}
				else if (drillBeam.cooldown == 1)
				{
					drillBeam.cooldown = 0;
					drillBeam.curTileTarget = Point16.NegativeOne;
				}
			}
			drillMountData.diodeRotation = drillMountData.diodeRotation * 0.85f + 0.15f * drillMountData.diodeRotationTarget;
			if (drillMountData.beamCooldown > 0)
			{
				drillMountData.beamCooldown--;
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00150F98 File Offset: 0x0014F198
		public unsafe void UseDrill(Player mountedPlayer)
		{
			if (this._type != 8 || !this._abilityActive)
			{
				return;
			}
			Mount.DrillMountData drillMountData = (Mount.DrillMountData)this._mountSpecificData;
			bool flag = mountedPlayer.whoAmI == Main.myPlayer;
			if (mountedPlayer.controlUseItem)
			{
				int i = 0;
				while (i < Mount.amountOfBeamsAtOnce && drillMountData.beamCooldown == 0)
				{
					for (int j = 0; j < drillMountData.beams.Length; j++)
					{
						Mount.DrillBeam drillBeam = drillMountData.beams[j];
						if (drillBeam.cooldown == 0)
						{
							Point16 point = this.DrillSmartCursor_Blocks(mountedPlayer, drillMountData);
							if (!(point == Point16.NegativeOne))
							{
								drillBeam.curTileTarget = point;
								int pickPower = Mount.drillPickPower;
								if (flag)
								{
									bool flag2 = true;
									if (WorldGen.InWorld((int)point.X, (int)point.Y, 0) && Main.tile[(int)point.X, (int)point.Y] != null && *Main.tile[(int)point.X, (int)point.Y].type == 26 && !Main.hardMode)
									{
										flag2 = false;
										mountedPlayer.Hurt(PlayerDeathReason.ByOther(4, -1), mountedPlayer.statLife / 2, -mountedPlayer.direction, false, false, -1, true, 0f, 0f, 4.5f);
									}
									if (mountedPlayer.noBuilding)
									{
										flag2 = false;
									}
									if (flag2)
									{
										mountedPlayer.PickTile((int)point.X, (int)point.Y, pickPower);
									}
								}
								Vector2 vector;
								vector..ctor((float)(point.X << 4) + 8f, (float)(point.Y << 4) + 8f);
								float num = (vector - mountedPlayer.Center).ToRotation();
								for (int k = 0; k < 2; k++)
								{
									float num2 = num + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.5707964f;
									float num3 = (float)Main.rand.NextDouble() * 2f + 2f;
									Vector2 vector2;
									vector2..ctor((float)Math.Cos((double)num2) * num3, (float)Math.Sin((double)num2) * num3);
									int num4 = Dust.NewDust(vector, 0, 0, 230, vector2.X, vector2.Y, 0, default(Color), 1f);
									Main.dust[num4].noGravity = true;
									Main.dust[num4].customData = mountedPlayer;
								}
								if (flag)
								{
									Tile.SmoothSlope((int)point.X, (int)point.Y, true, true);
								}
								drillBeam.cooldown = Mount.drillPickTime;
								drillBeam.lastPurpose = 0;
								break;
							}
						}
					}
					i++;
				}
			}
			if (!mountedPlayer.controlUseTile)
			{
				return;
			}
			int l = 0;
			while (l < Mount.amountOfBeamsAtOnce && drillMountData.beamCooldown == 0)
			{
				for (int m = 0; m < drillMountData.beams.Length; m++)
				{
					Mount.DrillBeam drillBeam2 = drillMountData.beams[m];
					if (drillBeam2.cooldown == 0)
					{
						Point16 point2 = this.DrillSmartCursor_Walls(mountedPlayer, drillMountData);
						if (!(point2 == Point16.NegativeOne))
						{
							drillBeam2.curTileTarget = point2;
							int damage = Mount.drillPickPower;
							if (flag)
							{
								bool flag3 = true;
								if (mountedPlayer.noBuilding)
								{
									flag3 = false;
								}
								if (flag3)
								{
									mountedPlayer.PickWall((int)point2.X, (int)point2.Y, damage);
								}
							}
							Vector2 vector3;
							vector3..ctor((float)(point2.X << 4) + 8f, (float)(point2.Y << 4) + 8f);
							float num5 = (vector3 - mountedPlayer.Center).ToRotation();
							for (int n = 0; n < 2; n++)
							{
								float num6 = num5 + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.5707964f;
								float num7 = (float)Main.rand.NextDouble() * 2f + 2f;
								Vector2 vector4;
								vector4..ctor((float)Math.Cos((double)num6) * num7, (float)Math.Sin((double)num6) * num7);
								int num8 = Dust.NewDust(vector3, 0, 0, 230, vector4.X, vector4.Y, 0, default(Color), 1f);
								Main.dust[num8].noGravity = true;
								Main.dust[num8].customData = mountedPlayer;
							}
							drillBeam2.cooldown = Mount.drillPickTime;
							drillBeam2.lastPurpose = 1;
							break;
						}
					}
				}
				l++;
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0015140C File Offset: 0x0014F60C
		private Point16 DrillSmartCursor_Blocks(Player mountedPlayer, Mount.DrillMountData data)
		{
			Vector2 vector3 = (mountedPlayer.whoAmI != Main.myPlayer) ? data.crosshairPosition : (Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY));
			Vector2 center = mountedPlayer.Center;
			Vector2 vector2 = vector3 - center;
			float num = vector2.Length();
			if (num > 224f)
			{
				num = 224f;
			}
			num += 32f;
			vector2.Normalize();
			Vector2 end = center + vector2 * num;
			Point16 tilePoint = new Point16(-1, -1);
			if (!Utils.PlotTileLine(center, end, 65.6f, delegate(int x, int y)
			{
				tilePoint = new Point16(x, y);
				for (int i = 0; i < data.beams.Length; i++)
				{
					if (data.beams[i].curTileTarget == tilePoint && data.beams[i].lastPurpose == 0)
					{
						return true;
					}
				}
				return !WorldGen.CanKillTile(x, y) || Main.tile[x, y] == null || Main.tile[x, y].inActive() || !Main.tile[x, y].active();
			}))
			{
				return tilePoint;
			}
			return new Point16(-1, -1);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x001514D4 File Offset: 0x0014F6D4
		private unsafe Point16 DrillSmartCursor_Walls(Player mountedPlayer, Mount.DrillMountData data)
		{
			Vector2 vector3 = (mountedPlayer.whoAmI != Main.myPlayer) ? data.crosshairPosition : (Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY));
			Vector2 center = mountedPlayer.Center;
			Vector2 vector2 = vector3 - center;
			float num = vector2.Length();
			if (num > 224f)
			{
				num = 224f;
			}
			num += 32f;
			num += 16f;
			vector2.Normalize();
			Vector2 end = center + vector2 * num;
			Point16 tilePoint = new Point16(-1, -1);
			if (!Utils.PlotTileLine(center, end, 97.6f, delegate(int x, int y)
			{
				tilePoint = new Point16(x, y);
				for (int i = 0; i < data.beams.Length; i++)
				{
					if (data.beams[i].curTileTarget == tilePoint && data.beams[i].lastPurpose == 1)
					{
						return true;
					}
				}
				Tile tile = Main.tile[x, y];
				return !(tile == null) && (*tile.wall <= 0 || !Player.CanPlayerSmashWall(x, y));
			}))
			{
				return tilePoint;
			}
			return new Point16(-1, -1);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x001515A4 File Offset: 0x0014F7A4
		public void UseAbility(Player mountedPlayer, Vector2 mousePosition, bool toggleOn)
		{
			int type3 = this._type;
			if (type3 != 8)
			{
				if (type3 != 9)
				{
					if (type3 != 46)
					{
						return;
					}
					if (Main.myPlayer == mountedPlayer.whoAmI)
					{
						SoundStyle soundStyle;
						if (this._abilityCooldown <= 10)
						{
							int damage = 120;
							Vector2 vector = mountedPlayer.Center + new Vector2((float)(mountedPlayer.width * -(float)mountedPlayer.direction), 26f);
							Vector2 vector2 = new Vector2(0f, -4f).RotatedByRandom(0.10000000149011612);
							Projectile.NewProjectile(this.GetProjectileSpawnSource(mountedPlayer), vector.X, vector.Y, vector2.X, vector2.Y, 930, damage, 0f, Main.myPlayer, 0f, 0f, 0f);
							soundStyle = SoundID.Item89;
							soundStyle.Volume = SoundID.Item89.Volume * 0.2f;
							SoundEngine.PlaySound(soundStyle, new Vector2?(vector), null);
						}
						int type = 14;
						int damage2 = 100;
						mousePosition = this.ClampToDeadZone(mountedPlayer, mousePosition);
						Vector2 vector3 = default(Vector2);
						vector3.X = mountedPlayer.position.X + (float)(mountedPlayer.width / 2);
						vector3.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
						Vector2 vector4;
						vector4..ctor(vector3.X + (float)(mountedPlayer.width * mountedPlayer.direction), vector3.Y - 12f);
						Vector2 v = mousePosition - vector4;
						v = v.SafeNormalize(Vector2.Zero);
						v *= 12f;
						v = v.RotatedByRandom(0.20000000298023224);
						Projectile.NewProjectile(this.GetProjectileSpawnSource(mountedPlayer), vector4.X, vector4.Y, v.X, v.Y, type, damage2, 0f, Main.myPlayer, 0f, 0f, 0f);
						soundStyle = SoundID.Item11;
						soundStyle.Volume = SoundID.Item11.Volume * 0.2f;
						SoundEngine.PlaySound(soundStyle, new Vector2?(vector4), null);
						return;
					}
				}
				else if (Main.myPlayer == mountedPlayer.whoAmI)
				{
					int type2 = 606;
					mousePosition = this.ClampToDeadZone(mountedPlayer, mousePosition);
					Vector2 vector5 = default(Vector2);
					vector5.X = mountedPlayer.position.X + (float)(mountedPlayer.width / 2);
					vector5.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
					int num3 = (this._frameExtra - 6) * 2;
					Vector2 vector6 = default(Vector2);
					for (int i = 0; i < 2; i++)
					{
						vector6.Y = vector5.Y + Mount.scutlixEyePositions[num3 + i].Y + (float)this._data.yOffset;
						if (mountedPlayer.direction == -1)
						{
							vector6.X = vector5.X - Mount.scutlixEyePositions[num3 + i].X - (float)this._data.xOffset;
						}
						else
						{
							vector6.X = vector5.X + Mount.scutlixEyePositions[num3 + i].X + (float)this._data.xOffset;
						}
						Vector2 vector7 = mousePosition - vector6;
						vector7.Normalize();
						vector7 *= 14f;
						int damage3 = 150;
						vector6 += vector7;
						Projectile.NewProjectile(this.GetProjectileSpawnSource(mountedPlayer), vector6.X, vector6.Y, vector7.X, vector7.Y, type2, damage3, 0f, Main.myPlayer, 0f, 0f, 0f);
					}
					return;
				}
			}
			else if (Main.myPlayer == mountedPlayer.whoAmI)
			{
				if (!toggleOn)
				{
					this._abilityActive = false;
					return;
				}
				if (!this._abilityActive)
				{
					if (mountedPlayer.whoAmI == Main.myPlayer)
					{
						float num4 = Main.screenPosition.X + (float)Main.mouseX;
						float num5 = Main.screenPosition.Y + (float)Main.mouseY;
						float ai = num4 - mountedPlayer.position.X;
						float ai2 = num5 - mountedPlayer.position.Y;
						Projectile.NewProjectile(this.GetProjectileSpawnSource(mountedPlayer), num4, num5, 0f, 0f, 453, 0, 0f, mountedPlayer.whoAmI, ai, ai2, 0f);
					}
					this._abilityActive = true;
					return;
				}
			}
			else
			{
				this._abilityActive = toggleOn;
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00151A2C File Offset: 0x0014FC2C
		public bool Hover(Player mountedPlayer)
		{
			bool flag = this.DoesHoverIgnoresFatigue();
			bool flag2 = this._frameState == 2 || this._frameState == 4;
			if (this._type == 49)
			{
				flag2 = (this._frameState == 4);
			}
			if (flag2)
			{
				bool flag3 = true;
				float num = 1f;
				float num2 = mountedPlayer.gravity / Player.defaultGravity;
				if (mountedPlayer.slowFall)
				{
					num2 /= 3f;
				}
				if (num2 < 0.25f)
				{
					num2 = 0.25f;
				}
				if (!flag)
				{
					if (this._flyTime > 0)
					{
						this._flyTime--;
					}
					else if (this._fatigue < this._fatigueMax)
					{
						this._fatigue += num2;
					}
					else
					{
						flag3 = false;
					}
				}
				if (this._type == 12 && !mountedPlayer.MountFishronSpecial)
				{
					num = 0.5f;
				}
				float num3 = this._fatigue / this._fatigueMax;
				if (flag)
				{
					num3 = 0f;
				}
				bool flag4 = true;
				if (this._type == 48)
				{
					flag4 = false;
				}
				float num4 = 4f * num3;
				float num5 = 4f * num3;
				bool flag5 = false;
				if (this._type == 48)
				{
					num4 = 0f;
					num5 = 0f;
					if (!flag3)
					{
						flag5 = true;
					}
					if (mountedPlayer.controlDown)
					{
						num5 = 8f;
					}
				}
				if (num4 == 0f)
				{
					num4 = -0.001f;
				}
				if (num5 == 0f)
				{
					num5 = -0.001f;
				}
				float num6 = mountedPlayer.velocity.Y;
				if (flag4 && (mountedPlayer.controlUp || mountedPlayer.controlJump) && flag3)
				{
					num4 = -2f - 6f * (1f - num3);
					if (this._type == 48)
					{
						num4 /= 3f;
					}
					num6 -= this._data.acceleration * num;
				}
				else if (mountedPlayer.controlDown)
				{
					num6 += this._data.acceleration * num;
					num5 = 8f;
				}
				else if (flag5)
				{
					float num7 = mountedPlayer.gravity * mountedPlayer.gravDir;
					num6 += num7;
					num5 = 4f;
				}
				else
				{
					int jump = mountedPlayer.jump;
				}
				if (num6 < num4)
				{
					num6 = ((num4 - num6 >= this._data.acceleration) ? (num6 + this._data.acceleration * num) : num4);
				}
				else if (num6 > num5)
				{
					num6 = ((num6 - num5 >= this._data.acceleration) ? (num6 - this._data.acceleration * num) : num5);
				}
				mountedPlayer.velocity.Y = num6;
				if (num4 == -0.001f && num5 == -0.001f && num6 == -0.001f)
				{
					mountedPlayer.position.Y = mountedPlayer.position.Y - -0.001f;
				}
				mountedPlayer.fallStart = (int)(mountedPlayer.position.Y / 16f);
			}
			else if (!flag)
			{
				mountedPlayer.velocity.Y = mountedPlayer.velocity.Y + mountedPlayer.gravity * mountedPlayer.gravDir;
			}
			else if (mountedPlayer.velocity.Y == 0f)
			{
				Vector2 velocity = Vector2.UnitY * mountedPlayer.gravDir * 1f;
				if (Collision.TileCollision(mountedPlayer.position, velocity, mountedPlayer.width, mountedPlayer.height, false, false, (int)mountedPlayer.gravDir).Y != 0f || mountedPlayer.controlDown)
				{
					mountedPlayer.velocity.Y = 0.001f;
				}
			}
			else if (mountedPlayer.velocity.Y == -0.001f)
			{
				mountedPlayer.velocity.Y = mountedPlayer.velocity.Y - -0.001f;
			}
			if (this._type == 7)
			{
				float num8 = mountedPlayer.velocity.X / this._data.dashSpeed;
				if ((double)num8 > 0.95)
				{
					num8 = 0.95f;
				}
				if ((double)num8 < -0.95)
				{
					num8 = -0.95f;
				}
				float fullRotation = 0.7853982f * num8 / 2f;
				float num9 = Math.Abs(2f - (float)this._frame / 2f) / 2f;
				Lighting.AddLight((int)(mountedPlayer.position.X + (float)(mountedPlayer.width / 2)) / 16, (int)(mountedPlayer.position.Y + (float)(mountedPlayer.height / 2)) / 16, 0.4f, 0.2f * num9, 0f);
				mountedPlayer.fullRotation = fullRotation;
			}
			else if (this._type == 8)
			{
				float num10 = mountedPlayer.velocity.X / this._data.dashSpeed;
				if ((double)num10 > 0.95)
				{
					num10 = 0.95f;
				}
				if ((double)num10 < -0.95)
				{
					num10 = -0.95f;
				}
				float fullRotation2 = 0.7853982f * num10 / 2f;
				mountedPlayer.fullRotation = fullRotation2;
				Mount.DrillMountData drillMountData = (Mount.DrillMountData)this._mountSpecificData;
				float outerRingRotation = drillMountData.outerRingRotation;
				outerRingRotation += mountedPlayer.velocity.X / 80f;
				if (outerRingRotation > 3.1415927f)
				{
					outerRingRotation -= 6.2831855f;
				}
				else if (outerRingRotation < -3.1415927f)
				{
					outerRingRotation += 6.2831855f;
				}
				drillMountData.outerRingRotation = outerRingRotation;
			}
			else if (this._type == 23)
			{
				float value = (0f - mountedPlayer.velocity.Y) / this._data.dashSpeed;
				value = MathHelper.Clamp(value, -1f, 1f);
				float value2 = mountedPlayer.velocity.X / this._data.dashSpeed;
				value2 = MathHelper.Clamp(value2, -1f, 1f);
				float num12 = -0.19634955f * value * (float)mountedPlayer.direction;
				float num11 = 0.19634955f * value2;
				float fullRotation3 = num12 + num11;
				mountedPlayer.fullRotation = fullRotation3;
				mountedPlayer.fullRotationOrigin = new Vector2((float)(mountedPlayer.width / 2), (float)mountedPlayer.height);
			}
			return true;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00151FFC File Offset: 0x001501FC
		private bool DoesHoverIgnoresFatigue()
		{
			return this._type == 7 || this._type == 8 || this._type == 12 || this._type == 23 || this._type == 44 || this._type == 49;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0015203C File Offset: 0x0015023C
		private float GetWitchBroomTrinketRotation(Player player)
		{
			float num = Utils.Clamp<float>(player.velocity.X / 10f, -1f, 1f);
			Point point = player.Center.ToTileCoordinates();
			float num2 = 0.5f;
			if (WorldGen.InAPlaceWithWind(point.X, point.Y, 1, 1))
			{
				num2 = 1f;
			}
			float num3 = (float)Math.Sin((double)((float)player.miscCounter / 300f * 6.2831855f * 3f)) * 0.7853982f * Math.Abs(Main.WindForVisuals) * 0.5f + 0.7853982f * (0f - Main.WindForVisuals) * 0.5f;
			num3 *= num2;
			return num * (float)Math.Sin((double)((float)player.miscCounter / 150f * 6.2831855f * 3f)) * 0.7853982f * 0.5f + num * 0.7853982f * 0.5f + num3;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0015212F File Offset: 0x0015032F
		private Vector2 GetWitchBroomTrinketOriginOffset(Player player)
		{
			return new Vector2((float)(29 * player.direction), -4f);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00152148 File Offset: 0x00150348
		public unsafe void UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
		{
			if (!MountLoader.UpdateFrame(mountedPlayer, state, velocity))
			{
				return;
			}
			if (this._frameState != state)
			{
				this._frameState = state;
				if (this._type != 48 || (state != 1 && state != 2))
				{
					this._frameCounter = 0f;
				}
			}
			if (state != 0)
			{
				this._idleTime = 0;
			}
			if (this._data.emitsLight)
			{
				Point point = mountedPlayer.Center.ToTileCoordinates();
				Lighting.AddLight(point.X, point.Y, this._data.lightColor.X, this._data.lightColor.Y, this._data.lightColor.Z);
			}
			int type2 = this._type;
			switch (type2)
			{
			case 5:
				if (state != 2)
				{
					this._frameExtra = 0;
					this._frameExtraCounter = 0f;
					goto IL_1407;
				}
				goto IL_1407;
			case 6:
			case 11:
			case 12:
			case 13:
			case 15:
			case 16:
				goto IL_1407;
			case 7:
				state = 2;
				goto IL_1407;
			case 8:
			{
				if (state != 0 && state != 1)
				{
					goto IL_1407;
				}
				Vector2 position = default(Vector2);
				position.X = mountedPlayer.position.X;
				position.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
				int num6 = (int)(position.X / 16f);
				float num37 = position.Y / 16f;
				float num7 = 0f;
				float num8 = (float)mountedPlayer.width;
				while (num8 > 0f)
				{
					float num9 = (float)((num6 + 1) * 16) - position.X;
					if (num9 > num8)
					{
						num9 = num8;
					}
					num7 += Collision.GetTileRotation(position) * num9;
					num8 -= num9;
					position.X += num9;
					num6++;
				}
				float num10 = num7 / (float)mountedPlayer.width - mountedPlayer.fullRotation;
				float num11 = 0f;
				float num12 = 0.15707964f;
				if (num10 < 0f)
				{
					num11 = ((num10 <= 0f - num12) ? (0f - num12) : num10);
				}
				else if (num10 > 0f)
				{
					num11 = ((num10 >= num12) ? num12 : num10);
				}
				if (num11 == 0f)
				{
					goto IL_1407;
				}
				mountedPlayer.fullRotation += num11;
				if (mountedPlayer.fullRotation > 0.7853982f)
				{
					mountedPlayer.fullRotation = 0.7853982f;
				}
				if (mountedPlayer.fullRotation < -0.7853982f)
				{
					mountedPlayer.fullRotation = -0.7853982f;
					goto IL_1407;
				}
				goto IL_1407;
			}
			case 9:
				if (this._aiming)
				{
					goto IL_1407;
				}
				this._frameExtraCounter += 1f;
				if (this._frameExtraCounter < 12f)
				{
					goto IL_1407;
				}
				this._frameExtraCounter = 0f;
				this._frameExtra++;
				if (this._frameExtra >= 6)
				{
					this._frameExtra = 0;
					goto IL_1407;
				}
				goto IL_1407;
			case 10:
				break;
			case 14:
			{
				bool flag9 = Math.Abs(velocity.X) > this.RunSpeed / 2f;
				float num13 = (float)Math.Sign(mountedPlayer.velocity.X);
				float num14 = 12f;
				float num15 = 40f;
				if (!flag9)
				{
					mountedPlayer.basiliskCharge = 0f;
				}
				else
				{
					mountedPlayer.basiliskCharge = Utils.Clamp<float>(mountedPlayer.basiliskCharge + 0.0055555557f, 0f, 1f);
				}
				if ((double)mountedPlayer.position.Y > Main.worldSurface * 16.0 + 160.0)
				{
					Lighting.AddLight(mountedPlayer.Center, 0.5f, 0.1f, 0.1f);
				}
				if (flag9 && velocity.Y == 0f)
				{
					for (int i = 0; i < 2; i++)
					{
						Dust dust6 = Main.dust[Dust.NewDust(mountedPlayer.BottomLeft, mountedPlayer.width, 6, 31, 0f, 0f, 0, default(Color), 1f)];
						dust6.velocity = new Vector2(velocity.X * 0.15f, Main.rand.NextFloat() * -2f);
						dust6.noLight = true;
						dust6.scale = 0.5f + Main.rand.NextFloat() * 0.8f;
						dust6.fadeIn = 0.5f + Main.rand.NextFloat() * 1f;
						dust6.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
					}
					if (mountedPlayer.cMount == 0)
					{
						mountedPlayer.position += new Vector2(num13 * 24f, 0f);
						mountedPlayer.FloorVisuals(true);
						mountedPlayer.position -= new Vector2(num13 * 24f, 0f);
					}
				}
				if (num13 == (float)mountedPlayer.direction)
				{
					for (int j = 0; j < (int)(3f * mountedPlayer.basiliskCharge); j++)
					{
						Dust dust = Main.dust[Dust.NewDust(mountedPlayer.BottomLeft, mountedPlayer.width, 6, 6, 0f, 0f, 0, default(Color), 1f)];
						Vector2 vector = mountedPlayer.Center + new Vector2(num13 * num15, num14);
						dust.position = mountedPlayer.Center + new Vector2(num13 * (num15 - 2f), num14 - 6f + Main.rand.NextFloat() * 12f);
						dust.velocity = (dust.position - vector).SafeNormalize(Vector2.Zero) * (3.5f + Main.rand.NextFloat() * 0.5f);
						if (dust.velocity.Y < 0f)
						{
							Dust dust7 = dust;
							dust7.velocity.Y = dust7.velocity.Y * (1f + 2f * Main.rand.NextFloat());
						}
						dust.velocity += mountedPlayer.velocity * 0.55f;
						dust.velocity *= mountedPlayer.velocity.Length() / this.RunSpeed;
						dust.velocity *= mountedPlayer.basiliskCharge;
						dust.noGravity = true;
						dust.noLight = true;
						dust.scale = 0.5f + Main.rand.NextFloat() * 0.8f;
						dust.fadeIn = 0.5f + Main.rand.NextFloat() * 1f;
						dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
					}
					goto IL_1407;
				}
				goto IL_1407;
			}
			case 17:
				this.UpdateFrame_GolfCart(mountedPlayer, state, velocity);
				goto IL_1407;
			default:
				switch (type2)
				{
				case 39:
					this._frameExtraCounter += 1f;
					if (this._frameExtraCounter <= 6f)
					{
						goto IL_1407;
					}
					this._frameExtraCounter = 0f;
					this._frameExtra++;
					if (this._frameExtra > 5)
					{
						this._frameExtra = 0;
						goto IL_1407;
					}
					goto IL_1407;
				case 40:
				case 41:
				case 42:
				case 47:
					break;
				case 43:
					if (mountedPlayer.velocity.Y == 0f)
					{
						mountedPlayer.isPerformingPogostickTricks = false;
					}
					if (mountedPlayer.isPerformingPogostickTricks)
					{
						mountedPlayer.fullRotation += (float)mountedPlayer.direction * 6.2831855f / 30f;
					}
					else
					{
						mountedPlayer.fullRotation = (float)Math.Sign(mountedPlayer.velocity.X) * Utils.GetLerpValue(0f, this.RunSpeed - 0.2f, Math.Abs(mountedPlayer.velocity.X), true) * 0.4f;
					}
					mountedPlayer.fullRotationOrigin = new Vector2((float)(mountedPlayer.width / 2), (float)mountedPlayer.height * 0.8f);
					goto IL_1407;
				case 44:
				{
					state = 1;
					bool flag3 = Math.Abs(velocity.X) > this.DashSpeed - this.RunSpeed / 4f;
					if (this._mountSpecificData == null)
					{
						this._mountSpecificData = false;
					}
					bool flag4 = (bool)this._mountSpecificData;
					if (flag4 && !flag3)
					{
						this._mountSpecificData = false;
					}
					else if (!flag4 && flag3)
					{
						this._mountSpecificData = true;
						Vector2 vector2 = mountedPlayer.Center + new Vector2((float)(mountedPlayer.width * mountedPlayer.direction), 0f);
						Vector2 vector3;
						vector3..ctor(40f, 30f);
						float num16 = 6.2831855f * Main.rand.NextFloat();
						for (float num17 = 0f; num17 < 20f; num17 += 1f)
						{
							Dust dust2 = Main.dust[Dust.NewDust(vector2, 0, 0, 228, 0f, 0f, 0, default(Color), 1f)];
							Vector2 vector4 = Vector2.UnitY.RotatedBy((double)(num17 * 6.2831855f / 20f + num16), default(Vector2));
							vector4 *= 0.8f;
							dust2.position = vector2 + vector4 * vector3;
							dust2.velocity = vector4 + new Vector2(this.RunSpeed - (float)Math.Sign(velocity.Length()), 0f);
							if (velocity.X > 0f)
							{
								Dust dust8 = dust2;
								dust8.velocity.X = dust8.velocity.X * -1f;
							}
							if (Main.rand.Next(2) == 0)
							{
								dust2.velocity *= 0.5f;
							}
							dust2.noGravity = true;
							dust2.scale = 1.5f + Main.rand.NextFloat() * 0.8f;
							dust2.fadeIn = 0f;
							dust2.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
						}
					}
					int num18 = (int)this.RunSpeed - (int)Math.Abs(velocity.X);
					if (num18 <= 0)
					{
						num18 = 1;
					}
					if (Main.rand.Next(num18) == 0)
					{
						int num19 = 22;
						int num20 = mountedPlayer.width / 2 + 10;
						Vector2 bottom = mountedPlayer.Bottom;
						bottom.X -= (float)num20;
						bottom.Y -= (float)(num19 - 6);
						Dust dust9 = Main.dust[Dust.NewDust(bottom, num20 * 2, num19, 228, 0f, 0f, 0, default(Color), 1f)];
						dust9.velocity = Vector2.Zero;
						dust9.noGravity = true;
						dust9.noLight = true;
						dust9.scale = 0.25f + Main.rand.NextFloat() * 0.8f;
						dust9.fadeIn = 0.5f + Main.rand.NextFloat() * 2f;
						dust9.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
						goto IL_1407;
					}
					goto IL_1407;
				}
				case 45:
				{
					bool flag5 = Math.Abs(velocity.X) > this.DashSpeed * 0.9f;
					if (this._mountSpecificData == null)
					{
						this._mountSpecificData = false;
					}
					bool flag6 = (bool)this._mountSpecificData;
					if (flag6 && !flag5)
					{
						this._mountSpecificData = false;
					}
					else if (!flag6 && flag5)
					{
						this._mountSpecificData = true;
						Vector2 vector5 = mountedPlayer.Center + new Vector2((float)(mountedPlayer.width * mountedPlayer.direction), 0f);
						Vector2 vector6;
						vector6..ctor(40f, 30f);
						float num21 = 6.2831855f * Main.rand.NextFloat();
						for (float num22 = 0f; num22 < 20f; num22 += 1f)
						{
							Dust dust3 = Main.dust[Dust.NewDust(vector5, 0, 0, 6, 0f, 0f, 0, default(Color), 1f)];
							Vector2 vector7 = Vector2.UnitY.RotatedBy((double)(num22 * 6.2831855f / 20f + num21), default(Vector2));
							vector7 *= 0.8f;
							dust3.position = vector5 + vector7 * vector6;
							dust3.velocity = vector7 + new Vector2(this.RunSpeed - (float)Math.Sign(velocity.Length()), 0f);
							if (velocity.X > 0f)
							{
								Dust dust10 = dust3;
								dust10.velocity.X = dust10.velocity.X * -1f;
							}
							if (Main.rand.Next(2) == 0)
							{
								dust3.velocity *= 0.5f;
							}
							dust3.noGravity = true;
							dust3.scale = 1.5f + Main.rand.NextFloat() * 0.8f;
							dust3.fadeIn = 0f;
							dust3.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
						}
					}
					if (flag5)
					{
						int num23 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
						{
							6,
							6,
							31
						});
						int num24 = 6;
						Dust dust4 = Main.dust[Dust.NewDust(mountedPlayer.Center - new Vector2((float)num24, (float)(num24 - 12)), num24 * 2, num24 * 2, num23, 0f, 0f, 0, default(Color), 1f)];
						dust4.velocity = mountedPlayer.velocity * 0.1f;
						if (Main.rand.Next(2) == 0)
						{
							dust4.noGravity = true;
						}
						dust4.scale = 0.7f + Main.rand.NextFloat() * 0.8f;
						if (Main.rand.Next(3) == 0)
						{
							dust4.fadeIn = 0.1f;
						}
						if (num23 == 31)
						{
							dust4.noGravity = true;
							dust4.scale *= 1.5f;
							dust4.fadeIn = 0.2f;
						}
						dust4.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
						goto IL_1407;
					}
					goto IL_1407;
				}
				case 46:
					if (state != 0)
					{
						state = 1;
					}
					if (!this._aiming)
					{
						if (state == 0)
						{
							this._frameExtra = 12;
							this._frameExtraCounter = 0f;
							goto IL_1407;
						}
						if (this._frameExtra < 12)
						{
							this._frameExtra = 12;
						}
						this._frameExtraCounter += Math.Abs(velocity.X);
						if (this._frameExtraCounter < 8f)
						{
							goto IL_1407;
						}
						this._frameExtraCounter = 0f;
						this._frameExtra++;
						if (this._frameExtra >= 24)
						{
							this._frameExtra = 12;
							goto IL_1407;
						}
						goto IL_1407;
					}
					else
					{
						if (this._frameExtra < 24)
						{
							this._frameExtra = 24;
						}
						this._frameExtraCounter += 1f;
						if (this._frameExtraCounter < 3f)
						{
							goto IL_1407;
						}
						this._frameExtraCounter = 0f;
						this._frameExtra++;
						if (this._frameExtra >= 27)
						{
							this._frameExtra = 24;
							goto IL_1407;
						}
						goto IL_1407;
					}
					break;
				case 48:
					state = 1;
					goto IL_1407;
				case 49:
				{
					if (state != 4 && mountedPlayer.wet)
					{
						state = (this._frameState = 4);
					}
					velocity.Length();
					float num25 = mountedPlayer.velocity.Y * 0.05f;
					if (mountedPlayer.direction < 0)
					{
						num25 *= -1f;
					}
					mountedPlayer.fullRotation = num25;
					mountedPlayer.fullRotationOrigin = new Vector2((float)(mountedPlayer.width / 2), (float)(mountedPlayer.height / 2));
					goto IL_1407;
				}
				case 50:
					if (mountedPlayer.velocity.Y == 0f)
					{
						this._frameExtraCounter = 0f;
						this._frameExtra = 3;
						goto IL_1407;
					}
					this._frameExtraCounter += 1f;
					if (this.Flight())
					{
						this._frameExtraCounter += 1f;
					}
					if (this._frameExtraCounter <= 7f)
					{
						goto IL_1407;
					}
					this._frameExtraCounter = 0f;
					this._frameExtra++;
					if (this._frameExtra > 3)
					{
						this._frameExtra = 0;
						goto IL_1407;
					}
					goto IL_1407;
				case 51:
					goto IL_1407;
				case 52:
					if (state == 4)
					{
						state = 2;
						goto IL_1407;
					}
					goto IL_1407;
				default:
					goto IL_1407;
				}
				break;
			}
			bool flag7 = Math.Abs(velocity.X) > this.DashSpeed - this.RunSpeed / 2f;
			if (state == 1)
			{
				bool flag8 = false;
				if (flag7)
				{
					state = 5;
					if (this._frameExtra < 6)
					{
						flag8 = true;
					}
					this._frameExtra++;
				}
				else
				{
					this._frameExtra = 0;
				}
				if ((this._type == 10 || this._type == 47) && flag8)
				{
					int type = 6;
					if (this._type == 10)
					{
						type = Utils.SelectRandom<int>(Main.rand, new int[]
						{
							176,
							177,
							179
						});
					}
					Vector2 vector8 = mountedPlayer.Center + new Vector2((float)(mountedPlayer.width * mountedPlayer.direction), 0f);
					Vector2 vector9;
					vector9..ctor(40f, 30f);
					float num26 = 6.2831855f * Main.rand.NextFloat();
					for (float num27 = 0f; num27 < 14f; num27 += 1f)
					{
						Dust dust5 = Main.dust[Dust.NewDust(vector8, 0, 0, type, 0f, 0f, 0, default(Color), 1f)];
						Vector2 vector10 = Vector2.UnitY.RotatedBy((double)(num27 * 6.2831855f / 14f + num26), default(Vector2));
						vector10 *= 0.2f * (float)this._frameExtra;
						dust5.position = vector8 + vector10 * vector9;
						dust5.velocity = vector10 + new Vector2(this.RunSpeed - (float)(Math.Sign(velocity.X) * this._frameExtra * 2), 0f);
						dust5.noGravity = true;
						if (this._type == 47)
						{
							dust5.noLightEmittence = true;
						}
						dust5.scale = 1f + Main.rand.NextFloat() * 0.8f;
						dust5.fadeIn = Main.rand.NextFloat() * 2f;
						dust5.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
					}
				}
			}
			if (this._type == 10 && flag7)
			{
				Dust dust11 = Main.dust[Dust.NewDust(mountedPlayer.position, mountedPlayer.width, mountedPlayer.height, Utils.SelectRandom<int>(Main.rand, new int[]
				{
					176,
					177,
					179
				}), 0f, 0f, 0, default(Color), 1f)];
				dust11.velocity = Vector2.Zero;
				dust11.noGravity = true;
				dust11.scale = 0.5f + Main.rand.NextFloat() * 0.8f;
				dust11.fadeIn = 1f + Main.rand.NextFloat() * 2f;
				dust11.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
			}
			if (this._type == 47 && flag7 && velocity.Y == 0f)
			{
				int num28 = (int)mountedPlayer.Center.X / 16;
				int num29 = (int)(mountedPlayer.position.Y + (float)mountedPlayer.height - 1f) / 16;
				Tile tile = Main.tile[num28, num29 + 1];
				if (tile != null && tile.active() && *tile.liquid == 0 && WorldGen.SolidTileAllowBottomSlope(num28, num29 + 1))
				{
					ParticleOrchestrator.RequestParticleSpawn(true, ParticleOrchestraType.WallOfFleshGoatMountFlames, new ParticleOrchestraSettings
					{
						PositionInWorld = new Vector2((float)(num28 * 16 + 8), (float)(num29 * 16 + 16))
					}, new int?(mountedPlayer.whoAmI));
				}
			}
			IL_1407:
			switch (state)
			{
			case 0:
				if (this._data.idleFrameCount != 0)
				{
					if (this._type == 5)
					{
						if (this._fatigue != 0f)
						{
							if (this._idleTime == 0)
							{
								this._idleTimeNext = this._idleTime + 1;
							}
						}
						else
						{
							this._idleTime = 0;
							this._idleTimeNext = 2;
						}
					}
					else if (this._idleTime == 0)
					{
						this._idleTimeNext = Main.rand.Next(900, 1500);
						if (this._type == 17)
						{
							this._idleTimeNext = Main.rand.Next(120, 300);
						}
					}
					this._idleTime++;
				}
				this._frameCounter += 1f;
				if (this._data.idleFrameCount != 0 && this._idleTime >= this._idleTimeNext)
				{
					float num30 = (float)this._data.idleFrameDelay;
					if (this._type == 5)
					{
						num30 *= 2f - 1f * this._fatigue / this._fatigueMax;
					}
					int num31 = (int)((float)(this._idleTime - this._idleTimeNext) / num30);
					int idleFrameCount = this._data.idleFrameCount;
					if (num31 >= idleFrameCount)
					{
						if (this._data.idleFrameLoop)
						{
							this._idleTime = this._idleTimeNext;
							this._frame = this._data.idleFrameStart;
						}
						else
						{
							this._frameCounter = 0f;
							this._frame = this._data.standingFrameStart;
							this._idleTime = 0;
						}
					}
					else
					{
						this._frame = this._data.idleFrameStart + num31;
						if (this._data.idleFrameLoop)
						{
							if (this._frame < this._data.idleFrameStart || this._frame >= this._data.idleFrameStart + this._data.idleFrameCount)
							{
								this._frame = this._data.idleFrameStart;
							}
						}
						else if (this._frame < this._data.standingFrameStart || this._frame >= this._data.standingFrameStart + this._data.standingFrameCount)
						{
							this._frame = this._data.standingFrameStart;
						}
					}
					if (this._type == 5)
					{
						this._frameExtra = this._frame;
					}
					if (this._type == 17)
					{
						this._frameExtra = this._frame;
						this._frame = 0;
						return;
					}
				}
				else
				{
					if (this._frameCounter > (float)this._data.standingFrameDelay)
					{
						this._frameCounter -= (float)this._data.standingFrameDelay;
						this._frame++;
					}
					if (this._frame < this._data.standingFrameStart || this._frame >= this._data.standingFrameStart + this._data.standingFrameCount)
					{
						this._frame = this._data.standingFrameStart;
						return;
					}
				}
				break;
			case 1:
			{
				type2 = this._type;
				float num32;
				if (type2 <= 9)
				{
					if (type2 == 6)
					{
						num32 = (this._flipDraw ? velocity.X : (0f - velocity.X));
						goto IL_1824;
					}
					if (type2 != 9)
					{
						goto IL_1817;
					}
				}
				else
				{
					if (type2 == 13)
					{
						num32 = (this._flipDraw ? velocity.X : (0f - velocity.X));
						goto IL_1824;
					}
					switch (type2)
					{
					case 44:
						num32 = Math.Max(1f, Math.Abs(velocity.X) * 0.25f);
						goto IL_1824;
					case 45:
					case 47:
					case 49:
						goto IL_1817;
					case 46:
						break;
					case 48:
						num32 = Math.Max(0.5f, velocity.Length() * 0.125f);
						goto IL_1824;
					case 50:
						num32 = Math.Abs(velocity.X) * 0.5f;
						goto IL_1824;
					default:
						goto IL_1817;
					}
				}
				num32 = ((!this._flipDraw) ? Math.Abs(velocity.X) : (0f - Math.Abs(velocity.X)));
				goto IL_1824;
				IL_1817:
				num32 = Math.Abs(velocity.X);
				IL_1824:
				this._frameCounter += num32;
				if (num32 >= 0f)
				{
					if (this._frameCounter > (float)this._data.runningFrameDelay)
					{
						this._frameCounter -= (float)this._data.runningFrameDelay;
						this._frame++;
					}
					if (this._frame < this._data.runningFrameStart || this._frame >= this._data.runningFrameStart + this._data.runningFrameCount)
					{
						this._frame = this._data.runningFrameStart;
						return;
					}
				}
				else
				{
					if (this._frameCounter < 0f)
					{
						this._frameCounter += (float)this._data.runningFrameDelay;
						this._frame--;
					}
					if (this._frame < this._data.runningFrameStart || this._frame >= this._data.runningFrameStart + this._data.runningFrameCount)
					{
						this._frame = this._data.runningFrameStart + this._data.runningFrameCount - 1;
						return;
					}
				}
				break;
			}
			case 2:
				this._frameCounter += 1f;
				if (this._frameCounter > (float)this._data.inAirFrameDelay)
				{
					this._frameCounter -= (float)this._data.inAirFrameDelay;
					this._frame++;
				}
				if (this._frame < this._data.inAirFrameStart || this._frame >= this._data.inAirFrameStart + this._data.inAirFrameCount)
				{
					this._frame = this._data.inAirFrameStart;
				}
				if (this._type == 4)
				{
					if (velocity.Y < 0f)
					{
						this._frame = 3;
						return;
					}
					this._frame = 6;
					return;
				}
				else if (this._type == 52)
				{
					if (velocity.Y < 0f)
					{
						this._frame = this._data.inAirFrameStart;
					}
					if (this._frame == this._data.inAirFrameStart + this._data.inAirFrameCount - 1)
					{
						this._frameCounter = 0f;
						return;
					}
				}
				else if (this._type == 5)
				{
					float num33 = this._fatigue / this._fatigueMax;
					this._frameExtraCounter += 6f - 4f * num33;
					if (this._frameExtraCounter > (float)this._data.flyingFrameDelay)
					{
						this._frameExtra++;
						this._frameExtraCounter -= (float)this._data.flyingFrameDelay;
					}
					if (this._frameExtra < this._data.flyingFrameStart || this._frameExtra >= this._data.flyingFrameStart + this._data.flyingFrameCount)
					{
						this._frameExtra = this._data.flyingFrameStart;
						return;
					}
				}
				else if (this._type == 23)
				{
					float num34 = mountedPlayer.velocity.Length();
					if (num34 < 1f)
					{
						this._frame = 0;
						this._frameCounter = 0f;
						return;
					}
					if (num34 > 5f)
					{
						this._frameCounter += 1f;
						return;
					}
				}
				break;
			case 3:
				this._frameCounter += 1f;
				if (this._frameCounter > (float)this._data.flyingFrameDelay)
				{
					this._frameCounter -= (float)this._data.flyingFrameDelay;
					this._frame++;
				}
				if (this._frame < this._data.flyingFrameStart || this._frame >= this._data.flyingFrameStart + this._data.flyingFrameCount)
				{
					this._frame = this._data.flyingFrameStart;
					return;
				}
				break;
			case 4:
			{
				int num35 = (int)((Math.Abs(velocity.X) + Math.Abs(velocity.Y)) / 2f);
				this._frameCounter += (float)num35;
				if (this._frameCounter > (float)this._data.swimFrameDelay)
				{
					this._frameCounter -= (float)this._data.swimFrameDelay;
					this._frame++;
				}
				if (this._frame < this._data.swimFrameStart || this._frame >= this._data.swimFrameStart + this._data.swimFrameCount)
				{
					this._frame = this._data.swimFrameStart;
				}
				if (this.Type == 37 && velocity.X == 0f)
				{
					this._frame = 4;
					return;
				}
				break;
			}
			case 5:
			{
				type2 = this._type;
				float num36;
				if (type2 != 6)
				{
					if (type2 != 9)
					{
						if (type2 != 13)
						{
							num36 = Math.Abs(velocity.X);
						}
						else
						{
							num36 = (this._flipDraw ? velocity.X : (0f - velocity.X));
						}
					}
					else
					{
						num36 = ((!this._flipDraw) ? Math.Abs(velocity.X) : (0f - Math.Abs(velocity.X)));
					}
				}
				else
				{
					num36 = (this._flipDraw ? velocity.X : (0f - velocity.X));
				}
				this._frameCounter += num36;
				if (num36 >= 0f)
				{
					if (this._frameCounter > (float)this._data.dashingFrameDelay)
					{
						this._frameCounter -= (float)this._data.dashingFrameDelay;
						this._frame++;
					}
					if (this._frame < this._data.dashingFrameStart || this._frame >= this._data.dashingFrameStart + this._data.dashingFrameCount)
					{
						this._frame = this._data.dashingFrameStart;
						return;
					}
				}
				else
				{
					if (this._frameCounter < 0f)
					{
						this._frameCounter += (float)this._data.dashingFrameDelay;
						this._frame--;
					}
					if (this._frame < this._data.dashingFrameStart || this._frame >= this._data.dashingFrameStart + this._data.dashingFrameCount)
					{
						this._frame = this._data.dashingFrameStart + this._data.dashingFrameCount - 1;
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00153FD4 File Offset: 0x001521D4
		public void TryBeginningFlight(Player mountedPlayer, int state)
		{
			if (this._frameState == state || (state != 2 && state != 3) || !this.CanHover() || mountedPlayer.controlUp || mountedPlayer.controlDown || mountedPlayer.controlJump)
			{
				return;
			}
			Vector2 velocity = Vector2.UnitY * mountedPlayer.gravDir;
			if (Collision.TileCollision(mountedPlayer.position + new Vector2(0f, -0.001f), velocity, mountedPlayer.width, mountedPlayer.height, false, false, (int)mountedPlayer.gravDir).Y != 0f)
			{
				if (this.DoesHoverIgnoresFatigue())
				{
					mountedPlayer.position.Y = mountedPlayer.position.Y + -0.001f;
					return;
				}
				float num = mountedPlayer.gravity * mountedPlayer.gravDir;
				mountedPlayer.position.Y = mountedPlayer.position.Y - mountedPlayer.velocity.Y;
				mountedPlayer.velocity.Y = mountedPlayer.velocity.Y - num;
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x001540BA File Offset: 0x001522BA
		public int GetIntendedGroundedFrame(Player mountedPlayer)
		{
			if (mountedPlayer.velocity.X == 0f || ((mountedPlayer.slippy || mountedPlayer.slippy2 || mountedPlayer.windPushed) && !mountedPlayer.controlLeft && !mountedPlayer.controlRight))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x001540FC File Offset: 0x001522FC
		public void TryLanding(Player mountedPlayer)
		{
			if ((this._frameState == 3 || this._frameState == 2) && !mountedPlayer.controlUp && !mountedPlayer.controlDown && !mountedPlayer.controlJump)
			{
				Vector2 velocity = Vector2.UnitY * mountedPlayer.gravDir * 4f;
				if (Collision.TileCollision(mountedPlayer.position, velocity, mountedPlayer.width, mountedPlayer.height, false, false, (int)mountedPlayer.gravDir).Y == 0f)
				{
					this.UpdateFrame(mountedPlayer, this.GetIntendedGroundedFrame(mountedPlayer), mountedPlayer.velocity);
				}
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00154190 File Offset: 0x00152390
		private void UpdateFrame_GolfCart(Player mountedPlayer, int state, Vector2 velocity)
		{
			if (state != 2)
			{
				if (this._frameExtraCounter != 0f || this._frameExtra != 0)
				{
					if (this._frameExtraCounter == -1f)
					{
						this._frameExtraCounter = 0f;
						this._frameExtra = 1;
					}
					if ((this._frameExtraCounter += 1f) >= 6f)
					{
						this._frameExtraCounter = 0f;
						if (this._frameExtra > 0)
						{
							this._frameExtra--;
						}
					}
				}
				else
				{
					this._frameExtra = 0;
					this._frameExtraCounter = 0f;
				}
			}
			else if (velocity.Y >= 0f)
			{
				if (this._frameExtra < 1)
				{
					this._frameExtra = 1;
				}
				if (this._frameExtra == 2)
				{
					this._frameExtraCounter = -1f;
				}
				else if ((this._frameExtraCounter += 1f) >= 6f)
				{
					this._frameExtraCounter = 0f;
					if (this._frameExtra < 2)
					{
						this._frameExtra++;
					}
				}
			}
			if (state != 2 && state != 0 && state != 3 && state != 4)
			{
				Mount.EmitGolfCartWheelDust(mountedPlayer, mountedPlayer.Bottom + new Vector2((float)(mountedPlayer.direction * -20), 0f));
				Mount.EmitGolfCartWheelDust(mountedPlayer, mountedPlayer.Bottom + new Vector2((float)(mountedPlayer.direction * 20), 0f));
			}
			Mount.EmitGolfCartlight(mountedPlayer.Bottom + new Vector2((float)(mountedPlayer.direction * 40), -20f), mountedPlayer.direction);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0015432C File Offset: 0x0015252C
		private static void EmitGolfCartSmoke(Player mountedPlayer, bool rushing)
		{
			Vector2 position = mountedPlayer.Bottom + new Vector2((float)(-(float)mountedPlayer.direction * 34), (0f - mountedPlayer.gravDir) * 12f);
			Dust dust = Dust.NewDustDirect(position, 0, 0, 31, (float)(-(float)mountedPlayer.direction), (0f - mountedPlayer.gravDir) * 0.24f, 100, default(Color), 1f);
			dust.position = position;
			dust.velocity *= 0.1f;
			dust.velocity += new Vector2((float)(-(float)mountedPlayer.direction), (0f - mountedPlayer.gravDir) * 0.25f);
			dust.scale = 0.5f;
			if (mountedPlayer.velocity.X != 0f)
			{
				dust.velocity += new Vector2((float)Math.Sign(mountedPlayer.velocity.X) * 1.3f, 0f);
			}
			if (rushing)
			{
				dust.fadeIn = 0.8f;
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00154448 File Offset: 0x00152648
		private static void EmitGolfCartlight(Vector2 worldLocation, int playerDirection)
		{
			float num = 0f;
			if (playerDirection == -1)
			{
				num = 3.1415927f;
			}
			float num2 = 0.09817477f;
			int num3 = 5;
			float num4 = 200f;
			DelegateMethods.v2_1 = worldLocation.ToTileCoordinates().ToVector2();
			DelegateMethods.f_1 = num4 / 16f;
			DelegateMethods.v3_1 = new Vector3(0.7f, 0.7f, 0.7f);
			for (float num5 = 0f; num5 < (float)num3; num5 += 1f)
			{
				Vector2 vector = (num + num2 * (num5 - (float)(num3 / 2))).ToRotationVector2();
				Vector2 end = worldLocation + vector * num4;
				float width = 8f;
				Utils.TileActionAttempt plot;
				if ((plot = Mount.<>O.<12>__CastLightOpen_StopForSolids_ScaleWithDistance) == null)
				{
					plot = (Mount.<>O.<12>__CastLightOpen_StopForSolids_ScaleWithDistance = new Utils.TileActionAttempt(DelegateMethods.CastLightOpen_StopForSolids_ScaleWithDistance));
				}
				Utils.PlotTileLine(worldLocation, end, width, plot);
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00154508 File Offset: 0x00152708
		private static bool ShouldGolfCartEmitLight()
		{
			return true;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0015450C File Offset: 0x0015270C
		private static void EmitGolfCartWheelDust(Player mountedPlayer, Vector2 legSpot)
		{
			if (Main.rand.Next(5) != 0)
			{
				return;
			}
			Point p = (legSpot + new Vector2(0f, mountedPlayer.gravDir * 2f)).ToTileCoordinates();
			if (!WorldGen.InWorld(p.X, p.Y, 10))
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(p.X, p.Y);
			if (WorldGen.SolidTile(p))
			{
				int num = WorldGen.KillTile_GetTileDustAmount(true, tileSafely, p.X, p.Y);
				if (num > 1)
				{
					num = 1;
				}
				Vector2 vector;
				vector..ctor((float)(-(float)mountedPlayer.direction), (0f - mountedPlayer.gravDir) * 1f);
				for (int i = 0; i < num; i++)
				{
					Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(p.X, p.Y, tileSafely)];
					dust.velocity *= 0.2f;
					dust.velocity += vector;
					dust.position = legSpot;
					dust.scale *= 0.8f;
					dust.fadeIn *= 0.8f;
				}
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00154634 File Offset: 0x00152834
		private void DoGemMinecartEffect(Player mountedPlayer, int dustType)
		{
			if (Main.rand.Next(10) == 0)
			{
				Vector2 vector = Main.rand.NextVector2Square(-1f, 1f) * new Vector2(22f, 10f);
				Vector2 vector2 = new Vector2(0f, 10f) * mountedPlayer.Directions;
				Vector2 pos = mountedPlayer.Center + vector2 + vector;
				pos = mountedPlayer.RotatedRelativePoint(pos, false, true);
				Dust dust = Dust.NewDustPerfect(pos, dustType, null, 0, default(Color), 1f);
				dust.noGravity = true;
				dust.fadeIn = 0.6f;
				dust.scale = 0.4f;
				dust.velocity *= 0.25f;
				dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0015471C File Offset: 0x0015291C
		private void DoSteamMinecartEffect(Player mountedPlayer, int dustType)
		{
			float num = Math.Abs(mountedPlayer.velocity.X);
			if (num >= 1f && (num >= 6f || this._frame == 0))
			{
				Vector2 vector = Main.rand.NextVector2Square(-1f, 1f) * new Vector2(3f, 3f);
				Vector2 vector2 = new Vector2(-10f, -4f) * mountedPlayer.Directions;
				Vector2 pos = mountedPlayer.Center + vector2 + vector;
				pos = mountedPlayer.RotatedRelativePoint(pos, false, true);
				Dust dust = Dust.NewDustPerfect(pos, dustType, null, 0, default(Color), 1f);
				dust.noGravity = true;
				dust.fadeIn = 0.6f;
				dust.scale = 1.8f;
				dust.velocity *= 0.25f;
				dust.velocity.Y = dust.velocity.Y - 2f;
				dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00154838 File Offset: 0x00152A38
		private void DoExhaustMinecartEffect(Player mountedPlayer, int dustType)
		{
			float num = mountedPlayer.velocity.Length();
			if (num < 1f && Main.rand.Next(4) != 0)
			{
				return;
			}
			int num2 = 1 + (int)num / 6;
			while (num2 > 0)
			{
				num2--;
				Vector2 vector = Main.rand.NextVector2Square(-1f, 1f) * new Vector2(3f, 3f);
				Vector2 vector2 = new Vector2(-18f, 20f) * mountedPlayer.Directions;
				if (num > 6f)
				{
					vector2.X += (float)(4 * mountedPlayer.direction);
				}
				if (num2 > 0)
				{
					vector2 += mountedPlayer.velocity * (float)(num2 / 3);
				}
				Vector2 pos = mountedPlayer.Center + vector2 + vector;
				pos = mountedPlayer.RotatedRelativePoint(pos, false, true);
				Dust dust = Dust.NewDustPerfect(pos, dustType, null, 0, default(Color), 1f);
				dust.noGravity = true;
				dust.fadeIn = 0.6f;
				dust.scale = 1.2f;
				dust.velocity *= 0.2f;
				if (num < 1f)
				{
					Dust dust2 = dust;
					dust2.velocity.X = dust2.velocity.X - 0.5f * (float)mountedPlayer.direction;
				}
				dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x001549B0 File Offset: 0x00152BB0
		private void DoConfettiMinecartEffect(Player mountedPlayer)
		{
			float num = mountedPlayer.velocity.Length();
			if ((num < 1f && Main.rand.Next(6) != 0) || (num < 3f && Main.rand.Next(3) != 0))
			{
				return;
			}
			int num2 = 1 + (int)num / 6;
			while (num2 > 0)
			{
				num2--;
				float num3 = Main.rand.NextFloat() * 2f;
				Vector2 vector = Main.rand.NextVector2Square(-1f, 1f) * new Vector2(3f, 8f);
				Vector2 vector2 = new Vector2(-18f, 4f) * mountedPlayer.Directions;
				vector2.X += num * (float)mountedPlayer.direction * 0.5f + (float)(mountedPlayer.direction * num2) * num3;
				if (num2 > 0)
				{
					vector2 += mountedPlayer.velocity * (float)(num2 / 3);
				}
				Vector2 pos = mountedPlayer.Center + vector2 + vector;
				pos = mountedPlayer.RotatedRelativePoint(pos, false, true);
				Dust dust = Dust.NewDustPerfect(pos, 139 + Main.rand.Next(4), null, 0, default(Color), 1f);
				dust.noGravity = true;
				dust.fadeIn = 0.6f;
				dust.scale = 0.5f + num3 / 2f;
				dust.velocity *= 0.2f;
				if (num < 1f)
				{
					Dust dust2 = dust;
					dust2.velocity.X = dust2.velocity.X - 0.5f * (float)mountedPlayer.direction;
				}
				dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00154B74 File Offset: 0x00152D74
		public unsafe void UpdateEffects(Player mountedPlayer)
		{
			mountedPlayer.autoJump = this.AutoJump;
			this._shouldSuperCart = (MountID.Sets.Cart[this._type] && mountedPlayer.UsingSuperCart);
			MountLoader.UpdateEffects(mountedPlayer);
			if (this._shouldSuperCart)
			{
				this.CastSuperCartLaser(mountedPlayer);
				float num = 1f + Math.Abs(mountedPlayer.velocity.X) / this.RunSpeed * 2.5f;
				mountedPlayer.statDefense += (int)(2f * num);
			}
			switch (this._type)
			{
			case 8:
				if (mountedPlayer.ownedProjectileCounts[453] < 1)
				{
					this._abilityActive = false;
					return;
				}
				break;
			case 9:
			case 46:
			{
				if (this._type == 46)
				{
					mountedPlayer.hasJumpOption_Santank = true;
				}
				Vector2 center = mountedPlayer.Center;
				Vector2 vector10 = center;
				bool flag = false;
				float num2 = 1500f;
				float num3 = 850f;
				for (int i = 0; i < 200; i++)
				{
					NPC nPC = Main.npc[i];
					if (nPC.CanBeChasedBy(this, false))
					{
						Vector2 v = nPC.Center - center;
						float num4 = v.Length();
						if (num4 < num3 && ((Vector2.Distance(vector10, center) > num4 && num4 < num2) || !flag))
						{
							bool flag2 = true;
							float num5 = Math.Abs(v.ToRotation());
							if (mountedPlayer.direction == 1 && (double)num5 > 1.047197594907988)
							{
								flag2 = false;
							}
							else if (mountedPlayer.direction == -1 && (double)num5 < 2.0943951461045853)
							{
								flag2 = false;
							}
							if (Collision.CanHitLine(center, 0, 0, nPC.position, nPC.width, nPC.height) && flag2)
							{
								num2 = num4;
								vector10 = nPC.Center;
								flag = true;
							}
						}
					}
				}
				if (!flag)
				{
					this._abilityCharging = false;
					this.ResetHeadPosition();
					return;
				}
				bool flag3 = this._abilityCooldown == 0;
				if (this._type == 46)
				{
					flag3 = (this._abilityCooldown % 10 == 0);
				}
				if (flag3 && mountedPlayer.whoAmI == Main.myPlayer)
				{
					this.AimAbility(mountedPlayer, vector10);
					if (this._abilityCooldown == 0)
					{
						this.StopAbilityCharge();
					}
					this.UseAbility(mountedPlayer, vector10, false);
					return;
				}
				this.AimAbility(mountedPlayer, vector10);
				this._abilityCharging = true;
				return;
			}
			case 10:
				mountedPlayer.hasJumpOption_Unicorn = true;
				if (Math.Abs(mountedPlayer.velocity.X) > mountedPlayer.mount.DashSpeed - mountedPlayer.mount.RunSpeed / 2f)
				{
					mountedPlayer.noKnockback = true;
				}
				if (mountedPlayer.itemAnimation > 0 && mountedPlayer.inventory[mountedPlayer.selectedItem].type == 1260)
				{
					AchievementsHelper.HandleSpecialEvent(mountedPlayer, 5);
					return;
				}
				break;
			case 11:
			{
				Vector3 vector11;
				vector11..ctor(0.4f, 0.12f, 0.15f);
				float num6 = 1f + Math.Abs(mountedPlayer.velocity.X) / this.RunSpeed * 2.5f;
				int num7 = Math.Sign(mountedPlayer.velocity.X);
				if (num7 == 0)
				{
					num7 = mountedPlayer.direction;
				}
				if (Main.netMode != 2)
				{
					vector11 *= num6;
					Lighting.AddLight(mountedPlayer.Center, vector11.X, vector11.Y, vector11.Z);
					Lighting.AddLight(mountedPlayer.Top, vector11.X, vector11.Y, vector11.Z);
					Lighting.AddLight(mountedPlayer.Bottom, vector11.X, vector11.Y, vector11.Z);
					Lighting.AddLight(mountedPlayer.Left, vector11.X, vector11.Y, vector11.Z);
					Lighting.AddLight(mountedPlayer.Right, vector11.X, vector11.Y, vector11.Z);
					float num8 = -24f;
					if (mountedPlayer.direction != num7)
					{
						num8 = -22f;
					}
					if (num7 == -1)
					{
						num8 += 1f;
					}
					Vector2 vector12 = new Vector2(num8 * (float)num7, -19f).RotatedBy((double)mountedPlayer.fullRotation, default(Vector2));
					Vector2 vector13 = new Vector2(MathHelper.Lerp(0f, -8f, mountedPlayer.fullRotation / 0.7853982f), MathHelper.Lerp(0f, 2f, Math.Abs(mountedPlayer.fullRotation / 0.7853982f))).RotatedBy((double)mountedPlayer.fullRotation, default(Vector2));
					if (num7 == Math.Sign(mountedPlayer.fullRotation))
					{
						vector13 *= MathHelper.Lerp(1f, 0.6f, Math.Abs(mountedPlayer.fullRotation / 0.7853982f));
					}
					Vector2 vector14 = mountedPlayer.Bottom + vector12 + vector13;
					Vector2 vector15 = mountedPlayer.oldPosition + mountedPlayer.Size * new Vector2(0.5f, 1f) + vector12 + vector13;
					if (Vector2.Distance(vector14, vector15) > 3f)
					{
						int num9 = (int)Vector2.Distance(vector14, vector15) / 3;
						if (Vector2.Distance(vector14, vector15) % 3f != 0f)
						{
							num9++;
						}
						for (float num10 = 1f; num10 <= (float)num9; num10 += 1f)
						{
							Dust dust4 = Main.dust[Dust.NewDust(mountedPlayer.Center, 0, 0, 182, 0f, 0f, 0, default(Color), 1f)];
							dust4.position = Vector2.Lerp(vector15, vector14, num10 / (float)num9);
							dust4.noGravity = true;
							dust4.velocity = Vector2.Zero;
							dust4.customData = mountedPlayer;
							dust4.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
						}
						return;
					}
					Dust dust5 = Main.dust[Dust.NewDust(mountedPlayer.Center, 0, 0, 182, 0f, 0f, 0, default(Color), 1f)];
					dust5.position = vector14;
					dust5.noGravity = true;
					dust5.velocity = Vector2.Zero;
					dust5.customData = mountedPlayer;
					dust5.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
					return;
				}
				break;
			}
			case 12:
				if (mountedPlayer.MountFishronSpecial)
				{
					Vector3 vector16 = Colors.CurrentLiquidColor.ToVector3();
					vector16 *= 0.4f;
					Point point = (mountedPlayer.Center + Vector2.UnitX * (float)mountedPlayer.direction * 20f + mountedPlayer.velocity * 10f).ToTileCoordinates();
					if (!WorldGen.SolidTile(point.X, point.Y, false))
					{
						Lighting.AddLight(point.X, point.Y, vector16.X, vector16.Y, vector16.Z);
					}
					else
					{
						Lighting.AddLight(mountedPlayer.Center + Vector2.UnitX * (float)mountedPlayer.direction * 20f, vector16.X, vector16.Y, vector16.Z);
					}
					*mountedPlayer.allDamage += 0.15f;
				}
				if (mountedPlayer.statLife <= mountedPlayer.statLifeMax2 / 2)
				{
					mountedPlayer.MountFishronSpecialCounter = 60f;
				}
				if (mountedPlayer.wet || (Main.raining && WorldGen.InAPlaceWithWind(mountedPlayer.position, mountedPlayer.width, mountedPlayer.height)))
				{
					mountedPlayer.MountFishronSpecialCounter = 420f;
					return;
				}
				break;
			case 13:
			case 15:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 33:
			case 35:
			case 38:
			case 39:
			case 43:
			case 44:
			case 45:
				break;
			case 14:
				mountedPlayer.hasJumpOption_Basilisk = true;
				if (Math.Abs(mountedPlayer.velocity.X) > mountedPlayer.mount.DashSpeed - mountedPlayer.mount.RunSpeed / 2f)
				{
					mountedPlayer.noKnockback = true;
					return;
				}
				break;
			case 16:
				mountedPlayer.ignoreWater = true;
				return;
			case 22:
			{
				mountedPlayer.lavaMax += 420;
				Vector2 vector17 = mountedPlayer.Center + new Vector2(20f, 10f) * mountedPlayer.Directions;
				Vector2 pos = vector17 + mountedPlayer.velocity;
				Vector2 pos2 = vector17 + new Vector2(-1f, -0.5f) * mountedPlayer.Directions;
				vector17 = mountedPlayer.RotatedRelativePoint(vector17, false, true);
				pos = mountedPlayer.RotatedRelativePoint(pos, false, true);
				pos2 = mountedPlayer.RotatedRelativePoint(pos2, false, true);
				Vector2 value = mountedPlayer.shadowPos[2] - mountedPlayer.position + vector17;
				Vector2 vector18 = pos - vector17;
				vector17 += vector18;
				value += vector18;
				Vector2 vector19 = pos - pos2;
				float num11 = MathHelper.Clamp(mountedPlayer.velocity.Length() / 5f, 0f, 1f);
				for (float num12 = 0f; num12 <= 1f; num12 += 0.1f)
				{
					if (Main.rand.NextFloat() >= num11)
					{
						Dust dust = Dust.NewDustPerfect(Vector2.Lerp(value, vector17, num12), 65, new Vector2?(Main.rand.NextVector2Circular(0.5f, 0.5f) * num11), 0, default(Color), 1f);
						dust.scale = 0.6f;
						dust.fadeIn = 0f;
						dust.customData = mountedPlayer;
						dust.velocity *= -1f;
						dust.noGravity = true;
						dust.velocity -= vector19;
						dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
						if (Main.rand.Next(10) == 0)
						{
							dust.fadeIn = 1.3f;
							dust.velocity = Main.rand.NextVector2Circular(3f, 3f) * num11;
						}
					}
				}
				return;
			}
			case 23:
			{
				Vector2 pos3 = mountedPlayer.Center + this.GetWitchBroomTrinketOriginOffset(mountedPlayer) + (this.GetWitchBroomTrinketRotation(mountedPlayer) + 1.5707964f).ToRotationVector2() * 11f;
				Vector3 rgb = new Vector3(1f, 0.75f, 0.5f) * 0.85f;
				Vector2 vector20 = mountedPlayer.RotatedRelativePoint(pos3, false, true);
				Lighting.AddLight(vector20, rgb);
				if (Main.rand.Next(45) == 0)
				{
					Vector2 vector21 = Main.rand.NextVector2Circular(4f, 4f);
					Dust dust2 = Dust.NewDustPerfect(vector20 + vector21, 43, new Vector2?(Vector2.Zero), 254, new Color(255, 255, 0, 255), 0.3f);
					if (vector21 != Vector2.Zero)
					{
						dust2.velocity = vector20.DirectionTo(dust2.position) * 0.2f;
					}
					dust2.fadeIn = 0.3f;
					dust2.noLightEmittence = true;
					dust2.customData = mountedPlayer;
					dust2.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
				}
				float num13 = 0.1f;
				num13 += mountedPlayer.velocity.Length() / 30f;
				Vector2 pos4 = mountedPlayer.Center + new Vector2(18f - 20f * Main.rand.NextFloat() * (float)mountedPlayer.direction, 12f);
				Vector2 pos5 = mountedPlayer.Center + new Vector2((float)(52 * mountedPlayer.direction), -6f);
				pos4 = mountedPlayer.RotatedRelativePoint(pos4, false, true);
				pos5 = mountedPlayer.RotatedRelativePoint(pos5, false, true);
				if (Main.rand.NextFloat() <= num13)
				{
					float num14 = Main.rand.NextFloat();
					for (float num15 = 0f; num15 < 1f; num15 += 0.125f)
					{
						if (Main.rand.Next(15) == 0)
						{
							Vector2 spinningpoint = (6.2831855f * num15 + num14).ToRotationVector2() * new Vector2(0.5f, 1f) * 4f;
							spinningpoint = spinningpoint.RotatedBy((double)mountedPlayer.fullRotation, default(Vector2));
							Dust dust3 = Dust.NewDustPerfect(pos4 + spinningpoint, 43, new Vector2?(Vector2.Zero), 254, new Color(255, 255, 0, 255), 0.3f);
							dust3.velocity = spinningpoint * 0.025f + pos5.DirectionTo(dust3.position) * 0.5f;
							dust3.fadeIn = 0.3f;
							dust3.noLightEmittence = true;
							dust3.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
						}
					}
					return;
				}
				break;
			}
			case 24:
			{
				DelegateMethods.v3_1 = new Vector3(0.1f, 0.3f, 1f) * 0.4f;
				Vector2 mountedCenter = mountedPlayer.MountedCenter;
				Vector2 end = mountedPlayer.MountedCenter + mountedPlayer.velocity * 6f;
				float width = 40f;
				Utils.TileActionAttempt plot;
				if ((plot = Mount.<>O.<13>__CastLightOpen) == null)
				{
					plot = (Mount.<>O.<13>__CastLightOpen = new Utils.TileActionAttempt(DelegateMethods.CastLightOpen));
				}
				Utils.PlotTileLine(mountedCenter, end, width, plot);
				Vector2 left = mountedPlayer.Left;
				Vector2 right = mountedPlayer.Right;
				float width2 = 40f;
				Utils.TileActionAttempt plot2;
				if ((plot2 = Mount.<>O.<13>__CastLightOpen) == null)
				{
					plot2 = (Mount.<>O.<13>__CastLightOpen = new Utils.TileActionAttempt(DelegateMethods.CastLightOpen));
				}
				Utils.PlotTileLine(left, right, width2, plot2);
				return;
			}
			case 25:
				this.DoGemMinecartEffect(mountedPlayer, 86);
				return;
			case 26:
				this.DoGemMinecartEffect(mountedPlayer, 87);
				return;
			case 27:
				this.DoGemMinecartEffect(mountedPlayer, 88);
				return;
			case 28:
				this.DoGemMinecartEffect(mountedPlayer, 89);
				return;
			case 29:
				this.DoGemMinecartEffect(mountedPlayer, 90);
				return;
			case 30:
				this.DoGemMinecartEffect(mountedPlayer, 91);
				return;
			case 31:
				this.DoGemMinecartEffect(mountedPlayer, 262);
				return;
			case 32:
				this.DoExhaustMinecartEffect(mountedPlayer, 31);
				return;
			case 34:
				this.DoConfettiMinecartEffect(mountedPlayer);
				return;
			case 36:
				this.DoSteamMinecartEffect(mountedPlayer, 303);
				return;
			case 37:
				mountedPlayer.canFloatInWater = true;
				mountedPlayer.accFlipper = true;
				break;
			case 40:
			case 41:
			case 42:
				if (Math.Abs(mountedPlayer.velocity.X) > mountedPlayer.mount.DashSpeed - mountedPlayer.mount.RunSpeed / 2f)
				{
					mountedPlayer.noKnockback = true;
					return;
				}
				break;
			case 47:
				mountedPlayer.hasJumpOption_WallOfFleshGoat = true;
				if (Math.Abs(mountedPlayer.velocity.X) > mountedPlayer.mount.DashSpeed - mountedPlayer.mount.RunSpeed / 2f)
				{
					mountedPlayer.noKnockback = true;
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00155A6C File Offset: 0x00153C6C
		private void CastSuperCartLaser(Player mountedPlayer)
		{
			int num = Math.Sign(mountedPlayer.velocity.X);
			if (num == 0)
			{
				num = mountedPlayer.direction;
			}
			if (mountedPlayer.whoAmI != Main.myPlayer || mountedPlayer.velocity.X == 0f)
			{
				return;
			}
			Vector2 minecartMechPoint = Mount.GetMinecartMechPoint(mountedPlayer, 20, -19);
			int damage = 60;
			int num2 = 0;
			float num3 = 0f;
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.active && nPC.immune[mountedPlayer.whoAmI] <= 0 && !nPC.dontTakeDamage && nPC.Distance(minecartMechPoint) < 300f && nPC.CanBeChasedBy(mountedPlayer, false) && Collision.CanHitLine(nPC.position, nPC.width, nPC.height, minecartMechPoint, 0, 0) && Math.Abs(MathHelper.WrapAngle(MathHelper.WrapAngle(nPC.AngleFrom(minecartMechPoint)) - MathHelper.WrapAngle((mountedPlayer.fullRotation + (float)num == -1f) ? 3.1415927f : 0f))) < 0.7853982f)
				{
					minecartMechPoint = Mount.GetMinecartMechPoint(mountedPlayer, -20, -39);
					Vector2 v = nPC.position + nPC.Size * Utils.RandomVector2(Main.rand, 0f, 1f) - minecartMechPoint;
					num3 += v.ToRotation();
					num2++;
					int num4 = Projectile.NewProjectile(this.GetProjectileSpawnSource(mountedPlayer), minecartMechPoint.X, minecartMechPoint.Y, v.X, v.Y, 591, 0, 0f, mountedPlayer.whoAmI, (float)mountedPlayer.whoAmI, 0f, 0f);
					Main.projectile[num4].Center = nPC.Center;
					Main.projectile[num4].damage = damage;
					Main.projectile[num4].Damage();
					Main.projectile[num4].damage = 0;
					Main.projectile[num4].Center = minecartMechPoint;
				}
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00155C84 File Offset: 0x00153E84
		public static Vector2 GetMinecartMechPoint(Player mountedPlayer, int offX, int offY)
		{
			int num = Math.Sign(mountedPlayer.velocity.X);
			if (num == 0)
			{
				num = mountedPlayer.direction;
			}
			float num2 = (float)offX;
			int num3 = Math.Sign(offX);
			if (mountedPlayer.direction != num)
			{
				num2 -= (float)num3;
			}
			if (num == -1)
			{
				num2 -= (float)num3;
			}
			Vector2 vector = new Vector2(num2 * (float)num, (float)offY).RotatedBy((double)mountedPlayer.fullRotation, default(Vector2));
			Vector2 vector2 = new Vector2(MathHelper.Lerp(0f, -8f, mountedPlayer.fullRotation / 0.7853982f), MathHelper.Lerp(0f, 2f, Math.Abs(mountedPlayer.fullRotation / 0.7853982f))).RotatedBy((double)mountedPlayer.fullRotation, default(Vector2));
			if (num == Math.Sign(mountedPlayer.fullRotation))
			{
				vector2 *= MathHelper.Lerp(1f, 0.6f, Math.Abs(mountedPlayer.fullRotation / 0.7853982f));
			}
			return mountedPlayer.Bottom + vector + vector2;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00155D91 File Offset: 0x00153F91
		public void ResetFlightTime(float xVelocity)
		{
			this._flyTime = (this._active ? this._data.flightTimeMax : 0);
			if (this._type == 0)
			{
				this._flyTime += (int)(Math.Abs(xVelocity) * 20f);
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00155DD4 File Offset: 0x00153FD4
		public void CheckMountBuff(Player mountedPlayer)
		{
			if (this._type == -1)
			{
				return;
			}
			for (int i = 0; i < Player.maxBuffs; i++)
			{
				if (mountedPlayer.buffType[i] == this._data.buff || (this.Cart && mountedPlayer.buffType[i] == this._data.extraBuff))
				{
					return;
				}
			}
			this.Dismount(mountedPlayer);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00155E35 File Offset: 0x00154035
		public void ResetHeadPosition()
		{
			if (this._aiming)
			{
				this._aiming = false;
				if (this._type != 46)
				{
					this._frameExtra = 0;
				}
				this._flipDraw = false;
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00155E60 File Offset: 0x00154060
		private Vector2 ClampToDeadZone(Player mountedPlayer, Vector2 position)
		{
			int type = this._type;
			int num;
			int num2;
			if (type != 8)
			{
				if (type != 9)
				{
					if (type != 46)
					{
						return position;
					}
					num = (int)Mount.santankTextureSize.Y;
					num2 = (int)Mount.santankTextureSize.X;
				}
				else
				{
					num = (int)Mount.scutlixTextureSize.Y;
					num2 = (int)Mount.scutlixTextureSize.X;
				}
			}
			else
			{
				num = (int)Mount.drillTextureSize.Y;
				num2 = (int)Mount.drillTextureSize.X;
			}
			Vector2 center = mountedPlayer.Center;
			position -= center;
			if (position.X > (float)(-(float)num2) && position.X < (float)num2 && position.Y > (float)(-(float)num) && position.Y < (float)num)
			{
				float num3 = (float)num2 / Math.Abs(position.X);
				float num4 = (float)num / Math.Abs(position.Y);
				if (num3 > num4)
				{
					position *= num4;
				}
				else
				{
					position *= num3;
				}
			}
			return position + center;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00155F54 File Offset: 0x00154154
		public bool AimAbility(Player mountedPlayer, Vector2 mousePosition)
		{
			this._aiming = true;
			MountLoader.AimAbility(this, mountedPlayer, mousePosition);
			int type = this._type;
			if (type == 8)
			{
				Vector2 v = this.ClampToDeadZone(mountedPlayer, mousePosition) - mountedPlayer.Center;
				Mount.DrillMountData drillMountData = (Mount.DrillMountData)this._mountSpecificData;
				float num = v.ToRotation();
				if (num < 0f)
				{
					num += 6.2831855f;
				}
				drillMountData.diodeRotationTarget = num;
				float num2 = drillMountData.diodeRotation % 6.2831855f;
				if (num2 < 0f)
				{
					num2 += 6.2831855f;
				}
				if (num2 < num)
				{
					if (num - num2 > 3.1415927f)
					{
						num2 += 6.2831855f;
					}
				}
				else if (num2 - num > 3.1415927f)
				{
					num2 -= 6.2831855f;
				}
				drillMountData.diodeRotation = num2;
				drillMountData.crosshairPosition = mousePosition;
				return true;
			}
			if (type == 9)
			{
				int frameExtra = this._frameExtra;
				int direction = mountedPlayer.direction;
				float num3 = MathHelper.ToDegrees((this.ClampToDeadZone(mountedPlayer, mousePosition) - mountedPlayer.Center).ToRotation());
				if (num3 > 90f)
				{
					mountedPlayer.direction = -1;
					num3 = 180f - num3;
				}
				else if (num3 < -90f)
				{
					mountedPlayer.direction = -1;
					num3 = -180f - num3;
				}
				else
				{
					mountedPlayer.direction = 1;
				}
				if ((mountedPlayer.direction > 0 && mountedPlayer.velocity.X < 0f) || (mountedPlayer.direction < 0 && mountedPlayer.velocity.X > 0f))
				{
					this._flipDraw = true;
				}
				else
				{
					this._flipDraw = false;
				}
				if (num3 >= 0f)
				{
					if ((double)num3 < 22.5)
					{
						this._frameExtra = 8;
					}
					else if ((double)num3 < 67.5)
					{
						this._frameExtra = 9;
					}
					else if ((double)num3 < 112.5)
					{
						this._frameExtra = 10;
					}
				}
				else if ((double)num3 > -22.5)
				{
					this._frameExtra = 8;
				}
				else if ((double)num3 > -67.5)
				{
					this._frameExtra = 7;
				}
				else if ((double)num3 > -112.5)
				{
					this._frameExtra = 6;
				}
				float abilityCharge = this.AbilityCharge;
				if (abilityCharge > 0f)
				{
					Vector2 vector = default(Vector2);
					vector.X = mountedPlayer.position.X + (float)(mountedPlayer.width / 2);
					vector.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
					int num4 = (this._frameExtra - 6) * 2;
					Vector2 vector2 = default(Vector2);
					for (int i = 0; i < 2; i++)
					{
						vector2.Y = vector.Y + Mount.scutlixEyePositions[num4 + i].Y;
						if (mountedPlayer.direction == -1)
						{
							vector2.X = vector.X - Mount.scutlixEyePositions[num4 + i].X - (float)this._data.xOffset;
						}
						else
						{
							vector2.X = vector.X + Mount.scutlixEyePositions[num4 + i].X + (float)this._data.xOffset;
						}
						Lighting.AddLight((int)(vector2.X / 16f), (int)(vector2.Y / 16f), 1f * abilityCharge, 0f, 0f);
					}
				}
				return this._frameExtra != frameExtra || mountedPlayer.direction != direction;
			}
			if (type != 46)
			{
				return false;
			}
			int frameExtra2 = this._frameExtra;
			int direction2 = mountedPlayer.direction;
			float num5 = MathHelper.ToDegrees((this.ClampToDeadZone(mountedPlayer, mousePosition) - mountedPlayer.Center).ToRotation());
			if (num5 > 90f)
			{
				mountedPlayer.direction = -1;
				num5 = 180f - num5;
			}
			else if (num5 < -90f)
			{
				mountedPlayer.direction = -1;
				num5 = -180f - num5;
			}
			else
			{
				mountedPlayer.direction = 1;
			}
			if ((mountedPlayer.direction > 0 && mountedPlayer.velocity.X < 0f) || (mountedPlayer.direction < 0 && mountedPlayer.velocity.X > 0f))
			{
				this._flipDraw = true;
			}
			else
			{
				this._flipDraw = false;
			}
			if (this.AbilityCharge > 0f)
			{
				Vector2 vector3 = default(Vector2);
				vector3.X = mountedPlayer.position.X + (float)(mountedPlayer.width / 2);
				vector3.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
				for (int j = 0; j < 2; j++)
				{
					Vector2 vector4;
					vector4..ctor(vector3.X + (float)(mountedPlayer.width * mountedPlayer.direction), vector3.Y - 12f);
					Lighting.AddLight((int)(vector4.X / 16f), (int)(vector4.Y / 16f), 0.7f, 0.4f, 0.4f);
				}
			}
			return this._frameExtra != frameExtra2 || mountedPlayer.direction != direction2;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00156450 File Offset: 0x00154650
		public void Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, Vector2 Position, Color drawColor, SpriteEffects playerEffect, float shadow)
		{
			if (playerDrawData == null)
			{
				return;
			}
			Texture2D texture2D;
			Texture2D texture2D2;
			switch (drawType)
			{
			case 0:
				texture2D = this._data.backTexture.Value;
				texture2D2 = this._data.backTextureGlow.Value;
				break;
			case 1:
				texture2D = this._data.backTextureExtra.Value;
				texture2D2 = this._data.backTextureExtraGlow.Value;
				break;
			case 2:
				if (this._type == 0 && this._idleTime >= this._idleTimeNext)
				{
					return;
				}
				texture2D = this._data.frontTexture.Value;
				texture2D2 = this._data.frontTextureGlow.Value;
				break;
			case 3:
				texture2D = this._data.frontTextureExtra.Value;
				texture2D2 = this._data.frontTextureExtraGlow.Value;
				break;
			default:
				texture2D = null;
				texture2D2 = null;
				break;
			}
			int type = this._type;
			if (type == 50 && texture2D != null && texture2D != Asset<Texture2D>.DefaultValue)
			{
				PlayerQueenSlimeMountTextureContent queenSlimeMount = TextureAssets.RenderTargets.QueenSlimeMount;
				queenSlimeMount.Request();
				if (queenSlimeMount.IsReady)
				{
					texture2D = queenSlimeMount.GetTarget();
				}
			}
			if (texture2D == null)
			{
				return;
			}
			type = this._type;
			if ((type == 0 || type == 9) && drawType == 3 && shadow != 0f)
			{
				return;
			}
			int num = this.XOffset;
			int num2 = this.YOffset + this.PlayerOffset;
			if (drawPlayer.direction <= 0 && (!this.Cart || !this.Directional))
			{
				num *= -1;
			}
			Position.X = (float)((int)(Position.X - Main.screenPosition.X + (float)(drawPlayer.width / 2) + (float)num));
			Position.Y = (float)((int)(Position.Y - Main.screenPosition.Y + (float)(drawPlayer.height / 2) + (float)num2));
			bool flag = true;
			int num3 = this._data.totalFrames;
			int num4 = this._data.textureHeight;
			int type2 = this._type;
			int num5;
			if (type2 <= 17)
			{
				if (type2 != 5)
				{
					if (type2 == 9)
					{
						switch (drawType)
						{
						case 0:
							num5 = this._frame;
							goto IL_3D0;
						case 2:
							num5 = this._frameExtra;
							goto IL_3D0;
						case 3:
							num5 = this._frameExtra;
							goto IL_3D0;
						}
						num5 = 0;
						goto IL_3D0;
					}
					if (type2 == 17)
					{
						num4 = texture2D.Height;
						if (drawType == 0)
						{
							num5 = this._frame;
							num3 = 4;
							goto IL_3D0;
						}
						if (drawType != 1)
						{
							num5 = 0;
							goto IL_3D0;
						}
						num5 = this._frameExtra;
						num3 = 4;
						goto IL_3D0;
					}
				}
				else
				{
					if (drawType == 0)
					{
						num5 = this._frame;
						goto IL_3D0;
					}
					if (drawType != 1)
					{
						num5 = 0;
						goto IL_3D0;
					}
					num5 = this._frameExtra;
					goto IL_3D0;
				}
			}
			else if (type2 <= 39)
			{
				if (type2 == 23)
				{
					num5 = this._frame;
					goto IL_3D0;
				}
				if (type2 == 39)
				{
					num4 = texture2D.Height;
					if (drawType == 2)
					{
						num5 = this._frame;
						num3 = 3;
						goto IL_3D0;
					}
					if (drawType != 3)
					{
						num5 = 0;
						goto IL_3D0;
					}
					num5 = this._frameExtra;
					num3 = 6;
					goto IL_3D0;
				}
			}
			else if (type2 != 46)
			{
				if (type2 == 52)
				{
					if (drawType != 3)
					{
						num5 = this._frame;
						goto IL_3D0;
					}
					if (drawPlayer.itemAnimation <= 0)
					{
						int holdStyle = drawPlayer.lastVisualizedSelectedItem.holdStyle;
						num5 = this._frame;
						goto IL_3D0;
					}
					Rectangle bodyFrame = drawPlayer.bodyFrame;
					int num6 = bodyFrame.Y / bodyFrame.Height;
					int useStyle = drawPlayer.lastVisualizedSelectedItem.useStyle;
					num5 = Utils.Clamp<int>(num6, 1, 4);
					if (num5 == 3 || num6 == 0 || useStyle == 13)
					{
						num5 = this._frame;
					}
					if (useStyle == 12 && drawPlayer.itemAnimation > drawPlayer.itemAnimationMax / 2)
					{
						num5 = 3;
						goto IL_3D0;
					}
					goto IL_3D0;
				}
			}
			else
			{
				if (drawType == 2)
				{
					num5 = this._frame;
					goto IL_3D0;
				}
				if (drawType != 3)
				{
					num5 = 0;
					goto IL_3D0;
				}
				num5 = this._frameExtra;
				goto IL_3D0;
			}
			num5 = this._frame;
			IL_3D0:
			int num7 = num4 / num3;
			Rectangle value;
			value..ctor(0, num7 * num5, this._data.textureWidth, num7);
			if (flag)
			{
				value.Height -= 2;
			}
			type2 = this._type;
			if (type2 != 0)
			{
				if (type2 != 7)
				{
					if (type2 == 9)
					{
						if (drawType == 3)
						{
							if (this._abilityCharge == 0)
							{
								return;
							}
							drawColor = Color.Multiply(Color.White, (float)this._abilityCharge / (float)this._data.abilityChargeMax);
							drawColor.A = 0;
						}
					}
				}
				else if (drawType == 3)
				{
					drawColor = new Color(250, 250, 250, 255) * drawPlayer.stealth * (1f - shadow);
				}
			}
			else if (drawType == 3)
			{
				drawColor = Color.White;
			}
			Color color;
			color..ctor(drawColor.ToVector4() * 0.25f + new Vector4(0.75f));
			type2 = this._type;
			if (type2 <= 12)
			{
				if (type2 != 11)
				{
					if (type2 == 12)
					{
						if (drawType == 0)
						{
							float num8 = MathHelper.Clamp(drawPlayer.MountFishronSpecialCounter / 60f, 0f, 1f);
							color = Colors.CurrentLiquidColor;
							if (color == Color.Transparent)
							{
								color = Color.White;
							}
							color.A = 127;
							color *= num8;
						}
					}
				}
				else if (drawType == 2)
				{
					color = Color.White;
					color.A = 127;
				}
			}
			else if (type2 != 24)
			{
				if (type2 == 45 && drawType == 2)
				{
					color..ctor(150, 110, 110, 100);
				}
			}
			else if (drawType == 2)
			{
				color = Color.SkyBlue * 0.5f;
				color.A = 20;
			}
			float num9 = 0f;
			type2 = this._type;
			if (type2 != 7)
			{
				if (type2 == 8)
				{
					Mount.DrillMountData drillMountData = (Mount.DrillMountData)this._mountSpecificData;
					if (drawType != 0)
					{
						if (drawType == 3)
						{
							num9 = drillMountData.diodeRotation - num9 - drawPlayer.fullRotation;
						}
					}
					else
					{
						num9 = drillMountData.outerRingRotation - num9;
					}
				}
			}
			else
			{
				num9 = drawPlayer.fullRotation;
			}
			Vector2 origin = this.Origin;
			type = this._type;
			float scale = 1f;
			type2 = this._type;
			SpriteEffects spriteEffects;
			switch (type2)
			{
			case 6:
				break;
			case 7:
				spriteEffects = 0;
				goto IL_657;
			case 8:
				spriteEffects = ((drawPlayer.direction == 1 && drawType == 2) ? 1 : 0);
				goto IL_657;
			default:
				if (type2 != 13)
				{
					spriteEffects = playerEffect;
					goto IL_657;
				}
				break;
			}
			spriteEffects = (this._flipDraw ? 1 : 0);
			IL_657:
			if (MountID.Sets.FacePlayersVelocity[this._type])
			{
				spriteEffects = ((Math.Sign(drawPlayer.velocity.X) == -drawPlayer.direction) ? (playerEffect ^ 1) : playerEffect);
			}
			type2 = this._type;
			if (type2 != 35)
			{
				if (type2 != 38)
				{
					if (type2 == 50 && drawType == 0)
					{
						Vector2 position = Position + new Vector2(0f, (float)(8 - this.PlayerOffset + 20));
						Rectangle value2;
						value2..ctor(0, num7 * this._frameExtra, this._data.textureWidth, num7);
						if (flag)
						{
							value2.Height -= 2;
						}
						playerDrawData.Add(new DrawData(TextureAssets.Extra[207].Value, position, new Rectangle?(value2), drawColor, num9, origin, scale, spriteEffects, 0f)
						{
							shader = Mount.currentShader
						});
					}
				}
				else if (drawType == 0)
				{
					int num10 = 0;
					if (spriteEffects.HasFlag(1))
					{
						num10 = 22;
					}
					Vector2 vector;
					vector..ctor((float)num10, -10f);
					Texture2D value3 = TextureAssets.Extra[151].Value;
					Rectangle value4 = value3.Frame(1, 1, 0, 0, 0, 0);
					playerDrawData.Add(new DrawData(value3, Position + vector, new Rectangle?(value4), drawColor, num9, origin, scale, spriteEffects, 0f)
					{
						shader = Mount.currentShader
					});
				}
			}
			else if (drawType == 2)
			{
				Mount.ExtraFrameMountData extraFrameMountData = (Mount.ExtraFrameMountData)this._mountSpecificData;
				int num11 = -36;
				if (spriteEffects.HasFlag(1))
				{
					num11 *= -1;
				}
				Vector2 vector2;
				vector2..ctor((float)num11, -26f);
				if (shadow == 0f)
				{
					if (Math.Abs(drawPlayer.velocity.X) > 1f)
					{
						extraFrameMountData.frameCounter += Math.Min(2f, Math.Abs(drawPlayer.velocity.X * 0.4f));
						while (extraFrameMountData.frameCounter > 6f)
						{
							extraFrameMountData.frameCounter -= 6f;
							extraFrameMountData.frame++;
							if ((extraFrameMountData.frame > 2 && extraFrameMountData.frame < 5) || extraFrameMountData.frame > 7)
							{
								extraFrameMountData.frame = 0;
							}
						}
					}
					else
					{
						extraFrameMountData.frameCounter += 1f;
						while (extraFrameMountData.frameCounter > 6f)
						{
							extraFrameMountData.frameCounter -= 6f;
							extraFrameMountData.frame++;
							if (extraFrameMountData.frame > 5)
							{
								extraFrameMountData.frame = 5;
							}
						}
					}
				}
				Texture2D value5 = TextureAssets.Extra[142].Value;
				Rectangle value6 = value5.Frame(1, 8, 0, extraFrameMountData.frame, 0, 0);
				if (flag)
				{
					value6.Height -= 2;
				}
				playerDrawData.Add(new DrawData(value5, Position + vector2, new Rectangle?(value6), drawColor, num9, origin, scale, spriteEffects, 0f)
				{
					shader = Mount.currentShader
				});
			}
			if (MountLoader.Draw(this, playerDrawData, drawType, drawPlayer, ref texture2D, ref texture2D2, ref Position, ref value, ref drawColor, ref color, ref num9, ref spriteEffects, ref origin, ref scale, shadow))
			{
				DrawData item = new DrawData(texture2D, Position, new Rectangle?(value), drawColor, num9, origin, scale, spriteEffects, 0f)
				{
					shader = Mount.currentShader
				};
				playerDrawData.Add(item);
				if (texture2D2 != null)
				{
					item = new DrawData(texture2D2, Position, new Rectangle?(value), color * ((float)drawColor.A / 255f), num9, origin, scale, spriteEffects, 0f);
					item.shader = Mount.currentShader;
				}
				playerDrawData.Add(item);
			}
			type2 = this._type;
			if (type2 <= 17)
			{
				if (type2 != 8)
				{
					if (type2 != 17)
					{
						return;
					}
					if (drawType == 1 && Mount.ShouldGolfCartEmitLight())
					{
						value..ctor(0, num7 * 3, this._data.textureWidth, num7);
						if (flag)
						{
							value.Height -= 2;
						}
						drawColor = Color.White * 1f;
						drawColor.A = 0;
						playerDrawData.Add(new DrawData(texture2D, Position, new Rectangle?(value), drawColor, num9, origin, scale, spriteEffects, 0f)
						{
							shader = Mount.currentShader
						});
						return;
					}
				}
				else if (drawType == 3)
				{
					Mount.DrillMountData drillMountData2 = (Mount.DrillMountData)this._mountSpecificData;
					Rectangle value7;
					value7..ctor(0, 0, 1, 1);
					Vector2 vector3 = Mount.drillDiodePoint1.RotatedBy((double)drillMountData2.diodeRotation, default(Vector2));
					Vector2 spinningpoint = Mount.drillDiodePoint2;
					double radians = (double)drillMountData2.diodeRotation;
					Vector2 vector7 = default(Vector2);
					Vector2 vector4 = spinningpoint.RotatedBy(radians, vector7);
					for (int i = 0; i < drillMountData2.beams.Length; i++)
					{
						Mount.DrillBeam drillBeam = drillMountData2.beams[i];
						if (!(drillBeam.curTileTarget == Point16.NegativeOne))
						{
							for (int j = 0; j < 2; j++)
							{
								Vector2 vector8 = new Vector2((float)(drillBeam.curTileTarget.X * 16 + 8), (float)(drillBeam.curTileTarget.Y * 16 + 8)) - Main.screenPosition - Position;
								Vector2 vector5;
								Color color2;
								if (j == 0)
								{
									vector5 = vector3;
									color2 = Color.CornflowerBlue;
								}
								else
								{
									vector5 = vector4;
									color2 = Color.LightGreen;
								}
								color2.A = 128;
								color2 *= 0.5f;
								Vector2 v = vector8 - vector5;
								float num12 = v.ToRotation();
								float y = v.Length();
								vector7..ctor(2f, y);
								playerDrawData.Add(new DrawData(TextureAssets.MagicPixel.Value, vector5 + Position, new Rectangle?(value7), color2, num12 - 1.5707964f, Vector2.Zero, vector7, 0, 0f)
								{
									ignorePlayerRotation = true,
									shader = Mount.currentShader
								});
							}
						}
					}
				}
			}
			else if (type2 != 23)
			{
				if (type2 != 45)
				{
					if (type2 == 50 && drawType == 0)
					{
						texture2D = TextureAssets.Extra[205].Value;
						DrawData item2 = new DrawData(texture2D, Position, new Rectangle?(value), drawColor, num9, origin, scale, spriteEffects, 0f)
						{
							shader = Mount.currentShader
						};
						playerDrawData.Add(item2);
						Vector2 position2 = Position + new Vector2(0f, (float)(8 - this.PlayerOffset + 20));
						Rectangle value8;
						value8..ctor(0, num7 * this._frameExtra, this._data.textureWidth, num7);
						if (flag)
						{
							value8.Height -= 2;
						}
						texture2D = TextureAssets.Extra[206].Value;
						item2 = new DrawData(texture2D, position2, new Rectangle?(value8), drawColor, num9, origin, scale, spriteEffects, 0f)
						{
							shader = Mount.currentShader
						};
						playerDrawData.Add(item2);
						return;
					}
				}
				else if (drawType == 0 && shadow == 0f)
				{
					if (Math.Abs(drawPlayer.velocity.X) > this.DashSpeed * 0.9f)
					{
						color..ctor(255, 220, 220, 200);
						scale = 1.1f;
					}
					for (int k = 0; k < 2; k++)
					{
						Vector2 position3 = Position + new Vector2((float)Main.rand.Next(-10, 11) * 0.1f, (float)Main.rand.Next(-10, 11) * 0.1f);
						value..ctor(0, num7 * 3, this._data.textureWidth, num7);
						if (flag)
						{
							value.Height -= 2;
						}
						playerDrawData.Add(new DrawData(texture2D2, position3, new Rectangle?(value), color, num9, origin, scale, spriteEffects, 0f)
						{
							shader = Mount.currentShader
						});
					}
					return;
				}
			}
			else if (drawType == 0)
			{
				texture2D = TextureAssets.Extra[114].Value;
				value = texture2D.Frame(2, 1, 0, 0, 0, 0);
				int width = value.Width;
				value.Width -= 2;
				float witchBroomTrinketRotation = this.GetWitchBroomTrinketRotation(drawPlayer);
				Vector2 vector6 = Position + this.GetWitchBroomTrinketOriginOffset(drawPlayer);
				num9 = witchBroomTrinketRotation;
				origin..ctor((float)(value.Width / 2), 0f);
				DrawData item3 = new DrawData(texture2D, vector6.Floor(), new Rectangle?(value), drawColor, num9, origin, scale, spriteEffects, 0f)
				{
					shader = Mount.currentShader
				};
				playerDrawData.Add(item3);
				Color color3;
				color3..ctor(new Vector3(0.9f, 0.85f, 0f));
				color3.A /= 2;
				float num13 = ((float)drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 1f;
				Color color4 = new Color(80, 70, 40, 0) * (num13 / 8f + 0.5f) * 0.8f;
				value.X += width;
				for (int l = 0; l < 4; l++)
				{
					item3 = new DrawData(texture2D, (vector6 + ((float)l * 1.5707964f).ToRotationVector2() * num13).Floor(), new Rectangle?(value), color4, num9, origin, scale, spriteEffects, 0f)
					{
						shader = Mount.currentShader
					};
					playerDrawData.Add(item3);
				}
				return;
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00157458 File Offset: 0x00155658
		public void Dismount(Player mountedPlayer)
		{
			if (this._active)
			{
				bool cart = this.Cart;
				this._active = false;
				mountedPlayer.ClearBuff(this._data.buff);
				bool skipDust = false;
				MountLoader.Dismount(this, mountedPlayer, ref skipDust);
				this._mountSpecificData = null;
				int type = this._type;
				if (cart)
				{
					mountedPlayer.ClearBuff(this._data.extraBuff);
					mountedPlayer.cartFlip = false;
					mountedPlayer.lastBoost = Vector2.Zero;
				}
				mountedPlayer.fullRotation = 0f;
				mountedPlayer.fullRotationOrigin = Vector2.Zero;
				if (!skipDust)
				{
					this.DoSpawnDust(mountedPlayer, true);
				}
				this.Reset();
				mountedPlayer.position.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
				mountedPlayer.height = 42;
				mountedPlayer.position.Y = mountedPlayer.position.Y - (float)mountedPlayer.height;
				if (mountedPlayer.whoAmI == Main.myPlayer)
				{
					NetMessage.SendData(13, -1, -1, null, mountedPlayer.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00157558 File Offset: 0x00155758
		public void SetMount(int m, Player mountedPlayer, bool faceLeft = false)
		{
			if (this._type == m || m <= -1 || m >= Mount.mounts.Length || (m == 5 && mountedPlayer.wet))
			{
				return;
			}
			if (this._active)
			{
				mountedPlayer.ClearBuff(this._data.buff);
				if (this.Cart)
				{
					mountedPlayer.ClearBuff(this._data.extraBuff);
					mountedPlayer.cartFlip = false;
					mountedPlayer.lastBoost = Vector2.Zero;
				}
				mountedPlayer.fullRotation = 0f;
				mountedPlayer.fullRotationOrigin = Vector2.Zero;
				this._mountSpecificData = null;
			}
			else
			{
				this._active = true;
			}
			this._flyTime = 0;
			this._type = m;
			this._data = Mount.mounts[m];
			this._fatigueMax = (float)this._data.fatigueMax;
			if (this.Cart && !faceLeft && !this.Directional)
			{
				mountedPlayer.AddBuff(this._data.extraBuff, 3600, true, false);
				this._flipDraw = true;
			}
			else
			{
				mountedPlayer.AddBuff(this._data.buff, 3600, true, false);
				this._flipDraw = false;
			}
			if (this._type == 44)
			{
				mountedPlayer.velocity *= 0.2f;
				mountedPlayer.dash = 0;
				mountedPlayer.dashType = 0;
				mountedPlayer.dashDelay = 0;
				mountedPlayer.dashTime = 0;
			}
			if (this._type == 9 && this._abilityCooldown < 20)
			{
				this._abilityCooldown = 20;
			}
			if (this._type == 46 && this._abilityCooldown < 40)
			{
				this._abilityCooldown = 40;
			}
			mountedPlayer.position.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
			for (int i = 0; i < mountedPlayer.shadowPos.Length; i++)
			{
				Vector2[] shadowPos = mountedPlayer.shadowPos;
				int num = i;
				shadowPos[num].Y = shadowPos[num].Y + (float)mountedPlayer.height;
			}
			mountedPlayer.height = 42 + this._data.heightBoost;
			mountedPlayer.position.Y = mountedPlayer.position.Y - (float)mountedPlayer.height;
			for (int j = 0; j < mountedPlayer.shadowPos.Length; j++)
			{
				Vector2[] shadowPos2 = mountedPlayer.shadowPos;
				int num2 = j;
				shadowPos2[num2].Y = shadowPos2[num2].Y - (float)mountedPlayer.height;
			}
			mountedPlayer.ResetAdvancedShadows();
			if (this._type == 7 || this._type == 8)
			{
				mountedPlayer.fullRotationOrigin = new Vector2((float)(mountedPlayer.width / 2), (float)(mountedPlayer.height / 2));
			}
			if (this._type == 8)
			{
				this._mountSpecificData = new Mount.DrillMountData();
			}
			if (this._type == 35)
			{
				this._mountSpecificData = new Mount.ExtraFrameMountData();
			}
			bool skipDust = false;
			MountLoader.SetMount(this, mountedPlayer, ref skipDust);
			if (!skipDust)
			{
				this.DoSpawnDust(mountedPlayer, false);
			}
			if (mountedPlayer.whoAmI == Main.myPlayer)
			{
				NetMessage.SendData(13, -1, -1, null, mountedPlayer.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00157828 File Offset: 0x00155A28
		private void DoSpawnDust(Player mountedPlayer, bool isDismounting)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			if (this._type == 52)
			{
				for (int i = 0; i < 100; i++)
				{
					int spawnDust = this._data.spawnDust;
					Dust dust = Dust.NewDustDirect(new Vector2(mountedPlayer.position.X - 20f, mountedPlayer.position.Y), mountedPlayer.width + 40, mountedPlayer.height, 267, 0f, 0f, 60, new Color(130, 60, 255, 70), 1f);
					dust.scale += (float)Main.rand.Next(-10, 21) * 0.01f;
					dust.noGravity = true;
					dust.velocity += mountedPlayer.velocity * 0.8f;
					dust.velocity *= Main.rand.NextFloat();
					Dust dust2 = dust;
					dust2.velocity.Y = dust2.velocity.Y + 2f * Main.rand.NextFloatDirection();
					dust.noLight = true;
					if (Main.rand.Next(3) == 0)
					{
						Dust dust3 = Dust.CloneDust(dust);
						dust3.color = Color.White;
						dust3.scale *= 0.5f;
						dust3.alpha = 0;
					}
				}
				return;
			}
			Color newColor = Color.Transparent;
			if (this._type == 23)
			{
				newColor..ctor(255, 255, 0, 255);
			}
			for (int j = 0; j < 100; j++)
			{
				if (MountID.Sets.Cart[this._type])
				{
					if (j % 10 == 0)
					{
						int type = Main.rand.Next(61, 64);
						int num = Gore.NewGore(new Vector2(mountedPlayer.position.X - 20f, mountedPlayer.position.Y), Vector2.Zero, type, 1f);
						Main.gore[num].alpha = 100;
						Main.gore[num].velocity = Vector2.Transform(new Vector2(1f, 0f), Matrix.CreateRotationZ((float)(Main.rand.NextDouble() * 6.2831854820251465)));
					}
				}
				else
				{
					int type2 = this._data.spawnDust;
					float scale = 1f;
					int alpha = 0;
					if (this._type == 40 || this._type == 41 || this._type == 42)
					{
						type2 = ((Main.rand.Next(2) != 0) ? 16 : 31);
						scale = 0.9f;
						alpha = 50;
						if (this._type == 42)
						{
							type2 = 31;
						}
						if (this._type == 41)
						{
							type2 = 16;
						}
					}
					int num2 = Dust.NewDust(new Vector2(mountedPlayer.position.X - 20f, mountedPlayer.position.Y), mountedPlayer.width + 40, mountedPlayer.height, type2, 0f, 0f, alpha, newColor, scale);
					Main.dust[num2].scale += (float)Main.rand.Next(-10, 21) * 0.01f;
					if (this._data.spawnDustNoGravity)
					{
						Main.dust[num2].noGravity = true;
					}
					else if (Main.rand.Next(2) == 0)
					{
						Main.dust[num2].scale *= 1.3f;
						Main.dust[num2].noGravity = true;
					}
					else
					{
						Main.dust[num2].velocity *= 0.5f;
					}
					Main.dust[num2].velocity += mountedPlayer.velocity * 0.8f;
					if (this._type == 40 || this._type == 41 || this._type == 42)
					{
						Main.dust[num2].velocity *= Main.rand.NextFloat();
					}
				}
			}
			if (this._type == 40 || this._type == 41 || this._type == 42)
			{
				for (int k = 0; k < 5; k++)
				{
					int type3 = Main.rand.Next(61, 64);
					if (this._type == 41 || (this._type == 40 && Main.rand.Next(2) == 0))
					{
						type3 = Main.rand.Next(11, 14);
					}
					int num3 = Gore.NewGore(new Vector2(mountedPlayer.position.X + (float)(mountedPlayer.direction * 8), mountedPlayer.position.Y + 20f), Vector2.Zero, type3, 1f);
					Main.gore[num3].alpha = 100;
					Main.gore[num3].velocity = Vector2.Transform(new Vector2(1f, 0f), Matrix.CreateRotationZ((float)(Main.rand.NextDouble() * 6.2831854820251465))) * 1.4f;
				}
			}
			if (this._type == 23)
			{
				for (int l = 0; l < 4; l++)
				{
					int type4 = Main.rand.Next(61, 64);
					int num4 = Gore.NewGore(new Vector2(mountedPlayer.position.X - 20f, mountedPlayer.position.Y), Vector2.Zero, type4, 1f);
					Main.gore[num4].alpha = 100;
					Main.gore[num4].velocity = Vector2.Transform(new Vector2(1f, 0f), Matrix.CreateRotationZ((float)(Main.rand.NextDouble() * 6.2831854820251465)));
				}
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00157DE7 File Offset: 0x00155FE7
		public bool CanMount(int m, Player mountingPlayer)
		{
			return mountingPlayer.CanFitSpace(Mount.mounts[m].heightBoost);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00157DFC File Offset: 0x00155FFC
		public unsafe bool FindTileHeight(Vector2 position, int maxTilesDown, out float tileHeight)
		{
			int num = (int)(position.X / 16f);
			int num2 = (int)(position.Y / 16f);
			for (int i = 0; i <= maxTilesDown; i++)
			{
				Tile tile = Main.tile[num, num2];
				bool flag = Main.tileSolid[(int)(*tile.type)];
				bool flag2 = Main.tileSolidTop[(int)(*tile.type)];
				bool flag3 = !tile.active() || !flag || flag2;
				num2++;
			}
			tileHeight = 0f;
			return true;
		}

		// Token: 0x040007B7 RID: 1975
		public static int currentShader = 0;

		// Token: 0x040007B8 RID: 1976
		public const int FrameStanding = 0;

		// Token: 0x040007B9 RID: 1977
		public const int FrameRunning = 1;

		// Token: 0x040007BA RID: 1978
		public const int FrameInAir = 2;

		// Token: 0x040007BB RID: 1979
		public const int FrameFlying = 3;

		// Token: 0x040007BC RID: 1980
		public const int FrameSwimming = 4;

		// Token: 0x040007BD RID: 1981
		public const int FrameDashing = 5;

		// Token: 0x040007BE RID: 1982
		public const int DrawBack = 0;

		// Token: 0x040007BF RID: 1983
		public const int DrawBackExtra = 1;

		// Token: 0x040007C0 RID: 1984
		public const int DrawFront = 2;

		// Token: 0x040007C1 RID: 1985
		public const int DrawFrontExtra = 3;

		// Token: 0x040007C2 RID: 1986
		public static Mount.MountData[] mounts;

		// Token: 0x040007C3 RID: 1987
		private static Vector2[] scutlixEyePositions;

		// Token: 0x040007C4 RID: 1988
		private static Vector2 scutlixTextureSize;

		// Token: 0x040007C5 RID: 1989
		public const int scutlixBaseDamage = 50;

		// Token: 0x040007C6 RID: 1990
		public static Vector2 drillDiodePoint1 = new Vector2(36f, -6f);

		// Token: 0x040007C7 RID: 1991
		public static Vector2 drillDiodePoint2 = new Vector2(36f, 8f);

		// Token: 0x040007C8 RID: 1992
		public static Vector2 drillTextureSize;

		// Token: 0x040007C9 RID: 1993
		public const int drillTextureWidth = 80;

		// Token: 0x040007CA RID: 1994
		public const float drillRotationChange = 0.05235988f;

		// Token: 0x040007CB RID: 1995
		public static int drillPickPower = 210;

		// Token: 0x040007CC RID: 1996
		public static int drillPickTime = 1;

		// Token: 0x040007CD RID: 1997
		public static int amountOfBeamsAtOnce = 2;

		// Token: 0x040007CE RID: 1998
		public const float maxDrillLength = 48f;

		// Token: 0x040007CF RID: 1999
		private static Vector2 santankTextureSize;

		// Token: 0x040007D0 RID: 2000
		public Mount.MountData _data;

		// Token: 0x040007D1 RID: 2001
		public int _type;

		// Token: 0x040007D2 RID: 2002
		public bool _flipDraw;

		// Token: 0x040007D3 RID: 2003
		public int _frame;

		// Token: 0x040007D4 RID: 2004
		public float _frameCounter;

		// Token: 0x040007D5 RID: 2005
		public int _frameExtra;

		// Token: 0x040007D6 RID: 2006
		public float _frameExtraCounter;

		// Token: 0x040007D7 RID: 2007
		public int _frameState;

		// Token: 0x040007D8 RID: 2008
		public int _flyTime;

		// Token: 0x040007D9 RID: 2009
		public int _idleTime;

		// Token: 0x040007DA RID: 2010
		public int _idleTimeNext;

		// Token: 0x040007DB RID: 2011
		public float _fatigue;

		// Token: 0x040007DC RID: 2012
		public float _fatigueMax;

		// Token: 0x040007DD RID: 2013
		public bool _abilityCharging;

		// Token: 0x040007DE RID: 2014
		public int _abilityCharge;

		// Token: 0x040007DF RID: 2015
		public int _abilityCooldown;

		// Token: 0x040007E0 RID: 2016
		public int _abilityDuration;

		// Token: 0x040007E1 RID: 2017
		public bool _abilityActive;

		// Token: 0x040007E2 RID: 2018
		public bool _aiming;

		// Token: 0x040007E3 RID: 2019
		public bool _shouldSuperCart;

		// Token: 0x040007E4 RID: 2020
		public List<DrillDebugDraw> _debugDraw;

		// Token: 0x040007E5 RID: 2021
		public object _mountSpecificData;

		// Token: 0x040007E6 RID: 2022
		public bool _active;

		// Token: 0x040007E7 RID: 2023
		[Obsolete("Unused, use MountData.MinecartUpgradeRunSpeed instead")]
		public static float SuperCartRunSpeed = 20f;

		// Token: 0x040007E8 RID: 2024
		[Obsolete("Unused, use MountData.MinecartUpgradeDashSpeed instead")]
		public static float SuperCartDashSpeed = 20f;

		// Token: 0x040007E9 RID: 2025
		[Obsolete("Unused, use MountData.MinecartUpgradeAcceleration instead")]
		public static float SuperCartAcceleration = 0.1f;

		// Token: 0x040007EA RID: 2026
		[Obsolete("Unused, use MountData.MinecartUpgradeJumpHeight instead")]
		public static int SuperCartJumpHeight = 15;

		// Token: 0x040007EB RID: 2027
		[Obsolete("Unused, use MountData.MinecartUpgradeJumpSpeed instead")]
		public static float SuperCartJumpSpeed = 5.15f;

		// Token: 0x040007EC RID: 2028
		private Mount.MountDelegatesData _defaultDelegatesData = new Mount.MountDelegatesData();

		// Token: 0x020007B7 RID: 1975
		private class DrillBeam
		{
			// Token: 0x06004F00 RID: 20224 RVA: 0x00675C74 File Offset: 0x00673E74
			public DrillBeam()
			{
				this.curTileTarget = Point16.NegativeOne;
				this.cooldown = 0;
				this.lastPurpose = 0;
			}

			// Token: 0x04006653 RID: 26195
			public Point16 curTileTarget;

			// Token: 0x04006654 RID: 26196
			public int cooldown;

			// Token: 0x04006655 RID: 26197
			public int lastPurpose;
		}

		// Token: 0x020007B8 RID: 1976
		private class DrillMountData
		{
			// Token: 0x06004F01 RID: 20225 RVA: 0x00675C98 File Offset: 0x00673E98
			public DrillMountData()
			{
				this.beams = new Mount.DrillBeam[8];
				for (int i = 0; i < this.beams.Length; i++)
				{
					this.beams[i] = new Mount.DrillBeam();
				}
			}

			// Token: 0x04006656 RID: 26198
			public float diodeRotationTarget;

			// Token: 0x04006657 RID: 26199
			public float diodeRotation;

			// Token: 0x04006658 RID: 26200
			public float outerRingRotation;

			// Token: 0x04006659 RID: 26201
			public Mount.DrillBeam[] beams;

			// Token: 0x0400665A RID: 26202
			public int beamCooldown;

			// Token: 0x0400665B RID: 26203
			public Vector2 crosshairPosition;
		}

		// Token: 0x020007B9 RID: 1977
		private class BooleanMountData
		{
			// Token: 0x06004F02 RID: 20226 RVA: 0x00675CD7 File Offset: 0x00673ED7
			public BooleanMountData()
			{
				this.boolean = false;
			}

			// Token: 0x0400665C RID: 26204
			public bool boolean;
		}

		// Token: 0x020007BA RID: 1978
		private class ExtraFrameMountData
		{
			// Token: 0x06004F03 RID: 20227 RVA: 0x00675CE6 File Offset: 0x00673EE6
			public ExtraFrameMountData()
			{
				this.frame = 0;
				this.frameCounter = 0f;
			}

			// Token: 0x0400665D RID: 26205
			public int frame;

			// Token: 0x0400665E RID: 26206
			public float frameCounter;
		}

		// Token: 0x020007BB RID: 1979
		public class MountDelegatesData
		{
			// Token: 0x06004F04 RID: 20228 RVA: 0x00675D00 File Offset: 0x00673F00
			public MountDelegatesData()
			{
				Action<Vector2> minecartDust;
				if ((minecartDust = Mount.MountDelegatesData.<>O.<0>__Sparks) == null)
				{
					minecartDust = (Mount.MountDelegatesData.<>O.<0>__Sparks = new Action<Vector2>(DelegateMethods.Minecart.Sparks));
				}
				this.MinecartDust = minecartDust;
				Action<Player, Vector2, int, int> minecartJumpingSound;
				if ((minecartJumpingSound = Mount.MountDelegatesData.<>O.<1>__JumpingSound) == null)
				{
					minecartJumpingSound = (Mount.MountDelegatesData.<>O.<1>__JumpingSound = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.JumpingSound));
				}
				this.MinecartJumpingSound = minecartJumpingSound;
				Action<Player, Vector2, int, int> minecartLandingSound;
				if ((minecartLandingSound = Mount.MountDelegatesData.<>O.<2>__LandingSound) == null)
				{
					minecartLandingSound = (Mount.MountDelegatesData.<>O.<2>__LandingSound = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.LandingSound));
				}
				this.MinecartLandingSound = minecartLandingSound;
				Action<Player, Vector2, int, int> minecartBumperSound;
				if ((minecartBumperSound = Mount.MountDelegatesData.<>O.<3>__BumperSound) == null)
				{
					minecartBumperSound = (Mount.MountDelegatesData.<>O.<3>__BumperSound = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.BumperSound));
				}
				this.MinecartBumperSound = minecartBumperSound;
			}

			// Token: 0x0400665F RID: 26207
			public Action<Vector2> MinecartDust;

			// Token: 0x04006660 RID: 26208
			public Action<Player, Vector2, int, int> MinecartJumpingSound;

			// Token: 0x04006661 RID: 26209
			public Action<Player, Vector2, int, int> MinecartLandingSound;

			// Token: 0x04006662 RID: 26210
			public Action<Player, Vector2, int, int> MinecartBumperSound;

			// Token: 0x04006663 RID: 26211
			public Mount.MountDelegatesData.OverridePositionMethod MouthPosition;

			// Token: 0x04006664 RID: 26212
			public Mount.MountDelegatesData.OverridePositionMethod HandPosition;

			// Token: 0x02000DD6 RID: 3542
			// (Invoke) Token: 0x06006438 RID: 25656
			public delegate bool OverridePositionMethod(Player player, out Vector2? position);

			// Token: 0x02000DD7 RID: 3543
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04007BB1 RID: 31665
				public static Action<Vector2> <0>__Sparks;

				// Token: 0x04007BB2 RID: 31666
				public static Action<Player, Vector2, int, int> <1>__JumpingSound;

				// Token: 0x04007BB3 RID: 31667
				public static Action<Player, Vector2, int, int> <2>__LandingSound;

				// Token: 0x04007BB4 RID: 31668
				public static Action<Player, Vector2, int, int> <3>__BumperSound;
			}
		}

		// Token: 0x020007BC RID: 1980
		public class MountData
		{
			// Token: 0x1700089B RID: 2203
			// (get) Token: 0x06004F05 RID: 20229 RVA: 0x00675D97 File Offset: 0x00673F97
			// (set) Token: 0x06004F06 RID: 20230 RVA: 0x00675D9F File Offset: 0x00673F9F
			public ModMount ModMount { get; set; }

			/// <summary> Texture drawn behind the player while in use. </summary>
			// Token: 0x04006666 RID: 26214
			public Asset<Texture2D> backTexture = Asset<Texture2D>.Empty;

			// Token: 0x04006667 RID: 26215
			public Asset<Texture2D> backTextureGlow = Asset<Texture2D>.Empty;

			// Token: 0x04006668 RID: 26216
			public Asset<Texture2D> backTextureExtra = Asset<Texture2D>.Empty;

			// Token: 0x04006669 RID: 26217
			public Asset<Texture2D> backTextureExtraGlow = Asset<Texture2D>.Empty;

			/// <summary> Texture drawn in front of the player while in use. </summary>
			// Token: 0x0400666A RID: 26218
			public Asset<Texture2D> frontTexture = Asset<Texture2D>.Empty;

			// Token: 0x0400666B RID: 26219
			public Asset<Texture2D> frontTextureGlow = Asset<Texture2D>.Empty;

			// Token: 0x0400666C RID: 26220
			public Asset<Texture2D> frontTextureExtra = Asset<Texture2D>.Empty;

			// Token: 0x0400666D RID: 26221
			public Asset<Texture2D> frontTextureExtraGlow = Asset<Texture2D>.Empty;

			// Token: 0x0400666E RID: 26222
			public int textureWidth;

			// Token: 0x0400666F RID: 26223
			public int textureHeight;

			// Token: 0x04006670 RID: 26224
			public int xOffset;

			// Token: 0x04006671 RID: 26225
			public int yOffset;

			// Token: 0x04006672 RID: 26226
			public int[] playerYOffsets;

			// Token: 0x04006673 RID: 26227
			public int bodyFrame;

			// Token: 0x04006674 RID: 26228
			public int playerHeadOffset;

			// Token: 0x04006675 RID: 26229
			public int heightBoost;

			// Token: 0x04006676 RID: 26230
			public int buff;

			// Token: 0x04006677 RID: 26231
			public int extraBuff;

			// Token: 0x04006678 RID: 26232
			public int flightTimeMax;

			// Token: 0x04006679 RID: 26233
			public bool usesHover;

			// Token: 0x0400667A RID: 26234
			public float runSpeed;

			// Token: 0x0400667B RID: 26235
			public float dashSpeed;

			// Token: 0x0400667C RID: 26236
			public float swimSpeed;

			// Token: 0x0400667D RID: 26237
			public float acceleration;

			// Token: 0x0400667E RID: 26238
			public float jumpSpeed;

			// Token: 0x0400667F RID: 26239
			public int jumpHeight;

			// Token: 0x04006680 RID: 26240
			public float fallDamage;

			// Token: 0x04006681 RID: 26241
			public int fatigueMax;

			// Token: 0x04006682 RID: 26242
			public bool constantJump;

			// Token: 0x04006683 RID: 26243
			public bool blockExtraJumps;

			// Token: 0x04006684 RID: 26244
			public int abilityChargeMax;

			// Token: 0x04006685 RID: 26245
			public int abilityDuration;

			// Token: 0x04006686 RID: 26246
			public int abilityCooldown;

			// Token: 0x04006687 RID: 26247
			public int spawnDust;

			// Token: 0x04006688 RID: 26248
			public bool spawnDustNoGravity;

			// Token: 0x04006689 RID: 26249
			public int totalFrames;

			// Token: 0x0400668A RID: 26250
			public int standingFrameStart;

			// Token: 0x0400668B RID: 26251
			public int standingFrameCount;

			// Token: 0x0400668C RID: 26252
			public int standingFrameDelay;

			// Token: 0x0400668D RID: 26253
			public int runningFrameStart;

			// Token: 0x0400668E RID: 26254
			public int runningFrameCount;

			// Token: 0x0400668F RID: 26255
			public int runningFrameDelay;

			// Token: 0x04006690 RID: 26256
			public int flyingFrameStart;

			// Token: 0x04006691 RID: 26257
			public int flyingFrameCount;

			// Token: 0x04006692 RID: 26258
			public int flyingFrameDelay;

			// Token: 0x04006693 RID: 26259
			public int inAirFrameStart;

			// Token: 0x04006694 RID: 26260
			public int inAirFrameCount;

			// Token: 0x04006695 RID: 26261
			public int inAirFrameDelay;

			// Token: 0x04006696 RID: 26262
			public int idleFrameStart;

			// Token: 0x04006697 RID: 26263
			public int idleFrameCount;

			// Token: 0x04006698 RID: 26264
			public int idleFrameDelay;

			// Token: 0x04006699 RID: 26265
			public bool idleFrameLoop;

			// Token: 0x0400669A RID: 26266
			public int swimFrameStart;

			// Token: 0x0400669B RID: 26267
			public int swimFrameCount;

			// Token: 0x0400669C RID: 26268
			public int swimFrameDelay;

			// Token: 0x0400669D RID: 26269
			public int dashingFrameStart;

			// Token: 0x0400669E RID: 26270
			public int dashingFrameCount;

			// Token: 0x0400669F RID: 26271
			public int dashingFrameDelay;

			// Token: 0x040066A0 RID: 26272
			public bool Minecart;

			// Token: 0x040066A1 RID: 26273
			internal bool MinecartDirectional;

			// Token: 0x040066A2 RID: 26274
			public Vector3 lightColor = Vector3.One;

			// Token: 0x040066A3 RID: 26275
			public bool emitsLight;

			// Token: 0x040066A4 RID: 26276
			public Mount.MountDelegatesData delegations = new Mount.MountDelegatesData();

			// Token: 0x040066A5 RID: 26277
			public int playerXOffset;

			/// <summary> Alternate <see cref="F:Terraria.Mount.MountData.runSpeed" /> for this minecart while the <see href="https://terraria.wiki.gg/wiki/Minecart_Upgrade_Kit">Minecart Upgrade Kit</see> is active. Defaults to <c>20f</c>.</summary>
			// Token: 0x040066A6 RID: 26278
			public float MinecartUpgradeRunSpeed = 20f;

			/// <summary> Alternate <see cref="F:Terraria.Mount.MountData.acceleration" /> for this minecart while the <see href="https://terraria.wiki.gg/wiki/Minecart_Upgrade_Kit">Minecart Upgrade Kit</see> is active. Defaults to <c>0.1f</c>.</summary>
			// Token: 0x040066A7 RID: 26279
			public float MinecartUpgradeAcceleration = 0.1f;

			/// <summary> Alternate <see cref="F:Terraria.Mount.MountData.jumpSpeed" /> for this minecart while the <see href="https://terraria.wiki.gg/wiki/Minecart_Upgrade_Kit">Minecart Upgrade Kit</see> is active. Defaults to <c>5.15f</c>.</summary>
			// Token: 0x040066A8 RID: 26280
			public float MinecartUpgradeJumpSpeed = 5.15f;

			/// <summary> Alternate <see cref="F:Terraria.Mount.MountData.dashSpeed" /> for this minecart while the <see href="https://terraria.wiki.gg/wiki/Minecart_Upgrade_Kit">Minecart Upgrade Kit</see> is active. Defaults to <c>20f</c>.</summary>
			// Token: 0x040066A9 RID: 26281
			public float MinecartUpgradeDashSpeed = 20f;

			/// <summary> Alternate <see cref="F:Terraria.Mount.MountData.jumpHeight" /> for this minecart while the <see href="https://terraria.wiki.gg/wiki/Minecart_Upgrade_Kit">Minecart Upgrade Kit</see> is active. Defaults to <c>15</c>.</summary>
			// Token: 0x040066AA RID: 26282
			public int MinecartUpgradeJumpHeight = 15;
		}

		// Token: 0x020007BD RID: 1981
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040066AB RID: 26283
			public static Action<Vector2> <0>__Sparks;

			// Token: 0x040066AC RID: 26284
			public static Action<Vector2> <1>__SparksMeow;

			// Token: 0x040066AD RID: 26285
			public static Action<Player, Vector2, int, int> <2>__MeowcartLandingSound;

			// Token: 0x040066AE RID: 26286
			public static Action<Player, Vector2, int, int> <3>__MeowcartBumperSound;

			// Token: 0x040066AF RID: 26287
			public static Action<Vector2> <4>__SparksFart;

			// Token: 0x040066B0 RID: 26288
			public static Action<Player, Vector2, int, int> <5>__BumperSoundFart;

			// Token: 0x040066B1 RID: 26289
			public static Action<Player, Vector2, int, int> <6>__LandingSoundFart;

			// Token: 0x040066B2 RID: 26290
			public static Action<Player, Vector2, int, int> <7>__JumpingSoundFart;

			// Token: 0x040066B3 RID: 26291
			public static Action<Vector2> <8>__SparksTerraFart;

			// Token: 0x040066B4 RID: 26292
			public static Action<Vector2> <9>__SparksMech;

			// Token: 0x040066B5 RID: 26293
			public static Mount.MountDelegatesData.OverridePositionMethod <10>__WolfMouthPosition;

			// Token: 0x040066B6 RID: 26294
			public static Mount.MountDelegatesData.OverridePositionMethod <11>__NoHandPosition;

			// Token: 0x040066B7 RID: 26295
			public static Utils.TileActionAttempt <12>__CastLightOpen_StopForSolids_ScaleWithDistance;

			// Token: 0x040066B8 RID: 26296
			public static Utils.TileActionAttempt <13>__CastLightOpen;
		}
	}
}
