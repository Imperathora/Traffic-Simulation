using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] public List<GameObject> carsToSpawn = new List<GameObject>();

    public List<GameObject> carRegister;
    private float spawnTimer;
    private float currentTimeToSpawn;
    private bool spawnedOnCar;

    public event System.Action OnCarSpawn;
    public event System.Action OnCarDespawn;

    private float raycastDistance = 20f;
    public float overlapTestBoxSize = 1f;
    public LayerMask spawnedObjectLayer;
    public int carType;


    void Start()
    {
        carRegister = new List<GameObject>();
        PositionRaycast();
        currentTimeToSpawn = spawnTimer;
    }

    public void SetRandomSpawnTimer()
    {
        spawnTimer = Random.Range(1f, 10f);
    }

    public void RegisterCar(GameObject _car)
    {
        carRegister.Add(_car);
    }

    public void UnregisterCar(GameObject _car)
    {
        carRegister.Remove(_car);
    }

    public void OnDestroy()
    {
        OnCarSpawn = null;
        OnCarDespawn = null;
    }

    public void CalculateSpawnTimer()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            SetRandomSpawnTimer();
            currentTimeToSpawn = spawnTimer;
        }
    }

    public void DespawnCar()
    {
        for (int i = 0; i < carRegister.Count; i++)
        {
            switch (carRegister[i].GetComponent<Car>().direction)
            {
                case 1:
                    if (carRegister[i].transform.position.x < carRegister[i].GetComponent<Car>().dirXPos - 129)
                    {
                        OnCarDespawn?.Invoke();
                        Destroy(carRegister[i]);
                        UnregisterCar(carRegister[i]);
                    }
                    break;
                case 2:
                    if (carRegister[i].transform.position.z < carRegister[i].GetComponent<Car>().dirZPos - 129)
                    {
                        OnCarDespawn?.Invoke();
                        Destroy(carRegister[i]);
                        UnregisterCar(carRegister[i]);
                    }
                    break;
                case 3:
                    if (carRegister[i].transform.position.x > carRegister[i].GetComponent<Car>().dirXNeg + 129)
                    {
                        OnCarDespawn?.Invoke();
                        Destroy(carRegister[i]);
                        UnregisterCar(carRegister[i]);
                    }
                    break;
                case 4:
                    if (carRegister[i].transform.position.z > carRegister[i].GetComponent<Car>().dirZNeg + 129)
                    {
                        OnCarDespawn?.Invoke();
                        Destroy(carRegister[i]);
                        UnregisterCar(carRegister[i]);
                    }
                    break;
            }
        }
    }

    public void PositionRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            Vector3 overlapTestBoxScale = new Vector3(overlapTestBoxSize, overlapTestBoxSize, overlapTestBoxSize);
            Collider[] collidersInsideOverlapBox = new Collider[1];
            int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTestBoxScale, collidersInsideOverlapBox, spawnRotation, spawnedObjectLayer);

            if (numberOfCollidersFound == 0)
            {
                CalculateSpawnTimer();
            }
        }
        else
            CalculateSpawnTimer();
    }

    public void SpawnObject()
    {
        int index = Random.Range(0, 5);
        if (index > 1)
            index = 0;

        if (carsToSpawn.Count > 0)
        {
            RegisterCar(Instantiate(carsToSpawn[index], transform.position, transform.rotation));
            carType= getSpawnedObject(index);
             OnCarSpawn?.Invoke();
        }
    }

    public int getSpawnedObject(int index)
    {
        return index;
    }
}
