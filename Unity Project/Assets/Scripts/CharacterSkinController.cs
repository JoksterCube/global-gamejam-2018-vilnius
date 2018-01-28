using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterSkinController : MonoBehaviour
{
    public Side[] sides;
    public bool bouncing = true;
    public float bounceTime = 0.25f;
    public float bounceAngle = 15f;
    public float bounceHeight = 0.1f;

    Side currentSide;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(Bounce());
    }

    IEnumerator Bounce()
    {
        while (!bouncing) { yield return null; }
        float percent = 0;
        float speed = 1 / bounceTime;

        Vector3 initialRot = transform.localEulerAngles;

        while (percent < 1)
        {
            percent += speed * Time.deltaTime;

            float interpolation = (1 - Mathf.Cos(Mathf.PI * percent)) / 2;
            float interpolationHeihgt = (-Mathf.Pow(percent, 2) + percent) * 4;

            float bounceHeight = Mathf.Lerp(0, this.bounceHeight, interpolationHeihgt);
            float bounceAngle = Mathf.Lerp(-this.bounceAngle, this.bounceAngle, interpolation);

            transform.localPosition = new Vector3(transform.localPosition.x, bounceHeight, transform.localPosition.z);
            transform.localEulerAngles = initialRot + Vector3.forward * bounceAngle;

            yield return null;
        }

        percent = 0;

        while (percent < 1)
        {
            percent += speed * Time.deltaTime;

            float interpolation = (1 - Mathf.Cos(Mathf.PI * percent)) / 2;
            float interpolationHeihgt = (-Mathf.Pow(percent, 2) + percent) * 4;

            float bounceHeight = Mathf.Lerp(0, this.bounceHeight, interpolationHeihgt);
            float bounceAngle = Mathf.Lerp(this.bounceAngle, -this.bounceAngle, interpolation);

            transform.localPosition = new Vector3(transform.localPosition.x, bounceHeight, transform.localPosition.z);
            transform.localEulerAngles = initialRot + Vector3.forward * bounceAngle;

            yield return null;
        }
        yield return Bounce();
    }

    private void Update()
    {
        float playerAngle = transform.parent.eulerAngles.y;
        foreach (Side s in sides)
        {
            if (IsCorrectSide(s, playerAngle))
            {
                if (!s.Equals(currentSide))
                {
                    SetSprite(s.sprite);
                    currentSide = s;
                }
                // TODO: Adjust player angle relative to camera - stand (almose) perpandicular to camera
                //Debug.Log(Vector3.Angle(viewCamera.transform.position - transform.position, Vector3.forward));
                transform.rotation = Quaternion.Euler(new Vector3(0, GetAdjustedAngle(playerAngle, s), 0));
                break;
            }
        }
    }

    float GetAdjustedAngle(float angle, Side side)
    {
        return angle + (side.startAngle + side.endAngle) / 2;
    }

    bool IsCorrectSide(Side side, float angle)
    {
        if (side.endAngle < side.startAngle)
        {
            float newEnd = side.endAngle + 360;
            float newAngle = (angle < side.startAngle) ? angle + 360 : angle;

            return side.startAngle <= newAngle && newAngle < newEnd;
        }

        return side.startAngle <= angle && angle < side.endAngle;
    }

    void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}

[System.Serializable]
public struct Side : IEqualityComparer<Side>
{
    public Sprite sprite;
    public float startAngle;
    public float endAngle;

    public Side(Sprite sprite, float startAngle, float endAngle)
    {
        this.sprite = sprite;
        this.startAngle = startAngle;
        this.endAngle = endAngle;
    }

    public bool Equals(Side x, Side y)
    {
        return x.sprite.Equals(y.sprite) && x.startAngle.Equals(y.startAngle) && x.endAngle.Equals(y.endAngle);
    }

    public int GetHashCode(Side obj)
    {
        throw new System.NotImplementedException();
    }
}