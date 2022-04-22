using System.Collections;
using System.Collections.Generic;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;
using Random = System.Random;

public class PathSpawner_Anticipation : MonoBehaviour
{
    public PathFollower followerPrefab;
    public Transform spawnPoint;
    public PathCreator[] pathPrefabs;

    private void Start()
    {
        //SpawnRandomPath();
    }

    public void SpawnRandomPath()
    {
        Random random = new Random();
        int index = random.Next(0, pathPrefabs.Length);
        PathCreator p = pathPrefabs[index];
        var path = Instantiate(p, spawnPoint.position, spawnPoint.rotation);
        var follower = Instantiate(followerPrefab);
        follower.pathCreator = path;
        path.transform.parent = spawnPoint;
    }
}
