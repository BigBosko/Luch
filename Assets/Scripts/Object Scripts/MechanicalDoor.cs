using UnityEngine;
using UnityEngine.AI;

public class MechanicalDoor : MonoBehaviour
{
    private float moveDuration;
    private Vector3 closePos;
    private Vector3 openPos;
    private bool isOpen;
    [SerializeField] private bool isStorageDoor;
    [SerializeField] private NavMeshAgent agent;

    private void Start()
    {
        closePos = transform.position;
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
            if (isStorageDoor)
            {
                agent.isStopped = false;
            }
        }
    }

}
