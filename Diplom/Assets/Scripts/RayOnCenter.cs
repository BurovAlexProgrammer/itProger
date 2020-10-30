using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RayOnCenter : MonoBehaviour
{
    [SerializeField]
    GameObject prefabOnRayCollided;

    [SerializeField]
    float rayDistance = 300f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var camera = Camera.main;
        var ray = new Ray(camera.transform.position, camera.transform.forward);
        var hits = Physics.RaycastAll(ray, rayDistance);
        if (hits.Length == 0) return;
        Debug.DrawLine(Camera.main.transform.position, hits.First().point);
        var hit = hits.DefaultIfEmpty().OrderBy(h => h.distance).FirstOrDefault(h => new int[] {0,9}.Contains(h.transform.gameObject.layer));
        
        if (hit.transform != null)
        {
            prefabOnRayCollided.transform.position = hit.point;
            var dir = hit.point - camera.transform.position;
            prefabOnRayCollided.transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
