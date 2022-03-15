using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private GameObject _blastObject;

    private PointEffector2D _blastEffect;

    private void Awake()
    {
        if (_blastObject)
        {
            _blastObject.SetActive(true);
            _blastEffect = _blastObject.GetComponent<PointEffector2D>();
            _blastEffect.enabled = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damageable"))
        {
            collision.gameObject.transform.parent.parent.GetComponent<Health>().TakeDamage(damage);

        }
        if (gameObject.CompareTag("Destroyable"))
        {
            if (_blastObject)
            {
                Debug.Log("BOOM!");
                Debug.Log(_blastObject.name);
                BlastAnimation(collision);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
        if (collision.CompareTag("Out"))
        {
            Destroy(gameObject);
        }


    }
    public void BlastAnimation(Collider2D blastTarget)
    {
        Debug.Log(blastTarget.name);
        _blastObject.transform.position = blastTarget.transform.position;
        Animator _blastAnimation = _blastObject.GetComponent<Animator>();
        _blastAnimation.SetTrigger("Boom");
        _blastEffect.enabled = true;
    }
}
