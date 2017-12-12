using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Services {
    public static GameManager GameManager { get; set; }
    public static EventManager EventManager { get; set; }
	public static TaskManager TaskManager { get; set; }
    public static PrefabDB Prefabs { get; set; }
    public static MaterialDB Materials { get; set; }
    public static SceneStackManager<TransitionData> SceneStackManager { get; set; }
    public static InputManager InputManager { get; set; }
    // public static Main Main { get; set; }
    public static MapManager MapManager { get; set; }
    public static TimeManager TimeManager { get; set;}
    public static CanvasManager CanvasManager { get; set; }
    public static WeaponDefinitions WeaponDefinitions { get; set; }
    public static WeaponSoundManager WeaponSoundManager { get; set; }
    public static GameStateManager GameStateManager { get; set; }
    public static HealthManager HealthManager { get; set; }
    public static DisplayScript DisplayScript { get; set; }
    public static CurrentPlayerTracker CurrentPlayerTracker { get; set; }
    public static PlayerNames PlayerNames { get; set; }
    public static ScoreKeeper ScoreKeeper { get; set; }
    public static TurnManager TurnManager { get; set; }

}
