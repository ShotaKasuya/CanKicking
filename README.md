# CanKicking

缶を引っ張って飛ばし、ゴールを目指すゲーム

## クラス構成

* Installer
    * DIコンテナの役割を背負う
    * クラス名は最後にInstallerをつける

以降のクラス間ではインターフェースを利用して直接依存しないようにする

* Controller
  * 主なゲームの動作を記述する
  * クラス名は最後にControllerをつける
  * 関心
    * Interface
* Logic
  * Controllerに抽象化した処理を提供する
  * クラス名は最後にLogicをつける
  * 関心
    * Interface
* Interface
  * ControllerとView,Modelとの依存を逆転する
* View
  * UnityのMonoBehaviourのコンポーネントを持ち、表示を司る
  * クラス名は最後にViewをつける
  * 関心
    * なし
* Model
  * データを保持・提供する
  * クラス名は最後にModelをつける
  * 関心
    * なし


## 使用ライブラリ

* DOTween
* R3
* UniTask
* VContainer
* CsprojModifier
