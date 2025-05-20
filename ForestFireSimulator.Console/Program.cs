using ForestFireSimulator.Domain.Entities;
using ForestFireSimulator.Domain.Services;
using ForestFireSimulator.Infrastructure.Persistence;
using ForestFireSimulator.Application.Interfaces;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        IForestRepository repository = new JsonForestRepository();
        Forest forest;

        if (File.Exists("forest.json"))
            forest = repository.Load("forest.json");
        else
        {
            forest = new Forest(20);
            forest.InitializeRandom();
            forest.Ignite(10, 10);
        }

        var simulator = new FireSimulationService(forest);

        while (true)
        {
            Console.Clear();
            Display(simulator.GetForest());
            simulator.Step();

            Console.WriteLine("[S] Save, [Q] Quit, any key to continue...");
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.S)
                repository.Save(simulator.GetForest(), "forest.json");
            else if (key == ConsoleKey.Q)
                break;

            Thread.Sleep(1500);
        }
    }

    public static void Display(Forest forest)
    {
        for (int i = 0; i < forest.Size; i++)
        {
            for (int j = 0; j < forest.Size; j++)
            {
                Console.Write(forest.Cells[i][j] switch
                {
                    TreeState.Tree => "🌲",
                    TreeState.Fire => "🔥",
                    TreeState.Ash => "⬛",
                    TreeState.Empty => "⬜",
                    _ => "?"
                });
            }
            Console.WriteLine();
        }
    }
}