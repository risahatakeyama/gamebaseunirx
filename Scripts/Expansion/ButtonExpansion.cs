using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace FictionProject.Expansion
{
    public static class ButtonExpansion
    {
        public enum ButtonClickIntent
        {
            Normal,
            OnlyOneTap,
            IntervalTap,
        }

        public static IObservable<Unit> OnClickIntentAsObservable(this Button button, ButtonClickIntent intent = ButtonClickIntent.IntervalTap, float coolTime = 0.5f)
        {
            var clickObservable = button.OnClickAsObservable();
            //.Where(_ => !Input.touchSupported ||(Input.touchSupported & Input.touches.Length <= 1));

            switch (intent)
            {
                case ButtonClickIntent.OnlyOneTap:
                    return clickObservable.First();

                case ButtonClickIntent.IntervalTap:
                    return clickObservable.ThrottleFirst(TimeSpan.FromSeconds(coolTime));

                case ButtonClickIntent.Normal:
                default:
                    return clickObservable;
            }
        }

        public static IObservable<long> OnHoldAsObservable(this Button button, Action releaseEvent = null, double ms = 350)
        {
            return button.OnPointerDownAsObservable()
                         .SelectMany(_ => Observable.Timer(TimeSpan.FromMilliseconds(ms)))
                         .TakeUntil(button.OnPointerUpAsObservable()
                         .Do(_ =>
                         {
                             if (releaseEvent != null)
                                 releaseEvent();
                         }))
                         .RepeatUntilDestroy(button)
                         .AsObservable();
        }
    }
}
