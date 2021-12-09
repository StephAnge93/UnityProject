using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("basket"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("wall"))
        {
            FindObjectOfType<Bat_Controller>().Obstecles_Touch(collision);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("basket"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("basket"))
        {
            Destroy(gameObject);
        }
    }
}
