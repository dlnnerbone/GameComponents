namespace GameComponents;
[Flags] public enum Actions 
{
    Disabled = 0,
    Ready = 1,
    Charging = 2,
    Active = 4,
    Completed = 8,
    Interrupted = 16,
    Cooldown = 32
}

