using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public GameObject target;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Sphere")
        {
            Text textObject = target.GetComponent<Text>();

            textObject.text = "Tumor Reached";
            // print("Collided");
            // SceneManager.LoadScene(1);
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        timeLapse();
        Text textObject = target.GetComponent<Text>();
        textObject.text = "";
    }

    IEnumerator timeLapse()
    {
        yield return new WaitForSeconds(55000);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
