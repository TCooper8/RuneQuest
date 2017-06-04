namespace RuneQuest.Engine

open System.Drawing
open System.Drawing.Imaging

open OpenTK
open OpenTK.Graphics.OpenGL

type Texture2D = {
  gid: int
  width: int
  height: int
  bounds: Rectangle
} with
  static member ofFile (filename:string): Texture2D =
    use bmp = new Bitmap(filename)

    GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest)
    let gid = GL.GenTexture()
    GL.BindTexture(TextureTarget.Texture2D, gid)

    let bounds = 
      Rectangle(
        0,
        0,
        bmp.Width,
        bmp.Height
      )
    let data =
      bmp.LockBits(
        bounds,
        ImageLockMode.ReadOnly,
        PixelFormat.Format32bppArgb
      )
    GL.TexImage2D(
      TextureTarget.Texture2D,
      0,
      PixelInternalFormat.Rgba,
      data.Width,
      data.Height,
      0,
      PixelFormat.Bgra,
      PixelType.UnsignedByte,
      data.Scan0
    )

    bmp.UnlockBits data

    GL.TexParameter(
      TextureTarget.Texture2D,
      TextureParameterName.TextureMinFilter,
      int TextureMinFilter.Linear
    )
    GL.TexParameter(
      TextureTarget.Texture2D,
      TextureParameterName.TextureMagFilter,
      int TextureMinFilter.Linear
    )
    GL.TexParameter(
      TextureTarget.Texture2D,
      TextureParameterName.TextureWrapS,
      int TextureWrapMode.Repeat
    )
    GL.TexParameter(
      TextureTarget.Texture2D,
      TextureParameterName.TextureWrapT,
      int TextureWrapMode.Repeat
    )

    { gid = gid
      width = bmp.Width
      height = bmp.Height
      bounds = bounds
    }