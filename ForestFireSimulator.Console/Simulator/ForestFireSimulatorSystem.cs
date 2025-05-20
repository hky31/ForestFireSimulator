using System;

namespace ForestFireSimulator.Console.Simulator;

/// <summary>
/// classe public decrivant l'expansion du feu de foret
/// </summary>
public class ForestFireSimulatorSystem
{
    /// <summary>
    /// variable representant la taille de la foret
    /// </summary>
    private const int SIZE = 20; 

    /// <summary>
    /// variable representant la foret
    /// </summary>
    private TreeState[,] forest = new TreeState[SIZE, SIZE]; 

    /// <summary>
    /// variable randomiser de chance d'avoir une case vide ou une case arbre
    /// </summary>
    private static readonly Random rand = new();

    /// <summary>
    /// constructeur par defaut
    /// </summary>
    public ForestFireSimulatorSystem() => InitForest();

    /// <summary>
    /// fonction de recuperation de l'etat de la foret
    /// </summary>
    /// <returns>Retourne une copie pour éviter les effets de bord</returns>
    public TreeState[,] GetForest() => (TreeState[,])forest.Clone();

    /// <summary>
    /// etape d'expansion du feu de foret
    /// </summary>
    public void Step()
    {
        var newForest = (TreeState[,])forest.Clone();

        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                newForest[i, j] = forest[i, j] switch
                {
                    TreeState.Empty => TreeState.Empty,
                    TreeState.Fire => TreeState.Ash,
                    TreeState.Ash => TreeState.Ash,
                    TreeState.Tree => HasBurningNeighbor(i, j) ? TreeState.Fire : TreeState.Tree,
                    _ => forest[i, j]
                };
            }
        }

        forest = newForest;
    }

    /// <summary>
    /// methode d'initialisation de la foret
    /// </summary>
    private void InitForest()
    {
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                // initialise de maniere aleatoire du vide ou un arbre sur chaque case de la foret, 80% de chance d'avoir un arbre 
                forest[i, j] = rand.NextDouble() < 0.8 ? TreeState.Tree : TreeState.Empty;
            }
        }

        // Départs de feu à différents emplacements
        forest[SIZE / 2, SIZE / 2] = TreeState.Fire;
        forest[SIZE / 4, SIZE / 2] = TreeState.Fire;
        forest[SIZE / 4, SIZE / 4] = TreeState.Fire;
    }

    /// <summary>
    /// methode qui verifie si un arbre voisin est en feu
    /// Vérifie les 4 voisins (haut, bas, gauche, droite)
    /// </summary>
    /// <param name="x">valeur abscisse de l'arbre</param>
    /// <param name="y">valeur ordonnee de l'arbre</param>
    /// <returns>true si un voisin de l'arbre est en feu</returns>
    private bool HasBurningNeighbor(int x, int y) =>
        (x > 0 && forest[x - 1, y] == TreeState.Fire) ||
        (x < SIZE - 1 && forest[x + 1, y] == TreeState.Fire) ||
        (y > 0 && forest[x, y - 1] == TreeState.Fire) ||
        (y < SIZE - 1 && forest[x, y + 1] == TreeState.Fire);
}


