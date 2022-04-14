using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner4 : SpawnerController
{
    private static Spawner4 instance;
    public static Spawner4 spawner4Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        PositionRaycast();
        DespawnCar();
    }
}
