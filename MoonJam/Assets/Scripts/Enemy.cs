using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour {
    [Header("Enemy Configs")]
    [SerializeField] private float health = 100f;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private int score = 1;

    [Header("Projectile Configs (Optional)")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 0;
    [SerializeField] private float projectileBPM = 0;

    // Cache
    private GameStatus gameStatus = null;
    private SortingGroup sortingGroup = null;

    // Start is called before the first frame update
    void Start() {
        gameStatus = FindObjectOfType<GameStatus>();

        sortingGroup = gameObject.GetComponent<SortingGroup>();
    }

    // Update is called once per frame
    void Update() {
        move();
    }

    public void setSortingGroup(SortingGroup sortingGroup) {
        getSortingGroup().sortingLayerName = sortingGroup.sortingLayerName;
        getSortingGroup().sortingOrder = sortingGroup.sortingOrder;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Shredder shredder = other.gameObject.GetComponent<Shredder>();

        if (shredder != null) {
            gameStatus.addToScore(score);
        }
    }

    private void move() {
        Vector3 movement = new Vector3(movementSpeed * Time.deltaTime, 0, 0);

        Vector3 destination = transform.position - movement;

        transform.position = destination;
    }

    private SortingGroup getSortingGroup() {
        return gameObject.GetComponent<SortingGroup>();
    }
}
