using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AjaxResponse
/// </summary>
public class AjaxResponse
{
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; }
    public object Data { get; set; }
}