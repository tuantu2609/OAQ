namespace Game_Ô_Ăn_Quan
{
    public partial class fMain : Form
    {
        #region Properties
        ChessBoardManager ChessBoard;
        #endregion
        public fMain()
        {
            InitializeComponent();
            ChessBoard = new ChessBoardManager(panelChessBoard, namePlayer1, namePlayer2, Score1, Score2);
            ChessBoard.DrawChessBoard();  

        }
    }
}