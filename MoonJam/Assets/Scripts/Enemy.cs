using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Enemy Configs")]
    [SerializeField] private float health = 100f;

    [Header("Projectile Configs")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private float projectileBPM = 60f;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }
}
