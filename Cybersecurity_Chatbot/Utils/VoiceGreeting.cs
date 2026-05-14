using System.Media;
using System.Windows;

namespace Cybersecurity_Chatbot.Utils
{
    public static class VoiceGreeting
    {
        public static void Play()
        {
            try
            {
                string wavPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "VoiceGreeting.wav");

                if (System.IO.File.Exists(wavPath))
                {
                    var player = new SoundPlayer(wavPath);
                    player.Play(); // non‑blocking
                }
                else
                {
                    MessageBox.Show("Voice greeting file not found. Please place VoiceGreeting.wav in the output folder.",
                                    "Missing File", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not play voice greeting: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}