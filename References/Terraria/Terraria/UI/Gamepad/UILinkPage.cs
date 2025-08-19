using System;
using System.Collections.Generic;

namespace Terraria.UI.Gamepad
{
	// Token: 0x020000A3 RID: 163
	public class UILinkPage
	{
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06001339 RID: 4921 RVA: 0x0049E6F8 File Offset: 0x0049C8F8
		// (remove) Token: 0x0600133A RID: 4922 RVA: 0x0049E730 File Offset: 0x0049C930
		public event Action<int, int> ReachEndEvent;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600133B RID: 4923 RVA: 0x0049E768 File Offset: 0x0049C968
		// (remove) Token: 0x0600133C RID: 4924 RVA: 0x0049E7A0 File Offset: 0x0049C9A0
		public event Action TravelEvent;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600133D RID: 4925 RVA: 0x0049E7D8 File Offset: 0x0049C9D8
		// (remove) Token: 0x0600133E RID: 4926 RVA: 0x0049E810 File Offset: 0x0049CA10
		public event Action LeaveEvent;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600133F RID: 4927 RVA: 0x0049E848 File Offset: 0x0049CA48
		// (remove) Token: 0x06001340 RID: 4928 RVA: 0x0049E880 File Offset: 0x0049CA80
		public event Action EnterEvent;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001341 RID: 4929 RVA: 0x0049E8B8 File Offset: 0x0049CAB8
		// (remove) Token: 0x06001342 RID: 4930 RVA: 0x0049E8F0 File Offset: 0x0049CAF0
		public event Action UpdateEvent;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001343 RID: 4931 RVA: 0x0049E928 File Offset: 0x0049CB28
		// (remove) Token: 0x06001344 RID: 4932 RVA: 0x0049E960 File Offset: 0x0049CB60
		public event Func<bool> IsValidEvent;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001345 RID: 4933 RVA: 0x0049E998 File Offset: 0x0049CB98
		// (remove) Token: 0x06001346 RID: 4934 RVA: 0x0049E9D0 File Offset: 0x0049CBD0
		public event Func<bool> CanEnterEvent;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001347 RID: 4935 RVA: 0x0049EA08 File Offset: 0x0049CC08
		// (remove) Token: 0x06001348 RID: 4936 RVA: 0x0049EA40 File Offset: 0x0049CC40
		public event Action<int> OnPageMoveAttempt;

		// Token: 0x06001349 RID: 4937 RVA: 0x0049EA75 File Offset: 0x0049CC75
		public UILinkPage()
		{
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0049EA96 File Offset: 0x0049CC96
		public UILinkPage(int id)
		{
			this.ID = id;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0049EABE File Offset: 0x0049CCBE
		public void Update()
		{
			if (this.UpdateEvent != null)
			{
				this.UpdateEvent();
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0049EAD3 File Offset: 0x0049CCD3
		public void Leave()
		{
			if (this.LeaveEvent != null)
			{
				this.LeaveEvent();
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0049EAE8 File Offset: 0x0049CCE8
		public void Enter()
		{
			if (this.EnterEvent != null)
			{
				this.EnterEvent();
			}
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0049EAFD File Offset: 0x0049CCFD
		public bool IsValid()
		{
			return this.IsValidEvent == null || this.IsValidEvent();
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0049EB14 File Offset: 0x0049CD14
		public bool CanEnter()
		{
			return this.CanEnterEvent == null || this.CanEnterEvent();
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0049EB2B File Offset: 0x0049CD2B
		public void TravelUp()
		{
			this.Travel(this.LinkMap[this.CurrentPoint].Up);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0049EB49 File Offset: 0x0049CD49
		public void TravelDown()
		{
			this.Travel(this.LinkMap[this.CurrentPoint].Down);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0049EB67 File Offset: 0x0049CD67
		public void TravelLeft()
		{
			this.Travel(this.LinkMap[this.CurrentPoint].Left);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0049EB85 File Offset: 0x0049CD85
		public void TravelRight()
		{
			this.Travel(this.LinkMap[this.CurrentPoint].Right);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0049EBA3 File Offset: 0x0049CDA3
		public void SwapPageLeft()
		{
			if (this.OnPageMoveAttempt != null)
			{
				this.OnPageMoveAttempt(-1);
			}
			UILinkPointNavigator.ChangePage(this.PageOnLeft);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0049EBC4 File Offset: 0x0049CDC4
		public void SwapPageRight()
		{
			if (this.OnPageMoveAttempt != null)
			{
				this.OnPageMoveAttempt(1);
			}
			UILinkPointNavigator.ChangePage(this.PageOnRight);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0049EBE8 File Offset: 0x0049CDE8
		private void Travel(int next)
		{
			if (next < 0)
			{
				if (this.ReachEndEvent != null)
				{
					this.ReachEndEvent(this.CurrentPoint, next);
					if (this.TravelEvent != null)
					{
						this.TravelEvent();
						return;
					}
				}
			}
			else
			{
				UILinkPointNavigator.ChangePoint(next);
				if (this.TravelEvent != null)
				{
					this.TravelEvent();
				}
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001357 RID: 4951 RVA: 0x0049EC40 File Offset: 0x0049CE40
		// (remove) Token: 0x06001358 RID: 4952 RVA: 0x0049EC78 File Offset: 0x0049CE78
		public event Func<string> OnSpecialInteracts;

		// Token: 0x06001359 RID: 4953 RVA: 0x0049ECAD File Offset: 0x0049CEAD
		public string SpecialInteractions()
		{
			if (this.OnSpecialInteracts != null)
			{
				return this.OnSpecialInteracts();
			}
			return string.Empty;
		}

		// Token: 0x04001184 RID: 4484
		public int ID;

		// Token: 0x04001185 RID: 4485
		public int PageOnLeft = -1;

		// Token: 0x04001186 RID: 4486
		public int PageOnRight = -1;

		// Token: 0x04001187 RID: 4487
		public int DefaultPoint;

		// Token: 0x04001188 RID: 4488
		public int CurrentPoint;

		// Token: 0x04001189 RID: 4489
		public Dictionary<int, UILinkPoint> LinkMap = new Dictionary<int, UILinkPoint>();
	}
}
