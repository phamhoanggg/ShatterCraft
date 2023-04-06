using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTF;
    [SerializeField] private float zIndex;

    private Vector3 defaultPosition;
    private Vector3 difference;
    private Vector3 origin;
    private bool drag = false;
    private int mouseUpCount;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = cameraTF.position;
        mouseUpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsCameraDragging)
        {
            if (Input.GetMouseButtonUp(0))
            {
                mouseUpCount++;
            }

            if (Input.GetMouseButton(0))
            {
                Debug.Log("Drag");
                if (drag == false)
                {
                    drag = true;
                    origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                difference = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x * 2, Input.mousePosition.y * 2 , zIndex)) - cameraTF.position;

            }
            else
            {
                drag = false;
            }

            if (drag)
            {
                cameraTF.position = origin - difference;
            }

            if (mouseUpCount == 2)
            {
                Debug.Log("Up");
                cameraTF.position = defaultPosition;
                drag = false; 
                GameManager.instance.IsCameraDragging = false;
                UI_Manager.instance.OpenUI<UIGamePlay>();
                mouseUpCount = 0;
            }
        }
    }
}
