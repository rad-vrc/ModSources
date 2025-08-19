using System;
using System.Collections.Generic;
using Terraria.IO;

namespace Terraria.Social.Base
{
	// Token: 0x020000FA RID: 250
	public abstract class CloudSocialModule : ISocialModule
	{
		// Token: 0x060018B7 RID: 6327 RVA: 0x004BE2F6 File Offset: 0x004BC4F6
		public virtual void BindTo(Preferences preferences)
		{
			preferences.OnSave += this.Configuration_OnSave;
			preferences.OnLoad += this.Configuration_OnLoad;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x004BE31C File Offset: 0x004BC51C
		private void Configuration_OnLoad(Preferences preferences)
		{
			this.EnabledByDefault = preferences.Get<bool>("CloudSavingDefault", false);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x004BE330 File Offset: 0x004BC530
		private void Configuration_OnSave(Preferences preferences)
		{
			preferences.Put("CloudSavingDefault", this.EnabledByDefault);
		}

		// Token: 0x060018BA RID: 6330
		public abstract void Initialize();

		// Token: 0x060018BB RID: 6331
		public abstract void Shutdown();

		// Token: 0x060018BC RID: 6332
		public abstract IEnumerable<string> GetFiles();

		// Token: 0x060018BD RID: 6333
		public abstract bool Write(string path, byte[] data, int length);

		// Token: 0x060018BE RID: 6334
		public abstract void Read(string path, byte[] buffer, int length);

		// Token: 0x060018BF RID: 6335
		public abstract bool HasFile(string path);

		// Token: 0x060018C0 RID: 6336
		public abstract int GetFileSize(string path);

		// Token: 0x060018C1 RID: 6337
		public abstract bool Delete(string path);

		// Token: 0x060018C2 RID: 6338
		public abstract bool Forget(string path);

		// Token: 0x060018C3 RID: 6339 RVA: 0x004BE348 File Offset: 0x004BC548
		public byte[] Read(string path)
		{
			byte[] array = new byte[this.GetFileSize(path)];
			this.Read(path, array, array.Length);
			return array;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x004BE36E File Offset: 0x004BC56E
		public void Read(string path, byte[] buffer)
		{
			this.Read(path, buffer, buffer.Length);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x004BE37B File Offset: 0x004BC57B
		public bool Write(string path, byte[] data)
		{
			return this.Write(path, data, data.Length);
		}

		// Token: 0x04001386 RID: 4998
		public bool EnabledByDefault;
	}
}
