using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float maxPowerSpeed = 12f;
    [SerializeField] private float rotationSpeed = 90f;
    private static readonly int Walking = Animator.StringToHash("walking");


    void Update()
    {
        controller.Move(Physics.gravity * Time.deltaTime);
        
        if (controller.isGrounded)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");


            Vector3 move = transform.forward * z;

            var powerSpeedMultiplier = Mathf.InverseLerp(0f, stats.MaxHealth, stats.CurrentHealth);
            animator.SetFloat(Walking, move.magnitude * (powerSpeedMultiplier + 1));
            var speed = initialSpeed + maxPowerSpeed * powerSpeedMultiplier;
            controller.SimpleMove(move * speed);
            
            transform.Rotate(Vector3.up * x * rotationSpeed * Time.deltaTime);
        }

    }
}
