using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI
{
	// Token: 0x020004CB RID: 1227
	public class GameTipsDisplay
	{
		// Token: 0x06003AAE RID: 15022 RVA: 0x005AB824 File Offset: 0x005A9A24
		public GameTipsDisplay()
		{
			this._tipsDefault = Language.FindAll(Lang.CreateDialogFilter("LoadingTips_Default."));
			this._tipsGamepad = Language.FindAll(Lang.CreateDialogFilter("LoadingTips_GamePad."));
			this._tipsKeyboard = Language.FindAll(Lang.CreateDialogFilter("LoadingTips_Keyboard."));
			this._lastTip = null;
			this.Initialize();
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x005AB890 File Offset: 0x005A9A90
		public void Update()
		{
			double time = Main.gameTimeCache.TotalGameTime.TotalSeconds;
			this._currentTips.RemoveAll((GameTipsDisplay.GameTip x) => x.IsExpired(time));
			bool flag = true;
			using (List<GameTipsDisplay.GameTip>.Enumerator enumerator = this._currentTips.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsExpiring(time))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				this.AddNewTip(time);
			}
			foreach (GameTipsDisplay.GameTip gameTip in this._currentTips)
			{
				gameTip.Update(time);
			}
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x005AB978 File Offset: 0x005A9B78
		public void ClearTips()
		{
			this._currentTips.Clear();
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x005AB988 File Offset: 0x005A9B88
		public void Draw()
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			float num = (float)Main.screenWidth;
			float y = (float)(Main.screenHeight - 150);
			float num2 = (float)Main.screenWidth * 0.5f;
			foreach (GameTipsDisplay.GameTip currentTip in this._currentTips)
			{
				if (currentTip.ScreenAnchorX >= -0.5f && currentTip.ScreenAnchorX <= 1.5f)
				{
					DynamicSpriteFont value = FontAssets.MouseText.Value;
					string text = value.CreateWrappedText(currentTip.Text, num2, Language.ActiveCulture.CultureInfo);
					if (text.Split('\n', StringSplitOptions.None).Length > 2)
					{
						text = value.CreateWrappedText(currentTip.Text, num2 * 1.5f - 50f, Language.ActiveCulture.CultureInfo);
					}
					if (WorldGen.getGoodWorldGen)
					{
						string text2 = "";
						for (int num3 = text.Length - 1; num3 >= 0; num3--)
						{
							text2 += text.Substring(num3, 1);
						}
						text = text2;
					}
					else if (WorldGen.drunkWorldGenText)
					{
						text = string.Concat(Main.rand.Next(999999999));
						for (int i = 0; i < 14; i++)
						{
							if (Main.rand.Next(2) == 0)
							{
								text += Main.rand.Next(999999999).ToString();
							}
						}
					}
					Vector2 vector = value.MeasureString(text);
					float num4 = 1.1f;
					float num5 = 110f;
					if (vector.Y > num5)
					{
						num4 = num5 / vector.Y;
					}
					Vector2 position;
					position..ctor(num * currentTip.ScreenAnchorX, y);
					position -= vector * num4 * 0.5f;
					if (WorldGen.tenthAnniversaryWorldGen && !WorldGen.remixWorldGen)
					{
						ChatManager.DrawColorCodedStringWithShadow(spriteBatch, value, text, position, Color.HotPink, 0f, Vector2.Zero, new Vector2(num4, num4), -1f, 2f);
					}
					else
					{
						ChatManager.DrawColorCodedStringWithShadow(spriteBatch, value, text, position, Color.White, 0f, Vector2.Zero, new Vector2(num4, num4), -1f, 2f);
					}
				}
			}
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x005ABBFC File Offset: 0x005A9DFC
		private void AddNewTip(double currentTime)
		{
			string textKey = "UI.Back";
			List<GameTipData> list = new List<GameTipData>(this.allTips);
			if (PlayerInput.UsingGamepad)
			{
				list.RemoveAll((GameTipData tip) => tip.Mod == null && tip.TipText.Key.StartsWith("LoadingTips_Keyboard"));
			}
			else
			{
				list.RemoveAll((GameTipData tip) => tip.Mod == null && tip.TipText.Key.StartsWith("LoadingTips_GamePad"));
			}
			if (this._lastTip != null)
			{
				list.RemoveAll((GameTipData tip) => tip.TipText == this._lastTip);
			}
			list.RemoveAll((GameTipData tip) => !tip.isVisible);
			string key = (this._lastTip = ((list.Count != 0) ? list[Main.rand.Next(list.Count)].TipText : LocalizedText.Empty)).Key;
			if (Language.Exists(key))
			{
				textKey = key;
			}
			this._currentTips.Add(new GameTipsDisplay.GameTip(textKey, currentTime));
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x005ABD08 File Offset: 0x005A9F08
		internal void Initialize()
		{
			this.allTips = (from localizedText in this._tipsDefault.Concat(this._tipsKeyboard).Concat(this._tipsGamepad)
			select new GameTipData(localizedText)).ToList<GameTipData>();
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x005ABD60 File Offset: 0x005A9F60
		internal void Reset()
		{
			this.ClearTips();
			this.allTips = (from tip in this.allTips
			where tip.Mod == null
			select tip).ToList<GameTipData>();
			this.allTips.ForEach(delegate(GameTipData tip)
			{
				tip.isVisible = true;
			});
			this._lastTip = null;
		}

		// Token: 0x040054B6 RID: 21686
		private LocalizedText[] _tipsDefault;

		// Token: 0x040054B7 RID: 21687
		private LocalizedText[] _tipsGamepad;

		// Token: 0x040054B8 RID: 21688
		private LocalizedText[] _tipsKeyboard;

		// Token: 0x040054B9 RID: 21689
		private readonly List<GameTipsDisplay.GameTip> _currentTips = new List<GameTipsDisplay.GameTip>();

		// Token: 0x040054BA RID: 21690
		private LocalizedText _lastTip;

		// Token: 0x040054BB RID: 21691
		internal List<GameTipData> allTips;

		// Token: 0x02000BD0 RID: 3024
		private class GameTip
		{
			// Token: 0x17000953 RID: 2387
			// (get) Token: 0x06005DDF RID: 24031 RVA: 0x006C99AB File Offset: 0x006C7BAB
			public string Text
			{
				get
				{
					if (this._textKey == null)
					{
						return "What?!";
					}
					return this._formattedText;
				}
			}

			// Token: 0x06005DE0 RID: 24032 RVA: 0x006C99C1 File Offset: 0x006C7BC1
			public bool IsExpired(double currentTime)
			{
				return currentTime >= this.SpawnTime + (double)this.Duration;
			}

			// Token: 0x06005DE1 RID: 24033 RVA: 0x006C99D7 File Offset: 0x006C7BD7
			public bool IsExpiring(double currentTime)
			{
				return currentTime >= this.SpawnTime + (double)this.Duration - 1.0;
			}

			// Token: 0x06005DE2 RID: 24034 RVA: 0x006C99F8 File Offset: 0x006C7BF8
			public GameTip(string textKey, double spawnTime)
			{
				this._textKey = Language.GetText(textKey);
				this.SpawnTime = spawnTime;
				this.ScreenAnchorX = 2.5f;
				this.Duration = 11.5f;
				object obj = Lang.CreateDialogSubstitutionObject(null);
				this._formattedText = this._textKey.FormatWith(obj);
				this._formattedText = Lang.SupportGlyphs(this._formattedText);
			}

			// Token: 0x06005DE3 RID: 24035 RVA: 0x006C9A60 File Offset: 0x006C7C60
			public void Update(double currentTime)
			{
				double num = currentTime - this.SpawnTime;
				if (num < 0.5)
				{
					this.ScreenAnchorX = MathHelper.SmoothStep(2.5f, 0.5f, (float)Utils.GetLerpValue(0.0, 0.5, num, true));
					return;
				}
				if (num >= (double)(this.Duration - 1f))
				{
					this.ScreenAnchorX = MathHelper.SmoothStep(0.5f, -1.5f, (float)Utils.GetLerpValue((double)(this.Duration - 1f), (double)this.Duration, num, true));
					return;
				}
				this.ScreenAnchorX = 0.5f;
			}

			// Token: 0x04007735 RID: 30517
			private const float APPEAR_FROM = 2.5f;

			// Token: 0x04007736 RID: 30518
			private const float APPEAR_TO = 0.5f;

			// Token: 0x04007737 RID: 30519
			private const float DISAPPEAR_TO = -1.5f;

			// Token: 0x04007738 RID: 30520
			private const float APPEAR_TIME = 0.5f;

			// Token: 0x04007739 RID: 30521
			private const float DISAPPEAR_TIME = 1f;

			// Token: 0x0400773A RID: 30522
			private const float DURATION = 11.5f;

			// Token: 0x0400773B RID: 30523
			private LocalizedText _textKey;

			// Token: 0x0400773C RID: 30524
			private string _formattedText;

			// Token: 0x0400773D RID: 30525
			public float ScreenAnchorX;

			// Token: 0x0400773E RID: 30526
			public readonly float Duration;

			// Token: 0x0400773F RID: 30527
			public readonly double SpawnTime;
		}
	}
}
