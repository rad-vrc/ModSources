using System;
using System.Collections.Generic;
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

namespace Terraria
{
	// Token: 0x0200002B RID: 43
	public class Mount
	{
		// Token: 0x060001FE RID: 510 RVA: 0x0002AB18 File Offset: 0x00028D18
		private static void MeowcartLandingSound(Player Player, Vector2 Position, int Width, int Height)
		{
			SoundEngine.PlaySound(37, (int)Position.X + Width / 2, (int)Position.Y + Height / 2, 5, 1f, 0f);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0002AB43 File Offset: 0x00028D43
		private static void MeowcartBumperSound(Player Player, Vector2 Position, int Width, int Height)
		{
			SoundEngine.PlaySound(37, (int)Position.X + Width / 2, (int)Position.Y + Height / 2, 3, 1f, 0f);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0002AB6E File Offset: 0x00028D6E
		public Mount()
		{
			this._debugDraw = new List<DrillDebugDraw>();
			this.Reset();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0002AB94 File Offset: 0x00028D94
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

		// Token: 0x06000202 RID: 514 RVA: 0x0002AC20 File Offset: 0x00028E20
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
			mountData.delegations.MinecartDust = new Action<Vector2>(DelegateMethods.Minecart.Sparks);
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
			mountData.delegations.MinecartDust = new Action<Vector2>(DelegateMethods.Minecart.SparksMeow);
			mountData.delegations.MinecartLandingSound = new Action<Player, Vector2, int, int>(Mount.MeowcartLandingSound);
			mountData.delegations.MinecartBumperSound = new Action<Player, Vector2, int, int>(Mount.MeowcartBumperSound);
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
			mountData.delegations.MinecartDust = new Action<Vector2>(DelegateMethods.Minecart.Sparks);
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
			mountData.delegations.MinecartDust = new Action<Vector2>(DelegateMethods.Minecart.SparksFart);
			mountData.delegations.MinecartBumperSound = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.BumperSoundFart);
			mountData.delegations.MinecartLandingSound = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.LandingSoundFart);
			mountData.delegations.MinecartJumpingSound = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.JumpingSoundFart);
			mountData = new Mount.MountData();
			Mount.mounts[53] = mountData;
			Mount.SetAsMinecart(mountData, 347, 346, TextureAssets.Extra[251], -10, -8);
			mountData.spawnDust = 211;
			mountData.delegations.MinecartDust = new Action<Vector2>(DelegateMethods.Minecart.SparksTerraFart);
			mountData.delegations.MinecartBumperSound = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.BumperSoundFart);
			mountData.delegations.MinecartLandingSound = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.LandingSoundFart);
			mountData.delegations.MinecartJumpingSound = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.JumpingSoundFart);
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
				Vector2 value = new Vector2((float)mountData.textureWidth, (float)(mountData.textureHeight / mountData.totalFrames));
				if (Mount.drillTextureSize != value)
				{
					throw new Exception(string.Concat(new object[]
					{
						"Be sure to update the Drill texture origin to match the actual texture size of ",
						mountData.textureWidth,
						", ",
						mountData.textureHeight,
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
				Vector2 value2 = new Vector2((float)(mountData.textureWidth / 2), (float)(mountData.textureHeight / mountData.totalFrames));
				if (Mount.scutlixTextureSize != value2)
				{
					throw new Exception(string.Concat(new object[]
					{
						"Be sure to update the Scutlix texture origin to match the actual texture size of ",
						mountData.textureWidth,
						", ",
						mountData.textureHeight,
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
			mountData.delegations.MinecartDust = new Action<Vector2>(DelegateMethods.Minecart.SparksMech);
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
			mountData.delegations.MinecartDust = new Action<Vector2>(DelegateMethods.Minecart.Sparks);
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
			mountData.delegations.MouthPosition = new Mount.MountDelegatesData.OverridePositionMethod(DelegateMethods.Mount.WolfMouthPosition);
			mountData.delegations.HandPosition = new Mount.MountDelegatesData.OverridePositionMethod(DelegateMethods.Mount.NoHandPosition);
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

		// Token: 0x06000203 RID: 515 RVA: 0x0002E630 File Offset: 0x0002C830
		private static void SetAsHorse(Mount.MountData newMount, int buff, Asset<Texture2D> texture)
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

		// Token: 0x06000204 RID: 516 RVA: 0x0002E83C File Offset: 0x0002CA3C
		private static void SetAsMinecart(Mount.MountData newMount, int buffToLeft, int buffToRight, Asset<Texture2D> texture, int verticalOffset = 0, int playerVerticalOffset = 0)
		{
			newMount.Minecart = true;
			newMount.delegations = new Mount.MountDelegatesData();
			newMount.delegations.MinecartDust = new Action<Vector2>(DelegateMethods.Minecart.Sparks);
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0002E9F7 File Offset: 0x0002CBF7
		public bool Active
		{
			get
			{
				return this._active;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0002E9FF File Offset: 0x0002CBFF
		public int Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0002EA07 File Offset: 0x0002CC07
		public int FlyTime
		{
			get
			{
				return this._flyTime;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0002EA0F File Offset: 0x0002CC0F
		public int BuffType
		{
			get
			{
				return this._data.buff;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0002EA1C File Offset: 0x0002CC1C
		public int BodyFrame
		{
			get
			{
				return this._data.bodyFrame;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0002EA29 File Offset: 0x0002CC29
		public int XOffset
		{
			get
			{
				return this._data.xOffset;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0002EA36 File Offset: 0x0002CC36
		public int YOffset
		{
			get
			{
				return this._data.yOffset;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0002EA43 File Offset: 0x0002CC43
		public int PlayerXOFfset
		{
			get
			{
				return this._data.playerXOffset;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0002EA50 File Offset: 0x0002CC50
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0002EA83 File Offset: 0x0002CC83
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0002EAA2 File Offset: 0x0002CCA2
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

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0002EAB9 File Offset: 0x0002CCB9
		public int HeightBoost
		{
			get
			{
				return this._data.heightBoost;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0002EAC6 File Offset: 0x0002CCC6
		public static int GetHeightBoost(int MountType)
		{
			if (MountType <= -1 || MountType >= MountID.Count)
			{
				return 0;
			}
			return Mount.mounts[MountType].heightBoost;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0002EAE4 File Offset: 0x0002CCE4
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
					return Mount.SuperCartRunSpeed;
				}
				return this._data.runSpeed;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0002EC04 File Offset: 0x0002CE04
		public float DashSpeed
		{
			get
			{
				if (this._shouldSuperCart)
				{
					return Mount.SuperCartDashSpeed;
				}
				return this._data.dashSpeed;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0002EC1F File Offset: 0x0002CE1F
		public float Acceleration
		{
			get
			{
				if (this._shouldSuperCart)
				{
					return Mount.SuperCartAcceleration;
				}
				return this._data.acceleration;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0002EC3A File Offset: 0x0002CE3A
		public float FallDamage
		{
			get
			{
				return this._data.fallDamage;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0002EC48 File Offset: 0x0002CE48
		public int JumpHeight(float xVelocity)
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
				num = Mount.SuperCartJumpHeight;
			}
			return num;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0002ECCC File Offset: 0x0002CECC
		public float JumpSpeed(float xVelocity)
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
				num = Mount.SuperCartJumpSpeed;
			}
			return num;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0002ED2C File Offset: 0x0002CF2C
		public bool AutoJump
		{
			get
			{
				return this._data.constantJump;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0002ED39 File Offset: 0x0002CF39
		public bool BlockExtraJumps
		{
			get
			{
				return this._data.blockExtraJumps;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0002ED46 File Offset: 0x0002CF46
		public bool IsConsideredASlimeMount
		{
			get
			{
				return this._type == 3 || this._type == 50;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0002ED5D File Offset: 0x0002CF5D
		public bool Cart
		{
			get
			{
				return this._data != null && this._active && this._data.Minecart;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0002ED7C File Offset: 0x0002CF7C
		public bool Directional
		{
			get
			{
				return this._data == null || this._data.MinecartDirectional;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0002ED93 File Offset: 0x0002CF93
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0002EDAF File Offset: 0x0002CFAF
		public Vector2 Origin
		{
			get
			{
				return new Vector2((float)this._data.textureWidth / 2f, (float)this._data.textureHeight / (2f * (float)this._data.totalFrames));
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0002EDE7 File Offset: 0x0002CFE7
		public bool CanFly()
		{
			return this._active && this._data.flightTimeMax != 0 && this._type != 48;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0002EE0D File Offset: 0x0002D00D
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0002EE4A File Offset: 0x0002D04A
		public bool AbilityCharging
		{
			get
			{
				return this._abilityCharging;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0002EE52 File Offset: 0x0002D052
		public bool AbilityActive
		{
			get
			{
				return this._abilityActive;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0002EE5A File Offset: 0x0002D05A
		public float AbilityCharge
		{
			get
			{
				return (float)this._abilityCharge / (float)this._data.abilityChargeMax;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0002EE70 File Offset: 0x0002D070
		public IEntitySource GetProjectileSpawnSource(Player mountedPlayer)
		{
			return new EntitySource_Mount(mountedPlayer, this.Type);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0002EE80 File Offset: 0x0002D080
		public void StartAbilityCharge(Player mountedPlayer)
		{
			if (Main.myPlayer == mountedPlayer.whoAmI)
			{
				int type = this._type;
				if (type == 9)
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
			else
			{
				int type = this._type;
				if (type == 9)
				{
					this._abilityCharging = true;
				}
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0002EF40 File Offset: 0x0002D140
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

		// Token: 0x06000227 RID: 551 RVA: 0x0002EF87 File Offset: 0x0002D187
		public bool CheckBuff(int buffID)
		{
			return this._data.buff == buffID || this._data.extraBuff == buffID;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0002EFA8 File Offset: 0x0002D1A8
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

		// Token: 0x06000229 RID: 553 RVA: 0x0002F025 File Offset: 0x0002D225
		public void FatigueRecovery()
		{
			if (this._fatigue > 2f)
			{
				this._fatigue -= 2f;
				return;
			}
			this._fatigue = 0f;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0002F052 File Offset: 0x0002D252
		public bool Flight()
		{
			if (this._flyTime <= 0)
			{
				return false;
			}
			this._flyTime--;
			return true;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0002F070 File Offset: 0x0002D270
		public bool AllowDirectionChange
		{
			get
			{
				int type = this._type;
				return type != 9 || this._abilityCooldown < this._data.abilityCooldown / 2;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0002F0A0 File Offset: 0x0002D2A0
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

		// Token: 0x0600022D RID: 557 RVA: 0x0002F140 File Offset: 0x0002D340
		public void UseDrill(Player mountedPlayer)
		{
			if (this._type != 8 || !this._abilityActive)
			{
				return;
			}
			Mount.DrillMountData drillMountData = (Mount.DrillMountData)this._mountSpecificData;
			bool flag = mountedPlayer.whoAmI == Main.myPlayer;
			if (mountedPlayer.controlUseItem)
			{
				int num = 0;
				while (num < Mount.amountOfBeamsAtOnce && drillMountData.beamCooldown == 0)
				{
					for (int i = 0; i < drillMountData.beams.Length; i++)
					{
						Mount.DrillBeam drillBeam = drillMountData.beams[i];
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
									if (WorldGen.InWorld((int)point.X, (int)point.Y, 0) && Main.tile[(int)point.X, (int)point.Y] != null && Main.tile[(int)point.X, (int)point.Y].type == 26 && !Main.hardMode)
									{
										flag2 = false;
										mountedPlayer.Hurt(PlayerDeathReason.ByOther(4), mountedPlayer.statLife / 2, -mountedPlayer.direction, false, false, false, -1, true);
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
								Vector2 vector = new Vector2((float)(point.X << 4) + 8f, (float)(point.Y << 4) + 8f);
								float num2 = (vector - mountedPlayer.Center).ToRotation();
								for (int j = 0; j < 2; j++)
								{
									float num3 = num2 + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.5707964f;
									float num4 = (float)Main.rand.NextDouble() * 2f + 2f;
									Vector2 vector2 = new Vector2((float)Math.Cos((double)num3) * num4, (float)Math.Sin((double)num3) * num4);
									int num5 = Dust.NewDust(vector, 0, 0, 230, vector2.X, vector2.Y, 0, default(Color), 1f);
									Main.dust[num5].noGravity = true;
									Main.dust[num5].customData = mountedPlayer;
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
					num++;
				}
			}
			if (mountedPlayer.controlUseTile)
			{
				int num6 = 0;
				while (num6 < Mount.amountOfBeamsAtOnce && drillMountData.beamCooldown == 0)
				{
					for (int k = 0; k < drillMountData.beams.Length; k++)
					{
						Mount.DrillBeam drillBeam2 = drillMountData.beams[k];
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
								Vector2 vector3 = new Vector2((float)(point2.X << 4) + 8f, (float)(point2.Y << 4) + 8f);
								float num7 = (vector3 - mountedPlayer.Center).ToRotation();
								for (int l = 0; l < 2; l++)
								{
									float num8 = num7 + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.5707964f;
									float num9 = (float)Main.rand.NextDouble() * 2f + 2f;
									Vector2 vector4 = new Vector2((float)Math.Cos((double)num8) * num9, (float)Math.Sin((double)num8) * num9);
									int num10 = Dust.NewDust(vector3, 0, 0, 230, vector4.X, vector4.Y, 0, default(Color), 1f);
									Main.dust[num10].noGravity = true;
									Main.dust[num10].customData = mountedPlayer;
								}
								drillBeam2.cooldown = Mount.drillPickTime;
								drillBeam2.lastPurpose = 1;
								break;
							}
						}
					}
					num6++;
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0002F598 File Offset: 0x0002D798
		private Point16 DrillSmartCursor_Blocks(Player mountedPlayer, Mount.DrillMountData data)
		{
			Vector2 value;
			if (mountedPlayer.whoAmI == Main.myPlayer)
			{
				value = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			}
			else
			{
				value = data.crosshairPosition;
			}
			Vector2 center = mountedPlayer.Center;
			Vector2 value2 = value - center;
			float num = value2.Length();
			if (num > 224f)
			{
				num = 224f;
			}
			num += 32f;
			value2.Normalize();
			Vector2 start = center;
			Vector2 end = center + value2 * num;
			Point16 tilePoint = new Point16(-1, -1);
			if (!Utils.PlotTileLine(start, end, 65.6f, delegate(int x, int y)
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

		// Token: 0x0600022F RID: 559 RVA: 0x0002F66C File Offset: 0x0002D86C
		private Point16 DrillSmartCursor_Walls(Player mountedPlayer, Mount.DrillMountData data)
		{
			Vector2 value;
			if (mountedPlayer.whoAmI == Main.myPlayer)
			{
				value = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			}
			else
			{
				value = data.crosshairPosition;
			}
			Vector2 center = mountedPlayer.Center;
			Vector2 value2 = value - center;
			float num = value2.Length();
			if (num > 224f)
			{
				num = 224f;
			}
			num += 32f;
			num += 16f;
			value2.Normalize();
			Vector2 start = center;
			Vector2 end = center + value2 * num;
			Point16 tilePoint = new Point16(-1, -1);
			if (!Utils.PlotTileLine(start, end, 97.6f, delegate(int x, int y)
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
				return tile != null && (tile.wall <= 0 || !Player.CanPlayerSmashWall(x, y));
			}))
			{
				return tilePoint;
			}
			return new Point16(-1, -1);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0002F74C File Offset: 0x0002D94C
		public void UseAbility(Player mountedPlayer, Vector2 mousePosition, bool toggleOn)
		{
			int type = this._type;
			if (type != 8)
			{
				if (type != 9)
				{
					if (type != 46)
					{
						return;
					}
					if (Main.myPlayer == mountedPlayer.whoAmI)
					{
						if (this._abilityCooldown <= 10)
						{
							int damage = 120;
							Vector2 vector = mountedPlayer.Center + new Vector2((float)(mountedPlayer.width * -(float)mountedPlayer.direction), 26f);
							Vector2 vector2 = new Vector2(0f, -4f).RotatedByRandom(0.10000000149011612);
							Projectile.NewProjectile(this.GetProjectileSpawnSource(mountedPlayer), vector.X, vector.Y, vector2.X, vector2.Y, 930, damage, 0f, Main.myPlayer, 0f, 0f, 0f);
							SoundEngine.PlaySound(SoundID.Item89.SoundId, (int)vector.X, (int)vector.Y, SoundID.Item89.Style, 0.2f, 0f);
						}
						int type2 = 14;
						int damage2 = 100;
						mousePosition = this.ClampToDeadZone(mountedPlayer, mousePosition);
						Vector2 vector3;
						vector3.X = mountedPlayer.position.X + (float)(mountedPlayer.width / 2);
						vector3.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
						Vector2 vector4 = new Vector2(vector3.X + (float)(mountedPlayer.width * mountedPlayer.direction), vector3.Y - 12f);
						Vector2 vector5 = mousePosition - vector4;
						vector5 = vector5.SafeNormalize(Vector2.Zero);
						vector5 *= 12f;
						vector5 = vector5.RotatedByRandom(0.20000000298023224);
						Projectile.NewProjectile(this.GetProjectileSpawnSource(mountedPlayer), vector4.X, vector4.Y, vector5.X, vector5.Y, type2, damage2, 0f, Main.myPlayer, 0f, 0f, 0f);
						SoundEngine.PlaySound(SoundID.Item11.SoundId, (int)vector4.X, (int)vector4.Y, SoundID.Item11.Style, 0.2f, 0f);
						return;
					}
				}
				else if (Main.myPlayer == mountedPlayer.whoAmI)
				{
					int type3 = 606;
					mousePosition = this.ClampToDeadZone(mountedPlayer, mousePosition);
					Vector2 vector6;
					vector6.X = mountedPlayer.position.X + (float)(mountedPlayer.width / 2);
					vector6.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
					int num = (this._frameExtra - 6) * 2;
					for (int i = 0; i < 2; i++)
					{
						Vector2 vector7;
						vector7.Y = vector6.Y + Mount.scutlixEyePositions[num + i].Y + (float)this._data.yOffset;
						if (mountedPlayer.direction == -1)
						{
							vector7.X = vector6.X - Mount.scutlixEyePositions[num + i].X - (float)this._data.xOffset;
						}
						else
						{
							vector7.X = vector6.X + Mount.scutlixEyePositions[num + i].X + (float)this._data.xOffset;
						}
						Vector2 vector8 = mousePosition - vector7;
						vector8.Normalize();
						vector8 *= 14f;
						int damage3 = 150;
						vector7 += vector8;
						Projectile.NewProjectile(this.GetProjectileSpawnSource(mountedPlayer), vector7.X, vector7.Y, vector8.X, vector8.Y, type3, damage3, 0f, Main.myPlayer, 0f, 0f, 0f);
					}
					return;
				}
			}
			else
			{
				if (Main.myPlayer != mountedPlayer.whoAmI)
				{
					this._abilityActive = toggleOn;
					return;
				}
				if (!toggleOn)
				{
					this._abilityActive = false;
					return;
				}
				if (!this._abilityActive)
				{
					if (mountedPlayer.whoAmI == Main.myPlayer)
					{
						float num2 = Main.screenPosition.X + (float)Main.mouseX;
						float num3 = Main.screenPosition.Y + (float)Main.mouseY;
						float ai = num2 - mountedPlayer.position.X;
						float ai2 = num3 - mountedPlayer.position.Y;
						Projectile.NewProjectile(this.GetProjectileSpawnSource(mountedPlayer), num2, num3, 0f, 0f, 453, 0, 0f, mountedPlayer.whoAmI, ai, ai2, 0f);
					}
					this._abilityActive = true;
					return;
				}
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0002FBC0 File Offset: 0x0002DDC0
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
					if (num4 - num6 < this._data.acceleration)
					{
						num6 = num4;
					}
					else
					{
						num6 += this._data.acceleration * num;
					}
				}
				else if (num6 > num5)
				{
					if (num6 - num5 < this._data.acceleration)
					{
						num6 = num5;
					}
					else
					{
						num6 -= this._data.acceleration * num;
					}
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
				float num11 = drillMountData.outerRingRotation;
				num11 += mountedPlayer.velocity.X / 80f;
				if (num11 > 3.1415927f)
				{
					num11 -= 6.2831855f;
				}
				else if (num11 < -3.1415927f)
				{
					num11 += 6.2831855f;
				}
				drillMountData.outerRingRotation = num11;
			}
			else if (this._type == 23)
			{
				float num12 = -mountedPlayer.velocity.Y / this._data.dashSpeed;
				num12 = MathHelper.Clamp(num12, -1f, 1f);
				float num13 = mountedPlayer.velocity.X / this._data.dashSpeed;
				num13 = MathHelper.Clamp(num13, -1f, 1f);
				float num14 = -0.19634955f * num12 * (float)mountedPlayer.direction;
				float num15 = 0.19634955f * num13;
				float fullRotation3 = num14 + num15;
				mountedPlayer.fullRotation = fullRotation3;
				mountedPlayer.fullRotationOrigin = new Vector2((float)(mountedPlayer.width / 2), (float)mountedPlayer.height);
			}
			return true;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0003018F File Offset: 0x0002E38F
		private bool DoesHoverIgnoresFatigue()
		{
			return this._type == 7 || this._type == 8 || this._type == 12 || this._type == 23 || this._type == 44 || this._type == 49;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000301D0 File Offset: 0x0002E3D0
		private float GetWitchBroomTrinketRotation(Player player)
		{
			float num = Utils.Clamp<float>(player.velocity.X / 10f, -1f, 1f);
			Point point = player.Center.ToTileCoordinates();
			float num2 = 0.5f;
			if (WorldGen.InAPlaceWithWind(point.X, point.Y, 1, 1))
			{
				num2 = 1f;
			}
			float num3 = (float)Math.Sin((double)((float)player.miscCounter / 300f * 6.2831855f * 3f)) * 0.7853982f * Math.Abs(Main.WindForVisuals) * 0.5f + 0.7853982f * -Main.WindForVisuals * 0.5f;
			num3 *= num2;
			return num * (float)Math.Sin((double)((float)player.miscCounter / 150f * 6.2831855f * 3f)) * 0.7853982f * 0.5f + num * 0.7853982f * 0.5f + num3;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000302BE File Offset: 0x0002E4BE
		private Vector2 GetWitchBroomTrinketOriginOffset(Player player)
		{
			return new Vector2((float)(29 * player.direction), -4f);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000302D4 File Offset: 0x0002E4D4
		public void UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
		{
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
			int type = this._type;
			switch (type)
			{
			case 5:
				if (state != 2)
				{
					this._frameExtra = 0;
					this._frameExtraCounter = 0f;
					goto IL_13EB;
				}
				goto IL_13EB;
			case 6:
			case 11:
			case 12:
			case 13:
			case 15:
			case 16:
				goto IL_13EB;
			case 7:
				state = 2;
				goto IL_13EB;
			case 8:
			{
				if (state != 0 && state != 1)
				{
					goto IL_13EB;
				}
				Vector2 vector;
				vector.X = mountedPlayer.position.X;
				vector.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
				int num = (int)(vector.X / 16f);
				float num2 = vector.Y / 16f;
				float num3 = 0f;
				float num4 = (float)mountedPlayer.width;
				while (num4 > 0f)
				{
					float num5 = (float)((num + 1) * 16) - vector.X;
					if (num5 > num4)
					{
						num5 = num4;
					}
					num3 += Collision.GetTileRotation(vector) * num5;
					num4 -= num5;
					vector.X += num5;
					num++;
				}
				float num6 = num3 / (float)mountedPlayer.width - mountedPlayer.fullRotation;
				float num7 = 0f;
				float num8 = 0.15707964f;
				if (num6 < 0f)
				{
					if (num6 > -num8)
					{
						num7 = num6;
					}
					else
					{
						num7 = -num8;
					}
				}
				else if (num6 > 0f)
				{
					if (num6 < num8)
					{
						num7 = num6;
					}
					else
					{
						num7 = num8;
					}
				}
				if (num7 == 0f)
				{
					goto IL_13EB;
				}
				mountedPlayer.fullRotation += num7;
				if (mountedPlayer.fullRotation > 0.7853982f)
				{
					mountedPlayer.fullRotation = 0.7853982f;
				}
				if (mountedPlayer.fullRotation < -0.7853982f)
				{
					mountedPlayer.fullRotation = -0.7853982f;
					goto IL_13EB;
				}
				goto IL_13EB;
			}
			case 9:
				if (this._aiming)
				{
					goto IL_13EB;
				}
				this._frameExtraCounter += 1f;
				if (this._frameExtraCounter < 12f)
				{
					goto IL_13EB;
				}
				this._frameExtraCounter = 0f;
				this._frameExtra++;
				if (this._frameExtra >= 6)
				{
					this._frameExtra = 0;
					goto IL_13EB;
				}
				goto IL_13EB;
			case 10:
				break;
			case 14:
			{
				bool flag = Math.Abs(velocity.X) > this.RunSpeed / 2f;
				float num9 = (float)Math.Sign(mountedPlayer.velocity.X);
				float num10 = 12f;
				float num11 = 40f;
				if (!flag)
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
				if (flag && velocity.Y == 0f)
				{
					for (int i = 0; i < 2; i++)
					{
						Dust dust = Main.dust[Dust.NewDust(mountedPlayer.BottomLeft, mountedPlayer.width, 6, 31, 0f, 0f, 0, default(Color), 1f)];
						dust.velocity = new Vector2(velocity.X * 0.15f, Main.rand.NextFloat() * -2f);
						dust.noLight = true;
						dust.scale = 0.5f + Main.rand.NextFloat() * 0.8f;
						dust.fadeIn = 0.5f + Main.rand.NextFloat() * 1f;
						dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
					}
					if (mountedPlayer.cMount == 0)
					{
						mountedPlayer.position += new Vector2(num9 * 24f, 0f);
						mountedPlayer.FloorVisuals(true);
						mountedPlayer.position -= new Vector2(num9 * 24f, 0f);
					}
				}
				if (num9 == (float)mountedPlayer.direction)
				{
					for (int j = 0; j < (int)(3f * mountedPlayer.basiliskCharge); j++)
					{
						Dust dust2 = Main.dust[Dust.NewDust(mountedPlayer.BottomLeft, mountedPlayer.width, 6, 6, 0f, 0f, 0, default(Color), 1f)];
						Vector2 value = mountedPlayer.Center + new Vector2(num9 * num11, num10);
						dust2.position = mountedPlayer.Center + new Vector2(num9 * (num11 - 2f), num10 - 6f + Main.rand.NextFloat() * 12f);
						dust2.velocity = (dust2.position - value).SafeNormalize(Vector2.Zero) * (3.5f + Main.rand.NextFloat() * 0.5f);
						if (dust2.velocity.Y < 0f)
						{
							Dust dust3 = dust2;
							dust3.velocity.Y = dust3.velocity.Y * (1f + 2f * Main.rand.NextFloat());
						}
						dust2.velocity += mountedPlayer.velocity * 0.55f;
						dust2.velocity *= mountedPlayer.velocity.Length() / this.RunSpeed;
						dust2.velocity *= mountedPlayer.basiliskCharge;
						dust2.noGravity = true;
						dust2.noLight = true;
						dust2.scale = 0.5f + Main.rand.NextFloat() * 0.8f;
						dust2.fadeIn = 0.5f + Main.rand.NextFloat() * 1f;
						dust2.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
					}
					goto IL_13EB;
				}
				goto IL_13EB;
			}
			case 17:
				this.UpdateFrame_GolfCart(mountedPlayer, state, velocity);
				goto IL_13EB;
			default:
				switch (type)
				{
				case 39:
					this._frameExtraCounter += 1f;
					if (this._frameExtraCounter <= 6f)
					{
						goto IL_13EB;
					}
					this._frameExtraCounter = 0f;
					this._frameExtra++;
					if (this._frameExtra > 5)
					{
						this._frameExtra = 0;
						goto IL_13EB;
					}
					goto IL_13EB;
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
					goto IL_13EB;
				case 44:
				{
					state = 1;
					bool flag2 = Math.Abs(velocity.X) > this.DashSpeed - this.RunSpeed / 4f;
					if (this._mountSpecificData == null)
					{
						this._mountSpecificData = false;
					}
					bool flag3 = (bool)this._mountSpecificData;
					if (flag3 && !flag2)
					{
						this._mountSpecificData = false;
					}
					else if (!flag3 && flag2)
					{
						this._mountSpecificData = true;
						Vector2 vector2 = mountedPlayer.Center + new Vector2((float)(mountedPlayer.width * mountedPlayer.direction), 0f);
						Vector2 value2 = new Vector2(40f, 30f);
						float num12 = 6.2831855f * Main.rand.NextFloat();
						for (float num13 = 0f; num13 < 20f; num13 += 1f)
						{
							Dust dust4 = Main.dust[Dust.NewDust(vector2, 0, 0, 228, 0f, 0f, 0, default(Color), 1f)];
							Vector2 vector3 = Vector2.UnitY.RotatedBy((double)(num13 * 6.2831855f / 20f + num12), default(Vector2));
							vector3 *= 0.8f;
							dust4.position = vector2 + vector3 * value2;
							dust4.velocity = vector3 + new Vector2(this.RunSpeed - (float)Math.Sign(velocity.Length()), 0f);
							if (velocity.X > 0f)
							{
								Dust dust5 = dust4;
								dust5.velocity.X = dust5.velocity.X * -1f;
							}
							if (Main.rand.Next(2) == 0)
							{
								dust4.velocity *= 0.5f;
							}
							dust4.noGravity = true;
							dust4.scale = 1.5f + Main.rand.NextFloat() * 0.8f;
							dust4.fadeIn = 0f;
							dust4.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
						}
					}
					int num14 = (int)this.RunSpeed - (int)Math.Abs(velocity.X);
					if (num14 <= 0)
					{
						num14 = 1;
					}
					if (Main.rand.Next(num14) == 0)
					{
						int num15 = 22;
						int num16 = mountedPlayer.width / 2 + 10;
						Vector2 bottom = mountedPlayer.Bottom;
						bottom.X -= (float)num16;
						bottom.Y -= (float)(num15 - 6);
						Dust dust6 = Main.dust[Dust.NewDust(bottom, num16 * 2, num15, 228, 0f, 0f, 0, default(Color), 1f)];
						dust6.velocity = Vector2.Zero;
						dust6.noGravity = true;
						dust6.noLight = true;
						dust6.scale = 0.25f + Main.rand.NextFloat() * 0.8f;
						dust6.fadeIn = 0.5f + Main.rand.NextFloat() * 2f;
						dust6.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
						goto IL_13EB;
					}
					goto IL_13EB;
				}
				case 45:
				{
					bool flag4 = Math.Abs(velocity.X) > this.DashSpeed * 0.9f;
					if (this._mountSpecificData == null)
					{
						this._mountSpecificData = false;
					}
					bool flag5 = (bool)this._mountSpecificData;
					if (flag5 && !flag4)
					{
						this._mountSpecificData = false;
					}
					else if (!flag5 && flag4)
					{
						this._mountSpecificData = true;
						Vector2 vector4 = mountedPlayer.Center + new Vector2((float)(mountedPlayer.width * mountedPlayer.direction), 0f);
						Vector2 value3 = new Vector2(40f, 30f);
						float num17 = 6.2831855f * Main.rand.NextFloat();
						for (float num18 = 0f; num18 < 20f; num18 += 1f)
						{
							Dust dust7 = Main.dust[Dust.NewDust(vector4, 0, 0, 6, 0f, 0f, 0, default(Color), 1f)];
							Vector2 vector5 = Vector2.UnitY.RotatedBy((double)(num18 * 6.2831855f / 20f + num17), default(Vector2));
							vector5 *= 0.8f;
							dust7.position = vector4 + vector5 * value3;
							dust7.velocity = vector5 + new Vector2(this.RunSpeed - (float)Math.Sign(velocity.Length()), 0f);
							if (velocity.X > 0f)
							{
								Dust dust8 = dust7;
								dust8.velocity.X = dust8.velocity.X * -1f;
							}
							if (Main.rand.Next(2) == 0)
							{
								dust7.velocity *= 0.5f;
							}
							dust7.noGravity = true;
							dust7.scale = 1.5f + Main.rand.NextFloat() * 0.8f;
							dust7.fadeIn = 0f;
							dust7.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
						}
					}
					if (flag4)
					{
						int num19 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
						{
							6,
							6,
							31
						});
						int num20 = 6;
						Dust dust9 = Main.dust[Dust.NewDust(mountedPlayer.Center - new Vector2((float)num20, (float)(num20 - 12)), num20 * 2, num20 * 2, num19, 0f, 0f, 0, default(Color), 1f)];
						dust9.velocity = mountedPlayer.velocity * 0.1f;
						if (Main.rand.Next(2) == 0)
						{
							dust9.noGravity = true;
						}
						dust9.scale = 0.7f + Main.rand.NextFloat() * 0.8f;
						if (Main.rand.Next(3) == 0)
						{
							dust9.fadeIn = 0.1f;
						}
						if (num19 == 31)
						{
							dust9.noGravity = true;
							dust9.scale *= 1.5f;
							dust9.fadeIn = 0.2f;
						}
						dust9.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
						goto IL_13EB;
					}
					goto IL_13EB;
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
							goto IL_13EB;
						}
						if (this._frameExtra < 12)
						{
							this._frameExtra = 12;
						}
						this._frameExtraCounter += Math.Abs(velocity.X);
						if (this._frameExtraCounter < 8f)
						{
							goto IL_13EB;
						}
						this._frameExtraCounter = 0f;
						this._frameExtra++;
						if (this._frameExtra >= 24)
						{
							this._frameExtra = 12;
							goto IL_13EB;
						}
						goto IL_13EB;
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
							goto IL_13EB;
						}
						this._frameExtraCounter = 0f;
						this._frameExtra++;
						if (this._frameExtra >= 27)
						{
							this._frameExtra = 24;
							goto IL_13EB;
						}
						goto IL_13EB;
					}
					break;
				case 48:
					state = 1;
					goto IL_13EB;
				case 49:
				{
					if (state != 4 && mountedPlayer.wet)
					{
						state = (this._frameState = 4);
					}
					velocity.Length();
					float num21 = mountedPlayer.velocity.Y * 0.05f;
					if (mountedPlayer.direction < 0)
					{
						num21 *= -1f;
					}
					mountedPlayer.fullRotation = num21;
					mountedPlayer.fullRotationOrigin = new Vector2((float)(mountedPlayer.width / 2), (float)(mountedPlayer.height / 2));
					goto IL_13EB;
				}
				case 50:
					if (mountedPlayer.velocity.Y == 0f)
					{
						this._frameExtraCounter = 0f;
						this._frameExtra = 3;
						goto IL_13EB;
					}
					this._frameExtraCounter += 1f;
					if (this.Flight())
					{
						this._frameExtraCounter += 1f;
					}
					if (this._frameExtraCounter <= 7f)
					{
						goto IL_13EB;
					}
					this._frameExtraCounter = 0f;
					this._frameExtra++;
					if (this._frameExtra > 3)
					{
						this._frameExtra = 0;
						goto IL_13EB;
					}
					goto IL_13EB;
				case 51:
					goto IL_13EB;
				case 52:
					if (state == 4)
					{
						state = 2;
						goto IL_13EB;
					}
					goto IL_13EB;
				default:
					goto IL_13EB;
				}
				break;
			}
			bool flag6 = Math.Abs(velocity.X) > this.DashSpeed - this.RunSpeed / 2f;
			if (state == 1)
			{
				bool flag7 = false;
				if (flag6)
				{
					state = 5;
					if (this._frameExtra < 6)
					{
						flag7 = true;
					}
					this._frameExtra++;
				}
				else
				{
					this._frameExtra = 0;
				}
				if ((this._type == 10 || this._type == 47) && flag7)
				{
					int type2 = 6;
					if (this._type == 10)
					{
						type2 = Utils.SelectRandom<int>(Main.rand, new int[]
						{
							176,
							177,
							179
						});
					}
					Vector2 vector6 = mountedPlayer.Center + new Vector2((float)(mountedPlayer.width * mountedPlayer.direction), 0f);
					Vector2 value4 = new Vector2(40f, 30f);
					float num22 = 6.2831855f * Main.rand.NextFloat();
					for (float num23 = 0f; num23 < 14f; num23 += 1f)
					{
						Dust dust10 = Main.dust[Dust.NewDust(vector6, 0, 0, type2, 0f, 0f, 0, default(Color), 1f)];
						Vector2 vector7 = Vector2.UnitY.RotatedBy((double)(num23 * 6.2831855f / 14f + num22), default(Vector2));
						vector7 *= 0.2f * (float)this._frameExtra;
						dust10.position = vector6 + vector7 * value4;
						dust10.velocity = vector7 + new Vector2(this.RunSpeed - (float)(Math.Sign(velocity.X) * this._frameExtra * 2), 0f);
						dust10.noGravity = true;
						if (this._type == 47)
						{
							dust10.noLightEmittence = true;
						}
						dust10.scale = 1f + Main.rand.NextFloat() * 0.8f;
						dust10.fadeIn = Main.rand.NextFloat() * 2f;
						dust10.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
					}
				}
			}
			if (this._type == 10 && flag6)
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
			if (this._type == 47 && flag6 && velocity.Y == 0f)
			{
				int num24 = (int)mountedPlayer.Center.X / 16;
				int num25 = (int)(mountedPlayer.position.Y + (float)mountedPlayer.height - 1f) / 16;
				Tile tile = Main.tile[num24, num25 + 1];
				if (tile != null && tile.active() && tile.liquid == 0 && WorldGen.SolidTileAllowBottomSlope(num24, num25 + 1))
				{
					ParticleOrchestrator.RequestParticleSpawn(true, ParticleOrchestraType.WallOfFleshGoatMountFlames, new ParticleOrchestraSettings
					{
						PositionInWorld = new Vector2((float)(num24 * 16 + 8), (float)(num25 * 16 + 16))
					}, new int?(mountedPlayer.whoAmI));
				}
			}
			IL_13EB:
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
					float num26 = (float)this._data.idleFrameDelay;
					if (this._type == 5)
					{
						num26 *= 2f - 1f * this._fatigue / this._fatigueMax;
					}
					int num27 = (int)((float)(this._idleTime - this._idleTimeNext) / num26);
					int idleFrameCount = this._data.idleFrameCount;
					if (num27 >= idleFrameCount)
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
						this._frame = this._data.idleFrameStart + num27;
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
				type = this._type;
				float num28;
				if (type <= 9)
				{
					if (type == 6)
					{
						num28 = (this._flipDraw ? velocity.X : (-velocity.X));
						goto IL_17FE;
					}
					if (type != 9)
					{
						goto IL_17F1;
					}
				}
				else
				{
					if (type == 13)
					{
						num28 = (this._flipDraw ? velocity.X : (-velocity.X));
						goto IL_17FE;
					}
					switch (type)
					{
					case 44:
						num28 = Math.Max(1f, Math.Abs(velocity.X) * 0.25f);
						goto IL_17FE;
					case 45:
					case 47:
					case 49:
						goto IL_17F1;
					case 46:
						break;
					case 48:
						num28 = Math.Max(0.5f, velocity.Length() * 0.125f);
						goto IL_17FE;
					case 50:
						num28 = Math.Abs(velocity.X) * 0.5f;
						goto IL_17FE;
					default:
						goto IL_17F1;
					}
				}
				if (this._flipDraw)
				{
					num28 = -Math.Abs(velocity.X);
					goto IL_17FE;
				}
				num28 = Math.Abs(velocity.X);
				goto IL_17FE;
				IL_17F1:
				num28 = Math.Abs(velocity.X);
				IL_17FE:
				this._frameCounter += num28;
				if (num28 >= 0f)
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
					float num29 = this._fatigue / this._fatigueMax;
					this._frameExtraCounter += 6f - 4f * num29;
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
					float num30 = mountedPlayer.velocity.Length();
					if (num30 < 1f)
					{
						this._frame = 0;
						this._frameCounter = 0f;
						return;
					}
					if (num30 > 5f)
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
				int num31 = (int)((Math.Abs(velocity.X) + Math.Abs(velocity.Y)) / 2f);
				this._frameCounter += (float)num31;
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
				type = this._type;
				float num28;
				if (type != 6)
				{
					if (type != 9)
					{
						if (type != 13)
						{
							num28 = Math.Abs(velocity.X);
						}
						else
						{
							num28 = (this._flipDraw ? velocity.X : (-velocity.X));
						}
					}
					else if (this._flipDraw)
					{
						num28 = -Math.Abs(velocity.X);
					}
					else
					{
						num28 = Math.Abs(velocity.X);
					}
				}
				else
				{
					num28 = (this._flipDraw ? velocity.X : (-velocity.X));
				}
				this._frameCounter += num28;
				if (num28 >= 0f)
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

		// Token: 0x06000236 RID: 566 RVA: 0x0003212C File Offset: 0x0003032C
		public void TryBeginningFlight(Player mountedPlayer, int state)
		{
			if (this._frameState == state)
			{
				return;
			}
			if (state != 2 && state != 3)
			{
				return;
			}
			if (!this.CanHover())
			{
				return;
			}
			if (mountedPlayer.controlUp || mountedPlayer.controlDown || mountedPlayer.controlJump)
			{
				return;
			}
			Vector2 velocity = Vector2.UnitY * mountedPlayer.gravDir;
			if (Collision.TileCollision(mountedPlayer.position + new Vector2(0f, -0.001f), velocity, mountedPlayer.width, mountedPlayer.height, false, false, (int)mountedPlayer.gravDir).Y == 0f)
			{
				return;
			}
			if (this.DoesHoverIgnoresFatigue())
			{
				mountedPlayer.position.Y = mountedPlayer.position.Y + -0.001f;
				return;
			}
			float num = mountedPlayer.gravity * mountedPlayer.gravDir;
			mountedPlayer.position.Y = mountedPlayer.position.Y - mountedPlayer.velocity.Y;
			mountedPlayer.velocity.Y = mountedPlayer.velocity.Y - num;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0003221C File Offset: 0x0003041C
		public int GetIntendedGroundedFrame(Player mountedPlayer)
		{
			if (mountedPlayer.velocity.X == 0f || ((mountedPlayer.slippy || mountedPlayer.slippy2 || mountedPlayer.windPushed) && !mountedPlayer.controlLeft && !mountedPlayer.controlRight))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00032270 File Offset: 0x00030470
		public void TryLanding(Player mountedPlayer)
		{
			if (this._frameState != 3 && this._frameState != 2)
			{
				return;
			}
			if (mountedPlayer.controlUp || mountedPlayer.controlDown || mountedPlayer.controlJump)
			{
				return;
			}
			Vector2 velocity = Vector2.UnitY * mountedPlayer.gravDir * 4f;
			if (Collision.TileCollision(mountedPlayer.position, velocity, mountedPlayer.width, mountedPlayer.height, false, false, (int)mountedPlayer.gravDir).Y != 0f)
			{
				return;
			}
			this.UpdateFrame(mountedPlayer, this.GetIntendedGroundedFrame(mountedPlayer), mountedPlayer.velocity);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0003230C File Offset: 0x0003050C
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
					float num = this._frameExtraCounter + 1f;
					this._frameExtraCounter = num;
					if (num >= 6f)
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
				else
				{
					float num = this._frameExtraCounter + 1f;
					this._frameExtraCounter = num;
					if (num >= 6f)
					{
						this._frameExtraCounter = 0f;
						if (this._frameExtra < 2)
						{
							this._frameExtra++;
						}
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

		// Token: 0x0600023A RID: 570 RVA: 0x000324B0 File Offset: 0x000306B0
		private static void EmitGolfCartSmoke(Player mountedPlayer, bool rushing)
		{
			Vector2 position = mountedPlayer.Bottom + new Vector2((float)(-(float)mountedPlayer.direction * 34), -mountedPlayer.gravDir * 12f);
			Dust dust = Dust.NewDustDirect(position, 0, 0, 31, (float)(-(float)mountedPlayer.direction), -mountedPlayer.gravDir * 0.24f, 100, default(Color), 1f);
			dust.position = position;
			dust.velocity *= 0.1f;
			dust.velocity += new Vector2((float)(-(float)mountedPlayer.direction), -mountedPlayer.gravDir * 0.25f);
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

		// Token: 0x0600023B RID: 571 RVA: 0x000325BC File Offset: 0x000307BC
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
				Vector2 value = (num + num2 * (num5 - (float)(num3 / 2))).ToRotationVector2();
				Utils.PlotTileLine(worldLocation, worldLocation + value * num4, 8f, new Utils.TileActionAttempt(DelegateMethods.CastLightOpen_StopForSolids_ScaleWithDistance));
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0003266D File Offset: 0x0003086D
		private static bool ShouldGolfCartEmitLight()
		{
			return true;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00032670 File Offset: 0x00030870
		private static void EmitGolfCartWheelDust(Player mountedPlayer, Vector2 legSpot)
		{
			if (Main.rand.Next(5) != 0)
			{
				return;
			}
			Point point = (legSpot + new Vector2(0f, mountedPlayer.gravDir * 2f)).ToTileCoordinates();
			if (!WorldGen.InWorld(point.X, point.Y, 10))
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(point.X, point.Y);
			if (!WorldGen.SolidTile(point))
			{
				return;
			}
			int num = WorldGen.KillTile_GetTileDustAmount(true, tileSafely);
			if (num > 1)
			{
				num = 1;
			}
			Vector2 value = new Vector2((float)(-(float)mountedPlayer.direction), -mountedPlayer.gravDir * 1f);
			for (int i = 0; i < num; i++)
			{
				Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(point.X, point.Y, tileSafely)];
				dust.velocity *= 0.2f;
				dust.velocity += value;
				dust.position = legSpot;
				dust.scale *= 0.8f;
				dust.fadeIn *= 0.8f;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00032784 File Offset: 0x00030984
		private void DoGemMinecartEffect(Player mountedPlayer, int dustType)
		{
			if (Main.rand.Next(10) != 0)
			{
				return;
			}
			Vector2 value = Main.rand.NextVector2Square(-1f, 1f) * new Vector2(22f, 10f);
			Vector2 value2 = new Vector2(0f, 10f) * mountedPlayer.Directions;
			Vector2 vector = mountedPlayer.Center + value2 + value;
			vector = mountedPlayer.RotatedRelativePoint(vector, false, true);
			Dust dust = Dust.NewDustPerfect(vector, dustType, null, 0, default(Color), 1f);
			dust.noGravity = true;
			dust.fadeIn = 0.6f;
			dust.scale = 0.4f;
			dust.velocity *= 0.25f;
			dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0003286C File Offset: 0x00030A6C
		private void DoSteamMinecartEffect(Player mountedPlayer, int dustType)
		{
			float num = Math.Abs(mountedPlayer.velocity.X);
			if (num < 1f || (num < 6f && this._frame != 0))
			{
				return;
			}
			Vector2 value = Main.rand.NextVector2Square(-1f, 1f) * new Vector2(3f, 3f);
			Vector2 value2 = new Vector2(-10f, -4f) * mountedPlayer.Directions;
			Vector2 vector = mountedPlayer.Center + value2 + value;
			vector = mountedPlayer.RotatedRelativePoint(vector, false, true);
			Dust dust = Dust.NewDustPerfect(vector, dustType, null, 0, default(Color), 1f);
			dust.noGravity = true;
			dust.fadeIn = 0.6f;
			dust.scale = 1.8f;
			dust.velocity *= 0.25f;
			dust.velocity.Y = dust.velocity.Y - 2f;
			dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00032984 File Offset: 0x00030B84
		private void DoExhaustMinecartEffect(Player mountedPlayer, int dustType)
		{
			float num = mountedPlayer.velocity.Length();
			if (num < 1f && Main.rand.Next(4) != 0)
			{
				return;
			}
			int i = 1 + (int)num / 6;
			while (i > 0)
			{
				i--;
				Vector2 value = Main.rand.NextVector2Square(-1f, 1f) * new Vector2(3f, 3f);
				Vector2 vector = new Vector2(-18f, 20f) * mountedPlayer.Directions;
				if (num > 6f)
				{
					vector.X += (float)(4 * mountedPlayer.direction);
				}
				if (i > 0)
				{
					vector += mountedPlayer.velocity * (float)(i / 3);
				}
				Vector2 vector2 = mountedPlayer.Center + vector + value;
				vector2 = mountedPlayer.RotatedRelativePoint(vector2, false, true);
				Dust dust = Dust.NewDustPerfect(vector2, dustType, null, 0, default(Color), 1f);
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

		// Token: 0x06000241 RID: 577 RVA: 0x00032AFC File Offset: 0x00030CFC
		private void DoConfettiMinecartEffect(Player mountedPlayer)
		{
			float num = mountedPlayer.velocity.Length();
			if ((num < 1f && Main.rand.Next(6) != 0) || (num < 3f && Main.rand.Next(3) != 0))
			{
				return;
			}
			int i = 1 + (int)num / 6;
			while (i > 0)
			{
				i--;
				float num2 = Main.rand.NextFloat() * 2f;
				Vector2 value = Main.rand.NextVector2Square(-1f, 1f) * new Vector2(3f, 8f);
				Vector2 vector = new Vector2(-18f, 4f) * mountedPlayer.Directions;
				vector.X += num * (float)mountedPlayer.direction * 0.5f + (float)(mountedPlayer.direction * i) * num2;
				if (i > 0)
				{
					vector += mountedPlayer.velocity * (float)(i / 3);
				}
				Vector2 vector2 = mountedPlayer.Center + vector + value;
				vector2 = mountedPlayer.RotatedRelativePoint(vector2, false, true);
				Dust dust = Dust.NewDustPerfect(vector2, 139 + Main.rand.Next(4), null, 0, default(Color), 1f);
				dust.noGravity = true;
				dust.fadeIn = 0.6f;
				dust.scale = 0.5f + num2 / 2f;
				dust.velocity *= 0.2f;
				if (num < 1f)
				{
					Dust dust2 = dust;
					dust2.velocity.X = dust2.velocity.X - 0.5f * (float)mountedPlayer.direction;
				}
				dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00032CC0 File Offset: 0x00030EC0
		public void UpdateEffects(Player mountedPlayer)
		{
			mountedPlayer.autoJump = this.AutoJump;
			this._shouldSuperCart = (MountID.Sets.Cart[this._type] && mountedPlayer.UsingSuperCart);
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
				Vector2 vector = center;
				bool flag = false;
				float num2 = 1500f;
				float num3 = 850f;
				for (int i = 0; i < 200; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy(this, false))
					{
						Vector2 v = npc.Center - center;
						float num4 = v.Length();
						if (num4 < num3 && ((Vector2.Distance(vector, center) > num4 && num4 < num2) || !flag))
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
							if (Collision.CanHitLine(center, 0, 0, npc.position, npc.width, npc.height) && flag2)
							{
								num2 = num4;
								vector = npc.Center;
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
					this.AimAbility(mountedPlayer, vector);
					if (this._abilityCooldown == 0)
					{
						this.StopAbilityCharge();
					}
					this.UseAbility(mountedPlayer, vector, false);
					return;
				}
				this.AimAbility(mountedPlayer, vector);
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
				Vector3 vector2 = new Vector3(0.4f, 0.12f, 0.15f);
				float scaleFactor = 1f + Math.Abs(mountedPlayer.velocity.X) / this.RunSpeed * 2.5f;
				int num6 = Math.Sign(mountedPlayer.velocity.X);
				if (num6 == 0)
				{
					num6 = mountedPlayer.direction;
				}
				if (Main.netMode != 2)
				{
					vector2 *= scaleFactor;
					Lighting.AddLight(mountedPlayer.Center, vector2.X, vector2.Y, vector2.Z);
					Lighting.AddLight(mountedPlayer.Top, vector2.X, vector2.Y, vector2.Z);
					Lighting.AddLight(mountedPlayer.Bottom, vector2.X, vector2.Y, vector2.Z);
					Lighting.AddLight(mountedPlayer.Left, vector2.X, vector2.Y, vector2.Z);
					Lighting.AddLight(mountedPlayer.Right, vector2.X, vector2.Y, vector2.Z);
					float num7 = -24f;
					if (mountedPlayer.direction != num6)
					{
						num7 = -22f;
					}
					if (num6 == -1)
					{
						num7 += 1f;
					}
					Vector2 value = new Vector2(num7 * (float)num6, -19f).RotatedBy((double)mountedPlayer.fullRotation, default(Vector2));
					Vector2 vector3 = new Vector2(MathHelper.Lerp(0f, -8f, mountedPlayer.fullRotation / 0.7853982f), MathHelper.Lerp(0f, 2f, Math.Abs(mountedPlayer.fullRotation / 0.7853982f))).RotatedBy((double)mountedPlayer.fullRotation, default(Vector2));
					if (num6 == Math.Sign(mountedPlayer.fullRotation))
					{
						vector3 *= MathHelper.Lerp(1f, 0.6f, Math.Abs(mountedPlayer.fullRotation / 0.7853982f));
					}
					Vector2 vector4 = mountedPlayer.Bottom + value + vector3;
					Vector2 vector5 = mountedPlayer.oldPosition + mountedPlayer.Size * new Vector2(0.5f, 1f) + value + vector3;
					if (Vector2.Distance(vector4, vector5) > 3f)
					{
						int num8 = (int)Vector2.Distance(vector4, vector5) / 3;
						if (Vector2.Distance(vector4, vector5) % 3f != 0f)
						{
							num8++;
						}
						for (float num9 = 1f; num9 <= (float)num8; num9 += 1f)
						{
							Dust dust = Main.dust[Dust.NewDust(mountedPlayer.Center, 0, 0, 182, 0f, 0f, 0, default(Color), 1f)];
							dust.position = Vector2.Lerp(vector5, vector4, num9 / (float)num8);
							dust.noGravity = true;
							dust.velocity = Vector2.Zero;
							dust.customData = mountedPlayer;
							dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
						}
						return;
					}
					Dust dust2 = Main.dust[Dust.NewDust(mountedPlayer.Center, 0, 0, 182, 0f, 0f, 0, default(Color), 1f)];
					dust2.position = vector4;
					dust2.noGravity = true;
					dust2.velocity = Vector2.Zero;
					dust2.customData = mountedPlayer;
					dust2.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
					return;
				}
				break;
			}
			case 12:
				if (mountedPlayer.MountFishronSpecial)
				{
					Vector3 vector6 = Colors.CurrentLiquidColor.ToVector3();
					vector6 *= 0.4f;
					Point point = (mountedPlayer.Center + Vector2.UnitX * (float)mountedPlayer.direction * 20f + mountedPlayer.velocity * 10f).ToTileCoordinates();
					if (!WorldGen.SolidTile(point.X, point.Y, false))
					{
						Lighting.AddLight(point.X, point.Y, vector6.X, vector6.Y, vector6.Z);
					}
					else
					{
						Lighting.AddLight(mountedPlayer.Center + Vector2.UnitX * (float)mountedPlayer.direction * 20f, vector6.X, vector6.Y, vector6.Z);
					}
					mountedPlayer.meleeDamage += 0.15f;
					mountedPlayer.rangedDamage += 0.15f;
					mountedPlayer.magicDamage += 0.15f;
					mountedPlayer.minionDamage += 0.15f;
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
				Vector2 vector7 = mountedPlayer.Center + new Vector2(20f, 10f) * mountedPlayer.Directions;
				Vector2 vector8 = vector7 + mountedPlayer.velocity;
				Vector2 vector9 = vector7 + new Vector2(-1f, -0.5f) * mountedPlayer.Directions;
				vector7 = mountedPlayer.RotatedRelativePoint(vector7, false, true);
				vector8 = mountedPlayer.RotatedRelativePoint(vector8, false, true);
				vector9 = mountedPlayer.RotatedRelativePoint(vector9, false, true);
				Vector2 value2 = mountedPlayer.shadowPos[2] - mountedPlayer.position + vector7;
				Vector2 value3 = vector8 - vector7;
				vector7 += value3;
				value2 += value3;
				Vector2 value4 = vector8 - vector9;
				float num10 = MathHelper.Clamp(mountedPlayer.velocity.Length() / 5f, 0f, 1f);
				for (float num11 = 0f; num11 <= 1f; num11 += 0.1f)
				{
					if (Main.rand.NextFloat() >= num10)
					{
						Dust dust3 = Dust.NewDustPerfect(Vector2.Lerp(value2, vector7, num11), 65, new Vector2?(Main.rand.NextVector2Circular(0.5f, 0.5f) * num10), 0, default(Color), 1f);
						dust3.scale = 0.6f;
						dust3.fadeIn = 0f;
						dust3.customData = mountedPlayer;
						dust3.velocity *= -1f;
						dust3.noGravity = true;
						dust3.velocity -= value4;
						dust3.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMinecart, mountedPlayer);
						if (Main.rand.Next(10) == 0)
						{
							dust3.fadeIn = 1.3f;
							dust3.velocity = Main.rand.NextVector2Circular(3f, 3f) * num10;
						}
					}
				}
				return;
			}
			case 23:
			{
				Vector2 pos = mountedPlayer.Center + this.GetWitchBroomTrinketOriginOffset(mountedPlayer) + (this.GetWitchBroomTrinketRotation(mountedPlayer) + 1.5707964f).ToRotationVector2() * 11f;
				Vector3 rgb = new Vector3(1f, 0.75f, 0.5f) * 0.85f;
				Vector2 vector10 = mountedPlayer.RotatedRelativePoint(pos, false, true);
				Lighting.AddLight(vector10, rgb);
				if (Main.rand.Next(45) == 0)
				{
					Vector2 vector11 = Main.rand.NextVector2Circular(4f, 4f);
					Dust dust4 = Dust.NewDustPerfect(vector10 + vector11, 43, new Vector2?(Vector2.Zero), 254, new Color(255, 255, 0, 255), 0.3f);
					if (vector11 != Vector2.Zero)
					{
						dust4.velocity = vector10.DirectionTo(dust4.position) * 0.2f;
					}
					dust4.fadeIn = 0.3f;
					dust4.noLightEmittence = true;
					dust4.customData = mountedPlayer;
					dust4.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
				}
				float num12 = 0.1f;
				num12 += mountedPlayer.velocity.Length() / 30f;
				Vector2 vector12 = mountedPlayer.Center + new Vector2(18f - 20f * Main.rand.NextFloat() * (float)mountedPlayer.direction, 12f);
				Vector2 vector13 = mountedPlayer.Center + new Vector2((float)(52 * mountedPlayer.direction), -6f);
				vector12 = mountedPlayer.RotatedRelativePoint(vector12, false, true);
				vector13 = mountedPlayer.RotatedRelativePoint(vector13, false, true);
				if (Main.rand.NextFloat() <= num12)
				{
					float num13 = Main.rand.NextFloat();
					for (float num14 = 0f; num14 < 1f; num14 += 0.125f)
					{
						if (Main.rand.Next(15) == 0)
						{
							Vector2 vector14 = (6.2831855f * num14 + num13).ToRotationVector2() * new Vector2(0.5f, 1f) * 4f;
							vector14 = vector14.RotatedBy((double)mountedPlayer.fullRotation, default(Vector2));
							Dust dust5 = Dust.NewDustPerfect(vector12 + vector14, 43, new Vector2?(Vector2.Zero), 254, new Color(255, 255, 0, 255), 0.3f);
							dust5.velocity = vector14 * 0.025f + vector13.DirectionTo(dust5.position) * 0.5f;
							dust5.fadeIn = 0.3f;
							dust5.noLightEmittence = true;
							dust5.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
						}
					}
					return;
				}
				break;
			}
			case 24:
				DelegateMethods.v3_1 = new Vector3(0.1f, 0.3f, 1f) * 0.4f;
				Utils.PlotTileLine(mountedPlayer.MountedCenter, mountedPlayer.MountedCenter + mountedPlayer.velocity * 6f, 40f, new Utils.TileActionAttempt(DelegateMethods.CastLightOpen));
				Utils.PlotTileLine(mountedPlayer.Left, mountedPlayer.Right, 40f, new Utils.TileActionAttempt(DelegateMethods.CastLightOpen));
				return;
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

		// Token: 0x06000243 RID: 579 RVA: 0x00033BB8 File Offset: 0x00031DB8
		private void CastSuperCartLaser(Player mountedPlayer)
		{
			int num = Math.Sign(mountedPlayer.velocity.X);
			if (num == 0)
			{
				num = mountedPlayer.direction;
			}
			if (mountedPlayer.whoAmI == Main.myPlayer && mountedPlayer.velocity.X != 0f)
			{
				Vector2 minecartMechPoint = Mount.GetMinecartMechPoint(mountedPlayer, 20, -19);
				int damage = 60;
				int num2 = 0;
				float num3 = 0f;
				for (int i = 0; i < 200; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.active && npc.immune[mountedPlayer.whoAmI] <= 0 && !npc.dontTakeDamage && npc.Distance(minecartMechPoint) < 300f && npc.CanBeChasedBy(mountedPlayer, false) && Collision.CanHitLine(npc.position, npc.width, npc.height, minecartMechPoint, 0, 0) && Math.Abs(MathHelper.WrapAngle(MathHelper.WrapAngle(npc.AngleFrom(minecartMechPoint)) - MathHelper.WrapAngle((mountedPlayer.fullRotation + (float)num == -1f) ? 3.1415927f : 0f))) < 0.7853982f)
					{
						minecartMechPoint = Mount.GetMinecartMechPoint(mountedPlayer, -20, -39);
						Vector2 vector = npc.position + npc.Size * Utils.RandomVector2(Main.rand, 0f, 1f) - minecartMechPoint;
						num3 += vector.ToRotation();
						num2++;
						int num4 = Projectile.NewProjectile(this.GetProjectileSpawnSource(mountedPlayer), minecartMechPoint.X, minecartMechPoint.Y, vector.X, vector.Y, 591, 0, 0f, mountedPlayer.whoAmI, (float)mountedPlayer.whoAmI, 0f, 0f);
						Main.projectile[num4].Center = npc.Center;
						Main.projectile[num4].damage = damage;
						Main.projectile[num4].Damage();
						Main.projectile[num4].damage = 0;
						Main.projectile[num4].Center = minecartMechPoint;
					}
				}
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00033DD8 File Offset: 0x00031FD8
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
			Vector2 value = new Vector2(num2 * (float)num, (float)offY).RotatedBy((double)mountedPlayer.fullRotation, default(Vector2));
			Vector2 vector = new Vector2(MathHelper.Lerp(0f, -8f, mountedPlayer.fullRotation / 0.7853982f), MathHelper.Lerp(0f, 2f, Math.Abs(mountedPlayer.fullRotation / 0.7853982f))).RotatedBy((double)mountedPlayer.fullRotation, default(Vector2));
			if (num == Math.Sign(mountedPlayer.fullRotation))
			{
				vector *= MathHelper.Lerp(1f, 0.6f, Math.Abs(mountedPlayer.fullRotation / 0.7853982f));
			}
			return mountedPlayer.Bottom + value + vector;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00033EE5 File Offset: 0x000320E5
		public void ResetFlightTime(float xVelocity)
		{
			this._flyTime = (this._active ? this._data.flightTimeMax : 0);
			if (this._type == 0)
			{
				this._flyTime += (int)(Math.Abs(xVelocity) * 20f);
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00033F28 File Offset: 0x00032128
		public void CheckMountBuff(Player mountedPlayer)
		{
			if (this._type == -1)
			{
				return;
			}
			for (int i = 0; i < Player.maxBuffs; i++)
			{
				if (mountedPlayer.buffType[i] == this._data.buff)
				{
					return;
				}
				if (this.Cart && mountedPlayer.buffType[i] == this._data.extraBuff)
				{
					return;
				}
			}
			this.Dismount(mountedPlayer);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00033F8A File Offset: 0x0003218A
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

		// Token: 0x06000248 RID: 584 RVA: 0x00033FB4 File Offset: 0x000321B4
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

		// Token: 0x06000249 RID: 585 RVA: 0x000340A8 File Offset: 0x000322A8
		public bool AimAbility(Player mountedPlayer, Vector2 mousePosition)
		{
			this._aiming = true;
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
			int frameExtra;
			int direction;
			float num3;
			float abilityCharge;
			if (type == 9)
			{
				frameExtra = this._frameExtra;
				direction = mountedPlayer.direction;
				num3 = MathHelper.ToDegrees((this.ClampToDeadZone(mountedPlayer, mousePosition) - mountedPlayer.Center).ToRotation());
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
				abilityCharge = this.AbilityCharge;
				if (abilityCharge > 0f)
				{
					Vector2 vector;
					vector.X = mountedPlayer.position.X + (float)(mountedPlayer.width / 2);
					vector.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
					int num4 = (this._frameExtra - 6) * 2;
					for (int i = 0; i < 2; i++)
					{
						Vector2 vector2;
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
			frameExtra = this._frameExtra;
			direction = mountedPlayer.direction;
			num3 = MathHelper.ToDegrees((this.ClampToDeadZone(mountedPlayer, mousePosition) - mountedPlayer.Center).ToRotation());
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
			abilityCharge = this.AbilityCharge;
			if (abilityCharge > 0f)
			{
				Vector2 vector3;
				vector3.X = mountedPlayer.position.X + (float)(mountedPlayer.width / 2);
				vector3.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
				for (int j = 0; j < 2; j++)
				{
					Vector2 vector4 = new Vector2(vector3.X + (float)(mountedPlayer.width * mountedPlayer.direction), vector3.Y - 12f);
					Lighting.AddLight((int)(vector4.X / 16f), (int)(vector4.Y / 16f), 0.7f, 0.4f, 0.4f);
				}
			}
			return this._frameExtra != frameExtra || mountedPlayer.direction != direction;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0003457C File Offset: 0x0003277C
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
			if (type == 50 && texture2D != null)
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
			type = this._type;
			int num5;
			if (type <= 17)
			{
				if (type != 5)
				{
					if (type == 9)
					{
						switch (drawType)
						{
						case 0:
							num5 = this._frame;
							goto IL_3CD;
						case 2:
							num5 = this._frameExtra;
							goto IL_3CD;
						case 3:
							num5 = this._frameExtra;
							goto IL_3CD;
						}
						num5 = 0;
						goto IL_3CD;
					}
					if (type == 17)
					{
						num4 = texture2D.Height;
						if (drawType == 0)
						{
							num5 = this._frame;
							num3 = 4;
							goto IL_3CD;
						}
						if (drawType != 1)
						{
							num5 = 0;
							goto IL_3CD;
						}
						num5 = this._frameExtra;
						num3 = 4;
						goto IL_3CD;
					}
				}
				else
				{
					if (drawType == 0)
					{
						num5 = this._frame;
						goto IL_3CD;
					}
					if (drawType != 1)
					{
						num5 = 0;
						goto IL_3CD;
					}
					num5 = this._frameExtra;
					goto IL_3CD;
				}
			}
			else if (type <= 39)
			{
				if (type == 23)
				{
					num5 = this._frame;
					goto IL_3CD;
				}
				if (type == 39)
				{
					num4 = texture2D.Height;
					if (drawType == 2)
					{
						num5 = this._frame;
						num3 = 3;
						goto IL_3CD;
					}
					if (drawType != 3)
					{
						num5 = 0;
						goto IL_3CD;
					}
					num5 = this._frameExtra;
					num3 = 6;
					goto IL_3CD;
				}
			}
			else if (type != 46)
			{
				if (type == 52)
				{
					if (drawType != 3)
					{
						num5 = this._frame;
						goto IL_3CD;
					}
					if (drawPlayer.itemAnimation <= 0)
					{
						int holdStyle = drawPlayer.lastVisualizedSelectedItem.holdStyle;
						num5 = this._frame;
						goto IL_3CD;
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
						goto IL_3CD;
					}
					goto IL_3CD;
				}
			}
			else
			{
				if (drawType == 2)
				{
					num5 = this._frame;
					goto IL_3CD;
				}
				if (drawType != 3)
				{
					num5 = 0;
					goto IL_3CD;
				}
				num5 = this._frameExtra;
				goto IL_3CD;
			}
			num5 = this._frame;
			IL_3CD:
			int num7 = num4 / num3;
			Rectangle rectangle = new Rectangle(0, num7 * num5, this._data.textureWidth, num7);
			if (flag)
			{
				rectangle.Height -= 2;
			}
			type = this._type;
			if (type != 0)
			{
				if (type != 7)
				{
					if (type == 9)
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
			Color color = new Color(drawColor.ToVector4() * 0.25f + new Vector4(0.75f));
			type = this._type;
			if (type <= 12)
			{
				if (type != 11)
				{
					if (type == 12)
					{
						if (drawType == 0)
						{
							float scale = MathHelper.Clamp(drawPlayer.MountFishronSpecialCounter / 60f, 0f, 1f);
							color = Colors.CurrentLiquidColor;
							if (color == Color.Transparent)
							{
								color = Color.White;
							}
							color.A = 127;
							color *= scale;
						}
					}
				}
				else if (drawType == 2)
				{
					color = Color.White;
					color.A = 127;
				}
			}
			else if (type != 24)
			{
				if (type == 45 && drawType == 2)
				{
					color = new Color(150, 110, 110, 100);
				}
			}
			else if (drawType == 2)
			{
				color = Color.SkyBlue * 0.5f;
				color.A = 20;
			}
			float num8 = 0f;
			type = this._type;
			if (type != 7)
			{
				if (type == 8)
				{
					Mount.DrillMountData drillMountData = (Mount.DrillMountData)this._mountSpecificData;
					if (drawType == 0)
					{
						num8 = drillMountData.outerRingRotation - num8;
					}
					else if (drawType == 3)
					{
						num8 = drillMountData.diodeRotation - num8 - drawPlayer.fullRotation;
					}
				}
			}
			else
			{
				num8 = drawPlayer.fullRotation;
			}
			Vector2 origin = this.Origin;
			type = this._type;
			float scale2 = 1f;
			type = this._type;
			SpriteEffects spriteEffects;
			switch (type)
			{
			case 6:
				break;
			case 7:
				spriteEffects = SpriteEffects.None;
				goto IL_658;
			case 8:
				spriteEffects = ((drawPlayer.direction == 1 && drawType == 2) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				goto IL_658;
			default:
				if (type != 13)
				{
					spriteEffects = playerEffect;
					goto IL_658;
				}
				break;
			}
			spriteEffects = (this._flipDraw ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
			IL_658:
			if (MountID.Sets.FacePlayersVelocity[this._type])
			{
				spriteEffects = ((Math.Sign(drawPlayer.velocity.X) == -drawPlayer.direction) ? (playerEffect ^ SpriteEffects.FlipHorizontally) : playerEffect);
			}
			bool flag2 = false;
			type = this._type;
			if (type <= 35)
			{
				if (type != 8)
				{
					if (type == 35)
					{
						if (drawType == 2)
						{
							Mount.ExtraFrameMountData extraFrameMountData = (Mount.ExtraFrameMountData)this._mountSpecificData;
							int num9 = -36;
							if (spriteEffects.HasFlag(SpriteEffects.FlipHorizontally))
							{
								num9 *= -1;
							}
							Vector2 value = new Vector2((float)num9, -26f);
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
							Texture2D value2 = TextureAssets.Extra[142].Value;
							Rectangle value3 = value2.Frame(1, 8, 0, extraFrameMountData.frame, 0, 0);
							if (flag)
							{
								value3.Height -= 2;
							}
							DrawData item = new DrawData(value2, Position + value, new Rectangle?(value3), drawColor, num8, origin, scale2, spriteEffects, 0f)
							{
								shader = Mount.currentShader
							};
							playerDrawData.Add(item);
						}
					}
				}
			}
			else if (type != 38)
			{
				if (type == 50 && drawType == 0)
				{
					Vector2 position = Position + new Vector2(0f, (float)(8 - this.PlayerOffset + 20));
					Rectangle value4 = new Rectangle(0, num7 * this._frameExtra, this._data.textureWidth, num7);
					if (flag)
					{
						value4.Height -= 2;
					}
					DrawData item = new DrawData(TextureAssets.Extra[207].Value, position, new Rectangle?(value4), drawColor, num8, origin, scale2, spriteEffects, 0f)
					{
						shader = Mount.currentShader
					};
					playerDrawData.Add(item);
				}
			}
			else if (drawType == 0)
			{
				int num10 = 0;
				if (spriteEffects.HasFlag(SpriteEffects.FlipHorizontally))
				{
					num10 = 22;
				}
				Vector2 value5 = new Vector2((float)num10, -10f);
				Texture2D value6 = TextureAssets.Extra[151].Value;
				Rectangle value7 = value6.Frame(1, 1, 0, 0, 0, 0);
				DrawData item = new DrawData(value6, Position + value5, new Rectangle?(value7), drawColor, num8, origin, scale2, spriteEffects, 0f)
				{
					shader = Mount.currentShader
				};
				playerDrawData.Add(item);
				flag2 = true;
			}
			if (!flag2)
			{
				DrawData item = new DrawData(texture2D, Position, new Rectangle?(rectangle), drawColor, num8, origin, scale2, spriteEffects, 0f)
				{
					shader = Mount.currentShader
				};
				playerDrawData.Add(item);
				if (texture2D2 != null)
				{
					item = new DrawData(texture2D2, Position, new Rectangle?(rectangle), color * ((float)drawColor.A / 255f), num8, origin, scale2, spriteEffects, 0f);
					item.shader = Mount.currentShader;
				}
				playerDrawData.Add(item);
			}
			type = this._type;
			if (type <= 17)
			{
				if (type != 8)
				{
					if (type != 17)
					{
						return;
					}
					if (drawType == 1 && Mount.ShouldGolfCartEmitLight())
					{
						rectangle = new Rectangle(0, num7 * 3, this._data.textureWidth, num7);
						if (flag)
						{
							rectangle.Height -= 2;
						}
						drawColor = Color.White * 1f;
						drawColor.A = 0;
						DrawData item = new DrawData(texture2D, Position, new Rectangle?(rectangle), drawColor, num8, origin, scale2, spriteEffects, 0f)
						{
							shader = Mount.currentShader
						};
						playerDrawData.Add(item);
						return;
					}
				}
				else if (drawType == 3)
				{
					Mount.DrillMountData drillMountData2 = (Mount.DrillMountData)this._mountSpecificData;
					Rectangle value8 = new Rectangle(0, 0, 1, 1);
					Vector2 vector = Mount.drillDiodePoint1.RotatedBy((double)drillMountData2.diodeRotation, default(Vector2));
					Vector2 vector2 = Mount.drillDiodePoint2.RotatedBy((double)drillMountData2.diodeRotation, default(Vector2));
					for (int i = 0; i < drillMountData2.beams.Length; i++)
					{
						Mount.DrillBeam drillBeam = drillMountData2.beams[i];
						if (!(drillBeam.curTileTarget == Point16.NegativeOne))
						{
							for (int j = 0; j < 2; j++)
							{
								Vector2 value9 = new Vector2((float)(drillBeam.curTileTarget.X * 16 + 8), (float)(drillBeam.curTileTarget.Y * 16 + 8)) - Main.screenPosition - Position;
								Vector2 vector3;
								Color color2;
								if (j == 0)
								{
									vector3 = vector;
									color2 = Color.CornflowerBlue;
								}
								else
								{
									vector3 = vector2;
									color2 = Color.LightGreen;
								}
								color2.A = 128;
								color2 *= 0.5f;
								Vector2 v = value9 - vector3;
								float num11 = v.ToRotation();
								float y = v.Length();
								Vector2 scale3 = new Vector2(2f, y);
								DrawData item = new DrawData(TextureAssets.MagicPixel.Value, vector3 + Position, new Rectangle?(value8), color2, num11 - 1.5707964f, Vector2.Zero, scale3, SpriteEffects.None, 0f)
								{
									ignorePlayerRotation = true,
									shader = Mount.currentShader
								};
								playerDrawData.Add(item);
							}
						}
					}
				}
			}
			else if (type != 23)
			{
				if (type != 45)
				{
					if (type == 50 && drawType == 0)
					{
						texture2D = TextureAssets.Extra[205].Value;
						DrawData item = new DrawData(texture2D, Position, new Rectangle?(rectangle), drawColor, num8, origin, scale2, spriteEffects, 0f)
						{
							shader = Mount.currentShader
						};
						playerDrawData.Add(item);
						Vector2 position2 = Position + new Vector2(0f, (float)(8 - this.PlayerOffset + 20));
						Rectangle value10 = new Rectangle(0, num7 * this._frameExtra, this._data.textureWidth, num7);
						if (flag)
						{
							value10.Height -= 2;
						}
						texture2D = TextureAssets.Extra[206].Value;
						item = new DrawData(texture2D, position2, new Rectangle?(value10), drawColor, num8, origin, scale2, spriteEffects, 0f)
						{
							shader = Mount.currentShader
						};
						playerDrawData.Add(item);
						return;
					}
				}
				else if (drawType == 0 && shadow == 0f)
				{
					if (Math.Abs(drawPlayer.velocity.X) > this.DashSpeed * 0.9f)
					{
						color = new Color(255, 220, 220, 200);
						scale2 = 1.1f;
					}
					for (int k = 0; k < 2; k++)
					{
						Vector2 position3 = Position + new Vector2((float)Main.rand.Next(-10, 11) * 0.1f, (float)Main.rand.Next(-10, 11) * 0.1f);
						rectangle = new Rectangle(0, num7 * 3, this._data.textureWidth, num7);
						if (flag)
						{
							rectangle.Height -= 2;
						}
						DrawData item = new DrawData(texture2D2, position3, new Rectangle?(rectangle), color, num8, origin, scale2, spriteEffects, 0f)
						{
							shader = Mount.currentShader
						};
						playerDrawData.Add(item);
					}
					return;
				}
			}
			else if (drawType == 0)
			{
				texture2D = TextureAssets.Extra[114].Value;
				rectangle = texture2D.Frame(2, 1, 0, 0, 0, 0);
				int width = rectangle.Width;
				rectangle.Width -= 2;
				float witchBroomTrinketRotation = this.GetWitchBroomTrinketRotation(drawPlayer);
				Vector2 vector4 = Position + this.GetWitchBroomTrinketOriginOffset(drawPlayer);
				num8 = witchBroomTrinketRotation;
				origin = new Vector2((float)(rectangle.Width / 2), 0f);
				DrawData item = new DrawData(texture2D, vector4.Floor(), new Rectangle?(rectangle), drawColor, num8, origin, scale2, spriteEffects, 0f)
				{
					shader = Mount.currentShader
				};
				playerDrawData.Add(item);
				Color color3 = new Color(new Vector3(0.9f, 0.85f, 0f));
				color3.A /= 2;
				float num12 = ((float)drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 1f;
				Color color4 = new Color(80, 70, 40, 0) * (num12 / 8f + 0.5f) * 0.8f;
				rectangle.X += width;
				for (int l = 0; l < 4; l++)
				{
					item = new DrawData(texture2D, (vector4 + ((float)l * 1.5707964f).ToRotationVector2() * num12).Floor(), new Rectangle?(rectangle), color4, num8, origin, scale2, spriteEffects, 0f)
					{
						shader = Mount.currentShader
					};
					playerDrawData.Add(item);
				}
				return;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00035574 File Offset: 0x00033774
		public void Dismount(Player mountedPlayer)
		{
			if (!this._active)
			{
				return;
			}
			bool cart = this.Cart;
			this._active = false;
			mountedPlayer.ClearBuff(this._data.buff);
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
			this.DoSpawnDust(mountedPlayer, true);
			this.Reset();
			mountedPlayer.position.Y = mountedPlayer.position.Y + (float)mountedPlayer.height;
			mountedPlayer.height = 42;
			mountedPlayer.position.Y = mountedPlayer.position.Y - (float)mountedPlayer.height;
			if (mountedPlayer.whoAmI == Main.myPlayer)
			{
				NetMessage.SendData(13, -1, -1, null, mountedPlayer.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00035664 File Offset: 0x00033864
		public void SetMount(int m, Player mountedPlayer, bool faceLeft = false)
		{
			if (this._type == m || m <= -1 || m >= MountID.Count)
			{
				return;
			}
			if (m == 5 && mountedPlayer.wet)
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
			this.DoSpawnDust(mountedPlayer, false);
			if (mountedPlayer.whoAmI == Main.myPlayer)
			{
				NetMessage.SendData(13, -1, -1, null, mountedPlayer.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00035928 File Offset: 0x00033B28
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
			Color transparent = Color.Transparent;
			if (this._type == 23)
			{
				transparent = new Color(255, 255, 0, 255);
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
						if (Main.rand.Next(2) == 0)
						{
							type2 = 31;
						}
						else
						{
							type2 = 16;
						}
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
					int num2 = Dust.NewDust(new Vector2(mountedPlayer.position.X - 20f, mountedPlayer.position.Y), mountedPlayer.width + 40, mountedPlayer.height, type2, 0f, 0f, alpha, transparent, scale);
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

		// Token: 0x0600024E RID: 590 RVA: 0x00035EE9 File Offset: 0x000340E9
		public bool CanMount(int m, Player mountingPlayer)
		{
			return mountingPlayer.CanFitSpace(Mount.mounts[m].heightBoost);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00035F00 File Offset: 0x00034100
		public bool FindTileHeight(Vector2 position, int maxTilesDown, out float tileHeight)
		{
			int num = (int)(position.X / 16f);
			int num2 = (int)(position.Y / 16f);
			for (int i = 0; i <= maxTilesDown; i++)
			{
				Tile tile = Main.tile[num, num2];
				bool flag = Main.tileSolid[(int)tile.type];
				bool flag2 = Main.tileSolidTop[(int)tile.type];
				if (tile.active())
				{
					if (flag)
					{
						if (flag2)
						{
						}
					}
				}
				num2++;
			}
			tileHeight = 0f;
			return true;
		}

		// Token: 0x0400018E RID: 398
		public static int currentShader = 0;

		// Token: 0x0400018F RID: 399
		public const int FrameStanding = 0;

		// Token: 0x04000190 RID: 400
		public const int FrameRunning = 1;

		// Token: 0x04000191 RID: 401
		public const int FrameInAir = 2;

		// Token: 0x04000192 RID: 402
		public const int FrameFlying = 3;

		// Token: 0x04000193 RID: 403
		public const int FrameSwimming = 4;

		// Token: 0x04000194 RID: 404
		public const int FrameDashing = 5;

		// Token: 0x04000195 RID: 405
		public const int DrawBack = 0;

		// Token: 0x04000196 RID: 406
		public const int DrawBackExtra = 1;

		// Token: 0x04000197 RID: 407
		public const int DrawFront = 2;

		// Token: 0x04000198 RID: 408
		public const int DrawFrontExtra = 3;

		// Token: 0x04000199 RID: 409
		private static Mount.MountData[] mounts;

		// Token: 0x0400019A RID: 410
		private static Vector2[] scutlixEyePositions;

		// Token: 0x0400019B RID: 411
		private static Vector2 scutlixTextureSize;

		// Token: 0x0400019C RID: 412
		public const int scutlixBaseDamage = 50;

		// Token: 0x0400019D RID: 413
		public static Vector2 drillDiodePoint1 = new Vector2(36f, -6f);

		// Token: 0x0400019E RID: 414
		public static Vector2 drillDiodePoint2 = new Vector2(36f, 8f);

		// Token: 0x0400019F RID: 415
		public static Vector2 drillTextureSize;

		// Token: 0x040001A0 RID: 416
		public const int drillTextureWidth = 80;

		// Token: 0x040001A1 RID: 417
		public const float drillRotationChange = 0.05235988f;

		// Token: 0x040001A2 RID: 418
		public static int drillPickPower = 210;

		// Token: 0x040001A3 RID: 419
		public static int drillPickTime = 1;

		// Token: 0x040001A4 RID: 420
		public static int amountOfBeamsAtOnce = 2;

		// Token: 0x040001A5 RID: 421
		public const float maxDrillLength = 48f;

		// Token: 0x040001A6 RID: 422
		private static Vector2 santankTextureSize;

		// Token: 0x040001A7 RID: 423
		private Mount.MountData _data;

		// Token: 0x040001A8 RID: 424
		private int _type;

		// Token: 0x040001A9 RID: 425
		private bool _flipDraw;

		// Token: 0x040001AA RID: 426
		private int _frame;

		// Token: 0x040001AB RID: 427
		private float _frameCounter;

		// Token: 0x040001AC RID: 428
		private int _frameExtra;

		// Token: 0x040001AD RID: 429
		private float _frameExtraCounter;

		// Token: 0x040001AE RID: 430
		private int _frameState;

		// Token: 0x040001AF RID: 431
		private int _flyTime;

		// Token: 0x040001B0 RID: 432
		private int _idleTime;

		// Token: 0x040001B1 RID: 433
		private int _idleTimeNext;

		// Token: 0x040001B2 RID: 434
		private float _fatigue;

		// Token: 0x040001B3 RID: 435
		private float _fatigueMax;

		// Token: 0x040001B4 RID: 436
		private bool _abilityCharging;

		// Token: 0x040001B5 RID: 437
		private int _abilityCharge;

		// Token: 0x040001B6 RID: 438
		private int _abilityCooldown;

		// Token: 0x040001B7 RID: 439
		private int _abilityDuration;

		// Token: 0x040001B8 RID: 440
		private bool _abilityActive;

		// Token: 0x040001B9 RID: 441
		private bool _aiming;

		// Token: 0x040001BA RID: 442
		private bool _shouldSuperCart;

		// Token: 0x040001BB RID: 443
		public List<DrillDebugDraw> _debugDraw;

		// Token: 0x040001BC RID: 444
		private object _mountSpecificData;

		// Token: 0x040001BD RID: 445
		private bool _active;

		// Token: 0x040001BE RID: 446
		public static float SuperCartRunSpeed = 20f;

		// Token: 0x040001BF RID: 447
		public static float SuperCartDashSpeed = 20f;

		// Token: 0x040001C0 RID: 448
		public static float SuperCartAcceleration = 0.1f;

		// Token: 0x040001C1 RID: 449
		public static int SuperCartJumpHeight = 15;

		// Token: 0x040001C2 RID: 450
		public static float SuperCartJumpSpeed = 5.15f;

		// Token: 0x040001C3 RID: 451
		private Mount.MountDelegatesData _defaultDelegatesData = new Mount.MountDelegatesData();

		// Token: 0x020004A6 RID: 1190
		private class DrillBeam
		{
			// Token: 0x06002EC1 RID: 11969 RVA: 0x005C4D34 File Offset: 0x005C2F34
			public DrillBeam()
			{
				this.curTileTarget = Point16.NegativeOne;
				this.cooldown = 0;
				this.lastPurpose = 0;
			}

			// Token: 0x040055E3 RID: 21987
			public Point16 curTileTarget;

			// Token: 0x040055E4 RID: 21988
			public int cooldown;

			// Token: 0x040055E5 RID: 21989
			public int lastPurpose;
		}

		// Token: 0x020004A7 RID: 1191
		private class DrillMountData
		{
			// Token: 0x06002EC2 RID: 11970 RVA: 0x005C4D58 File Offset: 0x005C2F58
			public DrillMountData()
			{
				this.beams = new Mount.DrillBeam[8];
				for (int i = 0; i < this.beams.Length; i++)
				{
					this.beams[i] = new Mount.DrillBeam();
				}
			}

			// Token: 0x040055E6 RID: 21990
			public float diodeRotationTarget;

			// Token: 0x040055E7 RID: 21991
			public float diodeRotation;

			// Token: 0x040055E8 RID: 21992
			public float outerRingRotation;

			// Token: 0x040055E9 RID: 21993
			public Mount.DrillBeam[] beams;

			// Token: 0x040055EA RID: 21994
			public int beamCooldown;

			// Token: 0x040055EB RID: 21995
			public Vector2 crosshairPosition;
		}

		// Token: 0x020004A8 RID: 1192
		private class BooleanMountData
		{
			// Token: 0x06002EC3 RID: 11971 RVA: 0x005C4D97 File Offset: 0x005C2F97
			public BooleanMountData()
			{
				this.boolean = false;
			}

			// Token: 0x040055EC RID: 21996
			public bool boolean;
		}

		// Token: 0x020004A9 RID: 1193
		private class ExtraFrameMountData
		{
			// Token: 0x06002EC4 RID: 11972 RVA: 0x005C4DA6 File Offset: 0x005C2FA6
			public ExtraFrameMountData()
			{
				this.frame = 0;
				this.frameCounter = 0f;
			}

			// Token: 0x040055ED RID: 21997
			public int frame;

			// Token: 0x040055EE RID: 21998
			public float frameCounter;
		}

		// Token: 0x020004AA RID: 1194
		public class MountDelegatesData
		{
			// Token: 0x06002EC5 RID: 11973 RVA: 0x005C4DC0 File Offset: 0x005C2FC0
			public MountDelegatesData()
			{
				this.MinecartDust = new Action<Vector2>(DelegateMethods.Minecart.Sparks);
				this.MinecartJumpingSound = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.JumpingSound);
				this.MinecartLandingSound = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.LandingSound);
				this.MinecartBumperSound = new Action<Player, Vector2, int, int>(DelegateMethods.Minecart.BumperSound);
			}

			// Token: 0x040055EF RID: 21999
			public Action<Vector2> MinecartDust;

			// Token: 0x040055F0 RID: 22000
			public Action<Player, Vector2, int, int> MinecartJumpingSound;

			// Token: 0x040055F1 RID: 22001
			public Action<Player, Vector2, int, int> MinecartLandingSound;

			// Token: 0x040055F2 RID: 22002
			public Action<Player, Vector2, int, int> MinecartBumperSound;

			// Token: 0x040055F3 RID: 22003
			public Mount.MountDelegatesData.OverridePositionMethod MouthPosition;

			// Token: 0x040055F4 RID: 22004
			public Mount.MountDelegatesData.OverridePositionMethod HandPosition;

			// Token: 0x020007DE RID: 2014
			// (Invoke) Token: 0x06003942 RID: 14658
			public delegate bool OverridePositionMethod(Player player, out Vector2? position);
		}

		// Token: 0x020004AB RID: 1195
		private class MountData
		{
			// Token: 0x040055F5 RID: 22005
			public Asset<Texture2D> backTexture = Asset<Texture2D>.Empty;

			// Token: 0x040055F6 RID: 22006
			public Asset<Texture2D> backTextureGlow = Asset<Texture2D>.Empty;

			// Token: 0x040055F7 RID: 22007
			public Asset<Texture2D> backTextureExtra = Asset<Texture2D>.Empty;

			// Token: 0x040055F8 RID: 22008
			public Asset<Texture2D> backTextureExtraGlow = Asset<Texture2D>.Empty;

			// Token: 0x040055F9 RID: 22009
			public Asset<Texture2D> frontTexture = Asset<Texture2D>.Empty;

			// Token: 0x040055FA RID: 22010
			public Asset<Texture2D> frontTextureGlow = Asset<Texture2D>.Empty;

			// Token: 0x040055FB RID: 22011
			public Asset<Texture2D> frontTextureExtra = Asset<Texture2D>.Empty;

			// Token: 0x040055FC RID: 22012
			public Asset<Texture2D> frontTextureExtraGlow = Asset<Texture2D>.Empty;

			// Token: 0x040055FD RID: 22013
			public int textureWidth;

			// Token: 0x040055FE RID: 22014
			public int textureHeight;

			// Token: 0x040055FF RID: 22015
			public int xOffset;

			// Token: 0x04005600 RID: 22016
			public int yOffset;

			// Token: 0x04005601 RID: 22017
			public int[] playerYOffsets;

			// Token: 0x04005602 RID: 22018
			public int bodyFrame;

			// Token: 0x04005603 RID: 22019
			public int playerHeadOffset;

			// Token: 0x04005604 RID: 22020
			public int heightBoost;

			// Token: 0x04005605 RID: 22021
			public int buff;

			// Token: 0x04005606 RID: 22022
			public int extraBuff;

			// Token: 0x04005607 RID: 22023
			public int flightTimeMax;

			// Token: 0x04005608 RID: 22024
			public bool usesHover;

			// Token: 0x04005609 RID: 22025
			public float runSpeed;

			// Token: 0x0400560A RID: 22026
			public float dashSpeed;

			// Token: 0x0400560B RID: 22027
			public float swimSpeed;

			// Token: 0x0400560C RID: 22028
			public float acceleration;

			// Token: 0x0400560D RID: 22029
			public float jumpSpeed;

			// Token: 0x0400560E RID: 22030
			public int jumpHeight;

			// Token: 0x0400560F RID: 22031
			public float fallDamage;

			// Token: 0x04005610 RID: 22032
			public int fatigueMax;

			// Token: 0x04005611 RID: 22033
			public bool constantJump;

			// Token: 0x04005612 RID: 22034
			public bool blockExtraJumps;

			// Token: 0x04005613 RID: 22035
			public int abilityChargeMax;

			// Token: 0x04005614 RID: 22036
			public int abilityDuration;

			// Token: 0x04005615 RID: 22037
			public int abilityCooldown;

			// Token: 0x04005616 RID: 22038
			public int spawnDust;

			// Token: 0x04005617 RID: 22039
			public bool spawnDustNoGravity;

			// Token: 0x04005618 RID: 22040
			public int totalFrames;

			// Token: 0x04005619 RID: 22041
			public int standingFrameStart;

			// Token: 0x0400561A RID: 22042
			public int standingFrameCount;

			// Token: 0x0400561B RID: 22043
			public int standingFrameDelay;

			// Token: 0x0400561C RID: 22044
			public int runningFrameStart;

			// Token: 0x0400561D RID: 22045
			public int runningFrameCount;

			// Token: 0x0400561E RID: 22046
			public int runningFrameDelay;

			// Token: 0x0400561F RID: 22047
			public int flyingFrameStart;

			// Token: 0x04005620 RID: 22048
			public int flyingFrameCount;

			// Token: 0x04005621 RID: 22049
			public int flyingFrameDelay;

			// Token: 0x04005622 RID: 22050
			public int inAirFrameStart;

			// Token: 0x04005623 RID: 22051
			public int inAirFrameCount;

			// Token: 0x04005624 RID: 22052
			public int inAirFrameDelay;

			// Token: 0x04005625 RID: 22053
			public int idleFrameStart;

			// Token: 0x04005626 RID: 22054
			public int idleFrameCount;

			// Token: 0x04005627 RID: 22055
			public int idleFrameDelay;

			// Token: 0x04005628 RID: 22056
			public bool idleFrameLoop;

			// Token: 0x04005629 RID: 22057
			public int swimFrameStart;

			// Token: 0x0400562A RID: 22058
			public int swimFrameCount;

			// Token: 0x0400562B RID: 22059
			public int swimFrameDelay;

			// Token: 0x0400562C RID: 22060
			public int dashingFrameStart;

			// Token: 0x0400562D RID: 22061
			public int dashingFrameCount;

			// Token: 0x0400562E RID: 22062
			public int dashingFrameDelay;

			// Token: 0x0400562F RID: 22063
			public bool Minecart;

			// Token: 0x04005630 RID: 22064
			public bool MinecartDirectional;

			// Token: 0x04005631 RID: 22065
			public Vector3 lightColor = Vector3.One;

			// Token: 0x04005632 RID: 22066
			public bool emitsLight;

			// Token: 0x04005633 RID: 22067
			public Mount.MountDelegatesData delegations = new Mount.MountDelegatesData();

			// Token: 0x04005634 RID: 22068
			public int playerXOffset;
		}
	}
}
