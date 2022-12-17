using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public float power;
    private PlayerStats playerStats;
    [SerializeField] private Healthbar healthbar;
    // Use this for initialization
    void Start () {
        playerStats = GetComponent<PlayerStats>();
    }
    // Update is called once per frame
    void Update () {

    }

    void OnCollisionEnter(Collision col)
    {
        // if(col.gameObject.tag == "Human")
        // {
        col.gameObject.GetComponent<PlayerStats>().health -= 10;
        healthbar.UpdateHealthBar(100,col.gameObject.GetComponent<PlayerStats>().health);
        Debug.Log("Health " + col.gameObject.name + col.gameObject.GetComponent<PlayerStats>().health);
        //}
    }
}
