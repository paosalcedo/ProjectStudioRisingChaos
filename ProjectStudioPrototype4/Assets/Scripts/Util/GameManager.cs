using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public GameObject sceneRoot;
	// public Camera currentCamera;
	void Awake()
	{
		InitializeServices();
	}

	// Use this for initialization
	void Start()
	{
		// Services.EventManager.Register<Reset>(Reset);
		// Services.SceneStackManager.PushScene<TitleScreen>();
	}

	// Update is called once per frame
	void Update()
	{
		Services.TaskManager.Update();
		Debug.Log(Services.WeaponDefinitions.weapons[WeaponDefinitions.WeaponType.Grenade].description);
	}

	void InitializeServices()
	{
		Services.GameManager = this;
		Services.MapManager = GetComponentInChildren<MapManager>();
		Services.WeaponDefinitions = new WeaponDefinitions();
		Services.EventManager = new EventManager();
		Services.TaskManager = new TaskManager();
		Services.Prefabs = Resources.Load<PrefabDB>("Prefabs/PrefabDB");
		Services.Materials = Resources.Load<MaterialDB>("Art/Materials");
		Services.TimeManager = GetComponent<TimeManager>();
		// Services.SceneStackManager = new SceneStackManager<TransitionData>(sceneRoot, Services.Prefabs.Scenes);
		Services.InputManager = new InputManager();
		Services.CanvasManager = new CanvasManager();


        // Services.LightManager = new LightManager();

	}

	void Reset(Reset e)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	//UI buttons

}
