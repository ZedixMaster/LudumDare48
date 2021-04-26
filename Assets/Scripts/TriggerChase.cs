using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HERE");
            GameObject enemy = GameObject.FindGameObjectWithTag("ChasingEnemy");

            enemy.GetComponent<EnemyAI>().SetIsActive(true);
            enemy.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        }
    }
}
