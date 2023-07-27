using UnityEngine;

public class Bullet : BulletTrigger
{
    private float speed = 0;

    public void Init(int damage, float speed)
    {
        base.Init(damage);
        this.speed = speed;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
