# Pet / Light Pet / Town Pet ベストプラクティス（tModLoader 1.4）

本書は ExampleMod と `References/tModLoader.wiki_en/` の知見を元に、Vanity Pet（見た目ペット）、Light Pet（光源ペット）、Town Pet（町ペット）の実装要点を日本語で簡潔にまとめたガイド。新規実装時のチェックリストとしても使える。

---

## 対象と用語

- **Vanity Pet（見た目ペット）**: プレイヤーに追従する投射体（Projectile）。UIのペット枠に装備。`Main.vanityPet[buffType] = true`
- **Light Pet（光源ペット）**: 光を発するペット。`Main.lightPet[buffType] = true` と `ProjectileID.Sets.LightPet[projType] = true`
- **Town Pet（タウンペット）**: 町に住む NPC。`NPCID.Sets.IsTownPet[npcType] = true`（Happiness は対象外）
- （参考）Minion は召喚火力枠（本書対象外）

---

## 共通アーキテクチャ

- **Buff**: カテゴリ（Vanity/Light）を宣言し、召喚維持を担当
- **Item**: 使用時に Buff を付与（`Item.buffType`）。Vanity/Light は `Item.DefaultToVanitypet(projType, buffType)` が簡便
- **実体**: Vanity/Light は `ModProjectile`、Town Pet は `ModNPC`

---

## Vanity Pet 実装

- Buff（`ModBuff`）
  - `Main.vanityPet[Type] = true`
  - `Update` 内で「未召喚なら生成＋時間維持」

    ```csharp
    public override void Update(Player player, ref int buffIndex) {
        bool unused = false;
        player.BuffHandle_SpawnPetIfNeededAndSetTime(
            buffIndex, ref unused,
            ModContent.ProjectileType<MyVanityPetProj>());
    }
    ```

- Item（`ModItem`）
  - `Item.DefaultToVanitypet(ProjType, BuffType)` 推奨（装備スロット/並び順を自動整合）
- Projectile（`ModProjectile`）
  - `Main.projPet[Projectile.type] = true`
  - Buff 所持中は `Projectile.timeLeft = 2` を維持（消滅防止）
  - 移動・描画は通常の Projectile と同様。必要に応じて `Projectile.rotation`／`spriteDirection`／アフターイメージ等（参照: Basic-Projectile）

### よくある落とし穴（Vanity）

- Buff を付与するだけで実体を生成しない
- `Main.projPet` 未設定で挙動が不安定

---

## Light Pet 実装（Vanityとの差分）

- Buff: `Main.lightPet[Type] = true`
- Projectile: `ProjectileID.Sets.LightPet[Projectile.type] = true`
- 視覚効果: `Lighting.AddLight(Projectile.Center, r, g, b)` 等で光源を追加
- Item: `DefaultToVanitypet` の利用で問題なし（装備枠は Light Pet 枠に入る）

### 注意点（Light）

- Vanity と Light は原則同時 1 体ずつ。テスト時は Buff の付与順で置換されることに留意
- 過度な `AddLight` 呼び出しや重い描画はパフォーマンス低下の原因

---

## Town Pet 実装

- SetStaticDefaults
  - `NPCID.Sets.IsTownPet[Type] = true`
  - 多くの Town ペットは受動 AI: `NPC.aiStyle = NPCAIStyleID.Passive`
  - 必要に応じた各種セット:
    - 撫で距離/小型フラグ: `NPCID.Sets.PlayerDistanceWhilePetting[Type]` / `NPCID.Sets.IsPetSmallForPetting[Type]`
    - 椅子に座れる: `NPCID.Sets.CannotSitOnFurniture[Type] = false`
    - 帽子・表情泡の基準: `NPCID.Sets.NPCFramingGroup[Type]`
    - 特定デバフ免疫例: `NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Shimmer] = true`
- SetDefaults
  - `NPC.townNPC = true`、`NPC.housingCategory = 1`（住居共有可）
  - 既存タウン系の `AnimationType` を参照（例: `NPCID.TownBunny`）
