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
            print("arrow down");
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.up * Time.deltaTime;
            print("arrow up");
        }
    }
}
