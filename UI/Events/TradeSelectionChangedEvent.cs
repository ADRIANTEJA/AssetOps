

using Prism.Events;

namespace UI.Events
{
    public class TradeSelectionChangedEvent : PubSubEvent<TradeSelectionChangedDataBundle>
    {
    }

    public class TradeSelectionChangedDataBundle
    {
        public int TradeId { get; set; }
        public bool IsSelected { get; set; }
    }
}


