using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AttackZoneExpire());
    }
    IEnumerator AttackZoneExpire()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
