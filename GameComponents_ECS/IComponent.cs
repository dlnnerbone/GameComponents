public interface IComponent
{
    public void Update();
}

public interface IComponent<selectedType>
{
    public void Update(ref selectedType comp);
}