using System;
using System.Linq;

namespace Terraria.GameInput
{
	// Token: 0x02000488 RID: 1160
	public class TriggersPack
	{
		// Token: 0x06003830 RID: 14384 RVA: 0x00592F65 File Offset: 0x00591165
		public void Initialize()
		{
			this.Current.SetupKeys();
			this.Old.SetupKeys();
			this.JustPressed.SetupKeys();
			this.JustReleased.SetupKeys();
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x00592F93 File Offset: 0x00591193
		public void Reset()
		{
			this.Old.CloneFrom(this.Current);
			this.Current.Reset();
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x00592FB1 File Offset: 0x005911B1
		public void Update()
		{
			this.CompareDiffs(this.JustPressed, this.Old, this.Current);
			this.CompareDiffs(this.JustReleased, this.Current, this.Old);
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x00592FE4 File Offset: 0x005911E4
		public void CompareDiffs(TriggersSet Bearer, TriggersSet oldset, TriggersSet newset)
		{
			Bearer.Reset();
			foreach (string item in Bearer.KeyStatus.Keys.ToList<string>())
			{
				Bearer.KeyStatus[item] = (newset.KeyStatus[item] && !oldset.KeyStatus[item]);
			}
		}

		// Token: 0x040051F8 RID: 20984
		public TriggersSet Current = new TriggersSet();

		// Token: 0x040051F9 RID: 20985
		public TriggersSet Old = new TriggersSet();

		// Token: 0x040051FA RID: 20986
		public TriggersSet JustPressed = new TriggersSet();

		// Token: 0x040051FB RID: 20987
		public TriggersSet JustReleased = new TriggersSet();
	}
}
