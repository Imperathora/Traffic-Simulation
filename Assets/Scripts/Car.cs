using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    private Vector3 tempPos;
    public float dirXNeg, dirXPos, dirZNeg, dirZPos;
    public int direction;
    public float carDistance = 10f;
    public string tagCollider = "Collider";
    public string tagCar = "Car";
    public string tagSpecialCar = "SpecialCar";
    public string tagCheckpoint = "Checkpoint";


    private float speed;
    private float tempSpeed;
    public bool isMoving;
    public bool hasPassedCollider;
    public bool hasHitCar;
    public bool isStanding;


    void Start()
    {

        tempPos = this.transform.position;
        if (tempPos.x == 64.5)
        {
            dirXPos = tempPos.x;
            direction = 1;
        }

        else if (tempPos.z == 64.5)
        {
            dirZPos = tempPos.z;
            direction = 2;
        }

        else if (tempPos.x == -64.5)
        {
            dirXNeg = tempPos.x;
            direction = 3;
        }

        else if (tempPos.z == -64.5)
        {
            dirZNeg = tempPos.z;
            direction = 4;
        }

        isMoving = true;
        hasPassedCollider = false;
        isStanding = false;
        hasHitCar = false;
        RandomSpeed();
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, carDistance) && !isMoving && hit.transform.tag == tagCollider)
        {
            hasHitCar = false;
            return;

        }
        else if ( Physics.Raycast(transform.position, transform.forward, out hit, carDistance) && hit.transform.tag == tagCar || Physics.Raycast(transform.position, transform.forward, out hit, carDistance) && hit.transform.tag == tagSpecialCar)
        {
            Stop();
            hasHitCar = true;
            isStanding = true;


        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, carDistance) && hit.transform.tag == tagCheckpoint && !hasPassedCollider)
        {
            hasPassedCollider = true;
            hasHitCar = false;
        }
        else 
        {
            hasHitCar = false;
            isStanding = false;
            Move();
        }

    }

    public void Move()
    {
        speed = tempSpeed;
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
        isMoving = true;

    }

    public void Stop()
    {
        speed = 0;
        isMoving = false;
    }


    private void RandomSpeed()
    {
        speed = Random.Range(5, 15);
        tempSpeed = speed;
    }
}
