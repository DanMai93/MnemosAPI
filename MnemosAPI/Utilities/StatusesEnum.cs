using System.ComponentModel;

namespace MnemosAPI.Utilities
{
    public enum StatusesEnum
    {
        [Description("Bozza")]
        DRAFT = 0,

        [Description("In corso")]
        INPROGRESS = 1,

        [Description("Bloccato")]
        BLOCKED = 2,

        [Description("Chiuso")]
        CLOSED = 3
    }
}
