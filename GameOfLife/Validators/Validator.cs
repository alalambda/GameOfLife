using System;
using System.Collections.Generic;
using System.Text;
using GameOfLife.Constants;

namespace GameOfLife.Validators
{
    public class Validator
    {
        public bool IsMaxGamesCountInputValid(int maxGamesCount)
        {
            if (maxGamesCount > ConstantValues.MaxRunningGames && maxGamesCount < ConstantValues.MinRunningGames)
                return false;
            return true;
        }

        public bool IsChosenGamesInputValid(int[] chosenGames)
        {
            for (int i = 0; i < chosenGames.Length; i++)
            {
                if (chosenGames[i] < ConstantValues.MinRunningGames && chosenGames[i] < ConstantValues.MaxRunningGames)
                    return false;
            }
            return true;
        }

        public bool IsDimensionInputValid(int dimension)
        {
            if (dimension < ConstantValues.MinDimension && dimension > ConstantValues.MaxDimension)
                return false;
            return true;
        }
    }
}
