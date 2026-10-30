using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public GameObject playerSpawn;
    public player player;
    public GameObject weaponSpawn;
    public GameObject mapGenerator;
    public GameObject enemySpawner;
    //public GameObject UIspawns;
    public GameObject mainCamera;
    public CameraMovement cameraMovement;
   
    
    public bool playerActive = false;
    void Awake()
    {
        playerSpawn.SetActive(true);

        //UIspawns.SetActive(true);
        cameraMovement = mainCamera.GetComponent<CameraMovement>();
        cameraMovement.enabled = true;

        player.OnPlayerSpawned += OnPlayerSpawned;
        StartCoroutine(AssignCameraTarget());
      
    }
    
    public void Update()
    {
        if(player.gameObject.activeSelf && playerActive)
        {
            weaponSpawn.SetActive(true);
            mapGenerator.SetActive(true);
            enemySpawner.SetActive(true);
            playerActive = false;
        }
    }

    private void OnPlayerSpawned(player player)
    {
        cameraMovement.targetPoint = player.transform;
        player.OnPlayerSpawned -= OnPlayerSpawned; // Отписываемся, чтобы не засорять память
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator AssignCameraTarget()
    {
        yield return new WaitUntil(() => FindObjectOfType<player>() != null);
    
        player player = FindObjectOfType<player>();
        if (player != null)
        {
            cameraMovement.Initialize(player.transform);
        }
    }
}
