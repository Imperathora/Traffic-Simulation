using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{

    public event System.Action OnGreenLight;
    public event System.Action OnRedLight;
    [SerializeField] GameObject car;

    public float switchLightsTime;

    private float lightTimer = 0;
    private int currentLight = 0;
    private int previousLight = 0;

    private Spawner1 carSpawner1;
    private Spawner2 carSpawner2;
    private Spawner3 carSpawner3;
    private Spawner4 carSpawner4;

    void Start()
    {
        carSpawner1 = Spawner1.spawner1Instance;
        carSpawner2 = Spawner2.spawner2Instance;
        carSpawner3 = Spawner3.spawner3Instance;
        carSpawner4 = Spawner4.spawner4Instance;

        carSpawner1.OnCarSpawn += AddEvents;
        carSpawner1.OnCarDespawn += RemoveEvents;
        carSpawner2.OnCarSpawn += AddEvents;
        carSpawner2.OnCarDespawn += RemoveEvents;
        carSpawner3.OnCarSpawn += AddEvents;
        carSpawner3.OnCarDespawn += RemoveEvents;
        carSpawner4.OnCarSpawn += AddEvents;
        carSpawner4.OnCarDespawn += RemoveEvents;


        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        currentLight = Random.Range(0, 4);
    }

    void Update()
    {
        TrafficLightTimer();
        CheckCase();
    }

    private void CheckCase()
    {
        switch (previousLight)
        {
            case 0:
                for (int i = 0; i < carSpawner1.carRegister.Count; i++)
                {
                    carSpawner1.carRegister[i].GetComponent<Car>().isMoving = true;
                    OnGreenLight?.Invoke();
                }
                for (int i = 0; i < carSpawner2.carRegister.Count; i++)
                {
                    carSpawner2.carRegister[i].GetComponent<Car>().isMoving = false;
                    OnRedLight?.Invoke();
                }
                for (int i = 0; i < carSpawner3.carRegister.Count; i++)
                {
                    carSpawner3.carRegister[i].GetComponent<Car>().isMoving = false;
                    OnRedLight?.Invoke();
                }
                for (int i = 0; i < carSpawner4.carRegister.Count; i++)
                {
                    carSpawner4.carRegister[i].GetComponent<Car>().isMoving = false;
                    OnRedLight?.Invoke();
                }
                break;
            case 1:
                for (int i = 0; i < carSpawner1.carRegister.Count; i++)
                {
                    carSpawner1.carRegister[i].GetComponent<Car>().isMoving = false;
                    OnRedLight?.Invoke();
                }
                for (int i = 0; i < carSpawner2.carRegister.Count; i++)
                {
                    carSpawner2.carRegister[i].GetComponent<Car>().isMoving = true;
                    OnGreenLight?.Invoke();
                }
                for (int i = 0; i < carSpawner3.carRegister.Count; i++)
                {
                    carSpawner3.carRegister[i].GetComponent<Car>().isMoving = false;
                    OnRedLight?.Invoke();
                }
                for (int i = 0; i < carSpawner4.carRegister.Count; i++)
                {
                    carSpawner4.carRegister[i].GetComponent<Car>().isMoving = false;
                    OnRedLight?.Invoke();
                }
                break;
            case 2:
                for (int i = 0; i < carSpawner1.carRegister.Count; i++)
                {
                    carSpawner1.carRegister[i].GetComponent<Car>().isMoving = false;
                    OnRedLight?.Invoke();
                }
                for (int i = 0; i < carSpawner2.carRegister.Count; i++)
                {
                    carSpawner2.carRegister[i].GetComponent<Car>().isMoving = false;
                    OnRedLight?.Invoke();
                }
                for (int i = 0; i < carSpawner3.carRegister.Count; i++)
                {
                    carSpawner3.carRegister[i].GetComponent<Car>().isMoving = true;
                    OnGreenLight?.Invoke();

                }
                for (int i = 0; i < carSpawner4.carRegister.Count; i++)
                {
                    carSpawner4.carRegister[i].GetComponent<Car>().isMoving = false;
                    OnRedLight?.Invoke();
                }
                break;
            case 3:
                for (int i = 0; i < carSpawner1.carRegister.Count; i++)
                {
                    carSpawner1.carRegister[i].GetComponent<Car>().isMoving = false;
                    OnRedLight?.Invoke();
                }
                for (int i = 0; i < carSpawner2.carRegister.Count; i++)
                {
                    carSpawner2.carRegister[i].GetComponent<Car>().isMoving = false;
                    OnRedLight?.Invoke();
                }
                for (int i = 0; i < carSpawner3.carRegister.Count; i++)
                {
                    carSpawner3.carRegister[i].GetComponent<Car>().isMoving = false;
                    OnRedLight?.Invoke();
                }
                for (int i = 0; i < carSpawner4.carRegister.Count; i++)
                {
                    carSpawner4.carRegister[i].GetComponent<Car>().isMoving = true;
                    OnGreenLight?.Invoke();
                }
                break;
        }
    }

    public void TrafficLightTimer()
    {
        if (Time.time >= lightTimer)
        {
            lightTimer += switchLightsTime;
            this.gameObject.transform.GetChild(currentLight).gameObject.GetComponent<Renderer>().material.color = Color.green;

            if (previousLight != currentLight)
            {
                this.gameObject.transform.GetChild(previousLight).gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            previousLight = currentLight;
            if (currentLight >= transform.childCount - 1)
            {
                currentLight = 0;
            }
            else
            {
                currentLight += 1;
            }
        }
    }

    private void AddEvents()
    {
        OnGreenLight += car.GetComponent<Car>().Move;
        OnRedLight += car.GetComponent<Car>().Stop;
    }

    private void RemoveEvents()
    {
        OnGreenLight -= car.GetComponent<Car>().Move;
        OnRedLight -= car.GetComponent<Car>().Stop;
    }
}
