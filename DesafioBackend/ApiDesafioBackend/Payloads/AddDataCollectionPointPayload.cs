namespace ApiDesafioBackend.Payloads
{
    public struct AddDataCollectionPointPayload
    {
        public DateTime Date { get; init; }
        public double Value { get; init; }
    }
}
