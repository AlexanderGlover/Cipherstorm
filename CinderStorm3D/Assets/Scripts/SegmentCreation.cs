using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentCreation : MonoBehaviour {

    public GameObject[] prefabSegments;
    public GameObject camera;

    private GameObject[] spikeObjects;
    private Collider[] spikeColliders;
    private float totalHeight = 4.0f;
    private int prefabNum=0;

    void Start()
    {
        for(int i=0; i<4;i++)
        {
            prefabNum = Random.Range(0,8);

            float prefabHeight = prefabSegments[prefabNum].GetComponent<HeightMgr>().GetHeight();
            float prefabOffset = prefabSegments[prefabNum].GetComponent<HeightMgr>().GetOffset();
            GameObject newSegment = Instantiate(prefabSegments[prefabNum], new Vector3(0.0f, totalHeight + prefabOffset, 0.0f), Quaternion.identity) as GameObject;
            spikeColliders = newSegment.FindComponentsInChildrenWithTag<Collider>("TopSpike");
            
            for(int j = 0; j<spikeColliders.Length; j++)
            {
                spikeColliders[j].enabled = false;
                spikeColliders[j].enabled = true;
            }
            totalHeight += prefabHeight;
        }
    }

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("PlayerAvatar").transform.position.y > (totalHeight - 40))
        {
            prefabNum = Random.Range(0, 8);

            float prefabHeight = prefabSegments[prefabNum].GetComponent<HeightMgr>().GetHeight();
            float prefabOffset = prefabSegments[prefabNum].GetComponent<HeightMgr>().GetOffset();
            GameObject newSegment = Instantiate(prefabSegments[prefabNum], new Vector3(0.0f, totalHeight + prefabOffset, 0.0f), Quaternion.identity) as GameObject;
            spikeColliders = newSegment.FindComponentsInChildrenWithTag<Collider>("TopSpike");

            for (int j = 0; j < spikeColliders.Length; j++)
            {
                spikeColliders[j].enabled = false;
                spikeColliders[j].enabled = true;
            }
            totalHeight += prefabHeight;
        }
    }
}