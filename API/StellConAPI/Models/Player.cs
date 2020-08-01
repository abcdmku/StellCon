using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StellConAPI.Models
{
    [Serializable]
    public class Player
    {
        public string PlayerTag;
        public DateTime JoinDate;
        public int totalResReceived;
        public int totalSystemsCaptured;
        public int totalSystemsLost;
        public int totalFleetsBuilt;
        public int totalFleetsDestroyed;
        public int totalFleetsLost;
        public int preferedPlayerColor;
        public int gamesWon;
        public int gamesLost;

        public Player(string inPlayerTag, 
            int inTotalResReceived, 
            int inTotalSystemsCaptured, 
            int inTotalSystemsLost, 
            int inTotalFleetsBuilt,
            int inTotalFleetsDestroyed,
            int inTotalFleetsLost,
            int inPreferedPlayerColor, 
            int inGamesWon,
            int inGamesLost)
        {
            PlayerTag = inPlayerTag;
            JoinDate = DateTime.Now;
            totalResReceived = inTotalResReceived;
            totalSystemsCaptured = inTotalSystemsCaptured;
            totalSystemsLost = inTotalSystemsLost;
            totalFleetsBuilt = inTotalFleetsBuilt;
            totalFleetsDestroyed = inTotalFleetsDestroyed;
            totalFleetsLost = inTotalFleetsLost;
            preferedPlayerColor = inPreferedPlayerColor;
            gamesWon = inGamesWon;
            gamesLost = inGamesLost;
        }
    }
}