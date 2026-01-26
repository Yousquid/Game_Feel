using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 velocity;
    [SerializeField] private float speed;
    [SerializeField] private List<Transform> wallList = new List<Transform>();

    void FixedUpdate()
    {
        velocity = Vector3.zero; //Reset velocity each frame

        //Set velocity based on WASD input
        if (Input.GetKey(KeyCode.W)) velocity.y += 1;
        if (Input.GetKey(KeyCode.S)) velocity.y -= 1;
        if (Input.GetKey(KeyCode.D)) velocity.x += 1;
        if (Input.GetKey(KeyCode.A)) velocity.x -= 1;

        velocity = velocity.normalized; //Normalize the velocity to prevent faster diagonal movement.
        velocity *= speed; //Scale velocity by speed
        velocity *= Time.fixedDeltaTime; //Scale velocity by deltaTime to make movement framerate independent (and so you can set your speed based on units per second)

        Vector3 intendedNextPosition = transform.position + velocity; //Calculate where the player would be if they moved this frame

        //Only move the player if they are not going to be inside a wall next frame
        if (!IsThisTransformInAWall(intendedNextPosition))
        {
            transform.position += velocity;
        }
    }

    /// <summary>
    /// Checks if the given position is inside any wall in wallList
    /// </summary>
    bool IsThisTransformInAWall(Vector3 positionToCheck)
    {
        foreach (Transform currentWall in wallList)
        {
            //Calculate the distance between the player and the wall in X and in Y
            float xDistance = Mathf.Abs(positionToCheck.x - currentWall.position.x);
            float yDistance = Mathf.Abs(positionToCheck.y - currentWall.position.y);

            //Find the maximum allowed distance in X and Y by adding half of the player's scale and half of the wall's scale
            float xMaxDistance = transform.localScale.x / 2 + currentWall.localScale.x / 2;
            float yMaxDistance = transform.localScale.y / 2 + currentWall.localScale.y / 2;

            //If the player is closer to the wall than the maximum distance in both x and y, return true
            if (xDistance < xMaxDistance && yDistance < yMaxDistance) return true;
        }
        return false;
    }

}
