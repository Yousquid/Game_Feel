using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 velocity;

    [SerializeField] private float speed;
    [SerializeField] List<Transform> wallList = new List<Transform>();


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) velocity.y += 1;
        if (Input.GetKey(KeyCode.S)) velocity.y += -1;
        if (Input.GetKey(KeyCode.A)) velocity.x += -1;
        if (Input.GetKey(KeyCode.D)) velocity.x += 1;

        velocity = velocity.normalized;
        velocity *= speed;
        velocity *= Time.fixedDeltaTime;


        Vector3 intendedNextPosition = transform.position + velocity;
        if (!IsThisPlayerInAWall(intendedNextPosition))
        {
            transform.position += velocity;
        }
    }

    bool IsThisPlayerInAWall(Vector3 positionCheck)
    {
        foreach (Transform currentWall in wallList)
        {
            //Calculate the distance between the player and the wall in X and in Y
            float xDistance = Mathf.Abs(positionCheck.x - currentWall.position.x);
            float yDistance = Mathf.Abs(positionCheck.y - currentWall.position.y);

            float xMaxDistance = transform.localScale.x / 2 + currentWall.localScale.x / 2;
            float yMaxDistance = transform.localScale.y / 2 + currentWall.localScale.y / 2;

            if (xDistance < xMaxDistance && yDistance < yMaxDistance)
            {
                return true;
            }

        }
        return false;
    }
}
