using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class VRMap
{
    public Transform VRTarget;
    public Transform rigTarget;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    public void Map()
    {
        rigTarget.position = VRTarget.TransformPoint(positionOffset);
        rigTarget.rotation = VRTarget.rotation * Quaternion.Euler(rotationOffset);
    }
}

public class HeadBodyRig : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI nickName, weight;
    public float weightTemp;
    PhotonView photonView;
    public VRMap head;
    public VRMap rightHand;
    public VRMap leftHand;

    public Transform headConstraint;
    Vector3 offset;

    public float turnFactor = 1f;
    public ForwardAxis forwardAxis;

    public enum ForwardAxis
    {
        blue,
        green,
        red
    }
    [SerializeField]
    bool activeManual;

    void Start()
    {
        offset = transform.position - headConstraint.position;
        photonView = GetComponent<PhotonView>();
        SetProperties(activeManual);
    }
    public void InitLocal(){
        nickName.gameObject.SetActive(false);
        weight.gameObject.SetActive(false);
        SetProperties();
        head.VRTarget = GameObject.Find("CenterEyeAnchor").transform;
        rightHand.VRTarget = GameObject.Find("RightHandAnchor").transform;
        leftHand.VRTarget = GameObject.Find("LeftHandAnchor").transform;
    }
    public void SetProperties(bool enable = true){
        photonView = GetComponent<PhotonView>();
        nickName.text = photonView.Owner.NickName;
        print(photonView.Owner.NickName);
        weight.text = "Weight : "+photonView.Owner.CustomProperties["weight"].ToString();
        weightTemp = int.Parse(photonView.Owner.CustomProperties["weight"].ToString());
        activeManual = enable;
    }

    void FixedUpdate()
    {
        if(activeManual){
            transform.position = headConstraint.position + offset;
            Vector3 projectionVector = headConstraint.up;
            switch (forwardAxis)
            {
                case ForwardAxis.green:
                    projectionVector = headConstraint.up;
                    break;
                case ForwardAxis.blue:
                    projectionVector = headConstraint.forward;
                    break;
                case ForwardAxis.red:
                    projectionVector = headConstraint.right;
                    break;
            }
            transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(projectionVector, Vector3.up).normalized, Time.deltaTime * turnFactor);

            head.Map();
            rightHand.Map();
            leftHand.Map();
        }
    }
}
