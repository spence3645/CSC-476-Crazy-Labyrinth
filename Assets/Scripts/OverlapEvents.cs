using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapEvents : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Black Hole"){
            Camera.main.GetComponent<WorldEvents>().Lost();
        }
        else if(col.tag == "Finish Line"){
            Camera.main.GetComponent<WorldEvents>().Win();
        }
        else if (col.tag == "Key") {
            Destroy(col.gameObject);
            Camera.main.GetComponent<WorldEvents>().UnlockHole();
        }
    }
}
