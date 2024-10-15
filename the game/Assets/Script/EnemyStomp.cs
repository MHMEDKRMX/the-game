using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStomp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "WeakPoint")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }
}
