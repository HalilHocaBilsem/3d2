using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHareketliNesne : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    float mesafe = 5f;
    [SerializeField]
    LayerMask mask;
    float aci = 0;
    void Start()
    {
      cam=  Camera.main;   
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        //  Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, mesafe, mask)==true)
        {
            var interactable = hit.collider.GetComponent<HareketliNesne>();
            if (interactable != null)
            {
             
                if (Input.GetKey(KeyCode.E))
                {
                    var tr = interactable.GetComponent<Transform>();
               //     tr.position = new Vector3(tr.position.x, tr.position.y + 0.05f, tr.position.z);

                    aci += 0.01f;
                    tr.rotation = Quaternion.EulerRotation(0, aci, 0);
                   // interactable.BaseEtkilesim();
                }
            }
        }
        else
        {
           
        }
    }
}
