using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameInput;

namespace Terraria.UI
{
	// Token: 0x0200009A RID: 154
	public class UserInterface
	{
		// Token: 0x060012F9 RID: 4857 RVA: 0x0049CD3D File Offset: 0x0049AF3D
		public void ClearPointers()
		{
			this.LeftMouse.Clear();
			this.RightMouse.Clear();
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0049CD55 File Offset: 0x0049AF55
		public void ResetLasts()
		{
			if (this._lastElementHover != null)
			{
				this._lastElementHover.MouseOut(new UIMouseEvent(this._lastElementHover, this.MousePosition));
			}
			this.ClearPointers();
			this._lastElementHover = null;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0049CD88 File Offset: 0x0049AF88
		public void EscapeElements()
		{
			this.ResetLasts();
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0049CD90 File Offset: 0x0049AF90
		public UIState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0049CD98 File Offset: 0x0049AF98
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
			base..ctor();
			UserInterface.ActiveInstance = this;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0049CEFA File Offset: 0x0049B0FA
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

		// Token: 0x060012FF RID: 4863 RVA: 0x0049CF17 File Offset: 0x0049B117
		private void ImmediatelyUpdateInputPointers()
		{
			this.LeftMouse.WasDown = Main.mouseLeft;
			this.RightMouse.WasDown = Main.mouseRight;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0049CF3C File Offset: 0x0049B13C
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

		// Token: 0x06001301 RID: 4865 RVA: 0x0049CFA7 File Offset: 0x0049B1A7
		private void GetMousePosition()
		{
			this.MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0049CFC0 File Offset: 0x0049B1C0
		public void Update(GameTime time)
		{
			if (this._currentState == null)
			{
				return;
			}
			this.GetMousePosition();
			UIElement uielement = Main.hasFocus ? this._currentState.GetElementAt(this.MousePosition) : null;
			this._clickDisabledTimeRemaining = Math.Max(0.0, this._clickDisabledTimeRemaining - time.ElapsedGameTime.TotalMilliseconds);
			bool flag = this._clickDisabledTimeRemaining > 0.0;
			if (uielement != this._lastElementHover)
			{
				if (this._lastElementHover != null)
				{
					this._lastElementHover.MouseOut(new UIMouseEvent(this._lastElementHover, this.MousePosition));
				}
				if (uielement != null)
				{
					uielement.MouseOver(new UIMouseEvent(uielement, this.MousePosition));
				}
				this._lastElementHover = uielement;
			}
			if (!flag)
			{
				this.HandleClick(this.LeftMouse, time, Main.mouseLeft && Main.hasFocus, uielement);
				this.HandleClick(this.RightMouse, time, Main.mouseRight && Main.hasFocus, uielement);
			}
			if (PlayerInput.ScrollWheelDeltaForUI != 0)
			{
				if (uielement != null)
				{
					uielement.ScrollWheel(new UIScrollWheelEvent(uielement, this.MousePosition, PlayerInput.ScrollWheelDeltaForUI));
				}
				PlayerInput.ScrollWheelDeltaForUI = 0;
			}
			if (this._currentState != null)
			{
				this._currentState.Update(time);
			}
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0049D0F4 File Offset: 0x0049B2F4
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

		// Token: 0x06001304 RID: 4868 RVA: 0x0049D20E File Offset: 0x0049B40E
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

		// Token: 0x06001305 RID: 4869 RVA: 0x0049D244 File Offset: 0x0049B444
		public void DrawDebugHitbox(BasicDebugDrawer drawer)
		{
			UIState currentState = this._currentState;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0049D250 File Offset: 0x0049B450
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

		// Token: 0x06001307 RID: 4871 RVA: 0x0049D2CC File Offset: 0x0049B4CC
		public void GoBack()
		{
			if (this._history.Count < 2)
			{
				return;
			}
			UIState state = this._history[this._history.Count - 2];
			this._history.RemoveRange(this._history.Count - 2, 2);
			this.SetState(state);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0049D321 File Offset: 0x0049B521
		private void AddToHistory(UIState state)
		{
			this._history.Add(state);
			if (this._history.Count > 32)
			{
				this._history.RemoveRange(0, 4);
			}
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0049D34B File Offset: 0x0049B54B
		public void Recalculate()
		{
			if (this._currentState != null)
			{
				this._currentState.Recalculate();
			}
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0049D360 File Offset: 0x0049B560
		public CalculatedStyle GetDimensions()
		{
			Vector2 originalScreenSize = PlayerInput.OriginalScreenSize;
			return new CalculatedStyle(0f, 0f, originalScreenSize.X / Main.UIScale, originalScreenSize.Y / Main.UIScale);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0049D39A File Offset: 0x0049B59A
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

		// Token: 0x0600130C RID: 4876 RVA: 0x0049D3CB File Offset: 0x0049B5CB
		public bool IsElementUnderMouse()
		{
			return this.IsVisible && this._lastElementHover != null && !(this._lastElementHover is UIState);
		}

		// Token: 0x0400105A RID: 4186
		private const double DOUBLE_CLICK_TIME = 500.0;

		// Token: 0x0400105B RID: 4187
		private const double STATE_CHANGE_CLICK_DISABLE_TIME = 200.0;

		// Token: 0x0400105C RID: 4188
		private const int MAX_HISTORY_SIZE = 32;

		// Token: 0x0400105D RID: 4189
		private const int HISTORY_PRUNE_SIZE = 4;

		// Token: 0x0400105E RID: 4190
		public static UserInterface ActiveInstance = new UserInterface();

		// Token: 0x0400105F RID: 4191
		private List<UIState> _history = new List<UIState>();

		// Token: 0x04001060 RID: 4192
		private UserInterface.InputPointerCache LeftMouse;

		// Token: 0x04001061 RID: 4193
		private UserInterface.InputPointerCache RightMouse;

		// Token: 0x04001062 RID: 4194
		public Vector2 MousePosition;

		// Token: 0x04001063 RID: 4195
		private UIElement _lastElementHover;

		// Token: 0x04001064 RID: 4196
		private double _clickDisabledTimeRemaining;

		// Token: 0x04001065 RID: 4197
		private bool _isStateDirty;

		// Token: 0x04001066 RID: 4198
		public bool IsVisible;

		// Token: 0x04001067 RID: 4199
		private UIState _currentState;

		// Token: 0x0200054C RID: 1356
		// (Invoke) Token: 0x060030F8 RID: 12536
		private delegate void MouseElementEvent(UIElement element, UIMouseEvent evt);

		// Token: 0x0200054D RID: 1357
		private class InputPointerCache
		{
			// Token: 0x060030FB RID: 12539 RVA: 0x005E4B37 File Offset: 0x005E2D37
			public void Clear()
			{
				this.LastClicked = null;
				this.LastDown = null;
				this.LastTimeDown = 0.0;
			}

			// Token: 0x0400589A RID: 22682
			public double LastTimeDown;

			// Token: 0x0400589B RID: 22683
			public bool WasDown;

			// Token: 0x0400589C RID: 22684
			public UIElement LastDown;

			// Token: 0x0400589D RID: 22685
			public UIElement LastClicked;

			// Token: 0x0400589E RID: 22686
			public UserInterface.MouseElementEvent MouseDownEvent;

			// Token: 0x0400589F RID: 22687
			public UserInterface.MouseElementEvent MouseUpEvent;

			// Token: 0x040058A0 RID: 22688
			public UserInterface.MouseElementEvent ClickEvent;

			// Token: 0x040058A1 RID: 22689
			public UserInterface.MouseElementEvent DoubleClickEvent;
		}
	}
}
