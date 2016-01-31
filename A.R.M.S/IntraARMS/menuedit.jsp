<html>
<head>
<link href="intranet.css" rel=stylesheet type="text/css">
<title>A.R.M.S. - Edit Menu</title>
</head>

<body>
<% 
if (session.getAttribute("id")== null || session.getAttribute("id").equals(""))
    response.sendRedirect("index.jsp");
%>
<jsp:useBean id="menu" scope="session" class="intracupojava.MenuInfoBean" />
<form action='/intraarms/MenuEditServlet' method="get">
<table class=layout cellspacing=0>
<tr>

<td class=sidebar>
<img src="logo_s.jpg" class="logo"><br><br>
<a href="action.jsp" class=sideBarItem>Home</a>
<a href="MenuViewServlet" class="sideBarItem">View Menu</a>
<span class="currentBarItem">My Account</span>
<% 
String temp= (String)session.getAttribute("permission");
if (temp!= null){
    temp= temp.toLowerCase();}
if (temp.equals("m") || temp.equals("e")
    || temp.equals("s") || temp.equals("a")){ 
if (temp.equals("m") 
    || temp.equals("s") || temp.equals("a")){ %>
<a href="AccountManagementServlet" class="sideBarItem">Account Management</a><br />
<%}%>
<a href="MenuManagementServlet" class="sideBarItem">Menu Management</a>
<%} else response.sendRedirect("/arms");%>
<a href="SignOutServlet" class="sideBarItem">Sign Out</a><br>
</td>


<td class=content>

<!-- This next line is the main part of this page. It ouputs the menu -->
<table width="100%"  border="0">
  <tr>
    <tr><td><%=menu.flagName()%>Menu Name: </td>
    <td><input type="text" name="name" value="<jsp:getProperty name='menu' property='name' />"></td></tr>
    <tr><td>Description:</td>
    <td><textarea name="description"><jsp:getProperty name='menu' property='description' /></textarea></td></tr>
    <tr><td><%=menu.flagPrice()%>Price:</td>
    <td><input type="text" name="price" value="<jsp:getProperty name='menu' property='price' />"></td></tr>
    <tr><td>Availability: </td>
    <td><input type="checkbox" name="availability" 
        <% if (menu.getAvailability().equals("1"))
           out.write("checked");%>></td></tr>
  </tr>
</table>
<p>&nbsp;</p>
<table width="100%">
<tr>
<td align="left"><input type='submit' name='action' value='   Edit   '></td>
<td align="right"><input type='submit' name='action' value='Delete' onclick="alert('Are you sure you want to delete the Menu Item!?'); return true"></td>
</tr>
</table>

</td></tr></table>
</td>

</tr>
</table>
</form>
</body>
</html>


