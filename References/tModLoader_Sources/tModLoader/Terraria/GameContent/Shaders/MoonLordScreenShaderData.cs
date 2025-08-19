using System;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000574 RID: 1396
	public class MoonLordScreenShaderData : ScreenShaderData
	{
		// Token: 0x060041D2 RID: 16850 RVA: 0x005F0F94 File Offset: 0x005EF194
		public MoonLordScreenShaderData(string passName, bool aimAtPlayer) : base(passName)
		{
			this._aimAtPlayer = aimAtPlayer;
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x005F0FAC File Offset: 0x005EF1AC
		private void UpdateMoonLordIndex()
		{
			if (this._aimAtPlayer || (this._moonLordIndex >= 0 && Main.npc[this._moonLordIndex].active && Main.npc[this._moonLordIndex].type == 398))
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

		// Token: 0x060041D4 RID: 16852 RVA: 0x005F1038 File Offset: 0x005EF238
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

		// Token: 0x04005902 RID: 22786
		private int _moonLordIndex = -1;

		// Token: 0x04005903 RID: 22787
		private bool _aimAtPlayer;
	}
}
