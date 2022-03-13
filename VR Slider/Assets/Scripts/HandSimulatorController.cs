using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSimulatorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += transform.up * -1 * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.up * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += transform.forward * -1 * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.forward * Time.deltaTime;
        }
    }
}
