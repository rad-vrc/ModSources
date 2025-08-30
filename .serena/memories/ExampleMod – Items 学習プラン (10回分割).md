# ExampleMod – Items 学習プラン (10回分割)

対象: ExampleMod/Content/Items 配下。構成一覧:
- Accessories/
- Ammo/
- Armor/
- Consumables/
- Mounts/
- Placeable/
- Tools/
- Weapons/
- 単体ファイル: ActiveSoundShowcase.cs, CameraModifierShowcase.cs, CustomItemDrawingShowcase.cs(+textures), CustomItemSets.cs, ExampleDataItem.cs, ExampleDye.cs(+png), ExampleGolfBall.cs(+png), ExampleHairDye.cs(+png), ExampleInstancedItem.cs, ExampleItem.cs(+png), ExampleOnBuyItem.cs(+png), ExamplePaperAirplane.cs(+png), ExampleQuestFish.cs(+png), ExampleResearchPresent.cs(+png), ExampleResourcePickup.cs(+png), ExampleSignItem.cs(+png), ExampleSoul.cs(+png), ExampleStackableDurabilityItem.cs(+png), ExampleTooltipsItem.cs(+png), HoldStyleShowcase.cs, ShimmerShowcase.cs, UseStyleShowcase.cs

分割案（各回の狙い）
1) 基礎と共通パターン: ExampleItem/Tooltips/Instanced/OnBuy/Data/ResourcePickup/Sign/StackableDurability
2) 表示・描画: CustomItemDrawingShowcase/HoldStyleShowcase/UseStyleShowcase/ShimmerShowcase/ActiveSoundShowcase/CameraModifierShowcase
3) 染料・髪染料: ExampleDye/ExampleHairDye（テクスチャ連動・Shader適用）
4) アクセサリ: Accessories/（効果付与、UpdateAccessory/UpdateEquip、vanity）
5) 弾薬: Ammo/
6) 防具: Armor/（セット効果、装備能力、見た目）
7) 消費: Consumables/（ポーション/釣り/召喚/研究）
8) 設置: Placeable/（Tiles連携、家具、旗、看板）
9) 道具: Tools/（ツール系AI、ハンマー/ピッケル/フック等）
10) 武器: Weapons/（近接/飛び道具/魔法/召喚、チャネリング、チャージ、リロード）

各回: ファイルをまとめて読み込み→要点/フック/プロパティ・セット/レシピ/描画/ネット同期・保存を要約→Serenaメモ保存。