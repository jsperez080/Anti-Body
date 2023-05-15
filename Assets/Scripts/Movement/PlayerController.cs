using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement Variables
    public Rigidbody playerRb;
    public float speed;
    public float maxSpeed;

    //Fire Variables
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject firePoint;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        PlayerMovement(vertical, horizontal);
        RotationManager();
        FireProjectile();
    }

    private void RotationManager()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }

    private float AngleBetweenTwoPoints(Vector3 angle1, Vector3 angle2)
    {
        return Mathf.Atan2(angle2.x - angle1.x, angle2.y - angle1.y) * Mathf.Rad2Deg;
    }

    private void PlayerMovement(float vertical, float horizontal)
    {
        //Camera Handling
        Vector3 cameraRelativeMovement = CameraMovementVectorForPlayer(vertical, horizontal);

        //Vertical
        if (vertical != 0)
        {
            playerRb.AddForce(new Vector3(0f, 0f, cameraRelativeMovement.z) * speed * Time.deltaTime);
        }

        //Horizontal
        if (horizontal != 0)
        {
            playerRb.AddForce(new Vector3(cameraRelativeMovement.x, 0f, 0f) * speed * Time.deltaTime);
        }

        //Mitigate moving faster with diagonal movement
        if((vertical != 0) && (horizontal != 0))
        {
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, maxSpeed);
        }
    }

    private static Vector3 CameraMovementVectorForPlayer(float vertical, float horizontal)
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        //Create direction-relative-input vectors
        Vector3 forwardRelativeVerticalInput = vertical * forward;
        Vector3 rightRelativeHorizontalInput = horizontal * right;

        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;
        return cameraRelativeMovement;
    }

    private void FixedUpdate()
    {
        ClampMaxVelocity();
    }

    private void ClampMaxVelocity()
    {
        if (playerRb.velocity.magnitude > maxSpeed)
        {
            playerRb.velocity = playerRb.velocity.normalized * maxSpeed;
        }
    }

    private void FireProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(projectile, firePoint.transform.position, transform.rotation);
        }
    }
}
