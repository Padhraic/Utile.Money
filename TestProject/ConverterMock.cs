﻿using System;
using Utile.Money;

namespace TestProject
{
	public class ConverterMock : ICurrencyConverter
	{

		public double GetRate(CurrencyCodes fromCode, CurrencyCodes toCode, DateTime asOn)
		{
			// Don't use reflection if you want performance!
			return GetRate(Enum.GetName(typeof(CurrencyCodes), fromCode), Enum.GetName(typeof(CurrencyCodes), toCode), asOn);
		}

		public double GetRate(string fromCode, string toCode, DateTime asOn)
		{
            return 7.9;
		}
	}
}
