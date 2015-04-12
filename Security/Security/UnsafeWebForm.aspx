
<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" %>
<%@ Import Namespace = "Security.App_Code" %>
<%@ Import Namespace="Security" %>
<!DOCTYPE html>

<%
	if (!string.IsNullOrEmpty(Request.Unvalidated.Form["namebox"]) && (!string.IsNullOrEmpty(Request.Form["gpaBox"])))
		DatabaseAccess.CreateDataUnsafe(Request.Unvalidated.Form["namebox"], Request.Form["gpaBox"]);

%>;

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Grade (String Concatination) Test Page</title>
</head>
<body>
    <form id="form1" runat="server" action="UnsafeWebForm.aspx" method="post">
    <div>
        <h1>Student Grade (String Concatenation) Test Page</h1>
        <input type="text" name="nameBox" value=""/>
        <input type="text" name="gpaBox" value=""/>
        <input type="submit" name="submit" />
    </div>
    </form>
</body>
</html>
