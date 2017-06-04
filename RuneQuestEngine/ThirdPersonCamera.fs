namespace RuneQuest.Engine

open OpenTK

type Camera = {
  eye: Vector3
  target: Vector3
  up: Vector3
}