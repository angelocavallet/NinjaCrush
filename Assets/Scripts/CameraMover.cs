using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public string TARGET_TAG;
    public int timePerTarget;
    private CinemachineVirtualCamera vCam;
    private List<GameObject> targetList;

    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        targetList = GameObject.FindGameObjectsWithTag(TARGET_TAG).ToList<GameObject>();
        StartCoroutine(TravelBetweenTargets());
    }

    private IEnumerator TravelBetweenTargets()
    {
        int target = 0;

        while (target < targetList.Count)
        {
            yield return new WaitForSeconds(timePerTarget);
            vCam.Follow = targetList[target].transform;
            target++;

            if (target >= targetList.Count)
            {
                target = 0;
            }
        }
    }
}
