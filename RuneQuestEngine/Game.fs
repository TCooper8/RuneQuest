namespace RuneQuest.Engine

open System
open System.Windows.Forms
open OpenTK
open OpenTK.Graphics
open OpenTK.Graphics.OpenGL
open OpenTK.Input

type Game () =
  inherit GameWindow(
    Screen.PrimaryScreen.Bounds.Width,
    Screen.PrimaryScreen.Bounds.Height,
    GraphicsMode.Default,
    "RuneQuest",
    GameWindowFlags.Fullscreen
  )

  do
    base.VSync <- VSyncMode.On

  let mutable components: GameComponent array = Array.empty
  let mutable drawableComponents: DrawableGameComponent array = Array.empty

  override x.OnLoad e =
    base.OnLoad e

    GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f)
    GL.Enable(EnableCap.DepthTest)
    GL.Enable(EnableCap.StencilTest)

  override x.OnResize e =
    base.OnResize e

    GL.Viewport(
      base.ClientRectangle.X,
      base.ClientRectangle.Y,
      base.ClientRectangle.Width,
      base.ClientRectangle.Height
    )

    let projection =
      Matrix4.CreatePerspectiveFieldOfView(
        float32 Math.PI / 4.0f,
        float32 base.Width / float32 base.Height,
        1.0f,
        64.0f
      )
    GL.MatrixMode MatrixMode.Projection
    GL.LoadMatrix (ref projection)

  override x.OnUpdateFrame e =
    base.OnUpdateFrame e

    for elem in components do
      elem.update e.Time
    for elem in drawableComponents do
      elem.update e.Time

  override x.OnRenderFrame e =
    base.OnRenderFrame e
    GL.Clear(ClearBufferMask.ColorBufferBit ||| ClearBufferMask.DepthBufferBit)

    for elem in drawableComponents do
      elem.draw ()

    base.SwapBuffers()

  member x.updateComponent index elem =
    components.[index] <- elem

  member x.addComponent elem =
    components <- Array.append components [|elem|]

  member x.addDrawable elem =
    drawableComponents <- Array.append drawableComponents [|elem|]