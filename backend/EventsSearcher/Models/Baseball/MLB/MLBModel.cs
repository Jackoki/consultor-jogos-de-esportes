namespace consultor_jogos_de_esportes.Models.Baseball
{
    public class MLBModel
    {
        public int GamePk { get; set; }
        public DateTime GameDate { get; set; }
        public MLBTeams Teams { get; set; } = new();
        public MLBVenue Venue { get; set; } = new();
        public DateTime DateTimeStart => GameDate;
        public DateTime DateTimeEnd => GameDate.AddHours(4);
        public string NameEvent =>$"{Teams.Away.Team.Name} x {Teams.Home.Team.Name}";
        public string CountryName => "USA";
    }

    public class MLBTeams
    {
        public MLBTeamSide Away { get; set; } = new();
        public MLBTeamSide Home { get; set; } = new();
    }

    public class MLBTeamSide
    {
        public MLBTeam Team { get; set; } = new();
    }

    public class MLBTeam
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class MLBVenue
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}