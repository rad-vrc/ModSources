using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Golf;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.DataStructures
{
	// Token: 0x02000724 RID: 1828
	public struct PlayerDrawSet
	{
		// Token: 0x06004A1C RID: 18972 RVA: 0x0064F184 File Offset: 0x0064D384
		public void HeadOnlySetup(Player drawPlayer2, List<DrawData> drawData, List<int> dust, List<int> gore, float X, float Y, float Alpha, float Scale)
		{
			this.projectileDrawPosition = -1;
			this.headOnlyRender = true;
			this.DrawDataCache = drawData;
			this.DustCache = dust;
			this.GoreCache = gore;
			this.drawPlayer = drawPlayer2;
			this.Position = this.drawPlayer.position;
			this.CopyBasicPlayerFields();
			this.drawUnicornHorn = false;
			this.drawAngelHalo = false;
			this.skinVar = this.drawPlayer.skinVariant;
			this.hairDyePacked = PlayerDrawHelper.PackShader(this.drawPlayer.hairDye, PlayerDrawHelper.ShaderConfiguration.HairShader);
			if (this.drawPlayer.head == 0 && this.drawPlayer.hairDye == 0)
			{
				this.hairDyePacked = PlayerDrawHelper.PackShader(1, PlayerDrawHelper.ShaderConfiguration.HairShader);
			}
			this.skinDyePacked = this.drawPlayer.skinDyePacked;
			if (this.drawPlayer.face > 0 && this.drawPlayer.face < (int)ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace(this.drawPlayer.face);
			}
			this.drawUnicornHorn = this.drawPlayer.hasUnicornHorn;
			this.drawAngelHalo = this.drawPlayer.hasAngelHalo;
			Main.instance.LoadHair(this.drawPlayer.hair);
			this.colorEyeWhites = Main.quickAlpha(Color.White, Alpha);
			this.colorEyes = Main.quickAlpha(this.drawPlayer.eyeColor, Alpha);
			this.colorHair = Main.quickAlpha(this.drawPlayer.GetHairColor(false), Alpha);
			this.colorHead = Main.quickAlpha(this.drawPlayer.skinColor, Alpha);
			this.colorArmorHead = Main.quickAlpha(Color.White, Alpha);
			if (this.drawPlayer.isDisplayDollOrInanimate)
			{
				this.colorDisplayDollSkin = Main.quickAlpha(PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR, Alpha);
			}
			else
			{
				this.colorDisplayDollSkin = this.colorHead;
			}
			this.playerEffect = 0;
			if (this.drawPlayer.direction < 0)
			{
				this.playerEffect = 1;
			}
			this.headVect = new Vector2((float)this.drawPlayer.legFrame.Width * 0.5f, (float)this.drawPlayer.legFrame.Height * 0.4f);
			this.Position = new Vector2(X, Y);
			this.Position.X = this.Position.X - 6f;
			this.Position.Y = this.Position.Y - 4f;
			this.Position.Y = this.Position.Y - (float)this.drawPlayer.HeightMapOffset;
			this.SetupHairFrames();
			this.Position -= Main.OffsetsPlayerHeadgear[this.drawPlayer.bodyFrame.Y / this.drawPlayer.bodyFrame.Height];
			if (this.drawPlayer.head > 0 && this.drawPlayer.head < ArmorIDs.Head.Count)
			{
				Main.instance.LoadArmorHead(this.drawPlayer.head);
				int num = ArmorIDs.Head.Sets.FrontToBackID[this.drawPlayer.head];
				if (num >= 0)
				{
					Main.instance.LoadArmorHead(num);
				}
			}
			if (this.drawPlayer.face > 0 && this.drawPlayer.face < (int)ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace(this.drawPlayer.face);
			}
			if (this.drawPlayer.faceHead > 0 && this.drawPlayer.faceHead < (int)ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace(this.drawPlayer.faceHead);
			}
			if (this.drawPlayer.faceFlower > 0 && this.drawPlayer.faceFlower < (int)ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace(this.drawPlayer.faceFlower);
			}
			if (this.drawPlayer.beard > 0 && this.drawPlayer.beard < (int)ArmorIDs.Beard.Count)
			{
				Main.instance.LoadAccBeard(this.drawPlayer.beard);
			}
			this.hairOffset = this.drawPlayer.GetHairDrawOffset(this.drawPlayer.hair, this.hatHair);
			this.hairOffset.Y = this.hairOffset.Y * this.drawPlayer.Directions.Y;
			this.helmetOffset = this.drawPlayer.GetHelmetDrawOffset();
			this.helmetOffset.Y = this.helmetOffset.Y * this.drawPlayer.Directions.Y;
			this.drawPlayer.GetHairSettings(out this.fullHair, out this.hatHair, out this.hideHair, out this.backHairDraw, out this.drawsBackHairWithoutHeadgear);
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06004A1D RID: 18973 RVA: 0x0064F5F4 File Offset: 0x0064D7F4
		// (set) Token: 0x06004A1E RID: 18974 RVA: 0x0064F5FF File Offset: 0x0064D7FF
		internal bool missingHand
		{
			get
			{
				return !this.armorHidesHands;
			}
			set
			{
				this.armorHidesHands = !value;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06004A1F RID: 18975 RVA: 0x0064F60B File Offset: 0x0064D80B
		// (set) Token: 0x06004A20 RID: 18976 RVA: 0x0064F616 File Offset: 0x0064D816
		internal bool missingArm
		{
			get
			{
				return !this.armorHidesArms;
			}
			set
			{
				this.armorHidesArms = !value;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06004A21 RID: 18977 RVA: 0x0064F622 File Offset: 0x0064D822
		public Vector2 Center
		{
			get
			{
				return new Vector2(this.Position.X + (float)(this.drawPlayer.width / 2), this.Position.Y + (float)(this.drawPlayer.height / 2));
			}
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x0064F660 File Offset: 0x0064D860
		public void BoringSetup(Player player, List<DrawData> drawData, List<int> dust, List<int> gore, Vector2 drawPosition, float shadowOpacity, float rotation, Vector2 rotationOrigin)
		{
			this.headOnlyRender = false;
			this.DrawDataCache = drawData;
			this.DustCache = dust;
			this.GoreCache = gore;
			this.drawPlayer = player;
			this.shadow = shadowOpacity;
			this.rotation = rotation;
			this.rotationOrigin = rotationOrigin;
			this.CopyBasicPlayerFields();
			this.BoringSetup_2(player, drawData, dust, gore, drawPosition, shadowOpacity, rotation, rotationOrigin);
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x0064F6C4 File Offset: 0x0064D8C4
		private void CopyBasicPlayerFields()
		{
			this.heldItem = this.drawPlayer.lastVisualizedSelectedItem;
			this.cHead = this.drawPlayer.cHead;
			this.cBody = this.drawPlayer.cBody;
			this.cLegs = this.drawPlayer.cLegs;
			if (this.drawPlayer.wearsRobe)
			{
				this.cLegs = this.cBody;
			}
			this.cHandOn = this.drawPlayer.cHandOn;
			this.cHandOff = this.drawPlayer.cHandOff;
			this.cBack = this.drawPlayer.cBack;
			this.cFront = this.drawPlayer.cFront;
			this.cShoe = this.drawPlayer.cShoe;
			this.cFlameWaker = this.drawPlayer.cFlameWaker;
			this.cWaist = this.drawPlayer.cWaist;
			this.cShield = this.drawPlayer.cShield;
			this.cNeck = this.drawPlayer.cNeck;
			this.cFace = this.drawPlayer.cFace;
			this.cBalloon = this.drawPlayer.cBalloon;
			this.cWings = this.drawPlayer.cWings;
			this.cCarpet = this.drawPlayer.cCarpet;
			this.cPortableStool = this.drawPlayer.cPortableStool;
			this.cFloatingTube = this.drawPlayer.cFloatingTube;
			this.cUnicornHorn = this.drawPlayer.cUnicornHorn;
			this.cAngelHalo = this.drawPlayer.cAngelHalo;
			this.cLeinShampoo = this.drawPlayer.cLeinShampoo;
			this.cBackpack = this.drawPlayer.cBackpack;
			this.cTail = this.drawPlayer.cTail;
			this.cFaceHead = this.drawPlayer.cFaceHead;
			this.cFaceFlower = this.drawPlayer.cFaceFlower;
			this.cBalloonFront = this.drawPlayer.cBalloonFront;
			this.cBeard = this.drawPlayer.cBeard;
			this.isSitting = this.drawPlayer.sitting.isSitting;
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x0064F8DC File Offset: 0x0064DADC
		private void BoringSetup_2(Player player, List<DrawData> drawData, List<int> dust, List<int> gore, Vector2 drawPosition, float shadowOpacity, float rotation, Vector2 rotationOrigin)
		{
			this.seatYOffset = 0f;
			this.sittingIndex = 0;
			Vector2 posOffset = Vector2.Zero;
			this.drawPlayer.sitting.GetSittingOffsetInfo(this.drawPlayer, out posOffset, out this.seatYOffset);
			if (this.isSitting)
			{
				this.sittingIndex = this.drawPlayer.sitting.sittingIndex;
			}
			if (this.drawPlayer.mount.Active && this.drawPlayer.mount.Type == 17)
			{
				this.isSitting = true;
			}
			if (this.drawPlayer.mount.Active && this.drawPlayer.mount.Type == 23)
			{
				this.isSitting = true;
			}
			if (this.drawPlayer.mount.Active && this.drawPlayer.mount.Type == 45)
			{
				this.isSitting = true;
			}
			this.isSleeping = this.drawPlayer.sleeping.isSleeping;
			this.Position = drawPosition;
			this.Position += new Vector2(this.drawPlayer.MountXOffset * (float)this.drawPlayer.direction, 0f);
			if (this.isSitting)
			{
				this.torsoOffset = this.seatYOffset;
				this.Position += posOffset;
			}
			else
			{
				this.sittingIndex = -1;
			}
			if (this.isSleeping)
			{
				this.rotationOrigin = player.Size / 2f;
				Vector2 posOffset2;
				this.drawPlayer.sleeping.GetSleepingOffsetInfo(this.drawPlayer, out posOffset2);
				this.Position += posOffset2;
			}
			this.weaponDrawOrder = WeaponDrawOrder.BehindFrontArm;
			if (this.heldItem.type == 4952)
			{
				this.weaponDrawOrder = WeaponDrawOrder.BehindBackArm;
			}
			if (GolfHelper.IsPlayerHoldingClub(player) && player.itemAnimation >= player.itemAnimationMax)
			{
				this.weaponDrawOrder = WeaponDrawOrder.OverFrontArm;
			}
			this.projectileDrawPosition = -1;
			this.ItemLocation = this.Position + (this.drawPlayer.itemLocation - this.drawPlayer.position);
			this.armorAdjust = 0;
			this.heldProjOverHand = false;
			this.skinVar = this.drawPlayer.skinVariant;
			this.armorHidesHands = (this.drawPlayer.body > 0 && ArmorIDs.Body.Sets.HidesHands[this.drawPlayer.body]);
			this.armorHidesArms = (this.drawPlayer.body > 0 && ArmorIDs.Body.Sets.HidesArms[this.drawPlayer.body]);
			if (this.drawPlayer.heldProj >= 0 && this.shadow == 0f)
			{
				int type = Main.projectile[this.drawPlayer.heldProj].type;
				if (type == 460 || type == 535 || type == 600)
				{
					this.heldProjOverHand = true;
				}
				ProjectileLoader.DrawHeldProjInFrontOfHeldItemAndArms(Main.projectile[this.drawPlayer.heldProj], ref this.heldProjOverHand);
			}
			this.drawPlayer.GetHairSettings(out this.fullHair, out this.hatHair, out this.hideHair, out this.backHairDraw, out this.drawsBackHairWithoutHeadgear);
			this.hairDyePacked = PlayerDrawHelper.PackShader(this.drawPlayer.hairDye, PlayerDrawHelper.ShaderConfiguration.HairShader);
			if (this.drawPlayer.head == 0 && this.drawPlayer.hairDye == 0)
			{
				this.hairDyePacked = PlayerDrawHelper.PackShader(1, PlayerDrawHelper.ShaderConfiguration.HairShader);
			}
			this.skinDyePacked = player.skinDyePacked;
			if (this.drawPlayer.mount.Active && this.drawPlayer.mount.Type == 52)
			{
				this.AdjustmentsForWolfMount();
			}
			if (this.drawPlayer.isDisplayDollOrInanimate)
			{
				Point point = this.Center.ToTileCoordinates();
				bool actuallySelected;
				if (Main.InSmartCursorHighlightArea(point.X, point.Y, out actuallySelected))
				{
					Color color = Lighting.GetColor(point.X, point.Y);
					int num = (int)((color.R + color.G + color.B) / 3);
					if (num > 10)
					{
						this.selectionGlowColor = Colors.GetSelectionGlowColor(actuallySelected, num);
					}
				}
			}
			this.mountOffSet = this.drawPlayer.HeightOffsetVisual;
			this.Position.Y = this.Position.Y - this.mountOffSet;
			if (this.drawPlayer.mount.Active)
			{
				Mount.currentShader = (this.drawPlayer.mount.Cart ? this.drawPlayer.cMinecart : this.drawPlayer.cMount);
			}
			else
			{
				Mount.currentShader = 0;
			}
			this.playerEffect = 0;
			this.itemEffect = 1;
			this.colorHair = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.GetHairColor(true), this.shadow);
			this.colorEyeWhites = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.25) / 16.0), Color.White), this.shadow);
			this.colorEyes = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.25) / 16.0), this.drawPlayer.eyeColor), this.shadow);
			this.colorHead = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.25) / 16.0), this.drawPlayer.skinColor), this.shadow);
			this.colorBodySkin = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.5) / 16.0), this.drawPlayer.skinColor), this.shadow);
			this.colorLegs = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.75) / 16.0), this.drawPlayer.skinColor), this.shadow);
			this.colorShirt = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.5) / 16.0), this.drawPlayer.shirtColor), this.shadow);
			this.colorUnderShirt = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.5) / 16.0), this.drawPlayer.underShirtColor), this.shadow);
			this.colorPants = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.75) / 16.0), this.drawPlayer.pantsColor), this.shadow);
			this.colorShoes = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.75) / 16.0), this.drawPlayer.shoeColor), this.shadow);
			this.colorArmorHead = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)((double)this.Position.Y + (double)this.drawPlayer.height * 0.25) / 16, Color.White), this.shadow);
			this.colorArmorBody = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)((double)this.Position.Y + (double)this.drawPlayer.height * 0.5) / 16, Color.White), this.shadow);
			this.colorMount = this.colorArmorBody;
			this.colorArmorLegs = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)((double)this.Position.Y + (double)this.drawPlayer.height * 0.75) / 16, Color.White), this.shadow);
			this.floatingTubeColor = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)((double)this.Position.Y + (double)this.drawPlayer.height * 0.75) / 16, Color.White), this.shadow);
			this.colorElectricity = new Color(255, 255, 255, 100);
			this.colorDisplayDollSkin = this.colorBodySkin;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			this.headGlowMask = -1;
			this.bodyGlowMask = -1;
			this.armGlowMask = -1;
			this.legsGlowMask = -1;
			this.headGlowColor = Color.Transparent;
			this.bodyGlowColor = Color.Transparent;
			this.armGlowColor = Color.Transparent;
			this.legsGlowColor = Color.Transparent;
			int num29 = this.drawPlayer.head;
			switch (num29)
			{
			case 169:
				num2++;
				break;
			case 170:
				num3++;
				break;
			case 171:
				num4++;
				break;
			default:
				if (num29 == 189)
				{
					num5++;
				}
				break;
			}
			num29 = this.drawPlayer.body;
			switch (num29)
			{
			case 175:
				num2++;
				break;
			case 176:
				num3++;
				break;
			case 177:
				num4++;
				break;
			default:
				if (num29 == 190)
				{
					num5++;
				}
				break;
			}
			num29 = this.drawPlayer.legs;
			switch (num29)
			{
			case 110:
				num2++;
				break;
			case 111:
				num3++;
				break;
			case 112:
				num4++;
				break;
			default:
				if (num29 == 130)
				{
					num5++;
				}
				break;
			}
			num2 = 3;
			num3 = 3;
			num4 = 3;
			num5 = 3;
			this.ArkhalisColor = this.drawPlayer.underShirtColor;
			this.ArkhalisColor.A = 180;
			if (this.drawPlayer.head == 169)
			{
				this.headGlowMask = 15;
				byte b = (byte)(62.5f * (float)(1 + num2));
				this.headGlowColor = new Color((int)b, (int)b, (int)b, 0);
			}
			else if (this.drawPlayer.head == 216)
			{
				this.headGlowMask = 256;
				byte b2 = 127;
				this.headGlowColor = new Color((int)b2, (int)b2, (int)b2, 0);
			}
			else if (this.drawPlayer.head == 210)
			{
				this.headGlowMask = 242;
				byte b3 = 127;
				this.headGlowColor = new Color((int)b3, (int)b3, (int)b3, 0);
			}
			else if (this.drawPlayer.head == 214)
			{
				this.headGlowMask = 245;
				this.headGlowColor = this.ArkhalisColor;
			}
			else if (this.drawPlayer.head == 240)
			{
				this.headGlowMask = 273;
				this.headGlowColor = new Color(230, 230, 230, 60);
			}
			else if (this.drawPlayer.head == 267)
			{
				this.headGlowMask = 301;
				this.headGlowColor = new Color(230, 230, 230, 60);
			}
			else if (this.drawPlayer.head == 268)
			{
				this.headGlowMask = 302;
				float num6 = (float)Main.mouseTextColor / 255f;
				num6 *= num6;
				this.headGlowColor = new Color(255, 255, 255) * num6;
			}
			else if (this.drawPlayer.head == 269)
			{
				this.headGlowMask = 304;
				this.headGlowColor = new Color(200, 200, 200);
			}
			else if (this.drawPlayer.head == 270)
			{
				this.headGlowMask = 305;
				this.headGlowColor = new Color(200, 200, 200, 150);
			}
			else if (this.drawPlayer.head == 271)
			{
				this.headGlowMask = 309;
				this.headGlowColor = Color.White;
			}
			else if (this.drawPlayer.head == 170)
			{
				this.headGlowMask = 16;
				byte b4 = (byte)(62.5f * (float)(1 + num3));
				this.headGlowColor = new Color((int)b4, (int)b4, (int)b4, 0);
			}
			else if (this.drawPlayer.head == 189)
			{
				this.headGlowMask = 184;
				byte b5 = (byte)(62.5f * (float)(1 + num5));
				this.headGlowColor = new Color((int)b5, (int)b5, (int)b5, 0);
				this.colorArmorHead = this.drawPlayer.GetImmuneAlphaPure(new Color((int)b5, (int)b5, (int)b5, 255), this.shadow);
			}
			else if (this.drawPlayer.head == 171)
			{
				byte b6 = (byte)(62.5f * (float)(1 + num4));
				this.colorArmorHead = this.drawPlayer.GetImmuneAlphaPure(new Color((int)b6, (int)b6, (int)b6, 255), this.shadow);
			}
			else if (this.drawPlayer.head == 175)
			{
				this.headGlowMask = 41;
				this.headGlowColor = new Color(255, 255, 255, 0);
			}
			else if (this.drawPlayer.head == 193)
			{
				this.headGlowMask = 209;
				this.headGlowColor = new Color(255, 255, 255, 127);
			}
			else if (this.drawPlayer.head == 109)
			{
				this.headGlowMask = 208;
				this.headGlowColor = new Color(255, 255, 255, 0);
			}
			else if (this.drawPlayer.head == 178)
			{
				this.headGlowMask = 96;
				this.headGlowColor = new Color(255, 255, 255, 0);
			}
			if (this.drawPlayer.body == 175)
			{
				if (this.drawPlayer.Male)
				{
					this.bodyGlowMask = 13;
				}
				else
				{
					this.bodyGlowMask = 18;
				}
				byte b7 = (byte)(62.5f * (float)(1 + num2));
				this.bodyGlowColor = new Color((int)b7, (int)b7, (int)b7, 0);
			}
			else if (this.drawPlayer.body == 208)
			{
				if (this.drawPlayer.Male)
				{
					this.bodyGlowMask = 246;
				}
				else
				{
					this.bodyGlowMask = 247;
				}
				this.armGlowMask = 248;
				this.bodyGlowColor = this.ArkhalisColor;
				this.armGlowColor = this.ArkhalisColor;
			}
			else if (this.drawPlayer.body == 227)
			{
				this.bodyGlowColor = new Color(230, 230, 230, 60);
				this.armGlowColor = new Color(230, 230, 230, 60);
			}
			else if (this.drawPlayer.body == 237)
			{
				float num7 = (float)Main.mouseTextColor / 255f;
				num7 *= num7;
				this.bodyGlowColor = new Color(255, 255, 255) * num7;
			}
			else if (this.drawPlayer.body == 238)
			{
				this.bodyGlowColor = new Color(255, 255, 255);
				this.armGlowColor = new Color(255, 255, 255);
			}
			else if (this.drawPlayer.body == 239)
			{
				this.bodyGlowColor = new Color(200, 200, 200, 150);
				this.armGlowColor = new Color(200, 200, 200, 150);
			}
			else if (this.drawPlayer.body == 190)
			{
				if (this.drawPlayer.Male)
				{
					this.bodyGlowMask = 185;
				}
				else
				{
					this.bodyGlowMask = 186;
				}
				this.armGlowMask = 188;
				byte b8 = (byte)(62.5f * (float)(1 + num5));
				this.bodyGlowColor = new Color((int)b8, (int)b8, (int)b8, 0);
				this.armGlowColor = new Color((int)b8, (int)b8, (int)b8, 0);
				this.colorArmorBody = this.drawPlayer.GetImmuneAlphaPure(new Color((int)b8, (int)b8, (int)b8, 255), this.shadow);
			}
			else if (this.drawPlayer.body == 176)
			{
				if (this.drawPlayer.Male)
				{
					this.bodyGlowMask = 14;
				}
				else
				{
					this.bodyGlowMask = 19;
				}
				this.armGlowMask = 12;
				byte b9 = (byte)(62.5f * (float)(1 + num3));
				this.bodyGlowColor = new Color((int)b9, (int)b9, (int)b9, 0);
				this.armGlowColor = new Color((int)b9, (int)b9, (int)b9, 0);
			}
			else if (this.drawPlayer.body == 194)
			{
				this.bodyGlowMask = 210;
				this.armGlowMask = 211;
				this.bodyGlowColor = new Color(255, 255, 255, 127);
				this.armGlowColor = new Color(255, 255, 255, 127);
			}
			else if (this.drawPlayer.body == 177)
			{
				byte b10 = (byte)(62.5f * (float)(1 + num4));
				this.colorArmorBody = this.drawPlayer.GetImmuneAlphaPure(new Color((int)b10, (int)b10, (int)b10, 255), this.shadow);
			}
			else if (this.drawPlayer.body == 179)
			{
				if (this.drawPlayer.Male)
				{
					this.bodyGlowMask = 42;
				}
				else
				{
					this.bodyGlowMask = 43;
				}
				this.armGlowMask = 44;
				this.bodyGlowColor = new Color(255, 255, 255, 0);
				this.armGlowColor = new Color(255, 255, 255, 0);
			}
			if (this.drawPlayer.legs == 111)
			{
				this.legsGlowMask = 17;
				byte b11 = (byte)(62.5f * (float)(1 + num3));
				this.legsGlowColor = new Color((int)b11, (int)b11, (int)b11, 0);
			}
			else if (this.drawPlayer.legs == 157)
			{
				this.legsGlowMask = 249;
				this.legsGlowColor = this.ArkhalisColor;
			}
			else if (this.drawPlayer.legs == 158)
			{
				this.legsGlowMask = 250;
				this.legsGlowColor = this.ArkhalisColor;
			}
			else if (this.drawPlayer.legs == 210)
			{
				this.legsGlowMask = 274;
				this.legsGlowColor = new Color(230, 230, 230, 60);
			}
			else if (this.drawPlayer.legs == 222)
			{
				this.legsGlowMask = 303;
				float num8 = (float)Main.mouseTextColor / 255f;
				num8 *= num8;
				this.legsGlowColor = new Color(255, 255, 255) * num8;
			}
			else if (this.drawPlayer.legs == 225)
			{
				this.legsGlowMask = 306;
				this.legsGlowColor = new Color(200, 200, 200, 150);
			}
			else if (this.drawPlayer.legs == 226)
			{
				this.legsGlowMask = 307;
				this.legsGlowColor = new Color(200, 200, 200, 150);
			}
			else if (this.drawPlayer.legs == 110)
			{
				this.legsGlowMask = 199;
				byte b12 = (byte)(62.5f * (float)(1 + num2));
				this.legsGlowColor = new Color((int)b12, (int)b12, (int)b12, 0);
			}
			else if (this.drawPlayer.legs == 112)
			{
				byte b13 = (byte)(62.5f * (float)(1 + num4));
				this.colorArmorLegs = this.drawPlayer.GetImmuneAlphaPure(new Color((int)b13, (int)b13, (int)b13, 255), this.shadow);
			}
			else if (this.drawPlayer.legs == 134)
			{
				this.legsGlowMask = 212;
				this.legsGlowColor = new Color(255, 255, 255, 127);
			}
			else if (this.drawPlayer.legs == 130)
			{
				byte b14 = (byte)(127 * (1 + num5));
				this.legsGlowMask = 187;
				this.legsGlowColor = new Color((int)b14, (int)b14, (int)b14, 0);
				this.colorArmorLegs = this.drawPlayer.GetImmuneAlphaPure(new Color((int)b14, (int)b14, (int)b14, 255), this.shadow);
			}
			ItemLoader.DrawArmorColor(EquipType.Head, this.drawPlayer.head, this.drawPlayer, this.shadow, ref this.colorArmorHead, ref this.headGlowMask, ref this.headGlowColor);
			ItemLoader.DrawArmorColor(EquipType.Body, this.drawPlayer.body, this.drawPlayer, this.shadow, ref this.colorArmorBody, ref this.bodyGlowMask, ref this.bodyGlowColor);
			ItemLoader.ArmorArmGlowMask(this.drawPlayer.body, this.drawPlayer, this.shadow, ref this.armGlowMask, ref this.armGlowColor);
			ItemLoader.DrawArmorColor(EquipType.Legs, this.drawPlayer.legs, this.drawPlayer, this.shadow, ref this.colorArmorLegs, ref this.legsGlowMask, ref this.legsGlowColor);
			float alphaReduction = this.shadow;
			this.headGlowColor = this.drawPlayer.GetImmuneAlphaPure(this.headGlowColor, alphaReduction);
			this.bodyGlowColor = this.drawPlayer.GetImmuneAlphaPure(this.bodyGlowColor, alphaReduction);
			this.armGlowColor = this.drawPlayer.GetImmuneAlphaPure(this.armGlowColor, alphaReduction);
			this.legsGlowColor = this.drawPlayer.GetImmuneAlphaPure(this.legsGlowColor, alphaReduction);
			if (this.drawPlayer.head > 0 && this.drawPlayer.head < ArmorIDs.Head.Count)
			{
				Main.instance.LoadArmorHead(this.drawPlayer.head);
				int num9 = ArmorIDs.Head.Sets.FrontToBackID[this.drawPlayer.head];
				if (num9 >= 0)
				{
					Main.instance.LoadArmorHead(num9);
				}
			}
			if (this.drawPlayer.body > 0 && this.drawPlayer.body < ArmorIDs.Body.Count)
			{
				Main.instance.LoadArmorBody(this.drawPlayer.body);
			}
			if (this.drawPlayer.legs > 0 && this.drawPlayer.legs < ArmorIDs.Legs.Count)
			{
				Main.instance.LoadArmorLegs(this.drawPlayer.legs);
			}
			if (this.drawPlayer.handon > 0 && this.drawPlayer.handon < ArmorIDs.HandOn.Count)
			{
				Main.instance.LoadAccHandsOn(this.drawPlayer.handon);
			}
			if (this.drawPlayer.handoff > 0 && this.drawPlayer.handoff < ArmorIDs.HandOff.Count)
			{
				Main.instance.LoadAccHandsOff(this.drawPlayer.handoff);
			}
			if (this.drawPlayer.back > 0 && this.drawPlayer.back < ArmorIDs.Back.Count)
			{
				Main.instance.LoadAccBack(this.drawPlayer.back);
			}
			if (this.drawPlayer.front > 0 && this.drawPlayer.front < ArmorIDs.Front.Count)
			{
				Main.instance.LoadAccFront(this.drawPlayer.front);
			}
			if (this.drawPlayer.shoe > 0 && this.drawPlayer.shoe < ArmorIDs.Shoe.Count)
			{
				Main.instance.LoadAccShoes(this.drawPlayer.shoe);
			}
			if (this.drawPlayer.waist > 0 && this.drawPlayer.waist < ArmorIDs.Waist.Count)
			{
				Main.instance.LoadAccWaist(this.drawPlayer.waist);
			}
			if (this.drawPlayer.shield > 0 && this.drawPlayer.shield < ArmorIDs.Shield.Count)
			{
				Main.instance.LoadAccShield(this.drawPlayer.shield);
			}
			if (this.drawPlayer.neck > 0 && this.drawPlayer.neck < ArmorIDs.Neck.Count)
			{
				Main.instance.LoadAccNeck(this.drawPlayer.neck);
			}
			if (this.drawPlayer.face > 0 && this.drawPlayer.face < (int)ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace(this.drawPlayer.face);
			}
			if (this.drawPlayer.balloon > 0 && this.drawPlayer.balloon < ArmorIDs.Balloon.Count)
			{
				Main.instance.LoadAccBalloon(this.drawPlayer.balloon);
			}
			if (this.drawPlayer.backpack > 0 && this.drawPlayer.backpack < ArmorIDs.Back.Count)
			{
				Main.instance.LoadAccBack(this.drawPlayer.backpack);
			}
			if (this.drawPlayer.tail > 0 && this.drawPlayer.tail < ArmorIDs.Back.Count)
			{
				Main.instance.LoadAccBack(this.drawPlayer.tail);
			}
			if (this.drawPlayer.faceHead > 0 && this.drawPlayer.faceHead < (int)ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace(this.drawPlayer.faceHead);
			}
			if (this.drawPlayer.faceFlower > 0 && this.drawPlayer.faceFlower < (int)ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace(this.drawPlayer.faceFlower);
			}
			if (this.drawPlayer.balloonFront > 0 && this.drawPlayer.balloonFront < ArmorIDs.Balloon.Count)
			{
				Main.instance.LoadAccBalloon(this.drawPlayer.balloonFront);
			}
			if (this.drawPlayer.beard > 0 && this.drawPlayer.beard < (int)ArmorIDs.Beard.Count)
			{
				Main.instance.LoadAccBeard(this.drawPlayer.beard);
			}
			Main.instance.LoadHair(this.drawPlayer.hair);
			if (this.drawPlayer.eyebrellaCloud)
			{
				Main.instance.LoadProjectile(238);
			}
			if (this.drawPlayer.isHatRackDoll)
			{
				this.colorLegs = Color.Transparent;
				this.colorBodySkin = Color.Transparent;
				this.colorHead = Color.Transparent;
				this.colorHair = Color.Transparent;
				this.colorEyes = Color.Transparent;
				this.colorEyeWhites = Color.Transparent;
			}
			if (this.drawPlayer.isDisplayDollOrInanimate)
			{
				if (this.drawPlayer.isFullbright)
				{
					this.colorHead = Color.White;
					this.colorBodySkin = Color.White;
					this.colorLegs = Color.White;
					this.colorEyes = Color.White;
					this.colorEyeWhites = Color.White;
					this.colorArmorHead = Color.White;
					this.colorArmorBody = Color.White;
					this.colorArmorLegs = Color.White;
					this.colorDisplayDollSkin = PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR;
				}
				else
				{
					this.colorDisplayDollSkin = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)((double)this.Position.Y + (double)this.drawPlayer.height * 0.5) / 16, PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR), this.shadow);
				}
			}
			if (!this.drawPlayer.isDisplayDollOrInanimate)
			{
				if ((this.drawPlayer.head == 78 || this.drawPlayer.head == 79 || this.drawPlayer.head == 80) && this.drawPlayer.body == 51 && this.drawPlayer.legs == 47)
				{
					float num10 = (float)Main.mouseTextColor / 200f - 0.3f;
					if (this.shadow != 0f)
					{
						num10 = 0f;
					}
					this.colorArmorHead.R = (byte)((float)this.colorArmorHead.R * num10);
					this.colorArmorHead.G = (byte)((float)this.colorArmorHead.G * num10);
					this.colorArmorHead.B = (byte)((float)this.colorArmorHead.B * num10);
					this.colorArmorBody.R = (byte)((float)this.colorArmorBody.R * num10);
					this.colorArmorBody.G = (byte)((float)this.colorArmorBody.G * num10);
					this.colorArmorBody.B = (byte)((float)this.colorArmorBody.B * num10);
					this.colorArmorLegs.R = (byte)((float)this.colorArmorLegs.R * num10);
					this.colorArmorLegs.G = (byte)((float)this.colorArmorLegs.G * num10);
					this.colorArmorLegs.B = (byte)((float)this.colorArmorLegs.B * num10);
				}
				if (this.drawPlayer.head == 193 && this.drawPlayer.body == 194 && this.drawPlayer.legs == 134)
				{
					float num11 = 0.6f - this.drawPlayer.ghostFade * 0.3f;
					if (this.shadow != 0f)
					{
						num11 = 0f;
					}
					this.colorArmorHead.R = (byte)((float)this.colorArmorHead.R * num11);
					this.colorArmorHead.G = (byte)((float)this.colorArmorHead.G * num11);
					this.colorArmorHead.B = (byte)((float)this.colorArmorHead.B * num11);
					this.colorArmorBody.R = (byte)((float)this.colorArmorBody.R * num11);
					this.colorArmorBody.G = (byte)((float)this.colorArmorBody.G * num11);
					this.colorArmorBody.B = (byte)((float)this.colorArmorBody.B * num11);
					this.colorArmorLegs.R = (byte)((float)this.colorArmorLegs.R * num11);
					this.colorArmorLegs.G = (byte)((float)this.colorArmorLegs.G * num11);
					this.colorArmorLegs.B = (byte)((float)this.colorArmorLegs.B * num11);
				}
				if (this.shadow > 0f)
				{
					this.colorLegs = Color.Transparent;
					this.colorBodySkin = Color.Transparent;
					this.colorHead = Color.Transparent;
					this.colorHair = Color.Transparent;
					this.colorEyes = Color.Transparent;
					this.colorEyeWhites = Color.Transparent;
				}
			}
			float num12 = 1f;
			float num13 = 1f;
			float num14 = 1f;
			float num15 = 1f;
			if (this.drawPlayer.honey && Main.rand.Next(30) == 0 && this.shadow == 0f)
			{
				Dust dust2 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, 152, 0f, 0f, 150, default(Color), 1f);
				dust2.velocity.Y = 0.3f;
				Dust dust22 = dust2;
				dust22.velocity.X = dust22.velocity.X * 0.1f;
				dust2.scale += (float)Main.rand.Next(3, 4) * 0.1f;
				dust2.alpha = 100;
				dust2.noGravity = true;
				dust2.velocity += this.drawPlayer.velocity * 0.1f;
				this.DustCache.Add(dust2.dustIndex);
			}
			if (this.drawPlayer.dryadWard && this.drawPlayer.velocity.X != 0f && Main.rand.Next(4) == 0)
			{
				Dust dust3 = Dust.NewDustDirect(new Vector2(this.drawPlayer.position.X - 2f, this.drawPlayer.position.Y + (float)this.drawPlayer.height - 2f), this.drawPlayer.width + 4, 4, 163, 0f, 0f, 100, default(Color), 1.5f);
				dust3.noGravity = true;
				dust3.noLight = true;
				dust3.velocity *= 0f;
				this.DustCache.Add(dust3.dustIndex);
			}
			if (this.drawPlayer.poisoned)
			{
				if (Main.rand.Next(50) == 0 && this.shadow == 0f)
				{
					Dust dust4 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, 46, 0f, 0f, 150, default(Color), 0.2f);
					dust4.noGravity = true;
					dust4.fadeIn = 1.9f;
					this.DustCache.Add(dust4.dustIndex);
				}
				num12 *= 0.65f;
				num14 *= 0.75f;
			}
			if (this.drawPlayer.venom)
			{
				if (Main.rand.Next(10) == 0 && this.shadow == 0f)
				{
					Dust dust5 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, 171, 0f, 0f, 100, default(Color), 0.5f);
					dust5.noGravity = true;
					dust5.fadeIn = 1.5f;
					this.DustCache.Add(dust5.dustIndex);
				}
				num13 *= 0.45f;
				num12 *= 0.75f;
			}
			if (this.drawPlayer.onFire)
			{
				if (Main.rand.Next(4) == 0 && this.shadow == 0f)
				{
					Dust dust6 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 6, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					dust6.noGravity = true;
					dust6.velocity *= 1.8f;
					Dust dust23 = dust6;
					dust23.velocity.Y = dust23.velocity.Y - 0.5f;
					this.DustCache.Add(dust6.dustIndex);
				}
				num14 *= 0.6f;
				num13 *= 0.7f;
			}
			if (this.drawPlayer.onFire3)
			{
				if (Main.rand.Next(4) == 0 && this.shadow == 0f)
				{
					Dust dust7 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 6, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					dust7.noGravity = true;
					dust7.velocity *= 1.8f;
					Dust dust24 = dust7;
					dust24.velocity.Y = dust24.velocity.Y - 0.5f;
					this.DustCache.Add(dust7.dustIndex);
				}
				num14 *= 0.6f;
				num13 *= 0.7f;
			}
			if (this.drawPlayer.dripping && this.shadow == 0f && Main.rand.Next(4) != 0)
			{
				Vector2 position = this.Position;
				position.X -= 2f;
				position.Y -= 2f;
				if (Main.rand.Next(2) == 0)
				{
					Dust dust8 = Dust.NewDustDirect(position, this.drawPlayer.width + 4, this.drawPlayer.height + 2, 211, 0f, 0f, 50, default(Color), 0.8f);
					if (Main.rand.Next(2) == 0)
					{
						dust8.alpha += 25;
					}
					if (Main.rand.Next(2) == 0)
					{
						dust8.alpha += 25;
					}
					dust8.noLight = true;
					dust8.velocity *= 0.2f;
					Dust dust25 = dust8;
					dust25.velocity.Y = dust25.velocity.Y + 0.2f;
					dust8.velocity += this.drawPlayer.velocity;
					this.DustCache.Add(dust8.dustIndex);
				}
				else
				{
					Dust dust9 = Dust.NewDustDirect(position, this.drawPlayer.width + 8, this.drawPlayer.height + 8, 211, 0f, 0f, 50, default(Color), 1.1f);
					if (Main.rand.Next(2) == 0)
					{
						dust9.alpha += 25;
					}
					if (Main.rand.Next(2) == 0)
					{
						dust9.alpha += 25;
					}
					dust9.noLight = true;
					dust9.noGravity = true;
					dust9.velocity *= 0.2f;
					Dust dust26 = dust9;
					dust26.velocity.Y = dust26.velocity.Y + 1f;
					dust9.velocity += this.drawPlayer.velocity;
					this.DustCache.Add(dust9.dustIndex);
				}
			}
			if (this.drawPlayer.drippingSlime)
			{
				int alpha = 175;
				Color newColor;
				newColor..ctor(0, 80, 255, 100);
				if (Main.rand.Next(4) != 0 && this.shadow == 0f)
				{
					Vector2 position2 = this.Position;
					position2.X -= 2f;
					position2.Y -= 2f;
					if (Main.rand.Next(2) == 0)
					{
						Dust dust10 = Dust.NewDustDirect(position2, this.drawPlayer.width + 4, this.drawPlayer.height + 2, 4, 0f, 0f, alpha, newColor, 1.4f);
						if (Main.rand.Next(2) == 0)
						{
							dust10.alpha += 25;
						}
						if (Main.rand.Next(2) == 0)
						{
							dust10.alpha += 25;
						}
						dust10.noLight = true;
						dust10.velocity *= 0.2f;
						Dust dust27 = dust10;
						dust27.velocity.Y = dust27.velocity.Y + 0.2f;
						dust10.velocity += this.drawPlayer.velocity;
						this.DustCache.Add(dust10.dustIndex);
					}
				}
				num12 *= 0.8f;
				num13 *= 0.8f;
			}
			if (this.drawPlayer.drippingSparkleSlime)
			{
				int alpha2 = 100;
				if (Main.rand.Next(4) != 0 && this.shadow == 0f)
				{
					Vector2 position3 = this.Position;
					position3.X -= 2f;
					position3.Y -= 2f;
					if (Main.rand.Next(4) == 0)
					{
						Color newColor2 = Main.hslToRgb(0.7f + 0.2f * Main.rand.NextFloat(), 1f, 0.5f, byte.MaxValue);
						newColor2.A /= 2;
						Dust dust11 = Dust.NewDustDirect(position3, this.drawPlayer.width + 4, this.drawPlayer.height + 2, 4, 0f, 0f, alpha2, newColor2, 0.65f);
						if (Main.rand.Next(2) == 0)
						{
							dust11.alpha += 25;
						}
						if (Main.rand.Next(2) == 0)
						{
							dust11.alpha += 25;
						}
						dust11.noLight = true;
						dust11.velocity *= 0.2f;
						dust11.velocity += this.drawPlayer.velocity * 0.7f;
						dust11.fadeIn = 0.8f;
						this.DustCache.Add(dust11.dustIndex);
					}
					if (Main.rand.Next(30) == 0)
					{
						Color color2;
						Main.hslToRgb(0.7f + 0.2f * Main.rand.NextFloat(), 1f, 0.5f, byte.MaxValue).A = color2.A / 2;
						Dust dust12 = Dust.NewDustDirect(position3, this.drawPlayer.width + 4, this.drawPlayer.height + 2, 43, 0f, 0f, 254, new Color(127, 127, 127, 0), 0.45f);
						dust12.noLight = true;
						Dust dust28 = dust12;
						dust28.velocity.X = dust28.velocity.X * 0f;
						dust12.velocity *= 0.03f;
						dust12.fadeIn = 0.6f;
						this.DustCache.Add(dust12.dustIndex);
					}
				}
				num12 *= 0.94f;
				num13 *= 0.82f;
			}
			if (this.drawPlayer.ichor)
			{
				num14 = 0f;
			}
			if (this.drawPlayer.electrified && this.shadow == 0f && Main.rand.Next(3) == 0)
			{
				Dust dust13 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 226, 0f, 0f, 100, default(Color), 0.5f);
				dust13.velocity *= 1.6f;
				Dust dust29 = dust13;
				dust29.velocity.Y = dust29.velocity.Y - 1f;
				dust13.position = Vector2.Lerp(dust13.position, this.drawPlayer.Center, 0.5f);
				this.DustCache.Add(dust13.dustIndex);
			}
			if (this.drawPlayer.burned)
			{
				if (this.shadow == 0f)
				{
					Dust dust14 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 6, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 2f);
					dust14.noGravity = true;
					dust14.velocity *= 1.8f;
					Dust dust30 = dust14;
					dust30.velocity.Y = dust30.velocity.Y - 0.75f;
					this.DustCache.Add(dust14.dustIndex);
				}
				num12 = 1f;
				num14 *= 0.6f;
				num13 *= 0.7f;
			}
			if (this.drawPlayer.onFrostBurn)
			{
				if (Main.rand.Next(4) == 0 && this.shadow == 0f)
				{
					Dust dust15 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 135, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					dust15.noGravity = true;
					dust15.velocity *= 1.8f;
					Dust dust31 = dust15;
					dust31.velocity.Y = dust31.velocity.Y - 0.5f;
					this.DustCache.Add(dust15.dustIndex);
				}
				num12 *= 0.5f;
				num13 *= 0.7f;
			}
			if (this.drawPlayer.onFrostBurn2)
			{
				if (Main.rand.Next(4) == 0 && this.shadow == 0f)
				{
					Dust dust16 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 135, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					dust16.noGravity = true;
					dust16.velocity *= 1.8f;
					Dust dust32 = dust16;
					dust32.velocity.Y = dust32.velocity.Y - 0.5f;
					this.DustCache.Add(dust16.dustIndex);
				}
				num12 *= 0.5f;
				num13 *= 0.7f;
			}
			if (this.drawPlayer.onFire2)
			{
				if (Main.rand.Next(4) == 0 && this.shadow == 0f)
				{
					Dust dust17 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 75, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					dust17.noGravity = true;
					dust17.velocity *= 1.8f;
					Dust dust33 = dust17;
					dust33.velocity.Y = dust33.velocity.Y - 0.5f;
					this.DustCache.Add(dust17.dustIndex);
				}
				num14 *= 0.6f;
				num13 *= 0.7f;
			}
			if (this.drawPlayer.noItems)
			{
				num13 *= 0.8f;
				num12 *= 0.65f;
			}
			if (this.drawPlayer.blind)
			{
				num13 *= 0.65f;
				num12 *= 0.7f;
			}
			if (this.drawPlayer.bleed)
			{
				num13 *= 0.9f;
				num14 *= 0.9f;
				if (!this.drawPlayer.dead && Main.rand.Next(30) == 0 && this.shadow == 0f)
				{
					Dust dust18 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, 5, 0f, 0f, 0, default(Color), 1f);
					Dust dust34 = dust18;
					dust34.velocity.Y = dust34.velocity.Y + 0.5f;
					dust18.velocity *= 0.25f;
					this.DustCache.Add(dust18.dustIndex);
				}
			}
			if (this.shadow == 0f && this.drawPlayer.palladiumRegen && this.drawPlayer.statLife < this.drawPlayer.statLifeMax2 && Main.instance.IsActive && !Main.gamePaused && this.drawPlayer.miscCounter % 10 == 0 && this.shadow == 0f)
			{
				Vector2 position4 = default(Vector2);
				position4.X = this.Position.X + (float)Main.rand.Next(this.drawPlayer.width);
				position4.Y = this.Position.Y + (float)Main.rand.Next(this.drawPlayer.height);
				position4.X = this.Position.X + (float)(this.drawPlayer.width / 2) - 6f;
				position4.Y = this.Position.Y + (float)(this.drawPlayer.height / 2) - 6f;
				position4.X -= (float)Main.rand.Next(-10, 11);
				position4.Y -= (float)Main.rand.Next(-20, 21);
				int item = Gore.NewGore(position4, new Vector2((float)Main.rand.Next(-10, 11) * 0.1f, (float)Main.rand.Next(-20, -10) * 0.1f), 331, (float)Main.rand.Next(80, 120) * 0.01f);
				this.GoreCache.Add(item);
			}
			if (this.shadow == 0f && this.drawPlayer.loveStruck && Main.instance.IsActive && !Main.gamePaused && Main.rand.Next(5) == 0)
			{
				Vector2 vector;
				vector..ctor((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
				vector.Normalize();
				vector.X *= 0.66f;
				int num16 = Gore.NewGore(this.Position + new Vector2((float)Main.rand.Next(this.drawPlayer.width + 1), (float)Main.rand.Next(this.drawPlayer.height + 1)), vector * (float)Main.rand.Next(3, 6) * 0.33f, 331, (float)Main.rand.Next(40, 121) * 0.01f);
				Main.gore[num16].sticky = false;
				Main.gore[num16].velocity *= 0.4f;
				Gore gore2 = Main.gore[num16];
				gore2.velocity.Y = gore2.velocity.Y - 0.6f;
				this.GoreCache.Add(num16);
			}
			if (this.drawPlayer.stinky && Main.instance.IsActive && !Main.gamePaused)
			{
				num12 *= 0.7f;
				num14 *= 0.55f;
				if (Main.rand.Next(5) == 0 && this.shadow == 0f)
				{
					Vector2 vector2;
					vector2..ctor((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
					vector2.Normalize();
					vector2.X *= 0.66f;
					vector2.Y = Math.Abs(vector2.Y);
					Vector2 vector3 = vector2 * (float)Main.rand.Next(3, 5) * 0.25f;
					int num17 = Dust.NewDust(this.Position, this.drawPlayer.width, this.drawPlayer.height, 188, vector3.X, vector3.Y * 0.5f, 100, default(Color), 1.5f);
					Main.dust[num17].velocity *= 0.1f;
					Dust dust35 = Main.dust[num17];
					dust35.velocity.Y = dust35.velocity.Y - 0.5f;
					this.DustCache.Add(num17);
				}
			}
			if (this.drawPlayer.slowOgreSpit && Main.instance.IsActive && !Main.gamePaused)
			{
				num12 *= 0.6f;
				num14 *= 0.45f;
				if (Main.rand.Next(5) == 0 && this.shadow == 0f)
				{
					int type2 = Utils.SelectRandom<int>(Main.rand, new int[]
					{
						4,
						256
					});
					Dust dust19 = Main.dust[Dust.NewDust(this.Position, this.drawPlayer.width, this.drawPlayer.height, type2, 0f, 0f, 100, default(Color), 1f)];
					dust19.scale = 0.8f + Main.rand.NextFloat() * 0.6f;
					dust19.fadeIn = 0.5f;
					dust19.velocity *= 0.05f;
					dust19.noLight = true;
					if (dust19.type == 4)
					{
						dust19.color = new Color(80, 170, 40, 120);
					}
					this.DustCache.Add(dust19.dustIndex);
				}
				if (Main.rand.Next(5) == 0 && this.shadow == 0f)
				{
					int num18 = Gore.NewGore(this.Position + new Vector2(Main.rand.NextFloat(), Main.rand.NextFloat()) * this.drawPlayer.Size, Vector2.Zero, Utils.SelectRandom<int>(Main.rand, new int[]
					{
						1024,
						1025,
						1026
					}), 0.65f);
					Main.gore[num18].velocity *= 0.05f;
					this.GoreCache.Add(num18);
				}
			}
			if (Main.instance.IsActive && !Main.gamePaused && this.shadow == 0f)
			{
				float num19 = (float)this.drawPlayer.miscCounter / 180f;
				float num20 = 0f;
				float num21 = 10f;
				int type3 = 90;
				int num22 = 0;
				int i = 0;
				while (i < 3)
				{
					switch (i)
					{
					case 0:
						if (this.drawPlayer.nebulaLevelLife >= 1)
						{
							num20 = 6.2831855f / (float)this.drawPlayer.nebulaLevelLife;
							num22 = this.drawPlayer.nebulaLevelLife;
							goto IL_3AB3;
						}
						break;
					case 1:
						if (this.drawPlayer.nebulaLevelMana >= 1)
						{
							num20 = -6.2831855f / (float)this.drawPlayer.nebulaLevelMana;
							num22 = this.drawPlayer.nebulaLevelMana;
							num19 = (float)(-(float)this.drawPlayer.miscCounter) / 180f;
							num21 = 20f;
							type3 = 88;
							goto IL_3AB3;
						}
						break;
					case 2:
						if (this.drawPlayer.nebulaLevelDamage >= 1)
						{
							num20 = 6.2831855f / (float)this.drawPlayer.nebulaLevelDamage;
							num22 = this.drawPlayer.nebulaLevelDamage;
							num19 = (float)this.drawPlayer.miscCounter / 180f;
							num21 = 30f;
							type3 = 86;
							goto IL_3AB3;
						}
						break;
					default:
						goto IL_3AB3;
					}
					IL_3B89:
					i++;
					continue;
					IL_3AB3:
					for (int j = 0; j < num22; j++)
					{
						Dust dust20 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, type3, 0f, 0f, 100, default(Color), 1.5f);
						dust20.noGravity = true;
						dust20.velocity = Vector2.Zero;
						dust20.position = this.drawPlayer.Center + Vector2.UnitY * this.drawPlayer.gfxOffY + (num19 * 6.2831855f + num20 * (float)j).ToRotationVector2() * num21;
						dust20.customData = this.drawPlayer;
						this.DustCache.Add(dust20.dustIndex);
					}
					goto IL_3B89;
				}
			}
			if (this.drawPlayer.witheredArmor && Main.instance.IsActive && !Main.gamePaused)
			{
				num13 *= 0.5f;
				num12 *= 0.75f;
			}
			if (this.drawPlayer.witheredWeapon && this.drawPlayer.itemAnimation > 0 && this.heldItem.damage > 0 && Main.instance.IsActive && !Main.gamePaused && Main.rand.Next(3) == 0)
			{
				Dust dust21 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 272, 0f, 0f, 50, default(Color), 0.5f);
				dust21.velocity *= 1.6f;
				Dust dust36 = dust21;
				dust36.velocity.Y = dust36.velocity.Y - 1f;
				dust21.position = Vector2.Lerp(dust21.position, this.drawPlayer.Center, 0.5f);
				this.DustCache.Add(dust21.dustIndex);
			}
			bool shimmering = this.drawPlayer.shimmering;
			bool fullBright = false;
			PlayerLoader.DrawEffects(this, ref num12, ref num13, ref num14, ref num15, ref fullBright);
			if (num12 != 1f || num13 != 1f || num14 != 1f || num15 != 1f || fullBright)
			{
				if (this.drawPlayer.onFire || this.drawPlayer.onFire2 || this.drawPlayer.onFrostBurn || this.drawPlayer.onFire3 || this.drawPlayer.onFrostBurn2 || fullBright)
				{
					this.colorEyeWhites = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
					this.colorEyes = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.eyeColor, this.shadow);
					this.colorHair = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.GetHairColor(false), this.shadow);
					this.colorHead = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.skinColor, this.shadow);
					this.colorBodySkin = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.skinColor, this.shadow);
					this.colorShirt = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.shirtColor, this.shadow);
					this.colorUnderShirt = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.underShirtColor, this.shadow);
					this.colorPants = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.pantsColor, this.shadow);
					this.colorLegs = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.skinColor, this.shadow);
					this.colorShoes = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.shoeColor, this.shadow);
					this.colorArmorHead = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
					this.colorArmorBody = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
					this.colorArmorLegs = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
					if (this.drawPlayer.isDisplayDollOrInanimate)
					{
						this.colorDisplayDollSkin = this.drawPlayer.GetImmuneAlpha(PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR, this.shadow);
					}
				}
				else
				{
					this.colorEyeWhites = Main.buffColor(this.colorEyeWhites, num12, num13, num14, num15);
					this.colorEyes = Main.buffColor(this.colorEyes, num12, num13, num14, num15);
					this.colorHair = Main.buffColor(this.colorHair, num12, num13, num14, num15);
					this.colorHead = Main.buffColor(this.colorHead, num12, num13, num14, num15);
					this.colorBodySkin = Main.buffColor(this.colorBodySkin, num12, num13, num14, num15);
					this.colorShirt = Main.buffColor(this.colorShirt, num12, num13, num14, num15);
					this.colorUnderShirt = Main.buffColor(this.colorUnderShirt, num12, num13, num14, num15);
					this.colorPants = Main.buffColor(this.colorPants, num12, num13, num14, num15);
					this.colorLegs = Main.buffColor(this.colorLegs, num12, num13, num14, num15);
					this.colorShoes = Main.buffColor(this.colorShoes, num12, num13, num14, num15);
					this.colorArmorHead = Main.buffColor(this.colorArmorHead, num12, num13, num14, num15);
					this.colorArmorBody = Main.buffColor(this.colorArmorBody, num12, num13, num14, num15);
					this.colorArmorLegs = Main.buffColor(this.colorArmorLegs, num12, num13, num14, num15);
					if (this.drawPlayer.isDisplayDollOrInanimate)
					{
						this.colorDisplayDollSkin = Main.buffColor(PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR, num12, num13, num14, num15);
					}
				}
			}
			if (this.drawPlayer.socialGhost)
			{
				this.colorEyeWhites = Color.Transparent;
				this.colorEyes = Color.Transparent;
				this.colorHair = Color.Transparent;
				this.colorHead = Color.Transparent;
				this.colorBodySkin = Color.Transparent;
				this.colorShirt = Color.Transparent;
				this.colorUnderShirt = Color.Transparent;
				this.colorPants = Color.Transparent;
				this.colorShoes = Color.Transparent;
				this.colorLegs = Color.Transparent;
				if (this.colorArmorHead.A > Main.gFade)
				{
					this.colorArmorHead.A = Main.gFade;
				}
				if (this.colorArmorBody.A > Main.gFade)
				{
					this.colorArmorBody.A = Main.gFade;
				}
				if (this.colorArmorLegs.A > Main.gFade)
				{
					this.colorArmorLegs.A = Main.gFade;
				}
				if (this.drawPlayer.isDisplayDollOrInanimate)
				{
					this.colorDisplayDollSkin = Color.Transparent;
				}
			}
			if (this.drawPlayer.socialIgnoreLight)
			{
				float num23 = 1f;
				this.colorEyeWhites = Color.White * num23;
				this.colorEyes = this.drawPlayer.eyeColor * num23;
				this.colorHair = GameShaders.Hair.GetColor(this.drawPlayer.hairDye, this.drawPlayer, Color.White);
				this.colorHead = this.drawPlayer.skinColor * num23;
				this.colorBodySkin = this.drawPlayer.skinColor * num23;
				this.colorShirt = this.drawPlayer.shirtColor * num23;
				this.colorUnderShirt = this.drawPlayer.underShirtColor * num23;
				this.colorPants = this.drawPlayer.pantsColor * num23;
				this.colorShoes = this.drawPlayer.shoeColor * num23;
				this.colorLegs = this.drawPlayer.skinColor * num23;
				this.colorArmorHead = Color.White;
				this.colorArmorBody = Color.White;
				this.colorArmorLegs = Color.White;
				if (this.drawPlayer.isDisplayDollOrInanimate)
				{
					this.colorDisplayDollSkin = PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR * num23;
				}
			}
			if (this.drawPlayer.opacityForAnimation != 1f)
			{
				this.shadow = 1f - this.drawPlayer.opacityForAnimation;
				float opacityForAnimation = this.drawPlayer.opacityForAnimation;
				opacityForAnimation *= opacityForAnimation;
				this.colorEyeWhites = Color.White * opacityForAnimation;
				this.colorEyes = this.drawPlayer.eyeColor * opacityForAnimation;
				this.colorHair = GameShaders.Hair.GetColor(this.drawPlayer.hairDye, this.drawPlayer, Color.White) * opacityForAnimation;
				this.colorHead = this.drawPlayer.skinColor * opacityForAnimation;
				this.colorBodySkin = this.drawPlayer.skinColor * opacityForAnimation;
				this.colorShirt = this.drawPlayer.shirtColor * opacityForAnimation;
				this.colorUnderShirt = this.drawPlayer.underShirtColor * opacityForAnimation;
				this.colorPants = this.drawPlayer.pantsColor * opacityForAnimation;
				this.colorShoes = this.drawPlayer.shoeColor * opacityForAnimation;
				this.colorLegs = this.drawPlayer.skinColor * opacityForAnimation;
				this.colorArmorHead = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
				this.colorArmorBody = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
				this.colorArmorLegs = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
				if (this.drawPlayer.isDisplayDollOrInanimate)
				{
					this.colorDisplayDollSkin = PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR * opacityForAnimation;
				}
			}
			this.stealth = 1f;
			if (this.heldItem.type == 3106)
			{
				float num24 = this.drawPlayer.stealth;
				if ((double)num24 < 0.03)
				{
					num24 = 0.03f;
				}
				float num25 = (1f + num24 * 10f) / 11f;
				if (num24 < 0f)
				{
					num24 = 0f;
				}
				if (num24 >= 1f - this.shadow && this.shadow > 0f)
				{
					num24 = this.shadow * 0.5f;
				}
				this.stealth = num25;
				this.colorArmorHead = new Color((int)((byte)((float)this.colorArmorHead.R * num24)), (int)((byte)((float)this.colorArmorHead.G * num24)), (int)((byte)((float)this.colorArmorHead.B * num25)), (int)((byte)((float)this.colorArmorHead.A * num24)));
				this.colorArmorBody = new Color((int)((byte)((float)this.colorArmorBody.R * num24)), (int)((byte)((float)this.colorArmorBody.G * num24)), (int)((byte)((float)this.colorArmorBody.B * num25)), (int)((byte)((float)this.colorArmorBody.A * num24)));
				this.colorArmorLegs = new Color((int)((byte)((float)this.colorArmorLegs.R * num24)), (int)((byte)((float)this.colorArmorLegs.G * num24)), (int)((byte)((float)this.colorArmorLegs.B * num25)), (int)((byte)((float)this.colorArmorLegs.A * num24)));
				num24 *= num24;
				this.colorEyeWhites = Color.Multiply(this.colorEyeWhites, num24);
				this.colorEyes = Color.Multiply(this.colorEyes, num24);
				this.colorHair = Color.Multiply(this.colorHair, num24);
				this.colorHead = Color.Multiply(this.colorHead, num24);
				this.colorBodySkin = Color.Multiply(this.colorBodySkin, num24);
				this.colorShirt = Color.Multiply(this.colorShirt, num24);
				this.colorUnderShirt = Color.Multiply(this.colorUnderShirt, num24);
				this.colorPants = Color.Multiply(this.colorPants, num24);
				this.colorShoes = Color.Multiply(this.colorShoes, num24);
				this.colorLegs = Color.Multiply(this.colorLegs, num24);
				this.colorMount = Color.Multiply(this.colorMount, num24);
				this.headGlowColor = Color.Multiply(this.headGlowColor, num24);
				this.bodyGlowColor = Color.Multiply(this.bodyGlowColor, num24);
				this.armGlowColor = Color.Multiply(this.armGlowColor, num24);
				this.legsGlowColor = Color.Multiply(this.legsGlowColor, num24);
				if (this.drawPlayer.isDisplayDollOrInanimate)
				{
					this.colorDisplayDollSkin = Color.Multiply(this.colorDisplayDollSkin, num24);
				}
			}
			else if (this.drawPlayer.shroomiteStealth)
			{
				float num26 = this.drawPlayer.stealth;
				if ((double)num26 < 0.03)
				{
					num26 = 0.03f;
				}
				float num27 = (1f + num26 * 10f) / 11f;
				if (num26 < 0f)
				{
					num26 = 0f;
				}
				if (num26 >= 1f - this.shadow && this.shadow > 0f)
				{
					num26 = this.shadow * 0.5f;
				}
				this.stealth = num27;
				this.colorArmorHead = new Color((int)((byte)((float)this.colorArmorHead.R * num26)), (int)((byte)((float)this.colorArmorHead.G * num26)), (int)((byte)((float)this.colorArmorHead.B * num27)), (int)((byte)((float)this.colorArmorHead.A * num26)));
				this.colorArmorBody = new Color((int)((byte)((float)this.colorArmorBody.R * num26)), (int)((byte)((float)this.colorArmorBody.G * num26)), (int)((byte)((float)this.colorArmorBody.B * num27)), (int)((byte)((float)this.colorArmorBody.A * num26)));
				this.colorArmorLegs = new Color((int)((byte)((float)this.colorArmorLegs.R * num26)), (int)((byte)((float)this.colorArmorLegs.G * num26)), (int)((byte)((float)this.colorArmorLegs.B * num27)), (int)((byte)((float)this.colorArmorLegs.A * num26)));
				num26 *= num26;
				this.colorEyeWhites = Color.Multiply(this.colorEyeWhites, num26);
				this.colorEyes = Color.Multiply(this.colorEyes, num26);
				this.colorHair = Color.Multiply(this.colorHair, num26);
				this.colorHead = Color.Multiply(this.colorHead, num26);
				this.colorBodySkin = Color.Multiply(this.colorBodySkin, num26);
				this.colorShirt = Color.Multiply(this.colorShirt, num26);
				this.colorUnderShirt = Color.Multiply(this.colorUnderShirt, num26);
				this.colorPants = Color.Multiply(this.colorPants, num26);
				this.colorShoes = Color.Multiply(this.colorShoes, num26);
				this.colorLegs = Color.Multiply(this.colorLegs, num26);
				this.colorMount = Color.Multiply(this.colorMount, num26);
				this.headGlowColor = Color.Multiply(this.headGlowColor, num26);
				this.bodyGlowColor = Color.Multiply(this.bodyGlowColor, num26);
				this.armGlowColor = Color.Multiply(this.armGlowColor, num26);
				this.legsGlowColor = Color.Multiply(this.legsGlowColor, num26);
				if (this.drawPlayer.isDisplayDollOrInanimate)
				{
					this.colorDisplayDollSkin = Color.Multiply(this.colorDisplayDollSkin, num26);
				}
			}
			else if (this.drawPlayer.setVortex)
			{
				float num28 = this.drawPlayer.stealth;
				if ((double)num28 < 0.03)
				{
					num28 = 0.03f;
				}
				if (num28 < 0f)
				{
					num28 = 0f;
				}
				if (num28 >= 1f - this.shadow && this.shadow > 0f)
				{
					num28 = this.shadow * 0.5f;
				}
				this.stealth = num28;
				Color secondColor;
				secondColor..ctor(Vector4.Lerp(Vector4.One, new Vector4(0f, 0.12f, 0.16f, 0f), 1f - num28));
				this.colorArmorHead = this.colorArmorHead.MultiplyRGBA(secondColor);
				this.colorArmorBody = this.colorArmorBody.MultiplyRGBA(secondColor);
				this.colorArmorLegs = this.colorArmorLegs.MultiplyRGBA(secondColor);
				num28 *= num28;
				this.colorEyeWhites = Color.Multiply(this.colorEyeWhites, num28);
				this.colorEyes = Color.Multiply(this.colorEyes, num28);
				this.colorHair = Color.Multiply(this.colorHair, num28);
				this.colorHead = Color.Multiply(this.colorHead, num28);
				this.colorBodySkin = Color.Multiply(this.colorBodySkin, num28);
				this.colorShirt = Color.Multiply(this.colorShirt, num28);
				this.colorUnderShirt = Color.Multiply(this.colorUnderShirt, num28);
				this.colorPants = Color.Multiply(this.colorPants, num28);
				this.colorShoes = Color.Multiply(this.colorShoes, num28);
				this.colorLegs = Color.Multiply(this.colorLegs, num28);
				this.colorMount = Color.Multiply(this.colorMount, num28);
				this.headGlowColor = Color.Multiply(this.headGlowColor, num28);
				this.bodyGlowColor = Color.Multiply(this.bodyGlowColor, num28);
				this.armGlowColor = Color.Multiply(this.armGlowColor, num28);
				this.legsGlowColor = Color.Multiply(this.legsGlowColor, num28);
				if (this.drawPlayer.isDisplayDollOrInanimate)
				{
					this.colorDisplayDollSkin = Color.Multiply(this.colorDisplayDollSkin, num28);
				}
			}
			if (this.hideEntirePlayer)
			{
				this.stealth = 1f;
				this.colorDisplayDollSkin = (this.legsGlowColor = (this.armGlowColor = (this.bodyGlowColor = (this.headGlowColor = (this.colorLegs = (this.colorShoes = (this.colorPants = (this.colorUnderShirt = (this.colorShirt = (this.colorBodySkin = (this.colorHead = (this.colorHair = (this.colorEyes = (this.colorEyeWhites = (this.colorArmorLegs = (this.colorArmorBody = (this.colorArmorHead = Color.Transparent)))))))))))))))));
			}
			if (this.drawPlayer.gravDir == 1f)
			{
				if (this.drawPlayer.direction == 1)
				{
					this.playerEffect = 0;
					this.itemEffect = 0;
				}
				else
				{
					this.playerEffect = 1;
					this.itemEffect = 1;
				}
				if (!this.drawPlayer.dead)
				{
					this.drawPlayer.legPosition.Y = 0f;
					this.drawPlayer.headPosition.Y = 0f;
					this.drawPlayer.bodyPosition.Y = 0f;
				}
			}
			else
			{
				if (this.drawPlayer.direction == 1)
				{
					this.playerEffect = 2;
					this.itemEffect = 2;
				}
				else
				{
					this.playerEffect = 3;
					this.itemEffect = 3;
				}
				if (!this.drawPlayer.dead)
				{
					this.drawPlayer.legPosition.Y = 6f;
					this.drawPlayer.headPosition.Y = 6f;
					this.drawPlayer.bodyPosition.Y = 6f;
				}
			}
			num29 = this.heldItem.type;
			if (num29 <= 3185)
			{
				if (num29 != 3182 && num29 - 3184 > 1)
				{
					goto IL_4EBC;
				}
			}
			else if (num29 != 3782)
			{
				if (num29 != 5118)
				{
					goto IL_4EBC;
				}
				if (player.gravDir < 0f)
				{
					this.itemEffect ^= 3;
					goto IL_4EBC;
				}
				goto IL_4EBC;
			}
			this.itemEffect ^= 3;
			IL_4EBC:
			this.legVect = new Vector2((float)this.drawPlayer.legFrame.Width * 0.5f, (float)this.drawPlayer.legFrame.Height * 0.75f);
			this.bodyVect = new Vector2((float)this.drawPlayer.legFrame.Width * 0.5f, (float)this.drawPlayer.legFrame.Height * 0.5f);
			this.headVect = new Vector2((float)this.drawPlayer.legFrame.Width * 0.5f, (float)this.drawPlayer.legFrame.Height * 0.4f);
			if ((this.drawPlayer.merman || this.drawPlayer.forceMerman) && !this.drawPlayer.hideMerman)
			{
				this.drawPlayer.headRotation = this.drawPlayer.velocity.Y * (float)this.drawPlayer.direction * 0.1f;
				if ((double)this.drawPlayer.headRotation < -0.3)
				{
					this.drawPlayer.headRotation = -0.3f;
				}
				if ((double)this.drawPlayer.headRotation > 0.3)
				{
					this.drawPlayer.headRotation = 0.3f;
				}
			}
			else if (!this.drawPlayer.dead)
			{
				this.drawPlayer.headRotation = 0f;
			}
			this.SetupHairFrames();
			this.BoringSetup_End();
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x00654924 File Offset: 0x00652B24
		private void SetupHairFrames()
		{
			Rectangle bodyFrame = this.drawPlayer.bodyFrame;
			bodyFrame = this.drawPlayer.bodyFrame;
			bodyFrame.Y -= 336;
			if (bodyFrame.Y < 0)
			{
				bodyFrame.Y = 0;
			}
			this.hairFrontFrame = bodyFrame;
			this.hairBackFrame = bodyFrame;
			if (this.hideHair)
			{
				this.hairFrontFrame.Height = 0;
				this.hairBackFrame.Height = 0;
				return;
			}
			if (this.backHairDraw)
			{
				int height = 26;
				this.hairFrontFrame.Height = height;
			}
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x006549B0 File Offset: 0x00652BB0
		private void BoringSetup_End()
		{
			this.hidesTopSkin = ((this.drawPlayer.body > 0 && ArmorIDs.Body.Sets.HidesTopSkin[this.drawPlayer.body]) || (this.drawPlayer.legs > 0 && ArmorIDs.Legs.Sets.HidesTopSkin[this.drawPlayer.legs]));
			this.hidesBottomSkin = ((this.drawPlayer.body > 0 && ArmorIDs.Body.Sets.HidesBottomSkin[this.drawPlayer.body]) || (this.drawPlayer.legs > 0 && ArmorIDs.Legs.Sets.HidesBottomSkin[this.drawPlayer.legs]));
			this.isBottomOverriden = PlayerDrawLayers.IsBottomOverridden(ref this);
			this.drawFloatingTube = (this.drawPlayer.hasFloatingTube && !this.hideEntirePlayer);
			this.drawUnicornHorn = this.drawPlayer.hasUnicornHorn;
			this.drawAngelHalo = this.drawPlayer.hasAngelHalo;
			this.drawFrontAccInNeckAccLayer = false;
			if (this.drawPlayer.bodyFrame.Y / this.drawPlayer.bodyFrame.Height == 5)
			{
				this.drawFrontAccInNeckAccLayer = (this.drawPlayer.front > 0 && ArmorIDs.Front.Sets.DrawsInNeckLayer[this.drawPlayer.front]);
			}
			this.hairOffset = this.drawPlayer.GetHairDrawOffset(this.drawPlayer.hair, this.hatHair);
			this.helmetOffset = this.drawPlayer.GetHelmetDrawOffset();
			this.legsOffset = this.drawPlayer.GetLegsDrawOffset();
			this.CreateCompositeData();
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x00654B40 File Offset: 0x00652D40
		private void AdjustmentsForWolfMount()
		{
			this.hideEntirePlayer = true;
			this.weaponDrawOrder = WeaponDrawOrder.BehindBackArm;
			Vector2 vector;
			vector..ctor((float)(10 + this.drawPlayer.direction * 14), 12f);
			Vector2 vector2 = this.Position + vector;
			this.Position.X = this.Position.X - (float)(this.drawPlayer.direction * 10);
			bool flag = this.drawPlayer.heldProj != -1 || this.heldItem.useStyle == 5;
			bool flag10 = this.heldItem.useStyle == 2;
			bool flag2 = this.heldItem.useStyle == 9;
			bool flag3 = this.drawPlayer.itemAnimation > 0;
			bool flag4 = this.heldItem.fishingPole != 0;
			bool flag5 = this.heldItem.useStyle == 14;
			bool flag6 = this.heldItem.useStyle == 8;
			bool flag7 = this.heldItem.holdStyle == 1;
			bool flag8 = this.heldItem.holdStyle == 2;
			bool flag9 = this.heldItem.holdStyle == 5;
			if (flag10)
			{
				this.ItemLocation += new Vector2((float)(this.drawPlayer.direction * 14), -4f);
				return;
			}
			if (!flag4)
			{
				if (flag2)
				{
					this.ItemLocation += (flag3 ? new Vector2((float)(this.drawPlayer.direction * 18), -4f) : new Vector2((float)(this.drawPlayer.direction * 14), -18f));
					return;
				}
				if (flag9)
				{
					this.ItemLocation += new Vector2((float)(this.drawPlayer.direction * 17), -8f);
					return;
				}
				if (flag7 && this.drawPlayer.itemAnimation == 0)
				{
					this.ItemLocation += new Vector2((float)(this.drawPlayer.direction * 14), -6f);
					return;
				}
				if (flag8 && this.drawPlayer.itemAnimation == 0)
				{
					this.ItemLocation += new Vector2((float)(this.drawPlayer.direction * 17), 4f);
					return;
				}
				if (flag6)
				{
					this.ItemLocation = vector2 + new Vector2((float)(this.drawPlayer.direction * 12), 2f);
					return;
				}
				if (flag5)
				{
					this.ItemLocation += new Vector2((float)(this.drawPlayer.direction * 5), -2f);
					return;
				}
				if (flag)
				{
					this.ItemLocation += new Vector2((float)(this.drawPlayer.direction * 4), -4f);
					return;
				}
				this.ItemLocation = vector2;
			}
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x00654E08 File Offset: 0x00653008
		private void CreateCompositeData()
		{
			this.frontShoulderOffset = Vector2.Zero;
			this.backShoulderOffset = Vector2.Zero;
			this.usesCompositeTorso = (this.drawPlayer.body > 0 && ArmorIDs.Body.Sets.UsesNewFramingCode[this.drawPlayer.body]);
			this.usesCompositeFrontHandAcc = (this.drawPlayer.handon > 0 && ArmorIDs.HandOn.Sets.UsesNewFramingCode[this.drawPlayer.handon]);
			this.usesCompositeBackHandAcc = (this.drawPlayer.handoff > 0 && ArmorIDs.HandOff.Sets.UsesNewFramingCode[this.drawPlayer.handoff]);
			if (this.drawPlayer.body < 1)
			{
				this.usesCompositeTorso = true;
			}
			if (!this.usesCompositeTorso)
			{
				return;
			}
			Point pt;
			pt..ctor(1, 1);
			Point pt2;
			pt2..ctor(0, 1);
			Point pt3 = default(Point);
			Point frameIndex = default(Point);
			Point frameIndex2 = default(Point);
			int num = this.drawPlayer.bodyFrame.Y / this.drawPlayer.bodyFrame.Height;
			this.compShoulderOverFrontArm = true;
			this.hideCompositeShoulders = false;
			bool flag = true;
			if (this.drawPlayer.body > 0)
			{
				flag = ArmorIDs.Body.Sets.showsShouldersWhileJumping[this.drawPlayer.body];
			}
			bool flag2 = false;
			if (this.drawPlayer.handon > 0)
			{
				flag2 = ArmorIDs.HandOn.Sets.UsesOldFramingTexturesForWalking[this.drawPlayer.handon];
			}
			bool flag3 = !flag2;
			switch (num)
			{
			case 0:
				frameIndex2.X = 2;
				flag3 = true;
				break;
			case 1:
				frameIndex2.X = 3;
				this.compShoulderOverFrontArm = false;
				flag3 = true;
				break;
			case 2:
				frameIndex2.X = 4;
				this.compShoulderOverFrontArm = false;
				flag3 = true;
				break;
			case 3:
				frameIndex2.X = 5;
				this.compShoulderOverFrontArm = true;
				flag3 = true;
				break;
			case 4:
				frameIndex2.X = 6;
				this.compShoulderOverFrontArm = true;
				flag3 = true;
				break;
			case 5:
				frameIndex2.X = 2;
				frameIndex2.Y = 1;
				pt3.X = 1;
				this.compShoulderOverFrontArm = false;
				flag3 = true;
				if (!flag)
				{
					this.hideCompositeShoulders = true;
				}
				break;
			case 6:
				frameIndex2.X = 3;
				frameIndex2.Y = 1;
				break;
			case 7:
			case 8:
			case 9:
			case 10:
				frameIndex2.X = 4;
				frameIndex2.Y = 1;
				break;
			case 11:
			case 12:
			case 13:
				frameIndex2.X = 3;
				frameIndex2.Y = 1;
				break;
			case 14:
				frameIndex2.X = 5;
				frameIndex2.Y = 1;
				break;
			case 15:
			case 16:
				frameIndex2.X = 6;
				frameIndex2.Y = 1;
				break;
			case 17:
				frameIndex2.X = 5;
				frameIndex2.Y = 1;
				break;
			case 18:
			case 19:
				frameIndex2.X = 3;
				frameIndex2.Y = 1;
				break;
			}
			this.CreateCompositeData_DetermineShoulderOffsets(this.drawPlayer.body, num);
			this.backShoulderOffset *= new Vector2((float)this.drawPlayer.direction, this.drawPlayer.gravDir);
			this.frontShoulderOffset *= new Vector2((float)this.drawPlayer.direction, this.drawPlayer.gravDir);
			if (this.drawPlayer.body > 0 && ArmorIDs.Body.Sets.shouldersAreAlwaysInTheBack[this.drawPlayer.body])
			{
				this.compShoulderOverFrontArm = false;
			}
			this.usesCompositeFrontHandAcc = flag3;
			frameIndex.X = frameIndex2.X;
			frameIndex.Y = frameIndex2.Y + 2;
			this.UpdateCompositeArm(this.drawPlayer.compositeFrontArm, ref this.compositeFrontArmRotation, ref frameIndex2, 7);
			this.UpdateCompositeArm(this.drawPlayer.compositeBackArm, ref this.compositeBackArmRotation, ref frameIndex, 8);
			if (!this.drawPlayer.Male)
			{
				pt.Y += 2;
				pt2.Y += 2;
				pt3.Y += 2;
			}
			this.compBackShoulderFrame = this.CreateCompositeFrameRect(pt);
			this.compFrontShoulderFrame = this.CreateCompositeFrameRect(pt2);
			this.compBackArmFrame = this.CreateCompositeFrameRect(frameIndex);
			this.compFrontArmFrame = this.CreateCompositeFrameRect(frameIndex2);
			this.compTorsoFrame = this.CreateCompositeFrameRect(pt3);
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x00655244 File Offset: 0x00653444
		private void CreateCompositeData_DetermineShoulderOffsets(int armor, int targetFrameNumber)
		{
			int num = 0;
			if (armor <= 101)
			{
				if (armor != 55)
				{
					if (armor != 71)
					{
						if (armor == 101)
						{
							num = 6;
						}
					}
					else
					{
						num = 2;
					}
				}
				else
				{
					num = 1;
				}
			}
			else if (armor <= 201)
			{
				if (armor != 183)
				{
					if (armor == 201)
					{
						num = 5;
					}
				}
				else
				{
					num = 4;
				}
			}
			else if (armor != 204)
			{
				if (armor == 207)
				{
					num = 7;
				}
			}
			else
			{
				num = 3;
			}
			switch (num)
			{
			case 1:
				switch (targetFrameNumber)
				{
				case 6:
					this.frontShoulderOffset.X = -2f;
					return;
				case 7:
				case 8:
				case 9:
				case 10:
					this.frontShoulderOffset.X = -4f;
					return;
				case 11:
				case 12:
				case 13:
				case 14:
					this.frontShoulderOffset.X = -2f;
					return;
				case 15:
				case 16:
				case 17:
					break;
				case 18:
				case 19:
					this.frontShoulderOffset.X = -2f;
					return;
				default:
					return;
				}
				break;
			case 2:
				switch (targetFrameNumber)
				{
				case 6:
					this.frontShoulderOffset.X = -2f;
					return;
				case 7:
				case 8:
				case 9:
				case 10:
					this.frontShoulderOffset.X = -4f;
					return;
				case 11:
				case 12:
				case 13:
				case 14:
					this.frontShoulderOffset.X = -2f;
					return;
				case 15:
				case 16:
				case 17:
					break;
				case 18:
				case 19:
					this.frontShoulderOffset.X = -2f;
					return;
				default:
					return;
				}
				break;
			case 3:
				if (targetFrameNumber - 7 <= 2)
				{
					this.frontShoulderOffset.X = -2f;
					return;
				}
				if (targetFrameNumber - 15 > 2)
				{
					return;
				}
				this.frontShoulderOffset.X = 2f;
				return;
			case 4:
				switch (targetFrameNumber)
				{
				case 6:
					this.frontShoulderOffset.X = -2f;
					return;
				case 7:
				case 8:
				case 9:
				case 10:
					this.frontShoulderOffset.X = -4f;
					return;
				case 11:
				case 12:
				case 13:
					this.frontShoulderOffset.X = -2f;
					return;
				case 14:
				case 17:
					break;
				case 15:
				case 16:
					this.frontShoulderOffset.X = 2f;
					return;
				case 18:
				case 19:
					this.frontShoulderOffset.X = -2f;
					return;
				default:
					return;
				}
				break;
			case 5:
				if (targetFrameNumber - 7 <= 3)
				{
					this.frontShoulderOffset.X = -2f;
					return;
				}
				if (targetFrameNumber - 15 > 1)
				{
					return;
				}
				this.frontShoulderOffset.X = 2f;
				return;
			case 6:
				if (targetFrameNumber - 7 <= 3)
				{
					this.frontShoulderOffset.X = -2f;
					return;
				}
				if (targetFrameNumber - 14 > 3)
				{
					return;
				}
				this.frontShoulderOffset.X = 2f;
				return;
			case 7:
				switch (targetFrameNumber)
				{
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
					this.frontShoulderOffset.X = -2f;
					return;
				case 11:
				case 12:
				case 13:
				case 14:
					this.frontShoulderOffset.X = -2f;
					return;
				case 15:
				case 16:
				case 17:
					break;
				case 18:
				case 19:
					this.frontShoulderOffset.X = -2f;
					break;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x00655577 File Offset: 0x00653777
		private Rectangle CreateCompositeFrameRect(Point pt)
		{
			return new Rectangle(pt.X * 40, pt.Y * 56, 40, 56);
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x00655594 File Offset: 0x00653794
		private void UpdateCompositeArm(Player.CompositeArmData data, ref float rotation, ref Point frameIndex, int targetX)
		{
			if (!data.enabled)
			{
				rotation = 0f;
				return;
			}
			rotation = data.rotation;
			switch (data.stretch)
			{
			case Player.CompositeArmStretchAmount.Full:
				frameIndex.X = targetX;
				frameIndex.Y = 0;
				return;
			case Player.CompositeArmStretchAmount.None:
				frameIndex.X = targetX;
				frameIndex.Y = 3;
				return;
			case Player.CompositeArmStretchAmount.Quarter:
				frameIndex.X = targetX;
				frameIndex.Y = 2;
				return;
			case Player.CompositeArmStretchAmount.ThreeQuarters:
				frameIndex.X = targetX;
				frameIndex.Y = 1;
				return;
			default:
				return;
			}
		}

		// Token: 0x04005F31 RID: 24369
		public List<DrawData> DrawDataCache;

		// Token: 0x04005F32 RID: 24370
		public List<int> DustCache;

		// Token: 0x04005F33 RID: 24371
		public List<int> GoreCache;

		// Token: 0x04005F34 RID: 24372
		public Player drawPlayer;

		// Token: 0x04005F35 RID: 24373
		public float shadow;

		// Token: 0x04005F36 RID: 24374
		public Vector2 Position;

		// Token: 0x04005F37 RID: 24375
		public int projectileDrawPosition;

		// Token: 0x04005F38 RID: 24376
		public Vector2 ItemLocation;

		// Token: 0x04005F39 RID: 24377
		public int armorAdjust;

		// Token: 0x04005F3A RID: 24378
		public bool armorHidesHands;

		// Token: 0x04005F3B RID: 24379
		public bool armorHidesArms;

		// Token: 0x04005F3C RID: 24380
		public bool heldProjOverHand;

		// Token: 0x04005F3D RID: 24381
		public int skinVar;

		// Token: 0x04005F3E RID: 24382
		public bool fullHair;

		// Token: 0x04005F3F RID: 24383
		public bool drawsBackHairWithoutHeadgear;

		// Token: 0x04005F40 RID: 24384
		public bool hatHair;

		// Token: 0x04005F41 RID: 24385
		public bool hideHair;

		// Token: 0x04005F42 RID: 24386
		public int hairDyePacked;

		// Token: 0x04005F43 RID: 24387
		public int skinDyePacked;

		// Token: 0x04005F44 RID: 24388
		public float mountOffSet;

		// Token: 0x04005F45 RID: 24389
		public int cHead;

		// Token: 0x04005F46 RID: 24390
		public int cBody;

		// Token: 0x04005F47 RID: 24391
		public int cLegs;

		// Token: 0x04005F48 RID: 24392
		public int cHandOn;

		// Token: 0x04005F49 RID: 24393
		public int cHandOff;

		// Token: 0x04005F4A RID: 24394
		public int cBack;

		// Token: 0x04005F4B RID: 24395
		public int cFront;

		// Token: 0x04005F4C RID: 24396
		public int cShoe;

		// Token: 0x04005F4D RID: 24397
		public int cFlameWaker;

		// Token: 0x04005F4E RID: 24398
		public int cWaist;

		// Token: 0x04005F4F RID: 24399
		public int cShield;

		// Token: 0x04005F50 RID: 24400
		public int cNeck;

		// Token: 0x04005F51 RID: 24401
		public int cFace;

		// Token: 0x04005F52 RID: 24402
		public int cBalloon;

		// Token: 0x04005F53 RID: 24403
		public int cWings;

		// Token: 0x04005F54 RID: 24404
		public int cCarpet;

		// Token: 0x04005F55 RID: 24405
		public int cPortableStool;

		// Token: 0x04005F56 RID: 24406
		public int cFloatingTube;

		// Token: 0x04005F57 RID: 24407
		public int cUnicornHorn;

		// Token: 0x04005F58 RID: 24408
		public int cAngelHalo;

		// Token: 0x04005F59 RID: 24409
		public int cBeard;

		// Token: 0x04005F5A RID: 24410
		public int cLeinShampoo;

		// Token: 0x04005F5B RID: 24411
		public int cBackpack;

		// Token: 0x04005F5C RID: 24412
		public int cTail;

		// Token: 0x04005F5D RID: 24413
		public int cFaceHead;

		// Token: 0x04005F5E RID: 24414
		public int cFaceFlower;

		// Token: 0x04005F5F RID: 24415
		public int cBalloonFront;

		// Token: 0x04005F60 RID: 24416
		public SpriteEffects playerEffect;

		// Token: 0x04005F61 RID: 24417
		public SpriteEffects itemEffect;

		// Token: 0x04005F62 RID: 24418
		public Color colorHair;

		// Token: 0x04005F63 RID: 24419
		public Color colorEyeWhites;

		// Token: 0x04005F64 RID: 24420
		public Color colorEyes;

		// Token: 0x04005F65 RID: 24421
		public Color colorHead;

		// Token: 0x04005F66 RID: 24422
		public Color colorBodySkin;

		// Token: 0x04005F67 RID: 24423
		public Color colorLegs;

		// Token: 0x04005F68 RID: 24424
		public Color colorShirt;

		// Token: 0x04005F69 RID: 24425
		public Color colorUnderShirt;

		// Token: 0x04005F6A RID: 24426
		public Color colorPants;

		// Token: 0x04005F6B RID: 24427
		public Color colorShoes;

		// Token: 0x04005F6C RID: 24428
		public Color colorArmorHead;

		// Token: 0x04005F6D RID: 24429
		public Color colorArmorBody;

		// Token: 0x04005F6E RID: 24430
		public Color colorMount;

		// Token: 0x04005F6F RID: 24431
		public Color colorArmorLegs;

		// Token: 0x04005F70 RID: 24432
		public Color colorElectricity;

		// Token: 0x04005F71 RID: 24433
		public Color colorDisplayDollSkin;

		// Token: 0x04005F72 RID: 24434
		public int headGlowMask;

		// Token: 0x04005F73 RID: 24435
		public int bodyGlowMask;

		// Token: 0x04005F74 RID: 24436
		public int armGlowMask;

		// Token: 0x04005F75 RID: 24437
		public int legsGlowMask;

		// Token: 0x04005F76 RID: 24438
		public Color headGlowColor;

		// Token: 0x04005F77 RID: 24439
		public Color bodyGlowColor;

		// Token: 0x04005F78 RID: 24440
		public Color armGlowColor;

		// Token: 0x04005F79 RID: 24441
		public Color legsGlowColor;

		// Token: 0x04005F7A RID: 24442
		public Color ArkhalisColor;

		// Token: 0x04005F7B RID: 24443
		public float stealth;

		// Token: 0x04005F7C RID: 24444
		public Vector2 legVect;

		// Token: 0x04005F7D RID: 24445
		public Vector2 bodyVect;

		// Token: 0x04005F7E RID: 24446
		public Vector2 headVect;

		// Token: 0x04005F7F RID: 24447
		public Color selectionGlowColor;

		// Token: 0x04005F80 RID: 24448
		public float torsoOffset;

		// Token: 0x04005F81 RID: 24449
		public bool hidesTopSkin;

		// Token: 0x04005F82 RID: 24450
		public bool hidesBottomSkin;

		// Token: 0x04005F83 RID: 24451
		public float rotation;

		// Token: 0x04005F84 RID: 24452
		public Vector2 rotationOrigin;

		// Token: 0x04005F85 RID: 24453
		public Rectangle hairFrontFrame;

		// Token: 0x04005F86 RID: 24454
		public Rectangle hairBackFrame;

		// Token: 0x04005F87 RID: 24455
		public bool backHairDraw;

		// Token: 0x04005F88 RID: 24456
		public Color itemColor;

		// Token: 0x04005F89 RID: 24457
		public bool usesCompositeTorso;

		// Token: 0x04005F8A RID: 24458
		public bool usesCompositeFrontHandAcc;

		// Token: 0x04005F8B RID: 24459
		public bool usesCompositeBackHandAcc;

		// Token: 0x04005F8C RID: 24460
		public bool compShoulderOverFrontArm;

		// Token: 0x04005F8D RID: 24461
		public Rectangle compBackShoulderFrame;

		// Token: 0x04005F8E RID: 24462
		public Rectangle compFrontShoulderFrame;

		// Token: 0x04005F8F RID: 24463
		public Rectangle compBackArmFrame;

		// Token: 0x04005F90 RID: 24464
		public Rectangle compFrontArmFrame;

		// Token: 0x04005F91 RID: 24465
		public Rectangle compTorsoFrame;

		// Token: 0x04005F92 RID: 24466
		public float compositeBackArmRotation;

		// Token: 0x04005F93 RID: 24467
		public float compositeFrontArmRotation;

		// Token: 0x04005F94 RID: 24468
		public bool hideCompositeShoulders;

		// Token: 0x04005F95 RID: 24469
		public Vector2 frontShoulderOffset;

		// Token: 0x04005F96 RID: 24470
		public Vector2 backShoulderOffset;

		// Token: 0x04005F97 RID: 24471
		public WeaponDrawOrder weaponDrawOrder;

		// Token: 0x04005F98 RID: 24472
		public bool weaponOverFrontArm;

		// Token: 0x04005F99 RID: 24473
		public bool isSitting;

		// Token: 0x04005F9A RID: 24474
		public bool isSleeping;

		// Token: 0x04005F9B RID: 24475
		public float seatYOffset;

		// Token: 0x04005F9C RID: 24476
		public int sittingIndex;

		// Token: 0x04005F9D RID: 24477
		public bool drawFrontAccInNeckAccLayer;

		// Token: 0x04005F9E RID: 24478
		public Item heldItem;

		// Token: 0x04005F9F RID: 24479
		public bool drawFloatingTube;

		// Token: 0x04005FA0 RID: 24480
		public bool drawUnicornHorn;

		// Token: 0x04005FA1 RID: 24481
		public bool drawAngelHalo;

		// Token: 0x04005FA2 RID: 24482
		public Color floatingTubeColor;

		// Token: 0x04005FA3 RID: 24483
		public Vector2 hairOffset;

		// Token: 0x04005FA4 RID: 24484
		public Vector2 helmetOffset;

		// Token: 0x04005FA5 RID: 24485
		public Vector2 legsOffset;

		// Token: 0x04005FA6 RID: 24486
		public bool hideEntirePlayer;

		// Token: 0x04005FA7 RID: 24487
		public bool headOnlyRender;

		// Token: 0x04005FA8 RID: 24488
		public bool isBottomOverriden;
	}
}
