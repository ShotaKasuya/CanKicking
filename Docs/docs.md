# ドキュメント

## シーンの依存関係

```mermaid

block-beta
    columns 1
    db("Primary Scene")
    blockArrowId6<["Load"]>(down)
    
    block:ID
        Ui
        Environment
        GameplayResources
    end
    space
    
    Player

```