### ProjectileID ペット一覧（Vanity/Light）

目的: ペット系 Projectile を CloneDefaults/AIType のベースに選ぶ際の当たり付け用リスト。

- 注意
  - **LightPet**: `ProjectileID.Sets.LightPet[id] == true`
  - **Vanity/その他**: `Main.projPet[id] == true`（中にはミニオン系も含まれるので要注意）
  - 実装時は `Main.projPet`, `Main.lightPet`, `ProjectileID.Sets.LightPet` を正しく設定してください。

---

### Light Pet（ProjectileID.Sets.LightPet = true）

| ID | Name | 備考 |
|----|------|------|
| 18 | ShadowOrb | シャドウオーブ |
| 72 | BlueFairy | フェアリー（青） |
| 86 | PinkFairy | フェアリー（桃） |
| 87 | GreenFairy | フェアリー（緑） |
| 211 | Wisp | ウィスプ |
| 492 | MagicLantern | マジックランタン |
| 500 | CrimsonHeart | クリムゾンハート |
| 650 | SuspiciousTentacle | サスピシャス・テンタクル |
| 702 | DD2PetGhost | DD2 ゴースト |
| 891 | GolemPet | ボス系ペット（Light扱い） |
| 895 | FairyQueenPet | ボス系ペット（Light扱い） |
| 896 | PumpkingPet | ボス系ペット（Light扱い） |

---

### projPet 系（Main.projPet = true）

注: 下記にはミニオン系が混在します（例: Hornet, Spider 系, StormTiger 系など）。Vanity ペットのクローン用途ではミニオン系を避けてください。

| ID | Name | LightPet |
|----|------|----------|
| 111 | Bunny |  |
| 112 | Penguin |  |
| 127 | Turtle |  |
| 175 | BabyEater |  |
| 191 | Pygmy |  |
| 192 | Pygmy2 |  |
| 193 | Pygmy3 |  |
| 194 | Pygmy4 |  |
| 197 | BabySkeletronHead |  |
| 198 | BabyHornet |  |
| 199 | TikiSpirit |  |
| 200 | PetLizard |  |
| 208 | Parrot |  |
| 209 | Truffle |  |
| 210 | Sapling |  |
| 211 | Wisp | yes |
| 236 | BabyDino |  |
| 266 | BabySlime |  |
| 268 | EyeSpring |  |
| 269 | BabySnowman |  |
| 313 | Spider |  |
| 314 | Squashling |  |
| 317 | Raven |  |
| 319 | BlackCat |  |
| 324 | CursedSapling |  |
| 334 | Puppy |  |
| 353 | BabyGrinch |  |
| 373 | Hornet |  |
| 375 | FlyingImp |  |
| 380 | ZephyrFish |  |
| 387 | Retanimini |  |
| 388 | Spazmamini |  |
| 390 | VenomSpider |  |
| 391 | JumperSpider |  |
| 392 | DangerousSpider |  |
| 393 | OneEyedPirate |  |
| 394 | SoulscourgePirate |  |
| 395 | PirateCaptain |  |
| 398 | MiniMinotaur |  |
| 407 | Tempest |  |
| 423 | UFOMinion |  |
| 492 | MagicLantern | yes |
| 499 | BabyFaceMonster |  |
| 533 | DeadlySphere |  |
| 613 | StardustCellMinion |  |
| 623 | StardustGuardian |  |
| 625 | StardustDragon1 |  |
| 626 | StardustDragon2 |  |
| 627 | StardustDragon3 |  |
| 628 | StardustDragon4 |  |
| 653 | CompanionCube |  |
| 701 | DD2PetDragon |  |
| 702 | DD2PetGhost | yes |
| 703 | DD2PetGato |  |
| 755 | BatOfLight |  |
| 758 | VampireFrog |  |
| 759 | BabyBird |  |
| 764 | UpbeatStar |  |
| 765 | SugarGlider |  |
| 774 | SharkPup |  |
| 815 | LilHarpy |  |
| 816 | FennecFox |  |
| 817 | GlitteryButterfly |  |
| 821 | BabyImp |  |
| 825 | BabyRedPanda |  |
| 831 | StormTigerGem |  |
| 833 | StormTigerTier1 |  |
| 834 | StormTigerTier2 |  |
| 835 | StormTigerTier3 |  |
| 854 | Plantero |  |
| 858 | DynamiteKitten |  |
| 859 | BabyWerewolf |  |
| 860 | ShadowMimic |  |
| 864 | Smolstar |  |
| 875 | VoltBunny |  |
| 881 | KingSlimePet |  |
| 882 | EyeOfCthulhuPet |  |
| 883 | EaterOfWorldsPet |  |
| 884 | BrainOfCthulhuPet |  |
| 885 | SkeletronPet |  |
| 886 | QueenBeePet |  |
| 887 | DestroyerPet |  |
| 888 | TwinsPet |  |
| 889 | SkeletronPrimePet |  |
| 890 | PlanteraPet |  |
| 891 | GolemPet | yes |
| 892 | DukeFishronPet |  |
| 893 | LunaticCultistPet |  |
| 894 | MoonLordPet |  |
| 895 | FairyQueenPet | yes |
| 896 | PumpkingPet | yes |
| 897 | EverscreamPet |  |
| 898 | IceQueenPet |  |
| 899 | MartianPet |  |
| 900 | DD2OgrePet |  |
| 901 | DD2BetsyPet |  |
| 934 | QueenSlimePet |  |
| 946 | EmpressBlade |  |
| 951 | FlinxMinion |  |
| 957 | GlommerPet |  |
| 958 | DeerclopsPet |  |
| 959 | PigPet |  |
| 960 | ChesterPet |  |
| 963 | AbigailMinion |  |
| 970 | AbigailCounter |  |
| 994 | JunimoPet |  |
| 998 | BlueChickenPet |  |
| 1003 | Spiffo |  |
| 1004 | CavelingGardener |  |
| 1018 | DirtiestBlock |  |

---

### クローン用途のおすすめ（例）

- **ZephyrFish (380)**: 空中追従型（最も無難）。
- **SugarGlider (765)**: 羽ばたき型/地上寄りの動き。
- **UpbeatStar (764)**: ゆっくり浮遊するタイプ（キャラ選択プレビューとも相性良）。
- **Bunny (111), Penguin (112), Turtle (127)**: シンプルな追従ペット。
- **Wisp (211, Light)**: ライトペット挙動を試す時のベース。

実装ヒント:

```csharp
Projectile.CloneDefaults(ProjectileID.ZephyrFish);
AIType = ProjectileID.ZephyrFish;
```

プレビュー調整（任意）:

```csharp
ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] =
    ProjectileID.Sets.SimpleLoop(0, Main.projFrames[Projectile.type], 6)
    .WithOffset(-10, -20f)
    .WithSpriteDirection(-1);
```
