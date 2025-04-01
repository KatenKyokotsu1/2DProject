using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private List<Transform> Doors = new List<Transform>();
    [SerializeField] private int selectedDoorIndex = 0;
    public bool isChoosing = false;
    private Vector3 originalCameraPos; 
    public float cameraMoveSpeed = 5f;

    void Start()
    {
        foreach (GameObject door in GameObject.FindGameObjectsWithTag("TeleportPoint"))
        {
            Doors.Add(door.transform);
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        if (isChoosing)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                selectedDoorIndex = selectedDoorIndex + 1 ;
                MoveCameraToSelectedDoor();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                selectedDoorIndex = selectedDoorIndex - 1 ;
                MoveCameraToSelectedDoor();
            }

            if (Input.GetKey(KeyCode.Return))
            {
                StartCoroutine(TeleportToDoor());
            }
        }
        
        if (selectedDoorIndex >= Doors.Count)
        {
            selectedDoorIndex = 0;
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        originalCameraPos = Camera.main.transform.position;
        
        if (collision.gameObject.tag == "TeleportPoint")
        {
            Time.timeScale = 0.8f;
            isChoosing = true;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "TeleportPoint")
        {
            Time.timeScale = 1;
            isChoosing = false;
        }
    }
    private void MoveCameraToSelectedDoor()
    {
        StopAllCoroutines(); // Eski kamera hareketlerini iptal et
        StartCoroutine(SmoothMoveCamera(Doors[selectedDoorIndex].position));

        Animator anim = Doors[selectedDoorIndex].GetComponent<Animator>();
        anim.SetBool("isOpening",true);
    }

    IEnumerator SmoothMoveCamera(Vector3 targetPosition)
    {
        Vector3 startPos = Camera.main.transform.position;
        targetPosition.z = startPos.z; // Kameranýn Z ekseni deðiþmemeli

        float elapsedTime = 0f;
        float duration = 2f; // Kamera geçiþ süresi

        while (elapsedTime < duration)
        {
            Camera.main.transform.position = Vector3.Lerp(startPos, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime * cameraMoveSpeed;
            yield return null;
        }

        Camera.main.transform.position = targetPosition;
    }

    private IEnumerator TeleportToDoor()
    {
        isChoosing = false;

        transform.position = Doors[selectedDoorIndex].position;
        
        Animator anim = Doors[selectedDoorIndex].GetComponent<Animator>();
        anim.SetBool("isOpening",false);
        
        yield return null;
    }
}
