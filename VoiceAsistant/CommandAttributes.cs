using System;

namespace VoiceAsistant
{
    [System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class SpeechCommandAttribute : Attribute
    {
        readonly string name;

        public SpeechCommandAttribute(string name)
        {
            this.name = name;

        }

        public string Name
        {
            get { return name; }
        }

        public string[] Aliases { get; set; }
    }

    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class CommandClassAttribute : Attribute
    {
        public CommandClassAttribute()
        {

        }
    }
}
