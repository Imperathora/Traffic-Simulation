using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner1 : SpawnerController
{
    private static Spawner1 instance;
    public static Spawner1 spawner1Instance { get { return instance; } }

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
