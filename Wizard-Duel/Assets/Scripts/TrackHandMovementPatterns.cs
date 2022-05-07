using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackHandMovementPatterns : MonoBehaviour
{
    //colliders for tracking hand movement patterns
    [SerializeField] Collider centerCollider;
    [SerializeField] Collider leftCollider;
    [SerializeField] Collider rightCollider;
    [SerializeField] Collider upCollider;
    [SerializeField] Collider downCollider;
    bool tracker1 = false;
    bool tracker2 = false;
    bool tracker3 = false;
    bool tracker4 = false;
    bool tracker5 = false;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider == centerCollider)
        {
            
        }
        else if(collision.collider == leftCollider)
        {
            tracker2 = true;
            tracker4 = false;
            tracker5 = false;
            if(tracker3 == true)
            {
                //add right buff
            }
        }
        else if (collision.collider == rightCollider)
        {
            tracker3 = true;
            tracker4 = false;
            tracker5 = false;
            if (tracker2 == true)
            {
                //add left buff
            }
        }
        else if (collision.collider == upCollider)
        {
            tracker4 = true;
            tracker2 = false;
            tracker3 = false;
            if (tracker5 == true)
            {
                //add down buff
            }
        }
        else if (collision.collider == downCollider)
        {
            tracker5 = true;
            tracker2 = false;
            tracker3 = false;
            if (tracker4 == true)
            {
                //add up buff
            }
        }
    }
}
