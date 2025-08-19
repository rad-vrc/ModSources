using System;
using System.Collections.Generic;
using Terraria.IO;

namespace Terraria.Social.Base
{
	// Token: 0x02000190 RID: 400
	public abstract class CloudSocialModule : ISocialModule
	{
		// Token: 0x06001B37 RID: 6967 RVA: 0x004E6F7D File Offset: 0x004E517D
		public virtual void BindTo(Preferences preferences)
		{
			preferences.OnSave += this.Configuration_OnSave;
			preferences.OnLoad += this.Configuration_OnLoad;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x004E6FA3 File Offset: 0x004E51A3
		private void Configuration_OnLoad(Preferences preferences)
		{
			this.EnabledByDefault = preferences.Get<bool>("CloudSavingDefault", false);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x004E6FB7 File Offset: 0x004E51B7
		private void Configuration_OnSave(Preferences preferences)
		{
			preferences.Put("CloudSavingDefault", this.EnabledByDefault);
		}

		// Token: 0x06001B3A RID: 6970
		public abstract void Initialize();

		// Token: 0x06001B3B RID: 6971
		public abstract void Shutdown();

		// Token: 0x06001B3C RID: 6972
		public abstract IEnumerable<string> GetFiles();

		// Token: 0x06001B3D RID: 6973
		public abstract bool Write(string path, byte[] data, int length);

		// Token: 0x06001B3E RID: 6974
		public abstract void Read(string path, byte[] buffer, int length);

		// Token: 0x06001B3F RID: 6975
		public abstract bool HasFile(string path);

		// Token: 0x06001B40 RID: 6976
		public abstract int GetFileSize(string path);

		// Token: 0x06001B41 RID: 6977
		public abstract bool Delete(string path);

		// Token: 0x06001B42 RID: 6978
		public abstract bool Forget(string path);

		// Token: 0x06001B43 RID: 6979 RVA: 0x004E6FD0 File Offset: 0x004E51D0
		public byte[] Read(string path)
		{
			byte[] array = new byte[this.GetFileSize(path)];
			this.Read(path, array, array.Length);
			return array;
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x004E6FF6 File Offset: 0x004E51F6
		public void Read(string path, byte[] buffer)
		{
			this.Read(path, buffer, buffer.Length);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x004E7003 File Offset: 0x004E5203
		public bool Write(string path, byte[] data)
		{
			return this.Write(path, data, data.Length);
		}

		// Token: 0x04001607 RID: 5639
		public bool EnabledByDefault;
	}
}
