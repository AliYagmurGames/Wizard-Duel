using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    public int mouseInputType = 0;
    [SerializeField] TrackHandMovementPatterns handTracker;
    [SerializeField] Rigidbody wandAnchor;
    [SerializeField] GameObject projectilePrefab;
    GameObject target;
    [SerializeField] Transform wandTipTransform;
    [HideInInspector]
    public List<GameObject> approachingProjectiles;

    [SerializeField] float deflectRange;

    private void Awake()
    {
        approachingProjectiles = new List<GameObject>();

        //find the enemy wizard
        GameObject[] wizards = GameObject.FindGameObjectsWithTag("Wizard");
        foreach (GameObject w in wizards)
        {
            if(w != this.gameObject)
            {
                target = w;
            }
        }
    }
    void Update()
    {
        mouseInputSystem(); 
    }

    void mouseInputSystem()
    {
        if (Input.GetMouseButton(1) && Input.GetMouseButton(0))
        {
            //use shield. Shield shrinks by taking damage, can be restored
            if (mouseInputType != 1)
            {
                mouseInputType = 1;
            }
        }
        else if (Input.GetMouseButton(1))
        {
            //track the 4 sides of wand placement, apply buff if necessary
            //right click
            if (mouseInputType != 2)
            {
                mouseInputType = 2;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            //track the 4 sides of wand placement, apply buff if necessary
            //left click
            if(mouseInputType != 3)
            {
                mouseInputType = 3;
            }
            
        }

        if (Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
        {
            //when released fire a projectile.
            GameObject a = Instantiate(projectilePrefab, wandTipTransform.position, Quaternion.identity);
            a.GetComponent<ProjectileScript>().startProjectile(target, wandAnchor.velocity, 1);
            target.GetComponent<WandController>().approachingProjectiles.Add(a);
            handTracker.resetHandTracking();
        }
        else if (Input.GetMouseButtonUp(1) && !Input.GetMouseButton(0))
        {
            //animation, wand tip color fades
            //deflects projectile
            if (approachingProjectiles.Count != 0)
            {
                GameObject toBeDeflected = findClosestProjectile();
                if (Vector3.Distance(toBeDeflected.transform.position, transform.position) < deflectRange)
                {
                    toBeDeflected.GetComponent<Rigidbody>().AddForce(wandAnchor.velocity, ForceMode.Impulse);
                }

            }
            handTracker.resetHandTracking();
            //resets handPattern
        }
    }

    public void applyBuff(int type)
    {
        if (type == 1)
        {
            
            //apply left buff
        }
        else if (type == 2)
        {
            //apply right buff
        }
        else if (type == 3)
        {
            //up buff
        }
        else
        {
            //down buff
        }
    }

    GameObject findClosestProjectile()
    {
        if(approachingProjectiles.Count == 0)
        {
            return null;
        }
        float minDist = Mathf.Infinity;
        Vector3 currentPos = wandTipTransform.position;
        GameObject closestProjectile = approachingProjectiles[0];
        foreach (GameObject t in approachingProjectiles)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                minDist = dist;
                closestProjectile = t;
            }
        }
        return closestProjectile;
    }
}
