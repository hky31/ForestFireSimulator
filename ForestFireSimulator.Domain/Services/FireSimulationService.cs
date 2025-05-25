using ForestFireSimulator.Domain.Entities;

namespace ForestFireSimulator.Domain.Services;

using System.Text.Json;

/// <summary>
/// classe public decrivant l'expansion du feu de foret
/// </summary>
public class FireSimulationService
{
    /// <summary>
    /// variable representant la taille de la foret
    /// </summary>
    //private const int SIZE = 50;

    /// <summary>
    /// variable representant la foret
    /// </summary>
    private Forest forest;

    /// <summary>
    /// variable randomiser de chance d'avoir une case vide ou une case arbre
    /// </summary>
    private static readonly Random rand = new();

    /// <summary>
    /// constructeur par defaut
    /// </summary>
    public FireSimulationService(Forest forest)
    {
        this.forest = forest;
    }

    private class ForestData
    {
        public int Size { get; set; }
        public TreeState[][] Cells { get; set; }
    }

    /// <summary>
    /// fonction de recuperation de l'etat de la foret
    /// </summary>
    /// <returns>Retourne une copie pour éviter les effets de bord</returns>
    public Forest GetForest() => forest.Clone();

    /// <summary>
    /// etape d'expansion du feu de foret
    /// </summary>
    public void Step_old()
    {
        var newForest = forest.Clone();

        for (int i = 0; i < forest.Size; i++)
        {
            for (int j = 0; j < forest.Size; j++)
            {
                switch (forest.Cells[i][j])
                {
                    case TreeState.Empty: newForest.Cells[i][j] = TreeState.Empty; break;
                    case TreeState.Fire: newForest.Cells[i][j] = TreeState.Ash; break;
                    case TreeState.Ash: newForest.Cells[i][j] = TreeState.Ash; break;
                    case TreeState.Tree: newForest.Cells[i][j] = HasBurningNeighbor(i, j) ? TreeState.Fire : TreeState.Tree; break;
                    default: newForest.Cells[i][j] = forest.Cells[i][j]; break;
                }
            }
        }

        forest = newForest;
    }
    public void Step()
    {
        int size = forest.Size;
        TreeState[][] current = forest.Cells;
        TreeState[][] next = new TreeState[size][];

        // Initialiser le tableau "next"
        for (int i = 0; i < size; i++)
        {
            next[i] = new TreeState[size];
            for (int j = 0; j < size; j++)
            {
                TreeState state = current[i][j];

                if (state == TreeState.Fire)
                {
                    next[i][j] = TreeState.Ash;
                }
                else if (state == TreeState.Tree && HasBurningNeighbor(i, j))
                {
                    next[i][j] = TreeState.Fire;
                }
                else
                {
                    next[i][j] = state;
                }
            }
        }

        // Mise à jour de la forêt
        forest.Cells = next;
    }

    /// <summary>
    /// methode d'initialisation de la foret
    /// </summary>
    public void InitForest()
    {
        for (int i = 0; i < forest.Size; i++)
        {
            for (int j = 0; j < forest.Size; j++)
            {
                // initialise de maniere aleatoire du vide ou un arbre sur chaque case de la foret, 80% de chance d'avoir un arbre 
                forest.Cells[i][j] = rand.NextDouble() < 0.8 ? TreeState.Tree : TreeState.Empty;
            }
        }

        // initialisation des departs de feux
        forest.Ignite();
    }

    /// <summary>
    /// methode qui verifie si un arbre voisin est en feu
    /// Vérifie les 4 voisins (haut, bas, gauche, droite)
    /// </summary>
    /// <param name="x">valeur abscisse de l'arbre</param>
    /// <param name="y">valeur ordonnee de l'arbre</param>
    /// <returns>true si un voisin de l'arbre est en feu</returns>
    private bool HasBurningNeighbor(int x, int y)
    {
        // Vérifie les 4 voisins (haut, bas, gauche, droite)
        if (x > 0 && forest.Cells[x - 1][y] == TreeState.Fire) // haut
            return true;
        if (x < forest.Size - 1 && forest.Cells[x + 1][y] == TreeState.Fire) // bas
            return true;
        if (y > 0 && forest.Cells[x][y - 1] == TreeState.Fire) // gauche
            return true;
        if (y < forest.Size - 1 && forest.Cells[x][y + 1] == TreeState.Fire) // droite
            return true;
        return false;
    }

    /// <summary>
    /// methode qui sauvegarde la foret dans son etat actuel
    /// </summary>
    /// <returns></returns>
    public bool saveForest()
    {
        var data = new ForestData { Size = forest.Size, Cells = forest.Cells };
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText("forest.json", JsonSerializer.Serialize(data, options));
        return true;
    }

    /// <summary>
    /// methode qui recharge la foret dans un etat precedent
    /// </summary>
    /// <returns></returns>
    public Forest loadForest()
    {
        var json = File.ReadAllText("forest.json");
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
