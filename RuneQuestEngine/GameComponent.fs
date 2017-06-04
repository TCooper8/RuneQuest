namespace RuneQuest.Engine

open OpenTK

[<AbstractClass>]
type GameComponent (game:GameWindow) =
  abstract update: float -> unit

[<AbstractClass>]
type DrawableGameComponent (game:GameWindow) =
  inherit GameComponent(game)
  abstract draw: unit -> unit