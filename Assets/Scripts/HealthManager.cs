using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    private PlayerController playerController;
    private GameObject player;

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
        player = FindObjectOfType<PlayerController>().gameObject;
        playerController = player.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HurtPlayer(int damage, Vector3 hitDirection) {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            playerController.Die();
        }
        else {
            playerController.KnockBack(hitDirection);
        }
        
    }

    public void HealPlayer(int healAmount) {
        currentHealth += healAmount;

        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }
}
