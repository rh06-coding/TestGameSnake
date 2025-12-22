using System.Media;
using WMPLib;

namespace SnakeGame.Services
{
    public static class SoundService
    {
        private static WindowsMediaPlayer bgSound;
        private static WindowsMediaPlayer igSound;
        private static SoundPlayer eatSound;
        private static SoundPlayer loseSound;
        private static SoundPlayer chooseSound;
        private static SoundPlayer clickButtonSound;

        public static void Init()
        {
            bgSound = new WindowsMediaPlayer();
            bgSound.settings.setMode("loop", true);
            bgSound.settings.volume = 40;

            igSound = new WindowsMediaPlayer();
            igSound.settings.setMode("loop", true);
            igSound.settings.volume = 40;

            eatSound = new SoundPlayer("Resources/Sounds/eat.wav");
            loseSound = new SoundPlayer("Resources/Sounds/lose.wav");
            chooseSound = new SoundPlayer("Resources/Sounds/choose.wav");
            clickButtonSound = new SoundPlayer("Resources/Sounds/clickbutton.wav");
        }

        public static void PlayBackground()
        {
            bgSound.URL = "Resources/Sounds/background.mp3";
            bgSound.controls.play();
        }

        public static void StopBackground()
        {
            bgSound.controls.stop();
        }

        public static void PlayInGame()
        {
            igSound.URL = "Resources/Sounds/ingame.mp3";
            igSound.controls.play();
        }

        public static void StopInGame()
        {
            igSound.controls.pause();
        }

        public static void PlayEat()
        {
            eatSound.Play();
        }

        public static void PlayLose()
        {
            igSound.controls.stop();
            loseSound.Play();
        }

        public static void PlayChoose()
        {
            chooseSound.Play();
        }

        public static void PlayClickButton()
        {
            clickButtonSound.Play();
        }
    }
}
