namespace RuneQuest.Engine

open System
open System.Drawing

open OpenTK
open OpenTK.Graphics.OpenGL

type TilesetGrid (spriteBatch:SpriteBatch, cellSize, game, texture:Texture2D, grid:int[,]) =
  inherit DrawableGameComponent(game)

  override x.update gameTime =
    ()

  override x.draw () =
    grid
    |> Array2D.iteri (fun y x index ->
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, int TextureParameterName.ClampToEdge)
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, int TextureParameterName.ClampToEdge)
      let bounds =
        Rectangle(
          index % cellSize * cellSize,
          index / cellSize * cellSize,
          cellSize,
          cellSize
        )
      spriteBatch.draw (bounds, Vector3(float32 x, float32 y, 0.0f)) texture
    )