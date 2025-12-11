using SnakeGame.Forms;
using SnakeGame.Models;
using SnakeGame.Services;
using SnakeGame.Database;
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
        // Khai báo biến lưu trữ lựa chọn từ MenuForm
        private int LoaiMap;    // 1: map1 (mặc định) | 2: map2
        private int MauRan;     // 1: rắn xanh lá (mặc định) | 2: rắn đỏ | 3: rắn xanh dương

        private NewGameEngine _gameEngine;
        private const int GridSize = 20;
        private int columns;
        private int rows;

        // THÊM CÁC BIẾN HÌNH ẢNH
        private Image FoodImage;
        private Image SnakeBodyImage;

        // 4 hình ảnh cho 4 hướng của đầu rắn
        private Image SnakeHeadUpImage;
        private Image SnakeHeadDownImage;
        private Image SnakeHeadLeftImage;
        private Image SnakeHeadRightImage;
        private Image OrgSnakeHead;

        private bool _isPaused = false; // Biến để theo dõi trạng thái tạm dừng
        public GameForm(int mapduocchon, int randuocchon)
        {
            this.LoaiMap = mapduocchon;
            this.MauRan = randuocchon;

            InitializeComponent();
            
            this.KeyPreview = true;
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        
        private void VeBackground() // Hàm vẽ background dựa trên lựa chọn từ MenuForm
        {
            switch (LoaiMap)
            {
                case 1:
                    GameCanvas.BackgroundImage = Properties.Resources.GameBackground1;
                    GameCanvas.BackgroundImageLayout = ImageLayout.Stretch;
                    break;
                case 2:
                    GameCanvas.BackgroundImage = Properties.Resources.GameBackground2;
                    GameCanvas.BackgroundImageLayout = ImageLayout.Stretch;
                    break;
                default:
                    GameCanvas.BackgroundImage = Properties.Resources.GameBackground1;
                    GameCanvas.BackgroundImageLayout = ImageLayout.Stretch;
                    break;
            }
        }
        private void InitializeGame()
        {
            // Tính toán kích thước lưới dựa trên Panel
            columns = GameCanvas.Width / GridSize;
            rows = GameCanvas.Height / GridSize;

            // Khởi tạo game engine
            _gameEngine = new NewGameEngine(columns, rows);
            _gameEngine.StateChanged += GameEngine_StateChanged;
            _gameEngine.GameOver += GameEngine_GameOver;

            // Khởi tạo ảnh
            FoodImage = Properties.Resources.DefaultSnakeFood;
            VeBackground();
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

            // ⭐ AUTO-SAVE SCORE TO DATABASE
            if (SessionManager.IsLoggedIn && e.Score > 0)
            {
                bool saved = SessionManager.SaveGameScore(e.Score);
                
                if (saved)
                {
                    Debug.WriteLine($"✅ Score {e.Score} đã được lưu vào database!");
                }
                else
                {
                    Debug.WriteLine($"❌ Không thể lưu score {e.Score}");
                }
            }
            else if (!SessionManager.IsLoggedIn)
            {
                Debug.WriteLine("⚠️ User chưa đăng nhập, score không được lưu");
            }
            else if (e.Score <= 0)
            {
                Debug.WriteLine("⚠️ Score = 0, không cần lưu");
            }

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


        // Xử lý sự kiện KeyDown để điều khiển rắn và các chức năng khác
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if(_gameEngine == null)
            {
                return;
            }

            var state = _gameEngine.State;
            if (state == null) return;

            if(e.KeyCode == Keys.Escape)
            {
                TogglePause();
                e.Handled = true;
                return;
            }

            if (_isPaused) return;  // Ngăn chặn xử lý phím khác
            
            switch (e.KeyCode)      // Điều khiển rắn bằng phím mũi tên hoặc W,A,S,D hoặc phím mũi tên
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

            if (state.IsGameOver) // nếu đã thua
            {
                if (e.KeyCode == Keys.R)
                {
                    _gameEngine.reset(); // ấn R để chơi lại
                }
                else if(e.KeyCode == Keys.E)
                {
                    this.Hide();    // ấn E để thoát về menu
                    MenuForm menuForm = new MenuForm();
                    menuForm.ShowDialog();
                }
                    return;
            }

            if (e.KeyCode == Keys.Space)
            {
                GameTimer.Start();    // bắt đầu timer trong game engine
                e.Handled = true;       
                e.SuppressKeyPress = true;
                this.Focus();
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

            if (state == null)  // KIỂM TRA 2: state có tồn tại không?
            {
                return;
            }

           var food = state.Food;    // 1. Vẽ Thức ăn (Food)
            
            if (food != null)   // KIỂM TRA 3: food có tồn tại không?
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
                        // VẼ ĐẦU RẮN
                        Image HeadImage = GetSnakeHead(snake.CurrentDirection);
                        if(HeadImage != null)
                        {
                            g.DrawImage(HeadImage, rect);
                        }
                    }
                    else
                    {
                        // VẼ THÂN RẮN
                        g.DrawImage(SnakeBodyImage, rect);
                    }
                   
                }
            }

            // 3. Vẽ thông báo Game Over
            if (state.IsGameOver)
            {
                string message = $"GAME OVER!\nScore: {state.Score}\nPress: \n'R' to Restart  \n'E' to Back to menu";
                var font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
                var size = g.MeasureString(message, font);
                var x = (GameCanvas.Width - size.Width) / 2;
                var y = (GameCanvas.Height - size.Height) / 2;

                g.FillRectangle(Brushes.Black, x - 10, y - 10, size.Width + 20, size.Height + 20);
                g.DrawString(message, font, Brushes.White, x, y);
            }
        }

        private void TogglePause()
        {
            if (_gameEngine.State.IsGameOver) return;   // Nếu game kết thúc thì không cho pause

            _isPaused = !_isPaused;

            if (_isPaused)
            {
                GameTimer.Stop();
                PauseMenuPanel.Visible = true;
                PauseMenuPanel.BringToFront();
            }
            else
            {
                ResumeGame();
            }
        }

        private void ResumeGame()
        {
            _isPaused = false;
            PauseMenuPanel.Visible = false;
            GameTimer.Start();
            this.Focus();
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
            GameTimer?.Stop();
            if (_gameEngine is IDisposable d) d.Dispose();
            FoodImage?.Dispose();
            SnakeBodyImage?.Dispose();
            SnakeHeadUpImage?.Dispose();
            SnakeHeadDownImage?.Dispose();
            SnakeHeadLeftImage?.Dispose();
            SnakeHeadRightImage?.Dispose();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            InitializeGame();
            
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            _gameEngine.Tick();
            // Vẽ lại giao diện
            GameCanvas.Invalidate();
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            TogglePause();
        }

        private void ResumeBtn_Click(object sender, EventArgs e)
        {
            ResumeGame();
        }

        private void QuitToMenuBtn_Click(object sender, EventArgs e)
        {
            PauseMenuPanel.Visible = false;
            this.Hide();
            MenuForm menuForm = new MenuForm();
            menuForm.ShowDialog();
        }

        // Xử lý lỗi bị chuyển focus khi nhấn phím mũi tên
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
    
            if (_gameEngine != null && !_isPaused && !_gameEngine.State.IsGameOver)
            {
                switch (keyData)
                {
                    case Keys.Up:
                    case Keys.W:
                        _gameEngine.ChangeDirection(Direction.Huong.Up);
                        return true; 

                    case Keys.Down:
                    case Keys.S:
                        _gameEngine.ChangeDirection(Direction.Huong.Down);
                        return true;

                    case Keys.Left:
                    case Keys.A:
                        _gameEngine.ChangeDirection(Direction.Huong.Left);
                        return true;

                    case Keys.Right:
                    case Keys.D:
                        _gameEngine.ChangeDirection(Direction.Huong.Right);
                        return true;
                }
            }

            // Với các phím khác (như Space, Esc...), trả về xử lý mặc định
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GameForm_Closed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
