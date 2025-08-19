using System;

namespace Terraria.UI
{
	// Token: 0x02000086 RID: 134
	public class LegacyGameInterfaceLayer : GameInterfaceLayer
	{
		// Token: 0x060011FC RID: 4604 RVA: 0x004909F7 File Offset: 0x0048EBF7
		public LegacyGameInterfaceLayer(string name, GameInterfaceDrawMethod drawMethod, InterfaceScaleType scaleType = InterfaceScaleType.Game) : base(name, scaleType)
		{
			this._drawMethod = drawMethod;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00490A08 File Offset: 0x0048EC08
		protected override bool DrawSelf()
		{
			return this._drawMethod();
		}

		// Token: 0x04000FF4 RID: 4084
		private GameInterfaceDrawMethod _drawMethod;
	}
}
