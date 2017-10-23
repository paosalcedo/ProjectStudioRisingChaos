﻿using System.Collections;
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
    
    // public static LightManager LightManager { get; set; }

}
