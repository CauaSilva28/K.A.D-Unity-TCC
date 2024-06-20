using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : MonoBehaviour
{
    Vector3 point;
    public Camera _camera;
    Ray ray;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            ray = _camera.ScreenPointToRay(point);

            RaycastHit hit;

            float maxDistance = 20f;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.transform.gameObject.CompareTag("Inimigos"))
                {
                    
                }
            }
        }
    }
}
