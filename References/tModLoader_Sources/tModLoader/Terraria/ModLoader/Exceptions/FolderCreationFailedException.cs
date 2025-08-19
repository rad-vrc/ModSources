using System;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x020002A0 RID: 672
	internal class FolderCreationFailedException : Exception
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06002CDF RID: 11487 RVA: 0x0052AC62 File Offset: 0x00528E62
		public override string HelpLink
		{
			get
			{
				return "https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Usage-FAQ#systemunauthorizedaccessexception-access-to-the-path-is-denied";
			}
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x0052AC69 File Offset: 0x00528E69
		public FolderCreationFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
