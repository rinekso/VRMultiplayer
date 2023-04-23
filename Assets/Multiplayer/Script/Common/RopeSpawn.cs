using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject partPrefabs, parentObject;

    [SerializeField]
    [Range(1,100)]
    int length = 1;
    [SerializeField]
    float partDistance = .21f;
    [SerializeField]
    bool reset, spawn, snapFirst, snapLast;

    // Update is called once per frame
    void Update()
    {
        if(reset){
            foreach (var item in GameObject.FindGameObjectsWithTag("RopePart"))
            {
                Destroy(item);
            }
        }
        if(spawn){
            Spawn();
            spawn = false;
        }
    }
    public void Spawn(){
        int count = (int) (length / partDistance);

        for (int i = 0; i < count; i++)
        {
            GameObject tmp;
            tmp = Instantiate(partPrefabs, new Vector3(transform.position.x,transform.position.y + partDistance * (i+1),transform.position.z), Quaternion.identity, parentObject.transform);
            tmp.transform.eulerAngles = new Vector3(180,0,0);
            tmp.name = parentObject.transform.childCount.ToString();
            if(i == 0){
                Destroy(tmp.GetComponent<CharacterJoint>());
                if(snapFirst){
                    tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }else{
                tmp.GetComponent<CharacterJoint>().connectedBody = parentObject.transform.Find((parentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
        if(snapLast){
            parentObject.transform.Find(parentObject.transform.childCount.ToString()).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
