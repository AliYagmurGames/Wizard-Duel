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

    [SerializeField] WandController wandControl;

    bool centerTracker = false;
    bool leftTracker = false;
    bool rightTracker = false;
    bool upTracker = false;
    bool downTracker = false;

    int buffType = 0;

    private void Awake()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other == centerCollider)
        {
            centerTracker = true;
        }
        else if(other == leftCollider)
        {
            leftTracker = true;
            upTracker = false;
            downTracker = false;
            centerTracker = false;
            
            if (rightTracker == true)
            {
                buffType = 2;
                wandControl.applyBuff(buffType);
                resetHandTracking();
            }
        }
        else if (other == rightCollider)
        {
            rightTracker = true;
            upTracker = false;
            downTracker = false;
            centerTracker = false;
            if (leftTracker == true)
            {
                buffType = 1;
                wandControl.applyBuff(buffType);
                resetHandTracking();
            }
        }
        else if (other == upCollider)
        {
            upTracker = true;
            leftTracker = false;
            rightTracker = false;
            centerTracker = false;
            if (downTracker == true)
            {
                buffType = 4;
                wandControl.applyBuff(buffType);
                resetHandTracking();
            }
        }
        else if (other == downCollider)
        {
            downTracker = true;
            leftTracker = false;
            rightTracker = false;
            centerTracker = false;
            if (upTracker == true)
            {
                buffType = 3;
                wandControl.applyBuff(buffType);
                resetHandTracking();
            }
        }
    }

    public void resetHandTracking()
    {
        downTracker = false;
        leftTracker = false;
        rightTracker = false;
        upTracker = false;
        centerTracker = false;
        buffType = 0;

        //to be deleted later
        if(centerTracker == true)
        {
            Debug.Log("delete this part");
        }
    }
}
