using UnityEngine;

public class Bezier
{
    public Vector3[] points;

    public Bezier()
    {
        points = new Vector3[4];
    }

    public Bezier(Vector3[] Points)
    {
        this.points = Points;
    }

    public Vector3 StartPoint
    {
        get
        {
            return points[0];
        }
        set
        {
            points[0] = value;
        }
    }

    public Vector3 EndPoint
    {
        get
        {
            return points[3];
        }
        set
        {
            points[3] = value;
        }
    }

    public Vector3 GetSegments(float interpolation)
    {
        interpolation = Mathf.Clamp01(interpolation);
        float i = 1 - interpolation;
        return (i * i * i * points[0])
            + (3 * i * i * interpolation * points[1])
            + (3 * i * interpolation * interpolation * points[2])
            + (interpolation * interpolation * interpolation * points[3]);
    }

    public Vector3[] GetSegment(int Subdivisions)
    {
        Vector3[] segments = new Vector3[Subdivisions];

        float time;
        for (int i = 0; i < Subdivisions; i++)
        {
            time = (float)i / Subdivisions;
            segments[i] = GetSegments(time);
        }

        return segments;
    }
}
