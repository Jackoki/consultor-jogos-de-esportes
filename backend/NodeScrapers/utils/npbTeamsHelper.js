const teamMap = {
    S: "Tokyo Yakult Swallows",
    G: "Yomiuri Giants",
    T: "Hanshin Tigers",
    D: "Chunichi Dragons",
    F: "Hokkaido Nippon-Ham Fighters",
    B: "ORIX Buffaloes",
    E: "Tohoku Rakuten Golden Eagles",
    M: "Chiba Lotte Marines",
    H: "Fukuoka SoftBank Hawks",
    L: "Saitama Seibu Lions",
    DB: "YOKOHAMA DeNA BAYSTARS",
    C: "Hiroshima Toyo Carp",
};

export function getTeamName(teamCode) {
    if (!teamCode) {
        return null;
    }

    teamCode = teamCode.trim().toUpperCase();

    if (teamMap[teamCode]) {
        return teamMap[teamCode];
    }

    return teamCode;
}