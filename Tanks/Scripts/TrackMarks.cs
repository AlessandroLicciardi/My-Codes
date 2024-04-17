using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMarks : MonoBehaviour
{
    private Vector2 lastPosition;
    public float trackDistance = 0.2f;
    public GameObject trackPrefab;
    public Queue<GameObject> track;
    private int maxTrack = 15;

    private void Start()
    {
        track = new Queue<GameObject>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        var distanceDriven = Vector2.Distance(transform.position, lastPosition);
        if(distanceDriven >= trackDistance)
        {
            lastPosition = transform.position;
            GameObject spawnedObject;
            if(track.Count < maxTrack)
            {
                spawnedObject = Instantiate(trackPrefab);
                spawnedObject.transform.position = transform.position;
                spawnedObject.transform.rotation = transform.rotation;
            }
            else
            {
                spawnedObject = track.Dequeue();
                spawnedObject.transform.position = transform.position;
                spawnedObject.transform.rotation = transform.rotation;
                spawnedObject.SetActive(true);
            }
            track.Enqueue(spawnedObject);
        }
    }

    private void OnDestroy() 
    {
        foreach(var item in track)
        {
            Destroy(item);
        }    
    }
}
