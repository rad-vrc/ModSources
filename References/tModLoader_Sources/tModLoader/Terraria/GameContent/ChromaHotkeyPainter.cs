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
	// Token: 0x02000490 RID: 1168
	public class ChromaHotkeyPainter
	{
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x00594C46 File Offset: 0x00592E46
		public bool PotionAlert
		{
			get
			{
				return this._quickHealAlert != 0;
			}
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x00594C54 File Offset: 0x00592E54
		public void CollectBoundKeys()
		{
			foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> key in this._keys)
			{
				key.Value.Unbind();
			}
			this._keys.Clear();
			foreach (KeyValuePair<string, List<string>> item in PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus)
			{
				this._keys.Add(item.Key, new ChromaHotkeyPainter.PaintKey(item.Key, item.Value));
			}
			foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> key2 in this._keys)
			{
				key2.Value.Bind();
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
			foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> key3 in this._keys)
			{
				this._xnaKeysInUse.AddRange(key3.Value.GetXNAKeysInUse());
			}
			this._xnaKeysInUse = this._xnaKeysInUse.Distinct<Keys>().ToList<Keys>();
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x00594F18 File Offset: 0x00593118
		[Old("Reactive keys are no longer used so this catch-all method isn't used")]
		public void PressKey(Keys key)
		{
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x00594F1C File Offset: 0x0059311C
		private ChromaHotkeyPainter.ReactiveRGBKey FindReactiveKey(Keys keyTarget)
		{
			return this._reactiveKeys.FirstOrDefault((ChromaHotkeyPainter.ReactiveRGBKey x) => x.XNAKey == keyTarget);
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x00594F50 File Offset: 0x00593150
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
				foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> key in this._keys)
				{
					if (key.Key != "QuickHeal")
					{
						key.Value.SetClear();
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

		// Token: 0x060038E5 RID: 14565 RVA: 0x00595014 File Offset: 0x00593214
		private void SetGroupColorBase(List<ChromaHotkeyPainter.PaintKey> keys, Color color)
		{
			foreach (ChromaHotkeyPainter.PaintKey paintKey in keys)
			{
				paintKey.SetSolid(color);
			}
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x00595060 File Offset: 0x00593260
		private void SetGroupClear(List<ChromaHotkeyPainter.PaintKey> keys)
		{
			foreach (ChromaHotkeyPainter.PaintKey paintKey in keys)
			{
				paintKey.SetClear();
			}
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x005950AC File Offset: 0x005932AC
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

		// Token: 0x060038E8 RID: 14568 RVA: 0x00595154 File Offset: 0x00593354
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

		// Token: 0x060038E9 RID: 14569 RVA: 0x00595268 File Offset: 0x00593468
		private void Step_ClearAll()
		{
			foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> key in this._keys)
			{
				key.Value.SetClear();
			}
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x005952C0 File Offset: 0x005934C0
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

		// Token: 0x060038EB RID: 14571 RVA: 0x00595370 File Offset: 0x00593570
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

		// Token: 0x060038EC RID: 14572 RVA: 0x005953EC File Offset: 0x005935EC
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
					return;
				}
			}
			else
			{
				mountKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickMount);
			}
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x005954B4 File Offset: 0x005936B4
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

		// Token: 0x060038EE RID: 14574 RVA: 0x00595544 File Offset: 0x00593744
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

		// Token: 0x060038EF RID: 14575 RVA: 0x005955B8 File Offset: 0x005937B8
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
					return;
				}
			}
			else if ((float)this._player.statLife <= (float)this._player.statLifeMax2 / 2f)
			{
				if (this._quickHealAlert != 2)
				{
					this._quickHealAlert = 2;
					healKey.SetAlert(Color.Black, ChromaHotkeyPainter.PainterColors.QuickHealReadyUrgent, -1f, 2f);
					return;
				}
			}
			else
			{
				healKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickHealReady);
				this._quickHealAlert = 0;
			}
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x00595728 File Offset: 0x00593928
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

		// Token: 0x060038F1 RID: 14577 RVA: 0x00595780 File Offset: 0x00593980
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

		// Token: 0x04005216 RID: 21014
		private readonly Dictionary<string, ChromaHotkeyPainter.PaintKey> _keys = new Dictionary<string, ChromaHotkeyPainter.PaintKey>();

		// Token: 0x04005217 RID: 21015
		private readonly List<ChromaHotkeyPainter.ReactiveRGBKey> _reactiveKeys = new List<ChromaHotkeyPainter.ReactiveRGBKey>();

		// Token: 0x04005218 RID: 21016
		private List<Keys> _xnaKeysInUse = new List<Keys>();

		// Token: 0x04005219 RID: 21017
		private Player _player;

		// Token: 0x0400521A RID: 21018
		private int _quickHealAlert;

		// Token: 0x0400521B RID: 21019
		private List<ChromaHotkeyPainter.PaintKey> _wasdKeys = new List<ChromaHotkeyPainter.PaintKey>();

		// Token: 0x0400521C RID: 21020
		private ChromaHotkeyPainter.PaintKey _healKey;

		// Token: 0x0400521D RID: 21021
		private ChromaHotkeyPainter.PaintKey _mountKey;

		// Token: 0x0400521E RID: 21022
		private ChromaHotkeyPainter.PaintKey _jumpKey;

		// Token: 0x0400521F RID: 21023
		private ChromaHotkeyPainter.PaintKey _grappleKey;

		// Token: 0x04005220 RID: 21024
		private ChromaHotkeyPainter.PaintKey _throwKey;

		// Token: 0x04005221 RID: 21025
		private ChromaHotkeyPainter.PaintKey _manaKey;

		// Token: 0x04005222 RID: 21026
		private ChromaHotkeyPainter.PaintKey _buffKey;

		// Token: 0x04005223 RID: 21027
		private ChromaHotkeyPainter.PaintKey _smartCursorKey;

		// Token: 0x04005224 RID: 21028
		private ChromaHotkeyPainter.PaintKey _smartSelectKey;

		// Token: 0x02000B95 RID: 2965
		private class ReactiveRGBKey
		{
			// Token: 0x1700094F RID: 2383
			// (get) Token: 0x06005D43 RID: 23875 RVA: 0x006C791D File Offset: 0x006C5B1D
			public bool Expired
			{
				get
				{
					return this._expireTime < Main.gameTimeCache.TotalGameTime;
				}
			}

			// Token: 0x06005D44 RID: 23876 RVA: 0x006C7934 File Offset: 0x006C5B34
			public ReactiveRGBKey(Keys key, Color color, TimeSpan duration, string whatIsThisKeyFor)
			{
				this._color = color;
				this.XNAKey = key;
				this.WhatIsThisKeyFor = whatIsThisKeyFor;
				this._duration = duration;
				this._startTime = Main.gameTimeCache.TotalGameTime;
			}

			// Token: 0x06005D45 RID: 23877 RVA: 0x006C796C File Offset: 0x006C5B6C
			public void Update()
			{
				float amount = (float)Utils.GetLerpValue(this._startTime.TotalSeconds, this._expireTime.TotalSeconds, Main.gameTimeCache.TotalGameTime.TotalSeconds, true);
				this._rgbKey.SetSolid(Color.Lerp(this._color, Color.Black, amount));
			}

			// Token: 0x06005D46 RID: 23878 RVA: 0x006C79C5 File Offset: 0x006C5BC5
			public void Clear()
			{
				this._rgbKey.Clear();
			}

			// Token: 0x06005D47 RID: 23879 RVA: 0x006C79D2 File Offset: 0x006C5BD2
			public void Unbind()
			{
				Main.Chroma.UnbindKey(this.XNAKey);
			}

			// Token: 0x06005D48 RID: 23880 RVA: 0x006C79E4 File Offset: 0x006C5BE4
			public void Bind()
			{
				this._rgbKey = Main.Chroma.BindKey(this.XNAKey, this.WhatIsThisKeyFor);
			}

			// Token: 0x06005D49 RID: 23881 RVA: 0x006C7A02 File Offset: 0x006C5C02
			public void Refresh()
			{
				this._startTime = Main.gameTimeCache.TotalGameTime;
				this._expireTime = this._startTime;
				this._expireTime.Add(this._duration);
			}

			// Token: 0x0400766E RID: 30318
			public readonly Keys XNAKey;

			// Token: 0x0400766F RID: 30319
			public readonly string WhatIsThisKeyFor;

			// Token: 0x04007670 RID: 30320
			private readonly Color _color;

			// Token: 0x04007671 RID: 30321
			private readonly TimeSpan _duration;

			// Token: 0x04007672 RID: 30322
			private TimeSpan _startTime;

			// Token: 0x04007673 RID: 30323
			private TimeSpan _expireTime;

			// Token: 0x04007674 RID: 30324
			private RgbKey _rgbKey;
		}

		// Token: 0x02000B96 RID: 2966
		private class PaintKey
		{
			// Token: 0x06005D4A RID: 23882 RVA: 0x006C7A34 File Offset: 0x006C5C34
			public PaintKey(string triggerName, List<string> keys)
			{
				this._triggerName = triggerName;
				this._xnaKeys = new List<Keys>();
				using (List<string>.Enumerator enumerator = keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Keys result;
						if (Enum.TryParse<Keys>(enumerator.Current, true, out result))
						{
							this._xnaKeys.Add(result);
						}
					}
				}
				this._rgbKeys = new List<RgbKey>();
			}

			// Token: 0x06005D4B RID: 23883 RVA: 0x006C7AB4 File Offset: 0x006C5CB4
			public void Unbind()
			{
				foreach (RgbKey rgbKey in this._rgbKeys)
				{
					Main.Chroma.UnbindKey(rgbKey.Key);
				}
			}

			// Token: 0x06005D4C RID: 23884 RVA: 0x006C7B10 File Offset: 0x006C5D10
			public void Bind()
			{
				foreach (Keys xnaKey in this._xnaKeys)
				{
					this._rgbKeys.Add(Main.Chroma.BindKey(xnaKey, this._triggerName));
				}
				this._rgbKeys = this._rgbKeys.Distinct<RgbKey>().ToList<RgbKey>();
			}

			// Token: 0x06005D4D RID: 23885 RVA: 0x006C7B90 File Offset: 0x006C5D90
			public void SetSolid(Color color)
			{
				foreach (RgbKey rgbKey in this._rgbKeys)
				{
					rgbKey.SetSolid(color);
				}
			}

			// Token: 0x06005D4E RID: 23886 RVA: 0x006C7BE4 File Offset: 0x006C5DE4
			public void SetClear()
			{
				foreach (RgbKey rgbKey in this._rgbKeys)
				{
					rgbKey.Clear();
				}
			}

			// Token: 0x06005D4F RID: 23887 RVA: 0x006C7C34 File Offset: 0x006C5E34
			public bool UsesKey(Keys key)
			{
				return this._xnaKeys.Contains(key);
			}

			// Token: 0x06005D50 RID: 23888 RVA: 0x006C7C44 File Offset: 0x006C5E44
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

			// Token: 0x06005D51 RID: 23889 RVA: 0x006C7CA8 File Offset: 0x006C5EA8
			public List<Keys> GetXNAKeysInUse()
			{
				return new List<Keys>(this._xnaKeys);
			}

			// Token: 0x04007675 RID: 30325
			private string _triggerName;

			// Token: 0x04007676 RID: 30326
			private List<Keys> _xnaKeys;

			// Token: 0x04007677 RID: 30327
			private List<RgbKey> _rgbKeys;
		}

		// Token: 0x02000B97 RID: 2967
		private static class PainterColors
		{
			// Token: 0x04007678 RID: 30328
			private const float HOTKEY_COLOR_MULTIPLIER = 1f;

			// Token: 0x04007679 RID: 30329
			public static readonly Color MovementKeys = Color.Gray * 1f;

			// Token: 0x0400767A RID: 30330
			public static readonly Color QuickMount = Color.RoyalBlue * 1f;

			// Token: 0x0400767B RID: 30331
			public static readonly Color QuickGrapple = Color.Lerp(Color.RoyalBlue, Color.Blue, 0.5f) * 1f;

			// Token: 0x0400767C RID: 30332
			public static readonly Color QuickHealReady = Color.Pink * 1f;

			// Token: 0x0400767D RID: 30333
			public static readonly Color QuickHealReadyUrgent = Color.DeepPink * 1f;

			// Token: 0x0400767E RID: 30334
			public static readonly Color QuickHealCooldown = Color.HotPink * 0.5f * 1f;

			// Token: 0x0400767F RID: 30335
			public static readonly Color QuickMana = new Color(40, 0, 230) * 1f;

			// Token: 0x04007680 RID: 30336
			public static readonly Color Throw = Color.Red * 0.2f * 1f;

			// Token: 0x04007681 RID: 30337
			public static readonly Color SmartCursor = Color.Gold;

			// Token: 0x04007682 RID: 30338
			public static readonly Color SmartSelect = Color.Goldenrod;

			// Token: 0x04007683 RID: 30339
			public static readonly Color DangerKeyBlocked = Color.Red * 1f;
		}
	}
}
