import countries from "i18n-iso-countries";
import en from "i18n-iso-countries/langs/en.json";

countries.registerLocale(en);

const countryMap = {
    KSA: "Saudi Arabia",
    ENG: "England",
    SCO: "Scotland",
    WAL: "Wales",
    UAE: "United Arab Emirates",
    MNC: "Monaco",
    NED: "The Netherlands",
    GER: "Germany",
    CRO: "Croatia",
    MGL: "Mongolia",
    GRE: "Greece",
    BOT: "Botswana",
    RSA: "South Africa",
    SUI: "Switzerland",
    NGR: "Nigeria",
    CRC: "Costa Rica",
    POR: "Portugal",
    SRI: "Sri Lanka",
    MAS: "Malaysia",
    KOS: "Kosovo",
    IOM: "Isle of Man",
    ONL: "Online",
    TBD: "To Be Determined",
    TBA: "To Be Announced"
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
        return countries.getName(alpha2, "en");
    }

    return countryCode;
}