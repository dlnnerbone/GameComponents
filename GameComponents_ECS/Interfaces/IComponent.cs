public interface IComponent
{
    void Update();
}

public interface IComponent<selectedType>
{
    void Update(ref selectedType comp);
}