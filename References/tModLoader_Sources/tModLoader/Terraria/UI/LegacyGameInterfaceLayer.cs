using System;

namespace Terraria.UI
{
	// Token: 0x020000AD RID: 173
	public class LegacyGameInterfaceLayer : GameInterfaceLayer
	{
		// Token: 0x0600157B RID: 5499 RVA: 0x004AFB03 File Offset: 0x004ADD03
		public LegacyGameInterfaceLayer(string name, GameInterfaceDrawMethod drawMethod, InterfaceScaleType scaleType = InterfaceScaleType.Game) : base(name, scaleType)
		{
			this._drawMethod = drawMethod;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x004AFB14 File Offset: 0x004ADD14
		protected override bool DrawSelf()
		{
			return this._drawMethod();
		}

		// Token: 0x04001103 RID: 4355
		private GameInterfaceDrawMethod _drawMethod;
	}
}
