using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game_Ô_Ăn_Quan
{
    public class ChessBoardManager
    {
        #region Properties
        private Panel chessBoard;
        private Label score1;
        private Label score2;
        private int currentPlayer;
        private TextBox playerName1;
        private TextBox playerName2;
        private List<Button> listButton;
        private List<Label> listLabel;
        private List<int> value;
        private List<int> spectvalue;
        private List<PlayerInfo> playerScore;
        private Button arrowLeft;
        private Button arrowRight;
        private List<Button> listArrow;
        public int count = 0;
        public int temp = 1;
        public int borrow = 0;
        public Panel ChessBoard
        {
            get { return chessBoard; }
            set { chessBoard = value; }
        }
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        public TextBox PlayerName1 { get => playerName1; set => playerName1 = value; }
        public TextBox PlayerName2 { get => playerName2; set => playerName2 = value; }
        public List<Button> ListButton { get => listButton; set => listButton = value; }
        public List<int> Value { get => value; set => this.value = value; }
        public List<int> Spectvalue { get => spectvalue; set => spectvalue = value; }
        public List<Label> ListLabel { get => listLabel; set => listLabel = value; }
        public List<PlayerInfo> PlayerScore { get => playerScore; set => playerScore = value; }
        public Button ArrowLeft { get => arrowLeft; set => arrowLeft = value; }
        public Button ArrowRight { get => arrowRight; set => arrowRight = value; }
        public List<Button> ListArrow { get => listArrow; set => listArrow = value; }
        public Label Score1 { get => score1; set => score1 = value; }
        public Label Score2 { get => score2; set => score2 = value; }

        #endregion
        #region Initialize
        public ChessBoardManager(Panel chessBoard, TextBox playerName1, TextBox playerName2, Label Score1, Label Score2)
        {
            this.chessBoard = chessBoard;
            //this.numOfPlayer = numOfPlayer;
            this.playerName1 = playerName1;
            this.playerName2 = playerName2;
            this.Score1 = Score1;
            this.Score2 = Score2;
            CurrentPlayer = 1;
            ChangePlayer(1, playerName2);
        }
        #endregion
        #region Methods
        public void DrawChessBoard()
        {
            ListButton = new List<Button>(); //Tao List de luu gia tri
            Value = new List<int>();
            Spectvalue = new List<int>();
            ListLabel = new List<Label>();
            ListArrow = new List<Button>();
            PlayerScore = InitPlayer("Player1", "Player2");
            Button oldBtn = new Button() { Width = 0, Location = new Point(45, 145) };
            for (int i = 0; i < Const.numOfChessTwoPlayer; i++)
            {
                //Tao chessBoard cho 2 nguoi choi
                if (i == 0 || i == 6)
                {
                    //Tao 2 button O quan va add vao chessBoard
                    Button btnSpec = new Button()
                    {
                        Width = Const.chessSpecialWidth,
                        Height = Const.chessSpecialHeight,
                        Location = new Point(oldBtn.Location.X + oldBtn.Width, oldBtn.Location.Y),
                        BackColor = Color.Transparent,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    if (i == 0) btnSpec.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\so10t.png");
                    else btnSpec.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\so10p.png");
                    btnSpec.Click += btn_Click;
                    ChessBoard.Controls.Add(btnSpec);
                    oldBtn = btnSpec;

                    // Nhet button vao matrix
                    ListButton.Add(btnSpec);
                    Value.Add(10);
                    Spectvalue.Add(i);
                }
                else
                {
                    if (i > 0 && i < 6)
                    {
                        //Tao button O dan va add vao chessBoard
                        Button btnNorm1 = new Button()
                        {
                            Width = Const.chessNormalWidth,
                            Height = Const.chessNormalHeight,
                            Location = new Point(oldBtn.Location.X + oldBtn.Width, oldBtn.Location.Y),
                            BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\so5.png"),
                            BackColor = Color.Transparent,
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Tag = i.ToString()
                        };
                        btnNorm1.Click += btn_Click;
                        ChessBoard.Controls.Add(btnNorm1);
                        oldBtn = btnNorm1;

                        //Nhet button vao matrix
                        ListButton.Add(btnNorm1);
                    }
                    else
                    {
                        //Tao button O dan va add vao chessBoard
                        Button btnNorm2 = new Button()
                        {
                            Width = Const.chessNormalWidth,
                            Height = Const.chessNormalHeight,
                            BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\so5.png"),
                            BackColor = Color.Transparent,
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Tag = i.ToString()
                        };
                        if (i == 7)
                        {
                            btnNorm2.Location = new Point(oldBtn.Location.X - Const.chessNormalWidth, oldBtn.Location.Y + Const.chessNormalHeight);
                        }
                        else
                        {
                            btnNorm2.Location = new Point(oldBtn.Location.X - Const.chessNormalWidth, oldBtn.Location.Y);
                        }
                        btnNorm2.Click += btn_Click;
                        ChessBoard.Controls.Add(btnNorm2);
                        oldBtn = btnNorm2;

                        //Nhet button vao matrix
                        ListButton.Add(btnNorm2);
                    }
                    Value.Add(5);
                }
                Label label = new Label()
                {
                    Width= Const.labelWidth,
                    Height= Const.labelHeight,
                    Text = Value[i].ToString(),
                    BackColor = Color.White,
                    Location = new Point(oldBtn.Location.X, oldBtn.Location.Y)
                };
                ChessBoard.Controls.Add(label);
                label.BringToFront();
                ListLabel.Add(label);
            }

            //Tạo điểm ban đầu
            Score1.Text = PlayerScore[0].Score.ToString();
            Score2.Text = PlayerScore[1].Score.ToString();
            
            
            //Test();
        }
        private void btn_Click(object sender, EventArgs e)
        {
            count++;   
            Button btn = sender as Button;
            if (ListArrow.Count != 0)
            {
                ChessBoard.Controls.Remove(ListArrow[0]);
                ChessBoard.Controls.Remove(ListArrow[1]);
                ListArrow.Clear();
            }    
            //Tạo ra mũi tên 2 bên dùng để cho người dùng chọn hướng muốn rải
            Button arrowLeft = new Button()
            {
                Width = Const.arrowWidth,
                Height = Const.arrowHeight,
                Tag = btn.Tag,
                Location = new Point(btn.Location.X - Const.arrowWidth, (btn.Location.Y + Const.chessNormalHeight / 2) - Const.arrowHeight / 2),
                BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\mui_ten_trai.png"),
                BackgroundImageLayout = ImageLayout.Zoom
            };

            Button arrowRight = new Button()
            {
                Width = Const.arrowWidth,
                Height = Const.arrowHeight,
                Tag = btn.Tag,
                Location = new Point(btn.Location.X + Const.chessNormalWidth, (btn.Location.Y + Const.chessNormalHeight / 2) - Const.arrowHeight / 2),
                BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\mui_ten_phai.png"),
                BackgroundImageLayout = ImageLayout.Zoom
            };
            ListArrow.Add(arrowLeft);
            ListArrow.Add(arrowRight);

            //Kiểm tra xem giá trị đang click có bằng 0 hay không, nếu bằng => không làm gì cả
            if (Value[Convert.ToInt32(btn.Tag)] != 0)
            {
                if (currentPlayer == 0) //this is player 1
                {
                    if (CheckPermission(Convert.ToInt32(btn.Tag), Const.lowerBoundP1, Const.upperBoundP1))
                    {
                        //Được quyền trên button thì mới tạo 2 mũi tên 2 bên
                        ChessBoard.Controls.Add(arrowLeft);
                        ChessBoard.Controls.Add(arrowRight);
                        arrowLeft.BringToFront();
                        arrowRight.BringToFront();
                        arrowLeft.Click += arrow_Click_Left;
                        arrowRight.Click += arrow_Click_Right;
                    }
                }
                else //This is player 2
                {
                    if (CheckPermission(Convert.ToInt32(btn.Tag), Const.lowerBoundP2, Const.upperBoundP2))
                    {
                        //Được quyền trên button thì mới tạo 2 mũi tên 2 bên
                        ChessBoard.Controls.Add(arrowLeft);
                        ChessBoard.Controls.Add(arrowRight);
                        arrowLeft.BringToFront();
                        arrowRight.BringToFront();
                        arrowLeft.Click += arrow_Click_Left;
                        arrowRight.Click += arrow_Click_Right;
                    }
                }
            }
        }
        private void arrow_Click_Left(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (CurrentPlayer == 0)
            {
                LeftHandle(Convert.ToInt32(btn.Tag));

                //Cập nhật điểm
                Score1.Text = PlayerScore[0].Score.ToString();
                Score2.Text = PlayerScore[1].Score.ToString();

                if (IsEnd())
                {
                    EndGame();
                    return;
                }
                else //Nếu là người chơi thứ nhất, thì lượt tiếp theo sẽ là người chơi thứ 2, lúc này ktra xem các ô bên người chơi thứ 2 có null hay không
                {
                    if (CheckNULL(Const.lowerBoundP2, Const.upperBoundP2))
                    {
                        if (PlayerScore[1].Score >= 5)
                        {
                            PlayerScore[1].Score -= 5;
                            RaiSoi(Const.lowerBoundP2, Const.upperBoundP2);
                        }
                        else
                        {
                            borrow = 5 - PlayerScore[0].Score;
                            PlayerScore[1].Score = 0;
                            PlayerScore[0].Score -= borrow;
                            RaiSoi(Const.lowerBoundP2, Const.upperBoundP2);
                        }
                    }
                }

                if (CurrentPlayer == 0)
                    ChangePlayer(0, playerName1);
                else
                    ChangePlayer(0, playerName2);
                CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
                if (CurrentPlayer == 0)
                    ChangePlayer(1, playerName1);
                else
                    ChangePlayer(1, playerName2);
            }
            else
            {
                RightHandle(Convert.ToInt32(btn.Tag));

                //Cập nhật điểm
                Score1.Text = PlayerScore[0].Score.ToString();
                Score2.Text = PlayerScore[1].Score.ToString();

                if (IsEnd())
                {
                    EndGame();
                    return;
                }
                else // Tương tự như trên, đây là người chơi thứ 2, lượt tiếp là người chơi 1 => check người chơi 1
                {
                    if (CheckNULL(Const.lowerBoundP1, Const.upperBoundP1))
                    {
                        if (PlayerScore[0].Score >= 5)
                        {
                            PlayerScore[0].Score -= 5;
                            RaiSoi(Const.lowerBoundP1, Const.upperBoundP1);
                        }
                        else
                        {
                            borrow = 5 - PlayerScore[0].Score;
                            PlayerScore[0].Score = 0;
                            PlayerScore[1].Score -= borrow;
                            RaiSoi(Const.lowerBoundP1, Const.upperBoundP1);
                        }
                    }
                }

                if (CurrentPlayer == 0)
                    ChangePlayer(0, playerName1);
                else
                    ChangePlayer(0, playerName2);
                CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
                if (CurrentPlayer == 0)
                    ChangePlayer(1, playerName1);
                else
                    ChangePlayer(1, playerName2);
            }
            //Xóa 2 mũi tên khỏi bảng sau khi chọn hướng xong
            ChessBoard.Controls.Remove(ListArrow[0]);
            ChessBoard.Controls.Remove(ListArrow[1]);

        }
        private void arrow_Click_Right(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (CurrentPlayer == 0)
            {
                RightHandle(Convert.ToInt32(btn.Tag));

                //Cập nhật điểm
                Score1.Text = PlayerScore[0].Score.ToString();
                Score2.Text = PlayerScore[1].Score.ToString();

                if (IsEnd())
                {
                    EndGame();
                    return;
                }
                else
                {
                    if (CheckNULL(Const.lowerBoundP2, Const.upperBoundP2))
                    {
                        if (PlayerScore[1].Score >= 5)
                        {
                            PlayerScore[1].Score -= 5;
                            RaiSoi(Const.lowerBoundP2, Const.upperBoundP2);
                        }
                        else
                        {
                            borrow = 5 - PlayerScore[0].Score;
                            PlayerScore[1].Score = 0;
                            PlayerScore[0].Score -= borrow;
                            RaiSoi(Const.lowerBoundP2, Const.upperBoundP2);
                        }
                    }
                }

                if (CurrentPlayer == 0)
                    ChangePlayer(0, playerName1);
                else
                    ChangePlayer(0, playerName2);
                CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
                if (CurrentPlayer == 0)
                    ChangePlayer(1, playerName1);
                else
                    ChangePlayer(1, playerName2);
            }
            else
            {
                LeftHandle(Convert.ToInt32(btn.Tag));

                //Cập nhật điểm
                Score1.Text = PlayerScore[0].Score.ToString();
                Score2.Text = PlayerScore[1].Score.ToString();

                if (IsEnd())
                {
                    EndGame();
                    return;
                }
                else
                {
                    if (CheckNULL(Const.lowerBoundP1, Const.upperBoundP1))
                    {
                        if (PlayerScore[0].Score >= 5)
                        {
                            PlayerScore[0].Score -= 5;
                            RaiSoi(Const.lowerBoundP1, Const.upperBoundP1);
                        }
                        else
                        {
                            borrow = 5 - PlayerScore[0].Score;
                            PlayerScore[0].Score = 0;
                            PlayerScore[1].Score -= borrow;
                            RaiSoi(Const.lowerBoundP1, Const.upperBoundP1);
                        }
                    }
                }

                if (CurrentPlayer == 0)
                    ChangePlayer(0, playerName1);
                else
                    ChangePlayer(0, playerName2);
                CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
                if (CurrentPlayer == 0)
                    ChangePlayer(1, playerName1);
                else
                    ChangePlayer(1, playerName2);
            }
            //Loại mũi tên ra khỏi bàn cờ
            ChessBoard.Controls.Remove(ListArrow[0]);
            ChessBoard.Controls.Remove(ListArrow[1]);
        }
        private void ChangePlayer(int i, TextBox playerName)
        {
            if(i == 1)
            {
                playerName.Font = new Font(playerName.Font, FontStyle.Bold);
            }
            else
            {
                playerName.Font = new Font(playerName.Font, FontStyle.Regular);
            }    
        }
        private void EndGame()
        {
            MessageBox.Show("Ket thuc game");
        }
        private bool IsEnd()
        {
            for (int i = 0; i < Value.Count; i += Const.distanceSpecValue) 
            {
                if (Value[i] != 0) return false;
            }    
            return true;
        }
        private bool CheckPermission(int number, int lowerBound, int upperBound)
        {
            return number >= lowerBound && number <= upperBound;
        }
        private void GetValue(int index, int dir) //Mốt sẽ nhét i vào đây, i là số người chơi
        {
            //dir: Trái = 0, Phải = 1
            bool flag = true;
            while (flag)
            {
                if (Value[index] == 0) //Kiểm tra xem rỗng hay không
                {
                    if (dir == 0) // Trái = 0, phải = 1
                    {
                        index--;
                        if (index == -1)
                            index = Value.Count - 1;
                    }
                    else
                    {
                        index++;
                        if (index == Value.Count)
                            index = 0;
                    }
                    if (Value[index] != 0) //Giá trị kế tiếp nữa không rỗng => được ăn ô này
                    {
                        if (count == 1 && Spectvalue.Contains(Convert.ToInt32(ListButton[index].Tag))) //Count dùng để tránh trường hợp người chơi ăn ô quan ngay lượt đi đầu tiên
                            return;
                        else
                        {
                            if (CurrentPlayer == 0) //này chỉ có 2 người chơi
                            {
                                if (borrow == 0) //Nếu không có nợ giữa hai người chơi => đơn giản là cộng thêm điểm cho người chơi đó
                                {
                                    PlayerScore[0].Score += Value[index];
                                }
                                else //Nếu có nợ thì phải xét xem người chơi nào là người nợ
                                {
                                    if (PlayerScore[0].Score == 0) //Nếu người nợ là người chơi 1
                                    {
                                        if (borrow > Value[index]) //Nếu giá trị trong ô ăn được không đủ để trả nợ
                                                                   //=> giảm nợ, cộng điểm cho người chơi cho vay, người vay vẫn giữ điểm như cũ
                                        {
                                            PlayerScore[1].Score += Value[index];
                                            borrow -= Value[index];
                                        }
                                        else //Nếu giá trị trong ô ăn được đủ để trả nợ
                                             //=> nợ về 0, cộng điểm cho người chơi cho vay, người vay ăn điểm còn lại
                                        {
                                            PlayerScore[1].Score += borrow;
                                            PlayerScore[0].Score += (Value[index] - borrow);
                                            borrow = 0;
                                        }
                                    }
                                    else //Người nợ là người chơi 2
                                    {
                                        if (borrow > Value[index]) //Nếu giá trị trong ô ăn được không đủ để trả nợ
                                                                   //=> giảm nợ, cộng điểm cho người chơi cho vay, người vay vẫn giữ điểm như cũ
                                        {
                                            PlayerScore[0].Score += Value[index];
                                            borrow -= Value[index];
                                        }
                                        else //Nếu giá trị trong ô ăn được đủ để trả nợ
                                             //=> nợ về 0, cộng điểm cho người chơi cho vay, người vay ăn điểm còn lại
                                        {
                                            PlayerScore[0].Score += borrow;
                                            PlayerScore[1].Score += (Value[index] - borrow);
                                            borrow = 0;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (borrow == 0) //Nếu không có nợ giữa hai người chơi => đơn giản là cộng thêm điểm cho người chơi đó
                                {
                                    PlayerScore[1].Score += Value[index];
                                }
                                else //Nếu có nợ thì phải xét xem người chơi nào là người nợ
                                {
                                    if (PlayerScore[0].Score == 0) //Nếu người nợ là người chơi 1
                                    {
                                        if (borrow > Value[index]) //Nếu giá trị trong ô ăn được không đủ để trả nợ
                                                                   //=> giảm nợ, cộng điểm cho người chơi cho vay, người vay vẫn giữ điểm như cũ
                                        {
                                            PlayerScore[1].Score += Value[index];
                                            borrow -= Value[index];
                                        }
                                        else //Nếu giá trị trong ô ăn được đủ để trả nợ
                                             //=> nợ về 0, cộng điểm cho người chơi cho vay, người vay ăn điểm còn lại
                                        {
                                            PlayerScore[1].Score += borrow;
                                            PlayerScore[0].Score += (Value[index] - borrow);
                                            borrow = 0;
                                        }
                                    }
                                    else //Người nợ là người chơi 2
                                    {
                                        if (borrow > Value[index]) //Nếu giá trị trong ô ăn được không đủ để trả nợ
                                                                   //=> giảm nợ, cộng điểm cho người chơi cho vay, người vay vẫn giữ điểm như cũ
                                        {
                                            PlayerScore[0].Score += Value[index];
                                            borrow -= Value[index];
                                        }
                                        else //Nếu giá trị trong ô ăn được đủ để trả nợ
                                             //=> nợ về 0, cộng điểm cho người chơi cho vay, người vay ăn điểm còn lại
                                        {
                                            PlayerScore[0].Score += borrow;
                                            PlayerScore[1].Score += (Value[index] - borrow);
                                            borrow = 0;
                                        }
                                    }
                                }
                            }
                            Value[index] = 0; //Lấy điểm và set giá trị của ô bằng 0
                            ListLabel[index].Text = Value[index].ToString();


                            //Sau khi ăn, update hình ảnh trên ô
                            if (index == 0 || index == 6) //Day la o quan
                            {
                                if (index == 0)
                                    ListButton[index].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[index]}t.png");
                                else
                                    ListButton[index].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[index]}p.png");
                            }
                            else //Đây là ô dân
                                ListButton[index].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[index]}.png");

                            //Tăng hoặc giảm ô để tiếp tục kiểm tra xem được phép ăn điểm tiếp hay không
                            if (dir == 0)
                            {
                                index--;
                                if (index == -1)
                                    index = Value.Count - 1;
                            }
                            else
                            {
                                index++;
                                if (index == Value.Count)
                                    index = 0;
                            }
                        }
                    }
                    else
                        flag = false;
                }
                else
                    flag = false;
            }
        }
        private void LeftHandle(int index)
        {
            int temp = Value[index];
            Value[index] = 0;
            ListLabel[index].Text = Value[index].ToString();
            ListButton[index].BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\so0.png");
            int lastIndex = 0;
            for (int i = index - 1; temp > 0; i--)
            {
                if (i == -1)
                    i = Value.Count - 1;
                Value[i]++;
                ListLabel[i].Text = Value[i].ToString();
                if (i == 0 || i == 6) //Day la o quan
                {
                    if (i == 0)
                        ListButton[i].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[i]}t.png");
                    else
                        ListButton[i].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[i]}p.png");
                }
                else
                    ListButton[i].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[i]}.png");
                temp--;
                lastIndex = i; //lastIndex = vị trí rải tới ô cuối cùng
            }
            lastIndex--;
            if (lastIndex == -1)
                lastIndex = Value.Count - 1;

            if (Spectvalue.Contains(lastIndex)) //Ktra xem ô kế tiếp có phải ô quan hay không, nếu là ô quan thì không rải
                return;
            else
            {
                //Ktra xem ô kế tiếp có rỗng hay không
                if (Value[lastIndex] != 0) //Không rỗng và không phải ô quan => rải sỏi
                {
                    LeftHandle(lastIndex);
                }
                else //rỗng => ăn điểm
                {
                    GetValue(lastIndex, 0);
                }
            }
        }
        private void RightHandle(int index)
        {
            int temp = Value[index];
            Value[index] = 0;
            ListLabel[index].Text = Value[index].ToString();
            ListButton[index].BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\so0.png");
            int lastIndex = 0;
            for (int i = index + 1; temp > 0; i++)
            {
                if (i == Value.Count)
                    i = 0;
                Value[i]++;
                ListLabel[i].Text = Value[i].ToString();
                if (i == 0 || i == 6) //Day la o quan
                {
                    if (i == 0)
                        ListButton[i].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[i]}t.png");
                    else
                        ListButton[i].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[i]}p.png");
                }
                else
                    ListButton[i].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[i]}.png");
                temp--;
                lastIndex = i;
            }
            lastIndex++;
            if (lastIndex == Value.Count)
                lastIndex = 0;
            if (Spectvalue.Contains(lastIndex))
                return;
            else
            {
                if (Value[lastIndex] != 0)
                {
                    RightHandle(lastIndex);
                }
                else
                {
                    GetValue(lastIndex, 1);
                }    
            }
        }
        private List<PlayerInfo> InitPlayer(string name1, string name2) //Viết thêm cho player 3 và 4
        {
            List<PlayerInfo> PlayerScore = new List<PlayerInfo>();
            PlayerInfo player1 = new PlayerInfo(name1, 0);
            PlayerInfo player2 = new PlayerInfo(name2, 0);
            PlayerScore.Add(player1);
            PlayerScore.Add(player2);
            return PlayerScore;
        }
        private bool CheckNULL(int lowerBound, int upperBound)
        {
            for (int i = lowerBound; i <= upperBound; i++)
            {
                if (Value[i] != 0) return false;
            }
            return true;
        }
        private void RaiSoi(int lowerBound, int upperBound)
        {
            for(int i = lowerBound;i <= upperBound;i++)
            {
                Value[i]++;
                ListLabel[i].Text = Value[i].ToString();
                ListButton[i].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[i]}.png");
            }    
        }
        private void Test()
        {
            Value[0] = 0;
            Value[1] = 0;
            Value[2] = 0;
            Value[3] = 0;
            Value[4] = 1;
            Value[5] = 0;
            Value[6] = 1;
            Value[7] = 1;
            Value[8] = 0;
            Value[9] = 0;
            Value[10] = 0;
            Value[11] = 0;
            for (int i = 0; i < ListLabel.Count; i++)
            {
                ListLabel[i].Text = Value[i].ToString();
                if (i == 0 || i == 6) //Day la o quan
                {
                    if (i == 0)
                        ListButton[i].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[i]}t.png");
                    else
                        ListButton[i].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[i]}p.png");
                }
                else
                    ListButton[i].BackgroundImage = Image.FromFile(Application.StartupPath + $"\\Resources\\so{Value[i]}.png");
            }    
        }
        #endregion 
    }
}
