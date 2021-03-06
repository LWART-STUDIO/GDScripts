using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private List<RopeSegment> _ropeSegments = new List<RopeSegment>();
    [SerializeField] private float _ropeSegLen = 0.25f;
    [SerializeField] private int _segmentLength = 35;
    [SerializeField] private float _lineWidth = 0.01f;

    // Use this for initialization
    void Start()
    {
        this._lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = gameObject.transform.position;

        for (int i = 0; i < _segmentLength; i++)
        {
            this._ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= _ropeSegLen;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRope();
    }

    private void FixedUpdate()
    {
        this.Simulate();
    }

    private void Simulate()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < this._segmentLength; i++)
        {
            RopeSegment firstSegment = this._ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this._ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        //Constrant to Mouse
        RopeSegment firstSegment = this._ropeSegments[0];
        firstSegment.posNow = gameObject.transform.position;
        this._ropeSegments[0] = firstSegment;

        for (int i = 0; i < this._segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this._ropeSegments[i];
            RopeSegment secondSeg = this._ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this._ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > _ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < _ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this._ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this._ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this._ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    private void DrawRope()
    {
        float lineWidth = this._lineWidth;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this._segmentLength];
        for (int i = 0; i < this._segmentLength; i++)
        {
            ropePositions[i] = this._ropeSegments[i].posNow;
        }

        _lineRenderer.positionCount = ropePositions.Length;
        _lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}
