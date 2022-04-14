using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner3 : SpawnerController
{
    private static Spawner3 instance;
    public static Spawner3 spawner3Instance { get { return instance; } }

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


    void Update()
    {
        PositionRaycast();
        DespawnCar();
    }
}