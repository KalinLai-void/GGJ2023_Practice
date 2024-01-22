using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalentSpawner : MonoBehaviour
{
    public GameObject[] types;
    public float spawnPerSecs = 2.0f;
    public int spawnNums = 1;

    public float minScale = 1.0f;
    public float maxScale = 5.0f;

    // fly settings
    public float flySpeed = 10.0f;
    public bool isHorizontalFly = true;

    private void Start()
    {
        Spawn();
    }

    private void Update()
    {
        if (Timers.IsTimerFinished("SpawnTimer")) Spawn();
    }

    private GameObject GetRandomType()
    {
        int index = Random.Range(0, types.Length);
        return types[index];
    }

    private void Spawn()
    {
        Timers.SetTimer("SpawnTimer", spawnPerSecs);
        
        for (int i = 0; i < spawnNums; i++)
        {
            // Random Spawn from types
            GameObject obj = Instantiate(GetRandomType(), transform.position, Quaternion.identity);
            
            // Random scale
            float scale = Random.Range(minScale, maxScale);
            Vector3 scaleV3 = new Vector3(scale, scale, scale);
            obj.transform.localScale = scaleV3;

            Vector3 flyDirection;
            if (isHorizontalFly) flyDirection = new Vector3(Random.Range(-180f, 180f),
                                                            0,
                                                            Random.Range(-180f, 180f));
            else flyDirection = new Vector3(Random.Range(-180f, 180f),
                                            Random.Range(-180f, 180f),
                                            Random.Range(-180f, 180f));
            obj.GetComponent<Rigidbody>().AddForce(flyDirection * flySpeed);
            obj.GetComponent<MeshRenderer>().enabled = false;

            // Enable Radar Point
            obj.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Target"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
