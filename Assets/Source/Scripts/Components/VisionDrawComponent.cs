using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDrawComponent : MonoBehaviour
{
    private float _visionAngle;
    private float _visionDistance;
    [SerializeField]
    private float _meshResolution;
    [SerializeField]
    private int _edgeResolveIterations;
    [SerializeField]
    private float _edgeDistanceThreshold;
    [SerializeField]
    private LayerMask _obstacleLayer;
    private MeshFilter _visionMeshFilter;
    private Mesh _visionMesh;

    public void LateUpdate()
    {
        DrawVision(_visionAngle, _visionDistance);
    }

    private void InitializeValues()
    {
        _visionMeshFilter = GetComponent<MeshFilter>();
        _visionMesh = new Mesh();
        _visionMesh.name = "Vision Mesh";
        _visionMeshFilter.mesh = _visionMesh;
    }

    public void UpdateVisionValues(float visionAngle, float visionDistance)
    {
        _visionAngle = visionAngle;
        _visionDistance = visionDistance;
    }

    public void DrawVision(float visionAngle, float visionDistance)
    {
        if (_visionMesh == null) InitializeValues();
        UpdateVisionValues(visionAngle, visionDistance);
        int stepCount = Mathf.RoundToInt(_visionAngle * _meshResolution);
        float stepAngle = _visionAngle / stepCount;
        List<Vector3> visionPoints = new List<Vector3>();
        VisionCastInfo oldVisionCastInfo = new VisionCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - _visionAngle / 2f + stepAngle * i;
            VisionCastInfo newVisionCastInfo = VisionCast(angle);

            if (i > 0)
            {
                bool edgeDstThresholdEsceeded = Mathf.Abs(oldVisionCastInfo.Distance - newVisionCastInfo.Distance) > _edgeDistanceThreshold;
                if (oldVisionCastInfo.WasHit != newVisionCastInfo.WasHit 
                    || (oldVisionCastInfo.WasHit && newVisionCastInfo.WasHit && edgeDstThresholdEsceeded))
                {
                    EdgeInfo edge = FindEdge(oldVisionCastInfo, newVisionCastInfo);
                    if (edge.PointA != Vector3.zero)
                    {
                        visionPoints.Add(edge.PointA);
                    }
                    if (edge.PointB != Vector3.zero)
                    {
                        visionPoints.Add(edge.PointB);
                    }
                }
            }

            visionPoints.Add(newVisionCastInfo.Point);
            oldVisionCastInfo = newVisionCastInfo;
        }

        int vertexCount = visionPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount-1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(visionPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        _visionMesh.Clear();
        _visionMesh.vertices = vertices;
        _visionMesh.triangles = triangles;
        _visionMesh.RecalculateNormals();
    }

    private EdgeInfo FindEdge(VisionCastInfo minVisionCast, VisionCastInfo maxVisionCast)
    {
        float minAngle = minVisionCast.Angle;
        float maxAngle = maxVisionCast.Angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < _edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            VisionCastInfo newVisionCast = VisionCast(angle);

            bool edgeDstThresholdEsceeded = Mathf.Abs(minVisionCast.Distance - newVisionCast.Distance) > _edgeDistanceThreshold;
            if (newVisionCast.WasHit == minVisionCast.WasHit && !edgeDstThresholdEsceeded)
            {
                minAngle = angle;
                minPoint = newVisionCast.Point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newVisionCast.Point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    private Vector3 DirFromAngle(float angleInDeg, bool isAngleGlobal)
    {
        if (!isAngleGlobal)
        {
            angleInDeg += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
    }

    VisionCastInfo VisionCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, _visionDistance, _obstacleLayer))
        {
            return new VisionCastInfo(true, hit.point, hit.distance, globalAngle);
        } else
        {
            return new VisionCastInfo(false, transform.position + dir * _visionDistance, _visionDistance, globalAngle);
        }
    }

    private struct VisionCastInfo
    {
        public bool WasHit;
        public Vector3 Point;
        public float Distance;
        public float Angle;

        public VisionCastInfo(bool wasHit, Vector3 point, float distance, float angle)
        {
            WasHit = wasHit;
            Point = point;
            Distance = distance;
            Angle = angle;
        }
    }

    private struct EdgeInfo
    {
        public Vector3 PointA;
        public Vector3 PointB;

        public EdgeInfo(Vector3 pointA, Vector3 pointB)
        {
            PointA = pointA;
            PointB = pointB;
        }
    }
}
