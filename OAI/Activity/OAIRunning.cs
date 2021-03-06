﻿namespace OAI.Activity
{
    public class OAIRunning
    {
        private static bool Running = false;

        public static int ActiveThreads = 0;

        public static bool Active
        {
            get
            {
                return Running;
            }
            set
            {
                // Notify other background threads to stop running
                Running = value;
            }
        }
    }
}
