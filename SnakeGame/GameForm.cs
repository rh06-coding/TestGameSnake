using SnakeGame.Models;
using SnakeGame.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class GameForm : Form
    {
        //Khai báo biến lưu trữ lựa chọn từ MenuForm
        private int LoaiMap;    //1: map1 (mặc định) | 2: map2
        private int MauRan;     // 1: rắn xanh lá (mặc định) | 2: rắn đỏ | 3: rắn xanh dương

        private NewGameEngine _gameEngine;
        private const int GridSize = 20;
        private int columns;
        private int rows;

        //      ===THÊM CÁC BIẾN HÌNH ẢNH===
        private Image FoodImage;
        private Image SnakeBodyImage;

        //4 hình ảnh cho 4 hướng của đầu rắn
        private Image SnakeHeadUpImage;
        private Image SnakeHeadDownImage;
        private Image SnakeHeadLeftImage;
        private Image SnakeHeadRightImage;
        private Image OrgSnakeHead;
        public GameForm(int mapduocchon, int randuocchon)
        {
            this.LoaiMap = mapduocchon;
            this.MauRan = randuocchon;

            InitializeComponent();


            this.KeyPreview = true;
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        }

        private void InitializeGame()
        {
            //tính toán kích thước lưới dựa trên Panel
            columns = GameCanvas.Width / GridSize;
            rows = GameCanvas.Height / GridSize;

            //Khởi tạo game engine
            _gameEngine = new NewGameEngine(columns, rows);
            _gameEngine.StateChanged += GameEngine_StateChanged;
            _gameEngine.GameOver += GameEngine_GameOver;

            //khởi tạo ảnh
            FoodImage = Properties.Resources.DefaultSnakeFood;
            VeRan();
            UpdateUI(_gameEngine.State);
        }

        private void VeRan()    // Hàm vẽ rắn dựa trên lựa chọn màu từ MenuForm
        {
            switch(MauRan){
                case 1:
                    SnakeBodyImage = Properties.Resources.DefaultSnakeBody;
                    OrgSnakeHead = Properties.Resources.DefaultSnakeHead;
                    break;
                case 2:
                    SnakeBodyImage = Properties.Resources.RedBody;
                    OrgSnakeHead = Properties.Resources.RedHead;
                    break;
                case 3:
                    SnakeBodyImage = Properties.Resources.BlueBody;
                    OrgSnakeHead = Properties.Resources.BlueHead;
                    break;
                default:
                    SnakeBodyImage = Properties.Resources.DefaultSnakeBody;
                    OrgSnakeHead = Properties.Resources.DefaultSnakeHead;
                    break;
            }
            SnakeHeadUpImage = (Image)OrgSnakeHead.Clone();
            SnakeHeadUpImage.RotateFlip(RotateFlipType.Rotate270FlipNone);

            SnakeHeadDownImage = (Image)OrgSnakeHead.Clone();
            SnakeHeadDownImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

            SnakeHeadLeftImage = (Image)OrgSnakeHead.Clone();
            SnakeHeadLeftImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

            SnakeHeadRightImage = (Image)OrgSnakeHead.Clone();
        }

        private void GameEngine_StateChanged(object sender, NewGameEngine.GameStateEventArgs e)
        {
            UpdateUI(e.State);  // Xử lý sự kiện State Changed từ GameEngine
        }

        // Xử lý sự kiện Game Over từ GameEngine
        private void GameEngine_GameOver(object sender, NewGameEngine.GameOverEventArgs e)
        {
            GameTimer.Stop();
            ShowGameOver(e.State);
        }

        // Hàm helper để cập nhật UI
        private void UpdateUI(GameState state)
        {
            if (state == null) return;

            // Cập nhật điểm số
            ScoreLabel.Text = $"Score: {state.Score}";

            // Yêu cầu gameCanvas vẽ lại (sẽ gọi sự kiện GameCanvas_Paint)
            GameCanvas.Invalidate();
        }

        // Hàm helper để xử lý game over
        private void ShowGameOver(GameState state)
        {
            // Chỉ cần yêu cầu vẽ lại. Logic vẽ "Game Over" nằm trong sự kiện Paint.
            GameCanvas.Invalidate();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if(_gameEngine == null)
            {
                return;
            }

            var state = _gameEngine.State;
            if (state == null) return;

            
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    _gameEngine.ChangeDirection(Direction.Huong.Up);
                    break;
                case Keys.Down:
                case Keys.S:
                    _gameEngine.ChangeDirection(Direction.Huong.Down);
                    break;
                case Keys.Left:
                case Keys.A:
                    _gameEngine.ChangeDirection(Direction.Huong.Left);
                    break;
                case Keys.Right:
                case Keys.D:
                    _gameEngine.ChangeDirection(Direction.Huong.Right);
                    break;
            }
            if (state.IsGameOver) //nếu đã thua
            {
                if (e.KeyCode == Keys.R)
                {
                    _gameEngine.reset(); //ấn R để chơi lại
                }
                return;
            }
            if (e.KeyCode == Keys.Space)
            {
                GameTimer.Start();    //bắt đầu timer trong game engine
                return;
            }
        }

        private void GameCanvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            // KIỂM TRA 1: _gameEngine có tồn tại không?
            if (_gameEngine == null)
            {
                return; // Thoát ra nếu game chưa được khởi tạo
            }

            var state = _gameEngine.State;

            // KIỂM TRA 2: state có tồn tại không?
            if (state == null)
            {
                return;
            }

            // 1. Vẽ Thức ăn (Food)
            var food = state.Food;
            // KIỂM TRA 3: food có tồn tại không?
            if (food != null)
            {
                var foodRect = new Rectangle(food.Location.X * GridSize, food.Location.Y * GridSize, GridSize, GridSize);
                g.DrawImage(FoodImage, foodRect);
            }

            // 2. Vẽ Rắn (Snake)
            var snake = state.Snake;
            // KIỂM TRA 4: Rắn và thân rắn có tồn tại không?
            if (snake != null && snake.Body != null && snake.Body.Count > 0)
            {
                for (int i = 0; i < snake.Body.Count; i++)
                {
                    var p = snake.Body[i];
                    var rect = new Rectangle(p.X * GridSize, p.Y * GridSize, GridSize, GridSize);
                    if(i == 0)
                    {
                        //VẼ ĐẦU RẮN
                        Image HeadImage = GetSnakeHead(snake.CurrentDirection);
                        if(HeadImage != null)
                        {
                            g.DrawImage(HeadImage, rect);
                        }
                    }
                    else
                    {
                        //VẼ THÂN RẮN
                        g.DrawImage(SnakeBodyImage, rect);
                    }
                   
                }
            }

            // 3. Vẽ thông báo Game Over
            if (state.IsGameOver)
            {
                string message = $"GAME OVER!\nScore: {state.Score}\nPress 'R' to Restart";
                var font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold);
                var size = g.MeasureString(message, font);
                var x = (GameCanvas.Width - size.Width) / 2;
                var y = (GameCanvas.Height - size.Height) / 2;

                g.FillRectangle(Brushes.Black, x - 10, y - 10, size.Width + 20, size.Height + 20);
                g.DrawString(message, font, Brushes.White, x, y);
            }
        }


        private Image GetSnakeHead(Direction.Huong dir)
        {
            switch (dir)
            {
                case Direction.Huong.Up:
                    return SnakeHeadUpImage;
                case Direction.Huong.Down:
                    return SnakeHeadDownImage;
                case Direction.Huong.Left:
                    return SnakeHeadLeftImage;
                case Direction.Huong.Right:
                    return SnakeHeadRightImage;
                default:
                    return SnakeHeadRightImage;
            }
        }

        private void GameForm_Closing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            _gameEngine.Tick();
            // vẽ lại giao diện
            GameCanvas.Invalidate();
        }


    }
}
