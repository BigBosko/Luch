using UnityEngine;

public class MechanicalDoor : MonoBehaviour
{
    private float moveDuration;
    private Vector3 closePos;
    private Vector3 openPos;
    private bool isOpen;

    private void Start()
    {
        closePos = transform.position;
        //openPos = new Vector3(closePos.x, closePos.y + 6f, closePos.z);
        openPos = new Vector3(closePos.x, closePos.y + 6f, closePos.z);
        isOpen = false;
    }

    public void ToggleDoor()
    {

        if (isOpen)
        {
            isOpen = false;
            transform.position = closePos;
        }
        else
        {
            isOpen = true;
            transform.position = openPos;
        }
    }

}
