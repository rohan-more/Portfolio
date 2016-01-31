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
<table class=layout cellspacing=0>
<tr>

<td class=sidebar>
<img src="logo_s.jpg" class="logo"><br><br>
<a href="action.jsp" class=sideBarItem>Home</a>
<span class="currentBarItem">View Menu</span>
<a href="SetRedirectServlet" class=sideBarItem>My Account</a>
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
<%= session.getAttribute("menuView")%>

</td>

</tr>
</table>
</body>