﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private KeyCode restartKey;
	public GameObject sceneRoot;
	// public Camera currentCamera;
	void Awake()
	{
		InitializeServices();
	}

	// Use this for initialization
	void Start()
	{
		restartKey = KeyCode.F1;
		// Services.EventManager.Register<Reset>(Reset);
		// Services.SceneStackManager.PushScene<TitleScreen>();
	}

	// Update is called once per frame
	void Update()
	{
		Services.TaskManager.Update();
		RestartScene(restartKey);
		// Debug.Log(Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Grenade].description);
	}

	void InitializeServices()
	{
		Services.GameManager = this;
		Services.MapManager = new MapManager();
		Services.CurrentPlayerTracker = new CurrentPlayerTracker(); 
		Services.CanvasManager = new CanvasManager();
		Services.GameStateManager = new GameStateManager();
		Services.TimeManager = new TimeManager();
		Services.ScoreKeeper = new ScoreKeeper();
		Services.WeaponDefinitions = new WeaponDefinitions();
		Services.EventManager = new EventManager();
		Services.TaskManager = new TaskManager();
		Services.TurnManager = new TurnManager();
		Services.WeaponSoundManager = FindObjectOfType<WeaponSoundManager>();
		Services.Prefabs = Resources.Load<PrefabDB>("Prefabs/PrefabDB");
		Services.Materials = Resources.Load<MaterialDB>("Art/Materials");
		// Services.SceneStackManager = new SceneStackManager<TransitionData>(sceneRoot, Services.Prefabs.Scenes);
		Services.InputManager = new InputManager();

        // Services.LightManager = new LightManager();

	}

	void Reset(Reset e)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void RestartScene(KeyCode key){
		if(Input.GetKeyDown(key))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
