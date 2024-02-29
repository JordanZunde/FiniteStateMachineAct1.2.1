using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;
    //Start is called before the first frame update
    void Start()
    {
        transform.position = gameObject.transform.position;
    }

    public void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 directionInput = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(directionInput * speed * Time.deltaTime);

    }
    // Update is called once per frame
    void Update()
    {
      MovePlayer();
    }
}
