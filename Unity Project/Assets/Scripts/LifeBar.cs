using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Image greenBar;

    public LivingEntity livingEntity;

    public Vector3 worldOffset;
    public Vector2 offset;

    Camera viewCamera;

    private void Start()
    {
        viewCamera = Camera.main;
    }

    private void Update()
    {
        if (livingEntity == null)
        {
            Destroy(gameObject);
            return;
        }
        UpdatePosition();
    }

    private void FixedUpdate()
    {
        if (livingEntity == null)
        {
            Destroy(gameObject);
            return;
        }
        UpdateLifeBar();
    }

    void UpdateLifeBar()
    {
        float percent = GetPercentalLife();
        greenBar.fillAmount = percent;
    }

    float GetPercentalLife()
    {
        return (float)livingEntity.Health / livingEntity.startingHealth;
    }

    void UpdatePosition()
    {
        Vector3 targetWorldPosition = livingEntity.transform.position + worldOffset;
        Vector2 targetScreenPosition = viewCamera.WorldToScreenPoint(targetWorldPosition);
        //Debug.Log("World: " + targetWorldPosition + "; Screen: " + targetScreenPosition);
        ((RectTransform)transform).position = targetScreenPosition;
    }
}
