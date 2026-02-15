using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementRigidBody : MonoBehaviour
{

    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;

    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform FeetTransform;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float Jumpforce;
    private float CurrentSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }

    void MovePlayer()
    {
        /*------------------------------------------------------------------Run Function-------------------------------------------------------------------------------------*/
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CurrentSpeed = Speed * 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            CurrentSpeed = Speed;
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * CurrentSpeed;
        PlayerBody.linearVelocity = new Vector3(MoveVector.x, PlayerBody.linearVelocity.y, MoveVector.z);


        /* ---------------------------------------------------------------------Jump Function---------------------------------------------------------------------------*/

        if (Input.GetKeyDown(KeyCode.Space)) //If player presses space, they jump.
        {
            if (Physics.CheckSphere(FeetTransform.position, 0.1f, FloorMask))
            { //checks to see if the player is grounded.
                PlayerBody.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
            }
        }
        else // Applies a constant gravity force, so the player isn't so floaty
        {
            PlayerBody.AddForce(Vector3.down * 2, ForceMode.Force);
        }

        /* ----------------------------------------------------------------------------------------------------------------------------------------------------------------*/



        /*-------------------------------------------------------------------------Dash Function-------------------------------------------------------------------------------------*/
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerBody.AddForce(transform.forward * 1000, ForceMode.Impulse);
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        

    }
    void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y * sensitivity;
        transform.Rotate(0f, PlayerMouseInput.x * sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}
