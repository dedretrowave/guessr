using System;

namespace EventBus
{
    [Serializable]
    public enum EventName
    {
        // NAVIGATION
        ON_BACK_TO_MENU,
        // DIFFERS
        ON_ALL_DIFFERS_FOUND,
        ON_NEXT_LEVEL,
        ON_DIFFER_FOUND,
        ON_WRONG_DIFFER_FOUND,
        ON_REPLAY,
        ON_HINT,
        // GAME LOOP
        ON_ALL_LEVELS_PASSED,
        // TIMER
        ON_TIMEOUT,
        ON_MORE_TIME,
        ON_PAUSE,
        ON_UNPAUSE,
        // ADS
        ON_AD_OPEN,
        ON_AD_WATCHED,
        ON_REWARDED_OPEN,
        ON_REWARDED_CLOSED,
        ON_REWARDED_WATCHED,
    }
}