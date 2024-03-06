namespace ProjectToProcessALargeAmountOfData.Domain.DTOs
{
    public record struct MultiplicationResultResponse
    {
        public int Number { get; set; }
        public int Multiplier { get; set; }
        public int Result { get; set; }
    }
}
