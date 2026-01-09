using System.Collections;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField]
    private float destroyDelay = 3f;

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
