using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.SceneManagement;

public class KIController : Agent
{

    [SerializeField] public Spawner1 carSpawner;
    [SerializeField] public Spawner2 carSpawner2;
    [SerializeField] public Spawner3 carSpawner3;
    [SerializeField] public Spawner4 carSpawner4;
    [SerializeField] public List<GameObject> carsToSpawn = new List<GameObject>();

    [Tooltip("Maximum number of cars to check when deciding whether to switch the light. The max number for cars at each stoplight is 3")]
    [SerializeField] public int maxNumCars;

    public event System.Action OnGreenLight;
    public event System.Action OnRedLight;

    public float timeBetweenDecisionsAtInference;


    private float timeSinceDecision;

    private float timerReset;
    float timer = 0f;


    private void Start()
    {
        carSpawner = Spawner1.spawner1Instance;
        carSpawner2 = Spawner2.spawner2Instance;
        carSpawner3 = Spawner3.spawner3Instance;
        carSpawner4 = Spawner4.spawner4Instance;

        OnGreenLight += carsToSpawn[0].GetComponent<Car>().Move;
        OnRedLight += carsToSpawn[0].GetComponent<Car>().Stop;
        OnGreenLight += carsToSpawn[1].GetComponent<Car>().Move;
        OnRedLight += carsToSpawn[1].GetComponent<Car>().Stop;
    }

    public override void OnEpisodeBegin()
    {
        timerReset = 10f;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }



    // for each action the AI determines whether to change the light and if so to what
    public override void OnActionReceived(ActionBuffers actions)
    {
        changeLight(actions.DiscreteActions);
    }


    //AI needs to know:
    // - car count to determinethe priority of the traffic light 
    // - for different car types and with different priorities it also needs to know the car type
    // AI needs a space size of 8 because the array count and the carType are each 1 integer and it is for 4 spawners
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(carSpawner.carRegister.Count);
        sensor.AddObservation(carSpawner2.carRegister.Count);
        sensor.AddObservation(carSpawner3.carRegister.Count);
        sensor.AddObservation(carSpawner4.carRegister.Count);

