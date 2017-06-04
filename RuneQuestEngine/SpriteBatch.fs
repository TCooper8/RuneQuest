namespace RuneQuest.Engine

open System.Drawing

open OpenTK
open OpenTK.Graphics.OpenGL

type SpriteBatch () =
  member x.draw (bounds:Rectangle, position:Vector3) texture =
    GL.PushMatrix()
    GL.Enable EnableCap.Texture2D

    GL.Translate position
    GL.Color4(Color.White)
    GL.BindTexture(TextureTarget.Texture2D, texture.gid)
    GL.Begin PrimitiveType.Quads

    let txi = float bounds.X / float texture.width
    let tyi = float bounds.Y / float texture.height
    let txf = float bounds.Width / float texture.width + txi
    let tyf = float bounds.Height / float texture.height + tyi

    GL.TexCoord2(txf, tyi)
    GL.Vertex2(1, 1)
    GL.TexCoord2(txi, tyi)
    GL.Vertex2(0, 1)
    GL.TexCoord2(txi, tyf)
    GL.Vertex2(0, 0)
    GL.TexCoord2(txf, tyf)
    GL.Vertex2(1, 0)

    GL.End()
    GL.BindTexture(TextureTarget.Texture2D, 0)
    GL.PopMatrix()