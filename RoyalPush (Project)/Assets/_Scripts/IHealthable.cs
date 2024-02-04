namespace HealthSpace
{
    public interface IHealthable
    {
        void TakeDamage(int takeValue);
        void Die();
    }
}