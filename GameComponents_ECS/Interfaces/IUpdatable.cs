namespace GameComponents;

public interface IUpdatable
{
    void Update();
}

public interface IUpdatable<T>
{
    void Update(in T comp);
}