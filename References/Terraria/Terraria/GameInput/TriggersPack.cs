using System;
using System.Linq;

namespace Terraria.GameInput
{
	// Token: 0x02000139 RID: 313
	public class TriggersPack
	{
		// Token: 0x060017E6 RID: 6118 RVA: 0x004D761C File Offset: 0x004D581C
		public void Initialize()
		{
			this.Current.SetupKeys();
			this.Old.SetupKeys();
			this.JustPressed.SetupKeys();
			this.JustReleased.SetupKeys();
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x004D764A File Offset: 0x004D584A
		public void Reset()
		{
			this.Old.CloneFrom(this.Current);
			this.Current.Reset();
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x004D7668 File Offset: 0x004D5868
		public void Update()
		{
			this.CompareDiffs(this.JustPressed, this.Old, this.Current);
			this.CompareDiffs(this.JustReleased, this.Current, this.Old);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x004D769C File Offset: 0x004D589C
		public void CompareDiffs(TriggersSet Bearer, TriggersSet oldset, TriggersSet newset)
		{
			Bearer.Reset();
			foreach (string key in Bearer.KeyStatus.Keys.ToList<string>())
			{
				Bearer.KeyStatus[key] = (newset.KeyStatus[key] && !oldset.KeyStatus[key]);
			}
		}

		// Token: 0x040014B9 RID: 5305
		public TriggersSet Current = new TriggersSet();

		// Token: 0x040014BA RID: 5306
		public TriggersSet Old = new TriggersSet();

		// Token: 0x040014BB RID: 5307
		public TriggersSet JustPressed = new TriggersSet();

		// Token: 0x040014BC RID: 5308
		public TriggersSet JustReleased = new TriggersSet();
	}
}
