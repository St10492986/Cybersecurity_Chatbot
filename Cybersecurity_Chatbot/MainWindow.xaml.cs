using Cybersecurity_Chatbot.Core;
using Cybersecurity_Chatbot.Utils;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Cybersecurity_Chatbot
{
    public partial class MainWindow : Window
    {
        private readonly ChatbotEngine _chatbot;
        private string _displayName = "You";

        public MainWindow()
        {
            InitializeComponent();
            LoadAsciiArt();
            _chatbot = new ChatbotEngine();
            VoiceGreeting.Play();

            AppendBotMessage("Hello! I'm CyberBot, your Cybersecurity Awareness Assistant.");
            AppendBotMessage("What is your name?");
        }

        private void LoadAsciiArt()
        {
            string[] ascii = {
                @"  ██████╗██╗   ██╗██████╗ ███████╗██████╗ ██████╗  ██████╗ ████████╗",
                @" ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔═══██╗╚══██╔══╝",
                @" ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██████╔╝██║   ██║   ██║   ",
                @" ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██╗██║   ██║   ██║   ",
                @" ╚██████╗   ██║   ██████╔╝███████╗██║  ██║██████╔╝╚██████╔╝   ██║   ",
                @"  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═════╝  ╚═════╝   ╚═╝   ",
                @"",
                @"            ██████╗  ██████╗ ████████╗",
                @"            ██╔══██╗██╔═══██╗╚══██╔══╝",
                @"            ██████╔╝██║   ██║   ██║   ",
                @"            ██╔══██╗██║   ██║   ██║   ",
                @"            ██████╔╝╚██████╔╝   ██║   ",
                @"            ╚═════╝  ╚═════╝    ╚═╝   "
            };
            AsciiHeader.Text = string.Join("\n", ascii);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ProcessInput();
        }

        private void ProcessInput()
        {
            string userText = UserInput.Text.Trim();
            if (string.IsNullOrEmpty(userText)) return;

            AppendUserMessage(userText);
            UserInput.Clear();

            string response = _chatbot.GetResponse(userText);
            AppendBotMessage(response);

            // Update display name if we learned it
            if (_chatbot.UserName != "Friend" && _displayName == "You")
                _displayName = _chatbot.UserName;

            ChatScrollViewer.ScrollToEnd();
        }

        private void AppendBotMessage(string message)
        {
            ChatDisplay.Inlines.Add(new Run($"🤖 CyberBot: {message}\n\n")
            {
                Foreground = new SolidColorBrush(Color.FromRgb(0x00, 0xE5, 0xFF))
            });
        }

        private void AppendUserMessage(string message)
        {
            ChatDisplay.Inlines.Add(new Run($"💬 {_displayName}: {message}\n\n")
            {
                Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0xD7, 0x00))
            });
        }
    }
}