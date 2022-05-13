namespace InterfaceLib
{
    public class GebruikerDTO
    {
        public int ID { get; set; }
        private static int PlusÉénID = 1;        
        public string? Gerbuikersnaam { get; set;  }
        public string Alias { get; set; }
        public List<OutfitDTO> Outfits { get; set; } = new List<OutfitDTO>();
        public List<OnderdeelDTO> Onderdelen { get; set; } = new List<OnderdeelDTO>();

        public GebruikerDTO(string gerbuikersnaam, string alias)
        {
            this.Gerbuikersnaam = gerbuikersnaam;
            this.Alias = alias;
            this.ID = PlusÉénID;
            PlusÉénID++;
        }
    }
}