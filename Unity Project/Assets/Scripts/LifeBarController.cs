using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarController : MonoBehaviour
{
    public LifeBar prefab;
    public Transform lifeBarParent;

    private void Start()
    {
        LifeBar lifeBar = Instantiate(prefab, lifeBarParent) as LifeBar;
        lifeBar.livingEntity = GetComponent<LivingEntity>();
    }
}
