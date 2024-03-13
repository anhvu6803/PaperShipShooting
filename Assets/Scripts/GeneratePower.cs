using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePower : MonoBehaviour
{
    [SerializeField] private List<PowerAndRateUpSO> power;
    [SerializeField] private float maxRate;
    public void PowerGenerate(Vector3 position)
    {
        float delta = Random.Range(0, maxRate);
        foreach (var p in power)
        {
            if (delta < p.GetPowerRateUp())
            {
                GameObject instance = Instantiate(p.GetPower(), position, Quaternion.identity);
                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.down * p.GetSpeed();
                }
                break;
            }
        }
    }
}
