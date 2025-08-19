using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000415 RID: 1045
	public class SettingsForCharacterPreview
	{
		// Token: 0x06002B56 RID: 11094 RVA: 0x0059E0A8 File Offset: 0x0059C2A8
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

		// Token: 0x06002B57 RID: 11095 RVA: 0x0059E116 File Offset: 0x0059C316
		public SettingsForCharacterPreview WhenSelected(int? startFrame = null, int? frameCount = null, int? delayPerFrame = null, bool? bounceLoop = null)
		{
			SettingsForCharacterPreview.Modify(ref this.Selected, startFrame, frameCount, delayPerFrame, bounceLoop);
			return this;
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x0059E129 File Offset: 0x0059C329
		public SettingsForCharacterPreview WhenNotSelected(int? startFrame = null, int? frameCount = null, int? delayPerFrame = null, bool? bounceLoop = null)
		{
			SettingsForCharacterPreview.Modify(ref this.NotSelected, startFrame, frameCount, delayPerFrame, bounceLoop);
			return this;
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x0059E13C File Offset: 0x0059C33C
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

		// Token: 0x06002B5A RID: 11098 RVA: 0x0059E1DC File Offset: 0x0059C3DC
		public SettingsForCharacterPreview WithOffset(Vector2 offset)
		{
			this.Offset = offset;
			return this;
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x0059E1E6 File Offset: 0x0059C3E6
		public SettingsForCharacterPreview WithOffset(float x, float y)
		{
			this.Offset = new Vector2(x, y);
			return this;
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x0059E1F6 File Offset: 0x0059C3F6
		public SettingsForCharacterPreview WithSpriteDirection(int spriteDirection)
		{
			this.SpriteDirection = spriteDirection;
			return this;
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x0059E200 File Offset: 0x0059C400
		public SettingsForCharacterPreview WithCode(SettingsForCharacterPreview.CustomAnimationCode customAnimation)
		{
			this.CustomAnimation = customAnimation;
			return this;
		}

		// Token: 0x04004F6F RID: 20335
		public Vector2 Offset;

		// Token: 0x04004F70 RID: 20336
		public SettingsForCharacterPreview.SelectionBasedSettings Selected;

		// Token: 0x04004F71 RID: 20337
		public SettingsForCharacterPreview.SelectionBasedSettings NotSelected;

		// Token: 0x04004F72 RID: 20338
		public int SpriteDirection = 1;

		// Token: 0x04004F73 RID: 20339
		public SettingsForCharacterPreview.CustomAnimationCode CustomAnimation;

		// Token: 0x02000778 RID: 1912
		// (Invoke) Token: 0x06003913 RID: 14611
		public delegate void CustomAnimationCode(Projectile proj, bool walking);

		// Token: 0x02000779 RID: 1913
		public struct SelectionBasedSettings
		{
			// Token: 0x06003916 RID: 14614 RVA: 0x006152D0 File Offset: 0x006134D0
			public void ApplyTo(Projectile proj)
			{
				if (this.FrameCount == 0)
				{
					return;
				}
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

			// Token: 0x04006493 RID: 25747
			public int StartFrame;

			// Token: 0x04006494 RID: 25748
			public int FrameCount;

			// Token: 0x04006495 RID: 25749
			public int DelayPerFrame;

			// Token: 0x04006496 RID: 25750
			public bool BounceLoop;
		}
	}
}
