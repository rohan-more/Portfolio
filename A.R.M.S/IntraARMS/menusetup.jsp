<html>
<head>
<link href="intranet.css" rel=stylesheet type="text/css">
<title>A.R.M.S. - Set Up Menu</title>
</head>

<body>
<% 
if (session.getAttribute("id")== null || session.getAttribute("id").equals(""))
    response.sendRedirect("index.jsp");
%>
<jsp:useBean id="menu" scope="session" class="intracupojava.MenuInfoBean" />
<form action='/intraarms/MenuServlet' method="get">
<table class=layout cellspacing=0>
<tr>

<td class=sidebar>
<img src="logo_s.jpg" class="logo"><br><br>
<a href="action.jsp" class=sideBarItem>Home</a>
<a href="MenuViewServlet" class="sideBarItem">View Menu</a>
<a href="SetRedirectServlet" class=sideBarItem>My Account</a>


<% 
String temp= (String)session.getAttribute("permission");
if (temp!= null){
    temp= temp.toLowerCase();}
if (temp.equals("m") || temp.equals("e")
    || temp.equals("s") || temp.equals("a")){ 
if (temp.equals("m") 
    || temp.equals("s") || temp.equals("a")){ %>
<span class="currentBarItem">Account Management</span>
<%}%>
<a href="MenuManagementServlet" class="sideBarItem">Menu Management</a>
<%} else response.sendRedirect("/arms");%>
<a href="SignOutServlet" class="sideBarItem">Sign Out</a><br>
</td>


<td class=content>

<!-- This next line is the main part of this page. It ouputs the menu -->
<%if (session.getAttribute("menuinuse")!= null){%>
<%=session.getAttribute("menuinuse")%>
<%}%>
<p>&nbsp;</p>
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
<input type='submit' name='action' value='Submit'>
</td></tr></table>
</td>

</tr>
</table>
</form>
</body>
</html>

