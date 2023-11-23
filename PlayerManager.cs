using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Ô_Ăn_Quan
{
    public class PlayerInfo
    {
        #region Properties
        private string name;
        private int score;
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        public int Score { get => score; set => score = value; }
        #endregion
        #region Initialize
        public PlayerInfo(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        #endregion
        #region Methods
        #endregion
    }
}
