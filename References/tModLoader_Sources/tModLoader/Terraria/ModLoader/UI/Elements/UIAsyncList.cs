using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.ModLoader.UI.Elements
{
	/// <remarks>
	///   Remember to set GenElement is not provided in the constructor and TResource is not a TUIElement.
	///   DO NOT USE Add/AddRange directly, always use the provider methods.
	/// </remarks>
	// Token: 0x02000275 RID: 629
	public abstract class UIAsyncList<TResource, TUIElement> : UIList where TUIElement : UIElement
	{
		// Token: 0x06002B47 RID: 11079
		protected abstract TUIElement GenElement(TResource resource);

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x00521CF5 File Offset: 0x0051FEF5
		// (set) Token: 0x06002B49 RID: 11081 RVA: 0x00521CFD File Offset: 0x0051FEFD
		public AsyncProviderState State { get; private set; } = AsyncProviderState.Completed;

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x00521D06 File Offset: 0x0051FF06
		private AsyncProviderState RealtimeState
		{
			get
			{
				AsyncProvider<TResource> provider = this.Provider;
				if (provider == null)
				{
					return AsyncProviderState.Completed;
				}
				return provider.State;
			}
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06002B4B RID: 11083 RVA: 0x00521D1C File Offset: 0x0051FF1C
		// (remove) Token: 0x06002B4C RID: 11084 RVA: 0x00521D54 File Offset: 0x0051FF54
		public event UIAsyncList<TResource, TUIElement>.StateDelegate OnStartLoading;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06002B4D RID: 11085 RVA: 0x00521D8C File Offset: 0x0051FF8C
		// (remove) Token: 0x06002B4E RID: 11086 RVA: 0x00521DC4 File Offset: 0x0051FFC4
		public event UIAsyncList<TResource, TUIElement>.StateDelegateWithException OnFinished;

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x00521DF9 File Offset: 0x0051FFF9
		public IEnumerable<TUIElement> ReceivedItems
		{
			get
			{
				foreach (UIElement el in this)
				{
					if (el != this.EndItem)
					{
						yield return el as TUIElement;
					}
				}
				IEnumerator<UIElement> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x00521E09 File Offset: 0x00520009
		public UIAsyncList()
		{
			this.ManualSortMethod = delegate(List<UIElement> l)
			{
			};
		}

		/// <remarks>
		///   SetProvider will delegate all UI actions to next Update,
		///   so it NOT SAFE to be called out of the main thread,
		///   because having an assignment to ProviderChanged it CAN
		///   cause problems in case the list is cleared before the provider
		///   is swapped and the old provider is partially read giving unwanted
		///   elements, same if you do the other way around (the provider can be
		///   partially consumed before the clear)
		/// </remarks>
		// Token: 0x06002B51 RID: 11089 RVA: 0x00521E3D File Offset: 0x0052003D
		public void SetProvider(AsyncProvider<TResource> provider = null)
		{
			AsyncProvider<TResource> provider2 = this.Provider;
			if (provider2 != null)
			{
				provider2.Cancel();
			}
			this.ProviderChanged = true;
			this.Provider = provider;
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x00521E5E File Offset: 0x0052005E
		public void SetEnumerable(IAsyncEnumerable<TResource> enumerable = null)
		{
			if (enumerable != null)
			{
				this.SetProvider(new AsyncProvider<TResource>(enumerable));
				return;
			}
			this.SetProvider(null);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x00521E78 File Offset: 0x00520078
		public override void Update(GameTime gameTime)
		{
			bool endItemTextNeedUpdate = false;
			if (this.ProviderChanged)
			{
				this.Clear();
				this.Add(this.EndItem);
				this.ProviderChanged = false;
				this.InternalOnUpdateState(AsyncProviderState.Loading);
				endItemTextNeedUpdate = true;
			}
			if (this.Provider != null)
			{
				TUIElement[] uiels = this.Provider.GetData().Select(new Func<TResource, TUIElement>(this.GenElement)).ToArray<TUIElement>();
				if (uiels.Length != 0)
				{
					this.Remove(this.EndItem);
					this.AddRange(uiels);
					this.Add(this.EndItem);
				}
			}
			AsyncProviderState providerState = this.RealtimeState;
			if (providerState != this.State)
			{
				this.InternalOnUpdateState(providerState);
				endItemTextNeedUpdate = true;
			}
			if (endItemTextNeedUpdate)
			{
				this.EndItem.SetText(this.GetEndItemText());
			}
			base.Update(gameTime);
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x00521F34 File Offset: 0x00520134
		private void InternalOnUpdateState(AsyncProviderState state)
		{
			this.State = state;
			if (this.State.IsFinished())
			{
				UIAsyncList<TResource, TUIElement>.StateDelegateWithException onFinished = this.OnFinished;
				AsyncProviderState state2 = this.State;
				AsyncProvider<TResource> provider = this.Provider;
				onFinished(state2, (provider != null) ? provider.Exception : null);
				return;
			}
			this.OnStartLoading(this.State);
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x00521F8A File Offset: 0x0052018A
		public override void OnInitialize()
		{
			base.OnInitialize();
			this.EndItem = new UIText(this.GetEndItemText(), 1f, false)
			{
				HAlign = 0.5f
			}.WithPadding(15f);
			this.Add(this.EndItem);
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x00521FCC File Offset: 0x005201CC
		public virtual string GetEndItemText()
		{
			switch (this.State)
			{
			case AsyncProviderState.Loading:
				return Language.GetTextValue("tModLoader.ALLoading");
			case AsyncProviderState.Completed:
				if (!this.ReceivedItems.Any<TUIElement>())
				{
					return Language.GetTextValue("tModLoader.ALNoEntries");
				}
				return "";
			case AsyncProviderState.Canceled:
				return Language.GetTextValue("tModLoader.ALCancel");
			case AsyncProviderState.Aborted:
				return Language.GetTextValue("tModLoader.ALError");
			default:
				return "Invalid state";
			}
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0052203C File Offset: 0x0052023C
		public void Cancel()
		{
			AsyncProvider<TResource> provider = this.Provider;
			if (provider == null)
			{
				return;
			}
			provider.Cancel();
		}

		// Token: 0x04001BCA RID: 7114
		private bool ProviderChanged;

		// Token: 0x04001BCB RID: 7115
		private AsyncProvider<TResource> Provider;

		// Token: 0x04001BCC RID: 7116
		private UIText EndItem;

		// Token: 0x02000A2A RID: 2602
		// (Invoke) Token: 0x060057D2 RID: 22482
		public delegate void StateDelegate(AsyncProviderState state);

		// Token: 0x02000A2B RID: 2603
		// (Invoke) Token: 0x060057D6 RID: 22486
		public delegate void StateDelegateWithException(AsyncProviderState state, Exception e);
	}
}
