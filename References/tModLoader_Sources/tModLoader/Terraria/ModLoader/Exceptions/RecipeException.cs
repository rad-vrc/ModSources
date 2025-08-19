using System;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x020002A8 RID: 680
	public class RecipeException : Exception
	{
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x0052B364 File Offset: 0x00529564
		public override string HelpLink
		{
			get
			{
				return "https://github.com/tModLoader/tModLoader/wiki/Basic-Recipes";
			}
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x0052B36B File Offset: 0x0052956B
		public RecipeException()
		{
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x0052B373 File Offset: 0x00529573
		public RecipeException(string message) : base(message)
		{
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x0052B37C File Offset: 0x0052957C
		public RecipeException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
