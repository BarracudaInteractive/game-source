using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarComponent : MonoBehaviour
{
    [SerializeField] private GameObject car;
    private Slider healthSlider;

    private float damage;
    private float maxDamage;

    private float d;
    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
        maxDamage = car.GetComponent<HealthComponent>().ReturnMaxHealth();
        healthSlider = this.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        damage = car.GetComponent<HealthComponent>().GetDamage();
        d = damage / maxDamage;
       healthSlider.value = d;
    }
}
