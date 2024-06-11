using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    void Update()
    {
        if(transform.childCount != 0)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).gameObject.GetComponentInChildren<SpriteRenderer>().enabled) {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
        }
    }
}
