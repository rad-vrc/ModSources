using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200043E RID: 1086
	public class AnchoredEntitiesCollection
	{
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x0059E89E File Offset: 0x0059CA9E
		public int AnchoredPlayersAmount
		{
			get
			{
				return this._anchoredPlayers.Count;
			}
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x0059E8AB File Offset: 0x0059CAAB
		public AnchoredEntitiesCollection()
		{
			this._anchoredNPCs = new List<AnchoredEntitiesCollection.IndexPointPair>();
			this._anchoredPlayers = new List<AnchoredEntitiesCollection.IndexPointPair>();
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x0059E8C9 File Offset: 0x0059CAC9
		public void ClearNPCAnchors()
		{
			this._anchoredNPCs.Clear();
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x0059E8D6 File Offset: 0x0059CAD6
		public void ClearPlayerAnchors()
		{
			this._anchoredPlayers.Clear();
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x0059E8E4 File Offset: 0x0059CAE4
		public void AddNPC(int npcIndex, Point coords)
		{
			this._anchoredNPCs.Add(new AnchoredEntitiesCollection.IndexPointPair
			{
				index = npcIndex,
				coords = coords
			});
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x0059E915 File Offset: 0x0059CB15
		public int GetNextPlayerStackIndexInCoords(Point coords)
		{
			return this.GetEntitiesInCoords(coords);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x0059E920 File Offset: 0x0059CB20
		public void AddPlayerAndGetItsStackedIndexInCoords(int playerIndex, Point coords, out int stackedIndexInCoords)
		{
			stackedIndexInCoords = this.GetEntitiesInCoords(coords);
			this._anchoredPlayers.Add(new AnchoredEntitiesCollection.IndexPointPair
			{
				index = playerIndex,
				coords = coords
			});
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x0059E95C File Offset: 0x0059CB5C
		private int GetEntitiesInCoords(Point coords)
		{
			int num = 0;
			for (int i = 0; i < this._anchoredNPCs.Count; i++)
			{
				if (this._anchoredNPCs[i].coords == coords)
				{
					num++;
				}
			}
			for (int j = 0; j < this._anchoredPlayers.Count; j++)
			{
				if (this._anchoredPlayers[j].coords == coords)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x04004FC9 RID: 20425
		private List<AnchoredEntitiesCollection.IndexPointPair> _anchoredNPCs;

		// Token: 0x04004FCA RID: 20426
		private List<AnchoredEntitiesCollection.IndexPointPair> _anchoredPlayers;

		// Token: 0x0200077A RID: 1914
		private struct IndexPointPair
		{
			// Token: 0x04006497 RID: 25751
			public int index;

			// Token: 0x04006498 RID: 25752
			public Point coords;
		}
	}
}
