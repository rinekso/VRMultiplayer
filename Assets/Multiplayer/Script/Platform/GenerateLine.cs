using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLine : MonoBehaviour
{
    [SerializeField]
    LineRenderer line;
    public Transform point1, point2;
    // Start is called before the first frame update
    void Start()
    {
        line.transform.parent = null;
        line.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0,point1.position);
        line.SetPosition(1,point2.position);
    }
}
