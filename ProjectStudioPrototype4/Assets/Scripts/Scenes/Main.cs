using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : Scene<TransitionData>
{


	// Use this for initialization
	void Start()
	{
 		// Services.MapManager.GenerateMap();
        // Services.LightManager.Start();

	}

	// Update is called once per frame
	void Update()
	{
        // Services.LightManager.Update();

	}

	void InitializeServices()
	{
		// Services.Main = this;
		// Services.MapManager = GetComponentInChildren<MapManager>();

	}

	internal override void OnEnter(TransitionData data)
	{
		InitializeServices();
		// Services.GameManager.currentCamera = GetComponentInChildren<Camera>();
	}
}