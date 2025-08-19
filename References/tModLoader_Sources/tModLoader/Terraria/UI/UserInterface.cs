using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameInput;

namespace Terraria.UI
{
	// Token: 0x020000B9 RID: 185
	public class UserInterface
	{
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x004B22A5 File Offset: 0x004B04A5
		public UIState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x004B22AD File Offset: 0x004B04AD
		public void ClearPointers()
		{
			this.LeftMouse.Clear();
			this.RightMouse.Clear();
			this.MiddleMouse.Clear();
			this.XButton1Mouse.Clear();
			this.XButton2Mouse.Clear();
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x004B22E6 File Offset: 0x004B04E6
		public void ResetLasts()
		{
			if (this._lastElementHover != null)
			{
				this._lastElementHover.MouseOut(new UIMouseEvent(this._lastElementHover, this.MousePosition));
			}
			this.ClearPointers();
			this._lastElementHover = null;
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x004B2319 File Offset: 0x004B0519
		public void EscapeElements()
		{
			this.ResetLasts();
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x004B2324 File Offset: 0x004B0524
		public UserInterface()
		{
			UserInterface.InputPointerCache inputPointerCache = new UserInterface.InputPointerCache();
			inputPointerCache.MouseDownEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.LeftMouseDown(evt);
			};
			inputPointerCache.MouseUpEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.LeftMouseUp(evt);
			};
			inputPointerCache.ClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.LeftClick(evt);
			};
			inputPointerCache.DoubleClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.LeftDoubleClick(evt);
			};
			this.LeftMouse = inputPointerCache;
			UserInterface.InputPointerCache inputPointerCache2 = new UserInterface.InputPointerCache();
			inputPointerCache2.MouseDownEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.RightMouseDown(evt);
			};
			inputPointerCache2.MouseUpEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.RightMouseUp(evt);
			};
			inputPointerCache2.ClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.RightClick(evt);
			};
			inputPointerCache2.DoubleClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.RightDoubleClick(evt);
			};
			this.RightMouse = inputPointerCache2;
			UserInterface.InputPointerCache inputPointerCache3 = new UserInterface.InputPointerCache();
			inputPointerCache3.MouseDownEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.MiddleMouseDown(evt);
			};
			inputPointerCache3.MouseUpEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.MiddleMouseUp(evt);
			};
			inputPointerCache3.ClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.MiddleClick(evt);
			};
			inputPointerCache3.DoubleClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.MiddleDoubleClick(evt);
			};
			this.MiddleMouse = inputPointerCache3;
			UserInterface.InputPointerCache inputPointerCache4 = new UserInterface.InputPointerCache();
			inputPointerCache4.MouseDownEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.XButton1MouseDown(evt);
			};
			inputPointerCache4.MouseUpEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.XButton1MouseUp(evt);
			};
			inputPointerCache4.ClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.XButton1Click(evt);
			};
			inputPointerCache4.DoubleClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.XButton1DoubleClick(evt);
			};
			this.XButton1Mouse = inputPointerCache4;
			UserInterface.InputPointerCache inputPointerCache5 = new UserInterface.InputPointerCache();
			inputPointerCache5.MouseDownEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.XButton2MouseDown(evt);
			};
			inputPointerCache5.MouseUpEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.XButton2MouseUp(evt);
			};
			inputPointerCache5.ClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.XButton2Click(evt);
			};
			inputPointerCache5.DoubleClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.XButton2DoubleClick(evt);
			};
			this.XButton2Mouse = inputPointerCache5;
			base..ctor();
			UserInterface.ActiveInstance = this;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x004B2663 File Offset: 0x004B0863
		public void Use()
		{
			if (UserInterface.ActiveInstance != this)
			{
				UserInterface.ActiveInstance = this;
				this.Recalculate();
				return;
			}
			UserInterface.ActiveInstance = this;
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x004B2680 File Offset: 0x004B0880
		private void ImmediatelyUpdateInputPointers()
		{
			this.LeftMouse.WasDown = Main.mouseLeft;
			this.RightMouse.WasDown = Main.mouseRight;
			this.MiddleMouse.WasDown = Main.mouseMiddle;
			this.XButton1Mouse.WasDown = Main.mouseXButton1;
			this.XButton2Mouse.WasDown = Main.mouseXButton2;
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x004B26E0 File Offset: 0x004B08E0
		private void ResetState()
		{
			if (!Main.dedServ)
			{
				this.GetMousePosition();
				this.ImmediatelyUpdateInputPointers();
				if (this._lastElementHover != null)
				{
					this._lastElementHover.MouseOut(new UIMouseEvent(this._lastElementHover, this.MousePosition));
				}
			}
			this.ClearPointers();
			this._lastElementHover = null;
			this._clickDisabledTimeRemaining = Math.Max(this._clickDisabledTimeRemaining, 200.0);
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x004B274B File Offset: 0x004B094B
		private void GetMousePosition()
		{
			this.MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x004B2764 File Offset: 0x004B0964
		public void Update(GameTime time)
		{
			if (this._currentState == null)
			{
				return;
			}
			this.GetMousePosition();
			UIElement uIElement = Main.hasFocus ? this._currentState.GetElementAt(this.MousePosition) : null;
			this._clickDisabledTimeRemaining = Math.Max(0.0, this._clickDisabledTimeRemaining - time.ElapsedGameTime.TotalMilliseconds);
			bool num = this._clickDisabledTimeRemaining > 0.0;
			try
			{
				this.Update_Inner(time, uIElement, ref num);
			}
			catch
			{
				throw;
			}
			finally
			{
				this.Update_End(time);
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x004B280C File Offset: 0x004B0A0C
		private void Update_Inner(GameTime time, UIElement uIElement, ref bool num)
		{
			if (uIElement != this._lastElementHover)
			{
				if (this._lastElementHover != null)
				{
					this._lastElementHover.MouseOut(new UIMouseEvent(this._lastElementHover, this.MousePosition));
				}
				if (uIElement != null)
				{
					uIElement.MouseOver(new UIMouseEvent(uIElement, this.MousePosition));
				}
				this._lastElementHover = uIElement;
			}
			if (!num)
			{
				this.HandleClick(this.LeftMouse, time, Main.mouseLeft && Main.hasFocus, uIElement);
				this.HandleClick(this.RightMouse, time, Main.mouseRight && Main.hasFocus, uIElement);
				this.HandleClick(this.MiddleMouse, time, Main.mouseMiddle && Main.hasFocus, uIElement);
				this.HandleClick(this.XButton1Mouse, time, Main.mouseXButton1 && Main.hasFocus, uIElement);
				this.HandleClick(this.XButton2Mouse, time, Main.mouseXButton2 && Main.hasFocus, uIElement);
			}
			if (PlayerInput.ScrollWheelDeltaForUI != 0 && uIElement != null)
			{
				uIElement.ScrollWheel(new UIScrollWheelEvent(uIElement, this.MousePosition, PlayerInput.ScrollWheelDeltaForUI));
			}
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x004B291B File Offset: 0x004B0B1B
		private void Update_End(GameTime time)
		{
			if (this._currentState != null)
			{
				this._currentState.Update(time);
			}
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x004B2934 File Offset: 0x004B0B34
		private void HandleClick(UserInterface.InputPointerCache cache, GameTime time, bool isDown, UIElement mouseElement)
		{
			if (isDown && !cache.WasDown && mouseElement != null)
			{
				cache.LastDown = mouseElement;
				cache.MouseDownEvent(mouseElement, new UIMouseEvent(mouseElement, this.MousePosition));
				if (cache.LastClicked == mouseElement && time.TotalGameTime.TotalMilliseconds - cache.LastTimeDown < 500.0)
				{
					cache.DoubleClickEvent(mouseElement, new UIMouseEvent(mouseElement, this.MousePosition));
					cache.LastClicked = null;
				}
				cache.LastTimeDown = time.TotalGameTime.TotalMilliseconds;
			}
			else if (!isDown && cache.WasDown && cache.LastDown != null)
			{
				UIElement lastDown = cache.LastDown;
				if (lastDown.ContainsPoint(this.MousePosition))
				{
					cache.ClickEvent(lastDown, new UIMouseEvent(lastDown, this.MousePosition));
					cache.LastClicked = cache.LastDown;
				}
				cache.MouseUpEvent(lastDown, new UIMouseEvent(lastDown, this.MousePosition));
				cache.LastDown = null;
			}
			cache.WasDown = isDown;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x004B2A4E File Offset: 0x004B0C4E
		public void Draw(SpriteBatch spriteBatch, GameTime time)
		{
			this.Use();
			if (this._currentState != null)
			{
				if (this._isStateDirty)
				{
					this._currentState.Recalculate();
					this._isStateDirty = false;
				}
				this._currentState.Draw(spriteBatch);
			}
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x004B2A84 File Offset: 0x004B0C84
		public void DrawDebugHitbox(BasicDebugDrawer drawer)
		{
			UIState currentState = this._currentState;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x004B2A90 File Offset: 0x004B0C90
		public void SetState(UIState state)
		{
			if (state == this._currentState)
			{
				return;
			}
			if (state != null)
			{
				this.AddToHistory(state);
			}
			if (this._currentState != null)
			{
				if (this._lastElementHover != null)
				{
					this._lastElementHover.MouseOut(new UIMouseEvent(this._lastElementHover, this.MousePosition));
				}
				this._currentState.Deactivate();
			}
			this._currentState = state;
			this.ResetState();
			if (state != null)
			{
				this._isStateDirty = true;
				state.Activate();
				state.Recalculate();
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x004B2B0C File Offset: 0x004B0D0C
		public void GoBack()
		{
			if (this._history.Count >= 2)
			{
				UIState state = this._history[this._history.Count - 2];
				this._history.RemoveRange(this._history.Count - 2, 2);
				this.SetState(state);
			}
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x004B2B60 File Offset: 0x004B0D60
		private void AddToHistory(UIState state)
		{
			this._history.Add(state);
			if (this._history.Count > 32)
			{
				this._history.RemoveRange(0, 4);
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x004B2B8A File Offset: 0x004B0D8A
		public void Recalculate()
		{
			if (this._currentState != null)
			{
				this._currentState.Recalculate();
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x004B2BA0 File Offset: 0x004B0DA0
		public CalculatedStyle GetDimensions()
		{
			Vector2 originalScreenSize = PlayerInput.OriginalScreenSize;
			return new CalculatedStyle(0f, 0f, originalScreenSize.X / Main.UIScale, originalScreenSize.Y / Main.UIScale);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x004B2BDA File Offset: 0x004B0DDA
		internal void RefreshState()
		{
			if (this._currentState != null)
			{
				this._currentState.Deactivate();
			}
			this.ResetState();
			this._currentState.Activate();
			this._currentState.Recalculate();
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x004B2C0B File Offset: 0x004B0E0B
		public bool IsElementUnderMouse()
		{
			return this.IsVisible && this._lastElementHover != null && !(this._lastElementHover is UIState);
		}

		// Token: 0x0400115D RID: 4445
		private const double DOUBLE_CLICK_TIME = 500.0;

		// Token: 0x0400115E RID: 4446
		private const double STATE_CHANGE_CLICK_DISABLE_TIME = 200.0;

		// Token: 0x0400115F RID: 4447
		private const int MAX_HISTORY_SIZE = 32;

		// Token: 0x04001160 RID: 4448
		private const int HISTORY_PRUNE_SIZE = 4;

		// Token: 0x04001161 RID: 4449
		public static UserInterface ActiveInstance = new UserInterface();

		// Token: 0x04001162 RID: 4450
		private List<UIState> _history = new List<UIState>();

		// Token: 0x04001163 RID: 4451
		private UserInterface.InputPointerCache LeftMouse;

		// Token: 0x04001164 RID: 4452
		private UserInterface.InputPointerCache RightMouse;

		// Token: 0x04001165 RID: 4453
		public Vector2 MousePosition;

		// Token: 0x04001166 RID: 4454
		private UIElement _lastElementHover;

		// Token: 0x04001167 RID: 4455
		private double _clickDisabledTimeRemaining;

		// Token: 0x04001168 RID: 4456
		private bool _isStateDirty;

		// Token: 0x04001169 RID: 4457
		public bool IsVisible;

		// Token: 0x0400116A RID: 4458
		private UIState _currentState;

		// Token: 0x0400116B RID: 4459
		[Nullable(1)]
		private UserInterface.InputPointerCache MiddleMouse;

		// Token: 0x0400116C RID: 4460
		[Nullable(1)]
		private UserInterface.InputPointerCache XButton1Mouse;

		// Token: 0x0400116D RID: 4461
		[Nullable(1)]
		private UserInterface.InputPointerCache XButton2Mouse;

		// Token: 0x02000875 RID: 2165
		// (Invoke) Token: 0x06005181 RID: 20865
		private delegate void MouseElementEvent(UIElement element, UIMouseEvent evt);

		// Token: 0x02000876 RID: 2166
		private class InputPointerCache
		{
			// Token: 0x06005184 RID: 20868 RVA: 0x0069748D File Offset: 0x0069568D
			public void Clear()
			{
				this.LastClicked = null;
				this.LastDown = null;
				this.LastTimeDown = 0.0;
			}

			// Token: 0x0400698C RID: 27020
			public double LastTimeDown;

			// Token: 0x0400698D RID: 27021
			public bool WasDown;

			// Token: 0x0400698E RID: 27022
			public UIElement LastDown;

			// Token: 0x0400698F RID: 27023
			public UIElement LastClicked;

			// Token: 0x04006990 RID: 27024
			public UserInterface.MouseElementEvent MouseDownEvent;

			// Token: 0x04006991 RID: 27025
			public UserInterface.MouseElementEvent MouseUpEvent;

			// Token: 0x04006992 RID: 27026
			public UserInterface.MouseElementEvent ClickEvent;

			// Token: 0x04006993 RID: 27027
			public UserInterface.MouseElementEvent DoubleClickEvent;
		}
	}
}
