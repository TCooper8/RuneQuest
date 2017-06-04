namespace RuneQuest.Engine

open System
open OpenTK

type HeroStats = {
  health: int
  maxHealth: int
}

type HeroClass =
  | Warrior

type HeroState = {
  id: Guid
  name: string
  pos: Vector3
  speed: float32
  level: int
  stats: HeroStats
  heroClass: HeroClass
}