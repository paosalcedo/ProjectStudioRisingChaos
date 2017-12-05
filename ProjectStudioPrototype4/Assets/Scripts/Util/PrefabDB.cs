using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Prefab DB")]
public class PrefabDB : ScriptableObject {
    [SerializeField]
    private GameObject[] scenes;
    public GameObject[] Scenes { get { return scenes; } }

    [SerializeField]

    private GameObject[] players;
    public GameObject[] Players { get { return players; } }

    [SerializeField]

    private GameObject[] stealthPlayers;
    public GameObject[] StealthPlayers { get { return stealthPlayers; } }
    

    [SerializeField]
    private GameObject node;
    public GameObject Node { get { return node; } }

    [SerializeField]
    private GameObject grenade;
    public GameObject Grenade { get { return grenade; }}

    [SerializeField]
    private GameObject trap;
    public GameObject Trap { get { return trap; }}

    [SerializeField]
    private GameObject reflectoid;
    public GameObject Reflectoid {get { return reflectoid; }}

    [SerializeField]
    private GameObject cameraProjectile;
    public GameObject CameraProjectile {get {return cameraProjectile; }}
    
}
