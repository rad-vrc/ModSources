using System;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x020002A1 RID: 673
	internal class GetLoadableTypesException : Exception
	{
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x0052AC73 File Offset: 0x00528E73
		public override string HelpLink
		{
			get
			{
				return "https://github.com/tModLoader/tModLoader/wiki/Expert-Cross-Mod-Content#extendsfrommod";
			}
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x0052AC7A File Offset: 0x00528E7A
		public GetLoadableTypesException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
