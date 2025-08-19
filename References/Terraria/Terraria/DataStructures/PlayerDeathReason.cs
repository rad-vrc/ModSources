using System;
using System.IO;
using Terraria.Localization;

namespace Terraria.DataStructures
{
	// Token: 0x0200044E RID: 1102
	public class PlayerDeathReason
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x005A04A8 File Offset: 0x0059E6A8
		public int? SourceProjectileType
		{
			get
			{
				if (this._sourceProjectileLocalIndex == -1)
				{
					return null;
				}
				return new int?(this._sourceProjectileType);
			}
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x005A04D4 File Offset: 0x0059E6D4
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
				return true;
			}
			if (Main.player.IndexInRange(this._sourcePlayerIndex))
			{
				entity = Main.player[this._sourcePlayerIndex];
				return true;
			}
			return false;
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x005A054B File Offset: 0x0059E74B
		public static PlayerDeathReason LegacyEmpty()
		{
			return new PlayerDeathReason
			{
				_sourceOtherIndex = 254
			};
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x005A055D File Offset: 0x0059E75D
		public static PlayerDeathReason LegacyDefault()
		{
			return new PlayerDeathReason
			{
				_sourceOtherIndex = 255
			};
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x005A056F File Offset: 0x0059E76F
		public static PlayerDeathReason ByNPC(int index)
		{
			return new PlayerDeathReason
			{
				_sourceNPCIndex = index
			};
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x005A057D File Offset: 0x0059E77D
		public static PlayerDeathReason ByCustomReason(string reasonInEnglish)
		{
			return new PlayerDeathReason
			{
				_sourceCustomReason = reasonInEnglish
			};
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x005A058C File Offset: 0x0059E78C
		public static PlayerDeathReason ByPlayer(int index)
		{
			return new PlayerDeathReason
			{
				_sourcePlayerIndex = index,
				_sourceItemType = Main.player[index].inventory[Main.player[index].selectedItem].type,
				_sourceItemPrefix = (int)Main.player[index].inventory[Main.player[index].selectedItem].prefix
			};
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x005A05ED File Offset: 0x0059E7ED
		public static PlayerDeathReason ByOther(int type)
		{
			return new PlayerDeathReason
			{
				_sourceOtherIndex = type
			};
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x005A05FC File Offset: 0x0059E7FC
		public static PlayerDeathReason ByProjectile(int playerIndex, int projectileIndex)
		{
			PlayerDeathReason playerDeathReason = new PlayerDeathReason
			{
				_sourcePlayerIndex = playerIndex,
				_sourceProjectileLocalIndex = projectileIndex,
				_sourceProjectileType = Main.projectile[projectileIndex].type
			};
			if (playerIndex >= 0 && playerIndex <= 255)
			{
				playerDeathReason._sourceItemType = Main.player[playerIndex].inventory[Main.player[playerIndex].selectedItem].type;
				playerDeathReason._sourceItemPrefix = (int)Main.player[playerIndex].inventory[Main.player[playerIndex].selectedItem].prefix;
			}
			return playerDeathReason;
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x005A0684 File Offset: 0x0059E884
		public NetworkText GetDeathText(string deadPlayerName)
		{
			if (this._sourceCustomReason != null)
			{
				return NetworkText.FromLiteral(this._sourceCustomReason);
			}
			return Lang.CreateDeathMessage(deadPlayerName, this._sourcePlayerIndex, this._sourceNPCIndex, this._sourceProjectileLocalIndex, this._sourceOtherIndex, this._sourceProjectileType, this._sourceItemType);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x005A06C4 File Offset: 0x0059E8C4
		public void WriteSelfTo(BinaryWriter writer)
		{
			BitsByte bb = 0;
			bb[0] = (this._sourcePlayerIndex != -1);
			bb[1] = (this._sourceNPCIndex != -1);
			bb[2] = (this._sourceProjectileLocalIndex != -1);
			bb[3] = (this._sourceOtherIndex != -1);
			bb[4] = (this._sourceProjectileType != 0);
			bb[5] = (this._sourceItemType != 0);
			bb[6] = (this._sourceItemPrefix != 0);
			bb[7] = (this._sourceCustomReason != null);
			writer.Write(bb);
			if (bb[0])
			{
				writer.Write((short)this._sourcePlayerIndex);
			}
			if (bb[1])
			{
				writer.Write((short)this._sourceNPCIndex);
			}
			if (bb[2])
			{
				writer.Write((short)this._sourceProjectileLocalIndex);
			}
			if (bb[3])
			{
				writer.Write((byte)this._sourceOtherIndex);
			}
			if (bb[4])
			{
				writer.Write((short)this._sourceProjectileType);
			}
			if (bb[5])
			{
				writer.Write((short)this._sourceItemType);
			}
			if (bb[6])
			{
				writer.Write((byte)this._sourceItemPrefix);
			}
			if (bb[7])
			{
				writer.Write(this._sourceCustomReason);
			}
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x005A0830 File Offset: 0x0059EA30
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
				playerDeathReason._sourceItemType = (int)reader.ReadInt16();
			}
			if (bitsByte[6])
			{
				playerDeathReason._sourceItemPrefix = (int)reader.ReadByte();
			}
			if (bitsByte[7])
			{
				playerDeathReason._sourceCustomReason = reader.ReadString();
			}
			return playerDeathReason;
		}

		// Token: 0x04005021 RID: 20513
		private int _sourcePlayerIndex = -1;

		// Token: 0x04005022 RID: 20514
		private int _sourceNPCIndex = -1;

		// Token: 0x04005023 RID: 20515
		private int _sourceProjectileLocalIndex = -1;

		// Token: 0x04005024 RID: 20516
		private int _sourceOtherIndex = -1;

		// Token: 0x04005025 RID: 20517
		private int _sourceProjectileType;

		// Token: 0x04005026 RID: 20518
		private int _sourceItemType;

		// Token: 0x04005027 RID: 20519
		private int _sourceItemPrefix;

		// Token: 0x04005028 RID: 20520
		private string _sourceCustomReason;
	}
}
