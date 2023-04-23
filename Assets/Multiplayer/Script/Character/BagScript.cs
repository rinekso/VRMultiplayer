using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    [SerializeField]
    Transform _target,_pointLeft,_pointRight;
    [SerializeField]
    bool _isGrabbed = false;
    bool disable = false;
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name.Contains("Grab") && !_isGrabbed && !disable){
            _target.position = other.transform.position;
            _target.parent = null;
        }
        if(other.tag == "Hook"){
            disable = false;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.transform == _target){
            disable = true;
        }
    }
    public void SetGrab(bool val){
        _isGrabbed = val;
        if(Vector3.Distance(_target.position,_pointLeft.position) < Vector3.Distance(_target.position,_pointRight.position)){
            transform.parent.GetComponent<GenerateLine>().point1 = _pointLeft;
        }else{
            transform.parent.GetComponent<GenerateLine>().point1 = _pointRight;
        }
    }
}
