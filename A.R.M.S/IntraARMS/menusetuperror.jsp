<html>
<head>
<link href="intranet.css" rel=stylesheet type="text/css">
<title>A.R.M.S. - Menu</title>
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
<a href="index.jsp" class=sideBarItem>Home</a>
<span class=currentBarItem>View Menu</span>
<a href="SetRedirectServlet" class=sideBarItem>My Account</a
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

