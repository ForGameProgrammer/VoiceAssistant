using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;

namespace VoiceAsistant
{
    public delegate void SpeechRecognizedEvent(object sender, SpeechRecognizedEventArgs e);

    public class SpeechRecognizer
    {
        public event SpeechRecognizedEvent SpeechRecognized;
        SpeechRecognitionEngine recognizer;

        public SpeechRecognizer()
        {
            // Create an in-process speech recognizer for the en-US locale.  
            this.recognizer = new SpeechRecognitionEngine(
                new System.Globalization.CultureInfo("en-US"));


            // Create and load a dictation grammar.  
            recognizer.LoadGrammar(new DictationGrammar());

            // Add a handler for the speech recognized event.  
            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;

            // Configure input to the speech recognizer.  
            recognizer.SetInputToDefaultAudioDevice();

            // Start asynchronous, continuous speech recognition.  
            recognizer.RecognizeAsync(RecognizeMode.Multiple);

        }

        // Handle the SpeechRecognized event.  
        void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            SpeechRecognized?.Invoke(sender, e);
        }
    }
}

