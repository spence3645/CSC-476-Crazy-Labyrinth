using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeComponents : MonoBehaviour
{
    [Header("Set Dynamically")]
    public SpriteRenderer renderFinish;
    public MeshCollider finishCollider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(renderFinish == null)
        {
            renderFinish = GameObject.Find("finish-line").GetComponent<SpriteRenderer>();
            finishCollider = renderFinish.GetComponentInChildren<MeshCollider>();
        }
    }
}
