using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFace : MonoBehaviour
{
    [SerializeField]
    Transform _target;
    [SerializeField]
    bool useMainCam = true;
    // Start is called before the first frame update
    void Start()
    {
        if(_target == null){
            _target = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null){
            transform.LookAt(_target);
        }
    }
}
