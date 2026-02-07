using System;
using System.Collections;
using System.Collections.Generic;

namespace GameComponents.Helpers;

public static class ArrayHelper 
{
    // resizing arrays.
    public static void Resize<T>(ref T[] _buffer, int newCap) 
    {
        _buffer = new T[newCap];
    }
    
    public static T[] ResizeAndGetOldBuffer<T>(ref T[] _buffer, int newCap) 
    {
        var oldBuffer = _buffer;
        _buffer = new T[newCap];
        return oldBuffer;
    }
    
    // resizing while copying elements into new Array.
    
    public static void CopyAndResize<T>(ref T[] collection, int newCap, int length, int sourceIndex = 0, int sourceDestIndex = 0)
    {
        T[] destinationArray = new T[newCap];
        Array.Copy(collection, sourceIndex, destinationArray, sourceDestIndex, length);
        collection = destinationArray;
    }
    
    public static void EmptyOut<T>(ref T[] arrayToEmptyOut) 
    {
        var emptyArray = new T[arrayToEmptyOut.Length];
        arrayToEmptyOut = emptyArray;
    }
}