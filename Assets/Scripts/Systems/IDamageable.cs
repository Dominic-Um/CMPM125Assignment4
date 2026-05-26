using System;

public interface IDamageable
{
    public int CurrentHealth { get; }
    public int MaxHealth { get; }
    public void TakeDamage(int damage);
    public void Heal(int health);
    public event Action OnDeath; 
}
