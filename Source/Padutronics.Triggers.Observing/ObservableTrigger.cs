using Padutronics.Observing;
using System.Collections.Generic;

namespace Padutronics.Triggers.Observing;

public sealed class ObservableTrigger<T> : TriggerBase
{
    private readonly T? expectedValue;

    public ObservableTrigger(IObservable<T> valueHolder, T? expectedValue) :
        base(isActive: EqualityComparer<T>.Default.Equals(valueHolder.Value, expectedValue))
    {
        this.expectedValue = expectedValue;

        valueHolder.ValueChanged += ValueHolder_ValueChanged;
    }

    private void ValueHolder_ValueChanged(object? sender, ValueChangedEventArgs<T?> e)
    {
        if (EqualityComparer<T>.Default.Equals(e.NewValue, expectedValue))
        {
            IsActive = true;
        }
        else if (EqualityComparer<T>.Default.Equals(e.OldValue, expectedValue))
        {
            IsActive = false;
        }
    }
}