using System;
using System.Collections.Generic;

namespace Cybersecurity_Chatbot.Utils
{
    public class ErrorHandler
    {
        private readonly Random _rng = new Random();

        private readonly List<string> _fallbacks = new List<string>
        {
            "I'm not sure I understand. Could you try rephrasing?",
            "Hmm, I didn't quite catch that. Try asking about passwords, phishing, or privacy.",
            "I don't have an answer for that yet — can you rephrase or ask something else?",
            "That's outside my current knowledge. Try topics like malware, scams, or safe browsing.",
            "I'm not sure how to respond to that. What cybersecurity topic can I help you with?",
        };

        public string GetFallback() =>
            _fallbacks[_rng.Next(_fallbacks.Count)];
    }
}