using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDPointsComponent : MonoBehaviour
{
    [SerializeField] private GameObject car;
    private Text text;

    private int damagePoints;
    private int maxDamagePoints;
    // Start is called before the first frame update
    void Start()
    {
        damagePoints = 0;
        maxDamagePoints = (int) car.GetComponent<HealthComponent>().ReturnMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        damagePoints = (int) car.GetComponent<HealthComponent>().GetDamage();
        text.text = damagePoints.ToString() + "/" + maxDamagePoints.ToString();
    }
}
