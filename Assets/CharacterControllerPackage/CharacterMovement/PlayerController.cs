using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float speed;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb;

    //Accessing charController
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
    }

    //Subbing and unsubbing
    private void OnEnable()
    {
        Actions.MoveEvent += UpdateMoveVector;
    }

    private void OnDisable()
    {
        Actions.MoveEvent -= UpdateMoveVector;        
    }    

    //Will perform the logic to move the player using controllers built in Move method
    private void MovePlayer(Vector2 InputVector)
    {
        rb.velocity = new Vector2(InputVector.x * speed, InputVector.y * speed);
    }    

    //Using this method as a means to toggle the action of the main method MovePlayer
    private void UpdateMoveVector(Vector2 InputVector)
    {
        moveVector = InputVector;
    }

    //Running the main method through update for constant movement when button is held
    private void FixedUpdate()
    {
        MovePlayer(moveVector);
    }
}
