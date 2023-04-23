using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarHorizontalClip : MonoBehaviour
{
    GameObject _target;
    HookDetector _hookDetector;
    public bool _enableMove = true;
    public bool _freeMove; //false is left
    public SimpleCapsuleWithStickMovement _playerMovement;
    private void OnTriggerStay(Collider other) {
        if(other.tag == "Hook"){
            HookPlace(other.gameObject);
        }
    }
    void HookPlace(GameObject hook){
        _target = hook;
        // hook.GetComponent<HookDetector>().triggerArea.enabled = true;
        hook.transform.parent = transform;
        hook.transform.localPosition = new Vector3(-0.0866f,hook.transform.localPosition.y,0);
        hook.transform.localEulerAngles = new Vector3(-90,180,-90);

        _playerMovement._target = _target;

        _hookDetector = GameObject.FindAnyObjectByType<HookDetector>();
        _hookDetector._barHorizontal = this;
        _hookDetector.transform.position = hook.transform.position;
    }
    public void MoveHook(bool right){
        if(_enableMove){
            _target.transform.localPosition += new Vector3(0,(right) ? -.05f : .05f,0);
            _hookDetector.transform.position = _target.transform.position;
            _playerMovement.moveAllow = true;
        }else{
            if(right == _freeMove){
                _target.transform.localPosition += new Vector3(0,(right) ? -.05f : .05f,0);
                _hookDetector.transform.position = _target.transform.position;
                _playerMovement.moveAllow = true;
            }else{
                _playerMovement.moveAllow = false;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<SimpleCapsuleWithStickMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
