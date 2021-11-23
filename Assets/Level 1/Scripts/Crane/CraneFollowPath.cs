using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneFollowPath : MonoBehaviour
{
    PathCreation.Examples.PathFollower pathFollower;
    public int currentPath;
    public List<PathCreation.PathCreator> pathCreators;


    // Start is called before the first frame update
    void Start()
    {
        currentPath = 0;
        pathFollower = GetComponent<PathCreation.Examples.PathFollower>();
        pathFollower.pathCreator = pathCreators[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
