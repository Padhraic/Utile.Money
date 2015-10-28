﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Useful.Money
{
	public class Currency
	{
		public CurrencyCodes CurrencyCode { get; private set; }
		public string Code { get; private set; }
		public NumberFormatInfo NumberFormat { get; private set; }
		public string Symbol { get; private set; }
		public string EnglishName { get; private set; }

		/// <summary>
		/// Constructs a currency object with a NumberFormatInfo.
		/// </summary>
		/// <param name="currencyCode"></param>
		public Currency(CurrencyCodes currencyCode)
		{
			CurrencyCode = currencyCode;
			Code = Enum.GetName(typeof(CurrencyCodes), CurrencyCode);
			var cultureInfo = CultureInfoFromCurrencyISO(Code);
            if(cultureInfo == null)
                throw new Exception("Currency code " + Code + " is not supported by the current .Net Framework version.");
            NumberFormat = cultureInfo.NumberFormat;
			var region = new RegionInfo(cultureInfo.LCID);
			Symbol = region.CurrencySymbol;
			EnglishName = region.CurrencyEnglishName;
		}

        private Currency(CurrencyCodes currencyCode, CultureInfo cultureInfo)
        {
            CurrencyCode = currencyCode;
            Code = Enum.GetName(typeof(CurrencyCodes), CurrencyCode);
            NumberFormat = cultureInfo.NumberFormat;
            var region = new RegionInfo(cultureInfo.LCID);
            Symbol = region.CurrencySymbol;
            EnglishName = region.CurrencyEnglishName;
        }

        public static Currency Get(CurrencyCodes currencyCode)
        {
            return Exists(currencyCode) ? _currencyDictionary[currencyCode] : null;
        }

	    public static bool Exists(CurrencyCodes currencyCode)
		{
		    if (_currencyDictionary.ContainsKey(currencyCode))
		        return true;
		    var cultureInfo = CultureInfoFromCurrencyISO(Enum.GetName(typeof (CurrencyCodes), currencyCode));
            if(cultureInfo == null)
                return false;
            _currencyDictionary.Add(currencyCode, new Currency(currencyCode, cultureInfo));
            return true;
		}

		private static CultureInfo CultureInfoFromCurrencyISO(string isoCode)
		{
		    return (from ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures) let ri = new RegionInfo(ci.LCID) where ri.ISOCurrencySymbol == isoCode select ci).FirstOrDefault();
		}

	    private static Dictionary<CurrencyCodes, Currency> _currencyDictionary = new Dictionary<CurrencyCodes, Currency>();

	}

	/// <summary>
	/// Enumeration of ISO 4217 currency codes, indexed with their respective ISO 4217 numeric currency codes. 
	/// Only codes support in .Net with RegionInfo objects are listed
	/// </summary>
	public enum CurrencyCodes
	{
		AED = 784,
		AFN = 971,
		ALL = 8,
		AMD = 51,
		ARS = 32,
		AUD = 36,
		AZN = 944,
		BAM = 977,
		BDT = 50,
		BGN = 975,
		BHD = 48,
		BND = 96,
		BOB = 68,
		BRL = 986,
		BYR = 974,
		BZD = 84,
		CAD = 124,
		CHF = 756,
		CLP = 152,
		CNY = 156,
		COP = 170,
		CRC = 188,
		CZK = 203,
		DKK = 208,
		DOP = 214,
		DZD = 12,
		    EEK = 233,
		EGP = 818,
		ETB = 230,
		EUR = 978,
		GBP = 826,
		GEL = 981,
		GTQ = 320,
		HKD = 344,
		HNL = 340,
		HRK = 191,
		HUF = 348,
		IDR = 360,
		ILS = 376,
		INR = 356,
		IQD = 368,
		IRR = 364,
		ISK = 352,
		JMD = 388,
		JOD = 400,
		JPY = 392,
		KES = 404,
		KGS = 417,
		KHR = 116,
		KRW = 410,
		KWD = 414,
		KZT = 398,
		LAK = 418,
		LBP = 422,
		LKR = 144,
		    LTL = 440,
		    LVL = 428,
		LYD = 434,
		MAD = 504,
		MKD = 807,
		MNT = 496,
		MOP = 446,
		MVR = 462,
		MXN = 484,
		MYR = 458,
		NIO = 558,
		NOK = 578,
		NPR = 524,
		NZD = 554,
		OMR = 512,
		PAB = 590,
		PEN = 604,
		PHP = 608,
		PKR = 586,
		PLN = 985,
		PYG = 600,
		QAR = 634,
		RON = 946,
		RSD = 941,
		RUB = 643,
		RWF = 646,
		SAR = 682,
		SEK = 752,
		SGD = 702,
		SYP = 760,
		THB = 764,
		TJS = 972,
		TND = 788,
		TRY = 949,
		TTD = 780,
		TWD = 901,
		UAH = 980,
		USD = 840,
		UYU = 858,
		UZS = 860,
		VEF = 937,
		VND = 704,
		XOF = 952,
		YER = 886,
		ZAR = 710,
		    ZWL = 932
	}
}
