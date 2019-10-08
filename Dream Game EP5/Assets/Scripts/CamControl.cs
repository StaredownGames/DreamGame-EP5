using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float speed;
    public GameObject target;
    Vector2 curPos;
    Vector2 newPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;
        newPos = target.transform.position;
        transform.position = Vector2.Lerp(curPos,newPos, speed);
    }
}