- 表示プロファイル（`ITownNPCProfile`）
  - `GetTextureNPCShouldUse` / `GetHeadTextureIndex` で外観切替
  - アセット命名例: 本体 `.../NpcName.png`、頭 `.../NpcName_Head.png`
- 見た目・位置補正
  - 椅子座り時の `DrawOffsetY`／`ChatBubblePosition`／`PartyHatPosition` を必要に応じて調整
- ローカライズ
  - 図鑑、会話、表示名、名前候補を `Localization` で定義
  - 名前候補の拡張読み込み: `Language.FindAll(Lang.CreateDialogFilter(prefix))`
- アンロック／セーブ
  - 例: 「免許」アイテム→`ModSystem` でフラグ保存（`Save/Load`・`NetSend/NetReceive`）→ 在籍確認・交換

### よくある落とし穴（Town）

- `IsTownPet` だけではなく **受動 AI（Passive）** を設定しないと挙動が乱れるケース
- Happiness 設定は不要（Town Pet は計算対象外）
- 帽子/吹き出し/撫で位置の未調整で見た目破綻

---

## アセットとロード

- 画像命名: `NpcName.png`／`NpcName_Head.png`（Town）、Pet は通常 Projectile テクスチャ
- 追加バリアントは `ITownNPCProfile` で切替
- テクスチャ読み込みは `ModContent.Request<Texture2D>(path)`（原則非同期ロード）

---

## ネットワーク／セーブ同期

- Pet（Projectile）: 通常は Buff 駆動で同期の追加実装は不要
- Town Pet の解禁状態・カスタムフラグ: `ModSystem` の `Save/Load` と `NetSend/NetReceive` を実装
- 装備スロット同期はバニラが処理（`MessageID.SyncEquipment`。`player.miscEquips` の Pet/Light Pet スロット等）

---

## 実装スニペット集（抜粋）

- Vanity/Light 共通 Item 既定

  ```csharp
  public override void SetDefaults() {
      Item.DefaultToVanitypet(
          ModContent.ProjectileType<MyPetProj>(),
          ModContent.BuffType<MyPetBuff>());
  }
  ```

- Light での発光

  ```csharp
  public override void AI() {
      Lighting.AddLight(Projectile.Center, 0.9f, 0.1f, 0.3f);
  }
  ```

- Town Pet 既定

  ```csharp
  public override void SetStaticDefaults() {
      NPCID.Sets.IsTownPet[Type] = true;
      NPCID.Sets.PlayerDistanceWhilePetting[Type] = 32;
      NPCID.Sets.IsPetSmallForPetting[Type] = true;
  }
  public override void SetDefaults() {
      NPC.townNPC = true;
      NPC.aiStyle = NPCAIStyleID.Passive;
      NPC.housingCategory = 1;
  }
  ```

---

## 導入チェックリスト

- Vanity: `Main.vanityPet`、`DefaultToVanitypet`、`Main.projPet`、`timeLeft = 2` を維持
- Light: 上記に加え `Main.lightPet` と `ProjectileID.Sets.LightPet`、必要なら `Lighting.AddLight`
- Town: `IsTownPet`、`Passive` AI、住居カテゴリ、プロフィール・ローカライズ、撫で/帽子/吹き出し位置補正
- 保存/同期が必要なフラグは `ModSystem` で一元管理

---

## 参考（読み物）

- ExampleMod
  - `Content/Pets/ExamplePet`（Vanity）
  - `Content/Pets/ExampleLightPet`（Light）
  - `Content/NPCs/TownPets/ExampleTownPet`（Town Pet）
  - `Content/Items/Consumables/ExampleTownPetLicense`、`Common/Systems/ExampleTownPetSystem`
- tModLoader wiki（`References/tModLoader.wiki_en/`）
  - Basic-Projectile / Basic-Minion-Guide（AI・描画・ライト）
  - NetMessage-Class-Documentation（`SyncEquipment` と `player.miscEquips`）
  - Localization / Update-Migration-Guide（名前候補・撫で距離等）

以上。
