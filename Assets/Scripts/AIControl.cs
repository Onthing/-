using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    NavMeshAgent agent;
    PlayerCharacter player;
    Transform p;
	// Use this for initialization
	void Start ()
    {
        player = GetComponent<PlayerCharacter>();
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("FireControl", 1, 3);
        p = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FireControl()
    {
        player.Attack();
    }
    // Update is called once per frame
    void Update () {
        agent.destination = p.position;
        transform.LookAt(player.transform);
	}
}
