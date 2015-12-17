using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// ISetForWUC 的摘要说明
/// </summary>
public interface ISetForWUC
{
    DataSet dsFPZ { set; }
    Hashtable htPP { set; }
}