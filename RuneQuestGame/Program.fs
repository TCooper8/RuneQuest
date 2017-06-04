
open System

open RuneQuest.Engine

open OpenTK

type Sprite (game, spriteBatch:SpriteBatch, texture) =
  inherit DrawableGameComponent(game)

  override x.update gameTime = ()
  override x.draw () = spriteBatch.draw (texture.bounds, Vector3.Zero) texture

[<EntryPoint>]
[<STAThread>]
let main argv = 
  use game = new Game()

  let spriteBatch = SpriteBatch ()

  let tileset = Texture2D.ofFile "C:/Users/coope/Downloads/tileset.png"
  let grid =
    TilesetGrid(
      spriteBatch,
      32,
      game,
      tileset,
      [ for y in 1 .. 16 ->
        [ for x in 1 .. 16 ->
          5
        ]
      ]
      |> array2D
    )

  let playerControl =
    PlayerControlComponent(
      game,
      { id = Guid.NewGuid()
        name = "bob"
        pos = Vector3(2.0f, 0.0f, 0.0f)
        speed = 1.0f
        level = 1
        stats =
          { health = 10
            maxHealth = 10
          }
        heroClass = Warrior
      },
      { target = Vector3.UnitZ
        eye = Vector3.UnitZ * 10.0f
        up = Vector3.UnitY * -1.0f
      }
    )
  //let sprite = Sprite (game, spriteBatch, Texture2D.ofFile tileset)

  game.addDrawable playerControl
  game.addDrawable grid

  game.Run(30.0)
  0