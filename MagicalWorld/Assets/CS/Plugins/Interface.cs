using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public interface IMyType
{
    void Start();
}
public interface IProtobufMessage
{
    object GetInstance();
}