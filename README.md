# Cybersecurity Awareness Chatbot (WPF)

Part 2 of the Cybersecurity Awareness Chatbot – now with a Graphical User Interface, keyword recognition, random responses, conversation flow, memory, and sentiment detection.

## Features
- Clean WPF interface with ASCII art banner
- Voice greeting on startup
- Recognises 12+ cybersecurity keywords (password, scam, phishing, privacy, malware, etc.)
- Random tips for each keyword and dedicated phishing tips
- Natural conversation flow with follow‑up support ("tell me more", "explain further")
- Memory: remembers user's name and favourite cybersecurity topic
- Sentiment detection: adjusts tone for worried, curious, or frustrated users and delivers automatic guidance
- Robust error handling and fallback responses

## How to Run
1. Open the solution in Visual Studio
2. Build and run (F5)
3. Place `VoiceGreeting.wav` in the output folder (see setup instructions)
4. Interact with the chatbot via the GUI

## Project Structure
- `Core/ChatbotEngine.cs` – main conversation logic
- `Data/ResponseLibrary.cs` – keyword, general, and phishing tip responses
- `Utils/ErrorHandler.cs` – validation and fallback messages
- `Utils/VoiceGreeting.cs` – plays greeting audio
- `MainWindow.xaml/.cs` – WPF user interface

## GitHub Practices
- 6+ meaningful commits
- 2+ releases with tags
