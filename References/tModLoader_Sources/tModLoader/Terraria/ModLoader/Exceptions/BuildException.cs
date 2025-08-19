using System;
using System.CodeDom.Compiler;
using Terraria.ModLoader.Engine;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x0200029E RID: 670
	internal class BuildException : Exception
	{
		// Token: 0x06002CDC RID: 11484 RVA: 0x0052AC14 File Offset: 0x00528E14
		public BuildException(string message, ErrorReporting.TMLErrorCode errorCode = ErrorReporting.TMLErrorCode.TML002) : base(message)
		{
			this.errorCode = errorCode;
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x0052AC2B File Offset: 0x00528E2B
		public BuildException(string message, Exception innerException, ErrorReporting.TMLErrorCode errorCode = ErrorReporting.TMLErrorCode.TML002) : base(message, innerException)
		{
			this.errorCode = errorCode;
		}

		// Token: 0x04001C10 RID: 7184
		public CompilerErrorCollection compileErrors;

		// Token: 0x04001C11 RID: 7185
		public ErrorReporting.TMLErrorCode errorCode = ErrorReporting.TMLErrorCode.TML002;
	}
}
