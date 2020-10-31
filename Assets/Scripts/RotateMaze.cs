using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMaze : MonoBehaviour
{
    //bool isControlling = false;

    [Header("Set In Inspector")]
    public float angle = 10f;
    public float smooth = 1f;

    [Header("Set Dynamically")]
    public GameObject maze; //Set outside of function
    //public Transform balancePoint;

    void Start()
    {
        //balancePoint = GameObject.Find("Balance Point").GetComponent<Transform>();
    }

    void Update()
    {
        KeyRotate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void KeyRotate(float horizontal, float vertical)
    {
        horizontal *= angle;
        vertical *= angle;

        Quaternion target = Quaternion.Euler(vertical, 0, -horizontal);

        maze.transform.rotation = Quaternion.Slerp(maze.transform.rotation, target, Time.deltaTime * smooth);
    }

    //void SendRaycast(Vector3 mousePos)
    //{
    //RaycastHit hit;

    //Ray ray = Camera.main.ScreenPointToRay(mousePos);

    //if(Physics.Raycast(ray, out hit))
    //{
    //if(hit.transform.tag == "Maze")
    //{
    //isControlling = true;
    //}
    //}
    //}

    //void Rotate()
    //{
    //Vector3 mousePos = Input.mousePosition;
    //Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
    //worldPos = new Vector3(worldPos.x, balancePoint.position.y, worldPos.z);

    //Vector3 difference = worldPos - balancePoint.position;

    //float mazeAngle = maze.transform.rotation.eulerAngles.z;

    //mazeAngle = (mazeAngle > 180) ? mazeAngle - 360 : mazeAngle;

    //maze.transform.rotation = Quaternion.Euler(Mathf.Clamp(difference.z, -10, 10), 0, Mathf.Clamp(-difference.x, -10, 10));
    //}
}
