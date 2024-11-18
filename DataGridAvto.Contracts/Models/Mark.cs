using System.ComponentModel;

namespace DataGridAvto.Contracts.Models
{
    /// <summary>
    /// Марка
    /// </summary>
    public enum Mark
    {
        /// <summary>
        /// Хёндай крета
        /// </summary>
        [Description("Хёндай_крета")]
        Hunday_Creta = 1,

        /// <summary>
        /// Лада веста
        /// </summary>
        [Description("Лада_веста")]
        Lada_Vesta = 2,

        /// <summary>
        /// Митсубиси аутлендер
        /// </summary>
        [Description("Митсубиси_аутлендер")]
        Mitsubishi_Autlander = 3,
    }
}
