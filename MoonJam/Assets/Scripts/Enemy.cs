using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Enemy Configs")]
    [SerializeField] private float health = 100f;
    [SerializeField] private float movementSpeed = 10f;

    [Header("Projectile Configs (Optional)")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 0;
    [SerializeField] private float projectileBPM = 0;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        move();
    }

    private void move() {
        Vector3 movement = new Vector3(movementSpeed * Time.deltaTime, 0, 0);

        Vector3 destination = transform.position - movement;

        transform.position = destination;
    }
}
