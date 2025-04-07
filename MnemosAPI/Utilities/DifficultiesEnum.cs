using System.ComponentModel;

namespace MnemosAPI.Utilities
{
    public enum DifficultiesEnum
    {
        [Description("Non specificato")]
        DEFAULT = 0,

        [Description("Base")]
        BASE = 1,

        [Description("Intermedio")]
        INTERMEDIATE = 2,

        [Description("Avanzato")]
        ADVANCED = 3
    }
}
