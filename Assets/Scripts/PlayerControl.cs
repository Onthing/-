using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    PlayerCharacter player;
    void Start()
    {
        player = GetComponent<PlayerCharacter>();
    }
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            player.Attack();
        }
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        player.Move(new Vector3(h,0,v));
        var lookDir = Vector3.forward * v + Vector3.right * h;
        if (lookDir.magnitude != 0)
        {
            player.Rotate(lookDir);
        }
    }

}
