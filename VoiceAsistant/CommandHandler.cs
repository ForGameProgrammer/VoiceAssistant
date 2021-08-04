using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace VoiceAsistant
{
    public class CommandHandler
    {
        List<CommandClassModel> Commands;
        public CommandHandler()
        {
            Commands = new();
            var classes = (from type in Assembly.GetExecutingAssembly().GetTypes().AsParallel()
                           where type.IsDefined(typeof(CommandClassAttribute))
                           select type).ToList();
            classes.ForEach(type =>
            {
                var methods = from method in type.GetMethods().AsParallel()
                              where method.IsDefined(typeof(SpeechCommandAttribute))
                              select new CommandMethodModel
                              {
                                  CommandMethod = method,
                                  Name = method.GetCustomAttribute<SpeechCommandAttribute>().Name,
                                  Aliases = method.GetCustomAttribute<SpeechCommandAttribute>().Aliases
                              };

                Commands.Add(new CommandClassModel
                {
                    ClassInstance = Activator.CreateInstance(type),
                    CommandMethods = methods.ToList()
                });
            });
        }

        public void OnSpeech(SpeechRecognizedEventArgs recognizedText)
        {
            string[] words = recognizedText.Result.Text.ToLower().Split(' ');
            foreach (string word in words)
            {
                Commands.ForEach(command =>
                {
                    command.CommandMethods.ForEach(method =>
                    {
                        if (method.Name.ToLower() == word || method.Aliases.Any(alias => alias.ToLower() == word))
                        {
                            // Run Command
                            method.CommandMethod.Invoke(command.ClassInstance, new object[] { recognizedText });
                        }
                    });
                });
            }
        }

        private class CommandClassModel
        {
            public Object ClassInstance { get; set; }
            public List<CommandMethodModel> CommandMethods { get; set; }

        }

        private class CommandMethodModel
        {
            public MethodInfo CommandMethod { get; set; }
            public string Name { get; set; }
            public string[] Aliases { get; set; }
        }
    }
}
