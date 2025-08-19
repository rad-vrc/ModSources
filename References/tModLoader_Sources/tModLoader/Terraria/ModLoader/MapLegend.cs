using System;
using Terraria.Localization;
using Terraria.Map;

namespace Terraria.ModLoader
{
	// Token: 0x02000195 RID: 405
	public class MapLegend
	{
		// Token: 0x06001F40 RID: 8000 RVA: 0x004E0DAC File Offset: 0x004DEFAC
		public MapLegend(int size)
		{
			this.legend = new LocalizedText[size];
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x004E0DC0 File Offset: 0x004DEFC0
		public int Length
		{
			get
			{
				return this.legend.Length;
			}
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x004E0DCA File Offset: 0x004DEFCA
		internal void Resize(int newSize)
		{
			Array.Resize<LocalizedText>(ref this.legend, newSize);
		}

		// Token: 0x17000349 RID: 841
		public LocalizedText this[int i]
		{
			get
			{
				return this.legend[i];
			}
			set
			{
				this.legend[i] = value;
			}
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x004E0DED File Offset: 0x004DEFED
		public LocalizedText FromType(int type)
		{
			return this[MapHelper.TileToLookup(type, 0)];
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x004E0DFC File Offset: 0x004DEFFC
		public string FromTile(MapTile mapTile, int x, int y)
		{
			string name = this.legend[(int)mapTile.Type].Value;
			if (MapLoader.nameFuncs.ContainsKey(mapTile.Type))
			{
				name = MapLoader.nameFuncs[mapTile.Type](name, x, y);
			}
			return name;
		}

		// Token: 0x04001674 RID: 5748
		private LocalizedText[] legend;
	}
}
