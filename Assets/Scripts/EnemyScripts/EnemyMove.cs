using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{

    public NavMeshAgent player;
    public GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            player.destination = target.transform.position;
        }
    }
}
