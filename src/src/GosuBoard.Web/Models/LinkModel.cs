namespace GosuBoard.Web.Models
{
    public class LinkModel
    {
        public LinkModel(string rel, string prompt, string href)
        {
            Rel = rel;
            Prompt = prompt;
            Href = href;
        }

        public readonly string Rel;
        public readonly string Href;
        public readonly string Prompt;
    }
}
