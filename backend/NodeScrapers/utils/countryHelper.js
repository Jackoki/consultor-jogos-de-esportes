import countries from "i18n-iso-countries";
import en from "i18n-iso-countries/langs/en.json" with { type: "json" };
import pt from "i18n-iso-countries/langs/en.json" with { type: "json" };

countries.registerLocale(en);

const countryMap = {
    KSA: "Arabia Saudita",
    ENG: "Inglaterra",
    SCO: "Escócia",
    WAL: "País de Gales",
    UAE: "Emirados Árabes Unido",
    MNC: "Monaco",
    NED: "Países Baixos",
    GER: "Alemanha",
    CRO: "Croácia",
    MGL: "Mongólia",
    GRE: "Grécia",
    BOT: "Botsuana",
    RSA: "África do Sul",
    SUI: "Suiça",
    NGR: "Nigéria",
    CRC: "Costa Rica",
    POR: "Portugal",
    SRI: "Sri Lanka",
    MAS: "Malásia",
    KOS: "Kosovo",
    IOM: "Ilha de Man",
    ONL: "Online",
    TBD: "A Ser Determinado",
    TBA: "A Ser Anunciado"
};

export function getCountryName(countryCode) {
    if (!countryCode) {
        return null;
    }

    countryCode = countryCode.trim().toUpperCase();

    if (countryMap[countryCode]) {
        return countryMap[countryCode];
    }

    const alpha2 = countries.alpha3ToAlpha2(countryCode);

    if (alpha2) {
        return countries.getName(alpha2, "pt");
    }

    return countryCode;
}