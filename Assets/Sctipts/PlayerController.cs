using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    Rigidbody2D rb2D;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        #region Movement
            float yatay = Input.GetAxis("Horizontal") * speed;
            rb2D.velocity = new Vector2(yatay, rb2D.velocity.y);

        #endregion

    }
}
