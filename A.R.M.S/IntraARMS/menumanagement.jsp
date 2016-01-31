<html>
<head>
<link href="intranet.css" rel=stylesheet type="text/css">
<title>A.R.M.S. - Manage Menu</title>
</head>

<body>
<% 
if (session.getAttribute("id")== null || session.getAttribute("id").equals(""))
    response.sendRedirect("index.jsp");
%>
<jsp:useBean id="account" scope="session" class="intracupojava.AccountInfoBean" />
<form action='/intraarms/MenuManagementServlet' method="get">
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
<a href="AccountManagementServlet" class="sideBarItem">Account Management</a><br />
<%}%>
<span class="currentBarItem">Menu Management</span>
<%} else response.sendRedirect("/arms");%>
<a href="SignOutServlet" class="sideBarItem">Sign Out</a><br>
</td>


<td class=content>

<!-- This next line is the main part of this page. It ouputs the menu -->
<%if (session.getAttribute("menulist")!= null){%>
<%=(String)session.getAttribute("menulist")%>

<input type="hidden" name='hidden' value='y'>
<input type='submit' name='submit' value='proceed'>
<%}%>
</td>

</tr>
</table>
</form>
</body>
</html>

