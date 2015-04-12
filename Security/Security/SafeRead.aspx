<%@ Page Language="C#" AutoEventWireup="true"  %>
<% @Import Namespace="Security.App_Code" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Safe From XSS Attacks Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <thead><tr><th>ID</th><th>Name</th><th>GPA</th></tr></thead>
            <tbody>           
             <%
                foreach (Student temp in DatabaseAccess.ReadDataSafe())
                {
                    Response.Write("<tr><td>" + temp.id + "</td><td name='Name'>" + temp.name + "</td><td>" + temp.gpa + "</td></tr>");
                }
             %>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>