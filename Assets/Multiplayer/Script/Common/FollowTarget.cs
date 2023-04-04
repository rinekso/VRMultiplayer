using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform _target;
    bool _follow = true;
    public void FollowDisable(){
        _follow = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(_target != null && _follow)
            transform.position = _target.position;
    }
}
