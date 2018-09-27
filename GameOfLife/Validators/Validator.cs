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
            if (ConstantValues.MinRunningGames > maxGamesCount && ConstantValues.MaxRunningGames < maxGamesCount)
                return false;
            return true;
        }

        public bool IsChosenGamesInputValid(int[] chosenGames)
        {
            for (int i = 0; i < chosenGames.Length; i++)
            {
                if (ConstantValues.MinRunningGames > chosenGames[i] && ConstantValues.MaxRunningGames < chosenGames[i])
                    return false;
            }
            return true;
        }

        public bool IsDimensionInputValid(int dimension)
        {
            if (ConstantValues.MinDimension > dimension && ConstantValues.MaxDimension < dimension)
                return false;
            return true;
        }
    }
}
