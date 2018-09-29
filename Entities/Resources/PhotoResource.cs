namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class PhotoResource
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }
        public string Url { get; set; }
    }
}