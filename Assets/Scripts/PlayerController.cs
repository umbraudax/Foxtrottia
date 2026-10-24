using System;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody rb;
    private int _count;
    public TextMeshProUGUI countText;
    public GameObject winText;
    private float _movementX;
    private float _movementY;

    [SerializeField] private float speed = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _count = 0;
        SetCountText();
        winText.SetActive(false);
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);
        rb.AddForce(movement * speed);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            _count++;
            SetCountText();
            if (_count >= 11)
            {
                winText.SetActive(true);
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            }
        }
            
    }

    private void SetCountText()
    {
        countText.text = "Count: " + _count.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            winText.SetActive(true);
            winText.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }
    
}
