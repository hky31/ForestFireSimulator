namespace ForestFireSimulator.Application.Interfaces;

using ForestFireSimulator.Domain.Entities;

public interface IForestRepository
{
    void Save(Forest forest, string path);
    Forest Load(string path);
}