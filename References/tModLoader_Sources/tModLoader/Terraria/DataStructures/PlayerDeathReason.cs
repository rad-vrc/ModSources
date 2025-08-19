using System;
using System.IO;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Terraria.DataStructures
{
	// Token: 0x02000723 RID: 1827
	public class PlayerDeathReason
	{
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06004A05 RID: 18949 RVA: 0x0064ED06 File Offset: 0x0064CF06
		private int _sourceItemType
		{
			get
			{
				Item sourceItem = this._sourceItem;
				if (sourceItem == null)
				{
					return 0;
				}
				return sourceItem.type;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06004A06 RID: 18950 RVA: 0x0064ED19 File Offset: 0x0064CF19
		private int _sourceItemPrefix
		{
			get
			{
				Item sourceItem = this._sourceItem;
				if (sourceItem == null)
				{
					return 0;
				}
				return sourceItem.prefix;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06004A07 RID: 18951 RVA: 0x0064ED2C File Offset: 0x0064CF2C
		public ref int SourcePlayerIndex
		{
			get
			{
				return ref this._sourcePlayerIndex;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06004A08 RID: 18952 RVA: 0x0064ED34 File Offset: 0x0064CF34
		public ref int SourceNPCIndex
		{
			get
			{
				return ref this._sourceNPCIndex;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06004A09 RID: 18953 RVA: 0x0064ED3C File Offset: 0x0064CF3C
		public ref int SourceProjectileLocalIndex
		{
			get
			{
				return ref this._sourceProjectileLocalIndex;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06004A0A RID: 18954 RVA: 0x0064ED44 File Offset: 0x0064CF44
		public ref int SourceOtherIndex
		{
			get
			{
				return ref this._sourceOtherIndex;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06004A0B RID: 18955 RVA: 0x0064ED4C File Offset: 0x0064CF4C
		public ref int SourceProjectileType
		{
			get
			{
				return ref this._sourceProjectileType;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06004A0C RID: 18956 RVA: 0x0064ED54 File Offset: 0x0064CF54
		public ref Item SourceItem
		{
			get
			{
				return ref this._sourceItem;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06004A0D RID: 18957 RVA: 0x0064ED5C File Offset: 0x0064CF5C
		[Obsolete("CustomReason should be used instead")]
		public ref string SourceCustomReason
		{
			get
			{
				return ref this._sourceCustomReason;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06004A0E RID: 18958 RVA: 0x0064ED64 File Offset: 0x0064CF64
		public ref NetworkText CustomReason
		{
			get
			{
				return ref this._customReason;
			}
		}

		/// <summary>
		/// Only safe for use when the local player is the one taking damage! <br />
		/// Projectile ids are not synchronized across clients, and NPCs may have despawned/died by the time a strike/death packet arrives. <br />
		/// Just because the method returns true, doesn't mean that the _correct_ NPC/Projectile is returned on remote clients.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		// Token: 0x06004A0F RID: 18959 RVA: 0x0064ED6C File Offset: 0x0064CF6C
		public bool TryGetCausingEntity(out Entity entity)
		{
			entity = null;
			if (Main.npc.IndexInRange(this._sourceNPCIndex))
			{
				entity = Main.npc[this._sourceNPCIndex];
				return true;
			}
			if (Main.projectile.IndexInRange(this._sourceProjectileLocalIndex))
			{
				entity = Main.projectile[this._sourceProjectileLocalIndex];
				if (Main.player.IndexInRange(this._sourcePlayerIndex) && (this._sourcePlayerIndex != Main.myPlayer || ((Projectile)entity).owner != this._sourcePlayerIndex))
				{
					entity = Main.player[this._sourcePlayerIndex];
				}
				return true;
			}
			if (Main.player.IndexInRange(this._sourcePlayerIndex))
			{
				entity = Main.player[this._sourcePlayerIndex];
				return true;
			}
			return false;
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x0064EE24 File Offset: 0x0064D024
		public static PlayerDeathReason LegacyEmpty()
		{
			return new PlayerDeathReason
			{
				_sourceOtherIndex = 254
			};
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x0064EE36 File Offset: 0x0064D036
		public static PlayerDeathReason LegacyDefault()
		{
			return new PlayerDeathReason
			{
				_sourceOtherIndex = 255
			};
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x0064EE48 File Offset: 0x0064D048
		public static PlayerDeathReason ByNPC(int index)
		{
			return new PlayerDeathReason
			{
				_sourceNPCIndex = index
			};
		}

		/// <summary>
		/// A custom death message.
		/// <para /> This overload does not support localization, all players will see the same text regardless of their language settings. Use <see cref="M:Terraria.DataStructures.PlayerDeathReason.ByCustomReason(Terraria.Localization.NetworkText)" /> instead for localization support.
		/// </summary>
		// Token: 0x06004A13 RID: 18963 RVA: 0x0064EE56 File Offset: 0x0064D056
		[Obsolete("Use the NetworkText version instead")]
		public static PlayerDeathReason ByCustomReason(string reasonInEnglish)
		{
			return new PlayerDeathReason
			{
				_sourceCustomReason = reasonInEnglish
			};
		}

		/// <summary>
		/// A localized custom death message. Each client will see the death message in their own language, unlike with <see cref="M:Terraria.DataStructures.PlayerDeathReason.ByCustomReason(System.String)" />.
		/// <para /> Text substitutions can be provided by using <see cref="M:Terraria.Localization.LocalizedText.ToNetworkText(System.Object[])" /> or <see cref="M:Terraria.Localization.NetworkText.FromKey(System.String,System.Object[])" />.
		/// </summary>
		// Token: 0x06004A14 RID: 18964 RVA: 0x0064EE64 File Offset: 0x0064D064
		public static PlayerDeathReason ByCustomReason(NetworkText reason)
		{
			return new PlayerDeathReason
			{
				_customReason = reason
			};
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x0064EE72 File Offset: 0x0064D072
		public static PlayerDeathReason ByPlayerItem(int index, Item item)
		{
			return new PlayerDeathReason
			{
				_sourcePlayerIndex = index,
				_sourceItem = item
			};
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x0064EE87 File Offset: 0x0064D087
		public static PlayerDeathReason ByOther(int type, int playerIndex = -1)
		{
			return new PlayerDeathReason
			{
				_sourcePlayerIndex = playerIndex,
				_sourceOtherIndex = type
			};
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x0064EE9C File Offset: 0x0064D09C
		public static PlayerDeathReason ByProjectile(int playerIndex, int projectileIndex)
		{
			return new PlayerDeathReason
			{
				_sourcePlayerIndex = playerIndex,
				_sourceProjectileLocalIndex = projectileIndex,
				_sourceProjectileType = Main.projectile[projectileIndex].type
			};
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x0064EEC4 File Offset: 0x0064D0C4
		public NetworkText GetDeathText(string deadPlayerName)
		{
			if (this._customReason != null)
			{
				return this._customReason;
			}
			if (this._sourceCustomReason != null)
			{
				return NetworkText.FromLiteral(this._sourceCustomReason);
			}
			return Lang.CreateDeathMessage(deadPlayerName, this._sourcePlayerIndex, this._sourceNPCIndex, this._sourceProjectileLocalIndex, this._sourceOtherIndex, this._sourceProjectileType, this._sourceItemType);
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x0064EF20 File Offset: 0x0064D120
		public void WriteSelfTo(BinaryWriter writer)
		{
			BitsByte bitsByte = 0;
			bitsByte[0] = (this._sourcePlayerIndex != -1);
			bitsByte[1] = (this._sourceNPCIndex != -1);
			bitsByte[2] = (this._sourceProjectileLocalIndex != -1);
			bitsByte[3] = (this._sourceOtherIndex != -1);
			bitsByte[4] = (this._sourceProjectileType != 0);
			bitsByte[5] = (this._sourceItemType != 0);
			bitsByte[6] = (this._customReason != null);
			bitsByte[7] = (this._sourceCustomReason != null);
			writer.Write(bitsByte);
			if (bitsByte[0])
			{
				writer.Write((short)this._sourcePlayerIndex);
			}
			if (bitsByte[1])
			{
				writer.Write((short)this._sourceNPCIndex);
			}
			if (bitsByte[2])
			{
				writer.Write((short)this._sourceProjectileLocalIndex);
			}
			if (bitsByte[3])
			{
				writer.Write((byte)this._sourceOtherIndex);
			}
			if (bitsByte[4])
			{
				writer.Write((short)this._sourceProjectileType);
			}
			if (bitsByte[5])
			{
				ItemIO.Send(this._sourceItem, writer, false, false);
			}
			if (bitsByte[6])
			{
				this._customReason.Serialize(writer);
			}
			if (bitsByte[7])
			{
				writer.Write(this._sourceCustomReason);
			}
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x0064F08C File Offset: 0x0064D28C
		public static PlayerDeathReason FromReader(BinaryReader reader)
		{
			PlayerDeathReason playerDeathReason = new PlayerDeathReason();
			BitsByte bitsByte = reader.ReadByte();
			if (bitsByte[0])
			{
				playerDeathReason._sourcePlayerIndex = (int)reader.ReadInt16();
			}
			if (bitsByte[1])
			{
				playerDeathReason._sourceNPCIndex = (int)reader.ReadInt16();
			}
			if (bitsByte[2])
			{
				playerDeathReason._sourceProjectileLocalIndex = (int)reader.ReadInt16();
			}
			if (bitsByte[3])
			{
				playerDeathReason._sourceOtherIndex = (int)reader.ReadByte();
			}
			if (bitsByte[4])
			{
				playerDeathReason._sourceProjectileType = (int)reader.ReadInt16();
			}
			if (bitsByte[5])
			{
				playerDeathReason._sourceItem = ItemIO.Receive(reader, false, false);
			}
			if (bitsByte[6])
			{
				playerDeathReason._customReason = NetworkText.Deserialize(reader);
			}
			if (bitsByte[7])
			{
				playerDeathReason._sourceCustomReason = reader.ReadString();
			}
			return playerDeathReason;
		}

		// Token: 0x04005F29 RID: 24361
		private int _sourcePlayerIndex = -1;

		// Token: 0x04005F2A RID: 24362
		private int _sourceNPCIndex = -1;

		// Token: 0x04005F2B RID: 24363
		private int _sourceProjectileLocalIndex = -1;

		// Token: 0x04005F2C RID: 24364
		private int _sourceOtherIndex = -1;

		// Token: 0x04005F2D RID: 24365
		private int _sourceProjectileType;

		// Token: 0x04005F2E RID: 24366
		private Item _sourceItem;

		// Token: 0x04005F2F RID: 24367
		private string _sourceCustomReason;

		// Token: 0x04005F30 RID: 24368
		private NetworkText _customReason;
	}
}
