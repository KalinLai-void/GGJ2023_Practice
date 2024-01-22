using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject center;
    public float radius;
    public RenderTexture renderTexture;

    private void Start()
    {
        
    }

    private void Update()
    {
        GameObject[] radarObjs = FindGameObjectsInLayer("Radar");
        foreach(var obj in radarObjs)
        {
            if (Vector3.Distance(obj.transform.position, center.transform.position) > radius)
            {
                Destroy(obj.transform.parent.gameObject); // destroy out of range's palnets
                center.GetComponent<HP_Score>().AddHP(-1); // loss hp
            }
        }
        renderTexture.Release();
    }

    GameObject[] FindGameObjectsInLayer(string layer)
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == LayerMask.NameToLayer(layer))
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }
}
