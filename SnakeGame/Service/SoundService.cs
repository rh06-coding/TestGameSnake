using System.Media;
using WMPLib;

namespace TestGameSnake.Service
{
    public static class SoundService
    {
        private static WindowsMediaPlayer bgSound;
        private static SoundPlayer eatSound;
        private static SoundPlayer loseSound;
        private static SoundPlayer chooseSound;
        private static SoundPlayer clickButtonSound;

        public static void Init()
        {
            bgSound = new WindowsMediaPlayer();
            bgSound.URL = "Resources/Sounds/background.mp3";
            bgSound.settings.setMode("loop", true);
            bgSound.settings.volume = 50;

            eatSound = new SoundPlayer("Resources/Sounds/eat.wav");
            loseSound = new SoundPlayer("Resources/Sounds/lose.wav");
            levelUpSound = new SoundPlayer("Resources/Sounds/choose.wav");
            clickButtonSound = new SoundPlayer("Resources/Sounds/clickbutton.wav");
        }

        public static void PlayBackground()
        {
            bgSound.controls.play();
        }

        public static void StopBackground()
        {
            bgSound.controls.stop();
        }

        public static void PlayEat()
        {
            eatSound.Play();
        }

        public static void PlayLose()
        {
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
