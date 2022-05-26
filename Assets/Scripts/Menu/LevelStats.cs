using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AimTrainer.Utils
{
    public class LevelStats
    {
        public static int BASIC_TARGETS_MAX = 10;
        public static int BASIC_SCORE_MAX = 100 * BASIC_TARGETS_MAX;
        // Slowest time
        public static int BASIC_TIME_MAX = 20;
        // Fastest time
        public static int BASIC_TIME_MIN = 5;
        public static float BASIC_KPS_MAX = 1.7f;
        public static float BASIC_KPS_MIN = 0.1f;
        public static int BASIC_TTK_MAX = 500;
        public static int BASIC_TTK_MIN = 50;

        public static int MOVING_TARGETS_MAX = 10;
        public static int MOVING_SCORE_MAX = 100 * MOVING_TARGETS_MAX;
        // Slowest time
        public static int MOVING_TIME_MAX = 20;
        // Fastest time
        public static int MOVING_TIME_MIN = 5;
        public static float MOVING_KPS_MAX = 1.7f;
        public static float MOVING_KPS_MIN = 0.1f;
        public static int MOVING_TTK_MAX = 500;
        public static int MOVING_TTK_MIN = 50;

        public static int ANTICIPATION_TARGETS_MAX = 5;
        public static int ANTICIPATION_SCORE_MAX = 100 * ANTICIPATION_TARGETS_MAX;
        // Slowest time
        public static int ANTICIPATION_TIME_MAX = 30;
        // Fastest time
        public static int ANTICIPATION_TIME_MIN = 10;
        public static float ANTICIPATION_KPS_MAX = 2.5f;
        public static float ANTICIPATION_KPS_MIN = 0.2f;
        public static int ANTICIPATION_TTK_MAX = 500;
        public static int ANTICIPATION_TTK_MIN = 100;
    }
}
