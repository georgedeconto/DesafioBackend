using DesafioBackend.Indicators;

namespace ApiDesafioBackend.Payloads
{
    public struct EditIndicatorPayload
    {
        public string Name { get; init; }
        public EnumResult ResultType { get; init; }
    }
}
