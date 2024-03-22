namespace Backend
{
    public class Arzt
    {

        public int Arzt_ID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Adresse { get; set; }
        public string Ort { get; set; }
        public DateTime Oefnugszeit { get; set; }
        public DateTime Schließzeit { get; set; }

    }
}