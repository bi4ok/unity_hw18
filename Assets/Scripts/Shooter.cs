using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireSpeed;
    [SerializeField] private Transform firePoint;

    private SpriteRenderer _playerSprite;

    private void Awake()
    {
        _playerSprite = GetComponent<SpriteRenderer>();
    }

    public void Shoot(bool direction)
    {
        var firePointOffset = direction ? 1 : -1;
        GameObject currentBullet = Instantiate(bullet, firePoint.position + new Vector3(firePointOffset, 0, 0), Quaternion.identity);
        Rigidbody2D currentBulletVelocity = currentBullet.GetComponent<Rigidbody2D>();

        if (direction)
        {
            currentBulletVelocity.velocity = new Vector2(fireSpeed * 1, currentBulletVelocity.velocity.y);
        }
        else
        {
            currentBulletVelocity.velocity = new Vector2(fireSpeed * -1, currentBulletVelocity.velocity.y);
        }
    }

    public void RangeAttack()
    {
        bool rightDirection = !_playerSprite.flipX;
        Shoot(rightDirection);
    }
}
