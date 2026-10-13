using projekt_bazy_danych.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace projekt_bazy_danych.Common
{
    public static class FactoryIdentityExtension
    {
        public static FactoryIdentity ToFactoryIdentity(this string value)
        {
            if (Enum.TryParse<FactoryIdentity>(value, true, out FactoryIdentity result))
            {
                return result;
            }

            throw new ArgumentException($"Bład zamiany {value} na enuma");
        }
    }
}
