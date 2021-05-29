using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Player Configs")]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] float xPadding = 1f;
    [SerializeField] float yPadding = 1f;

    // Cache
    private float minXPosition;
    private float maxXPosition;
    private float minYPosition;
    private float maxYPosition;


    // Start is called before the first frame update
    void Start() {
        setupMoveBoundaries();
    }

    // Update is called once per frame
    void Update() {
        move();
    }

    private void move() {
        Vector2 movementInput = getMovementDirectionInput();

        float xDestination = Mathf.Clamp(
            transform.position.x + movementInput.x, minXPosition, maxXPosition);
        float yDestination = Mathf.Clamp(
            transform.position.y + movementInput.y, minYPosition, maxYPosition);

        Vector2 destination = new Vector2(xDestination, yDestination);

        transform.position = destination;
    }

    private void setupMoveBoundaries() {
        Camera gameCamera = Camera.main;

        Vector3 minWorldPoint = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxWorldPoint = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minXPosition = minWorldPoint.x + xPadding;
        maxXPosition = maxWorldPoint.x - xPadding;

        minYPosition = minWorldPoint.y + yPadding ;
        maxYPosition = maxWorldPoint.y - yPadding;
    }

    private Vector2 getMovementDirectionInput() {
        float xInput = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;;
        float yInput = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        return new Vector2(xInput, yInput);
    }
}
