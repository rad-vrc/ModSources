using System;
using Terraria.ModLoader;

namespace Terraria.DataStructures
{
	// Token: 0x0200073F RID: 1855
	[Autoload(false)]
	internal sealed class VanillaPlayerDrawTransform : PlayerDrawLayer.Transformation
	{
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06004B5D RID: 19293 RVA: 0x00669FEC File Offset: 0x006681EC
		public override PlayerDrawLayer.Transformation Parent
		{
			get
			{
				return this._parent;
			}
		}

		/// <summary> Creates a LegacyPlayerLayer with the given mod name, identifier name, and drawing action. </summary>
		// Token: 0x06004B5E RID: 19294 RVA: 0x00669FF4 File Offset: 0x006681F4
		public VanillaPlayerDrawTransform(VanillaPlayerDrawTransform.LayerFunction preDraw, VanillaPlayerDrawTransform.LayerFunction postDraw, PlayerDrawLayer.Transformation parent = null)
		{
			this.PreDrawFunc = preDraw;
			this.PostDrawFunc = postDraw;
			this._parent = parent;
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x0066A011 File Offset: 0x00668211
		protected override void PreDraw(ref PlayerDrawSet drawInfo)
		{
			this.PreDrawFunc(ref drawInfo);
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x0066A01F File Offset: 0x0066821F
		protected override void PostDraw(ref PlayerDrawSet drawInfo)
		{
			this.PostDrawFunc(ref drawInfo);
		}

		// Token: 0x04006071 RID: 24689
		private readonly VanillaPlayerDrawTransform.LayerFunction PreDrawFunc;

		// Token: 0x04006072 RID: 24690
		private readonly VanillaPlayerDrawTransform.LayerFunction PostDrawFunc;

		// Token: 0x04006073 RID: 24691
		private PlayerDrawLayer.Transformation _parent;

		/// <summary> The delegate of this method, which can either do the actual drawing or add draw data, depending on what kind of layer this is. </summary>
		// Token: 0x02000D66 RID: 3430
		// (Invoke) Token: 0x06006407 RID: 25607
		public delegate void LayerFunction(ref PlayerDrawSet info);
	}
}
