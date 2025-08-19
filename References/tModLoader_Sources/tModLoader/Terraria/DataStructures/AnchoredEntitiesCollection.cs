using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x020006D0 RID: 1744
	public class AnchoredEntitiesCollection
	{
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060048FD RID: 18685 RVA: 0x0064C301 File Offset: 0x0064A501
		public int AnchoredPlayersAmount
		{
			get
			{
				return this._anchoredPlayers.Count;
			}
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x0064C30E File Offset: 0x0064A50E
		public AnchoredEntitiesCollection()
		{
			this._anchoredNPCs = new List<AnchoredEntitiesCollection.IndexPointPair>();
			this._anchoredPlayers = new List<AnchoredEntitiesCollection.IndexPointPair>();
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x0064C32C File Offset: 0x0064A52C
		public void ClearNPCAnchors()
		{
			this._anchoredNPCs.Clear();
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x0064C339 File Offset: 0x0064A539
		public void ClearPlayerAnchors()
		{
			this._anchoredPlayers.Clear();
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x0064C348 File Offset: 0x0064A548
		public void AddNPC(int npcIndex, Point coords)
		{
			this._anchoredNPCs.Add(new AnchoredEntitiesCollection.IndexPointPair
			{
				index = npcIndex,
				coords = coords
			});
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x0064C379 File Offset: 0x0064A579
		public int GetNextPlayerStackIndexInCoords(Point coords)
		{
			return this.GetEntitiesInCoords(coords);
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x0064C384 File Offset: 0x0064A584
		public void AddPlayerAndGetItsStackedIndexInCoords(int playerIndex, Point coords, out int stackedIndexInCoords)
		{
			stackedIndexInCoords = this.GetEntitiesInCoords(coords);
			this._anchoredPlayers.Add(new AnchoredEntitiesCollection.IndexPointPair
			{
				index = playerIndex,
				coords = coords
			});
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x0064C3C0 File Offset: 0x0064A5C0
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

		// Token: 0x04005E52 RID: 24146
		private List<AnchoredEntitiesCollection.IndexPointPair> _anchoredNPCs;

		// Token: 0x04005E53 RID: 24147
		private List<AnchoredEntitiesCollection.IndexPointPair> _anchoredPlayers;

		// Token: 0x02000D59 RID: 3417
		private struct IndexPointPair
		{
			// Token: 0x04007B82 RID: 31618
			public int index;

			// Token: 0x04007B83 RID: 31619
			public Point coords;
		}
	}
}
