using System;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000227 RID: 551
	public class MoonLordScreenShaderData : ScreenShaderData
	{
		// Token: 0x06001ED6 RID: 7894 RVA: 0x0050E14C File Offset: 0x0050C34C
		public MoonLordScreenShaderData(string passName, bool aimAtPlayer) : base(passName)
		{
			this._aimAtPlayer = aimAtPlayer;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0050E164 File Offset: 0x0050C364
		private void UpdateMoonLordIndex()
		{
			if (this._aimAtPlayer)
			{
				return;
			}
			if (this._moonLordIndex >= 0 && Main.npc[this._moonLordIndex].active && Main.npc[this._moonLordIndex].type == 398)
			{
				return;
			}
			int moonLordIndex = -1;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == 398)
				{
					moonLordIndex = i;
					break;
				}
			}
			this._moonLordIndex = moonLordIndex;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0050E1F0 File Offset: 0x0050C3F0
		public override void Apply()
		{
			this.UpdateMoonLordIndex();
			if (this._aimAtPlayer)
			{
				base.UseTargetPosition(Main.LocalPlayer.Center);
			}
			else if (this._moonLordIndex != -1)
			{
				base.UseTargetPosition(Main.npc[this._moonLordIndex].Center);
			}
			base.Apply();
		}

		// Token: 0x040045E2 RID: 17890
		private int _moonLordIndex = -1;

		// Token: 0x040045E3 RID: 17891
		private bool _aimAtPlayer;
	}
}
