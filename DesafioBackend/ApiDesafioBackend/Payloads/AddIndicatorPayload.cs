using DesafioBackend.Indicators;

namespace ApiDesafioBackend.Payloads
{
    public struct AddIndicatorPayload
    {
        public string Name { get; init; }
        public EnumResult ResultType { get; init; }
    }
}
