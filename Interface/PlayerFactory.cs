using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSGame.Interface
{
    public class PlayerFactory
    {
        #region Factory Method
        public AbstractPlayer GetPlayerType(string playerType, string playerName, int playerScore)
        {
            if (playerType == null)
            {
                return null;
            }
            else if (string.Equals(playerType, "HUMAN", StringComparison.OrdinalIgnoreCase))
            {
                return new HumanPlayer(playerName,playerScore);

            }
            else if (string.Equals(playerType, "COMPUTER", StringComparison.OrdinalIgnoreCase))
            {
                return new ComputerPlayer(playerName, playerScore);

            }

            return null;
        }
        #endregion
    }
}
