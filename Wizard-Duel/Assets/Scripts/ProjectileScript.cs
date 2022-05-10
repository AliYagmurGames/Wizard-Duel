using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    bool startGravity = false;
    Rigidbody rb;
    Vector3 relativeGravity;
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (startGravity == true)
        {
            rb.AddForce(relativeGravity, ForceMode.Acceleration);
        }
    }

    IEnumerator projectileMovement(Transform target)
    {
        yield return new WaitForSeconds(5);
    }

    public void startProjectile(GameObject target, Vector3 velocity)
    {
        relativeGravity = -1 * velocity;
        float timeAmount = velocity.magnitude / 5;
        timeAmount = Mathf.Clamp(timeAmount, 0.5f, 3);
        timeAmount /= 3;
        timeAmount = 1.8f - timeAmount;
        rb.velocity = calculateVelocity(target.transform.position + new Vector3(0,1,0), this.transform.position, -1 * relativeGravity, timeAmount);
        startGravity = true;
    }

    Vector3 calculateVelocity(Vector3 target, Vector3 origin, Vector3 gravity, float time)
    {
        float dX = target.x - origin.x;
        float dY = target.y - origin.y;
        float dZ = target.z - origin.z;

        float vX = dX / time + 0.5f * gravity.x * time;
        float vY = dY / time + 0.5f * gravity.y * time;
        float vZ = dZ / time + 0.5f * gravity.z * time;
        return new Vector3 (vX, vY, vZ);
    }
}
