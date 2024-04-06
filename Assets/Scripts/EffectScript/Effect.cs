// Базовый класс для всех остальных эффектов
public abstract class Effect
{
    public float Duration;
    public float Power;

    protected Unit targetUnit;

    public Effect(Unit target, float duration, float power)
    {
        targetUnit = target;
        Duration = duration;
        Power = power;
    }

    public abstract void Apply();
    public abstract void Remove();
}

// Пример реализации одного из эффектов
public class SlowEffect : Effect
{
    public SlowEffect(Unit target, float duration, float power) : base(target, duration, power)
    {

    }

    public override void Apply()
    {
        // Логика применения замедления
    }

    public override void Remove()
    {
        // Логика удаления эффекта замедления
    }
}

//TODO other effects

