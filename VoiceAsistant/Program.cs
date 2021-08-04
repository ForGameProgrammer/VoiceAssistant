using System;

namespace VoiceAsistant
{
    class Program
    {
        CommandHandler commandHandler;
        static void Main(string[] args)
        {
            new Program().App();
        }

        private void SpeechRecognizer_SpeechRecognized(object sender, System.Speech.Recognition.SpeechRecognizedEventArgs e)
        {
            commandHandler.OnSpeech(e);
        }

        private void App()
        {
            commandHandler = new();
            SpeechRecognizer speechRecognizer = new();
            speechRecognizer.SpeechRecognized += SpeechRecognizer_SpeechRecognized;
            Console.WriteLine("Speak");
            while (true)
            {
                Console.ReadKey();
            }
        }
    }
}
