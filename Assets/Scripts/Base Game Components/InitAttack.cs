using UnityEngine;

public class InitAttack : MonoBehaviour
{
    [SerializeField] protected int damage;

    public virtual void Init(float levelModifier)
    {
        damage = (int)(damage * levelModifier);
    }
}
