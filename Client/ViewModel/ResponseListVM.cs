namespace Client.ViewModel
{
    public class ResponseListVM<Entity>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<Entity> Data { get; set; }
    }
}
