namespace AttackOnDotnetMvcCore.Models
{
    public class Technique
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TacticName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Url { get; set; }
    }
}