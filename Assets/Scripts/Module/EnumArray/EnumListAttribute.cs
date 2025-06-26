using System;
using UnityEngine;

namespace Module.EnumArray
{
    public class EnumArrayAttribute : PropertyAttribute
    {
        public Type EnumType { get; private set; }

        public EnumArrayAttribute(Type enumType)
        {
            EnumType = enumType;
        }
    }

    [Serializable]
    public class EnumArray<T>
    {
        [SerializeField] private T[] array;

        public T Get(int index)
        {
            return array[index];
        }
    }}