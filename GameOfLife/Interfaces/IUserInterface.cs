using GameOfLife.Model;

namespace GameOfLife.Interfaces
{
    interface IUserInterface
    {
        int GetDimensionInput(string dimensionName);
        void LiveCellsOutput(int liveCells);
        void IterationsOutput(int iterations);
        bool IsAnyKeyPressed();
        void GameOverOutput();
        void AskForTerminateGame();
        bool IsGameRestoreRequired();
        bool IsGameSaveRequired();
        void OutputField(MatrixField MatrixField);
        void NormalizeOutputRate();
    }
}
