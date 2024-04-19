using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float maxPowerSpeed = 12f;
    [SerializeField] private float rotationSpeed = 90f;

    
    void Update()
    {
        controller.Move(Physics.gravity * Time.deltaTime);
        
        if (controller.isGrounded)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.forward * z;

            var powerSpeedMultiplier = Mathf.InverseLerp(0f, stats.MaxHealth, stats.CurrentHealth);
            var speed = initialSpeed + maxPowerSpeed * powerSpeedMultiplier;
            controller.SimpleMove(move * speed);
            
            transform.Rotate(Vector3.up * x * rotationSpeed * Time.deltaTime);
        }
    }
}
