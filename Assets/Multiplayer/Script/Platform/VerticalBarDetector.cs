using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBarDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "VerticalBar"){
            transform.parent.GetComponent<HookDetector>()._barHorizontal._enableMove = false;
            transform.parent.GetComponent<HookDetector>()._barHorizontal._freeMove = !OnRight(transform.position,other.transform.position,Vector3.up);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag == "VerticalBar")
            transform.parent.GetComponent<HookDetector>()._barHorizontal._enableMove = true;
    }
    public bool OnRight(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);
 
        if (dir > 0.0f) {
            return true;
        } else {
            return false;
        }
    }
}
