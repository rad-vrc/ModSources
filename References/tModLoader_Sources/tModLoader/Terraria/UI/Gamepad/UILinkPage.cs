using System;
using System.Collections.Generic;

namespace Terraria.UI.Gamepad
{
	// Token: 0x020000BD RID: 189
	public class UILinkPage
	{
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001634 RID: 5684 RVA: 0x004B2F24 File Offset: 0x004B1124
		// (remove) Token: 0x06001635 RID: 5685 RVA: 0x004B2F5C File Offset: 0x004B115C
		public event Action<int, int> ReachEndEvent;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001636 RID: 5686 RVA: 0x004B2F94 File Offset: 0x004B1194
		// (remove) Token: 0x06001637 RID: 5687 RVA: 0x004B2FCC File Offset: 0x004B11CC
		public event Action TravelEvent;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001638 RID: 5688 RVA: 0x004B3004 File Offset: 0x004B1204
		// (remove) Token: 0x06001639 RID: 5689 RVA: 0x004B303C File Offset: 0x004B123C
		public event Action LeaveEvent;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x0600163A RID: 5690 RVA: 0x004B3074 File Offset: 0x004B1274
		// (remove) Token: 0x0600163B RID: 5691 RVA: 0x004B30AC File Offset: 0x004B12AC
		public event Action EnterEvent;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600163C RID: 5692 RVA: 0x004B30E4 File Offset: 0x004B12E4
		// (remove) Token: 0x0600163D RID: 5693 RVA: 0x004B311C File Offset: 0x004B131C
		public event Action UpdateEvent;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600163E RID: 5694 RVA: 0x004B3154 File Offset: 0x004B1354
		// (remove) Token: 0x0600163F RID: 5695 RVA: 0x004B318C File Offset: 0x004B138C
		public event Func<bool> IsValidEvent;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06001640 RID: 5696 RVA: 0x004B31C4 File Offset: 0x004B13C4
		// (remove) Token: 0x06001641 RID: 5697 RVA: 0x004B31FC File Offset: 0x004B13FC
		public event Func<bool> CanEnterEvent;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06001642 RID: 5698 RVA: 0x004B3234 File Offset: 0x004B1434
		// (remove) Token: 0x06001643 RID: 5699 RVA: 0x004B326C File Offset: 0x004B146C
		public event Action<int> OnPageMoveAttempt;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06001644 RID: 5700 RVA: 0x004B32A4 File Offset: 0x004B14A4
		// (remove) Token: 0x06001645 RID: 5701 RVA: 0x004B32DC File Offset: 0x004B14DC
		public event Func<string> OnSpecialInteracts;

		// Token: 0x06001646 RID: 5702 RVA: 0x004B3311 File Offset: 0x004B1511
		public UILinkPage()
		{
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x004B3332 File Offset: 0x004B1532
		public UILinkPage(int id)
		{
			this.ID = id;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x004B335A File Offset: 0x004B155A
		public void Update()
		{
			if (this.UpdateEvent != null)
			{
				this.UpdateEvent();
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x004B336F File Offset: 0x004B156F
		public void Leave()
		{
			if (this.LeaveEvent != null)
			{
				this.LeaveEvent();
			}
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x004B3384 File Offset: 0x004B1584
		public void Enter()
		{
			if (this.EnterEvent != null)
			{
				this.EnterEvent();
			}
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x004B3399 File Offset: 0x004B1599
		public bool IsValid()
		{
			return this.IsValidEvent == null || this.IsValidEvent();
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x004B33B0 File Offset: 0x004B15B0
		public bool CanEnter()
		{
			return this.CanEnterEvent == null || this.CanEnterEvent();
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x004B33C7 File Offset: 0x004B15C7
		public void TravelUp()
		{
			this.Travel(this.LinkMap[this.CurrentPoint].Up);
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x004B33E5 File Offset: 0x004B15E5
		public void TravelDown()
		{
			this.Travel(this.LinkMap[this.CurrentPoint].Down);
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x004B3403 File Offset: 0x004B1603
		public void TravelLeft()
		{
			this.Travel(this.LinkMap[this.CurrentPoint].Left);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x004B3421 File Offset: 0x004B1621
		public void TravelRight()
		{
			this.Travel(this.LinkMap[this.CurrentPoint].Right);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x004B343F File Offset: 0x004B163F
		public void SwapPageLeft()
		{
			if (this.OnPageMoveAttempt != null)
			{
				this.OnPageMoveAttempt(-1);
			}
			UILinkPointNavigator.ChangePage(this.PageOnLeft);
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x004B3460 File Offset: 0x004B1660
		public void SwapPageRight()
		{
			if (this.OnPageMoveAttempt != null)
			{
				this.OnPageMoveAttempt(1);
			}
			UILinkPointNavigator.ChangePage(this.PageOnRight);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x004B3484 File Offset: 0x004B1684
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

		// Token: 0x06001654 RID: 5716 RVA: 0x004B34DC File Offset: 0x004B16DC
		public string SpecialInteractions()
		{
			if (this.OnSpecialInteracts != null)
			{
				return this.OnSpecialInteracts();
			}
			return string.Empty;
		}

		// Token: 0x04001275 RID: 4725
		public int ID;

		// Token: 0x04001276 RID: 4726
		public int PageOnLeft = -1;

		// Token: 0x04001277 RID: 4727
		public int PageOnRight = -1;

		// Token: 0x04001278 RID: 4728
		public int DefaultPoint;

		// Token: 0x04001279 RID: 4729
		public int CurrentPoint;

		// Token: 0x0400127A RID: 4730
		public Dictionary<int, UILinkPoint> LinkMap = new Dictionary<int, UILinkPoint>();
	}
}
