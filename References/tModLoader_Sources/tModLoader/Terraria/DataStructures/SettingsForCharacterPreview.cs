using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000730 RID: 1840
	public class SettingsForCharacterPreview
	{
		// Token: 0x06004AC1 RID: 19137 RVA: 0x00668318 File Offset: 0x00666518
		public void ApplyTo(Projectile proj, bool walking)
		{
			proj.position += this.Offset;
			proj.spriteDirection = this.SpriteDirection;
			proj.direction = this.SpriteDirection;
			if (walking)
			{
				this.Selected.ApplyTo(proj);
			}
			else
			{
				this.NotSelected.ApplyTo(proj);
			}
			if (this.CustomAnimation != null)
			{
				this.CustomAnimation(proj, walking);
			}
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x00668386 File Offset: 0x00666586
		public SettingsForCharacterPreview WhenSelected(int? startFrame = null, int? frameCount = null, int? delayPerFrame = null, bool? bounceLoop = null)
		{
			SettingsForCharacterPreview.Modify(ref this.Selected, startFrame, frameCount, delayPerFrame, bounceLoop);
			return this;
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x00668399 File Offset: 0x00666599
		public SettingsForCharacterPreview WhenNotSelected(int? startFrame = null, int? frameCount = null, int? delayPerFrame = null, bool? bounceLoop = null)
		{
			SettingsForCharacterPreview.Modify(ref this.NotSelected, startFrame, frameCount, delayPerFrame, bounceLoop);
			return this;
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x006683AC File Offset: 0x006665AC
		private static void Modify(ref SettingsForCharacterPreview.SelectionBasedSettings target, int? startFrame, int? frameCount, int? delayPerFrame, bool? bounceLoop)
		{
			if (frameCount != null && frameCount.Value < 1)
			{
				frameCount = new int?(1);
			}
			target.StartFrame = ((startFrame != null) ? startFrame.Value : target.StartFrame);
			target.FrameCount = ((frameCount != null) ? frameCount.Value : target.FrameCount);
			target.DelayPerFrame = ((delayPerFrame != null) ? delayPerFrame.Value : target.DelayPerFrame);
			target.BounceLoop = ((bounceLoop != null) ? bounceLoop.Value : target.BounceLoop);
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x0066844C File Offset: 0x0066664C
		public SettingsForCharacterPreview WithOffset(Vector2 offset)
		{
			this.Offset = offset;
			return this;
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00668456 File Offset: 0x00666656
		public SettingsForCharacterPreview WithOffset(float x, float y)
		{
			this.Offset = new Vector2(x, y);
			return this;
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x00668466 File Offset: 0x00666666
		public SettingsForCharacterPreview WithSpriteDirection(int spriteDirection)
		{
			this.SpriteDirection = spriteDirection;
			return this;
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x00668470 File Offset: 0x00666670
		public SettingsForCharacterPreview WithCode(SettingsForCharacterPreview.CustomAnimationCode customAnimation)
		{
			this.CustomAnimation = customAnimation;
			return this;
		}

		// Token: 0x04006000 RID: 24576
		public Vector2 Offset;

		// Token: 0x04006001 RID: 24577
		public SettingsForCharacterPreview.SelectionBasedSettings Selected;

		// Token: 0x04006002 RID: 24578
		public SettingsForCharacterPreview.SelectionBasedSettings NotSelected;

		// Token: 0x04006003 RID: 24579
		public int SpriteDirection = 1;

		// Token: 0x04006004 RID: 24580
		public SettingsForCharacterPreview.CustomAnimationCode CustomAnimation;

		// Token: 0x02000D60 RID: 3424
		// (Invoke) Token: 0x060063F7 RID: 25591
		public delegate void CustomAnimationCode(Projectile proj, bool walking);

		// Token: 0x02000D61 RID: 3425
		public struct SelectionBasedSettings
		{
			// Token: 0x060063FA RID: 25594 RVA: 0x006D9B5C File Offset: 0x006D7D5C
			public void ApplyTo(Projectile proj)
			{
				if (this.FrameCount != 0)
				{
					if (proj.frame < this.StartFrame)
					{
						proj.frame = this.StartFrame;
					}
					int num = proj.frame - this.StartFrame;
					int num2 = this.FrameCount * this.DelayPerFrame;
					int num3 = num2;
					if (this.BounceLoop)
					{
						num3 = num2 * 2 - this.DelayPerFrame * 2;
					}
					int num4 = proj.frameCounter + 1;
					proj.frameCounter = num4;
					if (num4 >= num3)
					{
						proj.frameCounter = 0;
					}
					num = proj.frameCounter / this.DelayPerFrame;
					if (this.BounceLoop && num >= this.FrameCount)
					{
						num = this.FrameCount * 2 - num - 2;
					}
					proj.frame = this.StartFrame + num;
				}
			}

			// Token: 0x04007B8F RID: 31631
			public int StartFrame;

			// Token: 0x04007B90 RID: 31632
			public int FrameCount;

			// Token: 0x04007B91 RID: 31633
			public int DelayPerFrame;

			// Token: 0x04007B92 RID: 31634
			public bool BounceLoop;
		}
	}
}
