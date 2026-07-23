import countries from "i18n-iso-countries";
import en from "i18n-iso-countries/langs/en.json" with { type: "json" };
import pt from "i18n-iso-countries/langs/pt.json" with { type: "json" };

countries.registerLocale(en);
countries.registerLocale(pt);

const countryMapFromAlpha3 = {
    KSA: "Arabia Saudita",
    ENG: "Inglaterra",
    SCO: "Escócia",
    WAL: "País de Gales",
    UAE: "Emirados Árabes Unidos",
    MNC: "Mônaco",
    NED: "Países Baixos",
    GER: "Alemanha",
    CRO: "Croácia",
    MGL: "Mongólia",
    GRE: "Grécia",
    BOT: "Botsuana",
    RSA: "África do Sul",
    SUI: "Suíça",
    NGR: "Nigéria",
    CRC: "Costa Rica",
    SLO: "Eslováquia",
    POR: "Portugal",
    SRI: "Sri Lanka",
    MAS: "Malásia",
    KOS: "Kosovo",
    IOM: "Ilha de Man",
    ONL: "Online",
    TBD: "A Ser Determinado",
    TBA: "A Ser Anunciado"
};

const countryMapFromAlpha2 = {
    "GB-ENG": "Inglaterra",
    "GB-SCT": "Escócia",
    "GB-WLS": "País de Gales",
    "GB-NIR": "Irlanda do Norte",
	ARAB: "Liga Árabe",
	ASEAN: "ASEAN",
	CEFTA: "CEFTA",
	EAC: "EAC",
	"ES-CT": "Catalunha",
	"ES-GA": "Galiza",
	"ES-PV": "País Basco",
	"SH-AC": "Ilha de Ascensão",
	"SH-HL": "Santa Helena",
	"SH-TA": "Tristão da Cunha",
    "MC": "Mônaco",
    "Suiça": "CH",
};

const countryMapAlpha3FromName = {
    "Arábia Saudita": "KSA",
    "Inglaterra": "ENG",
    "Escócia": "SCO",
    "País de Gales": "WAL",
    "Emirados Árabes Unidos": "UAE",
    "Mônaco": "MNC",
    "Países Baixos": "NED",
    "Alemanha": "GER",
    "Croácia": "CRO",
    "Mongólia": "MGL",
    "Grécia": "GRE",
    "Botsuana": "BOT",
    "África do Sul": "RSA",
    "Suiça": "SUI",
    "Suíça": "SUI",
    "Nigéria": "NGR",
    "Costa Rica": "CRC",
    "Eslováquia": "SLO",
    "Portugal": "POR",
    "Sri Lanka": "SRI",
    "Malásia": "MAS",
    "Kosovo": "KOS",
    "Ilha de Man": "IOM",
    "Online": "ONL",
    "A Ser Determinado": "TBD",
    "A Ser Anunciado": "TBA"
};

const countryMapAlpha2FromName = {
    "Inglaterra": "GB-ENG",
    "Escócia": "GB-SCT",
    "País de Gales": "GB-WLS",
    "Irlanda do Norte": "GB-NIR",
    "Liga Árabe": "ARAB",
    "ASEAN": "ASEAN",
    "CEFTA": "CEFTA",
    "EAC": "EAC",
    "Catalunha": "ES-CT",
    "Galiza": "ES-GA",
    "País Basco": "ES-PV",
    "Ilha de Ascensão": "SH-AC",
    "Santa Helena": "SH-HL",
    "Tristão da Cunha": "SH-TA",
    "Mônaco": "MC",
    "Suiça": "CH",
};

export function getCountryNameFromAlpha3(countryCode) {
    if (!countryCode) {
        return null;
    }

    countryCode = countryCode.trim().toUpperCase();

    if (countryMapFromAlpha3[countryCode]) {
        return countryMapFromAlpha3[countryCode];
    }

    const alpha2 = countries.alpha3ToAlpha2(countryCode);

    if (alpha2) {
        return countries.getName(alpha2, "pt");
    }

    return countryCode;
}

export function getCountryNameFromAlpha2(countryCode) {
    if (!countryCode) {
        return null;
    }

    countryCode = countryCode.trim().toUpperCase();

    if (countryMapFromAlpha2[countryCode]) {
        return countryMapFromAlpha2[countryCode];
    }

    const name = countries.getName(countryCode, "pt");

    if (name) {
        return name;
    }

    return countryCode;
}

export function getAlpha3FromCountryName(countryName) {
    if (!countryName) {
        return null;
    }

    countryName = countryName.trim();

    if (countryMapAlpha3FromName[countryName]) {
        return countryMapAlpha3FromName[countryName];
    }

    const alpha2 = countries.getAlpha2Code(countryName, "pt");

    if (alpha2) {
        return countries.alpha2ToAlpha3(alpha2);
    }
    return null;
}


export function getAlpha2FromCountryName(countryName) {
    if (!countryName) {
        return null;
    }

    countryName = countryName.trim();

    if (countryMapAlpha2FromName[countryName]) {
        return countryMapAlpha2FromName[countryName];
    }

    const alpha2 = countries.getAlpha2Code(countryName, "pt");

    if (alpha2) {
        return alpha2;
    }

    return null;
}