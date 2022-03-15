using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject _blastObject;
    private PointEffector2D _blastEffect;

    private void Start()
    {
        _blastObject.SetActive(true);
        _blastEffect = _blastObject.GetComponent<PointEffector2D>();
        _blastEffect.enabled = false;


    }

    public void RangeAttack()
    {
        Animator _blastAnimation = _blastObject.GetComponent<Animator>();
        _blastAnimation.SetTrigger("Boom");
        _blastEffect.enabled = true;
    }
}
