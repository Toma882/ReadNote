using System;
using UnityEngine;

//AttributeUsage 指定该属性可以应用的成员类型
[AttributeUsage(AttributeTargets.Field)]
public sealed class TimeAttribute : PropertyAttribute { }