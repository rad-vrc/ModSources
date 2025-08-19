using System;
using System.Runtime.CompilerServices;
using Terraria.DataStructures;

namespace Terraria.ModLoader
{
	// Token: 0x020001E8 RID: 488
	[Autoload(false)]
	public class PlayerDrawLayerSlot : PlayerDrawLayer
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x004F6F5A File Offset: 0x004F515A
		public PlayerDrawLayer Layer { get; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x004F6F62 File Offset: 0x004F5162
		public PlayerDrawLayer.Multiple.Condition Condition { get; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x004F6F6C File Offset: 0x004F516C
		public override string Name
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
				defaultInterpolatedStringHandler.AppendFormatted(this.Layer.Name);
				defaultInterpolatedStringHandler.AppendLiteral("_slot");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this._slot);
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x004F6FB4 File Offset: 0x004F51B4
		internal PlayerDrawLayerSlot(PlayerDrawLayer layer, PlayerDrawLayer.Multiple.Condition cond, int slot)
		{
			this.Layer = layer;
			this.Condition = cond;
			this._slot = slot;
			base.AddChildAfter(this.Layer);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x004F6FDD File Offset: 0x004F51DD
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x004F6FE4 File Offset: 0x004F51E4
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x004F6FE6 File Offset: 0x004F51E6
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			return this.Condition(drawInfo);
		}

		// Token: 0x040017E1 RID: 6113
		private readonly int _slot;
	}
}
