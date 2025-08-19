using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.DataStructures
{
	// Token: 0x02000454 RID: 1108
	public struct PlayerDrawHeadSet
	{
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x005B8C74 File Offset: 0x005B6E74
		public Rectangle HairFrame
		{
			get
			{
				Rectangle result = this.bodyFrameMemory;
				result.Height--;
				return result;
			}
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x005B8C98 File Offset: 0x005B6E98
		public void BoringSetup(Player drawPlayer2, List<DrawData> drawData, List<int> dust, List<int> gore, float X, float Y, float Alpha, float Scale)
		{
			this.DrawData = drawData;
			this.Dust = dust;
			this.Gore = gore;
			this.drawPlayer = drawPlayer2;
			this.Position = this.drawPlayer.position;
			this.cHead = 0;
			this.cFace = 0;
			this.cUnicornHorn = 0;
			this.cAngelHalo = 0;
			this.cBeard = 0;
			this.drawUnicornHorn = false;
			this.drawAngelHalo = false;
			this.skinVar = this.drawPlayer.skinVariant;
			this.hairShaderPacked = PlayerDrawHelper.PackShader((int)this.drawPlayer.hairDye, PlayerDrawHelper.ShaderConfiguration.HairShader);
			if (this.drawPlayer.head == 0 && this.drawPlayer.hairDye == 0)
			{
				this.hairShaderPacked = PlayerDrawHelper.PackShader(1, PlayerDrawHelper.ShaderConfiguration.HairShader);
			}
			this.skinDyePacked = this.drawPlayer.skinDyePacked;
			if (this.drawPlayer.face > 0 && this.drawPlayer.face < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.face);
			}
			this.cHead = this.drawPlayer.cHead;
			this.cFace = this.drawPlayer.cFace;
			this.cFaceHead = this.drawPlayer.cFaceHead;
			this.cFaceFlower = this.drawPlayer.cFaceFlower;
			this.cUnicornHorn = this.drawPlayer.cUnicornHorn;
			this.cAngelHalo = this.drawPlayer.cAngelHalo;
			this.cBeard = this.drawPlayer.cBeard;
			this.drawUnicornHorn = this.drawPlayer.hasUnicornHorn;
			this.drawAngelHalo = this.drawPlayer.hasAngelHalo;
			Main.instance.LoadHair(this.drawPlayer.hair);
			this.scale = Scale;
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
			this.playerEffect = SpriteEffects.None;
			if (this.drawPlayer.direction < 0)
			{
				this.playerEffect = SpriteEffects.FlipHorizontally;
			}
			this.headVect = new Vector2((float)this.drawPlayer.legFrame.Width * 0.5f, (float)this.drawPlayer.legFrame.Height * 0.4f);
			this.bodyFrameMemory = this.drawPlayer.bodyFrame;
			this.bodyFrameMemory.Y = 0;
			this.Position = Main.screenPosition;
			this.Position.X = this.Position.X + X;
			this.Position.Y = this.Position.Y + Y;
			this.Position.X = this.Position.X - 6f;
			this.Position.Y = this.Position.Y - 4f;
			this.Position.Y = this.Position.Y - (float)this.drawPlayer.HeightMapOffset;
			if (this.drawPlayer.head > 0 && this.drawPlayer.head < ArmorIDs.Head.Count)
			{
				Main.instance.LoadArmorHead(this.drawPlayer.head);
				int num = ArmorIDs.Head.Sets.FrontToBackID[this.drawPlayer.head];
				if (num >= 0)
				{
					Main.instance.LoadArmorHead(num);
				}
			}
			if (this.drawPlayer.face > 0 && this.drawPlayer.face < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.face);
			}
			if (this.drawPlayer.faceHead > 0 && this.drawPlayer.faceHead < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.faceHead);
			}
			if (this.drawPlayer.faceFlower > 0 && this.drawPlayer.faceFlower < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.faceFlower);
			}
			if (this.drawPlayer.beard > 0 && this.drawPlayer.beard < ArmorIDs.Beard.Count)
			{
				Main.instance.LoadAccBeard((int)this.drawPlayer.beard);
			}
			bool flag;
			this.drawPlayer.GetHairSettings(out this.fullHair, out this.hatHair, out this.hideHair, out flag, out this.helmetIsOverFullHair);
			this.hairOffset = this.drawPlayer.GetHairDrawOffset(this.drawPlayer.hair, this.hatHair);
			this.hairOffset.Y = this.hairOffset.Y * this.drawPlayer.Directions.Y;
			this.helmetOffset = this.drawPlayer.GetHelmetDrawOffset();
			this.helmetOffset.Y = this.helmetOffset.Y * this.drawPlayer.Directions.Y;
			this.helmetIsTall = (this.drawPlayer.head == 14 || this.drawPlayer.head == 56 || this.drawPlayer.head == 158);
			this.helmetIsNormal = (!this.helmetIsTall && !this.helmetIsOverFullHair && this.drawPlayer.head > 0 && this.drawPlayer.head < ArmorIDs.Head.Count && this.drawPlayer.head != 28);
		}

		// Token: 0x040050AE RID: 20654
		public List<DrawData> DrawData;

		// Token: 0x040050AF RID: 20655
		public List<int> Dust;

		// Token: 0x040050B0 RID: 20656
		public List<int> Gore;

		// Token: 0x040050B1 RID: 20657
		public Player drawPlayer;

		// Token: 0x040050B2 RID: 20658
		public int cHead;

		// Token: 0x040050B3 RID: 20659
		public int cFace;

		// Token: 0x040050B4 RID: 20660
		public int cFaceHead;

		// Token: 0x040050B5 RID: 20661
		public int cFaceFlower;

		// Token: 0x040050B6 RID: 20662
		public int cUnicornHorn;

		// Token: 0x040050B7 RID: 20663
		public int cAngelHalo;

		// Token: 0x040050B8 RID: 20664
		public int cBeard;

		// Token: 0x040050B9 RID: 20665
		public int skinVar;

		// Token: 0x040050BA RID: 20666
		public int hairShaderPacked;

		// Token: 0x040050BB RID: 20667
		public int skinDyePacked;

		// Token: 0x040050BC RID: 20668
		public float scale;

		// Token: 0x040050BD RID: 20669
		public Color colorEyeWhites;

		// Token: 0x040050BE RID: 20670
		public Color colorEyes;

		// Token: 0x040050BF RID: 20671
		public Color colorHair;

		// Token: 0x040050C0 RID: 20672
		public Color colorHead;

		// Token: 0x040050C1 RID: 20673
		public Color colorArmorHead;

		// Token: 0x040050C2 RID: 20674
		public Color colorDisplayDollSkin;

		// Token: 0x040050C3 RID: 20675
		public SpriteEffects playerEffect;

		// Token: 0x040050C4 RID: 20676
		public Vector2 headVect;

		// Token: 0x040050C5 RID: 20677
		public Rectangle bodyFrameMemory;

		// Token: 0x040050C6 RID: 20678
		public bool fullHair;

		// Token: 0x040050C7 RID: 20679
		public bool hatHair;

		// Token: 0x040050C8 RID: 20680
		public bool hideHair;

		// Token: 0x040050C9 RID: 20681
		public bool helmetIsTall;

		// Token: 0x040050CA RID: 20682
		public bool helmetIsOverFullHair;

		// Token: 0x040050CB RID: 20683
		public bool helmetIsNormal;

		// Token: 0x040050CC RID: 20684
		public bool drawUnicornHorn;

		// Token: 0x040050CD RID: 20685
		public bool drawAngelHalo;

		// Token: 0x040050CE RID: 20686
		public Vector2 Position;

		// Token: 0x040050CF RID: 20687
		public Vector2 hairOffset;

		// Token: 0x040050D0 RID: 20688
		public Vector2 helmetOffset;
	}
}
