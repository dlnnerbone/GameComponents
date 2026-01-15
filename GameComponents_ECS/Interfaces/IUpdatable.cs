namespace GameComponents;

public interface IUpdatable
{
    void Update();
}

public interface IUpdatable<T> where T : notnull
{
    void Update(ref T comp);
}