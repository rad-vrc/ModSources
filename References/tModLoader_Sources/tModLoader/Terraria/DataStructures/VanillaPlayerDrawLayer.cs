using System;
using Terraria.ModLoader;

namespace Terraria.DataStructures
{
	// Token: 0x0200073E RID: 1854
	[Autoload(false)]
	internal sealed class VanillaPlayerDrawLayer : PlayerDrawLayer
	{
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06004B56 RID: 19286 RVA: 0x00669F11 File Offset: 0x00668111
		public override PlayerDrawLayer.Transformation Transform
		{
			get
			{
				return this._transform;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06004B57 RID: 19287 RVA: 0x00669F19 File Offset: 0x00668119
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06004B58 RID: 19288 RVA: 0x00669F21 File Offset: 0x00668121
		public override bool IsHeadLayer
		{
			get
			{
				return this._isHeadLayer;
			}
		}

		/// <summary> Creates a LegacyPlayerLayer with the given mod name, identifier name, and drawing action. </summary>
		// Token: 0x06004B59 RID: 19289 RVA: 0x00669F29 File Offset: 0x00668129
		public VanillaPlayerDrawLayer(string name, VanillaPlayerDrawLayer.DrawFunc drawFunc, PlayerDrawLayer.Transformation transform = null, bool isHeadLayer = false, VanillaPlayerDrawLayer.Condition condition = null, PlayerDrawLayer.Position position = null)
		{
			this._name = name;
			this.drawFunc = drawFunc;
			this.condition = condition;
			this.position = position;
			this._transform = transform;
			this._isHeadLayer = isHeadLayer;
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x00669F5E File Offset: 0x0066815E
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			VanillaPlayerDrawLayer.Condition condition = this.condition;
			return condition == null || condition(drawInfo);
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x00669F74 File Offset: 0x00668174
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			if (this.position != null)
			{
				return this.position;
			}
			int index = 0;
			while (PlayerDrawLayers.FixedVanillaLayers[index] != this)
			{
				index++;
			}
			return new PlayerDrawLayer.Between((index > 0) ? PlayerDrawLayers.FixedVanillaLayers[index - 1] : null, (index < PlayerDrawLayers.FixedVanillaLayers.Count - 1) ? PlayerDrawLayers.FixedVanillaLayers[index + 1] : null);
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x00669FDE File Offset: 0x006681DE
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			this.drawFunc(ref drawInfo);
		}

		// Token: 0x0400606B RID: 24683
		private readonly VanillaPlayerDrawLayer.DrawFunc drawFunc;

		// Token: 0x0400606C RID: 24684
		private readonly VanillaPlayerDrawLayer.Condition condition;

		// Token: 0x0400606D RID: 24685
		private PlayerDrawLayer.Transformation _transform;

		// Token: 0x0400606E RID: 24686
		private readonly string _name;

		// Token: 0x0400606F RID: 24687
		private readonly bool _isHeadLayer;

		// Token: 0x04006070 RID: 24688
		private readonly PlayerDrawLayer.Position position;

		/// <summary> The delegate of this method, which can either do the actual drawing or add draw data, depending on what kind of layer this is. </summary>
		// Token: 0x02000D64 RID: 3428
		// (Invoke) Token: 0x060063FF RID: 25599
		public delegate void DrawFunc(ref PlayerDrawSet info);

		/// <summary> The delegate of this method, which can either do the actual drawing or add draw data, depending on what kind of layer this is. </summary>
		// Token: 0x02000D65 RID: 3429
		// (Invoke) Token: 0x06006403 RID: 25603
		public delegate bool Condition(PlayerDrawSet info);
	}
}
