<%@ WebHandler Language="C#" Class="$=table.Name$Handler" %>

using System;
using System.Collections.Generic;
using System.Web;
using Cinar.Database;
using $=db.Name$.API;
using $=db.Name$.API.Entity;
using $=db.Name$.API.Staff.Handlers;

public class $=table.Name$Handler : BaseEntityHandler<$=table.Name$>
{

}