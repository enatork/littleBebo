
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    private HealthManager healthManager;
    private Slider slider;

	// Use this for initialization
	void Start () {
        healthManager = FindObjectOfType<HealthManager>();
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = healthManager.maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = healthManager.currentHealth;
	}
}
