using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    // Use this for initialization
    void Start () {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[0].position;
	}

    public void SetWaveConfig(WaveConfig wave)
    {
        this.waveConfig = wave;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move () {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (targetPosition == transform.position)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
	}
}
