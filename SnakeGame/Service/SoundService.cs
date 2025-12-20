using System;
using System.IO;
using System.Media;
using System.Windows.Forms;
using WMPLib;

namespace SnakeGame.Sounds
{
    public static class SoundService
    {
        private static WindowsMediaPlayer bgSound;
        private static SoundPlayer eatSound;
        private static SoundPlayer loseSound;
        private static SoundPlayer chooseSound;
        private static SoundPlayer clickButtonSound;
        private static bool isInitialized = false;

        public static void Init()
        {
            try
            {
                // Lấy đường dẫn base directory của ứng dụng
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string soundDir = Path.Combine(baseDir, "Resources", "Sound");

                // Kiểm tra thư mục sound có tồn tại không
                if (!Directory.Exists(soundDir))
                {
                    MessageBox.Show(
                        $"Thư mục sound không tồn tại:\n{soundDir}\n\nỨng dụng sẽ chạy không có âm thanh.",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Khởi tạo background music
                string bgPath = Path.Combine(soundDir, "background.mp3");
                if (File.Exists(bgPath))
                {
                    bgSound = new WindowsMediaPlayer();
                    bgSound.URL = bgPath;
                    bgSound.settings.setMode("loop", true);
                    bgSound.settings.volume = 50;
                }

                // Khởi tạo các sound effects
                string eatPath = Path.Combine(soundDir, "eat.wav");
                if (File.Exists(eatPath))
                    eatSound = new SoundPlayer(eatPath);

                string losePath = Path.Combine(soundDir, "lose.wav");
                if (File.Exists(losePath))
                    loseSound = new SoundPlayer(losePath);

                string choosePath = Path.Combine(soundDir, "choose.wav");
                if (File.Exists(choosePath))
                    chooseSound = new SoundPlayer(choosePath);

                string clickPath = Path.Combine(soundDir, "clickbutton.wav");
                if (File.Exists(clickPath))
                    clickButtonSound = new SoundPlayer(clickPath);

                isInitialized = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khởi tạo âm thanh:\n{ex.Message}\n\nỨng dụng sẽ chạy không có âm thanh.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public static void PlayBackground()
        {
            try
            {
                if (isInitialized && bgSound != null)
                    bgSound.controls.play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error playing background: {ex.Message}");
            }
        }

        public static void StopBackground()
        {
            try
            {
                if (isInitialized && bgSound != null)
                    bgSound.controls.stop();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error stopping background: {ex.Message}");
            }
        }

        public static void PlayEat()
        {
            try
            {
                if (isInitialized && eatSound != null)
                    eatSound.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error playing eat sound: {ex.Message}");
            }
        }

        public static void PlayLose()
        {
            try
            {
                if (isInitialized && loseSound != null)
                    loseSound.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error playing lose sound: {ex.Message}");
            }
        }

        public static void PlayChoose()
        {
            try
            {
                if (isInitialized && chooseSound != null)
                    chooseSound.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error playing choose sound: {ex.Message}");
            }
        }

        public static void PlayClickButton()
        {
            try
            {
                if (isInitialized && clickButtonSound != null)
                    clickButtonSound.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error playing click sound: {ex.Message}");
            }
        }
    }
}
