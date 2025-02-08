using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Claw claw;

    private void Update()
    {
        Movement();
        Momentum();
    }

    private const float speed = 0.09f;
    private const float rotSpeed = 6;
    void Movement()
    {
        if (Minigames.IsPlayingMinigame) return;

        if (Input.GetKey(KeyCode.W)) AddForce(speed * transform.up);

        if (Input.GetKey(KeyCode.A)) AddRotationForce(rotSpeed * Vector3.forward);
        if (Input.GetKey(KeyCode.D)) AddRotationForce(rotSpeed * Vector3.back);
    }

    private Vector3 momentum;
    private Vector3 rotationMomentum;
    private const float resistence = 0.99f;
    void Momentum()
    {
        momentum *= resistence;
        rotationMomentum *= resistence;

        transform.Translate(momentum * Time.deltaTime, Space.World);
        if(!claw.IsDeployed) transform.Rotate(rotationMomentum * Time.deltaTime);
    }

    void AddForce(Vector3 force) { momentum += force; }
    void AddRotationForce(Vector3 force) { rotationMomentum += force; }

    #region - Global Calls -
    public Vector3 MovementDir => Vector3.ClampMagnitude(momentum, 1);
    #endregion
}
