using System;
using System.Speech.Recognition;

namespace VoiceAsistant.Commands
{
    [CommandClass]
    public class TestCommand
    {
        [SpeechCommand("Test", Aliases = new string[] { "Example" })]
        public void Test(SpeechRecognizedEventArgs speech)
        {
            Console.WriteLine(speech.Result.Text);
        }
    }
}
