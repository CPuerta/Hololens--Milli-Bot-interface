using UnityEngine;
using System.Collections;

public class LineRender : MonoBehaviour
{
    // Creates a line renderer that obtains all the nodes in a guidancering and connects them 
    // and animates it.

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;
    private GameObject[] transform_list;

    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = lengthOfLineRenderer;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
        transform_list = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            transform_list[i] = transform.GetChild(i).gameObject;
        }


    }
    // Update is called once per frame
    void Update () {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        for (int i = 0; i < transform_list.Length; i++)
        {
            lineRenderer.SetPosition(i,transform_list[i].transform.position );
        }
       
    }
}
