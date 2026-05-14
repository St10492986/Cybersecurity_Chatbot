using Cybersecurity_Chatbot.Data;
using Cybersecurity_Chatbot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cybersecurity_Chatbot.Core
{
    public class ChatbotEngine
    {
        private readonly ResponseLibrary _lib = new ResponseLibrary();
        private readonly ErrorHandler _errors = new ErrorHandler();

        // Memory
        public string UserName { get; private set; } = "Friend";
        private string? _favouriteTopic;
        private string? _lastTopic;

        // Sentiment keywords mapping
        private readonly Dictionary<string, string[]> SentimentKeywords = new()
        {
            { "worried", new[]{"worried", "scared", "anxious", "nervous"} },
            { "curious", new[]{"curious", "wonder", "interested"} },
            { "frustrated", new[]{"frustrated", "annoyed", "irritated", "confused"} }
        };

        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type something — I'm listening!";

            if (input.Length > 500)
                return "That message is a bit long. Could you keep it under 500 characters?";

            try
            {
                return BuildResponse(input);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Error] {ex.Message}");
                return _errors.GetFallback();
            }
        }

        private string BuildResponse(string input)
        {
            // --- Name capture on first interaction ---
            if (UserName == "Friend")
            {
                UserName = input.Trim();
                return $"Great to meet you, {UserName}! 😊 Ask me about passwords, phishing, privacy, malware, safe browsing, or type 'exit' to leave.";
            }

            // --- Exit commands ---
            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                input.Equals("quit", StringComparison.OrdinalIgnoreCase) ||
                input.Equals("bye", StringComparison.OrdinalIgnoreCase))
            {
                return $"Goodbye, {UserName}! Stay safe online. 🔐";
            }

            // --- Sentiment detection ---
            string? sentiment = DetectSentiment(input);
            if (sentiment != null)
            {
                string sentimentResponse = GetSentimentReaction(sentiment);
                string tip = GetFallbackTip();
                return $"{sentimentResponse}\n{tip}";
            }

            // --- Favourite topic saving ---
            if (input.Contains("interested in", StringComparison.OrdinalIgnoreCase) ||
                input.Contains("favourite topic", StringComparison.OrdinalIgnoreCase) ||
                input.Contains("like to know about", StringComparison.OrdinalIgnoreCase))
            {
                string? topic = ExtractTopic(input);
                if (topic != null)
                {
                    _favouriteTopic = topic;
                    return $"Great! I'll remember that you're interested in {topic}. It's a crucial part of staying safe online.";
                }
            }

            // --- Follow‑up helper phrases ---
            if (IsFollowUp(input) && !string.IsNullOrEmpty(_lastTopic))
            {
                string more = _lib.GetKeywordResponseForKey(_lastTopic)
                              ?? _lib.GetRandomPhishingTip();
                return $"Here’s a bit more about {_lastTopic}: {more}";
            }

            // --- Keyword responses (cybersecurity topics) ---
            string? keywordResponse = _lib.GetKeywordResponse(input);
            if (keywordResponse != null)
            {
                UpdateLastTopic(input);
                return keywordResponse;
            }

            // --- Random phishing tip if the word 'tip' is mentioned ---
            if (input.IndexOf("tip", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                _lastTopic = "phishing";
                return _lib.GetRandomPhishingTip();
            }

            // --- Personalised recall ---
            if (!string.IsNullOrEmpty(_favouriteTopic))
            {
                string? recall = _lib.GetKeywordResponseForKey(_favouriteTopic);
                if (recall != null)
                    return $"As someone interested in {_favouriteTopic}, here’s a tip: {recall}";
            }

            // --- General conversational responses ---
            string? general = _lib.GetGeneralResponse(input);
            if (general != null)
                return general;

            // --- Fallback ---
            return _errors.GetFallback();
        }

        private string? DetectSentiment(string input)
        {
            foreach (var pair in SentimentKeywords)
                foreach (var word in pair.Value)
                    if (input.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                        return pair.Key;
            return null;
        }

        private string GetSentimentReaction(string sentiment) => sentiment switch
        {
            "worried" => "It's completely understandable to feel that way. Scammers can be very convincing. Let me share some advice to help you feel safer.",
            "curious" => "Curiosity is the first step to staying safe! Here's something you might find interesting.",
            "frustrated" => "I hear you — cybersecurity can be overwhelming. Take it one step at a time. Here's a simple tip that might help.",
            _ => ""
        };

        private string GetFallbackTip() => _lib.GetRandomPhishingTip();

        private string? ExtractTopic(string input)
        {
            string[] topics = { "password", "phishing", "scam", "privacy", "malware", "browsing", "2fa", "wifi", "backup", "ransomware" };
            foreach (var t in topics)
                if (input.IndexOf(t, StringComparison.OrdinalIgnoreCase) >= 0)
                    return t;
            return null;
        }

        private bool IsFollowUp(string input)
        {
            string[] phrases = {
                "tell me more", "more details", "explain more", "explain further",
                "continue", "go on", "elaborate", "another tip"
            };
            return phrases.Any(p => input.IndexOf(p, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void UpdateLastTopic(string input)
        {
            string[] topics = { "password", "phishing", "scam", "privacy", "malware",
                                "virus", "browsing", "2fa", "two-factor", "wifi",
                                "backup", "ransomware", "social engineering" };
            foreach (var t in topics)
                if (input.IndexOf(t, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    _lastTopic = t;
                    return;
                }
        }
    }
}