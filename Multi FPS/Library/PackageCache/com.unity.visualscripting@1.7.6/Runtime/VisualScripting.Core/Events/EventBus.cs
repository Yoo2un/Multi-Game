using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
    public static class EventBus
    {
        static EventBus()
        {
            events = new Dictionary<EventHook, HashSet<Delegate>>(new EventHookComparer());
        }

        private static readonly Dictionary<EventHook, HashSet<Delegate>> events;

        public static void Register<TArgs>(EventHook hook, Action<TArgs> handler)
        {
            if (!events.TryGetValue(hook, out var handlers