using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.Audio;
using Terraria.Social;
using Terraria.Social.Base;

namespace Terraria.GameContent.UI
{
	// Token: 0x020004D2 RID: 1234
	public class WorkshopPublishingIndicator
	{
		// Token: 0x06003ACC RID: 15052 RVA: 0x005AD044 File Offset: 0x005AB244
		public void Hide()
		{
			this._displayUpPercent = 0f;
			this._frameCounter = 0;
			this._timesSoundWasPlayed = 0;
			this._shouldPlayEndingSound = false;
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x005AD068 File Offset: 0x005AB268
		public void Draw(SpriteBatch spriteBatch)
		{
			WorkshopSocialModule workshop = SocialAPI.Workshop;
			if (workshop == null)
			{
				return;
			}
			AWorkshopProgressReporter progressReporter = workshop.ProgressReporter;
			bool hasOngoingTasks = progressReporter.HasOngoingTasks;
			bool flag2 = this._displayUpPercent == 1f;
			this._displayUpPercent = MathHelper.Clamp(this._displayUpPercent + (float)hasOngoingTasks.ToDirectionInt() / 60f, 0f, 1f);
			bool flag = this._displayUpPercent == 1f;
			if (flag2 && !flag)
			{
				this._shouldPlayEndingSound = true;
			}
			if (this._displayUpPercent == 0f)
			{
				return;
			}
			if (this._indicatorTexture == null)
			{
				this._indicatorTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/InProgress");
			}
			Texture2D value = this._indicatorTexture.Value;
			int num2 = 6;
			this._frameCounter++;
			int num3 = 5;
			int num4 = this._frameCounter / num3 % num2;
			Vector2 vector = Main.ScreenSize.ToVector2() + new Vector2(-40f, 40f);
			Vector2 value2 = vector + new Vector2(0f, -80f);
			Vector2 position = Vector2.Lerp(vector, value2, this._displayUpPercent);
			Rectangle rectangle = value.Frame(1, 6, 0, num4, 0, 0);
			Vector2 origin = rectangle.Size() / 2f;
			spriteBatch.Draw(value, position, new Rectangle?(rectangle), Color.White, 0f, origin, 1f, 0, 0f);
			float progress;
			if (progressReporter.TryGetProgress(out progress) && !float.IsNaN(progress))
			{
				string text = progress.ToString("P");
				DynamicSpriteFont value3 = FontAssets.ItemStack.Value;
				int num5 = 1;
				Vector2 origin2 = value3.MeasureString(text) * (float)num5 * new Vector2(0.5f, 1f);
				Utils.DrawBorderStringFourWay(spriteBatch, value3, text, position.X, position.Y - 10f, Color.White, Color.Black, origin2, (float)num5);
			}
			if (num4 == 3 && this._frameCounter % num3 == 0)
			{
				if (this._shouldPlayEndingSound)
				{
					this._shouldPlayEndingSound = false;
					this._timesSoundWasPlayed = 0;
					SoundEngine.PlaySound(64, -1, -1, 1, 1f, 0f);
				}
				if (hasOngoingTasks)
				{
					float volumeScale = Utils.Remap((float)this._timesSoundWasPlayed, 0f, 10f, 1f, 0f, true);
					SoundEngine.PlaySound(21, -1, -1, 1, volumeScale, 0f);
					this._timesSoundWasPlayed++;
				}
			}
		}

		// Token: 0x040054C4 RID: 21700
		private float _displayUpPercent;

		// Token: 0x040054C5 RID: 21701
		private int _frameCounter;

		// Token: 0x040054C6 RID: 21702
		private bool _shouldPlayEndingSound;

		// Token: 0x040054C7 RID: 21703
		private Asset<Texture2D> _indicatorTexture;

		// Token: 0x040054C8 RID: 21704
		private int _timesSoundWasPlayed;
	}
}
