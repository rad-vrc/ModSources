using System;

namespace Terraria.Map
{
	// Token: 0x020003CB RID: 971
	public interface IMapLayer
	{
		// Token: 0x0600332B RID: 13099
		void Draw(ref MapOverlayDrawContext context, ref string text);

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x0054934B File Offset: 0x0054754B
		// (set) Token: 0x0600332D RID: 13101 RVA: 0x00549352 File Offset: 0x00547552
		IMapLayer Spawn { get; set; } = new SpawnMapLayer();

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x0054935A File Offset: 0x0054755A
		// (set) Token: 0x0600332F RID: 13103 RVA: 0x00549361 File Offset: 0x00547561
		IMapLayer Pylons { get; set; } = new TeleportPylonsMapLayer();

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x00549369 File Offset: 0x00547569
		// (set) Token: 0x06003331 RID: 13105 RVA: 0x00549370 File Offset: 0x00547570
		IMapLayer Pings { get; set; } = new PingMapLayer();

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06003332 RID: 13106
		// (set) Token: 0x06003333 RID: 13107
		bool Visible { get; set; }

		// Token: 0x06003334 RID: 13108 RVA: 0x00549378 File Offset: 0x00547578
		void Hide()
		{
			this.Visible = false;
		}
	}
}