        sensor.AddObservation(carSpawner.carType);
        sensor.AddObservation(carSpawner2.carType);
        sensor.AddObservation(carSpawner3.carType);
        sensor.AddObservation(carSpawner4.carType);

    }

    // determine the traffic light state in regards to the action
    public void changeLight(ActionSegment<int> act)
    {
        var action = act[0];
        switch (action)
        {
            case 0:
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.green;
                this.gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.red;
                this.gameObject.transform.GetChild(2).gameObject.GetComponent<Renderer>().material.color = Color.red;
                this.gameObject.transform.GetChild(3).gameObject.GetComponent<Renderer>().material.color = Color.red;
                for (int i = 0; i < carSpawner.carRegister.Count; i++)
                {
                    carSpawner.carRegister[i].GetComponent<Car>().isMoving = true;
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
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.red;
                this.gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.green;
                this.gameObject.transform.GetChild(2).gameObject.GetComponent<Renderer>().material.color = Color.red;
                this.gameObject.transform.GetChild(3).gameObject.GetComponent<Renderer>().material.color = Color.red;
                for (int i = 0; i < carSpawner.carRegister.Count; i++)
                {
                    carSpawner.carRegister[i].GetComponent<Car>().isMoving = false;
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
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.red;
                this.gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.red;
                this.gameObject.transform.GetChild(2).gameObject.GetComponent<Renderer>().material.color = Color.green;
                this.gameObject.transform.GetChild(3).gameObject.GetComponent<Renderer>().material.color = Color.red;
                for (int i = 0; i < carSpawner.carRegister.Count; i++)
                {
                    carSpawner.carRegister[i].GetComponent<Car>().isMoving = false;
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
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.red;
                this.gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.red;
                this.gameObject.transform.GetChild(2).gameObject.GetComponent<Renderer>().material.color = Color.red;
                this.gameObject.transform.GetChild(3).gameObject.GetComponent<Renderer>().material.color = Color.green;
                for (int i = 0; i < carSpawner.carRegister.Count; i++)
                {
                    carSpawner.carRegister[i].GetComponent<Car>().isMoving = false;
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

    private void FixedUpdate()
    {
        if (carSpawner.carRegister.Count > 0 && carSpawner2.carRegister.Count > 0 && carSpawner3.carRegister.Count > 0 && carSpawner4.carRegister.Count > 0)
        {
            rewardOnCarType();

            rewardOnCarCount();

            CheckForCollision();
        }
        WaitTimeInference();
    }

    private void WaitTimeInference()
    {
        if (Academy.Instance.IsCommunicatorOn)
        {
            RequestDecision();
        }
        else
        {
            if (timeSinceDecision >= timeBetweenDecisionsAtInference)
            {
                timeSinceDecision = 0f;
                RequestDecision();
            }
            else
            {
                timeSinceDecision += Time.fixedDeltaTime;
            }
        }
    }

    private void rewardOnCarCount()
    {

        if (carSpawner.carRegister.Count > carSpawner2.carRegister.Count + maxNumCars || carSpawner.carRegister.Count > carSpawner3.carRegister.Count + maxNumCars || carSpawner.carRegister.Count > carSpawner4.carRegister.Count + maxNumCars)
        {
            int tempLastSpawner1 = carSpawner.carRegister.Count - 1;
            if (carSpawner.carRegister[tempLastSpawner1].GetComponent<Car>().isMoving == false)
            {
                int tempnum = -1;
                for (int i = 0; i < carSpawner.carRegister.Count; i++)
                {
                    if (carSpawner.carRegister[i].GetComponent<Car>().hasPassedCollider == false)
                    {
                        tempnum = i;
                    }

                }
                if (tempnum > carSpawner2.carRegister.Count + maxNumCars || tempnum > carSpawner3.carRegister.Count + maxNumCars || tempnum > carSpawner4.carRegister.Count + maxNumCars)
                {
                    AddReward(-.2f);
                    EndEpisode();
                }
            }

            else if (carSpawner.carRegister[tempLastSpawner1].GetComponent<Car>().isMoving == true)
            {
                AddReward(0.2f);
            }
        }


        if (carSpawner2.carRegister.Count > carSpawner.carRegister.Count + maxNumCars || carSpawner2.carRegister.Count > carSpawner3.carRegister.Count + maxNumCars || carSpawner2.carRegister.Count > carSpawner4.carRegister.Count + maxNumCars)
        {
            int tempLastSpawner2 = carSpawner2.carRegister.Count - 1;
            if (carSpawner2.carRegister[tempLastSpawner2].GetComponent<Car>().isMoving == false)
            {
                int tempnum = -1;
                for (int i = 0; i < carSpawner2.carRegister.Count; i++)
                {
                    if (carSpawner2.carRegister[i].GetComponent<Car>().hasPassedCollider == false)
                    {
                        tempnum = i;
                    }

                }
                if (tempnum > carSpawner.carRegister.Count + maxNumCars || tempnum > carSpawner3.carRegister.Count + maxNumCars || tempnum > carSpawner4.carRegister.Count + maxNumCars)
                {
                    AddReward(-.2f);
                    EndEpisode();
                }
            }
            else if (carSpawner2.carRegister[tempLastSpawner2].GetComponent<Car>().isMoving == true)
            {
                AddReward(0.2f);
            }
        }

        if (carSpawner3.carRegister.Count > carSpawner.carRegister.Count + maxNumCars || carSpawner3.carRegister.Count > carSpawner2.carRegister.Count + maxNumCars || carSpawner3.carRegister.Count > carSpawner4.carRegister.Count + maxNumCars)
        {
            int tempLastSpawner3 = carSpawner3.carRegister.Count - 1;
            if (carSpawner3.carRegister[tempLastSpawner3].GetComponent<Car>().isMoving == false)
            {
                int tempnum = -1;
                for (int i = 0; i < carSpawner3.carRegister.Count; i++)
                {
                    if (carSpawner3.carRegister[i].GetComponent<Car>().hasPassedCollider == false)
                    {
                        tempnum = i;
                    }

                }
                if (tempnum > carSpawner2.carRegister.Count + maxNumCars || tempnum > carSpawner.carRegister.Count + maxNumCars || tempnum > carSpawner4.carRegister.Count + maxNumCars)
                {
                    AddReward(-.2f);
                    EndEpisode();
                }
            }

            else if (carSpawner3.carRegister[tempLastSpawner3].GetComponent<Car>().isMoving == true)
            {
                AddReward(0.2f);
            }
        }


        if (carSpawner4.carRegister.Count > carSpawner.carRegister.Count + maxNumCars || carSpawner4.carRegister.Count > carSpawner2.carRegister.Count + maxNumCars || carSpawner4.carRegister.Count > carSpawner3.carRegister.Count + maxNumCars)
        {
            int tempLastSpawner4 = carSpawner4.carRegister.Count - 1;
            if (carSpawner4.carRegister[tempLastSpawner4].GetComponent<Car>().isMoving == false)
            {
                int tempnum = -1;
                for (int i = 0; i < carSpawner4.carRegister.Count; i++)
                {
                    if (carSpawner4.carRegister[i].GetComponent<Car>().hasPassedCollider == false)
                    {
                        tempnum = i;
                    }

                }
                if (tempnum > carSpawner.carRegister.Count + maxNumCars || tempnum > carSpawner2.carRegister.Count + maxNumCars || tempnum > carSpawner3.carRegister.Count + maxNumCars)
                {
                    AddReward(-.2f);
                    EndEpisode();
                }
            }
            else if (carSpawner4.carRegister[tempLastSpawner4].GetComponent<Car>().isMoving == true)
            {
                AddReward(0.2f);
            }
        }
    }

    private void rewardOnCarType()
    {
        if (carSpawner.carType == 1)
        {
            for (int i = 0; i < carSpawner.carRegister.Count; i++)
            {
                if (carSpawner.carRegister[i].GetComponent<Car>().isMoving == false)
                {
                    AddReward(-.2f);
                }
                else
                    AddReward(.2f);
            }
        }

        if (carSpawner2.carType == 1)
        {
            for (int i = 0; i < carSpawner2.carRegister.Count; i++)
            {
                if (carSpawner2.carRegister[i].GetComponent<Car>().isMoving == false)
                {
                    AddReward(-.2f);
                }
                else
                    AddReward(.2f);
            }
        }

        if (carSpawner3.carType == 1)
        {
            for (int i = 0; i < carSpawner3.carRegister.Count; i++)
            {
                if (carSpawner3.carRegister[i].GetComponent<Car>().isMoving == false)
                {
                    AddReward(-.2f);
                }
                else
                    AddReward(.2f);
            }
        }

        if (carSpawner4.carType == 1)
        {
            for (int i = 0; i < carSpawner4.carRegister.Count; i++)
            {
                if (carSpawner4.carRegister[i].GetComponent<Car>().isMoving == false)
                {
                    AddReward(-.2f);
                }
                else
                    AddReward(.2f);
            }
        }
    }

    private void CarHitTimer(GameObject _car)
    {
        timer += Time.deltaTime;
        if (timer >= timerReset)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            AddReward(-2f);
            timer = 0;
            EndEpisode();
        }
    }

    private void CheckForCollision()
    {
        if (carSpawner.carRegister[0].GetComponent<Car>().hasHitCar == true && carSpawner.carRegister[0].GetComponent<Car>().isStanding == true)
        {
            CarHitTimer(carSpawner.carRegister[0]);
        }
        else
            AddReward(.1f);

        if (carSpawner2.carRegister[0].GetComponent<Car>().hasHitCar == true && carSpawner2.carRegister[0].GetComponent<Car>().isStanding == true)
        {
            CarHitTimer(carSpawner2.carRegister[0]);
        }
        else
            AddReward(.1f);

        if (carSpawner3.carRegister[0].GetComponent<Car>().hasHitCar == true && carSpawner3.carRegister[0].GetComponent<Car>().isStanding == true)
        {
            CarHitTimer(carSpawner3.carRegister[0]);
        }
        else
            AddReward(.1f);

        if (carSpawner4.carRegister[0].GetComponent<Car>().hasHitCar == true && carSpawner4.carRegister[0].GetComponent<Car>().isStanding == true)
        {
            CarHitTimer(carSpawner4.carRegister[0]);
        }
        else
            AddReward(.1f);
    }
}



