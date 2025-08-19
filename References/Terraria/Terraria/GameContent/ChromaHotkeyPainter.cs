using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ReLogic.Peripherals.RGB;
using Terraria.GameInput;
using Terraria.Utilities;

namespace Terraria.GameContent
{
	// Token: 0x020001FC RID: 508
	public class ChromaHotkeyPainter
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0050073E File Offset: 0x004FE93E
		public bool PotionAlert
		{
			get
			{
				return this._quickHealAlert != 0;
			}
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0050074C File Offset: 0x004FE94C
		public void CollectBoundKeys()
		{
			foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> keyValuePair in this._keys)
			{
				keyValuePair.Value.Unbind();
			}
			this._keys.Clear();
			foreach (KeyValuePair<string, List<string>> keyValuePair2 in PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus)
			{
				this._keys.Add(keyValuePair2.Key, new ChromaHotkeyPainter.PaintKey(keyValuePair2.Key, keyValuePair2.Value));
			}
			foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> keyValuePair3 in this._keys)
			{
				keyValuePair3.Value.Bind();
			}
			this._wasdKeys = new List<ChromaHotkeyPainter.PaintKey>
			{
				this._keys["Up"],
				this._keys["Down"],
				this._keys["Left"],
				this._keys["Right"]
			};
			this._healKey = this._keys["QuickHeal"];
			this._mountKey = this._keys["QuickMount"];
			this._jumpKey = this._keys["Jump"];
			this._grappleKey = this._keys["Grapple"];
			this._throwKey = this._keys["Throw"];
			this._manaKey = this._keys["QuickMana"];
			this._buffKey = this._keys["QuickBuff"];
			this._smartCursorKey = this._keys["SmartCursor"];
			this._smartSelectKey = this._keys["SmartSelect"];
			this._reactiveKeys.Clear();
			this._xnaKeysInUse.Clear();
			foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> keyValuePair4 in this._keys)
			{
				this._xnaKeysInUse.AddRange(keyValuePair4.Value.GetXNAKeysInUse());
			}
			this._xnaKeysInUse = this._xnaKeysInUse.Distinct<Keys>().ToList<Keys>();
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		[Old("Reactive keys are no longer used so this catch-all method isn't used")]
		public void PressKey(Keys key)
		{
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x00500A10 File Offset: 0x004FEC10
		private ChromaHotkeyPainter.ReactiveRGBKey FindReactiveKey(Keys keyTarget)
		{
			return this._reactiveKeys.FirstOrDefault((ChromaHotkeyPainter.ReactiveRGBKey x) => x.XNAKey == keyTarget);
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x00500A44 File Offset: 0x004FEC44
		public void Update()
		{
			this._player = Main.LocalPlayer;
			if (!Main.hasFocus)
			{
				this.Step_ClearAll();
				return;
			}
			if (this.PotionAlert)
			{
				foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> keyValuePair in this._keys)
				{
					if (keyValuePair.Key != "QuickHeal")
					{
						keyValuePair.Value.SetClear();
					}
				}
				this.Step_QuickHeal();
			}
			else
			{
				this.Step_Movement();
				this.Step_QuickHeal();
			}
			if (Main.InGameUI.CurrentState == Main.ManageControlsMenu)
			{
				this.Step_ClearAll();
				this.Step_KeybindsMenu();
			}
			this.Step_UpdateReactiveKeys();
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x00500B08 File Offset: 0x004FED08
		private void SetGroupColorBase(List<ChromaHotkeyPainter.PaintKey> keys, Color color)
		{
			foreach (ChromaHotkeyPainter.PaintKey paintKey in keys)
			{
				paintKey.SetSolid(color);
			}
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x00500B54 File Offset: 0x004FED54
		private void SetGroupClear(List<ChromaHotkeyPainter.PaintKey> keys)
		{
			foreach (ChromaHotkeyPainter.PaintKey paintKey in keys)
			{
				paintKey.SetClear();
			}
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x00500BA0 File Offset: 0x004FEDA0
		private void Step_KeybindsMenu()
		{
			this.SetGroupColorBase(this._wasdKeys, ChromaHotkeyPainter.PainterColors.MovementKeys);
			this._jumpKey.SetSolid(ChromaHotkeyPainter.PainterColors.MovementKeys);
			this._grappleKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickGrapple);
			this._mountKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickMount);
			this._quickHealAlert = 0;
			this._healKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickHealReady);
			this._manaKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickMana);
			this._throwKey.SetSolid(ChromaHotkeyPainter.PainterColors.Throw);
			this._smartCursorKey.SetSolid(ChromaHotkeyPainter.PainterColors.SmartCursor);
			this._smartSelectKey.SetSolid(ChromaHotkeyPainter.PainterColors.SmartSelect);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x00500C48 File Offset: 0x004FEE48
		private void Step_UpdateReactiveKeys()
		{
			using (List<ChromaHotkeyPainter.ReactiveRGBKey>.Enumerator enumerator = this._reactiveKeys.FindAll((ChromaHotkeyPainter.ReactiveRGBKey x) => x.Expired).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ChromaHotkeyPainter.ReactiveRGBKey key = enumerator.Current;
					key.Clear();
					if (!this._keys.Any((KeyValuePair<string, ChromaHotkeyPainter.PaintKey> x) => x.Value.UsesKey(key.XNAKey)))
					{
						key.Unbind();
					}
				}
			}
			this._reactiveKeys.RemoveAll((ChromaHotkeyPainter.ReactiveRGBKey x) => x.Expired);
			foreach (ChromaHotkeyPainter.ReactiveRGBKey reactiveRGBKey in this._reactiveKeys)
			{
				reactiveRGBKey.Update();
			}
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x00500D5C File Offset: 0x004FEF5C
		private void Step_ClearAll()
		{
			foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> keyValuePair in this._keys)
			{
				keyValuePair.Value.SetClear();
			}
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00500DB4 File Offset: 0x004FEFB4
		private void Step_SmartKeys()
		{
			ChromaHotkeyPainter.PaintKey smartCursorKey = this._smartCursorKey;
			ChromaHotkeyPainter.PaintKey smartSelectKey = this._smartSelectKey;
			if (this._player.DeadOrGhost || this._player.frozen || this._player.tongued || this._player.webbed || this._player.stoned || this._player.noItems)
			{
				smartCursorKey.SetClear();
				smartSelectKey.SetClear();
				return;
			}
			if (Main.SmartCursorWanted)
			{
				smartCursorKey.SetSolid(ChromaHotkeyPainter.PainterColors.SmartCursor);
			}
			else
			{
				smartCursorKey.SetClear();
			}
			if (this._player.nonTorch >= 0)
			{
				smartSelectKey.SetSolid(ChromaHotkeyPainter.PainterColors.SmartSelect);
				return;
			}
			smartSelectKey.SetClear();
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x00500E64 File Offset: 0x004FF064
		private void Step_Movement()
		{
			List<ChromaHotkeyPainter.PaintKey> wasdKeys = this._wasdKeys;
			bool flag = this._player.frozen || this._player.tongued || this._player.webbed || this._player.stoned;
			if (this._player.DeadOrGhost)
			{
				this.SetGroupClear(wasdKeys);
				return;
			}
			if (flag)
			{
				this.SetGroupColorBase(wasdKeys, ChromaHotkeyPainter.PainterColors.DangerKeyBlocked);
				return;
			}
			this.SetGroupColorBase(wasdKeys, ChromaHotkeyPainter.PainterColors.MovementKeys);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x00500EE0 File Offset: 0x004FF0E0
		private void Step_Mount()
		{
			ChromaHotkeyPainter.PaintKey mountKey = this._mountKey;
			if (this._player.QuickMount_GetItemToUse() == null || this._player.DeadOrGhost)
			{
				mountKey.SetClear();
				return;
			}
			if (this._player.frozen || this._player.tongued || this._player.webbed || this._player.stoned || this._player.gravDir == -1f || this._player.noItems)
			{
				mountKey.SetSolid(ChromaHotkeyPainter.PainterColors.DangerKeyBlocked);
				if (this._player.gravDir == -1f)
				{
					mountKey.SetSolid(ChromaHotkeyPainter.PainterColors.DangerKeyBlocked * 0.6f);
				}
				return;
			}
			mountKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickMount);
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x00500FA8 File Offset: 0x004FF1A8
		private void Step_Grapple()
		{
			ChromaHotkeyPainter.PaintKey grappleKey = this._grappleKey;
			if (this._player.QuickGrapple_GetItemToUse() == null || this._player.DeadOrGhost)
			{
				grappleKey.SetClear();
				return;
			}
			if (this._player.frozen || this._player.tongued || this._player.webbed || this._player.stoned || this._player.noItems)
			{
				grappleKey.SetSolid(ChromaHotkeyPainter.PainterColors.DangerKeyBlocked);
				return;
			}
			grappleKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickGrapple);
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x00501038 File Offset: 0x004FF238
		private void Step_Jump()
		{
			ChromaHotkeyPainter.PaintKey jumpKey = this._jumpKey;
			if (this._player.DeadOrGhost)
			{
				jumpKey.SetClear();
				return;
			}
			if (this._player.frozen || this._player.tongued || this._player.webbed || this._player.stoned)
			{
				jumpKey.SetSolid(ChromaHotkeyPainter.PainterColors.DangerKeyBlocked);
				return;
			}
			jumpKey.SetSolid(ChromaHotkeyPainter.PainterColors.MovementKeys);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x005010AC File Offset: 0x004FF2AC
		private void Step_QuickHeal()
		{
			ChromaHotkeyPainter.PaintKey healKey = this._healKey;
			if (this._player.QuickHeal_GetItemToUse() == null || this._player.DeadOrGhost)
			{
				healKey.SetClear();
				this._quickHealAlert = 0;
				return;
			}
			if (this._player.potionDelay > 0)
			{
				float lerpValue = Utils.GetLerpValue((float)this._player.potionDelayTime, 0f, (float)this._player.potionDelay, true);
				Color solid = Color.Lerp(ChromaHotkeyPainter.PainterColors.DangerKeyBlocked, ChromaHotkeyPainter.PainterColors.QuickHealCooldown, lerpValue) * lerpValue * lerpValue * lerpValue;
				healKey.SetSolid(solid);
				this._quickHealAlert = 0;
				return;
			}
			if (this._player.statLife == this._player.statLifeMax2)
			{
				healKey.SetClear();
				this._quickHealAlert = 0;
				return;
			}
			if ((float)this._player.statLife <= (float)this._player.statLifeMax2 / 4f)
			{
				if (this._quickHealAlert != 1)
				{
					this._quickHealAlert = 1;
					healKey.SetAlert(Color.Black, ChromaHotkeyPainter.PainterColors.QuickHealReadyUrgent, -1f, 2f);
				}
				return;
			}
			if ((float)this._player.statLife <= (float)this._player.statLifeMax2 / 2f)
			{
				if (this._quickHealAlert != 2)
				{
					this._quickHealAlert = 2;
					healKey.SetAlert(Color.Black, ChromaHotkeyPainter.PainterColors.QuickHealReadyUrgent, -1f, 2f);
				}
				return;
			}
			healKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickHealReady);
			this._quickHealAlert = 0;
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0050121C File Offset: 0x004FF41C
		private void Step_QuickMana()
		{
			ChromaHotkeyPainter.PaintKey manaKey = this._manaKey;
			if (this._player.QuickMana_GetItemToUse() == null || this._player.DeadOrGhost || this._player.statMana == this._player.statManaMax2)
			{
				manaKey.SetClear();
				return;
			}
			manaKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickMana);
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x00501274 File Offset: 0x004FF474
		private void Step_Throw()
		{
			ChromaHotkeyPainter.PaintKey throwKey = this._throwKey;
			Item heldItem = this._player.HeldItem;
			if (this._player.DeadOrGhost || this._player.HeldItem.favorited || this._player.noThrow > 0)
			{
				throwKey.SetClear();
				return;
			}
			if (this._player.frozen || this._player.tongued || this._player.webbed || this._player.stoned || this._player.noItems)
			{
				throwKey.SetClear();
				return;
			}
			throwKey.SetSolid(ChromaHotkeyPainter.PainterColors.Throw);
		}

		// Token: 0x04004407 RID: 17415
		private readonly Dictionary<string, ChromaHotkeyPainter.PaintKey> _keys = new Dictionary<string, ChromaHotkeyPainter.PaintKey>();

		// Token: 0x04004408 RID: 17416
		private readonly List<ChromaHotkeyPainter.ReactiveRGBKey> _reactiveKeys = new List<ChromaHotkeyPainter.ReactiveRGBKey>();

		// Token: 0x04004409 RID: 17417
		private List<Keys> _xnaKeysInUse = new List<Keys>();

		// Token: 0x0400440A RID: 17418
		private Player _player;

		// Token: 0x0400440B RID: 17419
		private int _quickHealAlert;

		// Token: 0x0400440C RID: 17420
		private List<ChromaHotkeyPainter.PaintKey> _wasdKeys = new List<ChromaHotkeyPainter.PaintKey>();

		// Token: 0x0400440D RID: 17421
		private ChromaHotkeyPainter.PaintKey _healKey;

		// Token: 0x0400440E RID: 17422
		private ChromaHotkeyPainter.PaintKey _mountKey;

		// Token: 0x0400440F RID: 17423
		private ChromaHotkeyPainter.PaintKey _jumpKey;

		// Token: 0x04004410 RID: 17424
		private ChromaHotkeyPainter.PaintKey _grappleKey;

		// Token: 0x04004411 RID: 17425
		private ChromaHotkeyPainter.PaintKey _throwKey;

		// Token: 0x04004412 RID: 17426
		private ChromaHotkeyPainter.PaintKey _manaKey;

		// Token: 0x04004413 RID: 17427
		private ChromaHotkeyPainter.PaintKey _buffKey;

		// Token: 0x04004414 RID: 17428
		private ChromaHotkeyPainter.PaintKey _smartCursorKey;

		// Token: 0x04004415 RID: 17429
		private ChromaHotkeyPainter.PaintKey _smartSelectKey;

		// Token: 0x02000616 RID: 1558
		private class ReactiveRGBKey
		{
			// Token: 0x170003CA RID: 970
			// (get) Token: 0x06003376 RID: 13174 RVA: 0x0060647F File Offset: 0x0060467F
			public bool Expired
			{
				get
				{
					return this._expireTime < Main.gameTimeCache.TotalGameTime;
				}
			}

			// Token: 0x06003377 RID: 13175 RVA: 0x00606496 File Offset: 0x00604696
			public ReactiveRGBKey(Keys key, Color color, TimeSpan duration, string whatIsThisKeyFor)
			{
				this._color = color;
				this.XNAKey = key;
				this.WhatIsThisKeyFor = whatIsThisKeyFor;
				this._duration = duration;
				this._startTime = Main.gameTimeCache.TotalGameTime;
			}

			// Token: 0x06003378 RID: 13176 RVA: 0x006064CC File Offset: 0x006046CC
			public void Update()
			{
				float amount = (float)Utils.GetLerpValue(this._startTime.TotalSeconds, this._expireTime.TotalSeconds, Main.gameTimeCache.TotalGameTime.TotalSeconds, true);
				this._rgbKey.SetSolid(Color.Lerp(this._color, Color.Black, amount));
			}

			// Token: 0x06003379 RID: 13177 RVA: 0x00606525 File Offset: 0x00604725
			public void Clear()
			{
				this._rgbKey.Clear();
			}

			// Token: 0x0600337A RID: 13178 RVA: 0x00606532 File Offset: 0x00604732
			public void Unbind()
			{
				Main.Chroma.UnbindKey(this.XNAKey);
			}

			// Token: 0x0600337B RID: 13179 RVA: 0x00606544 File Offset: 0x00604744
			public void Bind()
			{
				this._rgbKey = Main.Chroma.BindKey(this.XNAKey, this.WhatIsThisKeyFor);
			}

			// Token: 0x0600337C RID: 13180 RVA: 0x00606562 File Offset: 0x00604762
			public void Refresh()
			{
				this._startTime = Main.gameTimeCache.TotalGameTime;
				this._expireTime = this._startTime;
				this._expireTime.Add(this._duration);
			}

			// Token: 0x0400608D RID: 24717
			public readonly Keys XNAKey;

			// Token: 0x0400608E RID: 24718
			public readonly string WhatIsThisKeyFor;

			// Token: 0x0400608F RID: 24719
			private readonly Color _color;

			// Token: 0x04006090 RID: 24720
			private readonly TimeSpan _duration;

			// Token: 0x04006091 RID: 24721
			private TimeSpan _startTime;

			// Token: 0x04006092 RID: 24722
			private TimeSpan _expireTime;

			// Token: 0x04006093 RID: 24723
			private RgbKey _rgbKey;
		}

		// Token: 0x02000617 RID: 1559
		private class PaintKey
		{
			// Token: 0x0600337D RID: 13181 RVA: 0x00606594 File Offset: 0x00604794
			public PaintKey(string triggerName, List<string> keys)
			{
				this._triggerName = triggerName;
				this._xnaKeys = new List<Keys>();
				using (List<string>.Enumerator enumerator = keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Keys item;
						if (Enum.TryParse<Keys>(enumerator.Current, true, out item))
						{
							this._xnaKeys.Add(item);
						}
					}
				}
				this._rgbKeys = new List<RgbKey>();
			}

			// Token: 0x0600337E RID: 13182 RVA: 0x00606614 File Offset: 0x00604814
			public void Unbind()
			{
				foreach (RgbKey rgbKey in this._rgbKeys)
				{
					Main.Chroma.UnbindKey(rgbKey.Key);
				}
			}

			// Token: 0x0600337F RID: 13183 RVA: 0x00606670 File Offset: 0x00604870
			public void Bind()
			{
				foreach (Keys keys in this._xnaKeys)
				{
					this._rgbKeys.Add(Main.Chroma.BindKey(keys, this._triggerName));
				}
				this._rgbKeys = this._rgbKeys.Distinct<RgbKey>().ToList<RgbKey>();
			}

			// Token: 0x06003380 RID: 13184 RVA: 0x006066F0 File Offset: 0x006048F0
			public void SetSolid(Color color)
			{
				foreach (RgbKey rgbKey in this._rgbKeys)
				{
					rgbKey.SetSolid(color);
				}
			}

			// Token: 0x06003381 RID: 13185 RVA: 0x00606744 File Offset: 0x00604944
			public void SetClear()
			{
				foreach (RgbKey rgbKey in this._rgbKeys)
				{
					rgbKey.Clear();
				}
			}

			// Token: 0x06003382 RID: 13186 RVA: 0x00606794 File Offset: 0x00604994
			public bool UsesKey(Keys key)
			{
				return this._xnaKeys.Contains(key);
			}

			// Token: 0x06003383 RID: 13187 RVA: 0x006067A4 File Offset: 0x006049A4
			public void SetAlert(Color colorBase, Color colorFlash, float time, float flashesPerSecond)
			{
				if (time == -1f)
				{
					time = 10000f;
				}
				foreach (RgbKey rgbKey in this._rgbKeys)
				{
					rgbKey.SetFlashing(colorBase, colorFlash, time, flashesPerSecond);
				}
			}

			// Token: 0x06003384 RID: 13188 RVA: 0x00606808 File Offset: 0x00604A08
			public List<Keys> GetXNAKeysInUse()
			{
				return new List<Keys>(this._xnaKeys);
			}

			// Token: 0x04006094 RID: 24724
			private string _triggerName;

			// Token: 0x04006095 RID: 24725
			private List<Keys> _xnaKeys;

			// Token: 0x04006096 RID: 24726
			private List<RgbKey> _rgbKeys;
		}

		// Token: 0x02000618 RID: 1560
		private static class PainterColors
		{
			// Token: 0x04006097 RID: 24727
			private const float HOTKEY_COLOR_MULTIPLIER = 1f;

			// Token: 0x04006098 RID: 24728
			public static readonly Color MovementKeys = Color.Gray * 1f;

			// Token: 0x04006099 RID: 24729
			public static readonly Color QuickMount = Color.RoyalBlue * 1f;

			// Token: 0x0400609A RID: 24730
			public static readonly Color QuickGrapple = Color.Lerp(Color.RoyalBlue, Color.Blue, 0.5f) * 1f;

			// Token: 0x0400609B RID: 24731
			public static readonly Color QuickHealReady = Color.Pink * 1f;

			// Token: 0x0400609C RID: 24732
			public static readonly Color QuickHealReadyUrgent = Color.DeepPink * 1f;

			// Token: 0x0400609D RID: 24733
			public static readonly Color QuickHealCooldown = Color.HotPink * 0.5f * 1f;

			// Token: 0x0400609E RID: 24734
			public static readonly Color QuickMana = new Color(40, 0, 230) * 1f;

			// Token: 0x0400609F RID: 24735
			public static readonly Color Throw = Color.Red * 0.2f * 1f;

			// Token: 0x040060A0 RID: 24736
			public static readonly Color SmartCursor = Color.Gold;

			// Token: 0x040060A1 RID: 24737
			public static readonly Color SmartSelect = Color.Goldenrod;

			// Token: 0x040060A2 RID: 24738
			public static readonly Color DangerKeyBlocked = Color.Red * 1f;
		}
	}
}
