using System;
using System.Collections.Generic;

namespace Cybersecurity_Chatbot.Data
{
    public class ResponseLibrary
    {
        private readonly Random _rng = new Random();

        public readonly Dictionary<string, string[]> KeywordResponses =
            new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
        {
            { "password", new[] {
                "Use strong, unique passwords for every account. Avoid personal details like your name or birthday.",
                "A strong password is at least 12 characters with uppercase, lowercase, numbers, and symbols.",
                "Never reuse passwords across sites. Use a password manager like Bitwarden or KeePass.",
                "Change passwords immediately if you suspect an account has been compromised." }},

            { "scam", new[] {
                "Scammers impersonate trusted organisations. Always verify the sender's identity before acting.",
                "If an offer sounds too good to be true, it almost certainly is. Never rush — scammers create urgency.",
                "Report scams to the South African Police Service (SAPS) and SABRIC.",
                "Never share OTPs, PINs, or passwords with anyone — not even people claiming to be from your bank." }},

            { "phishing", new[] {
                "Be cautious of emails asking for personal info. Check sender addresses carefully for subtle misspellings.",
                "Hover over links before clicking — the real URL often differs from what is displayed.",
                "Legitimate companies will NEVER ask for your password or PIN via email or SMS.",
                "When in doubt, navigate directly to a company's website rather than clicking any link in an email." }},

            { "privacy", new[] {
                "Review the security settings on all your accounts regularly.",
                "Limit the personal information you share on social media — cybercriminals harvest public profiles.",
                "Use a VPN when connecting to public Wi-Fi to protect your data from eavesdroppers.",
                "Read app permissions carefully before granting access — many apps request far more than they need." }},

            { "malware", new[] {
                "Keep your antivirus software up to date and run regular scans.",
                "Only download software from official, verified sources.",
                "Regular backups are your best defence against ransomware and other destructive malware." }},

            { "virus", new[] {
                "Antivirus software is your first line of defence. Ensure definitions are always up to date.",
                "Never click suspicious links or open unexpected email attachments — common virus delivery methods.",
                "Back up your important files regularly to an external drive or secure cloud storage." }},

            { "browsing", new[] {
                "Always check for HTTPS (the padlock icon) before entering any personal data on a website.",
                "Keep your browser and extensions updated to close known security vulnerabilities.",
                "Use an ad-blocker like uBlock Origin to protect against malicious advertisements." }},

            { "2fa", new[] {
                "Enable two-factor authentication (2FA) on all important accounts for an extra layer of security.",
                "Authenticator apps like Google Authenticator or Authy are more secure than SMS-based 2FA.",
                "Even if someone steals your password, 2FA prevents them from accessing your account." }},

            { "two-factor", new[] {
                "Two-factor authentication adds a second verification step, making accounts much harder to breach.",
                "Most banks, email providers, and social networks support 2FA. Enable it wherever possible." }},

            { "wifi", new[] {
                "Avoid using public Wi-Fi for banking or online shopping. Use a VPN if you must.",
                "Secure your home Wi-Fi with WPA3 encryption and a strong, unique password.",
                "Disable automatic Wi-Fi connection on your devices to prevent joining rogue hotspots." }},

            { "backup", new[] {
                "Follow the 3-2-1 rule: 3 copies of data, on 2 different media, with 1 stored offsite.",
                "Automate your backups so you never forget — manual backups are too easily skipped.",
                "Test your backups regularly to ensure they can actually be restored when needed." }},

            { "ransomware", new[] {
                "Ransomware encrypts your files and demands payment. Regular offline backups are your best defence.",
                "Never pay the ransom — it does not guarantee file recovery and funds further criminal activity.",
                "Keep all software patched and updated to close the vulnerabilities ransomware exploits." }},

            { "social engineering", new[] {
                "Social engineering manipulates people rather than technology. Always verify identities before sharing anything.",
                "Be sceptical of unexpected requests, even from apparent colleagues — their accounts may be compromised." }},
        };

        public readonly List<string> PhishingTips = new List<string>
        {
            "Be cautious of emails asking for personal information. Scammers disguise themselves as trusted organisations.",
            "Always hover over links before clicking to verify the real destination URL.",
            "Phishing emails create a false sense of urgency. Pause and verify before acting.",
            "Check sender email addresses carefully — phishers use addresses like 'support@paypa1.com' (with a '1').",
            "Legitimate organisations never ask for your PIN, password, or OTP via email or SMS.",
            "When unsure about an email, navigate directly to the company website instead of clicking any link.",
        };

        public readonly Dictionary<string, string> GeneralResponses =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "how are you",          "Running at full capacity and ready to help keep you safe! 😊" },
            { "what's your purpose",  "I'm your Cybersecurity Awareness Assistant — helping South Africans stay safe online." },
            { "what can i ask",       "Ask me about: passwords, phishing, privacy, malware, safe browsing, Wi-Fi, backups, 2FA, scams, or ransomware!" },
            { "hello",                "Hello! What cybersecurity topic can I help you with today?" },
            { "hi",                   "Hi there! Ready to boost your cybersecurity knowledge? Just ask!" },
            { "help",                 "Topics I can help with: passwords, phishing, privacy, malware, browsing, wifi, backup, 2fa, scam, ransomware." },
            { "bye",                  "Stay safe online! Goodbye! 👋" },
        };

        public string? GetKeywordResponse(string input)
        {
            foreach (var kvp in KeywordResponses)
                if (input.IndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                    return kvp.Value[_rng.Next(kvp.Value.Length)];
            return null;
        }

        public string? GetKeywordResponseForKey(string keyword)
        {
            if (KeywordResponses.TryGetValue(keyword, out var responses))
                return responses[_rng.Next(responses.Length)];
            return null;
        }

        public string GetRandomPhishingTip() =>
            PhishingTips[_rng.Next(PhishingTips.Count)];

        public string? GetGeneralResponse(string input)
        {
            foreach (var kvp in GeneralResponses)
                if (input.IndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                    return kvp.Value;
            return null;
        }
    }
}