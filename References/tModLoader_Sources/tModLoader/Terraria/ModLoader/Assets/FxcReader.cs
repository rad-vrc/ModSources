using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Content.Readers;

namespace Terraria.ModLoader.Assets
{
	// Token: 0x020003C5 RID: 965
	public class FxcReader : IAssetReader
	{
		// Token: 0x0600331E RID: 13086 RVA: 0x00549035 File Offset: 0x00547235
		public FxcReader(GraphicsDevice graphicsDevice)
		{
			this._graphicsDevice = graphicsDevice;
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x00549044 File Offset: 0x00547244
		public ValueTask<T> FromStream<T>(Stream stream, MainThreadCreationContext mainThreadCtx) where T : class
		{
			FxcReader.<FromStream>d__2<T> <FromStream>d__;
			<FromStream>d__.<>t__builder = AsyncValueTaskMethodBuilder<T>.Create();
			<FromStream>d__.<>4__this = this;
			<FromStream>d__.stream = stream;
			<FromStream>d__.mainThreadCtx = mainThreadCtx;
			<FromStream>d__.<>1__state = -1;
			<FromStream>d__.<>t__builder.Start<FxcReader.<FromStream>d__2<T>>(ref <FromStream>d__);
			return <FromStream>d__.<>t__builder.Task;
		}

		// Token: 0x04001DF4 RID: 7668
		private readonly GraphicsDevice _graphicsDevice;
	}
}
