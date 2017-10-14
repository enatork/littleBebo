using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damageToGive = 1;
    private GameObject player;
    private PlayerController playerController;
    private HealthManager healthManager;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>().gameObject;
        playerController = player.GetComponent<PlayerController>();
        healthManager = FindObjectOfType<HealthManager>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            healthManager.HurtPlayer(damageToGive);

            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            hitDirection.y = 0.5f;
            playerController.KnockBack(hitDirection);

        }
    }
}
