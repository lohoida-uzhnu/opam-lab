namespace opam_lab1 
{
    public struct Service
    {
        public int Id;
        public string Name;
        public double Price;
        public double Duration;
        public int Quantity;

        public Service(int id, string name, double price, double duration, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Duration = duration;
            Quantity = quantity;
        }
    }
}