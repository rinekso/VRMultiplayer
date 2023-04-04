using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTextFill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        string randomString = "";
        System.Random random = new System.Random();

        for (int i = 0; i < 5; i++)
        {
            int randomIndex = random.Next(chars.Length);
            randomString += chars[randomIndex];
        }
        GetComponent<TMPro.TMP_InputField>().text = randomString;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
