using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : SpawnerController
{

    private static Spawner2 instance;
    public static Spawner2 spawner2Instance { get { return instance; } }


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