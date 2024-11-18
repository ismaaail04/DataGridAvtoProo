using System;
using System.ComponentModel.DataAnnotations;

namespace DataGridAvto.Contracts.Models
{
    /// <summary>
    /// Машина
    /// </summary>
    public class ValidAvto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Марка
        /// </summary>
        /// <intheridoc cref="Models.Mark"/>
        public Mark Mark { get; set; }

        /// <summary>
        /// Гос номер
        /// </summary>
        [Required]
        [StringLength(9, MinimumLength = 7)]
        public string Number { get; set; }

        /// <summary>
        /// Пробег
        /// </summary>
        [Required]
        [Range(0, 999999)]
        public int Probeg { get; set; }

        /// <summary>
        /// Средний расход топлива за час 
        /// </summary>
        [Required]
        [Range(0, 7)]
        public decimal AvgFuelCons { get; set; }

        /// <summary>
        /// Текущий объем топлива
        /// </summary>
        [Required]
        [Range(0, 100)]
        public decimal CurrFuel { get; set; }

        /// <summary>
        /// Стоимость аренды (за минуту)
        /// </summary>
        [Required]
        [Range(0, 1000)]
        public decimal CostRent { get; set; }
    }
}
