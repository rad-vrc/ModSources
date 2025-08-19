using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Content.Readers;

namespace Terraria.ModLoader.Assets
{
	// Token: 0x020003C8 RID: 968
	public class RawImgReader : IAssetReader
	{
		// Token: 0x06003325 RID: 13093 RVA: 0x0054921C File Offset: 0x0054741C
		public RawImgReader(GraphicsDevice graphicsDevice)
		{
			this._graphicsDevice = graphicsDevice;
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x0054922C File Offset: 0x0054742C
		public ValueTask<T> FromStream<T>(Stream stream, MainThreadCreationContext mainThreadCtx) where T : class
		{
			RawImgReader.<FromStream>d__2<T> <FromStream>d__;
			<FromStream>d__.<>t__builder = AsyncValueTaskMethodBuilder<T>.Create();
			<FromStream>d__.<>4__this = this;
			<FromStream>d__.stream = stream;
			<FromStream>d__.mainThreadCtx = mainThreadCtx;
			<FromStream>d__.<>1__state = -1;
			<FromStream>d__.<>t__builder.Start<RawImgReader.<FromStream>d__2<T>>(ref <FromStream>d__);
			return <FromStream>d__.<>t__builder.Task;
		}

		// Token: 0x04001DF5 RID: 7669
		private readonly GraphicsDevice _graphicsDevice;
	}
}
