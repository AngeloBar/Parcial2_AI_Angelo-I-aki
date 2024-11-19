using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float _axisH;
    float _axisV;
    public float speed;

    void Update()
    {
        _axisH = Input.GetAxisRaw("Horizontal");
        _axisV = Input.GetAxisRaw("Vertical");
        transform.rotation= Quaternion.Euler(0,0,0);
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_axisH, _axisV, 0).normalized;
        Movement(direction);
    }

    public void Movement(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("Parcial2");
        }

        if (collision.gameObject.tag == "WinZone")
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
