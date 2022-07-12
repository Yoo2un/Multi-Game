using System;
using System.Collections;

namespace Unity.VisualScripting.FullSerializer
{
    public class fsArrayConverter : fsConverter
    {
        public override bool CanProcess(Type type)
        {
            return type.IsArray;
        }

        public override bool RequestCycleSupport(Type storageType)
        {
            return false;
        }

        public override bool RequestInheritanceSupport(Type storageType)
        {
            return false;
        }

        public override fsRes