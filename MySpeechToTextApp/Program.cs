/*  Simple app to turn speech into text
    Use "NuGet packaga manager" to get System.Speech 
    Has like-- solid 25% chance to get some words in sentence correctly.
*/

using System;
using System.Speech.Recognition;

namespace SpeechToTextApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine())
            {
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.LoadGrammar(new DictationGrammar());

                // Adjust recognition settings
                recognizer.BabbleTimeout = TimeSpan.FromSeconds(2);
                recognizer.InitialSilenceTimeout = TimeSpan.FromSeconds(3);
                recognizer.EndSilenceTimeout = TimeSpan.FromSeconds(1);


                recognizer.SpeechRecognized += (s, e) =>
                {
                    Console.WriteLine("Recognized text: " + e.Result.Text);
                };

                recognizer.RecognizeCompleted += (s, e) =>
                {
                    if (e.Error != null)
                    {
                        Console.WriteLine("Error: " + e.Error.Message);
                    }
                    else
                    {
                        Console.WriteLine("");  // just do nothing
                    }
                };

                recognizer.RecognizeAsync(RecognizeMode.Multiple);
                Console.WriteLine("Speak into your microphone.");
                Console.ReadLine();
            }
        }
    }
}
