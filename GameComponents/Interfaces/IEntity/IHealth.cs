using System;
namespace GameComponents.Interfaces;
public interface IHealthComponent 
{
    float Health { get; set; }
    float MinHealth { get; set; }
    float MaxHealth { get; set; }
}