using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    Vector3 startPos;
    public float currentWeight;
    [SerializeField]
    float maxWeight = 120;
    [SerializeField]
    bool overWeight = false;
    [SerializeField]
    float thresholdShake;
    List<GameObject> avatars;
    [SerializeField]
    bool disableAtFirst = false;
    // Start is called before the first frame update
    void Start()
    {
        avatars = new List<GameObject>();
        maxWeight = Random.Range(75,120);
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWeight > maxWeight){
            overWeight = true;
            transform.position = startPos + new Vector3(Random.Range(0,thresholdShake),Random.Range(0,thresholdShake),Random.Range(0,thresholdShake));
            StartCoroutine(FallingStart());
        }else{
            overWeight = false;
            transform.position = startPos;
            StopAllCoroutines();
        }
    }
    IEnumerator FallingStart(){
        yield return new WaitForSeconds(4);
        if(overWeight){
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Avatar"){
            if(!avatars.Contains(other.gameObject) && !disableAtFirst){
                avatars.Add(other.gameObject);
                print("add weight "+other.name);
                currentWeight += other.GetComponent<HeadBodyRig>().weightTemp;
            }else{
                avatars.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Avatar"){
            if(avatars.Contains(other.gameObject) && !disableAtFirst){
                avatars.Remove(other.gameObject);
                print("exit weight");
                currentWeight -= other.GetComponent<HeadBodyRig>().weightTemp;
            }else if(avatars.Contains(other.gameObject) && disableAtFirst){
                avatars.Remove(other.gameObject);
            }
        }
    }
    void CheckWeight(){

    }
}
