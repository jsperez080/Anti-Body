using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;

    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, yOffset, player.transform.position.z - zOffset);
    }
}
