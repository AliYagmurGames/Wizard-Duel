using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorjectileDummy : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject projectilePrefab;
    WandController _wandControl;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(throwProjectile());
        _wandControl = player.GetComponent<WandController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player.transform.position);
    }

    IEnumerator throwProjectile()
    {
        while(true) 
        {
            yield return new WaitForSeconds(5);
            //throwProjectile
            GameObject a = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
            a.GetComponent<ProjectileScript>().startProjectile(player, new Vector3(1, 0, 0));
            _wandControl.approachingProjectiles.Add(a);
        }
    }
}
