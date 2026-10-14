using projekt_bazy_danych.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace projekt_bazy_danych.Entities
{
    public sealed class Fabryka
    {
        [Key]
        public FactoryIdentity factoryId { get; set; }
    }
}
