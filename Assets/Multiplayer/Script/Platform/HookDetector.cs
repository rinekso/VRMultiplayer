using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    [SerializeField]
    bool outRange = false;
    Transform _target;
    [SerializeField]
    public BarHorizontalClip _barHorizontal;
    [SerializeField]
    bool x, y, z = false;
    public Collider triggerArea;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            outRange = false;
            _barHorizontal._playerMovement.moveAllow = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            outRange = true;
            _target = other.transform;
        }
    }
    private void Update()
    {
        if (outRange)
        {
            bool right = false;
            if (x)
            {
                right = (_target.position.x > transform.position.x);
            }
            if (y)
            {
                right = (_target.position.y > transform.position.y);
            }
            if (z)
            {
                right = (_target.position.z > transform.position.z);
            }

            if (_barHorizontal != null)
                _barHorizontal.MoveHook(right);
        }
    }
}
