using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerController : MonoBehaviour
{
    private Vector3 flyingSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        flyingSpeed = new Vector3(7.5f, 0, 0);
    }

    private void Update()
    {
        transform.position += flyingSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right, 1.2f * Time.deltaTime, Space.World);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Knight")
        {
            gameObject.SetActive(false);
        }
    }
}
