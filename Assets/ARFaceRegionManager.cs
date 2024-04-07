using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using Unity.XR.CoreUtils;

public class ARFaceRegionManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] regionPrefabs;    // Face Region�� ǥ���ϱ� ���� GameObject �迭
    private ARFaceManager faceManager;    // ARFace  ������
    private XROrigin xrOrigin;


    private NativeArray<ARCoreFaceRegionData> faceRegions;    // Face Region �����͸� ���� NativeArray


    private void Awake()
    {
        faceManager = GetComponent<ARFaceManager>();
        xrOrigin = GetComponent<XROrigin>();

        for (int i = 0; i < regionPrefabs.Length; i++)
        {
            regionPrefabs[i] = Instantiate(regionPrefabs[i], xrOrigin.TrackablesParent);
        }
    }
    private void Update()
    {
        ARCoreFaceSubsystem subsystem = (ARCoreFaceSubsystem)faceManager.subsystem;

        foreach (ARFace face in faceManager.trackables)
        {
            subsystem.GetRegionPoses(face.trackableId, Allocator.Persistent, ref faceRegions);    // �ش� �󱼿��� �� Face Region�� ��ġ�� ȸ���� ��� faceRegions NativeArray�� ����

            foreach (ARCoreFaceRegionData faceRegion in faceRegions)
            {
                ARCoreFaceRegion regionType = faceRegion.region;

                regionPrefabs[(int)regionType].transform.localPosition = faceRegion.pose.position;    // Face Region �������� ��ġ ����
                regionPrefabs[(int)regionType].transform.localRotation = faceRegion.pose.rotation;    // Face Region �������� ȸ�� ����
            }
        }
    }


}