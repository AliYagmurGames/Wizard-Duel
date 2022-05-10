using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float handSpeed;
    public float handResetSpeed;
    public float wandForceSpeed;
    [SerializeField] Animator _animator;
    [SerializeField] GameObject handPosition;
    [SerializeField] GameObject target;
    [SerializeField] Rigidbody rotationAnchor;
    [SerializeField] Transform centerHandPos;

    Transform startingHandTransform;
    // Start is called before the first frame update
    void Awake()
    {
        startingHandTransform = handPosition.transform;
    }

    // Update is called once per frame
    void Update()
    {
        movementControls();
        wandMovementControl();
        handRotationController();
        handRotation();

    }

    private void FixedUpdate()
    {
        rotateTowardsTarget();
    }

    void movementControls()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        if (movement.magnitude > 0)
        {
            movement.Normalize();
            Vector3 movementX = this.transform.right * movement.x;
            Vector3 movementZ = this.transform.forward * movement.z;
            movement = movementX + movementZ;
            movement *= speed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.right);

        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }

    void wandMovementControl()
    {
        if(Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            wandMovement();
        }
        else
        {
            var step = handResetSpeed * Time.deltaTime;
            handPosition.transform.position = Vector3.MoveTowards(handPosition.transform.position, centerHandPos.position, step);
        }
    }

    void handRotation()
    {
        Vector3 targetDirection = (rotationAnchor.transform.position - handPosition.transform.position).normalized;
        handPosition.transform.rotation = Quaternion.LookRotation(targetDirection);
        Vector3 relativeHandDirection = (handPosition.transform.position - centerHandPos.position);
        float relativeX = Vector3.Dot(relativeHandDirection, this.transform.right);
        float relativeY = Vector3.Dot(relativeHandDirection, Vector3.up);
        handPosition.transform.Rotate(0, -90, 0);
        Vector3 forceForHandRotation = this.transform.right * relativeX;
        forceForHandRotation.y = relativeY;
        rotationAnchor.AddForce(forceForHandRotation * wandForceSpeed * Time.deltaTime, ForceMode.Acceleration);
    }

    void wandMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 mouseMovement = new Vector3(mouseX, mouseY, 0f);
        Vector3 handMovement = this.transform.right * mouseX;
        handMovement.y = Vector3.Dot(mouseMovement, this.transform.up);
        handMovement *= handSpeed * Time.deltaTime;
        handPosition.transform.Translate(handMovement, Space.World);

        if ((handPosition.transform.position - centerHandPos.position).magnitude > 0.5)
        {
            //limits the hand movement
            handPosition.transform.position = centerHandPos.position + (handPosition.transform.position - centerHandPos.position).normalized / 2;
        }
    }

    void rotateTowardsTarget()
    {
        Vector3 lookingTowards = target.transform.position - this.transform.position;
        lookingTowards.y = 0;
        this.transform.forward = lookingTowards;
    }

    void handRotationController()
    {
        rotationAnchor.AddForce(this.transform.forward * 3000 * Time.deltaTime, ForceMode.Acceleration);
    }

    //This method tracks the hand movement in order to decide wheter it appylies certain patterns
    void trackElementalPower()
    {

    }
}
