using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] _components,_othersVisible,_followHands;
    public void SetNotLocal()
    {
        for (int i = 0; i < _followHands.Length; i++)
        {
            _followHands[i].GetComponent<FollowTarget>().FollowDisable();
        }
    }
    // Start is called before the first frame update
    public void SetLocal()
    {
        for (int i = 0; i < _components.Length; i++)
        {
            _components[i].SetActive(true);
            GetComponent<CharacterCameraConstraint>().enabled = true;
            GetComponent<SimpleCapsuleWithStickMovement>().enabled = true;
        }
        for (int i = 0; i < _othersVisible.Length; i++)
        {
            ChangeLayerRecursively(_othersVisible[i],6);
        }
    }
    public void ChangeLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, newLayer);
        }
    }
}
