using System.Text.Json;
using ForestFireSimulator.Application.Interfaces;
using ForestFireSimulator.Domain.Entities;

namespace ForestFireSimulator.Infrastructure.Persistence;

public class JsonForestRepository : IForestRepository
{
    /// <summary>
    /// representation de la foret dans le fichier de sauvegarde
    /// </summary>
    private class ForestData
    {
        public int Size { get; set; }
        public TreeState[][] Cells { get; set; }
    }

    /// <summary>
    /// methode pour sauvegarder le status courant de la foret
    /// </summary>
    /// <param name="forest"></param>
    /// <param name="path"></param>
    public void Save(Forest forest, string path)
    {
        var data = new ForestData { Size = forest.Size, Cells = forest.Cells };
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(path, JsonSerializer.Serialize(data, options));
    }

    /// <summary>
    /// methode pour restaurer le dernier etat de la foret
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Forest Load(string path)
    {
        var json = File.ReadAllText(path);
        var data = JsonSerializer.Deserialize<ForestData>(json);
        if (data is not null)
        {
            var forest = new Forest(data.Size);
            forest.Cells = data.Cells;
            return forest;
        }
        return new Forest(5);
    }
}