# Items 学習 (4-of-10) Armor

目的: 防具・頭装備・特殊頭（TallHat）・コスチュームの実装パターン、セットボーナス、描画制御の要点整理

共通パターン
- [AutoloadEquip(EquipType.Head/Body/Legs)] と対応するテクスチャ（_Head/_Body/_Legs）
- SetDefaults: defense/value/rare ほか装備系の基本設定
- UpdateEquip: 個別装備効果（耐性、最大マナ/ミニオン、移動速度など）
- セットボーナス: IsArmorSet(head,body,legs) → UpdateArmorSet(player)
- 描画: ArmorIDs.Head.Sets.DrawHead/DrawHatHair/DrawFullHair/IsTallHat 等

各ファイル要点
- ExampleHelmet.cs（頭/セット1）
  - AdditiveGenericDamageBonus=20。SetBonusText をローカライズキーから取得し WithFormatArgs
  - IsArmorSet: 胴/脚が ExampleBreastplate/ExampleLeggings の時
  - UpdateArmorSet: player.setBonus=SetBonusText.Value、GetDamage(Generic)+=0.20
  - 髪の描画制御の例をコメントで示す

- ExampleHood.cs（頭/セット2 + 影演出）
  - ManaCostReductionPercent=10。SetBonusText.Format(Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses? "Key.UP":"Key.DOWN"))
  - UpdateArmorSet: manaCost 減少、ExampleArmorSetBonusPlayer.ExampleSetHood=true
  - ArmorSetShadows: ModPlayer.ShadowStyle に応じて armorEffectDrawShadow / DrawOutlines / DrawOutlinesForbidden

- ExampleBreastplate.cs（胴）
  - MaxManaIncrease=20, MaxMinionIncrease=1。UpdateEquip: OnFire免疫、マナ上限+20、ミニオン+1

- ExampleLeggings.cs（脚）
  - MoveSpeedBonus=5。UpdateEquip: moveSpeed+=0.05

- ExampleTallHelmet.cs（TallHat）
  - ArmorIDs.Head.Sets.DrawHatHair[headSlot]=true
  - ArmorIDs.Head.Sets.IsTallHat[headSlot]=true（ウィザードハットのような背高描画）

- ExampleCostume.cs（コスチューム/アクセサリーでEquipTexture差替）
  - 補足: Merfolk/Werewolf 的な置換の実演。通常の自動EquipTextureではなく手動登録
  - Load(): EquipLoader.AddEquipTexture を Head/Body/Legs × 通常/Alt 名で6回登録（サーバーでは未実行）
  - SetupDrawing(): GetEquipSlot でスロットを取得し、Body/Legs で素肌隠し、Head は DrawHead=false
  - SetDefaults: accessory=true, hasVanityEffects=true
  - UpdateAccessory/UpdateVanity: ExampleCostumePlayer のフラグ（BlockyAccessory/Hide/Force）を制御
  - BlockyHead(EquipTexture): IsVanitySet=true、UpdateVanitySet で Name によって異なる Dust を発生

実装チートシート
- セットボーナス: IsArmorSet → UpdateArmorSet（player.setBonus に文字列、効果はここで）
- 個別効果: UpdateEquip（耐性/上限/速度等）、バフ免疫は player.buffImmune[BuffID.X] = true
- TallHat: ArmorIDs.Head.Sets.IsTallHat[slot]=true、必要なら独自 PlayerDrawLayer
- 髪/肌表示: ArmorIDs.Head/Body/Legs の Sets.* を適宜設定
- コスチューム: EquipLoader.AddEquipTexture を用いた手動登録 + ModPlayer.FrameEffects でスロット差替（実装は Common/Players 側）

注意/落とし穴
- サーバーでは EquipTexture をロードしない（Load/SetupDrawing で netMode チェック）
- SetBonusText のプレースホルダは UpdateArmorSet 側で最終文字列に整形するパターンあり
- 影/発光演出は ArmorSetShadows で切替、演出ロジックは ModPlayer 側の状態に依存