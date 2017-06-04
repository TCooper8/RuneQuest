namespace RuneQuest.Engine

open OpenTK
open OpenTK.Graphics
open OpenTK.Graphics.OpenGL
open OpenTK.Input

type PlayerControlComponent (game, hero:HeroState, camera:Camera) =
  inherit DrawableGameComponent (game)

  let mutable hero = hero
  let mutable camera = camera

  override x.update gameTime =
    let mutable push = Vector3.Zero
    if game.Keyboard.[Key.A] then
      push.X <- -1.0f
    if game.Keyboard.[Key.D] then
      push.X <- 1.0f

    if game.Keyboard.[Key.S] then
      push.Y <- -1.0f
    if game.Keyboard.[Key.W] then
      push.Y <- 1.0f

    if game.Keyboard.[Key.Escape] then
      game.Exit()

    if push <> Vector3.Zero then
      hero <- { hero with pos = hero.pos + push * hero.speed }
      //camera <- { camera with target = hero.pos }
    ()

  override x.draw () =
    //GL.MatrixMode MatrixMode.Projection
    //GL.LoadIdentity()
    GL.MatrixMode MatrixMode.Modelview

    let modelView = Matrix4.LookAt(camera.eye, camera.target, camera.up)
    GL.LoadMatrix(ref modelView)
    GL.PushMatrix()

    GL.Begin PrimitiveType.Triangles

    GL.Color3(1.0, 1.0, 0.0)
    GL.Vertex3(-1.0, -1.0, 4.0)
    GL.Color3(1.0, 0.0, 0.0)
    GL.Vertex3(1.0, -1.0, 4.0)
    GL.Color3(0.2, 0.9, 1.0)
    GL.Vertex3(0.0, 1.0, 4.0)

    GL.End()
    GL.PopMatrix()
    GL.Translate hero.pos
    ()